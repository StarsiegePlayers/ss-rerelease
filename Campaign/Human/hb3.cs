$LEVEL = 1;

function win()
{  
	missionObjective1.status = *IDSTR_OBJ_COMPLETED;
	missionObjective2.status = *IDSTR_OBJ_COMPLETED;
	missionObjective3.status = *IDSTR_OBJ_COMPLETED;
	updatePlanetInventory(hb3);
   schedule("forceToDebrief();", 3.0);
}

function god()
{
   $god = true;
}
function ungod()
{
   $god = false;
}
function restore()
{
}

/////////////////////////////////////////////////////////////////////////////
//                                                                         //
//  ---------------------------------------------------------------------  //
//   Mission HB3                                                           //
//  ---------------------------------------------------------------------  //
//              MISSION OBJECTIVES                                         //         
//   Primary                                                               //         
//   Objective 1 - Proceed to Nav Alpha and Secure the Imperial            //
//                  Landing Facility                                       //         
//                                                                         //         
//   Secondary                                                             //         
//   Objective 2 - Destory any resistance you may encounter                //         
//   Objective 3 - Down any transport ship you encounter                   //         
//                                                                         //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////



MissionBriefInfo missionBriefInfo
{
   campaign             = *IDSTR_HB3_CAMPAIGN;             
   title                = *IDSTR_HB3_TITLE;      
   planet               = *IDSTR_PLANET_MARS;     
   location             = *IDSTR_HB3_LOCATION; 
   dateOnMissionEnd     = *IDSTR_HB3_DATE;               
   shortDesc            = *IDSTR_HB3_SHORTBRIEF;
   longDescRichText     = *IDSTR_HB3_LONGBRIEF;
   media                = *IDSTR_HB3_MEDIA;
   successDescRichText  = *IDSTR_HB3_DEBRIEF_SUCC;
   failDescRichText     = *IDSTR_HB3_DEBRIEF_FAIL;
   nextMission          = *IDSTR_HB3_NEXTMISSION;
   soundVol             = "HB3.vol";
   preDebriefRec        = "cinHB3.rec";
   successWavFile       = "HB3_Debriefing.wav";
};

MissionBriefObjective missionObjective1
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HB3_OBJ1_SHORT;
   longTxt              = *IDSTR_HB3_OBJ1_LONG;
   bmpName              = *IDSTR_HB3_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2
{
   isPrimary            = false;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HB3_OBJ2_SHORT;
   longTxt              = *IDSTR_HB3_OBJ2_LONG;
   bmpName              = *IDSTR_HB3_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3
{
   isPrimary            = false;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HB3_OBJ3_SHORT;
   longTxt              = *IDSTR_HB3_OBJ3_LONG;
   bmpName              = *IDSTR_HB3_OBJ3_BMPNAME;
};                        


Pilot Harabec
{
   id = 29;
   
   name = Harabec;
   skill = 1.0;
   accuracy = 1.0;
   aggressiveness = 0.9;
   activateDist = 250.0;
   deactivateBuff = 100.0;
   targetFreq = 5.0;
   trackFreq = 1.0;
   fireFreq = 0.2;
   LOSFreq = 0.1;
   orderFreq = 3.0;    
};

//-----------------------------------------------------------------------------
function player::onAdd(%playerNum)
{
    $playerNum = %playerNum;
}
//-----------------------------------------------------------------------------
function onMissionStart(%playerNum)
{
   dbecho($LEVEL, "onMissionStart(" @ %playerNum @ ")");
   
   setHostile(*IDSTR_TEAM_RED, *IDSTR_TEAM_YELLOW); 
   setHostile(*IDSTR_TEAM_YELLOW, *IDSTR_TEAM_RED); 
   setHostile(*IDSTR_TEAM_PURPLE);                  

   marsSounds();

   $qwerty           =  3;
   $missionFailed    = false;
   $missionStart     = false; 

   cdAudioCycle(10, 7);
}

//-----------------------------------------------------------------------------
function onSPClientInit()
{
   init();
}

//-----------------------------------------------------------------------------

function init()
{
   dbecho($LEVEL, "init()");
   
   initFormations();
   initPlayer();
   initTransport();
   initNavPoints();
   initDrones();
   initImperials();
   initBase();
   initEvac();
   initFlyers();     
   initMission();
}

///////////////////////////////////////////////////////////////////////////////
// INIT Functions                                                            //
///////////////////////////////////////////////////////////////////////////////
//-----------------------------------------------------------------------------
function initFormations()
{
    dbecho($LEVEL, "initFormations()");
    
    newFormation(Follow,    0,0,0,  
                            0,-40,0,
                            0,-80,0);
                            
    newFormation(Wedge,     0,  0,  0,  
                          -40,-40,  0,
                           40,-40,  0,
                          -80,-80,  0,
                           80, 80,  0 );
}

function initMission()
{
   $missionStart = true;
}
///////////////////////////////////////////////////////////////////////////////
// Player Functions                                                          //
///////////////////////////////////////////////////////////////////////////////
function initPlayer()
{
   dbecho($LEVEL, "initPlayer()");
   $playerId = playerManager::playerNumToVehicleId($playerNum);
   $playerSquad = myGetObjectId(playerSquad);
   
   %num    = 1;
   $playerSquad.num[%num] = GetNextObject($playerSquad, $playerId);
   while($playerSquad.num[%num] != 0)
   {
      if($playerSquad.num[%num] != $playerId)
         setHercOwner( $playerSquad.num[%num], "MissionGroup\\vehicles\\Flyers" );
      
      %num++;
      $playerSquad.num[%num] = GetNextObject($playerSquad, $playerSquad.num[%num - 1]);
   }
}

function isPlayerSafe()
{
   dbecho(1, "isPlayerSafe()");
   
   if( ( IsSafe( *IDSTR_TEAM_YELLOW, $playerId, 1000 ) ) &&
       ( getDistance( $playerId, $navCharlie ) < 300 )       )
      schedule("objectiveCompleted(1);", 9.0);
   else
      schedule("isPlayerSafe();", 1.0);
}


///////////////////////////////////////////////////////////////////////////////
// Imperial Functions                                                        //
///////////////////////////////////////////////////////////////////////////////

function initImperials()
{
   dbecho($LEVEL, "initImperials()");

   $enemyNum         = 8;
   
   $alpha            = myGetObjectId("MissionGroup\\vehicles\\Alpha");
   $bravo            = myGetObjectId("MissionGroup\\vehicles\\Bravo");
   $knight           = myGetObjectId("MissionGroup\\vehicles\\Knight");
   $talon            = myGetObjectId("MissionGroup\\vehicles\\Talons");
   
   // artillery is in Flyers group to prevent player squad from auto attacking artillery
   // Flyers group owns player squad
   $ar1                = myGetObjectId("MissionGroup\\vehicles\\Flyers\\Ar1");
   $ar2                = myGetObjectId("MissionGroup\\vehicles\\Flyers\\Ar2");
   
   $alpha.attacked   = false;
   $bravo.attacked   = false;
   $knight.attacked  = false;
   $talon.attacked   = false;
   
   $ar1.outOfAmmo = false;
   $ar2.outOfAmmo = false;

   $talon.num        = 2;
   $talon.aNum       = 0;
   
   $knight.num       = 3;
   
   $alpha.spoke      = false;
   $bravo.spoke      = false;
              
   $knight1             = myGetObjectId("MissionGroup\\vehicles\\Knight\\Apocalypse1");
   $knight2             = myGetObjectId("MissionGroup\\vehicles\\Knight\\Gorgon1");
   
   $talon.atDropShip    = false;

   $knight1.attacked    = false;
   $knight2.attacked    = false;
   
   $turret              = myGetObjectId("MissionGroup\\Turret\\Turret");

   $alpha.route         = myGetObjectId("MissionGroup\\Patrols\\Alpha");
   $bravo.route         = myGetObjectId("MissionGroup\\Patrols\\Bravo");
   $talon.attackRoute   = myGetObjectId("MissionGroup\\Patrols\\Talon\\Attack");
   $talon.retreatRoute  = myGetObjectId("MissionGroup\\Patrols\\Talon\\retreat");

   $dropShipPoint1      = myGetObjectId("MissionGroup\\Patrols\\DropShip1");
   $dropShipPoint2      = myGetObjectId("MissionGroup\\Patrols\\DropShip2");
   $dropShipPoint3      = myGetObjectId("MissionGroup\\Patrols\\DropShip3");
   $dropShipPoint4      = myGetObjectId("MissionGroup\\Patrols\\DropShip4");
   
   $alpha.lead          = myGetObjectId("MissionGroup\\vehicles\\Alpha\\Bas1");
   $bravo.lead          = myGetObjectId("MissionGroup\\vehicles\\Bravo\\Dis1");
   $knight.lead         = myGetObjectId("MissionGroup\\vehicles\\Knight\\Apocalypse1");
   $talon.lead          = myGetObjectId("MissionGroup\\vehicles\\Talons\\Talon1");
   

   order($alpha.lead    ,MakeLeader , true );
   order($bravo.lead    ,MakeLeader , true );
   order($knight.lead   ,MakeLeader , true );
   order($talon.lead    ,MakeLeader , true );

   order($alpha , Formation, Wedge);
   order($bravo , Formation, Wedge);
   order($talon , Formation, Wedge);
   
   order($alpha , Speed, High);
   order($bravo , Speed, High);
   order($knight, Speed, High);
   order($talon,  Speed, High);

   order($alpha,  HoldPosition, false);
   order($bravo,  HoldPosition, false);
   order($knight, HoldPosition, false);
   order($talon,  HoldPosition, false);
   order($ar1,  HoldPosition, true);
   order($ar2,  HoldPosition, true);

   order($alpha,  HoldFire, false);
   order($bravo,  HoldFire, false);
   order($knight, HoldFire, false);
   order($talon,  HoldFire, false);
   order($ar1,  HoldFire, false);
   order($ar2,  HoldFire, false);
   
   checkDistance($playerId, $alpha.lead, 600, onEnterWayPoint, 5.0);
   checkDistance($playerId, $bravo.lead, 500, onEnterBravo, 5.0);
   
   order($talon.lead, Guard, $talon.attackRoute);
   checkDistance($playerId,  $talon.lead, 250, onTalonClose, 1.0);
    
}
//-----------------------------------------------------------------------------
function talon::vehicle::onAttacked(%this, %who)
{
//   dbecho($LEVEL, "talon::vehicle::onAttacked(" @ %this @ ", " @ %who @ ")");

   %group = getGroup(%this);
   
   %group.aNum++;
   echo("group.aNum = " @ %group.aNum);

   if(%group.aNum < 3 || %group.aNum > 3)
      return;
   echo("order talon.lead to retreat, " @ $talon.lead);
      
   order($talon,  HoldPosition,  true);
   order($talon,  Formation,     Wedge);
   order($talon.lead, Guard, $talon.retreatRoute);
}

//-----------------------------------------------------------------------------
function talon::vehicle::onArrived(%this, %where)
{
//   dbecho($LEVEL, "talon::vehicle::onArrived(" @ %this @ ", " @ %where @ ")");

   if(getObjectName(%where) != "Last")
      return;
  
   $talon.atDropShip = true;

   order($talon,        HoldPosision,  false);
   order($talon.lead,   Guard,         $dropShipPoint1);
}

//-----------------------------------------------------------------------------

function talon::vehicle::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "talon::vehicle::onDestroyed(" @ %this @ ", " @ %who @ ")");
   
   $enemyNum--;
   $talon.num--;
   
   if($enemyNum <= 0)
      objectiveCompleted(2);
}
   


function imp::vehicle::onAttacked(%this, %who)
{
   if(%this != %who)
   { 
      %group = getGroup(%this);
      
      if( %group == $knight && %this.attacked == false)
      {
         dbecho($LEVEL, "imp::vehicle::onAttacked(" @ %this @ ", " @ %who @ ")");
         
         %this.attacked = true;
         order(%this, Attack, %who);
         knightAttack();
      }
      else
      if( !(%group.attacked) )
      {  
         dbecho($LEVEL, "imp::vehicle::onAttacked(" @ %this @ ", " @ %who @ ")");
         %group.attacked = true;
         order( getLeader(%group), Attack, playerSquad );
      
         if( (%group == $alpha) && !($alpha.spoke) )
         {
             $alpha.spoke = true;
             say(0, %group, *IDSTR_HB3_ISA01, "HB3_ISA01.wav");
         }
         else if( (%group == $bravo) && !($bravo.spoke) )
         {
             $bravo.spoke = true;
             say(0, $bravo, *IDSTR_HB3_BPT01, "HB3_BPT01.wav");
         }
      }
   }
}


//-----------------------------------------------------------------------------

function imp::vehicle::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "imp::vehicle::onDestroyed(" @ %this @ ", " @ %who @ ")");

   $enemyNum--;
   if($enemyNum <= 0)
      objectiveCompleted(2);
}

function knight::vehicle::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "knight::vehicle::onDestroyed(" @ %this @ ", " @ %who @ ")");
   $knight.num--;
   $enemyNum--;

   killChannel(%this);
   
   if($knight.num <= 0)
      isPlayerSafe();

   if($enemyNum <= 0)
      objectiveCompleted(2);
 
}

function outer::vehicle::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "outer::vehicle::onDestroyed(" @ %this @ ", " @ %who @ ")");
   killChannel(%this);
}

function knightAttack()
{
   if( $knight.attacked == false )
   {
      dbecho($LEVEL, "knightAttack()");
      
      $knight.attacked = true;   
 
      say(0, $knight.lead, *IDSTR_HB3_KNI01, "HB3_KNI01.wav");
 
      order($turret, Attack, pick(playerSquad) );
      schedule("order(" @ $knight2 @ ", Attack,  playerSquad );",  3.0 );
      schedule("order(" @ $knight1 @ ", Attack,  playerSquad );", 20.0 );
   }
   
}

function onEnterBravo()
{
   dbecho($LEVEL, "onEnterBravo()");
   order( $bravo.lead, Attack, pick(playerSquad) );
   if( !($bravo.spoke) )
   {  
      $bravo.spoke = true;
      say(0, $bravo, *IDSTR_HB3_BPT01, "HB3_BPT01.wav");
   }
}

function onTalonClose()
{
   dbecho($LEVEL, "onTalonClose()");
   order($talon,  HoldPosition,  true);
   order($talon,  Formation,     Wedge);
   order($talon.lead, Guard, $talon.retreatRoute);
}

///////////////////////////////////////////////////////////////////////////////
// Drone Functions                                                           //
///////////////////////////////////////////////////////////////////////////////

function initDrones()
{
    dbecho($LEVEL, "initDrones()");
    
    $droneGroup         = myGetObjectId("MissionGroup\\vehicles\\Drone");
  
    $utility1           = myGetObjectId("MissionGroup\\vehicles\\Drone\\Utility1");
    $utility2           = myGetObjectId("MissionGroup\\vehicles\\Drone\\Utility2");
    $cargo              = myGetObjectId("MissionGroup\\vehicles\\Drone\\Cargo");
    $fuel               = myGetObjectId("MissionGroup\\vehicles\\Drone\\Fuel");
    
    $utility1.rested    = false;
    $utility2.rested    = false;
    $cargo.rested       = false;
    $fuel.rested        = false;

    $utility1Route      = myGetObjectId("MissionGroup\\Patrols\\Drone\\Utility1");
    $utility2Route      = myGetObjectId("MissionGroup\\Patrols\\Drone\\Utility2");
    $cargoRoute         = myGetObjectId("MissionGroup\\Patrols\\Drone\\Cargo");
    $fuelRoute          = myGetObjectId("MissionGroup\\Patrols\\Drone\\Fuel");
    
    $escapeRoute        = myGetObjectId("MissionGroup\\Patrols\\Drone\\escape");
    $escapePoint        = 0;
    
    order($utility1, Guard, $utility1Route );
    order($utility2, Guard, $utility2Route );
    order($cargo   , Guard, $cargoRoute     );
    order($fuel    , Guard, $fuelRoute     );

    order($utility1, HoldPosition, true);
    order($utility2, HoldPosition, true);
    order($cargo   , HoldPosition, true);
    order($fuel    , HoldPosition, true);
    
    order($utility1, Speed, Low);
    order($utility2, Speed, Low);
}
//-----------------------------------------------------------------------------

function drone::vehicle::onArrived(%this, %where)
{
//    dbecho($LEVEL, "drone::vehicle::onArrived(" @ %this @ ", " @ %where @ ")");
    
    if( getObjectName(%where) == "Pause" )
        pause(%this, %where);
}
//-----------------------------------------------------------------------------

function pause(%who, %where)
{
//    dbecho($LEVEL, "pause(" @ %who @ ", " @ %where @ ")");
    
    order(%who, ShutDown, true);
    schedule("order(" @ %who @ ", ShutDown, false);", 10.0);
}
//-----------------------------------------------------------------------------

///////////////////////////////////////////////////////////////////////////////////
// Transport Functions                                                           //
///////////////////////////////////////////////////////////////////////////////////

function initTransport()
{
   dbecho($LEVEL, "initTransport()");
   
   $transportGroup = myGetObjectId("MissionGroup\\vehicles\\Transport");

   
   $transportGroup.num     = 2;
   $transportGroup.spoke   = false;
   
   $transport1 = myGetObjectId("MissionGroup\\vehicles\\Flyers\\Transport1");
   $transport2 = myGetObjectId("MissionGroup\\vehicles\\Flyers\\Transport2");
   $cinematic  = myGetObjectId("MissionGroup\\vehicles\\Flyers\\cinematic");
   $dropShip   = myGetObjectId("MissionGroup\\vehicles\\Flyers\\drop");
   
   $dropShip.away    = false;
   
   $transport1.alive = true;
   $transport2.alive = true;
   
   $transport1.attacked    = false;
   $transport2.attacked    = false;
   $cinematic.attacked     = false;
   $dropShip.attacked      = false;

   $transport1Route    = myGetObjectId("MissionGroup\\Patrols\\Transport\\Transport1");
   $transport2Route    = myGetObjectId("MissionGroup\\Patrols\\Transport\\Transport2");
   $cinematicRoute     = myGetObjectId("MissionGroup\\Patrols\\Transport\\cinematic");
   $dropShipRoute      = myGetObjectId("MissionGroup\\Patrols\\Transport\\drop");

   order($transport1, Speed, High);
   order($transport2, Speed, High);
   order($cinematic , Speed, High);
   order($dropShip  , Speed, High);
   
   order($transport1,  ShutDown, true);
   order($transport2,  ShutDown, true);
   order($dropShip,    ShutDown, true);
   
   order($transport1, height, 50, 600);
   order($transport2, height, 50, 600);
   order($cinematic , height, 50, 600);
   order($dropShip  , height, 50, 600);
   
   checkDistance($playerId, $cinematic, 1000, orderCinematic, 1.0);
    
}
//-----------------------------------------------------------------------------

function launchTransport(%this)
{
    if(%this == $transport1)
    {
        order($transport1, Guard, $transport1Route);
        if( $transport1.alive && !($transportGroup.spoke) )
        {
            $transportGroup.spoke = true;
            say(0, $transport1, *IDSTR_GEN_DS21, "GEN_2DSA01.WAV");
            schedule("launchTransport(" @ $transport2 @ ");", 30);
        }
    }
    if(%this == $transport2)
    {
        order($transport2, Guard, $transport2Route);
        if( $transport2.alive && !($transportGroup.spoke) )
        {
            $transportGroup.spoke = true;
            say(0, $transport2, *IDSTR_GEN_DS21, "GEN_2DSA01.WAV");
            schedule("launchTransport(" @ $transport1 @ ");", 30);
        }
    }
}
//-----------------------------------------------------------------------------

function transport::vehicle::onAttacked(%this, %who)
{
    if(%this != %who)
    {
        if( !(%this.attacked) )
        {
            %this.attacked = true;
            launchTransport(%this);
            knightAttack();
        }
    }
}

//-----------------------------------------------------------------------------

function transport::vehicle::onDestroyed(%this, %who)
{
    if( ($transportGroup.num >= 2) )
         say(0, $knight2, *IDSTR_GEN_ICC1, "GEN_ICCB01.wav");

    $transportGroup.num--;
    
    $qwerty--;
    if($qwerty == 0)
      awardBonus();    
} 
//-----------------------------------------------------------------------------
function drop::vehicle::onAttacked(%this, %who)
{
   if(%this.attacked || %this.away)
      return;
  
   dbecho($LEVEL, "drop::vehicle::onAttacked(" @ %this @ "," @ %who @ ")");
   
   %this.attacked = true;
   onEnterNavCharlieShort();
}
//-----------------------------------------------------------------------------
function drop::vehicle::onDestroyed(%this, %who)
{
    $transportGroup.num--;
    
    $qwerty--;
    if($qwerty == 0)
      awardBonus();    
}

//-----------------------------------------------------------------------------
function orderCinematic()
{
    dbecho($LEVEL, "orderCinematic()");
    echo("Distance to Cinematic = " @ getDistance($playerId, $cinematic));
    order($cinematic,   Guard,  $cinematicRoute);
}
 
///////////////////////////////////////////////////////////////////////////////////////
// Nav Point Functions                                                               //
///////////////////////////////////////////////////////////////////////////////////////

function initNavPoints()
{
   dbecho($LEVEL, "initNavPoints()");
   
   $navGroup = myGetObjectId("MissionGroup\\NavPoints");
   
   $navCharlie = myGetObjectId("MissionGroup\\NavPoints\\NavCharlie");
   $navBravo   = myGetObjectId("MissionGroup\\NavPoints\\NavBravo");
   $navAlpha   = myGetObjectId("MissionGroup\\NavPoints\\NavAlpha");
   $navBogus   = myGetObjectId("MissionGroup\\NavPoints\\Bogus");
   
   $navCharlie.set = false;
   
   setNavMarker($navAlpha,    true,    -1);
   setNavMarker($navBravo,    false);
   setNavMarker($navCharlie,  false);
   setNavMarker($navBogus,    false);
   
   checkDistance($playerId, $navCharlie, 2920, onEnterWayPoint, 5.0);
   checkDistance($playerId, $navCharlie, 1400, onEnterNavCharlieLong, 1.0); 
   checkDistance($playerId, $navCharlie,  800, onEnterNavCharlieShort, 1.0);
   
   checkBound($playerId, $navCharlie, 4000, "missionBoundWarning", 5.0); 
   checkBound($playerId, $navCharlie, 4400, "missionBoundFail", 5.0); 
}

function missionBoundWarning()
{
   say(0, $playerId, *IDSTR_GEN_TCV1, "GEN_TCV01.wav");
}

function missionBoundFail()
{
   say(0, $playerId, *IDSTR_GEN_TCV2, "GEN_TCV02.wav");
   missionFailed(playerLeftMissionArea);
}
//-----------------------------------------------------------------------------

function onEnterNavCharlieLong()
{
   dbecho($LEVEL, "onEnterNavCharlieLong()");
   echo("Distance to navCharlie = " @ getDistance($playerId, $navCharlie));
   
   setNavMarker($navCharlie, true, -1);

   schedule("order(" @ $ar1 @ ", Attack, pick(playerSquad) );", 5.0 ); 
   order($ar2, Attack, pick(playerSquad) ); 
   order($alpha.lead, Attack, playerSquad); 
   order($bravo.lead, Attack, playerSquad);
   
   order($dropShip, Guard, $dropShipRoute);
   schedule( "$dropShip.away = true;", 3.0);
}
//-----------------------------------------------------------------------------

function onEnterNavCharlieShort()
{
   if( !($navCharlie.set) )
   {   
      dbecho($LEVEL, "onEnterNavCharlieShort()");
    echo("Distance to navCharlie = " @ getDistance($playerId, $navCharlie));
   
      $navCharlie.set = true;

      order($ar1, Clear);
      order($ar2, Clear);
      order($ar1, Guard, $evacDroneEscape);
      order($ar2, Guard, $evacDroneEscape);

      order($alpha.lead, Attack, pick(playerSquad) ); 
      order($bravo.lead, Attack, pick(playerSquad) );
      
      setNavMarker($navBogus, false, -1);
      
      knightAttack();
      
      schedule("launchTransport(" @ $transport1 @ ");", 30);
      schedule("launchTransport(" @ $transport2 @ ");", 45);
   }
}
//-----------------------------------------------------------------------------

function onEnterWayPoint()
{
    dbecho($LEVEL, "onEnterWayPoint()");
    echo("Distance to navCharlie = " @ getDistance($playerId, $navCharlie));
    
    setNavMarker($navBravo, true, -1);
    
    if(!($navCharlie.set) && !($alpha.spoke))
    {
        order($alpha.lead, Attack, playerSquad);
        
        $alpha.spoke = true;
        say(0, $alpha.lead, *IDSTR_HB3_ISA01, "HB3_ISA01.wav");
    }
}



///////////////////////////////////////////////////////////////////////////////
// Base Functions                                                            //
///////////////////////////////////////////////////////////////////////////////
function initBase()
{
   $baseGroup = myGetObjectId("MissionGroup\\Base");
   $baseGroup.num = 4;
    
   $boomGroup1 =  myGetObjectId("MissionGroup\\Base\\BoomGroup1");
   $boomGroup2 =  myGetObjectId("MissionGroup\\Base\\BoomGroup2");
   $boomGroup3 =  myGetObjectId("MissionGroup\\Base\\BoomGroup3");
   $boomGroup4 =  myGetObjectId("MissionGroup\\Base\\BoomGroup4");
}

function structure::onAttacked(%this, %who)
{
   dbecho($LEVEL, "structure::onAttacked(" @ %this @ "," @ %who @ ")");

   if( getGroup(%who) != myGetObjectId(playerSquad) )
      return;
   
   onEnterNavCharlieShort();
}

function cin::structure::onAttacked(){}
function cin::structure::onDestroyed(){}
function rock::structure::onAttacked(){}
function rock::structure::onDestroyed(){}

function structure::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "structure::onDestroyed(" @ %this @ ", " @ %who @ ")");

   if( getGroup(%who) != myGetObjectId(playerSquad) )
      return;
      
   %group = getGroup(%this);
   
   if( getObjectName(%this) == "hMarsSupplyDump1")
   {
      $escapePoint = GetNextObject($escapeRoute, $escapePoint);
      
      if(%group == $boomGroup1)
         order($utility1, Guard, $escapePoint);
      else
      if(%group == $boomGroup2)
         order($utility2, Guard, $escapePoint);
   }
   else
   if( getObjectName(%this) == "xtroopercontainer1")
   {
      $escapePoint = GetNextObject($escapeRoute, $escapePoint);
      order($cargo, Guard, $escapePoint);
   }
   
   if( isGroupDestroyed(%group) )
      $baseGroup.num--;
}

function fuel::structure::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "fuel::structure::onDestroyed(" @ %this @ ", " @ %who @ ")");
   
   boomSequence(getGroup(%this));
   
   $escapePoint = GetNextObject($escapeRoute, $escapePoint);
   
   order($fuel, Guard, $escapePoint);

   if( isGroupDestroyed(%group) )
      $baseGroup.num--;
}

function turret::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "turret::onDestroyed(" @ %this @ ", " @ %who @ ")");
   $knight.num--;
   if($knight.num <= 0)
      isPlayerSafe();
}

function boomSequence(%group)
{
   dbecho($LEVEL, "boomSequence(" @ %group @ ")");
   
   %obj     = GetNextObject(%group, 0);
   %delay   = 0;
   
   while( %obj != 0 )
   {
      schedule("damageObject(" @ %obj @ ", 100000);", (%delay * 3) );
      %obj = GetNextObject(%group, %obj);
      %delay++;
   }
}   

///////////////////////////////////////////////////////////////////////////////
// Evac Functions                                                            //
///////////////////////////////////////////////////////////////////////////////
function initEvac()
{
   $evac1         = myGetObjectId("MissionGroup\\Evac\\Evac1");
   $evac1.flyer   = myGetObjectId("MissionGroup\\vehicles\\Flyers\\drop1");
   $evac1.utility = myGetObjectId("MissionGroup\\Evac\\Evac1\\Drone\\Utility");
   $evac1.cargo   = myGetObjectId("MissionGroup\\Evac\\Evac1\\Drone\\Cargo");
   
   $evac1.attacked         = false;

   $evac1.utility.rested   = false;
   $evac1.cargo.rested     = false;
   
   $evac1.flyer.route   = myGetObjectId("MissionGroup\\Evac\\Evac1\\Route\\Flyer");
   $evac1.utility.route = myGetObjectId("MissionGroup\\Evac\\Evac1\\Route\\Utility");
   $evac1.cargo.route   = myGetObjectId("MissionGroup\\Evac\\Evac1\\Route\\Cargo");
   
   order($evac1.flyer,     Shutdown,   true);
   order($evac1.flyer,     Height,     100,  600);
   order($evac1.utility,   Guard,   $evac1.utility.route);
   order($evac1.cargo,     Guard,   $evac1.cargo.route);
//-----------------------------------------------------------------------------
   $evac2         = myGetObjectId("MissionGroup\\Evac\\Evac2");
   $evac2.flyer   = myGetObjectId("MissionGroup\\vehicles\\Flyers\\drop2");
   $evac2.utility = myGetObjectId("MissionGroup\\Evac\\Evac2\\Drone\\Utility");
   $evac2.cargo   = myGetObjectId("MissionGroup\\Evac\\Evac2\\Drone\\Cargo");
   
   $evac2.attacked         = false;
  
   $evac2.utility.rested   = false;
   $evac2.cargo.rested     = false;
   
   $evac2.flyer.route   = myGetObjectId("MissionGroup\\Evac\\Evac2\\Route\\Flyer");
   $evac2.utility.route = myGetObjectId("MissionGroup\\Evac\\Evac2\\Route\\Utility");
   $evac2.cargo.route   = myGetObjectId("MissionGroup\\Evac\\Evac2\\Route\\Cargo");
   
   order($evac2.flyer,     Shutdown,   true);
   order($evac2.flyer,     Height,     100,  600);
   order($evac2.utility,   Guard,   $evac2.utility.route);
   order($evac2.cargo,     Guard,   $evac2.cargo.route);
//-----------------------------------------------------------------------------
   $evac3         = myGetObjectId("MissionGroup\\Evac\\Evac3");
   $evac3.flyer   = myGetObjectId("MissionGroup\\vehicles\\Flyers\\drop3");
   $evac3.utility = myGetObjectId("MissionGroup\\Evac\\Evac3\\Drone\\Utility");
   $evac3.cargo   = myGetObjectId("MissionGroup\\Evac\\Evac3\\Drone\\Cargo");
   
   $evac3.attacked         = false;

   $evac3.utility.rested   = false;
   $evac3.cargo.rested     = false;
   
   $evac3.flyer.route   = myGetObjectId("MissionGroup\\Evac\\Evac3\\Route\\Flyer");
   $evac3.utility.route = myGetObjectId("MissionGroup\\Evac\\Evac3\\Route\\Utility");
   $evac3.cargo.route   = myGetObjectId("MissionGroup\\Evac\\Evac3\\Route\\Cargo");
   
   order($evac3.flyer,     Shutdown,   true);
   order($evac3.flyer,     Height,     100,  600);
   order($evac3.utility,   Guard,   $evac3.utility.route);
   order($evac3.cargo,     Guard,   $evac3.cargo.route);
//-----------------------------------------------------------------------------
   $evac4         = myGetObjectId("MissionGroup\\Evac\\Evac4");
   $evac4.flyer   = myGetObjectId("MissionGroup\\vehicles\\Flyers\\drop4");
   $evac4.utility = myGetObjectId("MissionGroup\\Evac\\Evac4\\Drone\\Utility");
   $evac4.cargo   = myGetObjectId("MissionGroup\\Evac\\Evac4\\Drone\\Cargo");
   
   $evac4.attacked         = false;

   $evac4.utility.rested   = false;
   $evac4.cargo.rested     = false;
   
   $evac4.flyer.route   = myGetObjectId("MissionGroup\\Evac\\Evac4\\Route\\Flyer");
   $evac4.utility.route = myGetObjectId("MissionGroup\\Evac\\Evac4\\Route\\Utility");
   $evac4.cargo.route   = myGetObjectId("MissionGroup\\Evac\\Evac4\\Route\\Cargo");
   
   order($evac4.flyer,     Shutdown,   true);
   order($evac4.flyer,     Height,     100,  600);
   order($evac4.utility,   Guard,   $evac4.utility.route);
   order($evac4.cargo,     Guard,   $evac4.cargo.route);
//-----------------------------------------------------------------------------

   checkDistance($playerId, $evac1.flyer, 500, onEnterEvac1, 5.0); 
   checkDistance($playerId, $evac2.flyer, 500, onEnterEvac2, 5.0); 
   checkDistance($playerId, $evac3.flyer, 500, onEnterEvac3, 5.0); 
   checkDistance($playerId, $evac4.flyer, 500, onEnterEvac4, 5.0); 

   checkDistance($playerId, $navCharlie , 2200, launchEvac, 5.0);
   
   $evacDroneEscape  = myGetObjectId("MissionGroup\\Patrols\\EvacDroneEscape"); 
            
}

function onEnterEvac1()
{
   dbecho($LEVEL, "onEnterEvac1()");
   order($talon.lead, Attack, pick(playerSquad) );
   order($bravo.lead, Attack, pick(playerSquad) );

   if( !($bravo.spoke) )
   {  
      $bravo.spoke = true;
      say(0, $bravo, *IDSTR_HB3_BPT01, "HB3_BPT01.wav");
   }

   schedule("evacAway(" @ $evac1 @ ");", 5.0);
                                               
}
function onEnterEvac2()
{
   dbecho($LEVEL, "onEnterEvac2()");
   schedule("evacAway(" @ $evac2 @ ");", 5.0);
}
function onEnterEvac3()
{
   dbecho($LEVEL, "onEnterEvac3()");
   schedule("evacAway(" @ $evac3 @ ");", 5.0);
}
function onEnterEvac4()
{
   dbecho($LEVEL, "onEnterEvac4()");
   schedule("evacAway(" @ $evac4 @ ");", 5.0);
}

function evacAway(%group)
{
   dbecho($LEVEL, "evacAway(" @ %group @ ")");

   order(%group.flyer,     Guard, %group.flyer.route );
   order(%group.utility,   Guard, $evacDroneEscape);
   order(%group.cargo,     Guard, $evacDroneEscape);
}

function launchEvac()
{
   dbecho($LEVEL, "launchEvac()");
   echo("Distance to navCharlie = " @ getDistance($playerId, $navCharlie));

   schedule("onEnterEvac1();", 60.0);
   schedule("onEnterEvac2();", 60.0);
   schedule("onEnterEvac3();", 120.0);
   schedule("onEnterEvac4();", 180.0);
}

function eFlyer::vehicle::onAttacked(%this, %who)
{
   %say = false;

   if(%this == $evac1.flyer && !($evac1.attacked) )
   {
      dbecho($LEVEL, "eFlyer::vehicle::onAttacked(" @ %this @ ", " @ %who @ ")");
      
      $evac1.attacked = true;
      onEnterEvac1();

      %say = true;
   }
   else if(%this == $evac2.flyer && !($evac2.attacked) )
   {
      dbecho($LEVEL, "eFlyer::vehicle::onAttacked(" @ %this @ ", " @ %who @ ")");
      
      $evac2.attacked = true;
      onEnterEvac2();
      
      %say = true;
   }
   else if(%this == $evac3.flyer && !($evac3.attacked) )
   {
      dbecho($LEVEL, "eFlyer::vehicle::onAttacked(" @ %this @ ", " @ %who @ ")");
      
      $evac3.attacked = true;
      onEnterEvac3();

      %say = true;
   }
   else if(%this == $evac4.flyer && !($evac4.attacked) )
   {
      dbecho($LEVEL, "eFlyer::vehicle::onAttacked(" @ %this @ ", " @ %who @ ")");
      
      $evac4.attacked = true;
      onEnterEvac4();

      %say = true;
   }
   
   if( %say )
   {
      %num = randomInt(0 , 2);
      if( %num == 0 )
         schedule("say(0," @ %this @ ",\"" @ *IDSTR_GEN_DS11 @ "\", \"1DSA01.wav\");", 5.0);
      else if ( %num == 1 )
         schedule("say(0," @ %this @ ",\"" @ *IDSTR_GEN_DS13 @ "\", \"1DSA02.wav\");", 5.0);
      else if ( %num == 2 )                                                               
         schedule("say(0," @ %this @ ",\"" @ *IDSTR_GEN_DS21 @ "\", \"2DSA02.wav\");", 5.0);
   }
}

function eFlyer::vehicle::onDestroyed(%this, %who)
{
   killChannel(%this);
   
    $qwerty--;
    if($qwerty == 0)
      awardBonus();    

}
      
   
///////////////////////////////////////////////////////////////////////////////
// Flyer Functions                                                           //
///////////////////////////////////////////////////////////////////////////////
function initFlyers()
{
   dbecho($LEVEL, "initFlyers()");
   
   $flyer      = myGetObjectId("MissionGroup\\vehicles\\Flyers");
   $flyer.f1   = myGetObjectId("MissionGroup\\vehicles\\Flyers\\Flyer1");
   $flyer.f2   = myGetObjectId("MissionGroup\\vehicles\\Flyers\\Flyer2");
   $flyer.f3   = myGetObjectId("MissionGroup\\vehicles\\Flyers\\Flyer3");
   $flyer.f4   = myGetObjectId("MissionGroup\\vehicles\\Flyers\\Flyer4");
   $flyer.f5   = myGetObjectId("MissionGroup\\vehicles\\Flyers\\Flyer5");
   $flyer.f6   = myGetObjectId("MissionGroup\\vehicles\\Flyers\\Flyer6");
   $flyer.f7   = myGetObjectId("MissionGroup\\vehicles\\Flyers\\Flyer7");
   
   $flyer.route1 = myGetObjectId("MissionGroup\\Patrols\\Flyer\\1");
   $flyer.route2 = myGetObjectId("MissionGroup\\Patrols\\Flyer\\2");
   $flyer.route3 = myGetObjectId("MissionGroup\\Patrols\\Flyer\\3");
   $flyer.route4 = myGetObjectId("MissionGroup\\Patrols\\Flyer\\4");
   $flyer.route5 = myGetObjectId("MissionGroup\\Patrols\\Flyer\\5");
   $flyer.route6 = myGetObjectId("MissionGroup\\Patrols\\Flyer\\6");
   $flyer.route7 = myGetObjectId("MissionGroup\\Patrols\\Flyer\\7");
   
   order($flyer.f1, Flythrough, true);
   order($flyer.f2, Flythrough, true);
   order($flyer.f3, Flythrough, true);
   order($flyer.f4, Flythrough, true);
   order($flyer.f5, Flythrough, true);
   order($flyer.f6, Flythrough, true);
   order($flyer.f7, Flythrough, true);
   
   order($flyer.f1, Height, 151, 170);
   order($flyer.f2, Height, 171, 190);
   order($flyer.f3, Height, 191, 210);
   order($flyer.f4, Height, 211, 230);
   order($flyer.f5, Height, 231, 250);
   order($flyer.f6, Height, 251, 270);
   order($flyer.f7, Height, 271, 290);
   
   order($flyer.f1, Speed, High);
   order($flyer.f2, Speed, High);
   order($flyer.f3, Speed, High);
   order($flyer.f4, Speed, High);
   order($flyer.f5, Speed, High);
   order($flyer.f6, Speed, High);
   order($flyer.f7, Speed, High);

   schedule("order(" @ $flyer @ ".f1, Guard," @ $flyer.route1 @ ");",   0.0);
   schedule("order(" @ $flyer @ ".f2, Guard," @ $flyer.route2 @ ");",  20.0);
   schedule("order(" @ $flyer @ ".f3, Guard," @ $flyer.route3 @ ");",  40.0);
   schedule("order(" @ $flyer @ ".f4, Guard," @ $flyer.route4 @ ");",  60.0);
   schedule("order(" @ $flyer @ ".f5, Guard," @ $flyer.route5 @ ");",  80.0);
   schedule("order(" @ $flyer @ ".f6, Guard," @ $flyer.route6 @ ");", 100.0);
   schedule("order(" @ $flyer @ ".f7, Guard," @ $flyer.route7 @ ");", 120.0);
}

function flyer::vehicle::onArrived(%this, %where)
{
   healObject(%this, 10000);
}
function flyer::vehicle::onDestroyed(){}

///////////////////////////////////////////////////////////////////////////////
// Mission Completion Functions                                              //
///////////////////////////////////////////////////////////////////////////////

//-----------------------------------------------------------------------------
function objectiveCompleted(%objective)
{
    dbecho($LEVEL, "objectiveCompleted(" @ %objective @ ")");
    
    if($missionFailed == false)
    {
        if(%objective == 1)
        {
            missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        }
        if(%objective == 2)
        {
            missionObjective2.status = *IDSTR_OBJ_COMPLETED;
        }
        if(%objective == 3)
        {
            missionObjective3.status = *IDSTR_OBJ_COMPLETED;
        }
        
        say(0, $playerId, *IDSTR_GEN_OC01, "GEN_OC01.wav");
        
    }
    
    if( missionObjective1.status == *IDSTR_OBJ_COMPLETED ) 
    {                                                       
        missionSuccess();                                   
    }                                                       
}


//-----------------------------------------------------------------------------
function missionSuccess()
{
    dbecho($LEVEL, "missionSuccess()");
    
    updatePlanetInventory(hb3); 
    
    forceToDebrief();
}
//-----------------------------------------------------------------------------
function missionFailed(%reason)
{
    dbecho($LEVEL, "missionFailed(" @ %reason @ ")");
    
    if(%reason == "transportEscaped")
    {
        missionObjective1.status = *IDSTR_OBJ_FAILED;
        %msg = "Transport Escaped";
    }
    else
    if(%reason == "playerDied")
    {
        %msg = "Player Died";
    }
    else
    if(%reason == "playerLeftMissionArea")
    {
        %msg = "Player Left Mission Area";
    }
            
    forceToDebrief();
}


///////////////////////////////////////////////////////////////////////////////
// Generic Functions                                                         //
///////////////////////////////////////////////////////////////////////////////

function checkDistance( %obj1, %obj2, %distance, %callback, %time )
{
  
   dbecho( 5, "DEBUG: checkDistance ( ", %obj1, " ", %obj2, " ) ", %distance, "m CALLBACK: ", %callback );

   %var = getDistance( %obj1, %obj2 );
   dbecho( 5, "DEBUG: Distance difference: ", %var, "m" );

   if( getDistance( %obj1, %obj2 ) <= %distance )
   {
       %func = %callback @ "();";
       schedule( %func, 0 );
   }
   else
   {    schedule( "checkDistance( "
                @ %obj1 @ ", "
                @ %obj2 @ ", "
                @ %distance @ ", "
                @ %callback @ ", "
                @ %time @ ");", %time  );
   }        
}

function checkBound( %obj1, %obj2, %distance, %callback, %time )
{
  
   dbecho( 5, "DEBUG: checkBound ( ", %obj1, " ", %obj2, " ) ", %distance, "m CALLBACK: ", %callback );

   %var = getDistance( %obj1, %obj2 );
   dbecho( 5, "DEBUG: Distance difference: ", %var, "m" );

   if( getDistance( %obj1, %obj2 ) > %distance )
   {
     %func = %callback @ "();";
      schedule( %func, 0 );
   }
   else
   {    schedule( "checkBound( "
                @ %obj1 @ ", "
                @ %obj2 @ ", "
                @ %distance @ ", "
                @ %callback @ ", "
                @ %time @ ");", %time  );
   }        
}


function myGetObjectId(%name)
{
    %id = getObjectId(%name);
    if( %id == "")
    {
        dbecho($LEVEL, "Object " @ %name @ " not found.");
        return false;
    }
    else
    {
        return %id;
    }
}

function vehicle::onNewLeader(%this)
{  
   if(!$missionStart)
      return;
  
   dbecho($LEVEL, "onNewLeader(" @ %this @ ")"); 
   
   %group = getGroup(%this);
   %group.lead = %this;
   order(%group,        Formation,  Wedge);
   order(%group.lead,   Attack,     pick(playerSquad) );
}

function talon::vehicle::onNewLeader(%this)
{  
   if(!$missionStart)
      return;
   
   dbecho($LEVEL, "talon::onNewLeader(" @ %this @ ")"); 
   
   %group = getGroup(%this);
   %group.lead = %this;
   order(%group,        Formation,  Wedge);

   if(!$talon.atDropShip)
      order(%group.lead, Guard, $talon.retreatRoute);
   else
      order(%group.lead,   Attack,     pick(playerSquad) );
}

function vehicle::onMessage( %this, %message )
{
   if( %message == "ArtilleryOutOfAmmo" )
   {
      %this.outOfAmmo = true;
      order(%this, Guard, $evacDroneEscape);
   }
}

function awardBonus()
{
   dbecho($LEVEL, "awardBonus()");
   objectiveCompleted(3);
   
   InventoryWeaponAdjust(  -1,   103,  2	);	//Comp Laser 
   InventoryWeaponAdjust(  -1,   129,  2	);	//Minion     
   InventoryWeaponAdjust(  -1,   128,  2	);	//SWARM 6    
}

   




