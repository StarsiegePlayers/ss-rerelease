/////////////////////////////////////////////////////
// HD1 Dies Irae CS File                           //
//                                                 //
// Primary Goals                                   //
// 1.  Defend Nav Alpha                            //
// 2.  Defend Uplink                               //
// 3.  Defend Escape Ships and Base                //
// 4.  Dies Irae Must Launch                       //
// Secondary Goals                                 //
// 1.  Prevent Cybrids from getting to Base        //
/////////////////////////////////////////////////////



MissionBriefInfo missionBriefInfo               
{                                               
	campaign            = *IDSTR_HD1_CAMPAIGN;     
	title               = *IDSTR_HD1_TITLE;        
	planet              = *IDSTR_PLANET_TITAN;      
	location            = *IDSTR_HD1_LOCATION;     
	dateOnMissionEnd    = *IDSTR_HD1_DATE;         
	media               = *IDSTR_HD1_MEDIA;        
	nextMission         = *IDSTR_HD1_NEXTMISSION;  
	successDescRichText = *IDSTR_HD1_DEBRIEF_SUCC; 
	failDescRichText    = *IDSTR_HD1_DEBRIEF_FAIL; 
	shortDesc           = *IDSTR_HD1_SHORTBRIEF;   
	longDescRichText    = *IDSTR_HD1_LONGBRIEF;
    successWavFile      = "HD1_Debriefing.wav";
    soundVol = "HD1.vol";    
};                                              
                                                
MissionBriefObjective  missionObjective1         // DEFEND NAV ALPHA
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_HD1_OBJ1_SHORT;
	longTxt             = *IDSTR_HD1_OBJ1_LONG;
    bmpname             = *IDSTR_HD1_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2         // DEFEND UPLINK
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_HD1_OBJ2_SHORT;
	longTxt             = *IDSTR_HD1_OBJ2_LONG;
    bmpname             = *IDSTR_HD1_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3     // DEFEND ESCAPE SHIP AND BASE
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_HD1_OBJ3_SHORT;
	longTxt             = *IDSTR_HD1_OBJ3_LONG;
    bmpname             = *IDSTR_HD1_OBJ3_BMPNAME;
};

MissionBriefObjective missionObjective4                    // DIES IRAE MUST LAUNCH
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_HD1_OBJ4_SHORT;
	longTxt             = *IDSTR_HD1_OBJ4_LONG;
    bmpname             = *IDSTR_HD1_OBJ4_BMPNAME;
};

MissionBriefObjective missionObjective5                    // PREVENT CYBRID FROM REACHING BASE
{
	isPrimary           = False;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_HD1_OBJ5_SHORT;
	longTxt             = *IDSTR_HD1_OBJ5_LONG;
    bmpname             = *IDSTR_HD1_OBJ5_BMPNAME;
};

DropPoint dropPoint1
{
    Name = "Bill";
    Desc = "high on a hill";
};

Pilot Caanon
{
   id = 51;
   
   name = "Caanon";
   skill = 0.6;
   accuracy = 0.8;
   aggressiveness = 2.0;
   activateDist = 700.0;
   deactivateBuff = 200.0;
   targetFreq = 0.7;
   trackFreq = 0.4;
   fireFreq = 0.1;
   LOSFreq = 0.4;
   orderFreq = 2.0;
};

Pilot Harabec
{
   id = 52;
   
   name = "Harabec";
   skill = 0.6;
   accuracy = 0.8;
   aggressiveness = 2.0;
   activateDist = 700.0;
   deactivateBuff = 200.0;
   targetFreq = 0.7;
   trackFreq = 0.4;
   fireFreq = 0.1;
   LOSFreq = 0.4;
   orderFreq = 2.0;
};

Pilot Bonesnap
{
   id = 25;
   
   name = "Bonesnap";
   skill = 0.5;
   accuracy = 0.5;
   aggressiveness = 0.5;
   activateDist = 500.0;
   deactivateBuff = 200.0;
   targetFreq = 2.0;
   trackFreq = 0.2;
   fireFreq = 0.2;
   LOSFreq = 0.1;
   orderFreq = 1.0;    
};

Pilot Pirouette
{
   id = 26;
   
   name = "Pirouette";
   skill = 0.5;
   accuracy = 0.5;
   aggressiveness = 0.5;
   activateDist = 500.0;
   deactivateBuff = 200.0;
   targetFreq = 2.0;
   trackFreq = 0.2;
   fireFreq = 0.2;
   LOSFreq = 0.1;
   orderFreq = 1.0;    
};

Pilot Kungulo
{
   id = 27;
   
   name = "Kungulo";
   skill = 0.5;
   accuracy = 0.5;
   aggressiveness = 0.5;
   activateDist = 500.0;
   deactivateBuff = 200.0;
   targetFreq = 2.0;
   trackFreq = 0.2;
   fireFreq = 0.2;
   LOSFreq = 0.1;
   orderFreq = 1.0;    
};

Pilot HattrasSmith
{
   id = 29;
   
   name = "Hattras-Smith";
   skill = 0.5;
   accuracy = 0.5;
   aggressiveness = 0.5;
   activateDist = 500.0;
   deactivateBuff = 200.0;
   targetFreq = 2.0;
   trackFreq = 0.2;
   fireFreq = 0.2;
   LOSFreq = 0.1;
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
    $playerAlive = True;
}

// ------------------------------------------------------------------------------

function onMissionStart()
{
    defineActors();
    defineNavMarkers();
    defineCounts();
    defineRoutes();
    defineZones();
    initFormations();
    cybridDroppings();
    schedule( "initPatrols();", 6 );
    isUplinkSafe();
    setHostile( *IDSTR_TEAM_PURPLE );
    schedule( "harTalks( *IDSTR_HD1_HAR01, \"HD1_HAR01.wav\" );", 4 );  // I go there, he goes there, and you go there.
    schedule( "caaTalks( *IDSTR_HD1_CAA01, \"HD1_CAA01.wav\" );", randomInt(30,45));
    cdAudioCycle(Watching, Cyberntx, Terror);
}

// ------------------------------------------------------------------------------

function defineActors()
{
    $har    =   "MissionGroup\\vehicles\\humans\\lightGroup\\Harabec";
    $light  =   "MissionGroup\\vehicles\\humans\\lightGroup";
    $caa    =   "MissionGroup\\vehicles\\humans\\zebraGroup\\caanon";
    $zebra  =   "MissionGroup\\vehicles\\humans\\zebraGroup";
    $drones =   "MissionGroup\\vehicles\\drones";
    $fuel   =   "MissionGroup\\vehicles\\drones\\fuel";
    $utility =  "MissionGroup\\vehicles\\drones\\utility";
    $shuttle =  "MissionGroup\\vehicles\\drones\\shuttle";
    $fly1   =   "MissionGroup\\vehicles\\drones\\Flyer1";
    $fly2   =   "MissionGroup\\vehicles\\drones\\Flyer2";
    $a      =   "MissionGroup\\vehicles\\cybrids\\squadA";
    $a1     =   "MissionGroup\\vehicles\\cybrids\\squadA\\a1";
    $b      =   "MissionGroup\\vehicles\\cybrids\\squadB";
    $b1     =   "MissionGroup\\vehicles\\cybrids\\squadB\\b1";
    $c      =   "MissionGroup\\vehicles\\cybrids\\squadC";
    $c1     =   "MissionGroup\\vehicles\\cybrids\\squadC\\c1";
    $d      =   "MissionGroup\\vehicles\\cybrids\\squadD";
    $d2     =   "MissionGroup\\vehicles\\cybrids\\squadD\\d2";
    $e      =   "MissionGroup\\vehicles\\cybrids\\squadE";
    $e2     =   "MissionGroup\\vehicles\\cybrids\\squadE\\e2";
    $pod1   =   "MissionGroup\\vehicles\\pod1";
    $pod2   =   "MissionGroup\\vehicles\\pod2";
    $pod3   =   "MissionGroup\\vehicles\\pod3";
    $upLink =   "MissionGroup\\base\\DiesIrae\\UpLink";
    $beam   =   "MissionGroup\\base\\DiesIrae\\beam";
    $base   =   "MissionGroup\\base\\buildings";
    $turrets =  "MissionGroup\\base\\Turrets";
}

// ------------------------------------------------------------------------------

function defineNavMarkers()
{
    $navAlpha   =   "MissionGroup\\navMarkers\\navAlpha";
    $navBravo   =   "MissionGroup\\navMarkers\\navBravo";
    $navCharlie =   "MissionGroup\\navMarkers\\navCharlie";
}

// ------------------------------------------------------------------------------

function defineZones()
{
    $aZone      =   "MissionGroup\\zoneMarkers\\aZone";
    $bZone      =   "MissionGroup\\zoneMarkers\\bZone";
    $cZone      =   "MissionGroup\\zoneMarkers\\cZone";
    $leftRock   =   "MissionGroup\\zoneMarkers\\leftRock";
    $rightRock   =   "MissionGroup\\zoneMarkers\\rightRock";
}

// ------------------------------------------------------------------------------

function defineCounts()
{
    $missionFlow    =   true;
    $uplinkSafe     =   true;
    $fly1Alive      =   true;
    $fly2Alive      =   true;
    $launchStart    =   false;
    $flyersGone     =   false;
    $launchReady    =   false;
    $harArrives     =   false;
    $caaArrives     =   false;
    $harAlive       =   true;
    $caaAlive       =   true;
    $dStarted       =   false;
    $eStarted       =   false;
    $cStarted       =   false;
    $helpCall       =   0;
    $filler         =   0;
    $harAttacked    =   0;
    $caaAttacked    =   0;
    $aAlive         =   true;
    $aCount         =   3;
    $aAttacked      =   false;
    $bAttacked      =   false;
    $bCount         =   3;
    $cCount         =   3;
    $dCount         =   3;
    $eCount         =   3;
    $bridCount      =   15;
    $buildCount     =   9;
    $flyerCount     =   2;
    $playerAlive    =   true;
    $warning        =   false;
    $fail           =   false;
}

// ------------------------------------------------------------------------------

function defineRoutes()
{
    $harRoute   =   "MissionGroup\\harRoute";
    $caaRoute   =   "MissionGroup\\caaRoute";
    $aRoute     =   "MissionGroup\\aRoute";
    $a2Route    =   "MissionGroup\\a2Route";
    $flyRoute   =   "MissionGroup\\flyRoute";
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
    // Harabec
    order( $har, makeLeader, true );
    schedule( "harGuard();", 8 );
    
    // Caanon
    order( $caa, makeLeader, true );
    schedule( "caaGuard();", 9.5 );
   
    // drones
    order( $drones, shutdown, true );
    
    // Turrets
    order( $turrets, shutdown, true);
    
    // cybrid leaders
    order( $c1, makeLeader, true );
    order( $c, formation, wedge );
    order( $d2, makeLeader, true );
    order( $d, formation, wedge );
    order( $d, holdFire, true );
    order( $e2, makeLeader, true );
    order( $e, formation, wedge );
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
    schedule( "distanceChecks();", 1 );
    schedule( "navInit();", 1 );
    schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot1\"));", 4 );
    schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot2\"));", 5.5 );
    schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot1\"));", randomInt(8,15) );
    schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot3\"));", 10 );
    schedule( "alphaCheck();", 100);
    $firstCheck = false;
}

// ------------------------------------------------------------------------------

function navInit()
{
    setNavMarker( getObjectId($navAlpha), true, -1 );
    setNavMarker( getObjectId($navBravo), false );
    setNavMarker( getObjectId($navCharlie), false );   
}

// ------------------------------------------------------------------------------

function alphaCheck()
{
    if( (getDistance($thePlayer, getObjectId($navAlpha)) > 800) && ($firstCheck == false))
    {
        tacTalks( *IDSTR_HD1_TAC01, "HD1_wu01.wav" ); //Where are you?
        $firstCheck = true;
    }
}

// ------------------------------------------------------------------------------

function har::vehicle::onAttacked(%attd, %attr)
{
    if( getGroup(%attr) == getObjectId(playerSquad) )
    {
        $harAttacked++;
        if( $harAttacked == 10 )
        {
            harTalks( *IDSTR_GEN_HAR3, "GEN_HAR03.wav" );    // Finger off the trigger!

        }
        if( $harAttacked == 15 )
        {
            harTalks( *IDSTR_GEN_HAR8, "GEN_HAR08.wav" );    // Fine! You want some?!
            order($light, attack, $thePlayer);
            order($zebra, attack, $thePlayer);
        }
    }
}

// ------------------------------------------------------------------------------

function caa::vehicle::onAttacked(%attd, %attr)
{
    if( getGroup(%attr) == getObjectId(playerSquad) )
    {
        $caaAttacked++;
        if( $caaAttacked == 10 )
        {
            caaTalks( *IDSTR_GEN_CAA1, "GEN_CAA01.wav" );    // Defective equipment?

        }
        if( $caaAttacked == 15 )
        {
            harTalks( *IDSTR_GEN_CAA7, "GEN_CAA07.wav" );    // You're dead!
            order($light, attack, $thePlayer);
            order($zebra, attack, $thePlayer);
        }
    }
}

// ------------------------------------------------------------------------------

function vehicle::onDestroyed(%destroyed, %destroyer)
{
    bridCheck();
    if( %destroyed == getObjectId($har) )  
    {
        $harAlive = false;
        forceToDebrief(*IDSTR_MISSION_FAILED);
    }
    if( %destroyed == getObjectId($caa) )
    {
        $caaAlive = false;
        forceToDebrief( *IDSTR_MISSION_FAILED );
    }
    if( (getGroup(%destroyed) == getObjectId($zebra)) || 
        (getGroup(%destroyed) == getObjectId($light)) )
    {
        order($zebra, attack, $thePlayer);
        order($light, attack, $thePlayer);    
    }
}

// ------------------------------------------------------------------------------

function cybridDroppings()
{
    %num = randomInt(1,4);
    if( %num == 1 )
    {
        schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot13\"));", randomInt(1,50) );
    }
    else if( %num == 2 )
    {
        schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot14\"));", randomInt(1,50) );
    }
    else if( %num == 4 )
    {
        schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot15\"));", randomInt(1,50) );
    }
    else if( %num == 5 )
    {
        schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot16\"));", randomInt(1,50) );
    }
    if( $launchStart == false )
    {
        schedule( "cybridDroppings();", randomInt(20,30));
    }
}

// ------------------------------------------------------------------------------

function isUplinkSafe()
{
    if( isSafe(*IDSTR_TEAM_YELLOW, "MissionGroup\\base\\DiesIrae\\UpLink", 600 ) == true )
    {
        $uplinkSafe = true;
        schedule( "isUplinkSafe();", 2 ); 
        youWin();
    }
    else 
    {
        if( $helpCall == 0 )
        {
            tacTalks(*IDSTR_HD1_TAC02, "HD1_wu02.wav");   //The uplink is under attack!
            $helpCall++;
            $uplinkSafe = false;
            schedule( "isUplinkSafe();", 2 );
            if( getDistance(getObjectId(getLeader($a)), getObjectId($upLink)) <= 300 )
                aAttack();
        }                   
    }
}

// ------------------------------------------------------------------------------

function distanceChecks()
{
    checkBoundary( enter, $thePlayer, getObjectId($navAlpha), 800, nearAlpha );
    $nearAlpha = false;
    checkBoundary( enter, getObjectId($har), getObjectId("MissionGroup\\harRoute\\m2"), 50, lightForm );    // Harabec to 2nd path marker
    $lFormCheck = false;
    checkBoundary( enter, getObjectId($caa), getObjectId("MissionGroup\\caaRoute\\m2"), 50, zebraForm );    // Caanon to 2nd path marker
    $zFormCheck = false;
    checkBoundary( leave, $thePlayer, getObjectId($upLink), 700, nearWrongZone );
    checkBoundary( leave, $thePlayer, getObjectId($upLink), 1000, nearWrongZone2 );
}
                                         
// ------------------------------------------------------------------------------

function harGuard()
{
    order( $har, guard, $harRoute );
    order( $har, speed, high );
}

// ------------------------------------------------------------------------------

function lightForm()
{
    order( $light, formation, wedge );
    order( $light, guard, "MissionGroup\\zoneMarkers\\cZone");
    order( $light, speed, high );
    schedule( "cZoneCheck();", 5 );
    schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot10\"));", 4 );
    schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot11\"));", 30 );
    dropPod(getObjectId("MissionGroup\\podSpots\\spot12"));
}

// ------------------------------------------------------------------------------

function cZoneCheck()
{
    %distance = getDistance( getObjectId($har), getObjectId($cZone) );
    if( (%distance <= 400 ) && ($harArrives != true) )
    {
        order( $light, holdposition, true );
        harTalks( *IDSTR_HD1_HAR02, "HD1_HAR02.wav");      //Charlie clear
        schedule( "atAlpha();", 10 );
        $harArrives = true;
    }
    else if( (%distance > 400) && ($harArrives != true) )
    {
        schedule( "cZoneCheck();", 2 );
    }
}

// ------------------------------------------------------------------------------

function caaGuard()
{
    order( $caa, guard, $caaRoute );
    order( $caa, speed, high );
}

// ------------------------------------------------------------------------------

function zebraForm()
{
    order( $zebra, formation, wedge );
    order( $zebra, guard, "MissionGroup\\zoneMarkers\\aZone");
    order( $zebra, speed, high );
    schedule( "aZoneCheck();", 5 );
    schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot3\"));", 5 );
    schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot4\"));", 10 );
}

// ------------------------------------------------------------------------------

function aZoneCheck()
{
    %distance = getDistance( getObjectId($caa), getObjectId($aZone) );
    if( (%distance <= 100 ) && ($caaArrives != true) )
    {
        order( $zebra, holdposition, true );
        caaTalks( *IDSTR_HD1_CAA02, "HD1_CAA02.wav" );    //Bravo is clear
        schedule( "atAlpha2();", randomInt( 10, 20 ) );
        $caaArrives = true;
    }
    else if( (%distance > 100) && ($caaArrives != true) )
    {
        schedule( "aZoneCheck();", 2 );
    }
}

// ------------------------------------------------------------------------------

function nearAlpha()
{
    order( $turrets, shutdown, false );
    if( $nearAlpha == false )
    {
        %routeNum = randomInt(1,7);
        if( %routeNum == 5 )
        {
            %route = $a2Route;
        }    
        else
        {
            %route = $aRoute;
        }
        $aPath = %route;
        order( $a, shutdown, false );
        order( $a1, makeLeader, true );
        order( $a, Formation, line );
        order( $a1, guard, $aPath );
        
        order( $a, speed, High );
        order( $a, holdPosition, true );
        dropPod(getObjectId("MissionGroup\\podSpots\\spot5"));
        schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot6\"));", randomInt(1.5,5) );
        schedule( "bStart();", 35 );
        $nearAlpha = true;
    }
    if( $fly2Alive == true )
    {
        order( $fly2, shutdown, false );
        order( $fly2, guard, $flyRoute );
        $flyersGone = true;
    }
    checkBoundary( leave, $thePlayer, getObjectId($navAlpha), 1500, warning );
    checkBoundary( leave, $thePlayer, getObjectId($navAlpha), 2500, fail );
}

// ------------------------------------------------------------------------------

function bStart()
{
    %target = pick($base, $utility, $shuttle, $fuel, $upLink );
    order( $b, shutdown, false );
    order( $b1, makeleader, true );
    order( $b, formation, line );
    order( $b1, attack, %target );
    order( $b, speed, high );
    schedule( "order( $b, cloak, true );", 25 );
}

// ------------------------------------------------------------------------------

function atAlpha()
{
    if( ($filler <= 1 ) &&  ($harArrives == true) )
    {
        harTalks(*IDSTR_HD1_HAR03, "HD1_HAR03.wav");                   ///bogies at Charlie
        $filler++;
        $harAttacked = 2;
        setPosition( getObjectId($har), -9100,5100,50 );
        setPosition( "MissionGroup/vehicles/humans/lightGroup/l1", -9120,5120,70 );
        setPosition( "MissionGroup/vehicles/humans/lightGroup/l2", -9080,5080,70 );
        schedule( "order($light, shutdown, true);", 1 );
        schedule( "harFights();", 5 );
    }
}

// ------------------------------------------------------------------------------

function atAlpha2()
{
    if( ($filler <= 1 ) && ($caaArrives == true)  )
    {
        schedule( "caaTalks(*IDSTR_HD1_CAA03, \"HD1_CAA03.wav\");", randomInt(5, 10));     ///bogies at Bravo
        $filler++;
        $caaAttacked = 2;
        setPosition( getObjectId($zebra), -9089,5060,48 );
        setPosition( "MissionGroup/vehicles/humans/zebraGroup/z1", -9100,5080,60 );
        setPosition( "MissionGroup/vehicles/humans/zebraGroup/z2", -9060,5040,60 );
        schedule( "order($zebra, shutdown, true);", 1 );
        schedule( "caaFights();", 15 );
    }
}

// ------------------------------------------------------------------------------

function harFights()
{
    harTalks(*IDSTR_GEN_HAR9, "GEN_HAR09.wav");                                        // stay bounce
    schedule("harTalks(*IDSTR_GEN_HAR11, \"GEN_HAR11.wav\");", randomInt(10,20) );     // watch six
    schedule("harTalks(*IDSTR_GEN_HAR12, \"GEN_HAR12.wav\");", randomInt(32,50) );     // oh yeah
    schedule("harTalks(*IDSTR_GEN_HAR9, \"GEN_HAR09.wav\");", randomInt(60,80) );      // bounce
    schedule("harTalks(*IDSTR_GEN_HAR12, \"GEN_HAR12.wav\");", randomInt(100,120) );     // oh yeah
    schedule("harTalks(*IDSTR_GEN_HAR10, \"GEN_HAR10.wav\");", randomInt(155,165) );     // nice one
    
    schedule("rebCTalks(*IDSTR_GEN_RCC5, \"GEN_RCCc05.wav\");", randomInt(12,22) );     // aim cockpit
    schedule("rebBTalks(*IDSTR_GEN_RCC3, \"GEN_RCCb03.wav\");", randomInt(25,38) );     // more coming
    schedule("rebATalks(*IDSTR_GEN_RCC4, \"GEN_RCCa04.wav\");", randomInt(40,45) );     // shut it down
    schedule("rebCTalks(*IDSTR_GEN_RCC6, \"GEN_RCCc06.wav\");", randomInt(48,50) );     // roj, going mark
    schedule("rebBTalks(*IDSTR_GEN_RCC10, \"GEN_RCCb10.wav\");", randomInt(65,90) );    // catching heat
    schedule("rebATalks(*IDSTR_GEN_RCC8, \"GEN_RCCa08.wav\");", randomInt(75,95) );     // squik
    schedule("rebCTalks(*IDSTR_GEN_RCC3, \"GEN_RCCc03.wav\");", randomInt(90,120) );    // more coming
    schedule("rebATalks(*IDSTR_GEN_RCC9, \"GEN_RCCa09.wav\");", randomInt(115,125) );     // watch back
    schedule("rebCTalks(*IDSTR_GEN_RCC13, \"GEN_RCCc13.wav\");", randomInt(120,130) );   // pour on
    schedule("rebBTalks(*IDSTR_GEN_RCC15, \"GEN_RCCb15.wav\");", randomInt(125,145) );    // getting chewed
    schedule("rebATalks(*IDSTR_GEN_RCC12, \"GEN_RCCA12.wav\");", randomInt(130,140) );   // left left
    schedule("rebBTalks(*IDSTR_GEN_RCC11, \"GEN_RCCb11.wav\");", randomInt(135,145) );    // role right
    schedule("rebCTalks(*IDSTR_GEN_RCC14, \"GEN_RCCc14.wav\");", randomInt(150,170) );  // i'm holding
}

// ------------------------------------------------------------------------------

function caaFights()
{
    caaTalks(*IDSTR_GEN_CAN9, "GEN_CAA09.wav");                                           // watch fire
    schedule( "caaTalks(*IDSTR_GEN_CAN10, \"GEN_CAA10.wav\");", randomInt(10,50) );       // vape um
    schedule( "caaTalks(*IDSTR_GEN_CAN11, \"GEN_CAA11.wav\");", randomInt(30,60) );       // keep moving
    schedule( "caaTalks(*IDSTR_GEN_CAN3, \"GEN_CAA03.wav\");", randomInt(60,80) );        // panic easy
    schedule( "caaTalks(*IDSTR_GEN_CAN12, \"GEN_CAA12.wav\");", randomInt(95,98) );       // chain two
    schedule( "caaTalks(*IDSTR_GEN_CAN8, \"GEN_CAA08.wav\");", randomInt(30,90) );        // burn you
    schedule( "caaTalks(*IDSTR_GEN_CAN11, \"GEN_CAA11.wav\");", randomInt(110,130) );       // keep moving
    
    schedule( "impCTalks(*IDSTR_GEN_ICC4, \"GEN_ICCc04.wav\");", randomInt(5,15) );       // snap line
    schedule( "impATalks(*IDSTR_GEN_ICC6, \"GEN_ICCA06.wav\");", randomInt(10,25) );       // target lock
    schedule( "impCTalks(*IDSTR_GEN_IC15, \"GEN_ICCc15.wav\");", randomInt(20,35) );       // taking hits
    schedule( "impBTalks(*IDSTR_GEN_ICC8, \"GEN_ICCb08.wav\");", randomInt(35,50) );       // watch crossfire
    schedule( "impCTalks(*IDSTR_GEN_ICC9, \"GEN_ICCc09.wav\");", randomInt(40,60) );       // watch six
    schedule( "impATalks(*IDSTR_GEN_IC11, \"GEN_ICCA11.wav\");", randomInt(50,70) );       // spin pin
    schedule( "impBTalks(*IDSTR_GEN_ICC15, \"GEN_ICCb15.wav\");", randomInt(55,70) );      // taking hits
    schedule( "impATalks(*IDSTR_GEN_ICC14, \"GEN_ICCa14.wav\");", randomInt(80,90) );      // switch chains
    schedule( "impBTalks(*IDSTR_GEN_ICC10, \"GEN_ICCb10.wav\");", randomInt(100,103) );      // move intercept - with can12
    schedule( "impATalks(*IDSTR_GEN_ICC8, \"GEN_ICCA08.wav\");", randomInt(110,120) );       // watch crossfire
    schedule( "impCTalks(*IDSTR_GEN_ICC13, \"GEN_ICCc13.wav\");", randomInt(125,128) );     // concentrate fire
    schedule( "impATalks(*IDSTR_GEN_ICC5, \"GEN_ICCA05.wav\");", randomInt(130,133) );       // centering fire
    schedule( "impBTalks(*IDSTR_GEN_ICC12, \"GEN_ICCb12.wav\");", randomInt(140,150) );    // push right
}

// ------------------------------------------------------------------------------


function a::vehicle::onTargeted(%targeted, %targeter)
{
    if( %targeter == $thePlayer )
    {
        order( $a, cloak, true );
        schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot8\"));", randomInt(8,20) );
    }
}

// ------------------------------------------------------------------------------

function nearWrongZone()
{
    if( $nearAlpha == false )
    {
        warning();
    }        
}

// ------------------------------------------------------------------------------

function nearWrongZone2()
{
    if( $nearAlpha == false )
    {
        fail();
    }        
}

// ------------------------------------------------------------------------------

function inZoneA()
{
    hellBreaksLoose();
    $missionFlow = false;
}

// ------------------------------------------------------------------------------

function inZoneB()
{
    hellBreaksLoose();
    $missionFlow = false;
}
// ------------------------------------------------------------------------------

function inZoneC()
{
    hellBreaksLoose();
    $missionFlow = false;
}

// ------------------------------------------------------------------------------

function hellBreaksLoose()
{
    order( $turrets, shutdown, true );
    damageObject( getObjectId($upLink), 3000 );
    cStart();
    dStart();
    eStart();
    if( $flyersGone == false)
    {
        order($drones, shutdown, true );
    }
    if( $missionFlow == false )
    {
        schedule( "tacTalks(*IDSTR_HD1_WU01, \"HD1_wu01.wav\");", 5 );      ///where are you, duster?
        $missionFlow = true;
    }
}

// ------------------------------------------------------------------------------

function a::vehicle::onDestroyed()
{
    $aCount--;
    $aAttacked = true;
    cCheck();
    order( $a, holdPosition, false );
    if( isGroupDestroyed($a) )
    {
        $aAlive = false;
    }
}

function a::vehicle::onNewLeader( %newLeader )
{
    if( $aCount < 3 )
    {
        order( $a, Formation, wedge );
        order( %newLeader, guard, $aPath );
        order($a, holdfire, true);
        order( $a, speed, High );
        order( $a, holdPosition, true );
    }       
}

// ------------------------------------------------------------------------------

function b::vehicle::onDestroyed()
{
    $bCount--;
    $bAttacked = true;
    aAttack();
    cCheck();
}

function b::vehicle::onNewLeader( %newLeader )
{
    if( $bCount < 3 )
        order( %newLeader, attack, playerSquad );
}

// ------------------------------------------------------------------------------

function aAttack()
{
    %target = pick( $base, $utility, $shuttle, $fuel, $upLink );
    order( $a, attack, %target );
    order( $a, holdPosition, false );
    $aAttacking = true;    
}

// ------------------------------------------------------------------------------

function positionSet()
{
    %leftDistance = getDistance($thePlayer, getObjectId($leftRock));
    %rightDistance = getDistance($thePlayer, getObjectId($rightRock));
    if(%leftDistance > %rightDistance)
        $position = 0;
    else if( %rightDistance > %leftDistance )
        $position = 1;
}

// ------------------------------------------------------------------------------

function cCheck()
{
    positionSet();
    if( ($aAttacked == true) && ($bAttacked == true) )
    {
        if( (isSafe(*IDSTR_TEAM_YELLOW, getObjectId($navAlpha), 800 ) == true ) )
        {
            cStart();
            tacTalks( *IDSTR_HD1_WU03, "HD1_wu03.wav" );   // Green light for launch
        }
    }
}

// ------------------------------------------------------------------------------

function cStart()
{
    if( ($cStarted == false) && ($position == 0) )
    {
        setPosition( getObjectId("MissionGroup\\vehicles\\cybrids\\squadC\\c1"), 145,-2000,45 );
        $cStarted = true;
    }
    else if( ($cStarted == false) && ($position == 1) )
    {
        setPosition( getObjectId("MissionGroup\\vehicles\\cybrids\\squadC\\c1"), -617,-2068,63 );
        $cStarted = true;
    }       
    schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot7\"));", randomInt(10,25) );
    schedule( "dropPod(getObjectId(\"MissionGroup/podSpots/spot9\"));", 40 );
    order( $c, speed, high );
    order( $c1, attack, pick($base, $upLink, playerSquad));
}

// ------------------------------------------------------------------------------

function c::vehicle::onDestroyed(%destroyed, %destroyer)
{
    $cCount--;
    positionSet();
    
    $cAttacked = true;
    beamCheck();
}

function c::vehicle::onNewLeader( %newLeader )
{
    if( $cCount < 3 )
        order( %newLeader, attack, playerSquad );
}

// ------------------------------------------------------------------------------

function beamCheck()
{
    if( $cAttacked == true )
    {
        positionSet();
        if( (isSafe(*IDSTR_TEAM_YELLOW, getObjectId($navAlpha), 1500) == true) && ($launchStart == false) )
        {
            setHudTimer(10, -1, *IDSTR_TIMER_HD1_1, 1);
            tacTalks( *IDSTR_HD1_WU04, "HD1_WU04.wav" );    //T-minus 10 seconds to launch.
            schedule( "tacTalks(*IDSTR_HD1_WU05, \"HD1_WU05.wav\" );", 5 );     //  5...4...3...
            schedule( "launch();", 10.1 );
            $attachedTowerCam = false;
            $launchStart = true;    
        }
        else if( isSafe(*IDSTR_TEAM_YELLOW, getObjectId($navAlpha), 1500) == false )
            schedule( "beamCheck();", 5 );    
    }
}

// ------------------------------------------------------------------------------

function dStart()
{
    if( ($dStarted == false) && ($position == 0) )
    {     
        dropPod(-256,-1144,42, getObjectId($pod1));
        schedule( "dropPod(-376,-1438,39, getObjectId($pod2));", 0.5 );
        schedule( "dropPod(-38,-1180,33, getObjectId($pod3));", 1.5 );
        schedule("setPosition( getObjectId(\"MissionGroup/vehicles/cybrids/squadD/d2\"), -399,-1086,45 );", 3);
        $dStarted = true;
    }
    else if( ($dStarted == false) && ($position == 1) )
    {
        dropPod(-782,-1566,40, getObjectId($pod1));
        schedule( "dropPod(-956,-1525,46, getObjectId($pod2) );", 1 );
        schedule( "dropPod(-694,-1725,45, getObjectId($pod3));", 1.2 );
        schedule("setPosition( getObjectId(\"MissionGroup/vehicles/cybrids/squadD/d2\"), -782,-1380,45 );", 3);
        $dStarted = true;
    }        
    %target = pick($base, $utility, $shuttle, $fuel, $upLink, playerSquad );
    order( $d, speed, High );
    order( $d2, attack, %target);
    positionSet();
    schedule( "eStart();", 10 );
}

// ------------------------------------------------------------------------------

function d::vehicle::onDestroyed()
{
    $dCount--;
    order( $e, speed, high );
}

function d::vehicle::onNewLeader( %newLeader )
{
    if($dCount < 3 )
        order( $d, attack, playerSquad );   
}

// ------------------------------------------------------------------------------

function eStart()
{
    if( ($eStarted == false) && ($position == 1) )
    {
        setPosition( getObjectId("MissionGroup\\vehicles\\cybrids\\squadE\\e2"), 138,-629,55 );
        $eStarted = true;
    }
    else if( ($eStarted == false) && ($position == 0) )
    {
        setPosition( getObjectId("MissionGroup\\vehicles\\cybrids\\squadE\\e2"), -1423,-1312,115 );
        $eStarted = true;
    }        
    order( $e2, attack, $upLink );
    order( $e, speed, High );
}

// ------------------------------------------------------------------------------

function e::vehicle::onNewLeader( %newLeader )
{
    if($eCount < 3 )
    {
        order( $e, formation, wedge );
        order( %newLeader, attack, $upLink );
    }    
}

// ------------------------------------------------------------------------------

function launcher::structure::onDestroyed(%destroyed, %destroyer)
{
    if( $launched == true )
    {
        setShapeVisibility(getObjectId($beam), false );
    }
    missionObjective2.status = *IDSTR_OBJ_FAILED;
    forceToDebrief(*IDSTR_MISSION_FAILED);
}

// ------------------------------------------------------------------------------

function launch::structure::onDestroyed(%destroyed, %destroyer)
{
    $buildCount--;
    if( $buildCount <= 6 )
    {
        missionObjective3.status = *IDSTR_OBJ_FAILED;
        forceToDebrief(*IDSTR_MISSION_FAILED);
    }
}

// ------------------------------------------------------------------------------

function fly::vehicle::onDestroyed(%destroyed, %destroyer)
{
    $flyerCount--;
    if( %destroyed == $flyer1 )
        $fly1Alive = false;
        
    if( %destroyed == $flyer2 )
        $fly2Alive = false;    
    if( $flyerCount == 0 )
    {       
        missionObjective3.status = *IDSTR_OBJ_FAILED;
        missionObjective4.status = *IDSTR_OBJ_FAILED;
        forceToDebrief(*IDSTR_MISSION_FAILED);
    }   
}

// ------------------------------------------------------------------------------

function e::vehicle::onDestroyed(%destroyed, %destroyer)
{
    $eCount--;
    if( %destroyed == $e2 )
    {
        order($e, attack, playerSquad );
    }
    if( isSafe(*IDSTR_TEAM_YELLOW, "MissionGroup\\base\\DiesIrae\\UpLink", 4000 ) == true )
    {
        $uplinkSafe = true;
        missionObjective2.status = *IDSTR_OBJ_COMPLETED;
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        if($buildCount >= 7)
        {
            missionObjective3.status = *IDSTR_OBJ_COMPLETED;   
        } 
        youWin();   
    }
    if( ($eCount <= 0) && ($uplinkSafe == true) )
    {
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        tacTalks( "GEN_OC01.wav" );
        isUplinkSafe();
        youWin();
    }  
}

// ------------------------------------------------------------------------------

function bridCheck()
{
    if( (isGroupDestroyed($a)) && (isGroupDestroyed($b)) && (isGroupDestroyed($c)) &&
         (isGroupDestroyed($d)) &&  (isGroupDestroyed($e)) )
    {
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        missionObjective2.status = *IDSTR_OBJ_COMPLETED;
        tacTalks( "GEN_OC01.wav" );
        if($buildCount >= 7)
        {
            missionObjective3.status = *IDSTR_OBJ_COMPLETED;   
        }
        if( (missionObjective4.status == *IDSTR_OBJ_COMPLETED) && ($helpCall < 1) )
        {
            missionObjective5.status = *IDSTR_OBJ_COMPLETED;
            InventoryWeaponAdjust(		-1,	112,	2	);	#Qgun
            schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 3.0);   
        }
    }
}

// ------------------------------------------------------------------------------

function launch()
{
    if( $attachedTowerCam == false )
    {
        setTowerCamera(getObjectId($upLink), -717,485,89, 20,0,70);
        dStart();
        schedule( "playAnimSequence(getObjectId($upLink), true);", 2 );
        schedule( "playAnimSequence(getObjectId($beam), true);", 4 );
        schedule( "tacTalks(\"\", \"sfx_bigbeam_fire.WAV\");", 2 );
        schedule( "order($d, holdFire, false);", 9.2 );
        schedule( "setPlayerCamera();", 9 );
        order( $fly1, shutdown, false );   
        schedule( "order( $fly1, guard, $flyRoute );",0.2 ); 
        $fly1Gone = true;
        missionObjective4.status = *IDSTR_OBJ_COMPLETED;
        schedule( "say(0, 1, \"GEN_OC01.wav\" );", 4.5 );
        $launched = true; 
        $attachedTowerCam = true;
    }   
}        

// ------------------------------------------------------------------------------        

function youWin()
{
    
    if( (missionObjective1.status == *IDSTR_OBJ_COMPLETED) &&
        (missionObjective2.status == *IDSTR_OBJ_COMPLETED) &&
        (missionObjective3.status == *IDSTR_OBJ_COMPLETED) &&
        (missionObjective4.status == *IDSTR_OBJ_COMPLETED) ) 
    {
        updatePlanetInventory(hd1);
        killChannel(4);
        killChannel(5);
        killChannel(6);
        killChannel(7);
        killChannel(8);
        killChannel(9);
        if( $helpCall == 0 )
        {
            missionObjective5.status = *IDSTR_OBJ_COMPLETED;   
        }
        schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 3.0);
    }        
}

// ------------------------------------------------------------------------------

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    missionObjective4.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(hd1);
    schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 3.0);   
}

// ------------------------------------------------------------------------------

function warning()
{
    if( $warning == false)
    {
        tacTalks( *IDSTR_GEN_TCM01, "GEN_TCM01.wav" );  // Off course
        $warning = true;
        secondChance();
    }        
}

// ------------------------------------------------------------------------------

function secondChance()
{
    checkBoundary( enter, $thePlayer, getObjectId($upLink), 800, onEnter );
    checkBoundary( enter, $thePlayer, getObjectId($navAlpha), 1000, onEnter );
}

// ------------------------------------------------------------------------------

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
        tacTalks( *IDSTR_GEN_TCM02, "GEN_TCM02.wav" );    // mission failed
        forceToDebrief("Mission scrubbed");
        $fail = true;
    }    
}

// ------------------------------------------------------------------------------

function tacTalks(%text, %wav)
{
    if($playerAlive)
        say( 0, 1, %text, %wav );
    else 
    {
        say(0, 1, "");
    } 
}

// ------------------------------------------------------------------------------

function harTalks(%text, %wav)
{
    if( ($playerAlive) && ($harAlive) )
        say( 0, 2, %text, %wav );
    else 
    {
        say(0, 2, "");
    } 
}

// ------------------------------------------------------------------------------

function caaTalks(%text, %wav)
{
    if( ($playerAlive) && ($caaAlive) )
        say( 0, 3, %text, %wav );
    else 
    {
        say(0, 3, "");
    } 
}

// ------------------------------------------------------------------------------

function rebATalks(%text, %wav)
{
    %num = randomInt(1,4);
    if( %num != 4 )
    {
        say( 0, 4, %text, %wav );
    }
    else
    {
        say( 0, 4, %text, %wav );
        schedule( "killChannel(4);", 1 );
        schedule( "say( 0, 44, \"SM_HMan_DC1.WAV\");", 1 );
        schedule( "say( 0, 45, \"GEN_STATIC01.WAV\");", 1.5 );
    }
}

// ------------------------------------------------------------------------------

function rebBTalks(%text, %wav)
{
    %num = randomInt(1,5);
    if( %num != 5 )
    {
        say( 0, 5, %text, %wav );
    }
    else
    {
        say( 0, 5, %text, %wav );
        schedule( "killChannel(5);", 1 );
        schedule( "say( 0, 55, \"SM_HF2_DC1.WAV\");", 1 );
        schedule( "say( 0, 56, \"GEN_STATIC01.WAV\");", 1.5 );
    } 
}

// ------------------------------------------------------------------------------

function rebCTalks(%text, %wav)
{
    %num = randomInt(1,5);
    if( %num != 5 )
    {
        say( 0, 6, %text, %wav );
    }
    else
    {
        say( 0, 6, %text, %wav );
        schedule( "killChannel(6);", 1 );
        schedule( "say( 0, 66, \"SM_Riana_DC1.WAV\");", 1 );
        schedule( "say( 0, 67, \"GEN_STATIC01.WAV\");", 1.5 );
    } 
}

// ------------------------------------------------------------------------------

function impATalks(%text, %wav)
{
    %num = randomInt(1,5);
    if( %num != 5 )
    {
        say( 0, 7, %text, %wav );
    }
    else
    {
        say( 0, 7, %text, %wav );
        schedule( "killChannel(7);", 1 );
        schedule( "say( 0, 77, \"SM_Hunter_DC1.WAV\");", 1 );
        schedule( "say( 0, 78, \"GEN_STATIC01.WAV\");", 1.5 );
    } 
}

// ------------------------------------------------------------------------------

function impBTalks(%text, %wav)
{
    %num = randomInt(1,5);
    if( %num != 5 )
    {
        say( 0, 8, %text, %wav );
    }
    else
    {
        say( 0, 8, %text, %wav );
        schedule( "killChannel(8);", 1 );
        schedule( "say( 0, 88, \"SM_Jaguar_DC1.WAV\");", 1 );
        schedule( "say( 0, 89, \"GEN_STATIC01.WAV\");", 1.5 );
    } 
}

// ------------------------------------------------------------------------------

function impCTalks(%text, %wav)
{
    %num = randomInt(1,5);
    if( %num != 5 )
    {
        say( 0, 9, %text, %wav );
    }
    else
    {
        say( 0, 9, %text, %wav );
        schedule( "killChannel(9);", 1 );
        schedule( "say( 0, 99, \"SM_Verity_DC1.WAV\");", 1 );
        schedule( "say( 0, 90, \"GEN_STATIC01.WAV\");", 1.5 );
    } 
}



// ----------------------------------- END --------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------



