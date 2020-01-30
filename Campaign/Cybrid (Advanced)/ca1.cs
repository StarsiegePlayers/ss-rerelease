//Mission Script for CA1: Sear//Strip//Sever


//Briefing info
//--------------------------------------------------------------------

MissionBriefInfo CA1
{
    title =                 *IDSTR_CA1_TITLE;
    planet =                *IDSTR_PLANET_MERCURY;           
    campaign =              *IDSTR_CA1_CAMPAIGN;   
    dateOnMissionEnd =      *IDSTR_CA1_DATE;  
    shortDesc =             *IDSTR_CA1_SHORTBRIEF;   
    longDescRichText =      *IDSTR_CA1_LONGBRIEF;   
    media =                 *IDSTR_CA1_MEDIA;
    nextMission =           *IDSTR_CA1_NEXTMISSION;
    successDescRichText =   *IDSTR_CA1_DEBRIEF_SUCC;
    failDescRichText =      *IDSTR_CA1_DEBRIEF_FAIL;
    location =              *IDSTR_CA1_LOCATION;
    soundvol =              "CA1.vol";
    successWavFile =        "CA1_debriefing.wav";
};

// Primary and Secondary Mission Objectives
//-----------------------------------------------------------------------

MissionBriefObjective missionObjective1        //destroy crates
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_ACTIVE;
   shortTxt     = *IDSTR_CA1_OBJ1_SHORT;
   longTxt      = *IDSTR_CA1_OBJ1_LONG;
   bmpName      = *IDSTR_CA1_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2         //clear human landing pad for 
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_IGNORE;
   shortTxt     = *IDSTR_CA1_OBJ2_SHORT;
   longTxt      = *IDSTR_CA1_OBJ2_LONG;
   bmpName      = *IDSTR_CA1_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3         //hook up with nexus
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_ACTIVE;
   shortTxt     = *IDSTR_CA1_OBJ3_SHORT;
   longTxt      = *IDSTR_CA1_OBJ3_LONG;
   bmpName      = *IDSTR_CA1_OBJ3_BMPNAME;
};

//Custom Pilots

Pilot excellentActivation
{
   id = 26;
   
   skill = 0.6;
   accuracy = 0.6;
   aggressiveness = 0.6;
   activateDist = 1000.0;
   deactivateBuff = 200.0;
   targetFreq = 1.6;
   trackFreq = 0.6;
   fireFreq = 2.2;
   LOSFreq = 0.8;
   orderFreq = 6.0;
};

//Map config
$server::HudMapViewOffsetX =  2000;
$server::HudMapViewOffsetY =  1000;

//-----------------------------------------------------------------------------
function onMissionStart()
{
    initVariables();
    initFormations();
    clearGeneralOrders();
    mercurySounds();
    earthquakeSounds();
    cdAudioCycle(Cyberntx, Cloudburst, Yougot);
}
function onSPClientInit()
{
    initActors();
}
//----------------------------------------------------------------------------
function initVariables()
{
    $bound1                     = false;
    $transportSpeaks            = false;
    $impAttacked                = false;
    $armoryLine                 = false;
    $nexusHitByPlayer           = 0;
}

function player::onAdd(%this)
{
    $playerNum = %this;
}

//get playerVehicleId (playerId) as global var from playerNum
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
    boundCheck();
    
    setNavMarker("MissionGroup/NAV/Nav001a", true, -1);
    
    order("MissionGroup/CargoShips/Cargo1", height, 300, 500);
    order("MissionGroup/CargoShips/Cargo2", height, 300, 500);
    
    order("MissionGroup/CargoShips/Cargo1", Guard, "MissionGroup/Away");   
    order("MissionGroup/CargoShips/Cargo2", Guard, "MissionGroup/LandingPad");
    
    schedule("say(0,1,\"GEN_RED06.wav\");", randomInt(30, 240));
    schedule("say(0,1,\"GEN_RED08.wav\");", randomInt(30, 240));
}

//
//===Distance Checks==================================================================
function boundCheck()
{
    if($missionEnd){ return; }
    %dist = getDistance(myGetObjectId("MissionGroup/Center"), $playerId);
    if (%dist > 4000)
    {
        say(0,1, *IDSTR_CYB_NEX02, "CYB_NEX02.wav");
        forceToDebrief();
        $missionEnd = true;
    }
    else if (%dist > 3500)
    {
        if(!($bound1))
        {
            $bound1 = true;
            schedule("$bound1 = false;",60);
            say(0,1, *IDSTR_CYB_NEX01, "CYB_NEX01.wav");
        }
    }
    
    schedule("boundCheck();", 15);
    
    //extra mission driven checks
    
    if(getDistance("MissionGroup/ExteriorTurrets/CenterTurret", "MissionGroup/Imps/Imp1") <2000 )
    {
        order("MissionGroup/Imps/Imp1", HoldPosition, true);
        schedule("order(\"MissionGroup/Imps/Imp1\", HoldPosition, false);", 10);
        order("MissionGroup/Imps/Imp1", Guard, "MissionGroup/Base/Power");
    }
    
    if( getDistance( $playerId, myGetObjectId( "MissionGroup/Nexus" ) ) < 300 )
    {
        //say(0,1,"Debug::Close enough for pickup.");
        if( missionObjective1.status == *IDSTR_OBJ_COMPLETED )    
        {
            missionObjective3.status = *IDSTR_OBJ_COMPLETED;
            forceToDebrief();
            updatePlanetInventory(CA1);
        }
        //else say(0,1,"Debug:: but an objective remains");
    }
}                        


//Mission functions===================================================================

function Power::structure::onDestroyed()
{
    order("MissionGroup/Enterance/Turrets", ShutDown, true);
    setStaticShapeShortName(myGetObjectId("MissionGroup/Enterance/Turrets/T1"), *IDACS_C_NOPOWERTURRET);
    setStaticShapeShortName(myGetObjectId("MissionGroup/Enterance/Turrets/T2"), *IDACS_C_NOPOWERTURRET);
    setStaticShapeShortName(myGetObjectId("MissionGroup/Enterance/Turrets/T3"), *IDACS_C_NOPOWERTURRET);
    setStaticShapeShortName(myGetObjectId("MissionGroup/Enterance/Turrets/T4"), *IDACS_C_NOPOWERTURRET);
    schedule("say(0,1,\"CYB_GN52.wav\");", 2);
    schedule("say(0,1,\"CYB_GN57.wav\");", 7);
}

function Gate::structure::onDestroyed()
{
    setNavMarker("MissionGroup/NAV/Nav001a", false);   
    setNavMarker("MissionGroup/NAV/Nav001b", true, -1);
    setNavMarker("MissionGroup/NAV/Nav001c", true);
}

function structure::onDestroyed()
{
    if(isGroupDestroyed("MissionGroup/DestroyObjective") &&
       missionObjective1.status != *IDSTR_OBJ_COMPLETED )
    {
        say(0,1, *IDSTR_CYB_NEX04, "CYB_NEX04.wav");
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        missionObjective3.status = *IDSTR_OBJ_ACTIVE;
        setNavMarker("MissionGroup/NAV/Nav002", true, -1);
    }
    else if(isGroupDestroyed("MissionGroup/DestroyObjective/Cache2") )
    {
        setNavMarker("MissionGroup/NAV/Nav001c", false);   
    }
    else if(isGroupDestroyed("MissionGroup/DestroyObjective/Cache1") )
    {
        setNavMarker("MissionGroup/NAV/Nav001b", false);
        if(!($armoryLine))
        {
            $armoryLine = true;
            schedule("say(0,1,\"CYB_ve01.wav\");", 4);
        }
    }
}

function Imp::vehicle::onDestroyed()
{
    say(0,1,"GEN_DTH06.wav");
}

function Imp::vehicle::onAttacked()
{
    if(!($impAttacked))
    {
        $impAttacked = true;
        schedule("say(0,1,\"CYB_gn01.wav\");",randomInt(1,4));
    }
}

function Cargo::vehicle::onArrived(%who, %where)
{
    //say(0,1,%who@" arrived at "@%where);
    order(%who, ShutDown, true);
    schedule("order("@%who@", ShutDown, true);",28);
    if(%where == myGetObjectId("MissionGroup/Away"))
    {schedule("order("@%who@", Guard, \"MissionGroup/LandingPad\");", 30);} 
    else{ schedule("transportLeaves(" @ %who @ ");", 30); }
}

function transportLeaves(%who)
{
    order(%who, Guard, "MissionGroup/Away");
    if( (!($transportSpeaks))                 &&
        (!(isGroupDestroyed(%who)))           &&
        (getDistance(%who, $playerId) < 500 )  )
        {
            $transportSpeaks = true;
            say(0,1,"GEN_1DSA02.wav");  // This place crawls, I'm Gone
        }
}

function Nexus::vehicle::onDestroyed()
{
    if( missionObjective3.status = *IDSTR_OBJ_ACTIVE )
        {missionObjective3.status = *IDSTR_OBJ_FAILED;}
    forceToDebrief();
}

function Nexus::vehicle::onAttacked(%attacked, %attacker)
{
    if(%attacker == $playerId)
    {
        $nexusHitByPlayer++;
        if( $nexusHitByPlayer == 13 )
        {
            if( missionObjective3.status = *IDSTR_OBJ_ACTIVE )
            {missionObjective3.status = *IDSTR_OBJ_FAILED;}
            forceToDebrief();
        }
    }
}

function Cargo::vehicle::onDisabled(%who)
{
    damageArea(%who, randomInt(-10,10), randomInt(-10,10), randomInt(-10,10), 30, 10000);
}

function Exterior::turret::onDestroyed(%destroyed, %destroyer)
{
    if( isGroupDestroyed("MissionGroup/ExteriorTurrets") ) 
    {
    }
}

//Misc functions
function myGetObjectId(%this)
{
    %id = getObjectId(%this);
    if (%id == "")
    {
        //say(0,1,"Problem with id for " @ %this);
    }
    else
    {return %id;}    
}

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(CA1);
    forceToDebrief();
}







