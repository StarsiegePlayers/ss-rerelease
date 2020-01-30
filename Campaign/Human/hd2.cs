// Script for HD2

MissionBriefInfo missionBriefInfo
{
    campaign                = *IDSTR_HD2_CAMPAIGN;
    title                   = *IDSTR_HD2_TITLE;
    planet                  = *IDSTR_PLANET_TITAN;
    location                = *IDSTR_HD2_LOCATION;
    dateOnMissionEnd        = *IDSTR_HD2_DATE;

    shortDesc               = *IDSTR_HD2_SHORTBRIEF;
    longDescRichText        = *IDSTR_HD2_LONGBRIEF;        

    media                   = *IDSTR_HD2_MEDIA;

    successDescRichText     = *IDSTR_HD2_DEBRIEF_SUCC;
    failDescRichText        = *IDSTR_HD2_DEBRIEF_FAIL;
    successWavFile          = "HD2_debriefing.wav";

    nextMission             = *IDSTR_HD2_NEXTMISSION;
    soundVol                = "HD2.vol";
    endCinematicSmk         = "cin_HE.smk"; 
    endCinematicRec         = "cinHE.rec"; 
};
 
 // OBJECTIVES --------------------------------------------------------------------

MissionBriefObjective missionObjective1  //Meet NTDF at NavAlpha
{
    isPrimary       = true;
    status          = *IDSTR_OBJ_ACTIVE;
    shortTxt        = *IDSTR_HD2_OBJ1_SHORT;
    longTxt         = *IDSTR_HD2_OBJ1_LONG;
    bmpName         = *IDSTR_HD2_OBJ1_BMPNAME;    
};

MissionBriefObjective missionObjective2  //Destroy ComTower
{
    isPrimary       = true;
    status          = *IDSTR_OBJ_ACTIVE;
    shortTxt        = *IDSTR_HD2_OBJ2_SHORT;
    longTxt         = *IDSTR_HD2_OBJ2_LONG;
    bmpName         = *IDSTR_HD2_OBJ2_BMPNAME;  
};

MissionBriefObjective missionObjective3    //Use Transmiter on Transport
{
    isPrimary       = true;
    status          = *IDSTR_OBJ_ACTIVE;
    shortTxt        = *IDSTR_HD2_OBJ3_SHORT;
    longTxt         = *IDSTR_HD2_OBJ3_LONG;
    bmpName         = *IDSTR_HD2_OBJ3_BMPNAME; 
};

MissionBriefObjective missionObjective4    //Destroy All Cybrids
{
    isPrimary       = false;
    status          = *IDSTR_OBJ_ACTIVE;
    shortTxt        = *IDSTR_HD2_OBJ4_SHORT;
    longTxt         = *IDSTR_HD2_OBJ4_LONG;
    bmpName         = *IDSTR_HD2_OBJ4_BMPNAME; 
};

MissionBriefObjective missionObjective5    //Defend Transport
{
    isPrimary       = true;
    status          = *IDSTR_OBJ_IGNORE;
    shortTxt        = *IDSTR_HD2_OBJ5_SHORT;
    longTxt         = *IDSTR_HD2_OBJ5_LONG;
    bmpName         = *IDSTR_HD2_OBJ5_BMPNAME; 
};


//Custom Pilots
Pilot greatActivation
{
   id = 25;
   
   skill = 0.6;
   accuracy = 0.6;
   aggressiveness = 0.6;
   activateDist = 800.0;
   deactivateBuff = 500.0;
   targetFreq = 1.6;
   trackFreq = 0.6;
   fireFreq = 2.2;
   LOSFreq = 0.8;
   orderFreq = 6.0;
};

Pilot excellentActivation
{
   id = 26;
   
   skill = 0.6;
   accuracy = 0.6;
   aggressiveness = 0.6;
   activateDist = 1500.0;
   deactivateBuff = 500.0;
   targetFreq = 1.6;
   trackFreq = 0.6;
   fireFreq = 2.2;
   LOSFreq = 0.8;
   orderFreq = 6.0;
};

Pilot cassell
{
   id = 27;
   
   name = "Sgt.Cassell";
   skill = 0.4;
   accuracy = 0.2;
   aggressiveness = 0.1;
   activateDist = 100.0;
   deactivateBuff = 200.0;
   targetFreq = 0.6;
   trackFreq = 0.6;
   fireFreq = 0.2;
   LOSFreq = 0.8;
   orderFreq = 1.0;
};


//Map config
$server::HudMapViewOffsetX =  0;
$server::HudMapViewOffsetY =  -1000;

function onMissionStart()
{
    //flags
    $cybridCDestroyed       = false;
    $met                    = false;
    $transmitter            = false;
    $bolosAttack            = false; 
    $cybridAttacked         = false;
    $comAttacked            = false;
    $bound1                 = false;
    $detect1                = false;
    $detect2                = false;
    $NearTransport          = false;
    
    //formations
    newFormation( Line,   0,0,0,   0,-40,0,  0,-80,0, 0,-120,0 );
    newFormation( Wedge,  0,0,0, -20,-20,0, 20,-20,0, 0, -20,0 );
    
    cdAudioCycle(Yougot, Cyberntx);
    clearGeneralOrders();
    titanSounds();
    meteorSounds();
}
function onSPClientInit()
{
    initActors();
}

function player::onAdd (%this)
{
    $playerNum = %this;
}

function vehicle::onAdd(%this)
{
    %num = playerManager::vehicleIdToPlayerNum(%this);
    if(%num == $playerNum) 
    {
        $playerId = playerManager::playerNumToVehicleId($playerNum);
    }
}

function initActors()
{
    initCybridA();
    initCybridB();
    initCybridC();
    initNTDF();
    boundCheck();
    setNavMarker(myGetObjectId("MissionGroup/NAV/NavAlpha"), true, -1);
    schedule("order(\"MissionGroup/ImpDropShip\", Guard, \"MissionGroup/ImpShipWaypoint\" );", 2);
    addGeneralOrder( *IDSTR_ORDER_HD2_1 , "useTransmitter();" );
    setHercOwner(playerSquad, "MissionGroup/CybridTransport");
    cybridBaseSounds(myGetObjectId("MissionGroup/CybridLandingZone/ComTower"),400);
}

function initCybridA()
{
    order(myGetObjectId("MissionGroup/CybridA/Bolo1"),MakeLeader, true);
    order("MissionGroup/CybridA", Formation, Wedge);
    order("MissionGroup/CybridA/Bolo1", Guard, "MissionGroup/PatrolRouteA");
}

function initCybridB()
{
    order("MissionGroup/CybridB/Ex1",MakeLeader, true);
}

function initCybridC()
{
    order("MissionGroup/CybridC/Shep1",MakeLeader, true);
    order("MissionGroup/CybridC", Formation, Wedge);
}

function initNTDF()
{
    damageObject("MissionGroup/NTDF/SgtCassell", 8000);
    order("MissionGroup/NTDF/SgtCassell", HoldPosition, true);
    order("MissionGroup/NTDF/SgtCassell", HoldFire, true);
    order("MissionGroup/NTDF/SgtCassell", ShutDown, true);
}


function meetNTDF()
{
    if(isGroupDestroyed("MissionGroup/NTDF/SgtCassell")){ return;}
    $met = true;
    //start dialog
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_ACTIVE;
    setNavMarker("MissionGroup/NAV/NavAlpha", false);
    schedule("setNavMarker(\"MissionGroup/NAV/NavBravo\", true, -1);",2);
    //setDominantCamera("MissionGroup/NTDF/SgtCassell", $playerId);
    //schedule("setPlayerCamera();", 3);
    NTDFSpeaks(IDSTR_HD2_NTD1,1);
    NTDFSpeaks(IDSTR_HD2_NTD2,2);
    NTDFSpeaks(IDSTR_HD2_NTD3,3);
    order("MissionGroup/NTDF", ShutDown, false);
    schedule("order(\"MissionGroup/NTDF/SgtCassell\", Guard, \"MissionGroup/NTDFMove\");", 24);
}


//====Boundry Checking===================
function boundCheck()
{
    %dist = getDistance(myGetObjectId("MissionGroup/Center"), $playerId);
    //say(0,1,"Debug: dist from center: "@ %dist);
    if (%dist > 4500)
    {
        TacComSpeaks(IDSTR_GEN_TCM2, GEN_TCV02);
        forceToDebrief();
    }
    if (%dist > 4000)
    {
        if(!($bound1))
        {
            $bound1 = true;
            schedule("$bound1 = false;",60);
            TacComSpeaks(IDSTR_GEN_TCM1, GEN_TCV01);
        }
    }
    schedule("boundCheck();", 10);
    
    //mission driven distance checks
    %dist2 = getDistance("MissionGroup/NTDF/SgtCassell", $playerId);
    if(%dist2 < 300 && %dist2 != 0 && !($met))
    { meetNTDF(); }
    
    //Motion Detector 1
    %dist3 = getDistance("MissionGroup/MotionDetectors/Motion2", $playerId );
    if( %dist3 < 800 && %dist3 != 0 && (!($detect1)))
    {
        $detect1 = true;
        DropIn1();
    } 
    //Motion Detector 2
    %dist4 = getDistance("MissionGroup/MotionDetectors/Motion3", $playerId );
    if( %dist4 < 800 && %dist4 != 0 && (!($detect2)))
    {
        $detect2 = true;
        DropIn2();
    }
    
    //countdown
    %dist5 = getDistance("MissionGroup/CybridTransport", $playerId );
    if( %dist5 < 800 && %dist5 != 0 && (!($NearTransport)))
    {
        $NearTransport = true;
        countdown();
    }
}


//====MissionFunctions====================
function AtCassell::trigger::onEnter(%this, %who)
{
    if(%who == $playerId && (!($met)))
    {meetNTDF();}
}

function vehicle::onDestroyed()
{
}

function useTransmitter()
{
    if( getDistance($playerId, myGetObjectId("MissionGroup/CybridTransport") ) <= 250 && (!($transmitter)))
    {
        setVehicleSpecialIdentity(myGetObjectId("MissionGroup/CybridTransport"), true, yellow);
        setTeam("MissionGroup/CybridTransport", *IDSTR_TEAM_YELLOW);
        //order("MissionGroup/CybridTransport", Guard, myGetObjectId("MissionGroup/ImpShipWayPoint") );
        $transmitter = true;
        setHudTimer(-1,1,"",1);
        removeGeneralOrder( *IDSTR_ORDER_HD2_1 );
        missionObjective3.status = *IDSTR_OBJ_IGNORE;
        missionObjective5.status = *IDSTR_OBJ_ACTIVE;
        say(0,1,"Mission_obj_new.WAV");
        defendTransport();
    }
    else{ say(0,1, *IDSTR_SCAN_OUT_OF_RANGE); }
}

function Cybrid::vehicle::onAttacked(%attacked, %attacker)
{
    if($cybridAttacked){return;}
    $cybridAttacked = true;
    //say(0,1,"Debug::CybridsAttacked");
    if(!(isGroupDestroyed("MissionGroup/CybridLandingZone/ComTower") && (!($bolosAttack))))
    {
        $bolosAttack = true; 
        order("MissionGroup/CybridA/Bolo1", attack, pick(playerSquad));
        //say(0,1,"Debug: Bolos attack players Squad");
    }
}

function Cybrid::vehicle::onDestroyed(%destroyed, %destroyer)
{
    if( isGroupDestroyed("MissionGroup/CybridD") &&
        missionObjective5.status == *IDSTR_OBJ_ACTIVE )
    {
        missionObjective5.status = *IDSTR_OBJ_COMPLETED;
        schedule("victory();",2);
    }
    else if( isGroupDestroyed("MissionGroup/CybridA") &&
        isGroupDestroyed("MissionGroup/CybridB") &&
        isGroupDestroyed("MissionGroup/CybridC") &&
        isGroupDestroyed("MissionGroup/CybridD") )
    {
        missionObjective4.status = *IDSTR_OBJ_COMPLETED;
        InventoryWeaponAdjust( -1, 130, 2);#Shrike8
        InventoryComponentAdjust( -1, 230, 1);# LargeCybridReactor2--theta
    }
}

function ComTower::structure::onAttacked(%attacked, %attacker)
{
    if($comAttacked){return;}
    $comAttacked = true;
    //Play Anmiation Sequence for structure
    playAnimSequence("MissionGroup/CybridLandingZone/ComTower", 0, false);
    order("MissionGroup/CybridB", Attack, %attacker);
}

function ComTower::structure::onDestroyed(%destroyed, %destroyer)
{
    say(0,1,"GEN_OC01.wav");
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    setNavMarker("MissionGroup/NAV/NavBravo", false);
    schedule("setNavMarker(\"MissionGroup/NAV/NavCharlie\", true, -1);",2);
}

function CybridTransport::vehicle::onDestroyed(%destroyed, %destroyer)
{
    setDominantCamera(%destroyer,%destroyed);
    if(missionObjective3.status == *IDSTR_OBJ_ACTIVE)
        {missionObjective3.status = *IDSTR_OBJ_FAILED;}
    else{missionObjective5.status = *IDSTR_OBJ_FAILED;}
    forceToDebrief();
}

function CybridTransport::vehicle::onArrived(%who, %where)
{
    if(!($transmitter))
    {
        setOrbitCamera(%who);
        missionObjective3.status = *IDSTR_OBJ_FAILED;
        forceToDebrief();
    }
}

function NTDF::vehicle::onDestroyed(%destroyed, %destroyer)
{
    killChannel(200);
    if(getTeam(%destroyer) == getTeam($playerId) )
    {
        missionObjective1.status = *IDSTR_OBJ_FAILED;
        forceToDebrief();
    }
}

function Detector1::structure::onAttacked(%attacked, %attacker)
{
    if(!($detect1)){
        $detect1 = true;
        DropIn1();
    }
}

function Detector2::structure::onAttacked(%attacked, %attacker)
{
    if(!($detect2)){
        $detect2 = true;
        DropIn1();
    }
}

function dropIn1()
{
    order("MissionGroup/CybridE", Attack, $playerId);
    dropPod("MissionGroup/DropPoints/M1Spot","MissionGroup/CybridE/Shep3");
    schedule("dropPod(\"MissionGroup/DropPoints/M2Spot\",\"MissionGroup/CybridE/Shep4\");", 1);
    schedule("dropPod(\"MissionGroup/DropPoints/M12Xspot\",\"MissionGroup/CybridE/Shep5\");", 2);
}

function dropIn2()
{
    order("MissionGroup/CybridF", Attack, $playerId);
    dropPod("MissionGroup/DropPoints/M3Spot","MissionGroup/CybridE/Shep6");
    schedule("dropPod(\"MissionGroup/DropPoints/M4Spot\",\"MissionGroup/CybridE/Shep7\");", 1);
    schedule("dropPod(\"MissionGroup/DropPoints/M34Xspot\",\"MissionGroup/CybridE/Shep8\");", 2);
}

function countdown()
{
    say(0,1,*IDSTR_HD2_WU03,"Hd2_wu03.wav");
    setHudTimer( 30, -1, *IDSTR_TIMER_HD2_1, 1);
    schedule("transportLeaves();", 30 + randomInt(1,10) );
}

function transportLeaves()
{
    if(!($transmitter))
    {
        TacComSpeaks(IDSTR_HD2_TCV1, HD2_WU01);
        order("MissionGroup/CybridTransport", Guard, "MissionGroup/TransportEscapeSpot");
        setDominantCamera("MissionGroup/CybridTransport", $playerId, 0, -100, 25);
        schedule("forceToDebrief();",5);
    }
}

function defendTransport()
{
    say(0,1,*IDSTR_GEN_WU04,"GEN_WU04.wav");
    order("MissionGroup/CybridD/Adjud2", Attack, "MissionGroup/CybridTransport");
    schedule("dropPod(\"MissionGroup/DropPoints/Spot1\",\"MissionGroup/CybridD/Adjud2\"); ",5);
    
    order("MissionGroup/CybridD/Adjud3", Attack, "MissionGroup/CybridTransport");
    schedule("dropPod(\"MissionGroup/DropPoints/Spot2\",\"MissionGroup/CybridD/Adjud3\"); ",7);
}

function victory()
{
    if( missionObjStatus() == ALL_COMPLETED )
    {
        if( isSafe(*IDSTR_TEAM_YELLOW, $playerId, 400) )
        {
            TacComSpeaks(IDSTR_HD2_TCV2, HD2_WU02);
            schedule("forceToDebrief();",4);
            updatePlanetInventory(hd2);
        }
        else say(0,1,"Debug::not safe!");
    }
    else schedule( "victory();", 15 );
}
//==Misc==================================

function myGetObjectId( %this )
{
    %id = getObjectId( %this );
    if ( %id == "" ){
        //say(0, 1 ,"Debug: Problem with Id for " @ %this );
        }
    else return %id;
}

//  Note: tacCom needs full wav others only need misson wav
function TacComSpeaks(%line, %wav)
{
    say(0, 1, *(%line), %wav@".wav");
}
function NTDFSpeaks(%line, %wav)
{
    if(isGroupDestroyed("MissionGroup/NTDF/SgtCassell")){killChannel(200);}
    say(0,200, *(%line), "HD2_NTD0"@%wav@".wav");
}
function cybridSpeaks(%line, %wav)
{
    say(0, 100,*(%line), "GEN_CYB0"@%wav@".wav");
}

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    missionObjective4.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(hd2);
    forceToDebrief();
}
