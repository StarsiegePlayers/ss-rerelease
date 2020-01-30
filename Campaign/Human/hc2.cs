$server::HudMapViewOffsetX = 60; 
$server::HudMapViewOffsetY = 10; 
$LEVEL = 1;
function win()
{  
	missionObjective1.status = *IDSTR_OBJ_COMPLETED;
	missionObjective2.status = *IDSTR_OBJ_COMPLETED;
	missionObjective3.status = *IDSTR_OBJ_COMPLETED;
	updatePlanetInventory(hc2);
   schedule("forceToDebrief();", 3.0);
}
///////////////////////////////////////////////////////////////////////////////
// HC2                                                                       //
//                                                                           //
// Primary Objectives                                                        //
// 1. Locate the Cybrid Nexus                                                //
// 2. Report position of the Nexus                                           //
// 3. Destroy the Nexus escorts                                              //
//                                                                           //
// Secondary Objectives                                                      //
// 4. Destroy any resistance you encounter                                   //
// 5. Scan the ruins for survivors                                           //
///////////////////////////////////////////////////////////////////////////////

MissionBriefInfo missionBriefInfo
{
   campaign             = *IDSTR_HC2_CAMPAIGN;             
   title                = *IDSTR_HC2_TITLE;      
   planet               = *IDSTR_PLANET_VENUS;     
   location             = *IDSTR_HC2_LOCATION; 
   dateOnMissionEnd     = *IDSTR_HC2_DATE;               
   shortDesc            = *IDSTR_HC2_SHORTBRIEF;
   longDescRichText     = *IDSTR_HC2_LONGBRIEF;
   media                = *IDSTR_HC2_MEDIA;
   successDescRichText  = *IDSTR_HC2_DEBRIEF_SUCC;
   failDescRichText     = *IDSTR_HC2_DEBRIEF_FAIL;
   nextMission          = *IDSTR_HC2_NEXTMISSION;
   soundVol             = "HC2.vol";
   successWavFile       = "HC2_Debriefing.wav";
};

MissionBriefObjective missionObjective1
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HC2_OBJ1_SHORT;
   longTxt              = *IDSTR_HC2_OBJ1_LONG;
   bmpName              = *IDSTR_HC2_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HC2_OBJ2_SHORT;
   longTxt              = *IDSTR_HC2_OBJ2_LONG;
   bmpName              = *IDSTR_HC2_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HC2_OBJ3_SHORT;
   longTxt              = *IDSTR_HC2_OBJ3_LONG;
   bmpName              = *IDSTR_HC2_OBJ3_BMPNAME;
};

MissionBriefObjective missionObjective4
{
   isPrimary            = false;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HC2_OBJ4_SHORT;
   longTxt              = *IDSTR_HC2_OBJ4_LONG;
   bmpName              = *IDSTR_HC2_OBJ4_BMPNAME;
};

MissionBriefObjective missionObjective5
{
   isPrimary            = false;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HC2_OBJ5_SHORT;
   longTxt              = *IDSTR_HC2_OBJ5_LONG;
   bmpName              = *IDSTR_HC2_OBJ5_BMPNAME;
};

Pilot Nexus
{
   id = 29;
   
   name = "Cybrid Nexus";
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
   
//    setHostile(*IDSTR_TEAM_RED);    
//    setHostile(*IDSTR_TEAM_YELLOW); 
//    setHostile(*IDSTR_TEAM_PURPLE); 

   setHostile(*IDSTR_TEAM_RED, *IDSTR_TEAM_YELLOW); 
   setHostile(*IDSTR_TEAM_YELLOW, *IDSTR_TEAM_RED); 
   setHostile(*IDSTR_TEAM_PURPLE);                  

   venusSounds();
   cdAudioCycle(5, 8);

   $missionStart        = false;
   $missionFailed       = false;
   $missionSuccess      = false;
   $scannedStructure    = false;
   $survivorTalking     = false;
   $caravanTalking      = false;
   $scannedCaravan      = false;
   $reportedSurvivors   = false;
   $reportedNexus       = false;

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
   initBase();
   initNavPoints();
   initCybrids();
   initTransport();
   initCaravan();
   
   $missionStart = true;
}

function outro()
{
   dbecho($LEVEL, "outro()");
   
   $flyerGroup = myGetObjectId("MissionGroup\\Outro\\Flyer");
   $routeGroup = myGetObjectId("MissionGroup\\Outro\\Route");
   $lead = myGetObjectId("MissionGroup\\Outro\\Flyer\\Lead");
   
   setFlybyCamera($lead, -6, 40, 20);
   
   %flyer = GetNextObject($flyerGroup, 0);
   %route = GetNextObject($routeGroup, 0);
   while (%flyer != 0 && %route != 0)
   {
      order(%flyer, Guard, %route); 
      schedule("order(" @ %flyer @ ", Height, 500, 1000);", 13.0); 
      %flyer = GetNextObject($flyerGroup, %flyer);
      %route = GetNextObject($routeGroup, %route);
   }
}

///////////////////////////////////////////////////////////////////////////////
// INIT Functions                                                            //
///////////////////////////////////////////////////////////////////////////////
//-----------------------------------------------------------------------------
function initFormations()
{
   dbecho ($LEVEL, "initFormations()");
 
   newFormation(Wedge,     0,  0,  0,
                         -40,-40,  0,
                          40,-40,  0);
   
                           
   newFormation(Peace,     0,  0,  0,
                           0, 80,  0,  
                         -80,-80,  0,
                          80,-80,  0);
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
         setHercOwner( $playerSquad.num[%num], "MissionGroup\\vehicles\\CYBRID\\Nexus\\Nexus" );
      
      %num++;
      $playerSquad.num[%num] = GetNextObject($playerSquad, $playerSquad.num[%num - 1]);
   }
}

///////////////////////////////////////////////////////////////////////////////
// Nav Point Functions                                                       //
///////////////////////////////////////////////////////////////////////////////

function initNavPoints()
{
   dbecho($LEVEL, "initNavPoints()");
   
   $navGroup = myGetObjectId("MissionGroup\\NavPoints");
   
   $navAlpha      = myGetObjectId("MissionGroup\\NavPoints\\NavAlpha");
   $navBogus      = myGetObjectId("MissionGroup\\NavPoints\\Bogus");
   $missionCenter = myGetObjectId("MissionGroup\\NavPoints\\MissionCenter");
   
   $navAlpha.set = false;
   
   setNavMarker($navAlpha, true, -1);
   setNavMarker($navBogus, false);
   
   %distance = randomInt(1500, 2500);
   
   checkDistance($playerId, $navAlpha, %distance, onEnterGoadTrap,  1.0); 
   checkDistance($playerId, $navAlpha, 1400, startExecutioner, 5.0); 
   checkDistance($playerId, $navAlpha,  400, onEnterNavAlpha,  5.0); 
   
   checkBound($playerId, $missionCenter, 6000, "missionBoundWarning", 5.0); 
   checkBound($playerId, $missionCenter, 6500, "missionBoundFail", 5.0); 

}
//----------------------------------------------------------------------------

function onEnterGoadTrap()
{
   dbecho($LEVEL, "onEnterGoadTrap()");
   
   calcandDrop();
   order($goad.lead, Attack, pick(playerSquad) );
}

function startExecutioner()
{
   dbecho($LEVEL, "startExecutioner()");
   order($executioner, Guard, $executioner.route);
}


function onEnterNavAlpha()
{
   dbecho($LEVEL, "onEnterNavAlpha()");
   
   setNavMarker($navBogus, false, -1);
   order($executioner, Attack, pick(playerSquad) );
}
//----------------------------------------------------------------------------

function missionBoundWarning()
{
   dbecho($LEVEL, "missionBoundWarning()");
   say(0, $playerId, *IDSTR_GEN_TCV1, "GEN_TCV01.wav");
}
//----------------------------------------------------------------------------

function missionBoundFail()
{
   dbecho($LEVEL, "missionBoundFail()");
   say(0, $playerId, *IDSTR_GEN_TCV2, "GEN_TCV02.wav");
   missionFailed(playerLeftMissionArea);
}
///////////////////////////////////////////////////////////////////////////////
// Cybrid Functions                                                          //
///////////////////////////////////////////////////////////////////////////////

//-----------------------------------------------------------------------------
function initCybrids()
{
      
   dbecho($LEVEL, "initCybrids()");
   
   $cybrid              = myGetObjectId("MissionGroup\\vehicles\\CYBRID");
   $cybrid.num          = 15;
   
   $patrol1             = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Patrol1");
   $patrol2             = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Patrol2");
   $nexusGroup          = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Nexus");
   $nexusGroup.num      = 3;

   $nexus               = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Nexus\\Nexus");
   $adjudicator1        = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Nexus\\Adjudicator1");
   $adjudicator2        = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Nexus\\Adjudicator2");
   $adjudicator3        = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Nexus\\Adjudicator3");
   
   $patrol1.lead        = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Patrol1\\Seeker1");
   $patrol2.lead        = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Patrol2\\Shepherd1");
   $nexusLead           = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Nexus\\Nexus");
   
   $nexusRoute          = myGetObjectId("MissionGroup\\Patrols\\Nexus");
   $finalNexus          = myGetObjectId("MissionGroup\\Patrols\\FinalNexus");
   $patrol1.route       = myGetObjectId("MissionGroup\\Patrols\\Patrol1");
   $patrol2.route       = myGetObjectId("MissionGroup\\Patrols\\Patrol2");

   $nexusGroup.attacking  = false;
   
   $executioner         = myGetObjectId("MissionGroup\\vehicles\\Cybrid\\Executioner\\Exec");
   $executioner.route   = myGetObjectId("MissionGroup\\Patrols\\Executioner");

   $goad                = myGetObjectId("MissionGroup\\vehicles\\Cybrid\\Goad");
   $goad.lead           = myGetObjectId("MissionGroup\\vehicles\\Cybrid\\Goad\\Goad1");

   $recluse             = myGetObjectId("MissionGroup\\vehicles\\Cybrid\\Recluse");
   $recluse1            = myGetObjectId("MissionGroup\\vehicles\\Cybrid\\Recluse\\r1");
   $recluse2            = myGetObjectId("MissionGroup\\vehicles\\Cybrid\\Recluse\\r2");
   $recluse3            = myGetObjectId("MissionGroup\\vehicles\\Cybrid\\Recluse\\r3");
   
   $trap                = myGetObjectId("MissionGroup\\trap");
   
   $recluse1.attacked      = false;
   $recluse2.attacked      = false;
   $recluse3.attacked      = false;
   
   order($patrol1.lead,     MakeLeader, true);
   order($patrol2.lead,     MakeLeader, true);
   order($nexusLead  ,     MakeLeader, true);
   order($goad.lead  ,     MakeLeader, true);
   
   order($nexusGroup,      Formation,  Peace);
   order($patrol1,         Formation,  Wedge);
   order($patrol2,         Formation,  Wedge);
   order($goad,            Formation,  Wedge);
   
   order($patrol1,         HoldPosition, false);
   order($patrol2,         HoldPosition, false);
   order($nexusGroup,      HoldPosition, true);
   order($goad,            HoldPosition, false);
   order($recluse,         HoldPosition, true);
   order($executioner,     HoldPosition, false);
   
   order($patrol1,         HoldFire, false);
   order($patrol2,         HoldFire, false);
   order($nexusGroup,      HoldFire, true);
   order($goad,            HoldFire, false);
   order($recluse,         HoldFire, false);
   order($executioner,     HoldFire, false);
   
   order($patrol1,         Speed, High);    
   order($patrol2,         Speed, High);    
   order($nexusGroup,      Speed, High);    
   order($goad,            Speed, High);    
   order($recluse,         Speed, High);    
   order($executioner,     Speed, Medium);    
   
   order($nexus, Speed, Low);
   
   order($patrol1.lead,    Guard,   $patrol1.route); 
   order($patrol2.lead,    Guard,   $patrol2.route); 
   
   checkDistance($playerId, $nexus, 2460, onEnterNexus1, 5.0); 
   checkDistance($playerId, $nexus, 2030, onEnterNexus2, 5.0); 
   checkDistance($playerId, $nexus,  800, onEnterNexus3, 5.0); 

   checkDistance($playerId, $trap,  1000, onEnterRecluseTrap, 5.0); 
}

function nexus::vehicle::onTargeted(%this, %who){}

function nexus::vehicle::onAttacked(%this, %who)
{
    healObject(%this, 100000); 
    healObject(%this, 100000); 
    healObject(%this, 100000); 
}

function nexus::vehicle::onDestroyed(%this, %who)
{
   setDominantCamera(%this, %who);
   missionFailed(nexusDestroyed);
}
   

function nexus::vehicle::onArrived(%this, %where)
{
   if(getObjectName(%where) == "end")
   {   
      order($nexus, Guard, $finalNexus);
      order($nexus, Speed, Low   );
   }
}   

function cybrid::vehicle::onAttacked(%this, %who)
{
   if(%this != %who)
   {
      if( getGroup(%this) ==  $patrol1 )
      {
         order($patrol1.lead, Attack, pick(playerSquad) );
      }
      else
      if( getGroup(%this) == $patrol2 )
      {
         order($patrol2.lead, Attack, pick(playerSquad));
      }
      else
      if( getGroup(%this) == $nexusGroup )
      {
         order(%this, Attack, pick(playerSquad));
         nexusAttack();
      }
   }
}

function cybrid::vehicle::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "cybrid::vehicle::onDestroyed(" @ %this @ ", " @ %who @ ")");
   
   $cybrid.num--;

   if( getGroup(%this) == $nexusGroup )
      $nexusGroup.num = $nexusGroup.num - 1;
   
   if($nexusGroup.num <= 0)
      objectiveCompleted(3);
}


function recluse::vehicle::onAttacked(%this, %who)
{
   if ( %this.attacked )
      return;
   
   dbecho($LEVEL, "recluse::vehicle::onAttacked(" @ %this @ ", " @ %who @ ")");
   
   %this.attacked = true;
   
//   setVehicleRadarVisible( %this, true );
}

function recluse::vehicle::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "recluse::vehicle::onDestroyed(" @ %this @ ", " @ %who @ ")");
   
   $cybrid.num--;
}

function exec::vehicle::onArrived(%this, %where)
{
   %location = getObjectName(%where);
   
   if( %location == "target1" && !($target1.destroyed) )
      order(%this, Attack, $target1);
   else if( %location == "Target2" && !($target2.destroyed) )
      order(%this, Attack, $target2);
   else if( %location == "Target3" && !($target3.destroyed) )
      order(%this, Attack, $target3);
   else if( %location == "Target4" && !($target4.destroyed) )
      order(%this, Attack, $target4);
   else if( %location == "Target5" && !($target5.destroyed) )
      order(%this, Attack, $target5);
}
   
   

function exec::vehicle::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "exec::vehicle::onDestroyed(" @ %this @ ", " @ %who @ ")");
   $cybrid.num--;
   order($caravan, Guard, $playerId);
   checkDistance($caravan, $playerId, 100, caravanAtPlayer, 1.0); 
}

function nexusAttack()
{
   if($nexusGroup.attacking == false)
   {
      dbecho($LEVEL, "nexusAttack()");
      
      $nexusGroup.attacking = true;
      schedule("order(" @ $adjudicator1 @ ", Attack,  pick(playerSquad) );", 10.0 );
      schedule("order(" @ $adjudicator2 @ ", Attack,  pick(playerSquad) );", 20.0 );
      schedule("order(" @ $adjudicator3 @ ", Attack,  pick(playerSquad) );", 30.0 );
   }
}
       

//-----------------------------------------------------------------------------
function onEnterNexus1()
{
   dbecho($LEVEL, "onEnterNexus1()");
   
   order($patrol1.lead, Attack, pick(playerSquad) );
}

//-----------------------------------------------------------------------------
function onEnterNexus2()
{
   dbecho($LEVEL, "onEnterNexus2()");
   
   setVehicleSpecialIdentity($nexus, true, *IDSTR_TEAM_RED);
   order($nexusLead,   Guard,   $nexusRoute);
   order($patrol2.lead, Attack, pick(playerSquad) );
}

//-----------------------------------------------------------------------------
function onEnterNexus3()
{
   dbecho($LEVEL, "onEnterNexus3()");
   
   objectiveCompleted(1);
   addGeneralOrder(*IDSTR_ORDER_HC2_1,   "onReportNexusPosition();");
   forceCommandPopup();
   nexusAttack();
}

function onReportNexusPosition()
{
   if( !($reportedNexus) )
   {
      dbecho($LEVEL, "onReportNexusPosition()");
   
      $reportedNexus = true;
      say(0, $playerId, *IDSTR_HC2_WU02, "HC2_WU02.wav");
      removeGeneralOrder( *IDSTR_ORDER_HC2_1 );
      objectiveCompleted(2);
   }
}

function onEnterRecluseTrap()
{
   dbecho($LEVEL, "onEnterRecluseTrap()");
   order($recluse, Cloak, true);
   order($recluse, Guard, pick(playersquad) );
}

//////////////////////////////////////////////////////////////////////////////////
// Transport Functions                                                          //
//////////////////////////////////////////////////////////////////////////////////

function initTransport()
{
   dbecho($LEVEL, "initTransport()");

   $transport        = myGetObjectId("MissionGroup\\vehicles\\Human\\Transport\\Transport");
   $transportRoute   = myGetObjectId("MissionGroup\\Patrols\\Transport");
   $end              = myGetObjectId("MissionGroup\\Patrols\\Transport\\end");      
   order($transport, Speed, High);
   
   schedule("order(" @ $transport @ ", Guard, " @ $transportRoute @ ");" , 2.0);
   schedule("order(" @ $transport @ ", Height, 500, 1000);", 13.0); 
}
function transport::vehicle::onDestroyed(){}
function transport::vehicle::onArrived(){}

///////////////////////////////////////////////////////////////////////////////
// Caravan Functions                                                         //
///////////////////////////////////////////////////////////////////////////////
function initCaravan()
{
   $caravan       = myGetObjectId("MissionGroup\\vehicles\\Human\\Caravan\\Caravan");
   $caravanRoute  = myGetObjectId("MissionGroup\\Patrols\\Caravan");

   order($caravan, Speed, Medium);
}

//-----------------------------------------------------------------------------
function caravan::vehicle::onScan(%scanned, %scanner, %string)
{
   if( !($scannedCaravan) )
   {
      $scannedCaravan = true;
      
      dbecho($LEVEL, "caravan::vehicle::onScan(" @ %scanned @ ", " @ %scanner @ ", " @ %string @ ")");

      if($survivorTalking)
         schedule("caravanTalks(" @ %scanned @ ");", 7);
      else
         caravanTalks(%scanned);

   }
}

//-----------------------------------------------------------------------------

function caravan::vehicle::onArrived(%this, %where)
{
   if(%where == $playerId)
      return;
   
   order($caravan, ShutDown, true);
}

function caravanAtPlayer()
{  
  dbecho($LEVEL, "caravanAtPlayer()");
  
  if( !($scannedCaravan) )
  {
     $scannedCaravan = true;
     
     if($survivorTalking)
        schedule("caravanTalks();", 7);
     else
        caravanTalks();
  }
}

function vehicle::onDestroyed(%this, %who)
{
   killChannel(%this);
}


//-----------------------------------------------------------------------------
function caravanTalks()
{
   dbecho($LEVEL, "caravanTalks()");
   
   $caravanTalking = true;
   say(0, $caravan, *IDSTR_HC2_NOM01, "HC2_NOM01.wav");
   schedule("order(" @ $caravan @ " , Guard, " @ $caravanRoute @ ");", 11.0);
   schedule("$caravanTalking = false;", 8);
}

///////////////////////////////////////////////////////////////////////////////
// Base Functions                                                            //
///////////////////////////////////////////////////////////////////////////////
function initBase()
{
   $base = myGetObjectId("MissionGroup\\Base");
   
   $target1 = myGetObjectId("MissionGroup\\Base\\target1");
   $target2 = myGetObjectId("MissionGroup\\Base\\Target2");
   $target3 = myGetObjectId("MissionGroup\\Base\\Target3");
   $target4 = myGetObjectId("MissionGroup\\Base\\Target4");
   $target5 = myGetObjectId("MissionGroup\\Base\\Target5");
   
   $target1.destroyed   = false;
   $target2.destroyed   = false;
   $target3.destroyed   = false;
   $target4.destroyed   = false;
   $target5.destroyed   = false;
}


function wreck::structure::onScan(%scanned, %scanner, %string)
{
   if( !($scannedStructure) )
   {
      dbecho($LEVEL, "wreck::structure::onScan(" @ %scanned @ ", " @ %scanner @ ", " @ %string @ ")");
   
      $scannedStructure = true;
      if( $caravanTalking )
         schedule("survivorTalks(" @ %scanned @ ");", 8);
      else
         survivorTalks(%scanned);
      
   }
}
//-----------------------------------------------------------------------------
function onReportSurvivors()
{
   if( !($reportedSurvivors) )
   {
      dbecho($LEVEL, "onReportSurvivors()");

      $reportedSurvivors = true;
      say(0, $playerId, *IDSTR_HC2_WU01, "HC2_WU01.wav");
      removeGeneralOrder( *IDSTR_ORDER_HC2_2 );
      objectiveCompleted(5);
      
      InventoryWeaponAdjust(		-1, 150,	2	);	//SMART Gun

   }
}

//-----------------------------------------------------------------------------
function survivorTalks(%channel)
{
   dbecho($LEVEL, "survivorTalks(" @ %channel @ ")");

   $survivorTalking = true;
   addGeneralOrder(*IDSTR_ORDER_HC2_2, "onReportSurvivors();");
   say(0, %channel, *IDSTR_HC2_SUR01, "HC2_SUR01.wav");
   schedule("forceCommandPopup();", 7.0);
   schedule("$survivorTalking = false;", 7.0);
}

//-----------------------------------------------------------------------------
function structure::onDestroyed(%this, %who)
{
   %this.destroyed = true;
   killChannel(%this);
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
        }
        if(%objective == 5)
        {
            missionObjective5.status = *IDSTR_OBJ_COMPLETED;
        }

        say(0, $playerId, "GEN_OC01.wav");
    }
    
    if( missionObjective1.status == *IDSTR_OBJ_COMPLETED && 
        missionObjective2.status == *IDSTR_OBJ_COMPLETED &&
        missionObjective3.status == *IDSTR_OBJ_COMPLETED  ) 
    {                                                       
        if($cybrid.num <= 0)
        missionObjective4.status = *IDSTR_OBJ_COMPLETED;
        schedule("missionSuccess();", 5.0);                                   
    }                                                       
}


//-----------------------------------------------------------------------------
function missionSuccess()
{
    if($missionFailed)
      return;
    
    dbecho($LEVEL, "missionSuccess()");
    
    $missionSuccess = true;
    outro(); 
    updatePlanetInventory(hc2);
    
    schedule("forceToDebrief();", 10.0);
}
//-----------------------------------------------------------------------------
function missionFailed(%reason)
{
    if($missionSuccess)
      return;
    
    dbecho($LEVEL, "missionFailed(" @ %reason @ ")");
    
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
    if(%reason == "nexusDestoryed")
    {
        %msg = "Nexus Destroyed";
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


function calcAndDrop()
{
   dbecho($LEVEL, "calcAndDrop()");  
 
   %offset  = randomInt(-350, 350);  
   
   %x    = getPosition($playerId, x);
   %y    = getPosition($playerId, y);
   %z    = getPosition($playerId, z);
   %rot  = getPosition($playerId, r);
   
   if(     %rot >=   -($PI/4)    &&      %rot <    ($PI/4)   )
   {
       %y = %y - 500;
       %x = %x + %offset;
   }
   else
   if(     %rot >=    ($PI/4)    &&      %rot <  3*($PI/4)   )
   {
       %x = %x + 500;
       %y = %y + %offset;
   }
   else
   if(     %rot >=  3*($PI/4)    ||      %rot < -3*($PI/4)   )
   {
       %y = %y + 500;
       %x = %x + %offset;
   }
   else
   if(     %rot >= -3*($PI/4)    &&      %rot <   -($PI/4)   )
   {
       %x = %x - 500;
       %y = %y + %offset;
   }
   
   setPosition($goad, %x, %y, getTerrainHeight(%x, %y) );
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
   if( !$missionStart )
      return;

   dbecho($LEVEL, "vehicle::onNewLeader(" @ %this @ ")"); 
   
   %group = getGroup(%this);
   
   if ( %group == $nexusGroup || %group == $recluse )
      return;

   %group.lead = %this;
   order(%group,        Formation,  Wedge);
   order(%group.lead,   Attack,     pick(playerSquad) );
}

