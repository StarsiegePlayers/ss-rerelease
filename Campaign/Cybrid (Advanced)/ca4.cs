$server::HudMapViewOffsetX = 0; 
$server::HudMapViewOffsetY = 0; 
$LEVEL = 1;

function win()
{  
	missionObjective1.status = *IDSTR_OBJ_COMPLETED;
	missionObjective2.status = *IDSTR_OBJ_COMPLETED;
	updatePlanetInventory(ca4);
   schedule("forceToDebrief();", 3.0);
}

///////////////////////////////////////////////////////////////////////////////
// CA4                                                                       //
//                                                                           //
// Primary Objectives                                                        //
// 1. Destroy the Imperial Trooper Evac site at Nav 001                      //
// 2. Destroy all resistance encountered                                     //
//                                                                           //
// Secondary Objectives                                                      //
// 3. Destroy the Evac Transports                                            //
///////////////////////////////////////////////////////////////////////////////

MissionBriefInfo missionBriefInfo
{
   campaign             = *IDSTR_CA4_CAMPAIGN;             
   title                = *IDSTR_CA4_TITLE;      
   planet               = *IDSTR_PLANET_MERCURY;     
   location             = *IDSTR_CA4_LOCATION; 
   dateOnMissionEnd     = *IDSTR_CA4_DATE;               
   shortDesc            = *IDSTR_CA4_SHORTBRIEF;
   longDescRichText     = *IDSTR_CA4_LONGBRIEF;
   media                = *IDSTR_CA4_MEDIA;
   successDescRichText  = *IDSTR_CA4_DEBRIEF_SUCC;
   failDescRichText     = *IDSTR_CA4_DEBRIEF_FAIL;
   nextMission          = *IDSTR_CA4_NEXTMISSION;
   successWavFile       = "CA4_Debriefing.wav";
   soundVol             = "CA4.vol";
   endCinematicRec      = "cinCB.rec";
   endCinematicSmk      = "cinCB.smk";
    
};

MissionBriefObjective missionObjective1
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_CA4_OBJ1_SHORT;
   longTxt              = *IDSTR_CA4_OBJ1_LONG;
   bmpName              = *IDSTR_CA4_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_CA4_OBJ2_SHORT;
   longTxt              = *IDSTR_CA4_OBJ2_LONG;
   bmpName              = *IDSTR_CA4_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3
{
   isPrimary            = false;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_CA4_OBJ3_SHORT;
   longTxt              = *IDSTR_CA4_OBJ3_LONG;
   bmpName              = *IDSTR_CA4_OBJ3_BMPNAME;
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

   earthquakeSounds();
   mercurySounds();
   cdAudioCycle(12, 13);

   $missionSuccess  = false;
   $missionFailed   = false;
   $missionStart    = false;
   $BOOL            = false;
   $PI              = 3.14;
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
   initArtillery();
   initDrone();
   initNavPoints();
   initFlyer();
   initBase();
   initHuman();
   initMission();
}

///////////////////////////////////////////////////////////////////////////////
// INIT Functions                                                            //
///////////////////////////////////////////////////////////////////////////////
//-----------------------------------------------------------------------------
function initFormations()
{
    dbecho ($LEVEL, "initFormations()");
    
    newFormation(Follow,    0,0,0,  
                            0,-40,0,
                            0,-80,0);
                            
    newFormation(Wedge,     0,  0,  0,  
                          -40,-40,  0,
                           40,-40,  0);
}
//-----------------------------------------------------------------------------

function initMission()
{
   dbecho($LEVEL, "initMission()");

   dropArtillery();
   startDrone();
   schedule("launchDriver();", 60.0);
   $missionStart  = true;
}
///////////////////////////////////////////////////////////////////////////////
// Player Functions                                                          //
///////////////////////////////////////////////////////////////////////////////
function initPlayer()
{
   dbecho($LEVEL, "initPlayer()");
   $playerId = playerManager::playerNumToVehicleId($playerNum);

   $playerSquad   = myGetObjectId(playerSquad);
   
   %num    = 1;
   $playerSquad.num[%num] = GetNextObject($playerSquad, $playerId);
   while($playerSquad.num[%num] != 0)
   {
      if($playerSquad.num[%num] != $playerId)
         setHercOwner( $playerSquad.num[%num], "MissionGroup\\vehicles\\HUMAN\\flyer" );
      
      %num++;
      $playerSquad.num[%num] = GetNextObject($playerSquad, $playerSquad.num[%num - 1]);
   }

}
//-----------------------------------------------------------------------------

function isPlayerSafe()
{
   dbecho($LEVEL, "isPlayerSafe()");
   if($power.num <= 0)
   {   
      if( IsSafe(*IDSTR_TEAM_YELLOW, $playerId, 1000, true) )
         objectiveCompleted(2);
      else
         schedule("isPlayerSafe();", 5.0);
   }
   else
   {
      if( IsSafe(*IDSTR_TEAM_YELLOW, $playerId, 1000 ) )
         objectiveCompleted(2);
      else
         schedule("isPlayerSafe();", 5.0);
    }
}

///////////////////////////////////////////////////////////////////////////////
// Base Functions                                                            //
///////////////////////////////////////////////////////////////////////////////
function initBase()
{
   dbecho($LEVEL, "initBase()");
   
   $base                = myGetObjectId("MissionGroup\\Base");
   $base.targets        = 3;
   $base.attacked       = false;
   $base.turretAttacked = false;
   
   $power               = myGetObjectId("MissionGroup\\Base\\Power");
   $hq                  = myGetObjectId("MissionGroup\\Base\\HQ");
   $comm                = myGetObjectId("MissionGroup\\Base\\comm");
   $turrets             = myGetObjectId("MissionGroup\\Turrets");
   $supply              = myGetObjectId("MissionGroup\\Base\\Supply");
   $hqMark              = myGetObjectId("MissionGroup\\marker1");
   
   $turret1             =myGetObjectId("MissionGroup\\Turrets\\1");
   $turret2             =myGetObjectId("MissionGroup\\Turrets\\2");
   $turret3             =myGetObjectId("MissionGroup\\Turrets\\3");
   $turret4             =myGetObjectId("MissionGroup\\Turrets\\4");
   
   $power.num           = 2;
   
   checkDistance($hqMark, $playerId, 275, "onEnterHeadQuarters", 1.0);
}

function onEnterHeadQuarters()
{
   if( getPosition($playerId, z) >= 165 )
   {
      placeObject($squad6, $patrol6);
      order($squad6.lead, Guard, $playerId);
   }
   else
      schedule("checkDistance(" @ $hqMark @ ", " @ $playerId @ ", 275, \"onEnterHeadQuarters\", 1.0);", 1.0);
}      
      

//-----------------------------------------------------------------------------
function base::structure::onAttacked(%this, %who)
{
   if(%this != %who)
   {
      if ( !($base.attacked) )
      {
         dbecho($LEVEL, "base::structure::onAttacked(" @ %this @ ", " @ %who @ ")");
         
         $base.attacked = true;
         
         order($squad1, Attack, playerSquad);
      }
   }
}

//-----------------------------------------------------------------------------
function base::structure::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "base::structure::onDestroyed(" @ %this @ ", " @ %who @ ")");
   
   if( getGroup(%this) == $power)
   {
      $power.num--;
      if($power.num == 0)
      {
         order($turrets, Shutdown, true);
         setStaticShapeShortName($turret1, *IDACS_C_NOPOWERTURRET);
         setStaticShapeShortName($turret2, *IDACS_C_NOPOWERTURRET);
         setStaticShapeShortName($turret3, *IDACS_C_NOPOWERTURRET);
         setStaticShapeShortName($turret4, *IDACS_C_NOPOWERTURRET);
         $base.targets--;
      } 
   }
   else if( %this == $hq )
      $base.targets--;
   else if( %this == $comm )
      $base.targets--;
   else if( %this == $supply )
   {
      if ( $apo.shutdown )
         damageObject($apo, 100000);
   } 
   if( $base.targets <= 0 && missionObjective1.status != *IDSTR_OBJ_COMPLETED )
   {   
      objectiveCompleted(1);
   }
}

//-----------------------------------------------------------------------------
function turret::onAttacked(%this, %who)
{
   if(%this != %who)
   {
      if( getGroup(%who) == myGetObjectId(playerSquad) && !($base.turretAttacked) )
      {
         dbecho($LEVEL, "turret::onAttacked(" @ %this @ ", " @ %who @ ")");
         
         $base.turretAttacked = true;
         order($squad1, Attack, playerSquad);
         order( getGroup(%this), Attack, playerSquad);
      }
   }
}

//-----------------------------------------------------------------------------
function turret::onDestroyed(%this, %who){}
      
///////////////////////////////////////////////////////////////////////////////
// Artillery Functions                                                       //
///////////////////////////////////////////////////////////////////////////////

function initArtillery()
{
    dbecho($LEVEL, "initArtillery()");

    $artillery       = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Artillery");
    $artillery.ar1   = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Artillery\\ar1");
    $artillery.ar2   = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Artillery\\ar2");
        
    $artillery.inPosition  = false;
    $artillery.curTarget   = "";
    $artillery.count       = 2;
    
    order($artillery, HoldFire, true);
    order($artillery, HoldPosition, true);    
}
//-----------------------------------------------------------------------------

function dropArtillery()
{
   dbecho($LEVEL, "dropArtillery()");
   
   $dropStart1 = myGetObjectId("MissionGroup\\Patrols\\drop\\start1"); 
   $dropEnd1   = myGetObjectId("MissionGroup\\Patrols\\drop\\end1"); 
   $dropStart2 = myGetObjectId("MissionGroup\\Patrols\\drop\\start2"); 
   $dropEnd2   = myGetObjectId("MissionGroup\\Patrols\\drop\\end2"); 
   
   schedule(   "dropPod(   getPosition(" @ $dropStart1 @ ", x),   getPosition(" @ $dropStart1 @ ", y),   getPosition(" @ $dropStart1 @ ", z),"
               @ "         getPosition(" @ $dropEnd1   @ ", x),   getPosition(" @ $dropEnd1   @ ", y),   getPosition(" @ $dropEnd1  @  ", z),"
               @           $artillery.ar1 @");", 2.0);
   

   schedule(   "dropPod(   getPosition(" @ $dropStart2 @ ", x),   getPosition(" @ $dropStart2 @ ", y),   getPosition(" @ $dropStart2 @ ", z),"
               @ "         getPosition(" @ $dropEnd2   @ ", x),   getPosition(" @ $dropEnd2   @ ", y),   getPosition(" @ $dropEnd2   @ ", z),"
               @           $artillery.ar2 @");", 1.0);

   

   checkDistance($dropEnd2, $artillery.ar2, 10, "artilleryArrived", 1.0);
}
//-----------------------------------------------------------------------------
function artilleryArrived()
{
   $artillery.inPosition = true;
   schedule("cinematic();", 2.0);
}
//-----------------------------------------------------------------------------

function vehicle::onSpot(%this, %target)
{
    dbecho($LEVEL, "vehicle::onSpot() = " @ %target);

    if(%target == "")
        clearArtilleryTarget();
    else
    {
        setArtilleryTarget(%target);
        say(0, %this, *IDSTR_CYB_NEX15, "CYB_NEX15.wav");
    }  
}

//-----------------------------------------------------------------------------
function clearArtilleryTarget()
{
    dbecho($LEVEL, "clearArtilleryTarget()");
    $artillery.curTarget = "";
    
    order($artillery.ar1, Clear);
    order($artillery.ar2, Clear);
    
    order($artillery, HoldFire, true);
}

//-----------------------------------------------------------------------------
function setArtilleryTarget(%target)
{  
   dbecho($LEVEL, "setArtilleryTarget()");
                  
   if($artillery.inPosition)
   {
      $artillery.curTarget = %target;
      echo("curTarget = " @ $artillery.curTarget);

      schedule("order($artillery.ar1, Attack, $artillery.curTarget);", randomInt(1,4) );
      schedule("order($artillery.ar2, Attack, $artillery.curTarget);", randomInt(1,4) );    
   }
}

//-----------------------------------------------------------------------------
function vehicle::onMessage(%this, %msg)
{
   dbecho($LEVEL, getObjectName(%this) @ "onMessage() = " @ %msg);

   if(%msg == "ArtilleryOutOfAmmo")
   {
      $artillery.count--;
      order(%this, Shutdown, true);
      
      if($artillery.count <= 0)
         say(0, %this, *IDSTR_CYB_NEX14, "CYB_NEX14.wav");
       
   }
}

///////////////////////////////////////////////////////////////////////////////
// Drone Functions                                                           //
///////////////////////////////////////////////////////////////////////////////
function initDrone()
{
   dbecho($LEVEL, "initDrone()");
   
   $utility1   = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Drone\\UtilityDrone1");
   $utility2   = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Drone\\UtilityDrone2");
   $utility3   = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Drone\\UtilityDrone3");
   $fuel1      = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Drone\\FuelDrone1");
   $fuel2      = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Drone\\FuelDrone2");
   $fuel3      = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Drone\\FuelDrone3");
                                                                      
   $utility1.route   = myGetObjectId("MissionGroup\\Patrols\\Drone\\UtilityDrone1");
   $utility2.route   = myGetObjectId("MissionGroup\\Patrols\\Drone\\UtilityDrone2");
   $utility3.route   = myGetObjectId("MissionGroup\\Patrols\\Drone\\UtilityDrone3");
   $fuel1.route      = myGetObjectId("MissionGroup\\Patrols\\Drone\\FuelDrone1");
   $fuel2.route      = myGetObjectId("MissionGroup\\Patrols\\Drone\\FuelDrone2");
   $fuel3.route      = myGetObjectId("MissionGroup\\Patrols\\Drone\\FuelDrone3");
   
   $escape           = myGetObjectId("MissionGroup\\escape");
   
   // drone init is completed in initFlyer
}

function startDrone()
{
   dbecho($LEVEL, "startDrone()");
   
   schedule("order(" @ $utility1 @ ",Guard," @  $utility1.route @ ");", 3.0);
   schedule("order(" @ $utility2 @ ",Guard," @  $utility2.route @ ");", 6.0);
   schedule("order(" @ $utility3 @ ",Guard," @  $utility3.route @ ");", 9.0);
   schedule("order(" @ $fuel1 @ ",Guard," @  $fuel1.flyer @ ");", 9.0);
   schedule("order(" @ $fuel2 @ ",Guard," @  $fuel2.flyer @ ");", 6.0);
   schedule("order(" @ $fuel3 @ ",Guard," @  $fuel3.flyer @ ");", 3.0);
}

function drone::vehicle::onArrived(%this, %where)
{
   schedule( "dealWithIt(" @ %this @ ", " @ %where @ ");", 15.0 );
}

function dealWithIt(%this, %where)
{
   //dbecho($LEVEL, "dealWithIt(" @ %this @ ", " @ %where @ ")");

   %flyer = %this.flyer;
   if( %flyer.done != false )
      return;

   if( %where == %this.flyer )
      order(%this, Guard, %this.route);
   else if ( %where == %this.route ) 
      order(%this, Guard, %this.flyer);
}    
///////////////////////////////////////////////////////////////////////////////
// Flyer Functions                                                           //
///////////////////////////////////////////////////////////////////////////////
function initFlyer()
{
   dbecho($LEVEL, "initFlyer()");
   
   $flyer      = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Flyer");
   $flyer.num  = 3;
   
   $dropShip1  = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Flyer\\DropShip1"); 
   $dropShip2  = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Flyer\\DropShip2"); 
   $dropShip1.route = myGetObjectId("MissionGroup\\Patrols\\DropShip\\DropShip1");
   $dropShip2.route = myGetObjectId("MissionGroup\\Patrols\\DropShip\\DropShip2");
   
   $flyer1     = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Flyer\\1");
   $flyer2     = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Flyer\\2");
   $flyer3     = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Flyer\\3");
   $flyer4     = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Flyer\\4");
   
   $stop1      = myGetObjectId("MissionGroup\\Patrols\\Flyer\\1");
   $stop2      = myGetObjectId("MissionGroup\\Patrols\\Flyer\\2");
   $stop3      = myGetObjectId("MissionGroup\\Patrols\\Flyer\\3");
   $stop4      = myGetObjectId("MissionGroup\\Patrols\\Flyer\\4");

   $flyer1.util = $utility1;  $flyer1.fuel = $fuel1;
   $flyer2.util = $utility2;  $flyer2.fuel = $fuel2;
   $flyer3.util = $utility3;  $flyer3.fuel = $fuel3;
   $flyer4.util = "";         $flyer4.fuel = "";
   
   $flyer1.stop = $stop1;  $flyer1.done = false;  
   $flyer2.stop = $stop2;  $flyer2.done = false;
   $flyer3.stop = $stop3;  $flyer3.done = false;
   $flyer4.stop = $stop4;  $flyer4.done = false;
   
   order($flyer, Height, 150, 300);
   
   %num     = randomInt(1, 4);
   for(%count = 1; %count <= 4; %count++)
   {
      if( %num > 4 )
         %num = 1;
      
      $flyer.seq[%count] = myGetObjectId("MissionGroup\\vehicles\\Human\\Flyer\\" @ %num );
      %num++;
   }
   
   schedule("order(" @ $dropShip1 @ ", Guard," @ $dropShip1.route  @ ");",  85.0);
   schedule("order(" @ $dropShip2 @ ", Guard," @ $dropShip2.route  @ ");", 190.0);
   
   // complete drone init
   $utility1.flyer   = $flyer1; 
   $utility2.flyer   = $flyer2;
   $utility3.flyer   = $flyer3;
   $fuel1.flyer      = $flyer1;  
   $fuel2.flyer      = $flyer2; 
   $fuel3.flyer      = $flyer3;    
   
}

function launchDriver()
{
   dbecho($LEVEL, "launchDriver()");
   
   %bool    = randomInt(0,1);
   %delay   = 1;
   
   if( %bool == 1 )
      %count = 1;
   else
      %count = 4;
      
   while( %count > 0 && %count < 5)
   {
      %flyer = $flyer.seq[%count];
      schedule( "launch(" @ %flyer @ ");", 60 * %delay );
      %delay++;
      
      if( %bool == 1 )
         %count++;
      else
         %count--;
   }
}

function launch(%this)
{
   dbecho($LEVEL, "launch(" @ %this @ ")");
   
   %this.done = true;
   order(%this, Guard, %this.stop);
   order(%this.util, Guard, $escape);
   order(%this.fuel, Guard, $escape);
}   
      
function flyer::vehicle::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "flyer::vehicle::onDestroyed(" @ %this @ ", " @ %who @ ")");
   
   $flyer.num--;
   %this.done = true;
   order(%this.util, Guard, $escape);
   order(%this.fuel, Guard, $escape);
   
   if( $flyer.num == 0 )
      objectiveCompleted(3);
} 

///////////////////////////////////////////////////////////////////////////////
// Human Functions                                                           //
///////////////////////////////////////////////////////////////////////////////

function initHuman()
{
   dbecho($LEVEL, "initHuman()");
   
   $squadGroup = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad");
   
   $squad1 = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\1");
   $squad2 = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\2");
   $squad3 = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\3");
   $squad4 = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\4");
   $squad5 = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\5");
   $squad6 = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\6");

   $squad1.num = 2;
   $squad2.num = 1;
   $squad3.num = 2;
   $squad4.num = 1;
   $squad5.num = 1;     // not really
   $squad6.num = 1;
   
   $squad1.h1        = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\1\\Bas");
   $squad1.h2        = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\1\\Min");
   $squad3.lead      = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\3\\Apo");
   $squad4.lead      = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\4\\MIN");
   $squad5.current   = GetNextObject($squad5, 0);
   $squad6.lead      = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\6\\Gor");
   $apo              = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\2\\Apo");
   $apo.shutdown     = true;
   
   $squad1.attacked = false;
   $squad2.attacked = false;
   $squad3.attacked = false;
   $squad4.attacked = false;
   $squad5.attacked = false;
   $squad6.attacked = false;
   
   $patrol1 = myGetObjectId("MissionGroup\\Patrols\\squad1\\1");
   $patrol2 = myGetObjectId("MissionGroup\\Patrols\\squad1\\2");
   $patrol6 = myGetObjectId("MissionGroup\\Patrols\\squad6\\marker1");

   order($squad3.lead, MakeLeader, true);
   order($squad4.lead, MakeLeader, true);
   
   order($squad3, Formation, Wedge);
   order($squad4, Formation, Wedge);
   
   order($squad1, HoldPosition, $BOOL);
   order($squad2, HoldPosition, $BOOL);

   order($squad1, HoldFire, $BOOL);
   order($squad2, HoldFire, $BOOL);
   
   order($squad2, Shutdown, true);
   
   order($squad1.h1, Guard, $patrol1);
   order($squad1.h2, Guard, $patrol2);

}
//-----------------------------------------------------------------------------

function sq::vehicle::onAttacked(%this, %who)
{
   %group = getGroup(%this);
   
   if(%group.attacked || %group != $squad1 || %this == %who)
      return;
   
   if( getGroup(%who) != myGetObjectId(playerSquad) )
      return;

   %group.attacked = true;
   
   order( $squad1, Attack, pick(playerSquad) );
}
            
function sq::vehicle::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "sq::vehicle::onDestroyed(" @ %this @ "," @ %who @ ")");
   %group = getGroup(%this);
   %group.num--;
   
   if(%group.num != 0 )
      return;
      
   if(%group == $squad1)
   {
      if( $squad2.num <= 0 )
      {
         schedule( "squad3Attacks();", 45.0 ); 
      }
      else
      {
         schedule( "squad2Attacks();", 3.0 ); 
      }
   }
   else if( %group == $squad2 )
   {
      if( $squad1.num <= 0 )
      {
         schedule( "squad3Attacks();", 45.0 ); 
      }
   }
   else if( %group == $squad3 )
   {
      schedule( "squad4Attacks();", 45.0 ); 
   }
   else if (%group == $squad4 && $squad5.current != 0 )
   {
      schedule( "squad5Attacks();", 60.0 ); 
   }
   else if( %group == $squad5 && $squad5.current != 0 )
   {
      schedule( "squad5Attacks();", 60.0 ); 
      %group.num++;
   }  
}


function squad2Attacks()
{
   if($missionSuccess)
      return;
   
   $apo.shutdown  = false;
   order( $squad2, Shutdown, false);
   order( $squad2, Attack, pick(playerSquad) );
}  

function squad3Attacks()
{
   if($missionSuccess)
      return;

   calcAndDrop($squad3);
   order( $squad3.lead, Attack, pick(playerSquad) );
   schedule("say(0, " @ $playerId @ ", *IDSTR_CYB_NEX06, \"CYB_NEX06.wav\");", 5.0);
}

function squad4Attacks()
{
   if($missionSuccess)
      return;

   calcAndDrop($squad4);
   order( $squad4.lead, Attack, pick(playerSquad) );
}

function squad5Attacks()
{
   if($missionSuccess)
      return;

   calcAndDrop($squad5.current);
   order($squad5.current, Attack, pick(playerSquad) );
   $squad5.current = GetNextObject($squad5, $squad5.current);
}

///////////////////////////////////////////////////////////////////////////////
// Nav Point Functions                                                       //
///////////////////////////////////////////////////////////////////////////////
function initNavPoints()
{
   dbecho($LEVEL, "initNavPoints()");
   
   $nav001     = myGetObjectId("MissionGroup\\NavPoints\\Nav001");
   $navBogus   = myGetObjectId("MissionGroup\\NavPoints\\Bogus");

   setNavMarker($nav001, true, -1);
   setNavMarker($navBogus, false);
   checkDistance($nav001, $playerId, 800, "onEnterNav001", 5.0);
   
   checkBound($playerId, $nav001, 3000, "missionBoundWarning", 5.0); 
   checkBound($playerId, $nav001, 3500, "missionBoundFail", 5.0); 
}

//-----------------------------------------------------------------------------
function onEnterNav001()
{
   dbecho($LEVEL, "onEnterNav001()");
   setNavMarker($navBogus, false, -1);
   
   order( $squad1,  Attack, playerSquad );
   order( $turrets, Attack, pick(playerSquad) );
}

//-----------------------------------------------------------------------------
function missionBoundWarning()
{
   dbecho($LEVEL, "missionBoundWarning()");
   say(0, $playerId, *IDSTR_CYB_NEX01, "CYB_NEX01.wav");
}
//-----------------------------------------------------------------------------

function missionBoundFail()
{
   dbecho($LEVEL, "missionBoundFail()");
   say(0, $playerId, *IDSTR_CYB_NEX02, "CYB_NEX02.wav");
   missionFailed(playerLeftMissionArea);
}


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
          say(0, $playerId, *IDSTR_CYB_NEX17, "CYB_NEX17.wav");
          return;
      }
      say(0, $playerId, *IDSTR_CYB_NEX17, "CYB_NEX17.wav");
   }
   
   if( missionObjective1.status == *IDSTR_OBJ_COMPLETED && 
       missionObjective2.status == *IDSTR_OBJ_ACTIVE     ) 
   {                                                       
      isPlayerSafe();                                   
   }                                                       
   if( missionObjective1.status == *IDSTR_OBJ_COMPLETED && 
       missionObjective2.status == *IDSTR_OBJ_COMPLETED     ) 
   {                                                       
      $missionSuccess = true;
      schedule("missionSuccess();",  10.0);                                   
   }                                                       
}


//-----------------------------------------------------------------------------
function missionSuccess()
{
   if($missionFailed)
     return;

   dbecho($LEVEL, "missionSuccess()");
   
   $missionSuccess = true;

   updatePlpoanetInventory(ca4); 
   
   say(0, $playerId, *IDSTR_CYB_NEX04, "CYB_NEX04.wav");
   
   forceToDebrief();
}
//-----------------------------------------------------------------------------
function missionFailed(%reason)
{
   if($missionSuccess)
     return;
   
   dbecho($LEVEL, "missionFailed(" @ %reason @ ")");
   
   $missionFailed = true;

   if(%reason == "playerLeftMissionArea")
   {
       %msg = "Player Left Mission Area";
   }
   else
   if(%reason == "playerDied")
   {
       %msg = "Player Died";
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

//-----------------------------------------------------------------------------
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


//-----------------------------------------------------------------------------
function myGetObjectId(%name)
{
    %id = getObjectId(%name);
    if( %id == "")
    {
        dbecho(1, "Object " @ %name @ " not found.");
        return false;
    }
    else
    {
        return %id;
    }
}
//-----------------------------------------------------------------------------

function calcAndDrop(%object)
{
   dbecho($LEVEL, "calcAndDrop()");  
 
   %offset  = randomInt(-1000, 1000);  
   
   %x    = getPosition($playerId, x);
   %y    = getPosition($playerId, y);
   %z    = getPosition($playerId, z);
   %rot  = getPosition($playerId, r);
   
   if(     %rot >=   -($PI/4)    &&      %rot <    ($PI/4)   )
   {
       %y = %y - 1000;
       %x = %x + %offset;
   }
   else
   if(     %rot >=    ($PI/4)    &&      %rot <  3*($PI/4)   )
   {
       %x = %x + 1000;
       %y = %y + %offset;
   }
   else
   if(     %rot >=  3*($PI/4)    ||      %rot < -3*($PI/4)   )
   {
       %y = %y + 1000;
       %x = %x + %offset;
   }
   else
   if(     %rot >= -3*($PI/4)    &&      %rot <   -($PI/4)   )
   {
       %x = %x - 1000;
       %y = %y + %offset;
   }
   
   setPosition(%object, %x, %y, getTerrainHeight(%x, %y) );
}
//-----------------------------------------------------------------------------
function placeObject(%object, %where)
{
   dbecho($LEVEL, "placeObject(" @ %object @ ", " @ %where @ ")");
   setPosition(%object, getPosition(%where, x),
                        getPosition(%where, y),
                        getPosition(%where, z) );
}
//-----------------------------------------------------------------------------
function vehicle::onNewLeader(%this)
{  
   if(!($missionStart))
      return;
   
   dbecho($LEVEL, "onNewLeader(" @ %this @ ")"); 
   order(%this, Attack, pick(playerSquad) );
}

//-----------------------------------------------------------------------------
function cinematic()
{
   schedule( "fadeEvent( 0, out, 0.5, 0, 0, 0 );", 0.5 );
	schedule( "setWidescreen(true);", 1.0 );
	schedule( "focusCamera( splineCamera, path1 );", 1.25 );
   schedule( "cameraLockFocus(true);", 1.30);
	schedule( "fadeEvent( 0, in, 1.0, 0, 0, 0 );", 1.5 );

}
//-----------------------------------------------------------------------------
function path1::camera::waypoint1()
{
   schedule("say(0, " @ $artillery @ ", *IDSTR_CYB_NEX05, \"CYB_NEX05.wav\");", 5.0);
   
   $sceneryValue     = $pref::sceneryDetail * 100;
   $lensFlareValue   = $pref::fxDetail * 100;
   $fxDetail         = $pref::fxDetail;
   
   if( $sceneryValue < 30 && $lensFlareValue >= 91 )
   {
      $pref::fxDetail   = 0.8;
      schedule( "$pref::fxDetail = $fxDetail;", 20.0);
   }
}
//-----------------------------------------------------------------------------
function path1::camera::waypoint4()
{	
   
   if($sceneryValue >= 30 && $lensFlareValue >= 91)
      schedule( "fadeEvent( 0, out, 0.5, 1, 1, 1 );", 0.0 );
   else
      schedule( "fadeEvent( 0, out, 0.5, 0, 0, 0 );", 0.0 );
   
   schedule( "cameraLockFocus(false);", 0.5);
	schedule( "setPlayerCamera();", 0.75 );
	schedule( "setWidescreen(False);", 0.85 );
  
   if($lensFlareValue >= 91)
      schedule( "$pref::fxDetail = $fxDetail;", 0.75);
   
   if($sceneryValue >= 30 && $lensFlareValue >= 91)
	   schedule( "fadeEvent( 0, in, 1.0, 1, 1, 1 );", 1.0 );
   else
	   schedule( "fadeEvent( 0, in, 1.0, 0, 0, 0 );", 1.0 );
}
