/////////////////////////////////////////////////////
// HA4 Predator's Cage CS File                     //
//                                                 //
// Primary Goals                                   //
// 1.  Destroy base defences at Alpha              //
// 2.  Destroy comm tower                          //
// 3.  Don't destroy hangers                       //
// 4.  Secure base for recovery crew               //
// Secondary Goals                                 //
// 1.  Destroy all enemy resistance                //
/////////////////////////////////////////////////////



MissionBriefInfo missionBriefInfo               
{                                               
	campaign            = *IDSTR_HA4_CAMPAIGN;     
	title               = *IDSTR_HA4_TITLE;        
	planet              = *IDSTR_PLANET_MARS;      
	location            = *IDSTR_HA4_LOCATION;     
	dateOnMissionEnd    = *IDSTR_HA4_DATE;         
	media               = *IDSTR_HA4_MEDIA;        
	nextMission         = *IDSTR_HA4_NEXTMISSION;  
	successDescRichText = *IDSTR_HA4_DEBRIEF_SUCC; 
	failDescRichText    = *IDSTR_HA4_DEBRIEF_FAIL; 
	shortDesc           = *IDSTR_HA4_SHORTBRIEF;   
	longDescRichText    = *IDSTR_HA4_LONGBRIEF;
    successWavFile = "HA4_Debriefing.wav";
    soundVol = "ha4.vol";    
};                                              
                                                
MissionBriefObjective  missionObjective1         // Spot and kill turrets
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_HA4_OBJ1_SHORT;
	longTxt             = *IDSTR_HA4_OBJ1_LONG;
    bmpname             = *IDSTR_HA4_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2         // Destroy comm tower
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_HA4_OBJ2_SHORT;
	longTxt             = *IDSTR_HA4_OBJ2_LONG;
    bmpname             = *IDSTR_HA4_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3     // Don't destroy hangers
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_HA4_OBJ3_SHORT;
	longTxt             = *IDSTR_HA4_OBJ3_LONG;
    bmpname             = *IDSTR_HA4_OBJ3_BMPNAME;
};

MissionBriefObjective missionObjective4                    // Secure base for recovery
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_HA4_OBJ4_SHORT;
	longTxt             = *IDSTR_HA4_OBJ4_LONG;
    bmpname             = *IDSTR_HA4_OBJ4_BMPNAME;
};

MissionBriefObjective missionObjective5                    // Destroy all resistance
{
	isPrimary           = False;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_HA4_OBJ5_SHORT;
	longTxt             = *IDSTR_HA4_OBJ5_LONG;
    bmpname             = *IDSTR_HA4_OBJ5_BMPNAME;
};

DropPoint dropPoint1
{
    Name = "Bill";
    Desc = "high on a hill";
};

//$server::HudMapViewOffsetX = -1700;
//$server::HudMapViewOffsetY = -2500;

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
    
    defineNavMarkers();
    defineCounts();
    defineRoutes();
    initFormations();
    initPatrols();
    setHostile( *IDSTR_TEAM_PURPLE );
    forceScope("MissionGroup\\vehicles\\rebels\\artillery\\art1", 9999);
    forceScope("MissionGroup\\vehicles\\rebels\\artillery\\art2", 9999);
    forceScope("MissionGroup\\vehicles\\rebels\\artillery\\art3", 9999);
    cdAudioCycle(Gnash, Terror, ss1);
}

// ------------------------------------------------------------------------------

function defineActors()
{
    $tac    =   "MissionGroup\\vehicles\\rebels\\tacCom1";
    $art    =   "MissionGroup\\vehicles\\rebels\\artillery";
    $art1   =   "MissionGroup\\vehicles\\rebels\\artillery\\art1";
    $art2   =   "MissionGroup\\vehicles\\rebels\\artillery\\art2";
    $art3   =   "MissionGroup\\vehicles\\rebels\\artillery\\art3";
    $artilleryTarget = "";
    
    $imp1   =   "MissionGroup\\vehicles\\imperials\\imp1Group";
    $lead1  =   "MissionGroup\\vehicles\\imperials\\imp1Group\\imp1a";
    $imp2   =   "MissionGroup\\vehicles\\imperials\\imp2Group";
    $lead2  =   "MissionGroup\\vehicles\\imperials\\imp2Group\\imp2a";
    $imp3   =   "MissionGroup\\vehicles\\imperials\\imp3Group";
    $lead3  =   "MissionGroup\\vehicles\\imperials\\imp3Group\\imp3a";
    
    $drones =   "MissionGroup\\vehicles\\imperials\\droneGroup";
    $leadDrone  =   "MissionGroup\\vehicles\\imperials\\droneGroup\\drone1";
    $baseDrone  =   "MissionGroup\\vehicles\\imperials\\droneGroup\\drone2";
    
    $rock1      =   "MissionGroup\\rocks\\rock1";
    $rock2      =   "MissionGroup\\rocks\\rock2";
    $rock3      =   "MissionGroup\\rocks\\rock3";   
}

// ------------------------------------------------------------------------------

function defineNavMarkers()
{
    $navAlpha   =   "MissionGroup\\navMarkers\\navAlpha";
    $navBravo   =   "MissionGroup\\navMarkers\\navBravo";
}

// ------------------------------------------------------------------------------

function defineCounts()
{
    $playerAlive    =   true;
    $tacAlive       =   true;
    $tacAttacked    =   false;
    $tacMoved       =   false;
    $turretAttacked =   false;
    $art1Alive      =   true;
    $art2Alive      =   true;
    $commAlive      =   true;
    $cageAlive      =   true;
    $artilleryAttachedCam = false;
    $attachedFlyCam =   false;
    $recoTalk       =   0;
    $warnTalk       =   0;
    $failTalk       =   0;
    $playerSquad    =   3;
    
    $obj1           =   false;
    $obj2           =   false;
    $obj3           =   false;
    $obj4           =   false;
    $obj5           =   false;
    $missionFailed  =   false;
    
    $imp1Count      =   2;
    $imp2Count      =   3;
    $imp3Count      =   2;
    $totImps        =   8;
    $turretCount    =   4;
    $hangerCount    =   3;
    $boggyCall      =   0;
    $impMoveToggle  =   0;
    $atBravo        =   false;
    $hangerAttack   =   false;
    
}

// ------------------------------------------------------------------------------

function defineRoutes()
{
    $imp1Route  =   "MissionGroup\\imp1Route";
    $imp3Route  =   "MissionGroup\\imp3Route";
    $droneRoute  =   "MissionGroup\\droneRoute";
    $tacRoute   =   "MissionGroup\\tacRoute";
    $baseRoute  =   "MissionGroup\\baseRoute";
}

// ------------------------------------------------------------------------------

function initFormations()
{
    newFormation( wedge,  0,0,0, 
                          -20,-20,0, 
                          20,-20,0 );
    newFormation( wall,   0,0,0, 
                          20,0,0, 
                          40,0,0 );
    newFormation( line,   0,0,0, 
                          0,-20,0, 
                          0,-40,0, 
                          0,-60,0 );
}

// ------------------------------------------------------------------------------

function initPatrols()
{
    // tac
    order( $tac, shutdown, true );
    
    // artillery
    order( $art, shutdown, true );
    setHercOwner( $imp1, $art );
    setHercOwner( $imp2, $art );
    setHercOwner( $imp3, $art );
    setHercOwner( "MissionGroup\\vehicles\\imperials\\guard", $art );
    
    
    // Imp patrol1
    order( $lead1, makeLeader, true );
    //order( $imp1, shutdown, true );
    
    
    // Imp patrol2
    order( $lead2, makeLeader, true );
    order( $imp2, guard, "MissionGroup/imp2Route");
    order($imp2, formation, wedge );
    
    // Imp patrol3
    order( $lead3, makeLeader, true );
    order( $imp3, guard, $imp3Route );
    order( $imp3, speed, medium );
    order( $imp3, Formation, wall );
    
    // drones
    order( $drones, shutdown, true );
    schedule( "droneStart();", 30.0 );
    
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
    schedule( "distanceChecks();", 2 );
    schedule( "navInit();", 2 );
    chooseCage();
    schedule( "artTalks( *IDSTR_GEN_AR11, \"GEN_1ARB01.WAV\");", 15 );  // In position.
}

// ------------------------------------------------------------------------------

function navInit()
{
    setNavMarker( getObjectId($navAlpha), true, -1 );
    setNavMarker( getObjectId($navBravo), false );   
}

// ------------------------------------------------------------------------------

function distanceChecks()
{
    checkBoundary( enter, $thePlayer, getObjectId($navAlpha), 1200, nearAlpha );
    $nearAlpha = false;
    checkBoundary( enter, $thePlayer, getObjectId($navAlpha), 200, atAlpha );
    $atAlpha = false;
    checkBoundary( leave, $thePlayer, getObjectId($navAlpha), 2000, warning );
    $warning = false;
    checkBoundary( leave, $thePlayer, getObjectId($navAlpha), 2500, fail );
    $fail = false;
    checkBoundary( enter, $thePlayer, getObjectId($navBravo), 700, atBravo );
}

// ------------------------------------------------------------------------------

function droneStart()
{
    order( $leadDrone, shutdown, false );
    order( $leadDrone, guard, $baseRoute );
    order( $leadDrone, speed, low );
}

// ------------------------------------------------------------------------------

function chooseCage()
{
    $cage = "MissionGroup\\base\\hangers\\hanger" @ randomInt(1, 4 );
    order("MissionGroup\\vehicles\\imperials\\guard", guard, $baseRoute);
    order("MissionGroup\\vehicles\\imperials\\guard", holdposition, true);
}

// ------------------------------------------------------------------------------

function vehicle::onDestroyed(%destroyed, %destroyer)
{
    if(%destroyed == $thePlayer)
        $playerAlive = false;
    %team = getTeam(%destroyed);
}

// ------------------------------------------------------------------------------

function nearAlpha()
{
    if( $nearAlpha == false )
    {
        artAwake();
        $nearAlpha = true;
    }
}

// ------------------------------------------------------------------------------

function atAlpha()
{
    if( $atAlpha == false )
    {
        setNavMarker( getObjectId($navAlpha), false );
        $atAlpha = true;
        setNavMarker( getObjectId($navBravo), true, -1 );
        checkBoundary( leave, $thePlayer, getObjectId($navBravo), 2000, warning2 );
        $warning2 = false;
        checkBoundary( leave, $thePlayer, getObjectId($navBravo), 2500, fail );
        $warnTalk2 = 0;
    }
}

// ------------------------------------------------------------------------------

function artAwake()
{
    order( $art, shutdown, false );
}

// ------------------------------------------------------------------------------

function artAttack( %target )
{
    order( $art, Attack, %target );
    $artilleryTarget = %target;
}

// ------------------------------------------------------------------------------

function vehicle::onSpot(%spotter, %target)
{
    if (%target != "")
    { 
      artAttack(%target);
    }
    else if( (%target == "") && ($artilleryTarget != "") )
    {
      artTalks( *IDSTR_GEN_AR27, "GEN_1ARB07.wav" );
      artilleryClearTarget();
    } 
    if( $attachedFlyCam == false )
    {
        setTowerCamera( "MissionGroup\\vehicles\\rebels\\artillery\\art2", -2549, -47, 226.5 );
        $attachedFlyCam = true;
        schedule( "setPlayerCamera();", 4 );
    }
    if( $boggyCall == 0 )
    {
        schedule( "imp1Talks( *IDSTR_GEN_ICC3, \"GEN_ICCA03.WAV\" );", randomInt(3,10) ); //  Boggies on perimeter.
        $boggyCall++;
    }
    if( $impMoveToggle == 0 )
    {
        imp1Positioning();
        $impMoveToggle = 1;
    }
}

// ------------------------------------------------------------------------------

function vehicle::onMessage(%this, %message, %_1, %_2, %_3, %_4, %_5, %_6, %_7, %_8, %_9)
{
    if (%message == "ArtilleryOutOfRange")
        echo("Artillery: target ", %_1, " out of range, moving into position");
        
    else if (%message == "ArtilleryOutOfAmmo") 
    {
        artTalks( *IDSTR_GEN_AR25, "GEN_1ARB05.WAV" );
        artilleryClearTarget();
    }
    else if( %message == "TargetDestroyed")
    { 
        if( %this == getObjectId($art1) || %this == getObjectId($art2) || %this == getObjectId($art3) )
        {
            artilleryClearTarget();
        }
    }
}

//------------------------------------------------------------------------------

function artilleryClearTarget()
{
    order( $art, Clear, True );
    $artilleryTarget = "";
}

// ------------------------------------------------------------------------------

function structure::onDestroyed(%destroyed, %destroyer)
{
    if( (%destroyer == $art1) || (%destroyer == $art2) || (%destroyer == $art3) )
        artTalks( *IDSTR_GEN_AR14, "GEN_1ARB04.WAV" );
    if( $boggyCall == 0 )
    {
        droneScatter();
        schedule( "imp1Talks( *IDSTR_GEN_ICC3, \"GEN_ICCA03.WAV\" );", randomInt(3,10) ); //  Boggies on perimeter.
        $boggyCall++;
    }
}

// ------------------------------------------------------------------------------

function imp1Positioning()
{
    %distance1 = getDistance($thePlayer, getObjectId($rock1));
    %distance2 = getDistance($thePlayer, getObjectId($rock2));
    if( %distance1 > %distance2 ) 
    {
        setPosition($lead1, 489, -1676, 163 );
        if( $atBravo == false )
            schedule( "imp1Attack();", 0.5 );
        else
            schedule( "order($imp1, attack, playerSquad);", 1 );
    }
    else if( %distance1 < %distance2 )
    {
        setPosition($lead1, -1629, -1788, 192 );
        if( $atBravo == false )
            schedule( "imp1Attack();", 0.5 );
        else
            schedule( "order($imp1, attack, playerSquad);", 1 );
        
    }
}

// ------------------------------------------------------------------------------

function turretAttack()
{
    order( "MissionGroup\\base\\Turrets", Attack, playerSquad );
}

// ------------------------------------------------------------------------------

function turret::onAttacked(%attd, %attr)
{
    
    if( (getTeam(%attr) == *IDSTR_TEAM_YELLOW) && ($impMoveToggle == 0) )
    {
        droneScatter();
        turretAttack();
        imp1Positioning();
        $impMoveToggle = 1;
        if( $boggyCall == 0 )
        {
            schedule( "imp1Talks( *IDSTR_GEN_ICC03, \"GEN_ICCA03.WAV\" );", randomInt(1,5) ); //  Boggies on perimeter.
            $boggyCall++;
        }    
    }
}

// ------------------------------------------------------------------------------

function imp1Attack()
{
    order( $imp1, guard, "MissionGroup\\imp1Route" );
    order( $imp1, formation, wall );
    order( $imp1, speed, high );
}

// ------------------------------------------------------------------------------

function vehicle::onTargeted(%targd, %targr)
{
    if( ((getTeam(%targr) != *IDSTR_TEAM_YELLOW) &&  (getTeam(%targd) == *IDSTR_TEAM_YELLOW)) && ($turretAttacked == false) )
    {
        schedule( "imp1Talks(*IDSTR_HA4_IPA01, \"HA4_IPA01.wav\");", 8 );    //  spotter spotted
        turretAttack();
        $turretAttacked = true;
    } 
}

// ------------------------------------------------------------------------------

function droneScatter()
{
    order( $baseDrone, shutdown, false );
    order( $baseDrone, guard, $droneRoute );
    order( $baseDrone, speed, high );
    order( $leadDrone, guard, $droneRoute );
    order( $leadDrone, speed, high );
}

// ------------------------------------------------------------------------------

function turret::onDestroyed(%destroyed, %destroyer)
{
    $turretCount--;
    if( $boggyCall == 0 ) 
    {
        schedule( "imp1Talks( *IDSTR_GEN_ICC3, \"GEN_ICCA03.WAV\" );", 1 ); //  Boggies on perimeter.
        $boggyCall++;
        droneScatter();
    }
    if( (%destroyer == getObjectId($art1)) || (%destroyer == getObjectId($art2)) || (%destroyer == getObjectId($art3))  )
    {
        artTalks( *IDSTR_GEN_AR14, "GEN_1ARB04.WAV" );
        droneScatter();
    }
    if( $turretCount == 3)
    {
        order( $imp2, Formation, wedge );
        order( $lead2, guard, $baseRoute);
        order( $imp2, speed, high );
        order( $imp2, holdPosition, true );
    }
    if( $turretCount == 2 )
    {
        if( $obj2 != true )
        {
            order( $imp3, guard, $baseRoute );
            order( $imp3, speed, high );
            schedule( "say(0,5, \"HA4_IMC01.WAV\");", 3 );
            schedule( "say(0,5, \"HA4_STT01.WAV\");", 8 );
        }    
    }    
    if( $turretCount == 0 )
    {
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        say( 0, 6, "GEN_OC01.wav" );
        $obj1 = true;
        safeCheck();
        if( $totImps <= 0 )
        { 
            missionObjective4.status = *IDSTR_OBJ_COMPLETED;
            safeCheck();
        }            
    }
}

// ------------------------------------------------------------------------------

function comm::structure::onDestroyed(%destroyed, %destroyer)
{
    $commAlive = false;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    say( 0, 6, "GEN_OC01.wav" );
    droneScatter();
    turretAttack();
    safeCheck();
    if( $turretCount == 4 )
    {
        order( $imp2, Formation, wedge );
        order( $lead2, guard, $baseRoute);
        order( $imp2, speed, high );
        order( $imp2, holdPosition, true );
        order("MissionGroup\\vehicles\\imperials\\guard", attack, playerSquad);
    }    
    if( $turretCount >= 3 )
    {
        $obj2 = true;
        order( $imp3, guard, "MissionGroup/extras/point" );
        order( $imp3, holdposition, true );
        order( $imp3, speed, low );
        $totImps = $totImps - 2;
    }
}

// ------------------------------------------------------------------------------

function atBravo()
{
    $atBravo = true;
    setNavMarker( getObjectId($navBravo), false );
    order( $imp1, Attack, playerSquad );
    order( $imp1, speed, high );
    order( $imp2, holdPosition, false );
    order( $imp2, attack, playerSquad );
    order("MissionGroup\\vehicles\\imperials\\guard", holdposition, false);
    safeCheck();
    if( $impMoveToggle == 0 )
    {
        imp1Positioning();
        $impMoveToggle = 1;
    }    
    if( ($totImps > 3) && ($obj2 != true) )
    {
        order( $imp3, Attack, "MissionGroup\\base\\hangers" );
        order( $imp3, medium, high );
        if( $hangerAttack != true )
        {
            artTalks( *IDSTR_HA4_ASC01, "HA4_ASC01.WAV" );
            $hangerAttack = true;
        }
    } 
}

// ------------------------------------------------------------------------------

function hang::structure::onDestroyed(%destroyed, %destroyer)
{
    if( %destroyed == getObjectId($cage) )
    {
        missionObjective3.status = *IDSTR_OBJ_FAILED;
        $cageAlive = false;
        schedule( "setDominantCamera( $thePlayer, getObjectId($cage), -200,-200, 30 );", 1 );
        schedule( "forceToDebrief( *IDSTR_MISSION_FAILED );", 5 );
    }
    else if( (%destroyed != $cage) && (getGroup(%destroyer) == getObjectId($imp3)) )
    {
        order($imp3, attack, "MissionGroup\\base\\hangers" );   
    }
}

// ------------------------------------------------------------------------------

function imp::vehicle::onDestroyed(%destroyed, %destroyer)
{
    $totImps--;
    vehicle::salvage(%destroyed);
    if(isGroupDestroyed($imp1))
    {
        order( $imp2, holdPosition, false );
        order( $imp2, Attack, playerSquad );
    }
    if(isGroupDestroyed($imp2))
    {
        order("MissionGroup\\vehicles\\imperials\\guard", holdposition, false);
        order("MissionGroup\\vehicles\\imperials\\guard", attack, playerSquad);
    }
    if( ($totImps <= 4) && ($obj2 != true) ) 
    {
        order( $imp3, guard,  $baseRoute );
        order( $imp3, speed, high );
    }
    if( $totImps <= 0 )
    {
        missionObjective5.status = *IDSTR_OBJ_COMPLETED;
        $obj5 = true;
        safeCheck();
    }
    if( ($totImps <= 0) && ($cageAlive == true) )
    {
        
        $obj3 = true;
        safeCheck();
        if( $recoTalk == 0 )
        {
            say( 0, 6, "GEN_OC01.wav" );
            $recoTalk++;
        }
    }
    if( ($totImps <= 0) && ($obj1 == true) )
    {
        missionObjective4.status = *IDSTR_OBJ_COMPLETED;
        $obj4 = true;
        schedule( "safeCheck();", 3 );
        if( $recoTalk == 0 )
        {
            schedule( "say( 0, 6, \"GEN_OC01.wav\" );", 2 );
            $recoTalk++;
        }
    }
}

// ------------------------------------------------------------------------------

function safeCheck()
{
    if( isSafe(*IDSTR_TEAM_YELLOW, $cage, 1000) )
    {
        
        missionObjective3.status = *IDSTR_OBJ_COMPLETED;
        missionObjective4.status = *IDSTR_OBJ_COMPLETED;
        missionObjective3.status = *IDSTR_OBJ_COMPLETED;
        winCheck();
    }
}

// ------------------------------------------------------------------------------

function winCheck()
{
    if( (missionObjective1.status == *IDSTR_OBJ_COMPLETED) &&
        (missionObjective2.status == *IDSTR_OBJ_COMPLETED) &&
        (missionObjective3.status == *IDSTR_OBJ_COMPLETED) &&
        (missionObjective4.status == *IDSTR_OBJ_COMPLETED) )
    {
        moveTac();
    }        
}

// ------------------------------------------------------------------------------

function moveTac()
{
    if( $tacMoved == false )
    {
        addGeneralOrder(*IDSTR_ORDER_HA4_1, "radioRecoveryTeam();");
        
        schedule( "repeatGeneralOrder(PlayerManager::playerNumToVehicleId(2049), *IDSTR_ORDER_HA4_1);", 4 );
        
        setPosition( $tac, 2038,1285,550 );
        schedule( "artTalks( *IDSTR_HA4_TCM01, \"HA4_TCM1.wav\" );", 4 );    // Area secure
        $tacMoved = true;
    } 
}

// ------------------------------------------------------------------------------

function radioRecoveryTeam() 
{
    dataStore(PlayerManager::playerNumToVehicleId(2049), *IDSTR_ORDER_HA4_1, True);
    removeGeneralOrder( *IDSTR_ORDER_HA4_1 );
       
    schedule( "recoveryTalks( *IDSTR_HA4_REC01, \"HA4_REC01.wav\");", 5.0 );
    order( $tac, guard, $tacRoute );
    order( $tac, speed, high );
    schedule( "setDominantCamera( getObjectId($tac), getObjectId($cage), -200,-200, 30 );", 3 );
    schedule( "youWin();", 10 );
}

// ------------------------------------------------------------------------------

function warning()
{
    if( ($warnTalk == 0) && ($atAlpha == false) )
    {
        artTalks( *IDSTR_GEN_TCM1, "GEN_TCM01.wav" );  // Off course
        $warnTalk = 1;
    }
}

// ------------------------------------------------------------------------------

function warning2()
{
    if( ($warnTalk2 == 0) && ($atBravo == true) )
    {
        artTalks( *IDSTR_GEN_TCM1, "GEN_TCM01.wav" );  // Off course
        $warnTalk2 = 1;
    }
}

// ------------------------------------------------------------------------------

function fail()
{
    if( $failTalk == 0 )
    {
        artTalks( *IDSTR_GEN_TCM2, "GEN_TCM02.wav" );    // mission failed
        schedule( "forceToDebrief(*IDSTR_MISSION_FAILED);", 3.0);
        $failTalk++;
    }    
}

// ------------------------------------------------------------------------------

function youWin()
{
    updatePlanetInventory(ha4);
    schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 3.0);
}

// ------------------------------------------------------------------------------

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    missionObjective4.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(ha4);
    schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 3.0);    
}

// ------------------------------------------------------------------------------

function tacTalks(%text, %wave)
{
    if( $playerAlive  )
        say( 0, 2, %text, %wav );
    else
    {
        say( 0, 2, "" );
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

function imp1Talks(%text, %wav)
{
    if( $playerAlive  )
        say( 0, 3, %text, %wav );
    else
    {
        say( 0, 3, "" );
    }
}

// ------------------------------------------------------------------------------

function recoveryTalks(%text, %wav)
{
    if( $playerAlive  )
        say( 0, 4, %text, %wav );
    else
    {
        say( 0, 4, "" );
    }
}


// END --------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
