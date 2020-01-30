//////////////////////////////////////////////////////////
// CD1 Desecrate//Destroy//Demoralize                   //
//                                                      //
// Primary Goals                                        //
// 1.  Destroy all destroyable structures at Nav001     //
// 2.  Destroy all human war machines                   //
//  Secondary Goals                                     //
// 3.  Use artillery to assist                          //
// 4.  Destroy escaping vehicles                        //
//////////////////////////////////////////////////////////



MissionBriefInfo missionBriefInfo               
{                                               
	campaign            = *IDSTR_CD1_CAMPAIGN;     
	title               = *IDSTR_CD1_TITLE;        
	planet              = *IDSTR_PLANET_TEMPERATE;      
	location            = *IDSTR_CD1_LOCATION;     
	dateOnMissionEnd    = *IDSTR_CD1_DATE;         
	media               = *IDSTR_CD1_MEDIA;        
	nextMission         = *IDSTR_CD1_NEXTMISSION;  
	successDescRichText = *IDSTR_CD1_DEBRIEF_SUCC; 
	failDescRichText    = *IDSTR_CD1_DEBRIEF_FAIL; 
	shortDesc           = *IDSTR_CD1_SHORTBRIEF;   
	longDescRichText    = *IDSTR_CD1_LONGBRIEF;
    successWavFile      = "CD1_Debriefing.wav";
    soundVol = "CD1.vol";    
};                                              
                                                
MissionBriefObjective  missionObjective1         //    DESTROY BUDDA
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CD1_OBJ1_SHORT;
	longTxt             = *IDSTR_CD1_OBJ1_LONG;
    bmpname             = *IDSTR_CD1_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2         // KILL EVERYONE
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CD1_OBJ2_SHORT;
	longTxt             = *IDSTR_CD1_OBJ2_LONG;
    bmpname             = *IDSTR_CD1_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3     // DESTROY 1/2 HIGH TEMPLES
{
	isPrimary           = False;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CD1_OBJ3_SHORT;
	longTxt             = *IDSTR_CD1_OBJ3_LONG;
    bmpname             = *IDSTR_CD1_OBJ3_BMPNAME;
};
                        
MissionBriefObjective missionObjective4                    // DESTROY ESCAPING VEHICLES
{
	isPrimary           = False;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CD1_OBJ4_SHORT;
	longTxt             = *IDSTR_CD1_OBJ4_LONG;
    bmpname             = *IDSTR_CD1_OBJ4_BMPNAME;
};

DropPoint One
{
	name = "MissionGroup\\dropPoints\\dropPoint1";
	desc = "Drop Point 001";
};

DropPoint Two
{
	name = "MissionGroup\\dropPoints\\dropPoint2";
	desc = "Drop Point 002";
};

DropPoint Three
{
	name = "MissionGroup\\dropPoints\\dropPoint3";
	desc = "Drop Point 003";
};

Pilot Artillery
{
   id = 25;  // artillery
   
   name = "Artillery";
   skill = 0.8;
   accuracy = 0.8;
   aggressiveness = 0.8;
   activateDist = 400.0;
   deactivateBuff = 200.0;
   targetFreq = 0.8;
   trackFreq = 1.0;
   fireFreq = 3.5;
   LOSFreq = 2.0;
   orderFreq = 1.0;
};

// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// Script
// ------------------------------------------------------------------------------

function player::onAdd(%this)
{
    $thePlayerNum = %this;
    setDropPodParams(0.25,0.25,-0.7, 5000, 300, 2000);
}

// ------------------------------------------------------------------------------

function onMissionStart()
{
    defineActors();
    defineCounts();
    defineRoutes();
    initFormations();
    schedule( "initPatrols();", 8 );
    temperateSounds();
    stormSounds();
    cdAudioCycle(Gnash, Cloudburst, Cyberntx);
}

// ------------------------------------------------------------------------------

function defineActors()
{
    $artillery  =   getObjectId("MissionGroup/vehicles/cybrid/art1Group");
    $art1       =   getObjectId("MissionGroup/vehicles/cybrid/art1Group/art1a");
    $art2       =   getObjectId("MissionGroup/vehicles/cybrid/art1Group/art1b");
    $art3       =   getObjectId("MissionGroup/vehicles/cybrid/art2Group/art2a");
    $art4       =   getObjectId("MissionGroup/vehicles/cybrid/art2Group/art2b");
    $art5       =   getObjectId("MissionGroup/vehicles/cybrid/art3Group/art3a");
    $art6       =   getObjectId("MissionGroup/vehicles/cybrid/art3Group/art3b");
    $hs1       =   "MissionGroup/vehicles/human/hs1";
    $hs1a      =   "MissionGroup/vehicles/human/hs1/hs1a";
    $hs2       =   "MissionGroup/vehicles/human/hs2";
    $hs2a      =   "MissionGroup/vehicles/human/hs2/hs2a";
    $hs3       =   "MissionGroup/vehicles/human/hs3";
    $hs3a      =   "MissionGroup/vehicles/human/hs3/hs3a";
    $hs4       =   "MissionGroup/vehicles/human/hs4";
    $hs4a      =   "MissionGroup/vehicles/human/hs4/hs4a";
    $hs5       =   "MissionGroup/vehicles/human/hs5";
    $hs6       =   "MissionGroup/vehicles/human/hs6";
    $fly1      =   "MissionGroup/vehicles/human/flyGroup/flyer1";
    $fly2      =   "MissionGroup/vehicles/human/flyGroup/Flyer2";
    
    $truck1     =   "MissionGroup/vehicles/drone/truck1";
    $truck2     =   "MissionGroup/vehicles/drone/truck2";
    $truck3     =   "MissionGroup/vehicles/drone/truck3";
    $truck6     =   "MissionGroup/vehicles/drone/truck6";
    $nav1       =   "MissionGroup/navMarkers/nav1";
    $city       =   "MissionGroup/city/primeGroup";
    $budda      =   "MissionGroup/city/extras/budda";
}
 
// ------------------------------------------------------------------------------

function defineCounts()
{
    $navCount       =   0;
    $hs1Attacked    =   false;
    $hs2Attacked    =   false;
    $hs3Attacked    =   false;
    $hs4Attacked    =   false;
    $hsCount        =   15;
    
    $primeCount     =   83;     
    $primeRubbled   =   false;
    $truckCount     =   8;
    $artUsed        =   false;
    $droneStart      =   false;
    $talGuard       =   false;
    $flyer1Destroyed =  false;
    $launchCount    =   0;
    $flyerLaunched  =   false;
    
    $warning        =   false;
    $fail           =   false;
    $buddaKilled    =   false;
    $templeCount    =   18;
    
    $artChat        =   false;
    $humChat1       =   false;
    $humChat2       =   false; 
}

// ------------------------------------------------------------------------------

function defineRoutes()
{
    $hs1Route    =   "MissionGroup/hs1Route";
    $hs3route     =   "MissionGroup/hs3Route";
    $hs4Route     =   "MissionGroup/hs4Route";
    
    $d1Route     =   "MissionGroup/d1Route";
    $d2Route     =   "MissionGroup/d2Route";
    $d3Route     =   "MissionGroup/d3Route";
    $sovRoute   =    "MissionGroup/sovRoute";
}

// ------------------------------------------------------------------------------

function initFormations()
{
    newFormation( wall,   0,0,0, 
                          20,0,0, 
                          40,0,0 );
    newFormation( line,   0,0,0,
                          0,-30,0,
                          0,-60,0 );
    newFormation( wedge,   0,0,0,
                          -30,-20,0,
                          30,-20,0 );                                                    
}

// ------------------------------------------------------------------------------

function initPatrols()
{
    // artillery
    setHercOwner( $hs1, $artillery );
    setHercOwner( $hs2, $artillery );
    setHercOwner( $hs3, $artillery );
    setHercOwner( $hs4, $artillery );
    setHercOwner( $hs5, $artillery );
    setHercOwner( $hs6, $artillery );
    
    // hs1 group
    order( $hs1, guard, $hs1Route );
    order( $hs1, speed, medium );
    
    // hs2 Group
    order($hs2, guard, $hs1Route);
    order($hs2, speed, medium);

    // hs3 Group
    order( $hs3, guard, $hs3Route );
    order( $hs3, speed, medium );
    
    // hs4 Group
    order( $hs4, guard, $hs4Route );
    order( $hs4, speed, medium );
    
    // drones
    order( $truck1, shutdown, true );
    order( $truck2, shutdown, true );
    order( $truck3, shutdown, true );
    order( $truck6, guard, $sovRoute );
    order(getObjectId($fly1), Height, 300, 400 );
    order(getObjectId($fly2), Height, 300, 400 );
}

// ------------------------------------------------------------------------------

function vehicle::onAdd(%this)
{
    if($thePlayerNum == playerManager::vehicleIdToPlayerNum(%this))
    {
        $thePlayer = %this;
        $playerAlive = True;
    }
}

// ------------------------------------------------------------------------------

function onSPClientInit()
{
    schedule( "setNavMarker( getObjectId($nav1), true, -1 );", 1 );
    distanceChecks();
    addGeneralOrder(*IDSTR_ORDER_CD1_1, "artAttack();" );
    say( 0, 6, *IDSTR_CD1_SUB01, "CD1_SUB01.WAV" );
    schedule( "repeatGeneralOrder(PlayerManager::playerNumToVehicleId(2049), *IDSTR_ORDER_CD1_1);", 20 );
    schedule( "artTalks( *IDSTR_CD1_NEX01, \"CD1_NEX01.WAV\" );", 20 );
}
        
// ------------------------------------------------------------------------------

function distanceChecks()
{
    checkBoundary( enter, $thePlayer, getObjectId($nav1), 1000, nearNav1 );
    checkBoundary( enter, $thePlayer, getObjectId($nav1), 3000, inCity );
    checkBoundary( enter, $thePlayer, getObjectId(getLeader($hs1)), 1300, hs1Attack );
    checkBoundary( enter, $thePlayer, getObjectId(getLeader($hs2)), 1300, hs2Attack );
    checkBoundary( enter, $thePlayer, getObjectId(getLeader($hs3)), 1000, hs3Attack );
    checkBoundary( enter, $thePlayer, getObjectId(getLeader($hs4)), 1000, hs4Attack );
    
    checkBoundary( leave, $thePlayer, getObjectId($nav1), 4000, warn );
    $warn = false;
    checkBoundary( leave, $thePlayer, getObjectId($nav1), 4500, fail );
    $fail = false;
}

// ------------------------------------------------------------------------------

function nearNav1()
{
    if($navCount == 0 )
    {
        hs4Attack();
        artTalks( *IDSTR_CD1_SUB02, "CD1_SUB02.WAV" );   
        $navCount++;
    }    
}

// ------------------------------------------------------------------------------

function inCity()
{
    if( $flyerLaunch != true )
    {
        launchFlyer();
        $flyerLaunch = true;
    }
}

// ------------------------------------------------------------------------------

function hs1Attack()
{
    if( ($hs1Attacked == false) && (isGroupDestroyed($hs1) == false) )
    {
        if( $humChat2 != true )
        {
            schedule( "hs1Talks( \"Cyb_ea02.wav\" );", 15);             // Where are those reinforcements?
            schedule( "hs2Talks( \"Cyb_ea07.wav\");", 20 );             // We're on our way
            $humChat2 = true;
        }
        schedule( "artTalks(*IDSTR_CYB_NEX06, \"CYB_NEX06.WAV\");", 5 );    // on attack vector
        order( $hs1, attack, playerSquad );
        order( $hs1, speed, high );
        $hs1Attacked = true;         
    }
}

// ------------------------------------------------------------------------------

function hs2Attack()
{
    if( ($hs2Attacked == false) && (isGroupDestroyed($hs2) == false) )
    {
        if( $humChat2 != true )
        {
            schedule( "hs2Talks( \"Cyb_ea02.wav\" );", 15);            // Where are those reinforcements?
            schedule( "hs3Talks( \"Cyb_ea07.wav\");", 20 );            // We're on our way
            $humChat2 = true;
        }
        schedule( "artTalks(*IDSTR_CYB_NEX06, \"CYB_NEX06.WAV\");", 5 );    // on attack vector
        order( $hs2, attack, playerSquad );
        order( $hs2, speed, high );
        $hs2Attacked = true;      
    }
}

// ------------------------------------------------------------------------------

function hs3Attack()
{
    if( ($hs3Attacked == false) && (isGroupDestroyed($hs3) == false) )
    {
        schedule( "artTalks(*IDSTR_CYB_NEX06, \"CYB_NEX06.WAV\");", 5 );      // on attack vector
        order( $hs3, attack, playerSquad );
        order( $hs3, speed, high );
        $hs3Attacked = true;   
    }
}

// ------------------------------------------------------------------------------

function hs4Attack()
{
    if( ($hs4Attacked == false) && (isGroupDestroyed($hs4) == false) )
    {
        schedule( "artTalks(*IDSTR_CYB_NEX06, \"CYB_NEX06.WAV\");", 5 );     // on attack vector
        order( $hs4, attack, playerSquad );
        order( $hs4, speed, high );
        $hs4Attacked = true;      
    }
}

// ------------------------------------------------------------------------------

function hs5Attack()
{
    %target = pick( $art1, $art2, $art3, $art4, $art5, $art6);
    if( isGroupDestroyed($hs5) == false )
    {
        order( $hs5, attack, %target );
    }
}

// ------------------------------------------------------------------------------

function hs6Attack()
{
    %target = pick( $art1, $art2, $art3, $art4, $art5, $art6);
    if( isGroupDestroyed($hs6) == false )
    {
        order( $hs6, attack, %target );
    }
}

// ------------------------------------------------------------------------------

function hs5Attack2()
{
    if( isGroupDestroyed($hs5) == false )
    {
        order( $hs5, attack, playerSquad );
    }
}

// ------------------------------------------------------------------------------

function hs6Attack2()
{
    if( isGroupDestroyed($hs6) == false )
    {
        order( $hs6, attack, playerSquad );
    }
}

// ------------------------------------------------------------------------------

function artAttack()
{
    dataStore(PlayerManager::playerNumToVehicleId(2049), *IDSTR_ORDER_CD1_1, True);
    removeGeneralOrder(*IDSTR_ORDER_CD1_1);
    artFire($art1);
    artFire($art2);
    artFire($art3);
    artFire($art4);
    artFire($art5);
    artFire($art6);
    if( $artUsed == false )
    {
        schedule( "artTalks( *IDSTR_CYB_NEX15, \"CYB_NEX15.WAV\" );", 3 );
        $artUsed = true;
        schedule( "hs5Attack();", 30 );
        schedule( "hs6Attack();", 30 );
    }
}

// ------------------------------------------------------------------------------

function artFire(%this)
{
    if( $templeCount >= 8 )
    {
        %target1 = pick("MissionGroup/city/primeGroup1");
        %target2 = pick("MissionGroup/city/primeGroup2");
        %target3 = pick("MissionGroup/city/highTemples");
        order( %this, Attack, pick(%target1, %target2, %target3) );
    }
    else
    {
        %target1 = pick("MissionGroup/city/primeGroup1");
        %target2 = pick("MissionGroup/city/primeGroup2");
        order( %this, Attack, pick(%target1, %target2) );
    }
}

// ------------------------------------------------------------------------------

function vehicle::onMessage(%this, %message, %_1, %_2, %_3, %_4, %_5, %_6, %_7, %_8, %_9)
{
   if (%message == "ArtilleryMissed") 
   {
      artilleryClearTarget(%this);
      artFire(%this);
   }      
   else if (%message == "ArtilleryHit") 
   {
      schedule( "droneStart();", 20 );
   }
   else if (%message == "ArtilleryOutOfAmmo") 
    {
        artilleryClearTarget(%this);
        reloadObject(%this, 30 );
        artFire(%this);
    }
   else if (%message == "ArtilleryOutOfRange")
   {
      artilleryClearTarget(%this);
      if( (missionObjective3.status == *IDSTR_OBJ_COMPLETED) && ($primeCount <= 60) )
      {
        order(%this, shutdown, true);
      }
      else
      {
        artFire(%this);
      }  
   }
   else if (%message == "TargetDestroyed")
   {
        if( (%this == $art1) || (%this == $art2) || (%this == $art3) ||
            (%this == $art4) || (%this == $art5) || (%this == $art6) )
        {
            artilleryClearTarget(%this);
            artFire(%this);
        }
   }
}

// ------------------------------------------------------------------------------

function artilleryClearTarget(%this)
{
   order(%this, clear, true);
}

// ------------------------------------------------------------------------------

function truck::vehicle::onDestroyed(%destroyed, %destroyer)
{
    $truckCount--;
    if( $truckCount == 3 )
    {
        missionObjective4.status = *IDSTR_OBJ_COMPLETED;
        artTalks(*IDSTR_CYB_NEX17, "CYB_NEX17.WAV" );
    }        
}

// ------------------------------------------------------------------------------

function hs::vehicle::onDestroyed(%destroyed, %destroyer)
{
    $hsCount--;
    youWin();
    if(getGroup(%destroyed) == getObjectId($hs2))
    {
        hs3Attack();
        order( $hs1, guard, $hs3Route );
        order( $hs1, speed, high );
    }
    if(getGroup(%destroyed) == getObjectId($hs1))
    {
        hs3Attack();
        order( $hs2, guard, $hs3Route );
        order( $hs2, speed, high );
    }
    if(getGroup(%destroyed) == getObjectId($hs3))
    {
        order( $hs1, guard, $hs3Route );
        order( $hs1, speed, high );
        order( $hs2, guard, $hs3Route );
        order( $hs2, speed, high );
    }
    if(getGroup(%destroyed) == getObjectId($hs4))
    {
        hs3Attack();
        order( $hs1, guard, $hs4Route );
        order( $hs1, speed, high );
        order( $hs2, guard, $hs4Route );
        order( $hs2, speed, high );
    }    
    if( $hsCount <= 6 )
    {
        hs5Attack2();
        hs6Attack2();
    }
    if( ($hsCount <= 0) )
    {
        missionObjective2.status = *IDSTR_OBJ_COMPLETED;
        artTalks(*IDSTR_CYB_NEX17, "CYB_NEX17.WAV" );
        youWin();
    }
}

// ------------------------------------------------------------------------------

function prime::structure::onDestroyed(%destroyed, %this)
{
    $primeCount--;
    schedule( "droneStart();", 10 );
    if( $primeCount == 75)
    {
        launchFlyer();
    }
    if( $primeCount == 65)
    {
        launchFlyer();
    }
}

// ------------------------------------------------------------------------------

function temple::structure::onDestroyed(%destroyed, %destroyer)
{
    $templeCount--;
    if( %destroyed == getObjectId($budda) )
    {
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        artTalks(*IDSTR_CYB_NEX17, "CYB_NEX17.wav" );
        youWin();
    }
    if( $templeCount == 17 )
        say( 0, 6, "CD1_CIV01.wav" );
    if( ($templeCount <= 7 )&& ($primeRubbled != true) )
    {
        missionObjective3.status = *IDSTR_OBJ_COMPLETED;
        artTalks(*IDSTR_CYB_NEX17, "CYB_NEX17.wav" );
        $primeRubbled = true;
        youWin();
    }
}

// ------------------------------------------------------------------------------

function droneStart()
{
    if( $droneStart != true )
    {
        schedule( "truckTalks( \"CYB_EA11.wav\" );", 3 );          // Get civilians out
        schedule( "artTalks(*IDSTR_CD1_NEX02, \"CD1_NEX02.WAV\");", 10 );       // Try to kill escaping trucks
        order( $truck1, shutdown, false );
        order( $truck2, shutdown, false );
        order( $truck3, shutdown, false );
        order("MissionGroup/vehicles/drone/truck4", guard, "MissionGroup/d4Route");
        order("MissionGroup/vehicles/drone/truck5", guard, "MissionGroup/d5Route");
        order( $truck1, guard, $d1Route);
        order( $truck2, guard, $d2Route);
        order( $truck3, guard, $d3Route);
        $droneStart = true;
    }    
}

// ------------------------------------------------------------------------------

function launchFlyer()
{
    if( $launchCount == 0 )
    {
        flyGuard($fly1);
        $launchCount = 1;
    }
    else if( $launchCount == 1 )
    {
        flyGuard($fly2);
        $launchCount = 2;
    }
}

// ------------------------------------------------------------------------------

function flyGuard(%this)
{
    if( %this == getObjectId($fly1) )
    {
        order( %this, guard, "MissionGroup/flyRoute/marker1" );
        order( %this, speed, low );
    }
    else
    {
        order( %this, guard, "MissionGroup/flyRoute/marker1" );
        order( %this, speed, low );
    }
}

// ------------------------------------------------------------------------------

function warn()
{
    if( $warnTalk == 0 )
    {
        artTalks( *IDSTR_CYB_NEX01, "CYB_NEX01.wav" );  // Off course
        $warnTalk++;
    }
}

// ------------------------------------------------------------------------------

function fail()
{
    if( $failTalk == 0 )
    {
        artTalks( *IDSTR_CYB_NEX02, "CYB_NEX02.wav" );    // mission failed
        schedule( "forceToDebrief(*IDSTR_MISSION_FAILED);", 3 );
        $failTalk++;
    }    
}

// ------------------------------------------------------------------------------

function youWin()
{
    if( (missionObjective1.status == *IDSTR_OBJ_COMPLETED) &&
        (isSafe(*IDSTR_TEAM_YELLOW, getObjectId($nav1), 2500)) )
    {
        missionObjective2.status = *IDSTR_OBJ_COMPLETED;
        schedule( "artTalks(*IDSTR_CYB_NEX04, \"CYB_NEX04.WAV\");", 2 );    // program complete
        updatePlanetInventory(cd1);
        schedule( "fadeEvent( 0, out, 2, 0, 0, 0 );", 5 );
        schedule( "spline();", 7 );
        schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 25.0);
        clearGeneralOrders();
    }    
}

// ------------------------------------------------------------------------------

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    missionObjective4.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(cd1);
    schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 3.0);    
}

// ------------------------------------------------------------------------------

function nexTalks(%text, %wave)
{
    if( $playerAlive  )
        say( 0, 8, %text, %wav );
    else
    {
        say( 0, 8, "" );
    }        
}

// ------------------------------------------------------------------------------

function artTalks(%text, %wav)
{
    if( $playerAlive  )
        say( 0, 2, %text, %wav );
    else
    {
        say( 0, 2, "" );
    }
}

// ------------------------------------------------------------------------------

function hs1Talks(%wav)
{
    if( ($playerAlive) && (isGroupDestroyed($hs1) == false)  )
        say( 0, 3, %wav );
    else
    {
        say( 0, 3, "" );
    }
}

// ------------------------------------------------------------------------------

function hs2Talks(%wav)
{
    if( ($playerAlive) && (isGroupDestroyed($hs2) == false)  )
        say( 0, 4, %wav );
    else
    {
        say( 0, 4, "" );
    }
}

// ------------------------------------------------------------------------------

function hs3Talks(%wav)
{
    if( ($playerAlive) && (isGroupDestroyed($hs3) == false)  )
        say( 0, 5, %wav );
    else
    {
        say( 0, 5, "" );
    }
}

// ------------------------------------------------------------------------------

function hs4Talks(%wav)
{
    if( ($playerAlive) && (isGroupDestroyed($hs4) == false)  )
        say( 0, 6, %wav );
    else
    {
        say( 0, 6, "" );
    }
}

// ------------------------------------------------------------------------------

function truckTalks(%wav)
{
    if( $playerAlive  )
        say( 0, 7,  %wav );
    else
    {
        say( 0, 7, "" );
    }
}

// ------------------------------------------------------------------------------

function spline()
{
    
    setWidescreen(true);
	focusCamera( splineCamera, path1 );
    fadeEvent( 0, in, 1.0, 0, 0, 0 );
}

// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// --------------------------------END-------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------

