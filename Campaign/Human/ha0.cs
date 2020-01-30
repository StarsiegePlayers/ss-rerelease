// HA0

//--Mission Briefing and Objectives

// Objective 1:Destroy downed flyer at Nav Alpha
// Objective 2:Return to tunnel access safely (Nav Bravo)
// Objective 3:Destroy any Imperial resistance you encounter.  

DropPoint dropPoint1
{
	name = "foo";
	desc = "foo";
};

MissionBriefInfo missionData
{
   title  			      = *IDSTR_HA0_TITLE;
   planet               = *IDSTR_PLANET_MARS;           
   campaign  			   = *IDSTR_HA0_CAMPAIGN;		   
   dateOnMissionEnd     = *IDSTR_HA0_DATE; 			  
   shortDesc            = *IDSTR_HA0_SHORTBRIEF;	   
   longDescRichText     = *IDSTR_HA0_LONGBRIEF;		   
   media                = *IDSTR_HA0_MEDIA;
   nextMission          = *IDSTR_HA0_NEXTMISSION;
   successDescRichText  = *IDSTR_HA0_DEBRIEF_SUCC;
   failDescRichText     = *IDSTR_HA0_DEBRIEF_FAIL;
   location             = *IDSTR_HA0_LOCATION;
   successWavFile       = "HA0_Debriefing.wav";
   endCinematicSmk      = "cin_HA.smk";
   soundvol             = "ha0.vol";
};

MissionBriefObjective missionObjective1
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HA0_OBJ1_SHORT;
	longTxt		= *IDSTR_HA0_OBJ1_LONG;
	bmpname		= *IDSTR_HA0_OBJ1_BMPNAME;
}; 

MissionBriefObjective missionObjective2
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HA0_OBJ2_SHORT;
	longTxt		= *IDSTR_HA0_OBJ2_LONG;
	bmpname		= *IDSTR_HA0_OBJ2_BMPNAME;
}; 

MissionBriefObjective missionObjective3
{
	isPrimary 	= false;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HA0_OBJ3_SHORT;
	longTxt		= *IDSTR_HA0_OBJ3_LONG;
	bmpname		= *IDSTR_HA1_OBJ3_BMPNAME;
};

function onMissionStart()
{
    cdAudioCycle(ss3, cyberntx, ss4);
}

function onSPClientInit()
{
    $DB = false;
    newFormation(patrolFormation, 0,0,0,
                                  20,0,0);
    newFormation(recoveryForm,  0,0,0,
                                10,40,0,
                                10,80,0,
                               -40,120,0,
                                40,120,0);
    initActors();
    marsSounds();
    windSounds();       
}

function db(%msg)
{
    if($DB)
        echo(%msg);
}

function initActors()
{
    initPlayer();
    initFlyer();
    initPatrol();
    initGuard();
    //initScout();
    initRecovery();
    //initAmbush();
}

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(ha0);
    schedule("forceToDebrief();", 5.0);
}

//--PLAYER FUNCTIONS
function initPlayer()
{
    schedule("setNavMarker(\"MissionGroup/navMarkers/navAlpha\", true, -1);", 2.0);
    setNavMarker("MissionGroup/navMarkers/navBravo", false);
    schedule("playerDistanceCheck(boundary);", 10.0);
    schedule("playerDistanceCheck(navAlpha);", 6.0);
    schedule("actorTalks($playerNum, IDSTR_HA0_TCM01, \"HA0_TCM01.wav\");", 1.0);
    $salvage = false;
}

function player::onAdd(%this)
{
    $playerNum = %this;
    $playerNum.missionFailed = false;
    $playerNum.leftMissionArea = false;
}

function vehicle::onAdd(%this)
{
    if(%this == playerManager::PlayerNumToVehicleId($playerNum))
        $playerNum.id = %this;
}

function vehicle::onDestroyed(%this, %who)
{
    if(%this == $playerNum.id)
        $playerNum.missionFailed = true;
}

function playerDistanceCheck(%arg)
{
    db("PlayerDistanceCheck() = " @ %arg);
    if(%arg == boundary)
    {
        %var = getDistance($playerNum.id, getObjectId("MissionGroup/navMarkers/navAlpha"));
        db("distance = " @ %var);
        
        if(getDistance($playerNum.id, getObjectId("MissionGroup/navMarkers/navAlpha")) > 3000)
        {
            
            if(getDistance($playerNum.id, getObjectId("MissionGroup/navMarkers/navAlpha")) > 3500)
            {
                // mission failed, player left area
                schedule("forceToDebrief();", 5.0);
                say(0,$playerNum,*IDSTR_GEN_TCM2, "GEN_TCM02.wav");

                return;
            }
            else if(!($playerNum.leftMissionArea)) 
            {
                // warn player, leaving mission area
                say(0,$playerNum,*IDSTR_GEN_TCM1, "GEN_TCM01.wav");
                $playerNum.leftMissionArea = true;    
            }          
        } 
        else if(getDistance($playerNum.id, getObjectId("MissionGroup/navMarkers/navAlpha")) < 2500 )
            $playerNum.leftMissionArea = false; 
                
        schedule("playerDistanceCheck(boundary);", 10.0);    
    }
    
    else if(%arg == patrol && !($patrol.attacked))
    {
        if(getDistance($playerNum.id, $patrol.leader) < 1000)
            order($patrol.leader, Attack, $playerNum.id);
        
        else
           schedule("playerDistanceCheck(patrol);", 10.0);            
    }
    
    else if(%arg == navBravo)
    {
        if(getDistance($playerNum.id, getObjectId("MissionGroup/navMarkers/navBravo")) < 200 )
        {
            missionObjective2.status = *IDSTR_OBJ_COMPLETED; 
                        
            if($salvage)
            {
                InventoryWeaponAdjust(	-1,	125,	2	);	#Pit Viper 12
                InventoryWeaponAdjust(	-1,	127,	2	);	#Sparrow 10
                InventoryWeaponAdjust(	-1,	128,	2	);	#SWARM 6
                InventoryWeaponAdjust(	-1,	129,	2	);	#Minion
            }
            updatePlanetInventory(ha0);
            imperialsRetreat();
            schedule("forceToDebrief();", 1.0);
        }
        else
            schedule("playerDistanceCheck(navBravo);", 5.0);
        
    } 
    
        
    else if(%arg == navAlpha)
    {
        if(getDistance($playerNum.id, getObjectId("MissionGroup/navMarkers/navAlpha")) < 500)
        {
            playerAttacksFlyer();
            setNavMarker(getObjectId("MissionGroup/navMarkers/navAlpha"),false);
        }
        else 
            schedule("playerDistanceCheck(navAlpha);", 5.0);    
    }   
}

function actorTalks(%actor, %txt, %snd)
{
    if(!($playerNum.missionFailed))
        say(0,%actor,*(%txt), %snd);
}

function returnToTunnel()
{
    if($guard.count == 0 && $patrol.count == 0 && isSafe(*IDSTR_TEAM_YELLOW, $playerNum.id, 1200))
    {
        schedule("actorTalks($playerNum, IDSTR_HA0_TCM03, \"HA0_TCM03.wav\");",3.0);
        //schedule("missionObjective2.status = *IDSTR_OBJ_COMPLETED;", 4.5);
    }
    else
        schedule("returnToTunnel();", 5.0);
}

//--FLYER FUNCTIONS

function initFlyer()
{
    $flyer = getObjectId("MissionGroup/flyerGroup");
    $flyer.attacked = false;
    damageArea(getObjectId("MissionGroup/flyerGroup/flyer1"),20,-50,0,60,7000);
}

function flyer::vehicle::onAttacked(%this, %who)
{
    if(getTeam(%this) != getTeam(%who))
        playerAttacksFlyer();
}

function flyer::vehicle::onDestroyed(%this, %who)
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    playSound(0,"GEN_OC01.wav", IDPRF_2D);
    missionObjective2.status = *IDSTR_OBJ_ACTIVE;
    setNavMarker("MissionGroup/navMarkers/navAlpha", false);
    setNavMarker("MissionGroup/navMarkers/navBravo", true, -1);
    schedule("returnToTunnel();", randomInt(2, 4));
    playerDistanceCheck(navBravo);
}

function playerAttacksFlyer()
{
    if(!($flyer.attacked))
    {
        $flyer.attacked = true;
        schedule("order($guard, Attack, $playerNum.id);", 3.0);
        order($patrol.leader, Speed, high);
        schedule("actorTalks($guard,IDSTR_HA0_HCA01, \"HA0_HCA01.wav\");", 2.0);    
    }
} 

// GUARD FUNCTIONS
function initGuard()
{
    $guard = getObjectId("MissionGroup/guardGroup");
    $guard.count = 1;
    order($guard, Guard, "MissionGroup/guardGroup/route");
} 

function guard::vehicle::onDestroyed(%this, %who)
{
    $guard.count--;
    echo("guard destroyed");
    patrolWarpsIn();
    //schedule("order($patrol.leader, Guard, $playerNum.id);", 5.0);
    checkImperialsDestroyed();
    $flyer.attacked = true;
    killChannel($guard);
}

function checkImperialsDestroyed()
{
    if($guard.count == 0 && $patrol.count == 0)
    {
        missionObjective3.status = *IDSTR_OBJ_COMPLETED;    
        playSound(0,"GEN_OC01.wav", IDPRF_2D);
        beginRecovery();
    }
}

function guard::vehicle::onAttacked(%this, %who)
{
    schedule("playerAttacksFlyer();",1.0);
}

// PATROL FUNCTIONS
function initPatrol()
{
    $patrol = getObjectId("MissionGroup/patrolGroup");
    $patrol.count = 1;
    $patrol.leader = getObjectId("MissionGroup/patrolGroup/patrol1");
    $patrol.attacked = false;
    schedule("initPatrolMarkers();",5.0);
        
    order($patrol.leader, MakeLeader, true);
    order($patrol.leader, Speed, High);
    order($patrol, Formation, patrolFormation);
    
    schedule("playerDistanceCheck(patrol);", 5.0);
    schedule("patrolJoinsFlyer();", 180.0);
}

function initPatrolMarkers()
{
    $patrol.m1 = getObjectId("MissionGroup/patrolGroup/m1"); 
    $patrol.m2 = getObjectId("MissionGroup/patrolGroup/m2"); 
    $patrol.m3 = getObjectId("MissionGroup/patrolGroup/m3"); 
    $patrol.m4 = getObjectId("MissionGroup/patrolGroup/m4"); 
}

function patrolWarpsIn()
{
    %x1 = getPosition($patrol.m1,x);
    %y1 = getPosition($patrol.m1,y);
    %z1 = getPosition($patrol.m1,z);
    
    if( getDistance($playerNum.id, $patrol.m1) < 800 )
    {
        %x1 = getPosition($patrol.m3,x);
        %y1 = getPosition($patrol.m3,y);
        %z1 = getPosition($patrol.m3,z);
    }
    
    setPosition($patrol.leader,  %x1,%y1,%z1);    
    schedule("order($patrol.leader, Attack, $playerNum.id);",1.0);
}

function patrol::vehicle::onNewLeader(%this)
{
    $patrol.leader = %this;
}

function patrolJoinsFlyer()
{
    if(!($patrol.attacked))
        order($patrol.leader, Guard, "MissionGroup/navMarkers/navAlpha");    
}

function patrol::vehicle::onAttacked(%this, %who)
{
    if(getTeam(%this) != getTeam(%who))
        $patrol.attacked = true;
}  

function patrol::vehicle::onDestroyed()
{
    $patrol.count--;
    checkImperialsDestroyed();
}

// AMBUSH FUNCTIONS
function initAmbush()
{
    $ambush = getObjectId("MissionGroup/ambushGroup/ambush1");
    $ambush.set = false;
}

function ambush::vehicle::onDestroyed(%this, %who)
{
    vehicle::salvage(%this);
}

// RECOVERY TEAM
function initRecovery()
{
    $recovery = getObjectId("MissionGroup/recovery");
    $recovery.leader = getObjectId("MissionGroup/recovery/r1");
    $recovery.c1 = getObjectId("MissionGroup/recovery/r2");
    $recovery.c2 = getObjectId("MissionGroup/recovery/r3");
    
    order($recovery.leader, MakeLeader, true);
    order($recovery.leader, Speed, High);
    order($recovery, Formation, recoveryForm);
    
    //schedule("beginRecovery();",240.0);   
}
 
function beginRecovery()
{
    order($recovery.leader, Guard, getObjectId("MissionGroup/navMarkers/navAlpha"));
    checkRecoveryDist();
}
 
function checkRecoveryDist()
{
    if(getDistance($recovery.leader, getObjectId("MissionGroup/navMarkers/navAlpha")) < 600 &&
        getDistance($recovery.leader,$playerNum.id)<1000)
    {
        order($recovery.c1, ShutDown, true);
        order($recovery.c2, ShutDown, true);
        playSound(0,"enem_squad_det.wav", IDPRF_2D);
    }
    else
        schedule("checkRecoveryDist();",10.0);        
}

function imperialsRetreat()
{
    order($guard, HoldFire, true);
    order($patrol, HoldFire, true);    
}

function salvage::structure::onScan(%this, %scanner, %str)
{
    if(!$salvage)
        $salvage = true;
}

function salvage::structure::onDestroyed(%this, %who)
{
    $salvage = false;    
}