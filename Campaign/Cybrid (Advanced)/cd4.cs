//////////////////////////////////////////////////////////
// CD4 Hunt//Find//Kill                                 //
//                                                      //
// Primary Goals                                        //
// 1.  Find and kill Caanon                             //
// 2.  Call drop ship                                   //
//  Secondary Goals                                     //
// 3.  Kill everyone                                    //
//////////////////////////////////////////////////////////



MissionBriefInfo missionBriefInfo               
{                                               
	campaign            = *IDSTR_CD4_CAMPAIGN;     
	title               = *IDSTR_CD4_TITLE;        
	planet              = *IDSTR_PLANET_TEMPERATE;      
	location            = *IDSTR_CD4_LOCATION;     
	dateOnMissionEnd    = *IDSTR_CD4_DATE;         
	media               = *IDSTR_CD4_MEDIA;        
	nextMission         = *IDSTR_CD4_NEXTMISSION;  
	successDescRichText = *IDSTR_CD4_DEBRIEF_SUCC; 
	failDescRichText    = *IDSTR_CD4_DEBRIEF_FAIL; 
	shortDesc           = *IDSTR_CD4_SHORTBRIEF;   
	longDescRichText    = *IDSTR_CD4_LONGBRIEF;
    successWavFile      = "CD4_Debriefing.wav";
    soundVol = "CD4.vol";
    endCinematicRec      = "cinCE.rec";
   	endCinematicSmk      = "cin_CE.smk";
    
};                                              
                                                
MissionBriefObjective  missionObjective1         //    DESTROY Caanon
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CD4_OBJ1_SHORT;
	longTxt             = *IDSTR_CD4_OBJ1_LONG;
    bmpname             = *IDSTR_CD4_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2         // Call drop ship
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CD4_OBJ2_SHORT;
	longTxt             = *IDSTR_CD4_OBJ2_LONG;
    bmpname             = *IDSTR_CD4_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3     // Kill everyone
{
	isPrimary           = False;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CD4_OBJ3_SHORT;
	longTxt             = *IDSTR_CD4_OBJ3_LONG;
    bmpname             = *IDSTR_CD4_OBJ3_BMPNAME;
};
                        
Pilot Caanon
{
   id = 30;  // Caanon
   
   name = "Caanon";
   skill = 1.0;
   accuracy = 1.0;
   aggressiveness = 1.0;
   activateDist = 800.0;
   deactivateBuff = 200.0;
   targetFreq = 1.2;
   trackFreq = 0.4;
   fireFreq = 0.2;
   LOSFreq = 0.4;
   orderFreq = 4.0;
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
    $playerAlive = True;
}

// ------------------------------------------------------------------------------

function onMissionStart()
{
    defineActors();
    defineCounts();
    defineRoutes();
    initFormations();
    cdAudioCycle(SS1, Mechsoul, SS4);
}

// ------------------------------------------------------------------------------

function defineActors()
{
    $caanon     =   "MissionGroup/vehicles/human/caanonGroup/Caanon";
    $caanonGroup =  "MissionGroup/vehicles/human/caanonGroup";
    $cg1        =   "MissionGroup/vehicles/human/caanonGroup/cg1";
    $cg2        =   "MissionGroup/vehicles/human/caanonGroup/cg2";
    $cg3        =   "MissionGroup/vehicles/human/caanonGroup/cg3";
    $imp1a      =   "MissionGroup/vehicles/human/imp1/imp1a";
    $imp1       =   "MissionGroup/vehicles/human/imp1";
    $imp2a      =   "MissionGroup/vehicles/human/imp2/imp2a";
    $imp2       =   "MissionGroup/vehicles/human/imp2";
    $imp3a      =   "MissionGroup/vehicles/human/imp3/imp3a";
    $imp3       =   "MissionGroup/vehicles/human/imp3";
    
    $drop       =   "MissionGroup/vehicles/cybrid/flyer1";
    
    $nav1       =   "MissionGroup/navMarkers/nav1";    
}

// ------------------------------------------------------------------------------

function defineCounts()
{
    $imp1Set    =   false;
    $imp2Set    =   false;
    $imp3Set    =   false;
    $cgAttacked =   false;
    $boarded    =   false;
    $timerStarted = false;
    $hidden     =   false;
    $setCaanon  =   false;
    $playerAlive =  true;
    $call       =   false;
    $chat       =   false;
    $warning    =   false;
    $fail       =   false;
    $caanonDead =   false;
    $impCount   =   9;
    $location   =   0;
    $reStart =   0;
}

// ------------------------------------------------------------------------------

function defineRoutes()
{
    $dropRoute  =   "MissionGroup/routes/dropRoute";
    $pickRoute1 =   "MissionGroup/routes/pickupRoute1";
    $pickRoute2 =   "MissionGroup/routes/pickupRoute2";
    
    $route1     =   "MissionGroup/routes/impRoute1";
    $route2     =   "MissionGroup/routes/impRoute2";
    $route3     =   "MissionGroup/routes/impRoute3";
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
    newFormation( diamond, 0,0,0,
                           -40,-40,0,
                           0,-80,0,
                           -40,40,0 );
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
    schedule( "setNavMarker( getObjectId($nav1), true, -1 );", 2 );
    initPatrols();
    navArrive();
}

// ------------------------------------------------------------------------------

function initPatrols()
{
    schedule( "order($drop, guard, $dropRoute);", 3 );
    setVehicleRadarVisible( getObjectId($caanon), false );
    setVehicleRadarVisible( getObjectId("MissionGroup/vehicles/human/caanonGroup/cg1"), false );
    setVehicleRadarVisible( getObjectId("MissionGroup/vehicles/human/caanonGroup/cg2"), false );
    setVehicleRadarVisible( getObjectId("MissionGroup/vehicles/human/caanonGroup/cg3"), false );
    order( $caanonGroup, useActiveRadar, false );
    order( $caanon, makeleader, true );
    order( $imp1a, makeleader, true );
    order( $imp1, formation, wedge );
    order( $imp2a, makeleader, true );
    order( $imp2, formation, wedge );
    order( $imp3a, makeleader, true );
    order( $imp3, formation, wall );
    distanceChecks();
}

// ------------------------------------------------------------------------------

function navArrive()
{
    if( isSafe(*IDSTR_TEAM_RED, getObjectId($nav1), 350) == false )
    {
        cgSet();  
    }
    else
        schedule( "navArrive();", 2 );
}

// ------------------------------------------------------------------------------

function distanceChecks()
{
    checkBoundary( enter, $thePlayer, getObjectId($nav1), 1500, hide );
    checkBoundary( leave, $thePlayer, getObjectId($nav1), 4000, warn );
    checkBoundary( leave, $thePlayer, getObjectId($nav1), 4500, fail );
}

// ------------------------------------------------------------------------------

function hide()
{
    if( $hidden != true )
    {
        say( 0, 6, *IDSTR_CD4_NEX01, "CD4_NEX01.wav" );       // Lost contact with Caanon
        $hidden = true;
    }
}

// ------------------------------------------------------------------------------

function cgSet()
{
    $location = randomInt(1,3);
    if( $setCaanon != true )
    {
        order( $caanonGroup, cloak, true );
        order( $caanonGroup, holdFire, true );
        if( $location == 1 )
        {
            setPosition($caanon, 1976, 408, 268 );
            setPosition("MissionGroup/vehicles/human/caanonGroup/cg1", -27,325,6);
            setPosition("MissionGroup/vehicles/human/caanonGroup/cg2", 829,-882,235);
            setPosition("MissionGroup/vehicles/human/caanonGroup/cg3", 1316,-135,10);
            cgAttack();
            $setCaanon = true;
        }
        else if( $location == 2 )
        {
            setPosition($caanon, 1976, 408, 268 );
            setPosition("MissionGroup/vehicles/human/caanonGroup/cg1", 1889,366,255);
            setPosition("MissionGroup/vehicles/human/caanonGroup/cg2", 2154,524,280);
            setPosition("MissionGroup/vehicles/human/caanonGroup/cg3", 1917,581,230);
            cgAttack();
            $setCaanon = true;
        }
        else if( $location == 3 )
        {
            setPosition($caanon, 1976, 408, 268 );
            setPosition("MissionGroup/vehicles/human/caanonGroup/cg1", -262,479,21);
            setPosition("MissionGroup/vehicles/human/caanonGroup/cg2", 1537,53,65);
            setPosition("MissionGroup/vehicles/human/caanonGroup/cg3", 1948,413,265);
            cgAttack();
            $setCaanon = true;
        }
    }        
}

// ------------------------------------------------------------------------------

function cgAttack()
{
    if( $cgAttacked != true )
    {
        order( $caanonGroup, guard, playerSquad );
        order( $caanonGroup, speed, high );
        schedule( "cgAttacking();", 15 );
        schedule( "imp1Positioning();", 40 );
        if ($location == 1 )
        {
            schedule( "say( 0, 3, *IDSTR_GEN_CAN10, \"GEN_CAA10.wav\" );", 15 );     // Vape 'um!
            schedule( "caaChat1();", 35 );
            schedule( "caaChat3();", 45 );
        }
        else if( $location == 2 )
        {
            schedule( "say( 0, 3, *IDSTR_GEN_CAN12, \"GEN_CAA12.wav\" );", 15 );     //  Chain 2, fire!
            schedule( "caaChat1();", 35 );
            schedule( "caaChat2();", 45 );
        }
        else if( $location == 3 )
        {
            schedule( "say( 0, 3, *IDSTR_GEN_CAN8, \"GEN_CAA8.wav\" );", 15 );       // Burning you!
            schedule( "caaChat1();", 35 );
            schedule( "caaChat2();", 45 );
        }
        $cgAttacked = true;
    }    
}

// ------------------------------------------------------------------------------

function cgAttacking()
{
    order( $caanonGroup, holdFire, false);
    order( $cg1, attack, pick(playerSquad) );
    order( $cg2, attack, pick(playerSquad) );
    order( $cg3, attack, pick(playerSquad) );
    order( $caanon, holdFire, false);
    order( $caanon, attack, pick(playerSquad) );
    setVehicleRadarVisible( getObjectId($caanon), true );
    setVehicleRadarVisible( getObjectId("MissionGroup/vehicles/human/caanonGroup/cg1"), true );
    setVehicleRadarVisible( getObjectId("MissionGroup/vehicles/human/caanonGroup/cg2"), true );
    setVehicleRadarVisible( getObjectId("MissionGroup/vehicles/human/caanonGroup/cg3"), true );
    schedule( "order($caanonGroup, useActiveRadar, true);", 5 );
}

// ------------------------------------------------------------------------------

function caaChat1()
{
    if( $caanonDead != true )
    {
        say( 0, 3, "GEN_CAA13.WAV" );                    // You're going down
    }
}

// ------------------------------------------------------------------------------

function caaChat2()
{
    if( $caanonDead != true )
    {
        say( 0, 3, "GEN_CAA10.wav" );                 // Vape 'um!
    }
}

// ------------------------------------------------------------------------------

function caaChat3()
{
    if( $caanonDead != true )
    {
        say( 0, 3, "GEN_CAA8.wav" );                  // Burning you!
    }
}

// ------------------------------------------------------------------------------

function imp1Positioning()
{
    if( $imp1Set != true )
    {
        setPosition( $imp1a, 3256,-1707,7 );
        order( $imp1a, guard, $route1 );
        order( $imp1a, speed, high );
        $imp1Set = true;
    }    
}

// ------------------------------------------------------------------------------

function cg::vehicle::onDestroyed(%destroyed, %destroyer)
{
    setPosition( $drop, -161,1301,590 );
    order( $drop, guard, $pickRoute1 );
    order( $drop, speed, medium );
    if( %destroyed == getObjectId($caanon) )
    {
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        say( 0, 6, *IDSTR_CYB_NEX04, "CYB_NEX04.wav" );
        $caanonDead = true;
    }
    if( isGroupDestroyed($caanonGroup) )
    {
        safeCheck();   
    }
}    
    
// ------------------------------------------------------------------------------

function imp::vehicle::onDestroyed(%destroyed, %destroyer)
{    
    $impCount--;
    if( ($impCount <= 7) && ($imp2Set != true) )
    {
        setPosition( $imp2a, 3391,338,400 );
        order( $imp2a, guard, $route2 );
        order( $imp2a, speed, high );
    }
    if( ($impCount <= 4) && ($imp3Set != true) )
    {
        setPosition( $imp3a, 955,-1189,231 );
        order( $imp3a, guard, $route3 );
        order( $imp3a, speed, high );
    }
    if( $impCount == 0 )
    {
        missionObjective3.status = *IDSTR_OBJ_COMPLETED;
        say( 0, 6, *IDSTR_CYB_NEX04, "CYB_NEX04.wav" );
    }
}

// ------------------------------------------------------------------------------

function vehicle::onDestroyed(%destroyed, %destroyer)
{
    if( %destroyed == $drop )
    {
        forceToDebrief( *IDSTR_MISSION_FAILED );
        missionObjective2.status = *IDSTR_OBJ_Failed;
    }
    if( %destroyed == $thePlayer )
        $playerAlive = false;    
}

// ------------------------------------------------------------------------------

function safeCheck()
{
    if( (isSafe(*IDSTR_TEAM_YELLOW, getObjectId($nav1), 1250)) && ($call != true) ) 
    {
        addGeneralOrder( *IDSTR_ORDER_CD4_1, "boardDrop();");        
        schedule( "forceCommandPopup();", 1 );
        $reStart++;
        $call = true;
        $chat = false;
    }
    else if( (isSafe(*IDSTR_TEAM_YELLOW, getObjectId($nav1), 1250) == false) && ($timerStarted == true) )
    {
        $call = false;
        if( $chat == false )
        {
            say( 0, 6, *IDSTR_CYB_NEX06, "CYB_NEX06.wav" );   //  Zone not safe
            $chat = true;
        }
        setHudTimer(-1, 0, "Timer Stopped", 1);
        $timerStarted = false;
    }
    else if( (isSafe(*IDSTR_TEAM_YELLOW, getObjectId($nav1), 1250)) && ($reStart != 0) )
    {
        schedule( "forceCommandPopup();", 1 );
        $reStart = 0;
    }
    schedule( "safeCheck();", 1 );
}

// ------------------------------------------------------------------------------

function callDrop()
{
    
    schedule( "forceCommandPopup();", 1 );
}

// ------------------------------------------------------------------------------

function boardDrop()
{
    if( $timerStarted != true )
    {
        setHudTimer(15, -1, *IDSTR_TIMER_CD4_1, 1);             // add Tag
        $timerStarted = true;
        schedule( "youWin();", 15 );   
    }
}

// ------------------------------------------------------------------------------

function youWin()
{
    if( $timerStarted == true )
    {       
        order( $drop, guard, $pickRoute2 );
        missionObjective2.status = *IDSTR_OBJ_COMPLETED;
        say( 0, 6, *IDSTR_CYB_NEX04, "CYB_NEX04.wav" );
        updatePlanetInventory(cd4);
        clearGeneralOrders();
        schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 3.0);
    }
}

// ------------------------------------------------------------------------------

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(cd4);
    clearGeneralOrders();
    schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 3.0);    
}

// ------------------------------------------------------------------------------

function nexTalks(%text, %wave)
{
    if( $playerAlive == true  )
    {
        say( 0, 6, %text, %wav );
    }    
    else
    {
        say( 0, 6, "" );
    }        
}

// ------------------------------------------------------------------------------

function caaTalks(%text, %wave)
{
    if( $playerAlive == true )
        say( 0, 3, %text, %wav );
    else
    {
        say( 0, 3, "" );
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
    checkBoundary( enter, $thePlayer, getObjectId($nav1), 4000, onEnter );
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
