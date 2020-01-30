//////////////////////////////////////////////////////////
// CA0 SECURE/INITIATE                                  //
//                                                      //
// Primary Goals                                        //
// 1.  Goto Nav002                                      //
// 2.  Goto Nav003                                      //
// 3.  Goto Nav004                                      //
// 4.  Destroy all humans detected                      //
// 5.  Return to Nav001 and secure for Nexus Drop Ship  //
//////////////////////////////////////////////////////////



MissionBriefInfo missionBriefInfo               
{                                               
	campaign            = *IDSTR_CA0_CAMPAIGN;     
	title               = *IDSTR_CA0_TITLE;        
	planet              = *IDSTR_PLANET_MERCURY;      
	location            = *IDSTR_CA0_LOCATION;     
	dateOnMissionEnd    = *IDSTR_CA0_DATE;         
	media               = *IDSTR_CA0_MEDIA;        
	nextMission         = *IDSTR_CA0_NEXTMISSION;  
	successDescRichText = *IDSTR_CA0_DEBRIEF_SUCC; 
	failDescRichText    = *IDSTR_CA0_DEBRIEF_FAIL; 
	shortDesc           = *IDSTR_CA0_SHORTBRIEF;   
	longDescRichText    = *IDSTR_CA0_LONGBRIEF;
    successWavFile      = "CA0_Debriefing.wav";
    soundVol = "CA0.vol"; 
};                                              
                                                
MissionBriefObjective  missionObjective1         //    Goto Nav001
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CA0_OBJ1_SHORT;
	longTxt             = *IDSTR_CA0_OBJ1_LONG;
    bmpname             = *IDSTR_CA0_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2         // Goto Nav002
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CA0_OBJ2_SHORT;
	longTxt             = *IDSTR_CA0_OBJ2_LONG;
    bmpname             = *IDSTR_CA0_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3     // Goto Nav003
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CA0_OBJ3_SHORT;
	longTxt             = *IDSTR_CA0_OBJ3_LONG;
    bmpname             = *IDSTR_CA0_OBJ3_BMPNAME;
};
                        
MissionBriefObjective missionObjective4                    // Destroy all humans detected
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CA0_OBJ4_SHORT;
	longTxt             = *IDSTR_CA0_OBJ4_LONG;
    bmpname             = *IDSTR_CA0_OBJ4_BMPNAME;
};

MissionBriefObjective missionObjective5                    // Return to Nav004 and secure for Nexus Drop Ship
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CA0_OBJ5_SHORT;
	longTxt             = *IDSTR_CA0_OBJ5_LONG;
    bmpname             = *IDSTR_CA0_OBJ5_BMPNAME;
};

DropPoint dropPoint1
{
    Name = "Bill";
    Desc = "high on a hill";
};

$server::HudMapViewOffsetX = 500; 
$server::HudMapViewOffsetY = 4000; 

// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// Script
// ------------------------------------------------------------------------------

function player::onAdd(%this)
{
    $thePlayerNum = %this;
    $playerAlive = True;
    //setDropPodParams(dir.x,dir.y,dir.z,[dropHeight],[dropSpeed],[dropNearDist]);
    setDropPodParams(0.25,0.25,-0.7, 5000, 500, 2000);
}

// ------------------------------------------------------------------------------

function onMissionStart()
{
    defineActors();
    defineNavMarkers();
    defineCounts();
    defineRoutes();
    initFormations();
    droppings();
    cameraLockFocus(true);
    forceScope("MissionGroup/extras/rock1", 99999);
    forceScope("MissionGroup/podStuff/rocks/rock1", 9999);
    forceScope("MissionGroup/podStuff/rocks/rock2", 9999);
    forceScope("MissionGroup/podStuff/rocks/rock3", 9999);
    forceScope("MissionGroup/podStuff/rocks/rock4", 9999);
    forceScope("MissionGroup/podStuff/rocks/rock5", 9999);
    forceScope("MissionGroup/podStuff/rocks/rock6", 9999);
    forceScope("MissionGroup/podStuff/rocks/rock7", 9999);
    forceScope("MissionGroup/podStuff/rocks/rock8", 9999);
    forceScope("MissionGroup/podStuff/rocks/rock9", 9999);
    forceScope("MissionGroup/podStuff/rocks/rock10", 9999);
    forceScope("MissionGroup/dropShips/fly1", 9999);
    forceScope("MissionGroup/dropShips/fly2", 9999);
    forceScope("MissionGroup/dropShips/fly3", 9999);
    forceScope("MissionGroup/podStuff/spot1", 9999);
    forceScope($c1, 9999);
    schedule( "initPatrols();", 8 );
    fadeEvent( 0, out, 0.1, 0, 0, 0 );
    cdAudioCycle(Newtech, Purge, Mechsoul);
    $muteComputer = true;
}

// ------------------------------------------------------------------------------

function defineActors()
{
    $pod1       =   "MissionGroup/extras/pod1";
    $pod2       =   "MissionGroup/extras/pod2";
    $drop       =   "MissionGroup/vehicles/cybrid/drop";
    
    $a          =   "MissionGroup/vehicles/human/aGroup";
    $a1         =   "MissionGroup/vehicles/human/aGroup/a1";    
    $b          =   "MissionGroup/vehicles/human/bGroup";
    $b1         =   "MissionGroup/vehicles/human/bGroup/b1";
    $c1         =   "MissionGroup/vehicles/human/cGroup/c1";
    $truckDead  =   false;
    
    $rock1      =   "MissionGroup/podStuff/rocks/rock1";
    $rock2      =   "MissionGroup/podStuff/rocks/rock2";
    $rock3      =   "MissionGroup/podStuff/rocks/rock3";
    $rock4      =   "MissionGroup/podStuff/rocks/rock4";
    $rock5      =   "MissionGroup/podStuff/rocks/rock5";
    $rock6      =   "MissionGroup/podStuff/rocks/rock6";
    $rock7      =   "MissionGroup/podStuff/rocks/rock7";
    $rock8      =   "MissionGroup/podStuff/rocks/rock8";
    $rock9      =   "MissionGroup/podStuff/rocks/rock9";
    $rock10     =   "MissionGroup/podStuff/rocks/rock10";
}
 
// ------------------------------------------------------------------------------

function defineNavMarkers()
{
    $nav1   =   "MissionGroup\\navMarkers\\nav001";
    $nav2   =   "MissionGroup\\navMarkers\\nav002";
    $nav3   =   "MissionGroup\\navMarkers\\nav003";
    $nav4   =   "MissionGroup\\navMarkers\\nav004";
}

// ------------------------------------------------------------------------------

function defineCounts()
{
    $navCount   =   0;
    $aAttacked  =   false;
    $aCount     =   1;
    $bAttacked  =   false;
    $bCount     =   2;
    $cCount     =   2;
    $conCount   =   2;
    $cDestroyed =   false;    
    $warning    =   false;
    $fail       =   false;
    $nav1Reached    =   false;
    $nav2Reached    =   false;
    $nav3Reached    =   false;
    $nav4Reached    =   false;
    $gameStart  =   true;
    $dropReady  =   false;
    $shipLaunch =   false;
    $dishCount  =   5;
    $callDrop   =   false;
    $conTalk    =   false;
    $final      =   false;
    $defAttacked    =   false;
}

// ------------------------------------------------------------------------------

function defineRoutes()
{
    $dropRoute   =   "MissionGroup/dropRoute";
    $aRoute      =   "MissionGroup/aRoute";
    $bRoute      =   "MissionGroup/bRoute";
}

// ------------------------------------------------------------------------------

function initFormations()
{
    newFormation( wall,   0,0,0, 
                          20,0,0, 
                          40,0,0 );
}

// ------------------------------------------------------------------------------

function initPatrols()
{
    // aGroup
    order( $a, guard, $aRoute );
    order( $a, speed, high );
    
    // bGroup
    order( $b1, makeLeader, true );
    order( $b, formation, wall );
    order( $b, guard, $bRoute );
    order( $b, speed, high );
    
    setVehicleRadarVisible(getObjectId($a1), false);
}

// ------------------------------------------------------------------------------

function vehicle::onAdd(%this)
{
    if($thePlayerNum == playerManager::vehicleIdToPlayerNum(%this))
    {
        $thePlayer = %this;
    }
}

// ------------------------------------------------------------------------------

function onSPClientInit()
{
    lockUserInput(true);
    schedule( "hum2Talks( \"CA0_STP01.wav\" );", 5 );          //What's with meteor shower
    droppings();
    dropWatch();

    // point the camera at the space ships
    // fade in will automatically occur 1.5 seconds after onSPCLientInit() is called
    setTowerCamera(getObjectId("MissionGroup/podStuff/spot1"), -5610,-5242,2588);

    // fade back out in nine seconds, go to pod watch in ten seconds
    schedule( "fadeEvent( 0, out, 2.0, 0, 0, 0 );", 9.0 );
    schedule( "podWatch();", 11.5);
}
        
// ------------------------------------------------------------------------------

function dropWatch()
{
    schedule( "setWidescreen(true);", 0.5 );
    order("MissionGroup/dropShips/fly1", guard, "MissionGroup/drop2Route");
    order("MissionGroup/dropShips/fly1", speed, high);
    order("MissionGroup/dropShips/fly2", guard, "MissionGroup/drop4Route");
    order("MissionGroup/dropShips/fly2", speed, high);
    order("MissionGroup/dropShips/fly3", guard, "MissionGroup/drop3Route");
    order("MissionGroup/dropShips/fly3", speed, high);
}
                                 
// ------------------------------------------------------------------------------

function droppings()
{
    if( $gameStart == true )
    {
        %num = randomInt(1,10);
        if( %num == 1 )
            schedule("dropPod(getObjectId($rock1));", randomInt(1,10));    
        if( %num == 2 )
            schedule("dropPod(getObjectId($rock2));", randomInt(1,10));
        if( %num == 3 )
            schedule("dropPod(getObjectId($rock3));", randomInt(1,10));
        if( %num == 4 )
            schedule("dropPod(getObjectId($rock4));", randomInt(1,10));
        if( %num == 5 )
            schedule("dropPod(getObjectId($rock5));", randomInt(1,10));
        if( %num == 6 )
            schedule("dropPod(getObjectId($rock6));", randomInt(1,10));
        if( %num == 7 )
            schedule("dropPod(getObjectId($rock7));", randomInt(1,10));
        if( %num == 8 )
            schedule("dropPod(getObjectId($rock8));", randomInt(1,10));
        if( %num == 9 )
            schedule("dropPod(getObjectId($rock9));", randomInt(1,10));
        if( %num == 10 )
            schedule("dropPod(getObjectId($rock10));", randomInt(1,10));
        schedule("droppings();", 1);    
    }
}

// ------------------------------------------------------------------------------

function backToCockpit()
{
    // we're currently faded out, setup the player's vehicle & cockpit, then
    setPosition( playerSquad, 220,3640,80 );
    setPlayerCamera();
    setWidescreen(false);
    cameraLockFocus(false);

    // ok, ready to let the player see the light
    schedule("lockUserInput(false);", 1.5);
    schedule("fadeEvent( 0, in, 3.0, 0, 0, 0 );", 1.0);
}

// ------------------------------------------------------------------------------

function podWatch()
{
    if($gameStart != false)
    {
        schedule( "hum2Talks( \"CA0_STP02.wav\" );", 3 );         // Orbital Control...damn solor flairs
        schedule( "hum2Talks(\"GEN_ICCA03.wav\");", 15 );
        setNavMarker( getObjectId($nav1), true, -1 );
        schedule( "invasionTalk1();", randomInt(35,65) );
        $gameStart = false;
    }
    droppings();
    order($c1, holdfire, true );

    // we're currently faded out, switch camera positions and fade in
    setTowerCamera(getObjectId("MissionGroup/extras/rock1"), -31,4482,202 );
    schedule("fadeEvent(0, in, 3.0, 0, 0, 0);", 1.0);

    // fade back out in eight seconds, then jump back to the cockpit
    schedule( "fadeEvent( 0, out, 2.0, 0, 0, 0 );", 10.0 );
    schedule( "backToCockpit();", 12.5);

    schedule( "dropPod(-677,2999,521, 137,3610,71, getObjectId($pod1));", 2 );
    schedule( "order($c1, attack, $thePlayer);", 6 );
    schedule( "order($c1, holdfire, false);", 13 );
    schedule( "navChecks();", 14.0 );
}

// ------------------------------------------------------------------------------

function navChecks()
{
    checkBoundary( leave, $thePlayer, getObjectId($nav4), 3000, warn );
    checkBoundary( leave, $thePlayer, getObjectId($nav4), 3500, fail );
    if( $nav2Reached == false )
        checkBoundary( enter, $thePlayer, getObjectId($nav2), 1000, nearNav2 );
        checkBoundary( enter, $thePlayer, getObjectId($nav2), 400, atNav2 );
    tankWatch();
}

// ------------------------------------------------------------------------------

function atNav4()
{
    schedule( "setNavMarker( getObjectId($nav4), false );", 2 );
    if( $navCount == 0 )
        schedule( "setNavMarker( getObjectId($nav1), true, -1 );", 2 );
}

// ------------------------------------------------------------------------------

function atNav1()
{
    if( $nav1Reached == false )
    {
        if( isSafe(*IDSTR_TEAM_YELLOW, getObjectId($nav1), 1000) )
        {
            $nav1Reached = true;
            setNavMarker( getObjectId($nav1), false );
            missionObjective1.status = *IDSTR_OBJ_COMPLETED;
            nexTalks(*IDSTR_CYB_NEX17, "CYB_NEX17.wav" );
            schedule( "setNavMarker( getObjectId($nav2), true, -1 );", 1 );
            $navCount++;
        }    
    }
    if( isSafe(*IDSTR_TEAM_YELLOW, getObjectId($nav1), 1000) == false )
        schedule( "atNav1();", 2 );
    if( $navCount >= 3 )
    {
        setNavMarker( getObjectId($nav4), true, -1 );
        dropShip();
        schedule( "invasionTalk4();", randomInt(3,6) );
        $navCount++;   
    }
}

// ------------------------------------------------------------------------------

function dish::structure::onDestroyed(%destroyed,%destroyer)
{
    $dishCount--;
    order("MissionGroup/vehicles/human/nav1Stuff/Tank1", attack, $thePlayer);
    if( ($dishCount <= 3) && ($nav1Reached == false) )
        atNav1();
}

// ------------------------------------------------------------------------------

function tankWatch()
{
    if( isSafe(*IDSTR_TEAM_RED, getObjectId("MissionGroup/vehicles/human/nav1Stuff/Tank1"), 800) )
        schedule( "tankWatch();", 3 );
    if( isSafe(*IDSTR_TEAM_RED, getObjectId("MissionGroup/vehicles/human/nav1Stuff/Tank1"), 800) == false )    
        order("MissionGroup/vehicles/human/nav1Stuff/Tank1", attack, $thePlayer);
}

// ------------------------------------------------------------------------------

function nearNav2()
{
    if( $aAttacked != true )
    {
        order("MissionGroup/vehicles/human/nav2Stuff/Drone1", guard, "MissionGroup/vehicles/human/nav2Stuff/flyer1");
        order("MissionGroup/vehicles/human/nav2Stuff/Drone2", guard, "MissionGroup/vehicles/human/nav2Stuff/flyer1");
        $aAttacked = true;
        setPosition(getObjectId($a), 2023,5785,143);
    }    
}

// ------------------------------------------------------------------------------

function atNav2()
{
    if( $nav2Reached == false )
    {
        if( isSafe(*IDSTR_TEAM_YELLOW, getObjectId($nav2), 1000) )
        {
            $nav2Reached = true;
            missionObjective2.status = *IDSTR_OBJ_COMPLETED;
            setNavMarker( getObjectId($nav2), false );
            schedule( "setNavMarker( getObjectId($nav3), true, -1 );", 1 );
            nexTalks(*IDSTR_CYB_NEX17, "CYB_NEX17.wav" );
            $navCount++;
        }
    }
    if( $navCount >= 3 )
    {
        setNavMarker( getObjectId($nav4), true, -1 );
        dropShip();
        schedule( "invasionTalk4();", randomInt(3,6) );
        $navCount++;   
    }    
}

// ------------------------------------------------------------------------------

function trans::vehicle::onAttacked(%attd, %attr)
{
    if( (%attd == getObjectId("MissionGroup/vehicles/human/nav2Stuff/flyer1")) && 
        ($shipLaunch != true) )
    {
        schedule( "humTalks(  \"GEN_1DSA01.WAV\" );", 2 );
        schedule( "order(\"MissionGroup/vehicles/human/nav2Stuff/flyer1\", guard, \"MissionGroup/bonus/marker1\");", 3);
        $shipLaunch = true;
        schedule( "aAttack();", 1 );
        schedule( "setPosition(getObjectId($a), 2023,5785,143);", 2 );
    }
    if( ((%attd == getObjectId("MissionGroup/vehicles/human/nav2Stuff/Drone1")) ||
        (%attd == getObjectId("MissionGroup/vehicles/human/nav2Stuff/Drone2"))) &&
        ($conTalk != true) )
    {
        schedule( "conTalks( \"CYB_CVC02.wav\" );", 5 );
        $conTalk = true;
    }
}

// ------------------------------------------------------------------------------

function trans::vehicle::onDestroyed(%destroyed, %destroyer)
{
    if(%destroyed == getObjectId("MissionGroup/vehicles/human/nav2Stuff/flyer1"))
    {
        order("MissionGroup/vehicles/human/nav2Stuff/Drone1", guard, "MissionGroup/bonus/marker1");
        order("MissionGroup/vehicles/human/nav2Stuff/Drone2", guard, "MissionGroup/bonus/marker1");
        killChannel(3);
        if( $shipLaunch == true )
            schedule( "conTalks( \"CYB_CVC04.wav\" );", 2 );
    }
    if( (%destroyed == getObjectId("MissionGroup/vehicles/human/nav2Stuff/Drone1")) ||
        (%destroyed == getObjectId("MissionGroup/vehicles/human/nav2Stuff/Drone2")) )
    {
        $conCount--;
        if( $conCount == 0 )
            killChannel(6);
    }
}

// ------------------------------------------------------------------------------

function defence::structure::onDestroyed(%destroyed, %destroyer)
{
    atNav3();
    bAttack();
}

// ------------------------------------------------------------------------------

function defence::structure::onAttacked(%destroyed, %destroyer)
{
    if( $defAttacked != true )
    {        
        schedule( "bAttack();", 5 );
        $defAttacked = true;
    }        
}

// ------------------------------------------------------------------------------

function atNav3()
{
    if( $nav3Reached == false )
    {
        if( isSafe(*IDSTR_TEAM_YELLOW, getObjectId($nav3), 1000) )
        {
            $nav3Reached = true;
            missionObjective3.status = *IDSTR_OBJ_COMPLETED;
            setNavMarker( getObjectId($nav3), false );
            nexTalks(*IDSTR_CYB_NEX17, "CYB_NEX17.wav" );
            $navCount++;
        }
    }
    if( isSafe(*IDSTR_TEAM_YELLOW, getObjectId($nav3), 1000) == false )
        schedule( "atNav3();", 2 );        
    if( $navCount >= 3 )
    {
        setNavMarker( getObjectId($nav4), true, -1 );
        dropShip();
        schedule( "invasionTalk4();", randomInt(3,6) );
        $navCount++;   
    }    
}

// ------------------------------------------------------------------------------

function aAttack()
{
    order( $a, attack, playerSquad );
    order( $a, cloak, true );
    order( $a, holdfire, true );
    schedule( "order($a, holdfire, false);", 15 );
    $aAttack = true;
    schedule( "setVehicleRadarVisible(getObjectId($a1), true);", 10 );
}

// ------------------------------------------------------------------------------

function bAttack()
{
    order( $b, formation, wall );
    order( $b, attack, playerSquad );
}

// ------------------------------------------------------------------------------

function a::vehicle::onDestroyed(%destroyed, %destroyer)
{
    $aCount--;
    if( $aCount == 0 )
    {
        $aDestroyed = true;
        allDead();
    }        
}

// ------------------------------------------------------------------------------

function b::vehicle::onDestroyed(%destroyed, %destroyer)
{
    $bCount--;
    if( $bCount == 0 )
    {
        $bDestroyed = true;              
        allDead();
    }        
}

// ------------------------------------------------------------------------------

function c::vehicle::onDestroyed(%destroyed, %destroyer)
{
    $cCount--;
    if( ($cCount == 0) && ($cDestroyed != true) )
    {
        $cDestroyed = true;
        allDead();
    }        
}

// ------------------------------------------------------------------------------

function allDead()
{
    if( ($aDestroyed == true) && ($bDestroyed == true) && ($cDestroyed == true) )
    {
        missionObjective4.status = *IDSTR_OBJ_COMPLETED;
        $dropReady = true;
        if( $navCount >= 3 )
        {
            dropShip();
        }
    }                                                                  
}

// ------------------------------------------------------------------------------

function dropShip()
{
    if( $callDrop != true )
    {
        addGeneralOrder(*IDSTR_ORDER_CA0_1, "safeCheck();" );
        schedule( "repeatGeneralOrder(PlayerManager::playerNumToVehicleId(2049), *IDSTR_ORDER_CA0_1);", 4);
        $callDrop = true;
    }
}

// ------------------------------------------------------------------------------

function safeCheck()
{
    if( isSafe(*IDSTR_TEAM_YELLOW, getObjectId($nav4), 3000) )
    {
        $dropReady = true;
        missionObjective4.status = *IDSTR_OBJ_COMPLETED;
        setPosition( $drop, -580,2916,500 );
        shipLand();
    }
    else
    {
        $dropReady = false;
        shipLand();
    }    
}

// ------------------------------------------------------------------------------

function shipLand()
{
    if($dropReady == true )
    {
        removeGeneralOrder(*IDSTR_ORDER_CA0_1);
        dataStore(PlayerManager::playerNumToVehicleId(2049), *IDSTR_ORDER_CA0_1, True);
        
        order($drop, guard, $dropRoute );
        nexTalks( *IDSTR_CA0_NEX01, "CA0_NEX01.wav" );     // initiat launch protocols
        missionObjective5.status = *IDSTR_OBJ_COMPLETED;
        schedule( "youWin();", 5 );
    }
    else     
    {
        nexTalks( *IDSTR_CA0_NEX02, "CA0_NEX02.wav");      // landing zone NOT secure
    }
}

// ------------------------------------------------------------------------------

function youWin()
{
    clearGeneralOrders();
    if( (missionObjective1.status == *IDSTR_OBJ_COMPLETED) &&
        (missionObjective2.status == *IDSTR_OBJ_COMPLETED) &&
        (missionObjective3.status == *IDSTR_OBJ_COMPLETED) &&
        (missionObjective4.status == *IDSTR_OBJ_COMPLETED) &&
        (missionObjective5.status == *IDSTR_OBJ_COMPLETED) ) 
    {
        nexTalks(*IDSTR_CYB_NEX04, "CYB_NEX04.wav" );
        setDominantCamera( getObjectId($drop), $thePlayer, -200,-200,15);
        updatePlanetInventory(ca0);
        schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 6.0);
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
    missionObjective5.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(ca0);
    schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 3.0);   
}

// ------------------------------------------------------------------------------

function invasionTalk1()
{
    hum2Talks( "CA0_WST01.wav" );                              // Movement at impact sites
    schedule( "hum3Talks( \"CYB_ME02.wav\" ); ", 3 );         //movement hotside
    schedule( "hum2Talks( \"CA0_STP03.wav\" );", 5 );         // Copy West
    schedule( "hum2Talks( \"CA0_BLU01.wav\" );", 10 );         // Unidentified signals
    schedule( "hum2Talks( \"CA0_STP04.wav\" );", 13 );         // Confirm, Blue
    schedule( "hum2Talks( \"CA0_WST02.wav\" );", 25 );         // Got movement, investigating
    schedule( "hum2Talks( \"CA0_BLU02.wav\" );", 45 );         // They're hostile
    schedule( "hum2Talks( \"CA0_STP05.wav\" );", 48 );         // Rebels?
    schedule( "invasionTalk2();", 50 );
}

// ------------------------------------------------------------------------------

function invasionTalk2()
{                                                                 
    hum2Talks( "CA0_BLU03.wav" );                              // Not rebs, outnumbered
    schedule( "hum2Talks( \"CA0_WST03.wav\" );", 5 );         // West here, under fire
    schedule( "hum2Talks( \"CA0_BLU04.wav\" );", 10 );         // Getting a better look...
    schedule( "hum2Talks( \"CA0_STP06.wav\" );", 13 );         // All units scramble
    schedule( "hum2Talks( \"CA0_WST04.wav\" );", 15 );         // Green Patrol offline
    schedule( "hum2Talks( \"CA0_BLU05.wav\" );", 20 );         // Christ and Hunter
    schedule( "hum2Talks( \"CA0_STP07.wav\" );", 23 );         // talk to me, blue
    schedule( "hum2Talks( \"CA0_BLU06.wav\" );", 25 );         // Cybrids!
    schedule( "hum2Talks( \"CA0_BLU07.wav\" );", 26 );         // death-cry
    schedule( "hum3Talks( \"CYB_GN53.wav\" );", 30 );         // sectors going out
    schedule( "hum3Talks( \"CYB_ME10.wav\" );", 33 );         // tac-com, help, please
    schedule( "invasionTalk3();", 35 );         
}
                            
// ------------------------------------------------------------------------------

function invasionTalk3()
{
    hum2Talks( "CA0_WST06.wav" );                                 // perimeter breach
    schedule( "hum3Talks( \"CYB_GN47.wav\" );", 5 );         // sectors going out
    schedule( "hum2Talks( \"CA0_WST05.wav\" );", 10 );         // Where's the navy
    schedule( "hum3Talks( \"CYB_GN55.wav\" );", 12 );         // sectors going out
    schedule( "hum2Talks( \"CA0_WST07.wav\" );", 15 );         // death-cry
    schedule( "hum3Talks( \"CYB_GN50.wav\" );", 18 );         // sectors going out
    schedule( "hum2Talks( \"CA0_STP08.wav\" );", 21 );         // blue? transfer commlinks
    schedule( "hum3Talks( \"CYB_GN54.wav\" );", 25 );            // sectors going out
    schedule( "hum3Talks( \"CYB_ME19.wav\" );", 30 );         // They're on the perimeter
    schedule( "hum3Talks( \"CYB_ME13.wav\" );", 35 );         // glitches everywhere
    schedule( "hum3Talks( \"CYB_GN52.wav\" );", 50 );         // sectors going out
    schedule( "hum2Talks( \"CYB_GN48.wav\" );", 70 );            // sectors going out
    schedule( "hum2Talks(\"CYB_ME22.wav\" );", 80 );         // glitches zapped coolent reserve
    schedule( "hum2Talks( \"CYB_GN46.wav\" );", 95 );         // sectors going out
    schedule( "hum2Talks(\"CYB_GN49.wav\" );", 120 );            // sectors going out
    schedule( "hum2Talks( \"CYB_GN51.wav\" );", 150 );           // sectors going out
    schedule( "hum2Talks( \"CYB_GN57.wav\" );", 165 );         // emergency crews responding
    schedule( "hum2Talks( \"CYB_GN56.wav\" );", 190 );           // sectors going out    
}

// ------------------------------------------------------------------------------

function invasionTalk4()
{
    if( $final != true )
    {
        hum2Talks( "CYB_ME04.wav" );                           // anyone still alive
        schedule( "hum2Talks( \"CYB_GN59.wav\" );", 10 );      // nobody left, save yourselves
        $final = true;
    }
}

// ------------------------------------------------------------------------------

function nexTalks(%text, %wav)
{
    if( $playerAlive ) 
        say( 0, 2, %text, %wav );
    else 
    {
        say(0, 2, "","");
    }
}

// ------------------------------------------------------------------------------

function humTalks(%wav)
{
    if( $playerAlive ) 
        say( 0, 3, %wav );
    else 
    {
        say(0, 3, "","");
    }
}
// ------------------------------------------------------------------------------

function hum2Talks( %wav)
{
    if( $playerAlive ) 
        say( 0, 4,  %wav );
    else 
    {
        say(0, 4, "");
    }
}

// ------------------------------------------------------------------------------

function hum3Talks( %wav)
{
    if( $playerAlive ) 
        say( 0, 5, %wav );
    else 
    {
        say(0, 5, "");
    }
}

// ------------------------------------------------------------------------------

function conTalks(%wav)
{
    if( $playerAlive ) 
        say( 0, 6, %wav );
    else 
    {
        say(0, 6, "");
    }
}

// ------------------------------------------------------------------------------

function warn()
{
    if( $warning == false)
    {
        nexTalks( *IDSTR_CYB_NEX01, "CYB_NEX01.WAV" );  // Off course
        $warning = true;
    }
    else 
    {
        secondChance();
    }    
}

// ------------------------------------------------------------------------------

function secondChance()
{
    checkBoundary( enter, $thePlayer, getObjectId($nav1), 3500, onEnter );
}

function onEnter()
{
    if ( $warning == true )
        $warning = false;
}

// ------------------------------------------------------------------------------

function fail()
{
    if( $fail == false)
    {
        nexTalks( *IDSTR_CYB_NEX02, "CYB_NEX02.WAV" );    // mission failed
        schedule( "forceToDebrief(*IDSTR_MISSION_FAILED);", 4 );
        $fail = true;
    }    
}

// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------