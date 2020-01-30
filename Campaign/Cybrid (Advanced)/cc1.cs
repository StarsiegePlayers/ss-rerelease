function win()
{  
	missionObjective1.status = *IDSTR_OBJ_COMPLETED;
	missionObjective2.status = *IDSTR_OBJ_COMPLETED;
	missionObjective3.status = *IDSTR_OBJ_COMPLETED;
	missionObjective4.status = *IDSTR_OBJ_COMPLETED;
	updatePlanetInventory(cc1);
   schedule("forceToDebrief();", 3.0);
}

function god()
{
    focusServer();
    $IAmGod = true;
    restore();
}
function ungod()
{
    focusServer();
    $IAmGod = false;
    focusClient();
}
function restore()
{
    if($IAmGod)
    {
        focusServer();
        reloadObject($playerId, 10000);
        healObject($playerId, 10000);
        healObject($playerId, 10000);
        healObject($playerId, 10000);
        healObject($playerId, 10000);
        healObject($playerId, 10000);
        focusClient();
        schedule("restore();", 2.0);
    }
}
///////////////////////////////////////////////////////////////////////////////
// CA4                                                                       //
//                                                                           //
// Primary Objectives                                                        //
// 1. Destroy the generator at NAV001                                        //
// 2. Destroy the generator at NAV002                                        //
// 3. Destroy the generator at NAV003                                        //
// 4. Secure Area                                                            //
///////////////////////////////////////////////////////////////////////////////

MissionBriefInfo missionBriefInfo
{
   campaign             = *IDSTR_CC1_CAMPAIGN;             
   title                = *IDSTR_CC1_TITLE;      
   planet               = *IDSTR_PLANET_ICE;     
   location             = *IDSTR_CC1_LOCATION; 
   dateOnMissionEnd     = *IDSTR_CC1_DATE;               
   shortDesc            = *IDSTR_CC1_SHORTBRIEF;
   longDescRichText     = *IDSTR_CC1_LONGBRIEF;
   media                = *IDSTR_CC1_MEDIA;
   successDescRichText  = *IDSTR_CC1_DEBRIEF_SUCC;
   failDescRichText     = *IDSTR_CC1_DEBRIEF_FAIL;
   nextMission          = *IDSTR_CC1_NEXTMISSION;
   successWavFile       = "CC1_Debriefing.wav";
   soundVol             = "CC1.vol";
    
};

MissionBriefObjective missionObjective1
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_CC1_OBJ1_SHORT;
   longTxt              = *IDSTR_CC1_OBJ1_LONG;
   bmpName              = *IDSTR_CC1_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_CC1_OBJ2_SHORT;
   longTxt              = *IDSTR_CC1_OBJ2_LONG;
   bmpName              = *IDSTR_CC1_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_CC1_OBJ3_SHORT;
   longTxt              = *IDSTR_CC1_OBJ3_LONG;
   bmpName              = *IDSTR_CC1_OBJ3_BMPNAME;
};

MissionBriefObjective missionObjective4
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_CC1_OBJ4_SHORT;
   longTxt              = *IDSTR_CC1_OBJ4_LONG;
   bmpName              = *IDSTR_CC1_OBJ4_BMPNAME;
};

DropPoint One
{
	name = "MissionGroup\\DropPointGroup1\\001";
	desc = "Drop Point 001";
};

DropPoint Two
{
	name = "MissionGroup\\DropPointGroup1\\002";
	desc = "Drop Point 002";
};

DropPoint Three
{
	name = "MissionGroup\\DropPointGroup1\\003";
	desc = "Drop Point 003";
};


//-----------------------------------------------------------------------------

function player::onAdd(%playerNum)
{
    $playerNum = %playerNum;
}
//-----------------------------------------------------------------------------
function onMissionStart(%playerNum)
{
   dbecho(1, "onMissionStart(" @ %playerNum @ ")");
   
   setHostile(*IDSTR_TEAM_RED, *IDSTR_TEAM_YELLOW);
   setHostile(*IDSTR_TEAM_YELLOW, *IDSTR_TEAM_RED);
   setHostile(*IDSTR_TEAM_PURPLE);

   iceSounds();
   windSounds();

   $missionSuccess   = false;
   $missionFailed    = false;
   $missionStart     = false;
   $BOOL             = false;
   $PI               = 3.14;
   $outerAttack      = 0;
}
//-----------------------------------------------------------------------------
function onSPClientInit()
{
   init();
}

//-----------------------------------------------------------------------------
function init()
{
    dbecho(1, "init()");
    
    initFormations();
    initPlayer();
    initNavPoints();
    initHuman();
    initTransport();
    initBase();
    initMission();
}

///////////////////////////////////////////////////////////////////////////////
// INIT Functions                                                            //
///////////////////////////////////////////////////////////////////////////////
//-----------------------------------------------------------------------------
function initFormations()
{
    dbecho (1, "initFormations()");
    
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
   $missionStart    = true;
   cinematicDrop();
   orderPatrols();
   schedule("cdAudioCycle(7, 10);", 10.0);
}

//-----------------------------------------------------------------------------
function missionBoundWarning()
{
   dbecho(1, "missionBoundWarning()");
   say(0, $playerId, *IDSTR_CYB_NEX01, "CYB_NEX01.wav");
}
//-----------------------------------------------------------------------------

function missionBoundFail()
{
   dbecho(1, "missionBoundFail()");
   say(0, $playerId, *IDSTR_CYB_NEX02, "CYB_NEX02.wav");
   missionFailed(playerLeftMissionArea);
}

///////////////////////////////////////////////////////////////////////////////
// Player Functions                                                          //
///////////////////////////////////////////////////////////////////////////////
function initPlayer()
{
   dbecho(1, "initPlayer()");
   $playerId = playerManager::playerNumToVehicleId($playerNum);
   $playerSquad   = myGetObjectId(playerSquad);
   
   %num    = 1;
   $playerSquad.num[%num] = GetNextObject($playerSquad, $playerId);
   while($playerSquad.num[%num] != 0)
   {
      if($playerSquad.num[%num] != $playerId)
         setHercOwner( $playerSquad.num[%num], "MissionGroup\\vehicles\\HUMAN\\Transport" );
      
      %num++;
      $playerSquad.num[%num] = GetNextObject($playerSquad, $playerSquad.num[%num - 1]);
   }


}
//-----------------------------------------------------------------------------

function isPlayerSafe()
{
   dbecho(1, "isPlayerSafe()");
   
   if( IsSafe(*IDSTR_TEAM_YELLOW, $playerId, 1000) )
      objectiveCompleted(4);
   else
      schedule("isPlayerSafe();", 1.0);
}

///////////////////////////////////////////////////////////////////////////////
// Human Functions                                                           //
///////////////////////////////////////////////////////////////////////////////

function initHuman()
{
   dbecho(1, "initHuman()");
   
   $squadGroup = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad");
   
   $squad1        = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\1");
   $squad2        = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\2");
   $squad3        = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\3");
   $squad4Left    = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\4\\Left");
   $squad4Right   = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\4\\Right");

   $squad1.lead = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\1\\MIN");
   $squad2.lead = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\2\\Gor");
   $squad3.lead = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\3\\Bas");
   $squad4Left.lead    = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\4\\Left\\Apo");
   $squad4Right.lead   = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\squad\\4\\Right\\Bas");
   
   $squad1.attacked = false;
   $squad2.attacked = false;
   $squad3.attacked = false;
   
   $patrol1 = myGetObjectId("MissionGroup\\Patrols\\squad1");
   $patrol2 = myGetObjectId("MissionGroup\\Patrols\\squad2");
   $patrol3 = myGetObjectId("MissionGroup\\Patrols\\squad3");
   $patrol4Left   = myGetObjectId("MissionGroup\\Patrols\\squad4Left");
   $patrol4Right  = myGetObjectId("MissionGroup\\Patrols\\squad4Right");

   order($squad1.lead, MakeLeader, true);
   order($squad2.lead, MakeLeader, true);
   order($squad3.lead, MakeLeader, true);
   order($squad4Left.lead, MakeLeader, true);
   order($squad4Right.lead, MakeLeader, true);

   order($squad1, Formation, Wedge);
   order($squad2, Formation, Wedge);
   order($squad3, Formation, Wedge);
   order($squad4Left, Formation, Wedge);
   order($squad4Right, Formation, Wedge);
   
   order($squad1, Speed, Medium);
   order($squad2, Speed, Medium);
   order($squad3, Speed, Medium);
   order($squad4Left, Speed, Medium);
   order($squad4Right, Speed, Medium);
                
   order($squad1, HoldPosition, $BOOL);
   order($squad2, HoldPosition, $BOOL);
   order($squad3, HoldPosition, $BOOL);
   order($squad4Left, HoldPosition, $BOOL);
   order($squad4Right, HoldPosition, $BOOL);

   order($squad1, HoldFire, $BOOL);
   order($squad2, HoldFire, $BOOL);
   order($squad3, HoldFire, $BOOL);
   order($squad4Left, HoldFire, $BOOL);
   order($squad4Right, HoldFire, $BOOL);

}
//-----------------------------------------------------------------------------

function orderPatrols()
{
   dbecho(1, "orderPatrols()");
   
   order($squad1.lead,    Guard,   $patrol1 );
   order($squad2.lead,    Guard,   $patrol2 );
   order($squad3.lead,    Guard,   $patrol3 );
   order($squad4Left.lead,    Guard,   $patrol4Left );
   order($squad4Right.lead,   Guard,   $patrol4Right );
}

//-----------------------------------------------------------------------------
function outerAttack()
{
   if($outerAttack > 2)
      return;
   dbecho(1, "outerAttack()");

   $outerAttack++;
   if( $outerAttack == 1 )
   {
      vehicleCalcAndDrop( $squad4Left );
      order( $squad4Left.lead, Attack, pick(playerSquad) );
      say(0, $playerId, *IDSTR_CC1_NEX01, "CC1_NEX01.wav");     
   }
   else if( $outerAttack == 2 )
   {
      vehicleCalcAndDrop( $squad4Right );
      order( $squad4Right.lead, Attack, pick(playerSquad) );
   }
}

//-----------------------------------------------------------------------------
function outer::vehicle::onDestroyed(%this, %who)
{
   order( squad4Left.lead, Attack, pick(playerSquad) );
   order( squad4Right.lead, Attack, pick(playerSquad) );
}

//-----------------------------------------------------------------------------
function human::vehicle::onAttacked(%this, %who)
{
   if(%this != %who)
   {
      if( !(getGroup(%this).attacked) )
      {
         dbecho(1, "human::vehicle::onAttacked(" @ %this @ ", " @ %who @ ")");
         getGroup(%this).attacked = true;
         order(getGroup(%this), Speed, High);
         order(getGroup(%this).lead, Attack, playerSquad);
      }
   }
}


///////////////////////////////////////////////////////////////////////////////
// Nav Point Functions                                                       //
///////////////////////////////////////////////////////////////////////////////
function initNavPoints()
{
   dbecho(1, "initNavPoints()");
   
   $nav001        = myGetObjectId("MissionGroup\\NavPoints\\001");
   $nav002        = myGetObjectId("MissionGroup\\NavPoints\\002");
   $nav003        = myGetObjectId("MissionGroup\\NavPoints\\003");
   $navBogus      = myGetObjectId("MissionGroup\\NavPoints\\Bogus");
   $missionCenter = myGetObjectId("MissionGroup\\NavPoints\\Central");
   
   setNavMarker($nav001, true);
   setNavMarker($nav002, true);
   setNavMarker($nav003, true);
   setNavMarker($navBogus, false);
   
   if     ($client::DropPointName == "MissionGroup\\DropPointGroup1\\001")
      setNavMarker($nav001, true, -1);
   else if($client::DropPointName == "MissionGroup\\DropPointGroup1\\002")
      setNavMarker($nav002, true, -1);
   else if($client::DropPointName == "MissionGroup\\DropPointGroup1\\003")
      setNavMarker($nav003, true, -1);
   
   checkDistance($nav001, $playerId, 600, "onEnterNav001", 5.0);
   checkDistance($nav002, $playerId, 600, "onEnterNav002", 5.0);
   checkDistance($nav003, $playerId, 600, "onEnterNav003", 5.0);
   
   checkDistance($nav002, $playerId, 1000, "onEnterNav002Long", 1.0);
   
   checkBound($playerId, $missionCenter, 4000, "missionBoundWarning", 5.0); 
   checkBound($playerId, $missionCenter, 4100, "missionBoundFail", 5.0); 
   
}

//-----------------------------------------------------------------------------
function onEnterNav001()
{
   dbecho(1, "onEnterNav001()");
   setNavMarker($nav001, true);
   cycleNavMarkers(1);
   order($squad1, Speed, High);
   order($squad1.lead, Attack, playerSquad);
   order( $turret1, Attack, pick(playerSquad) );
   launchTrans2();
   checkBound($nav001, $playerId, 700, "outerAttack", 1.0);
}


//-----------------------------------------------------------------------------
function onEnterNav002()
{
   dbecho(1, "onEnterNav002()");
   setNavMarker($nav002, true);
   cycleNavMarkers(2);
   order($squad2, Speed, High);
   order($squad2.lead, Attack, playerSquad);
   order( $turret2, Attack, pick(playerSquad) );
   
   %num = randomInt(700, 1100);

   checkBound($nav002, $playerId, %num, "outerAttack", 1.0);
}
//-----------------------------------------------------------------------------
function onEnterNav002Long()
{
   dbecho(1, "onEnterNav002Long()");
   launchTrans1();
}

//-----------------------------------------------------------------------------
function onEnterNav003()
{
   dbecho(1, "onEnterNav003()");
   setNavMarker($nav003, true);
   cycleNavMarkers(3);
   order($squad3, Speed, High);
   order($squad3.lead, Attack, playerSquad);
   order( $turret3, Attack, pick(playerSquad) );
   checkBound($nav003, $playerId, 700, "outerAttack", 1.0);
}
//-----------------------------------------------------------------------------

function cycleNavMarkers(%nav)
{
   if( %nav == 1 )
   {
      if( missionObjective2.status != *IDSTR_OBJ_COMPLETED )
         setNavMarker($nav002, true, -1);
      else if( missionObjective3.status != *IDSTR_OBJ_COMPLETED )
         setNavMarker($nav003, true, -1);
   }
   else if( %nav == 2 )
   {
      if( missionObjective3.status != *IDSTR_OBJ_COMPLETED )
         setNavMarker($nav003, true, -1);
      else if( missionObjective1.status != *IDSTR_OBJ_COMPLETED )
         setNavMarker($nav001, true, -1);
   }
   else if( %nav == 3 )
   {
      if( missionObjective1.status != *IDSTR_OBJ_COMPLETED )
         setNavMarker($nav001, true, -1);
      else if( missionObjective2.status != *IDSTR_OBJ_COMPLETED )
         setNavMarker($nav002, true, -1);
   }
}         
   
//-----------------------------------------------------------------------------
  

///////////////////////////////////////////////////////////////////////////////
// Base Function                                                             //
///////////////////////////////////////////////////////////////////////////////
function initBase()
{
   dbecho(1, "initBase()");
   
   $base1         = myGetObjectId("MissionGroup\\Base\\1");
   $base2         = myGetObjectId("MissionGroup\\Base\\2");
   $base3         = myGetObjectId("MissionGroup\\Base\\3");
   
   $base1.attacked      = false;
   $base2.attacked      = false;
   $base3.attacked      = false;
   
   $defense1_1    = myGetObjectId("MissionGroup\\Base\\1\\Defense1");
   $defense1_2    = myGetObjectId("MissionGroup\\Base\\1\\Defense2");

   $defense2_1    = myGetObjectId("MissionGroup\\Base\\2\\Defense1");

   $defense3_1    = myGetObjectId("MissionGroup\\Base\\3\\Defense1");
   $defense3_2    = myGetObjectId("MissionGroup\\Base\\3\\Defense2");
   $defense3_3    = myGetObjectId("MissionGroup\\Base\\3\\Defense3");
   
   $power1        = myGetObjectId("MissionGroup\\Base\\1\\Power");
   $power2        = myGetObjectId("MissionGroup\\Base\\2\\Power");
   $power3_1      = myGetObjectId("MissionGroup\\Base\\3\\Power1");
   $power3_2      = myGetObjectId("MissionGroup\\Base\\3\\Power2");
   $power3_oneDestroyed = false;

   $power1.beam   = myGetObjectId("MissionGroup\\Base\\1\\Beam");
   $power2.beam   = myGetObjectId("MissionGroup\\Base\\2\\Beam");
   $power3_1.beam = myGetObjectId("MissionGroup\\Base\\3\\Beam1");
   $power3_2.beam = myGetObjectId("MissionGroup\\Base\\3\\Beam2");

   $power1.halfDestroyed   = false;
   $power2.halfDestroyed   = false;
   $power3_1.halfDestroyed = false;
   $power3_2.halfDestroyed = false;

   $turret1       = myGetObjectId("MissionGroup\\Turrets\\1");
   $turret2       = myGetObjectId("MissionGroup\\Turrets\\2");
   $turret3       = myGetObjectId("MissionGroup\\Turrets\\3");
   
   $turret1.attacked = false;
   $turret2.attacked = false;
   $turret3.attacked = false;
   
   schedule("playAnimSequence(" @ $defense1_1 @ ", 0);",  1.0);
   schedule("playAnimSequence(" @ $defense1_2 @ ", 0);",  9.0);
   schedule("playAnimSequence(" @ $defense2_1 @ ", 0);", 12.0);
   schedule("playAnimSequence(" @ $defense3_1 @ ", 0);",  4.0);
   schedule("playAnimSequence(" @ $defense3_2 @ ", 0);",  7.0);
   schedule("playAnimSequence(" @ $defense3_3 @ ", 0);", 15.0);
   
   

}
//-----------------------------------------------------------------------------

function turret::onAttacked(%this, %who)
{
   dbecho(1, "turret::onAttacked(" @ %this @ ", " @ %who @ ")");
   
   %group = getGroup(%this);
   
   if( ! ( %group.attacked ) )
   {
      dbecho(1, "turret::onAttacked(" @ %this @ ", " @ %who @ ");");
      
      %group.attacked = true;
      order( %group, Attack, pick(playerSquad) );
      
      if (%group == $turret1)
         order($squad1.lead, Attack, playerSquad);
      else if(%group == $turret2)
         order($squad2.lead, Attack, playerSquad);
      else if(%group == $turret3)
         order($squad3.lead, Attack, playerSquad);
   }
}   

//-----------------------------------------------------------------------------
function base1::structure::onAttacked(%this, %who)
{
   if(%this != %who)
   {                                                                                                
      if( !($base1.attacked) )                                                                      
      {                                                                                             
         dbecho(1, "base1::structure::onAttacked(" @ %this @ ", " @ %who @ ")");

         $base1.attacked = true;                                                                   
         order($squad1.lead, Attack, pick(playerSquad) );
      }
   }
}
//-----------------------------------------------------------------------------
function base2::structure::onAttacked(%this, %who)
{
   if(%this != %who)
   {
      if( !($base2.attacked) )
      {
         dbecho(1, "base2::structure::onAttacked(" @ %this @ ", " @ %who @ ")");

         $base2.attacked = true;
         order($squad2.lead, Attack, pick(playerSquad) );
      }
   }
}
//-----------------------------------------------------------------------------
function base3::structure::onAttacked(%this, %who)
{
   if(%this != %who)
   {
      if( !($base3.attacked) )
      {
         dbecho(1, "base3::structure::onAttacked(" @ %this @ ", " @ %who @ ")");

         $base3.attacked = true;
         order($squad3.lead, Attack, pick(playerSquad) );
      }
   }
}

//-----------------------------------------------------------------------------
function base1::structure::onDestroyed(%this, %who)
{
   dbecho(1, "base1::structure::onDestroyed(" @ %this @ ", " @ %who @ ")");

   %group = getGroup(%this);
   if( %group == $power1 && %group.halfDestroyed != true )
   {
      %group.halfDestroyed = true;
      objectiveCompleted(1);
      stopAnimSequence($defense1_1, 0);
      stopAnimSequence($defense1_2, 0);
      playAnimSequence(%group.beam, 0);
   }
}

//-----------------------------------------------------------------------------
function base2::structure::onDestroyed(%this, %who)
{
   dbecho(1, "base2::structure::onDestroyed(" @ %this @ ", " @ %who @ ")");
   %group = getGroup(%this);
   if( %group == $power2 && %group.halfDestroyed != true )
   {
      %group.halfDestroyed = true;
      objectiveCompleted(2);
      stopAnimSequence($defense2_1, 0);
      playAnimSequence(%group.beam, 0);
   }
}
//-----------------------------------------------------------------------------

function base3::structure::onDestroyed(%this, %who)
{
   dbecho(1, "base3::structure::onDestroyed(" @ %this @ ", " @ %who @ ")");

   %group = getGroup(%this);
   if( ( %group == $power3_1 || %group == $power3_2 ) && ( %group.halfDestroyed != true ) )
   {
      playAnimSequence(%group.beam, 0);
      
      %group.halfDestroyed = true;

      if($power3_oneDestroyed)
      {
         objectiveCompleted(3);
         stopAnimSequence($defense3_1, 0);
         stopAnimSequence($defense3_2, 0);
         stopAnimSequence($defense3_3, 0);
      }
      $power3_oneDestroyed = true;
   }
}
///////////////////////////////////////////////////////////////////////////////
// Transport Functions                                                       //
///////////////////////////////////////////////////////////////////////////////
function initTransport()
{
   $trans1  = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Transport\\Transport1");
   $trans2  = myGetObjectId("MissionGroup\\vehicles\\HUMAN\\Transport\\Transport2");

   $trans2.route  = myGetObjectId("MissionGroup\\Patrols\\Transport\\2");
   $trans1.land   = myGetObjectId("MissionGroup\\Patrols\\Transport\\1\\land");
   $trans1.end    = myGetObjectId("MissionGroup\\Patrols\\Transport\\1\\end");
   
   order($trans1, Shutdown, true);
   order($trans2, Shutdown, true);
}

function launchTrans1()
{
   order($trans1, Guard, $trans1.land);
}
  
function launchTrans2()
{
   order($trans2, Guard, $trans2.route);
}

function transport::vehicle::onArrived(%this, %where)
{
   if(getObjectName(%where) == "land")
      schedule("order(" @ %this @ ", Guard," @  %this @ ".end);", 60.0);
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
        missionObjective3.status == *IDSTR_OBJ_COMPLETED && 
        missionObjective4.status != *IDSTR_OBJ_COMPLETED  ) 
    {                                                       
        isPlayerSafe();                                   
    }                                                       
    if( missionObjective1.status == *IDSTR_OBJ_COMPLETED && 
        missionObjective2.status == *IDSTR_OBJ_COMPLETED && 
        missionObjective3.status == *IDSTR_OBJ_COMPLETED && 
        missionObjective4.status == *IDSTR_OBJ_COMPLETED  ) 
    {                                                       
        schedule( "missionSuccess();", 10.0 );                                   
    }                                                       
}


//-----------------------------------------------------------------------------
function missionSuccess()
{
    dbecho(1, "missionSuccess()");
    
    if($missionFailed || $missionSuccess)
      return;
    
    $missionSuccess = true;
    updatePlanetInventory(CC1); 
    
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

function vehicle::onNewLeader(%this)
{
   if( !($missionStart) )
      return;

   dbecho($LEVEL, "vehicle::onNewLeader(" @ %this @ ")"); 
   
   %group      = getGroup(%this);
   %group.lead = %this;
   order(%group,        Formation,  Wedge);
   order(%group.lead,   Attack,     pick(playerSquad) );
}

function cinematicDrop()
{
   %next    = randomInt(20, 30);
   %num     = randomInt(1, 5);
   %delay   = 1;
   
   while ( %num > 0 )
   {
      schedule("calcAndDrop();", %delay);
      %num--;
      %delay++;
   }
   
   schedule("cinematicDrop();", %next);
}

function calcAndDrop()
{  
   
   %offset  = randomInt(-2500, 2500);  
   
   %x    = getPosition($playerId, x);
   %y    = getPosition($playerId, y);
   %z    = getPosition($playerId, z);
   %rot  = getPosition($playerId, r);
   
   if(     %rot >=   -($PI/4)    &&      %rot <    ($PI/4)   )
   {
       %y = %y + 3500;
       %x = %x + %offset;
   }
   else
   if(     %rot >=    ($PI/4)    &&      %rot <  3*($PI/4)   )
   {
       %x = %x - 3500;
       %y = %y + %offset;
   }
   else
   if(     %rot >=  3*($PI/4)    ||      %rot < -3*($PI/4)   )
   {
       %y = %y - 3500;
       %x = %x + %offset;
   }
   else
   if(     %rot >= -3*($PI/4)    &&      %rot <   -($PI/4)   )
   {
       %x = %x + 3500;
       %y = %y + %offset;
   }
   
   setDropPodParams(0.25, 0.25, -0.935, ( (getTerrainHeight(%x, %y) ) + 1000 )  );
   dropPod( %x, %y, getTerrainHeight(%x, %y) );
}

function vehicleCalcAndDrop(%squad)
{  
   %goofy = randomInt(0, 1);
   if( %goofy == 0 )
      %goofy = -1;  

   %x    = getPosition($playerId, x);
   %y    = getPosition($playerId, y);
   %z    = getPosition($playerId, z);
   %rot  = getPosition($playerId, r);
   
   if(     %rot >=   -($PI/4)    &&      %rot <    ($PI/4)   )
   {
       %x = %x + (%goofy)*500;
   }
   else
   if(     %rot >=    ($PI/4)    &&      %rot <  3*($PI/4)   )
   {
       %y = %y + (%goofy)*500;
   }
   else
   if(     %rot >=  3*($PI/4)    ||      %rot < -3*($PI/4)   )
   {
       %x = %x + (%goofy)*500;
   }
   else
   if(     %rot >= -3*($PI/4)    &&      %rot <   -($PI/4)   )
   {
       %y = %y + (%goofy)*500;
   }
   setPosition( %squad, %x, %y, getTerrainHeight(%x, %y) );
}

        





