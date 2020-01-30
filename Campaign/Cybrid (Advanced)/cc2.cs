$server::HudMapViewOffsetX = 0; 
$server::HudMapViewOffsetY = 100; 
$LEVEL   = 1;

function win()
{  
	missionObjective1.status = *IDSTR_OBJ_COMPLETED;
	missionObjective2.status = *IDSTR_OBJ_COMPLETED;
	missionObjective3.status = *IDSTR_OBJ_COMPLETED;
	missionObjective4.status = *IDSTR_OBJ_COMPLETED;
	updatePlanetInventory(cc2);
   schedule("forceToDebrief();", 3.0);
}

///////////////////////////////////////////////////////////////////////////////
// CC2                                                                       //
//                                                                           //
// Primary Objectives                                                        //
// 1. Destroy the military targets at the outpost located at NAV 001         //
// 2. Destroy the military targets at the outpost located at NAV 001         //
// 3. Destroy all resistance encountered                                     //
// 4. Scan all buildings and locate any civilians                            //
///////////////////////////////////////////////////////////////////////////////

MissionBriefInfo missionBriefInfo
{
   campaign             = *IDSTR_CC2_CAMPAIGN;             
   title                = *IDSTR_CC2_TITLE;      
   planet               = *IDSTR_PLANET_ICE;     
   location             = *IDSTR_CC2_LOCATION; 
   dateOnMissionEnd     = *IDSTR_CC2_DATE;               
   shortDesc            = *IDSTR_CC2_SHORTBRIEF;
   longDescRichText     = *IDSTR_CC2_LONGBRIEF;
   media                = *IDSTR_CC2_MEDIA;
   successDescRichText  = *IDSTR_CC2_DEBRIEF_SUCC;
   failDescRichText     = *IDSTR_CC2_DEBRIEF_FAIL;
   nextMission          = *IDSTR_CC2_NEXTMISSION;
   successWavFile       = "CC2_Debriefing.wav";
   soundVol             = "CC2.vol";
    
};

MissionBriefObjective missionObjective1
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_CC2_OBJ1_SHORT;
   longTxt              = *IDSTR_CC2_OBJ1_LONG;
   bmpName              = *IDSTR_CC2_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_CC2_OBJ2_SHORT;
   longTxt              = *IDSTR_CC2_OBJ2_LONG;
   bmpName              = *IDSTR_CC2_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_CC2_OBJ3_SHORT;
   longTxt              = *IDSTR_CC2_OBJ3_LONG;
   bmpName              = *IDSTR_CC2_OBJ3_BMPNAME;
};

MissionBriefObjective missionObjective4
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_CC2_OBJ4_SHORT;
   longTxt              = *IDSTR_CC2_OBJ4_LONG;
   bmpName              = *IDSTR_CC2_OBJ4_BMPNAME;
};

//-----------------------------------------------------------------------------

function player::onAdd(%this)
{
    $playerNum = %this;
}
//-----------------------------------------------------------------------------
function onMissionStart(%playerNum)
{
   dbecho($LEVEL, "onMissionStart(" @ %playerNum @ ")");
   
   setHostile(*IDSTR_TEAM_RED, *IDSTR_TEAM_YELLOW);
   setHostile(*IDSTR_TEAM_YELLOW, *IDSTR_TEAM_RED);
   setHostile(*IDSTR_TEAM_PURPLE);

   iceSounds();
   avalancheSounds();
   cdAudioCycle(13, 12);

   $onLeaveTriggered    = false;
   $playerSafe          = false; 
   $missionSuccess      = false;
   $missionFailed       = false;
   $missionStart        = false;
   $BOOL                = false;
   $surprise            = true;
   $PI                  = 3.14;

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
   initNavPoints();
   initBase();
   initHuman();
      
   $missionStart    = true;
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
// Player Functions                                                          //
///////////////////////////////////////////////////////////////////////////////
function initPlayer()
{
   dbecho($LEVEL, "initPlayer()");
   $playerId     = playerManager::playerNumToVehicleId($playerNum);
   $playerSquad  = myGetObjectId(playerSquad);
   
   %num    = 1;
   $playerSquad.num[%num] = GetNextObject($playerSquad, $playerId);
   while($playerSquad.num[%num] != 0)
   {
      if($playerSquad.num[%num] != $playerId)
         setHercOwner( $playerSquad.num[%num], "MissionGroup\\vehicles\\HUMAN\\Flyers" );
      
      %num++;
      $playerSquad.num[%num] = GetNextObject($playerSquad, $playerSquad.num[%num - 1]);
   }
   
   $playerSquadEngagedHS3   = false;
}
//-----------------------------------------------------------------------------

function vehicle::onAttacked( %this, %who )
{
   if( getGroup(%this)           != $playerSquad   ||
       getGroup(%who)            != $squad3        ||
       $playerSquadEngagedHS3    == true           )
      return;
   
   dbecho($LEVEL, "vehicle::onAttacked(" @ %this @ ", " @ %who @ ")");
   $playerSquadEngagedHS3 = true;
   
   say(0, $playerId, *IDSTR_CC2_NEX01, "CC2_NEX01.wav");   
}

//-----------------------------------------------------------------------------
function isPlayerSafe()
{
   dbecho($LEVEL, "isPlayerSafe()");
   
   if( IsSafe(*IDSTR_TEAM_YELLOW, $playerId, 1000) )
   {
      $playerSafe = true;
      objectiveCompleted(3);
   }
   else
      schedule("isPlayerSafe();", 1.0);
}

///////////////////////////////////////////////////////////////////////////////
// Human Functions                                                           //
///////////////////////////////////////////////////////////////////////////////
function initHuman()
{
   dbecho($LEVEL, "initHuman()");
   
   $human   = myGetObjectId("MissionGroup\\vehicles\\Human");
   $human.num = 10;
   
   $squad1  = myGetObjectId("MissionGroup\\vehicles\\Human\\Squad1"); 
   $squad2  = myGetObjectId("MissionGroup\\vehicles\\Human\\Squad2"); 
   $squad3  = myGetObjectId("MissionGroup\\vehicles\\Human\\Squad3"); 
   $squad4  = myGetObjectId("MissionGroup\\vehicles\\Human\\Squad4");
   $drone   = myGetObjectId("MissionGroup\\vehicles\\Human\\Drone\\cargo");
   $flyer   = myGetObjectId("MissionGroup\\vehicles\\Human\\Flyers\\MovingFlyer");         
   
   $squad1.attacked  = false;
   $squad2.attacked  = false;
   $squad3.attacked  = false;
   $squad4.attacked  = false;
   
   $squad1.route  = myGetObjectId("MissionGroup\\Patrols\\Squad1"); 
   $squad2.route  = myGetObjectId("MissionGroup\\Patrols\\Squad2"); 
   $squad3.n001    = myGetObjectId("MissionGroup\\Patrols\\Squad3\\001"); 
   $squad3.n002    = myGetObjectId("MissionGroup\\Patrols\\Squad3\\002"); 
   $drone.route   = myGetObjectId("MissionGroup\\Patrols\\Drone"); 
   $flyer.route   = myGetObjectId("MissionGroup\\Patrols\\Flyer"); 
    
   $squad1.lead    = myGetObjectId("MissionGroup\\vehicles\\Human\\Squad1\\Bas"); 
   $squad2.lead    = myGetObjectId("MissionGroup\\vehicles\\Human\\Squad2\\Bas"); 
   $squad3.lead    = myGetObjectId("MissionGroup\\vehicles\\Human\\Squad3\\Apo"); 
   $squad4.lead    = myGetObjectId("MissionGroup\\vehicles\\Human\\Squad4\\Mir1"); 

   order($squad1.lead,   MakeLeader, true);
   order($squad2.lead,   MakeLeader, true);
   order($squad3.lead,   MakeLeader, true);
   order($squad4.lead,   MakeLeader, true);

   order($squad3,  Formation, Wedge);
   order($squad4,  Formation, Wedge);
   
   order($squad1.lead, Guard, $squad1.route);
   order($squad2.lead, Guard, $squad2.route);

   order($drone, Guard, $drone.route);
}

function human::vehicle::onAttacked(%this, %who)
{
   if(%this == %who)
      return;
      
   %group = getGroup(%this);
   
   if(%group.attacked)
      return;
    
   %group.attacked = true;
   order( %group, Formation, Wedge );
   order( %group.lead, Attack, pick(playerSquad) );
}

function human::vehicle::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "human::vehicle::onDestroyed(" @ %this @ ", " @ %who @ ")");
   
   $human.num--;
   
   if($human.num == 2)
   {
      schedule("squad4Attacks();", 20.0);
   }
}

function drone::vehicle::onArrived(%this, %where)
{
   if(getObjectName(%where) == "pause")
   {
      order(%this, Shutdown, true);
      schedule("order(" @ %this @ ", Shutdown, false);", 10.0);
   } 
}

function squad4Attacks()
{
   if($playerSafe)
      return;

   calcAndDrop($squad4);
   order( $squad4.lead, Attack, pick(playerSquad) );
}   
   
///////////////////////////////////////////////////////////////////////////////
// Nav Point Functions                                                       //
///////////////////////////////////////////////////////////////////////////////
function initNavPoints()
{
   dbecho($LEVEL, "initNavPoints()");
   
   $nav001           = myGetObjectId("MissionGroup\\NavPoints\\001");
   $nav002           = myGetObjectId("MissionGroup\\NavPoints\\002");
   $missionCenter    = myGetObjectId("MissionGroup\\NavPoints\\MissionCenter");
   $navBogus         = myGetObjectId("MissionGroup\\NavPoints\\Bogus");
   
   $nav001.entered   = false;
   $nav002.entered   = false;
   
   setNavMarker($nav001, true, -1);
   setNavMarker($nav002, true);
   setNavMarker($navBogus, false);

   deselectNavOnEnter($nav001);

   checkDistance($nav001, $playerId, 400, "onEnterNav001", 5.0);
   checkDistance($nav002, $playerId, 400, "onEnterNav002", 5.0);
   checkDistance($nav001, $playerId, 400, "startOnLeave001", 1.0);
   checkDistance($nav002, $playerId, 400, "startOnLeave002", 1.0);

   checkDistance($nav002, $playerId, 1000, "launchFlyer", 5.0);

   checkBound($playerId, $missionCenter, 4000, "missionBoundWarning", 5.0); 
   checkBound($playerId, $missionCenter, 4100, "missionBoundFail", 5.0); 
}

function startOnLeave001()
{
   if($onLeaveTriggered)
      return;
   
   dbecho($LEVEL, "startOnLeave001()");
   checkBound($nav001, $playerId, 600, "onLeave", 1.0);
}      
function startOnLeave002()
{
   if($onLeaveTriggered)
      return;

   dbecho($LEVEL, "startOnLeave001()");
   checkBound($nav002, $playerId, 600, "onLeave", 1.0);
}      

function onEnterNav001()
{
   if( !($surprise) )
      return;
   
   if( $nav001.entered )
      return;
   
   dbecho($LEVEL, "onEnterNav001()");

   $nav001.entered = true;
   
   $surprise = false;
   setSquad3Position($squad3.n002);

   checkDistance($nav002, $playerId, 800, "onEnterNav002NoSurprise", 5.0);

   order( $squad1, Formation, Wedge );
   order( $squad1.lead, Attack, pick(playerSquad) );
   order( $turret1, Attack, pick(playerSquad) );
}

function onEnterNav001NoSurprise()
{
   if( $nav001.entered )
      return;

   dbecho($LEVEL, "onEnterNav001NoSurprise()");
 
   $nav001.entered = true;
   
   order( $squad1, Formation, Wedge );
   order( $squad1.lead, Attack, pick(playerSquad) );
   order( $turret1, Attack, pick(playerSquad) );
}   

function onEnterNav002()
{
   if( !($surprise) )
      return;
   
   if( $nav002.entered )
      return;

   dbecho($LEVEL, "onEnterNav002()");
   
   $nav002.entered = true;

   $surprise = false;
   setSquad3Position($squad3.n001);

   checkDistance($nav001, $playerId, 800, "onEnterNav001NoSurprise", 5.0);

   order( $squad2, Formation, Wedge );
   order( $squad2.lead, Attack, pick(playerSquad) );
   order( $turret2, Attack, pick(playerSquad) );
}

function onEnterNav002NoSurprise()
{
   if( $nav002.entered )
      return;

   dbecho($LEVEL, "onEnterNav002NoSurprise()");

   $nav002.entered = true;
   
   order( $squad2, Formation, Wedge );
   order( $squad2.lead, Attack, pick(playerSquad) );
   order( $turret2, Attack, pick(playerSquad) );
}   

function onLeave()
{
   dbecho($LEVEL, "onLeave()");

   $onLeaveTriggered = true;
   order( $squad3.lead, Guard, $playerId );
}

function setSquad3Position(%navPoint)
{
   dbecho(1, "setSquad3Position(" @ %navPoint @ ")");
   
   setPosition($squad3,    getPosition(%navPoint, x),
                           getPosition(%navPoint, y),
                           getPosition(%navPoint, z) + 100 );
}

function launchFlyer()
{
   order($flyer, Guard, $flyer.route);
}

function deselectNavOnEnter(%navPoint)
{
   if( getDistance(%navPoint, $playerId) <= 400 )
      setNavMarker($navBogus, false, -1);
   else
      schedule("deselectNavOnEnter(" @ %navPoint @ ");", 5.0 );
}
      


///////////////////////////////////////////////////////////////////////////////
// Base Functions                                                            //
///////////////////////////////////////////////////////////////////////////////
function initBase()
{
   dbecho($LEVEL, "initBase()");
   
   $base1 = myGetObjectId("MissionGroup\\Base\\001");
   $base2 = myGetObjectId("MissionGroup\\Base\\002");
   
   $base1.attacked   = false;
   $base2.attacked   = false;
   $base1.hqDestroyed   = false;
   $base2.hqDestroyed   = false;
   $base1.impNum     = 2;
   $base2.impNum     = 2;
   $base1.required   = 2;
   $base2.required   = 2;
   
   $civilians        = 4;
   $targetNum        = 22;
   
   %impGroup1  = myGetObjectId("MissionGroup\\Base\\001\\Imp");
   %impGroup2  = myGetObjectId("MissionGroup\\Base\\001\\Imp");
   %imp = GetNextObject(%impGroup1, 0);

   while(%imp != 0)
   {
      %imp.scanned = false;
      %imp = GetNextObject(%impGroup1, %imp);
   }
   %imp = GetNextObject(%impGroup2, 0);
   while(%imp != 0)
   {
      %imp.scanned = false;
      %imp = GetNextObject(%impGroup2, %imp);
   }

   %civGroup1  = myGetObjectId("MissionGroup\\Base\\001\\Civ");
   %civGroup2  = myGetObjectId("MissionGroup\\Base\\001\\Civ");
   %civ = GetNextObject(%civGroup1, 0);
   while(%civ != 0)
   {
      %civ.scanned = false;
      %civ = GetNextObject(%civGroup1, %civ);
   }
   %civ = GetNextObject(%civGroup2, 0);
   while(%civ != 0)
   {
      %civ.scanned = false;
      %civ = GetNextObject(%civGroup2, %civ);
   }
}

function structure::onAttacked(%this, %who)
{
   %base = getGroup(getGroup(%this));
   if( %base.attacked )
      return;
      
   dbecho($LEVEL, "structure::onAttacked(" @ %this @ ", " @ %who @  ")");
   
   %base.attacked = true;
   
   if ( %base == $base1 )
   {
      if ( $surprise )
         onEnterNav001();
      else
         onEnterNav001NoSurprise();
    }
   else if ( %base == $base2 )
   {
      if ( $surprise )
         onEnterNav002();
      else
         onEnterNav002NoSurprise();
    }
}

function imp::structure::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "imp::structure::onDestroyed(" @ %this @ ", " @ %who @ ")");
   
   %base = getGroup(getGroup(%this));
   %base.impNum--;
   
   if ( getObjectName(%this) == "hTroophq1" )
      %base.hqDestroyed = true;

   if( ( %base.impNum <= %base.required ) && (%base.hqDestroyed) )
   {
      if ( (%base == $base1) && (missionObjective1.status != *IDSTR_OBJ_COMPLETED) )
      {
         objectiveCompleted(1);
         if( missionObjective2.status != *IDSTR_OBJ_COMPLETED )
         {
            setNavMarker($nav002, true, -1);
            deselectNavOnEnter($nav002);
         }
      }
      else if ( (%base == $base2) && (missionObjective2.status != *IDSTR_OBJ_COMPLETED) )
      {
         objectiveCompleted(2);
         if( missionObjective1.status != *IDSTR_OBJ_COMPLETED )
         {
            setNavMarker($nav001, true, -1);
            deselectNavOnEnter($nav001);
         }
      }                    
   }
}

function imp::structure::onScan(%scanned, %scanner, %string)
{
   if(%scanned.scanned == true)
      return;

   dbecho($LEVEL, "imp::structure::onScan(" @ %scanned @ ", " @ %scanner @ ", " @ %string @ ")");

   %scanned.scanned = true;
   schedule("say(0, $playerId, *IDSTR_CC2_CPU02);", 0.25);
}

function civ::structure::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "civ::structure::onDestroyed(" @ %this @ ", " @ %who @ ")");
   killChannel(%this);
   
   $civilians--;
   
   if ( $civilians <= 0 )
      missionFailed(civKilled);
}

function civ::structure::onScan(%scanned, %scanner, %string)
{
   if(%scanned.scanned == true)
      return;

   dbecho($LEVEL, "civ::structure::onScan(" @ %scanned @ ", " @ %scanner @ ", " @ %string @ ")");

   %scanned.scanned = true;
   %num = randomInt(2, 5);
   %str = "<F5>" @ %num @ *IDSTR_CC2_CPU01;

   schedule("say(0," @ $playerId @ ", \"" @ %str @ "\");", 0.25);

   $targetNum = $targetNum - %num;
   if( $targetNum <= 0 && ! (missionObjective4.status == *IDSTR_OBJ_COMPLETED) )
      objectiveCompleted(4);      
}

function flyer::vehicle::onScan(%scanned, %scanner, %string)
{
   if(%scanned.scanned == true)
      return;

   dbecho($LEVEL, "imp::structure::onScan(" @ %scanned @ ", " @ %scanner @ ", " @ %string @ ")");

   %scanned.scanned = true;
   schedule("say(0, $playerId, *IDSTR_CC2_CPU02);", 0.25);
}

      
          
///////////////////////////////////////////////////////////////////////////////
// Mission Completion Functions                                              //
///////////////////////////////////////////////////////////////////////////////

//-----------------------------------------------------------------------------
function objectiveCompleted(%objective)
{
    dbecho(1, "objectiveCompleted(" @ %objective @ ")");
    
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
        if(%objective == 4)
        {
            missionObjective4.status = *IDSTR_OBJ_COMPLETED;
        }
        
        say(0, $playerId, *IDSTR_CYB_NEX17, "CYB_NEX17.wav");
    }
    
    if( missionObjective1.status == *IDSTR_OBJ_COMPLETED && 
        missionObjective2.status == *IDSTR_OBJ_COMPLETED && 
        missionObjective4.status == *IDSTR_OBJ_COMPLETED &&
        missionObjective3.status != *IDSTR_OBJ_COMPLETED  ) 
    {                                                       
        isPlayerSafe();                                   
    }                                                       

    if( missionObjective1.status == *IDSTR_OBJ_COMPLETED && 
        missionObjective2.status == *IDSTR_OBJ_COMPLETED && 
        missionObjective4.status == *IDSTR_OBJ_COMPLETED &&
        missionObjective3.status == *IDSTR_OBJ_COMPLETED  ) 
    {                                                       
        schedule("missionSuccess();", 10.0);                                   
    }                                                       
}


//-----------------------------------------------------------------------------
function missionSuccess()
{
    dbecho($LEVEL, "missionSuccess()");
    if( $missionFailed || $missionSuccess )
      return;
    
    $missionSuccess = true;
    updatePlanetInventory(CC2); 
    
    say(0, $playerId, *IDSTR_CYB_NEX04, "CYB_NEX04.wav");

    forceToDebrief();
}
//-----------------------------------------------------------------------------
function missionFailed(%reason)
{
    dbecho(1, "missionFailed(" @ %reason @ ")");
    
    if($missionSuccess)
      return;
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
    else
    if(%reason == "civKilled")
    {
        %msg = "Too Many Humans Killed";
        missionObjective4.status = *IDSTR_OBJ_FAILED;
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

function human::vehicle::onNewLeader(%this)
{  
   if(!$missionStart)
      return;
   
   dbecho(1, "onNewLeader(" @ %this @ ")"); 
   %group      = getGroup(%this);
   %group.lead = %this;
   order(%group, Formation, Wedge);
   order(%this, Attack, playerSquad);
}

