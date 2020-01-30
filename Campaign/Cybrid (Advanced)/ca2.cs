//Mission Script for CA2: Silence//Deafen


//Briefing info
//--------------------------------------------------------------------

MissionBriefInfo CA2
{
    title =                 *IDSTR_CA2_TITLE;
    planet =                *IDSTR_PLANET_MERCURY;           
    campaign =              *IDSTR_CA2_CAMPAIGN;   
    dateOnMissionEnd =      *IDSTR_CA2_DATE;  
    shortDesc =             *IDSTR_CA2_SHORTBRIEF;   
    longDescRichText =      *IDSTR_CA2_LONGBRIEF;   
    media =                 *IDSTR_CA2_MEDIA;
    nextMission =           *IDSTR_CA2_NEXTMISSION;
    successDescRichText =   *IDSTR_CA2_DEBRIEF_SUCC;
    failDescRichText =      *IDSTR_CA2_DEBRIEF_FAIL;
    location =              *IDSTR_CA2_LOCATION;
    soundvol =              "CA2.vol";
    successWavFile =        "CA2_debriefing.wav";
};

// Primary and Secondary Mission Objectives
//-----------------------------------------------------------------------

MissionBriefObjective missionObjective1  //Nav1-Destroy comm uplinks
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_ACTIVE;
   shortTxt     = *IDSTR_CA2_OBJ1_SHORT;
   longTxt      = *IDSTR_CA2_OBJ1_LONG;
   bmpName      = *IDSTR_CA2_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2   //Nav2-Destroy listening post
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_ACTIVE;
   shortTxt     = *IDSTR_CA2_OBJ2_SHORT;
   longTxt      = *IDSTR_CA2_OBJ2_LONG;
   bmpName      = *IDSTR_CA2_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3    //Await further orders
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_ACTIVE;
   shortTxt     = *IDSTR_CA2_OBJ3_SHORT;
   longTxt      = *IDSTR_CA2_OBJ3_LONG;
   bmpName      = *IDSTR_CA2_OBJ3_BMPNAME;
};

MissionBriefObjective missionObjective4     //Proceed to Nav 3
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_IGNORE;
   shortTxt     = *IDSTR_CA2_OBJ4_SHORT;
   longTxt      = *IDSTR_CA2_OBJ4_LONG;
   bmpName      = *IDSTR_CA2_OBJ4_BMPNAME;
};

MissionBriefObjective missionObjective5     //Destroy all enemies
{
   isPrimary    = false;
   status       = *IDSTR_OBJ_ACTIVE;
   shortTxt     = *IDSTR_CA2_OBJ5_SHORT;
   longTxt      = *IDSTR_CA2_OBJ5_LONG;
   bmpName      = *IDSTR_CA2_OBJ5_BMPNAME;
};

//Custom Pilots

Pilot pilotAveCaut1000
{
   id = 25;
   
   skill = 0.4;
   accuracy = 0.4;
   aggressiveness = 0.2;
   activateDist = 1000.0;
   deactivateBuff = 600.0;
   targetFreq = 5.4;
   trackFreq = 1.0;
   fireFreq = 2.0;
   LOSFreq = 0.8;
   orderFreq = 3.5;
};

Pilot pilotAveCaut600
{
   id = 26;
   
   skill = 0.4;
   accuracy = 0.4;
   aggressiveness = 0.2;
   activateDist = 600.0;
   deactivateBuff = 400.0;
   targetFreq = 5.4;
   trackFreq = 1.0;
   fireFreq = 2.0;
   LOSFreq = 0.8;
   orderFreq = 3.5;
};

//Map config
$server::HudMapViewOffsetX =  3500;
$server::HudMapViewOffsetY =  1000;

//-----------------------------------------------------------------------------
function onMissionStart()
{
    initVariables();
    initFormations();
    clearGeneralOrders();
    mercurySounds();
    earthquakeSounds();
    boundCheck();   
    cdAudioCycle(Watching, ss3, Gnash);
}
function onSPClientInit()
{
    initActors();
}
//----------------------------------------------------------------------------
function initVariables()
{
    $missionEnd                 = false;
    $comAttacked                = false;
    $spotted                    = false;
    $attackCalled               = false;
    $fullAttackCalled           = false;
    $bound1                     = false;
    $spottingTower              = "";
    $nav                        = 0;
    $nexusHitByPlayer           = 0;
    $dsHitByPlayer              = 0;
}

//-----------------------------------------------------------------------------
function initFormations()
{
    newFormation( wedge,   0,   0,0,    // leader
                          -20,-20,0,    // 1st
                           20,-20,0,
                          -30,-30,0  );                           
                           
    newFormation( line,    0,   0,0,   //leader                           
                           0, -40,0,   //1st
                           0, -80,0  );
                           
}

function player::onAdd(%this)
{
    $playerNum = %this;
}

//get playerVehicleId (playerId) as global var from playerNum/initiate mission by scheduling objective and mission events
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
    schedule("setNavMarker(myGetObjectId(\"MissionGroup/NAV/Nav001\"), True, -1);", 2);

    order("MissionGroup/HumanSquads/Squad1/Talon1", MakeLeader, true);
    order("MissionGroup/HumanSquads/Squad1", Formation, wedge);
    order("MissionGroup/HumanSquads/Squad1", Speed, Medium);

    order("MissionGroup/HumanSquads/Squad2/Paladin", MakeLeader, true);
    order("MissionGroup/HumanSquads/Squad2", Formation, wedge);
    order("MissionGroup/HumanSquads/Squad2", Speed, Medium);
    order("MissionGroup/HumanSquads/Squad2/Paladin", Guard, "MissionGroup/HumanBase");

    order("MissionGroup/HumanSquads/Squad3/Mino1", MakeLeader, true);
    order("MissionGroup/HumanSquads/Squad3", Formation, wedge);
    order("MissionGroup/HumanSquads/Squad3", Speed, Medium);
    order("MissionGroup/HumanSquads/Squad3/Mino1", Guard, "MissionGroup/BasePatrolPath");
    
    order("MissionGroup/Nexus", Guard, "MissionGroup/AwayPoint");
    order("MissionGroup/Nexus", HoldPosition, true);
    
    $spottingTower = myGetObjectId("MissionGroup/CommTowers/Comm" @ randomInt(1,3));
    //activateComms();
}   

//
//===Distance Checks==================================================================
function boundCheck()
{
    if($missionEnd){ return; }
    %dist = getDistance(getObjectId("MissionGroup/Center"), $playerId);
    if (%dist > 4500)
    {
        say(0,1, *IDSTR_CYB_NEX02, CYB_NEX02);
        forceToDebrief();
        $missionEnd = true;
    }
    else if (%dist > 4000)
    {
        if(!($bound1))
        {
            $bound1 = true;
            schedule("$bound1 = false;",60);
            say(0,1, *IDSTR_CYB_NEX01, CYB_NEX01);
        }
    }
    schedule("boundCheck();", 10);
    
    
    //extra mission driven checks
    %dist2 = getDistance( $spottingTower, $playerId);
    //say(0,1,"dist2="@%dist2);
    if( %dist2 < 600 && %dist2 != 0 && (!($spotted)))
    {
        order("MissionGroup/HumanSquads/Squad1/Talon1", attack, playerSquad);
        $spotted = true;
        //say(0,1, *IDSTR_CYB_NEX06, "CYB_NEX06.wav");
        
        //say(0,1,"Debug:: PlayerSpotted");
    }
    
    %dist3 = getDistance(myGetObjectId("MissionGroup/NAV/Nav" @ $Nav), $playerId);
    //say(0,1,"dist3="@%dist3);
    if ( %dist3 < 300 && %dist3 != 0 )
    {
        atEnd();
    }
}


//Mission functions===================================================================

function activateComms()
{
    playAnimSequence(myGetObjectId("MissionGroup/CommTowers/Comm1"), 0, true);
    playAnimSequence(myGetObjectId("MissionGroup/CommTowers/Comm2"), 0, true);
    playAnimSequence(myGetObjectId("MissionGroup/CommTowers/Comm3"), 0, true);
    schedule("stopAnimSequence(myGetObjectId(\"MissionGroup/CommTowers/Comm1\"), 0);", 3);
    schedule("stopAnimSequence(myGetObjectId(\"MissionGroup/CommTowers/Comm2\"), 0);", 3);
    schedule("stopAnimSequence(myGetObjectId(\"MissionGroup/CommTowers/Comm3\"), 0);", 3);
}

function CommTower::structure::onAttacked(%attacked, %attacker)
{
    if(getTeam(%attacker) != *IDSTR_TEAM_YELLOW){return;}
    if($spotted || $comAttacked || $attackCalled || $fullAttackCalled) {return;}
    $comAttacked = true;
    order("MissionGroup/HumanSquads/Squad1/Talon1", Guard, %attacked);
    
    //say(0,1,"Debug:: Talons Guarding Attacked ComTower");
}

function CommTower::structure::onDestroyed()
{
    if(isGroupDestroyed("MissionGroup/CommTowers"))
    {
        setNavMarker("MissionGroup/NAV/Nav001", false);
        schedule("setNavMarker(\"MissionGroup/NAV/Nav002\", true, -1);", 2);
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        say(0,1, *IDSTR_CYB_NEX04, "CYB_NEX04.wav");
        
        if(missionObjective2.status == *IDSTR_OBJ_COMPLETED)
        {schedule("furtherOrders();",10);}
        else say(0,1, *IDSTR_CYB_NEX09, "CYB_NEX09.wav");
    }
    
}

function Listen::structure::onAttacked(%attacked, %attacker)
{
    if($fullAttackCalled){return;}
    $fullAttackCalled = true;
    if( getTeam(%attacker) != *IDSTR_TEAM_RED )
    {
        order("MissionGroup/HumanSquads/Squad1/Talon1", Attack, playerSquad);
        order("MissionGroup/HumanSquads/Squad3/Mino1", Attack, playerSquad);
        order("MissionGroup/HumanSquads/Squad2/Paladin", Attack, playerSquad);
        //say(0,1,"Debug:: All Vehicles attacking player");
    }    
}

function Listen::structure::onDestroyed()
{
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    say(0,1, *IDSTR_CYB_NEX04, "CYB_NEX04.wav");
    if(missionObjective1.status == *IDSTR_OBJ_COMPLETED)
    { schedule("furtherOrders();",10); }
    else say(0,1, *IDSTR_CYB_NEX08, "CYB_NEX08.wav");
}

function vehicle::onAttacked(%attacked, %attacker)
{
    if( ($attackCalled) || (%attacked != $playerId) ){return;}
    $attackCalled = true;
    if( getTeam(%attacker) == *IDSTR_TEAM_RED )
    {
        order("MissionGroup/HumanSquads/Squad1/Talon1", Attack, playerSquad);
        //say(0,1,"Debug:: Patrol Vehicles attacking player");
    }    
}

function vehicle::onDestroyed(%destroyed, %destroyer)
{
    if(%destroyed == myGetObjectId("MissionGroup/HumanSquads/Squad1/Talon1") ||
            %destroyed == myGetObjectId("MissionGroup/HumanSquads/Squad1/Talon2")  )
    {
        order("MissionGroup/HumanSquads/Squad3/Mino1", Attack, playerSquad);
    }
    else if(isGroupDestroyed("MissionGroup/HumanSquads"))
    {
        missionObjective5.status = *IDSTR_OBJ_COMPLETED;
        //playerGets2Blaters
        InventoryWeaponSet( -1, 107, 2 );   #Blaster*****
    }
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

function DropShip::vehicle::onAttacked(%attacked, %attacker)
{
    if(%attacker == $playerId)
    {
        $dsHitByPlayer++;
        if( $dsHitByPlayer == 13 )
        {
            if( missionObjective3.status = *IDSTR_OBJ_ACTIVE )
            {missionObjective3.status = *IDSTR_OBJ_FAILED;}
            forceToDebrief();
        }
    }
}

function furtherOrders()
{
        setNavMarker("MissionGroup/NAV/Nav001", false);
        setNavMarker("MissionGroup/NAV/Nav002", false);
        $nav = randomInt(1,4);
        setNavMarker("MissionGroup/NAV/Nav" @ $nav, true, -1);
        missionObjective3.status = *IDSTR_OBJ_IGNORE;
        missionObjective4.status = *IDSTR_OBJ_ACTIVE;
        say(0,1,*IDSTR_CA2_NEX01, "CA2_NEX01.wav");
        order("MissionGroup/DropShips/DS1", Guard, "MissionGroup/NAV/Nav" @ $nav);
}

function atEnd()
{
    missionObjective4.status = *IDSTR_OBJ_COMPLETED;
    if(missionObjective1.status == *IDSTR_OBJ_COMPLETED &&
       missionObjective2.status == *IDSTR_OBJ_COMPLETED &&
       isSafe(*IDSTR_TEAM_YELLOW, $playerId, 800) ){
    updatePlanetInventory(CA2);
    forceToDebrief();    
    $missionEnd = true;}
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
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    missionObjective4.status = *IDSTR_OBJ_COMPLETED;
    missionObjective5.status = *IDSTR_OBJ_COMPLETED;
    forceToDebrief();
}







