// HA1

//--Mission Briefing and Objectives

// 1.Chase down and destroy Imperial Convoy
// 2.Return to Rebel Access tunnel when convoy is destroyed
// 3.Destroy all enemies encountered ( secondary )
// 4.Harabec must survive

$server::HudMapViewOffsetX = 5000;
$server::HudMapViewOffsetY = 500; 

function db(%msg)
{
    if($DB)
        echo(%msg);
}

DropPoint dropPoint1
{
	name = "foo";
	desc = "foo";
};

Pilot Squadmate
{
   id = 28;
   
   name = Mary;
   skill = 0.7;
   accuracy = 0.7;
   aggressiveness = 0.2;
   activateDist = 300.0;
   deactivateBuff = 100.0;
   targetFreq = 4.0;
   trackFreq = 0.2;
   fireFreq = 0.5;
   LOSFreq = 0.4;
   orderFreq = 4.0;
};

Pilot Harabec
{
   id = 29;
   
   name = Harabec;
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

MissionBriefInfo missionData
{
	title = 			*IDSTR_HA1_TITLE;
    planet =            *IDSTR_PLANET_MARS;           
	campaign = 			*IDSTR_HA1_CAMPAIGN;		   
	dateOnMissionEnd =  *IDSTR_HA1_DATE; 			  
	shortDesc = 		*IDSTR_HA1_SHORTBRIEF;	   
	longDescRichText = 	*IDSTR_HA1_LONGBRIEF;		   
	media = 		 	*IDSTR_HA1_MEDIA;
    nextMission =       "ha2";
    successDescRichText = *IDSTR_HA1_DEBRIEF_SUCC;
    failDescRichText =  *IDSTR_HA1_DEBRIEF_FAIL;
    location =          *IDSTR_HA1_LOCATION;
    successWavFile       = "HA1_Debriefing.wav";
    soundvol             = "ha1.vol";
};

MissionBriefObjective missionObjective1
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HA1_OBJ1_SHORT;
	longTxt		= *IDSTR_HA1_OBJ1_LONG;
	bmpname		= *IDSTR_HA1_OBJ1_BMPNAME;
}; 

MissionBriefObjective missionObjective2
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HA1_OBJ2_SHORT;
	longTxt		= *IDSTR_HA1_OBJ2_LONG;
	bmpname		= *IDSTR_HA1_OBJ2_BMPNAME;
}; 

MissionBriefObjective missionObjective3
{
	isPrimary 	= false;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HA1_OBJ3_SHORT;
	longTxt		= *IDSTR_HA1_OBJ3_LONG;
	bmpname		= *IDSTR_HA1_OBJ3_BMPNAME;
};

MissionBriefObjective missionObjective4
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HA1_OBJ4_SHORT;
	longTxt		= *IDSTR_HA1_OBJ4_LONG;
	bmpname		= *IDSTR_HA1_OBJ4_BMPNAME;
};

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    missionObjective4.status = *IDSTR_OBJ_COMPLETED;

    updatePlanetInventory(ha1);
    schedule("forceToDebrief();", 5.0);
}

function onMissionstart()
{
    cdAudioCycle(Newtech, ss4, Gnash);
}

function onSPClientInit()
{  
   $DB = false;
   setHostile(*IDSTR_TEAM_YELLOW, *IDSTR_TEAM_RED);
   setHostile(*IDSTR_TEAM_RED, *IDSTR_TEAM_YELLOW);
   initFormations();
   initActors();
   actorTalks($rebel.harabec, IDSTR_HA1_HAR01, "HA1_HAR01.wav" );
   marsSounds();
   windSounds();
}

function initFormations()
{
    newFormation(rebelFormation, 0,0,0,
                                 15,-10,0);
                                                               
    newFormation(patrolAFormation, 0,0,0,
                                   0,-40,0);
                                                                    
    newFormation(patrolBFormation, 0,0,0,
                                   -20,-30,0,
                                    40,-30,0 );
                                    
    newFormation(convoyFormation, 0,0,0,        
                                  10,80,0,
                                  10,-40,0,
                                  10,-120,0,
                                  -40,-150,0,
                                  40,-150,0
                                  );
}

function initActors()
{
    initPlayer();
    initRebel();
    initConvoy();
    initPatrol();
    initFlyer();
}

// PLAYER FUNCTIONS
function initPlayer()
{
    db("initPlayer()");
    $playerNum.missionFailed = false;
    $playerNum.flybyTriggered = false;
    schedule("playerDistanceCheck(boundaryFail);", 8.0);
    schedule("playerDistanceCheck(boundaryWarning);", 8.0); 
    $playerNum.attacked = false;  
}

function player::onAdd(%this)
{
    $playerNum = %this;
}

function vehicle::onAdd(%this)
{
    if(%this == playerManager::playerNumToVehicleId($playerNum))
        $playerNum.id = %this;
}

function vehicle::onAttacked(%this, %who)
{
    // if the player is attacked, and harabec and co. are returning to tunnel
    if( %this == ($playerNum.id) && ($rebel.state) == "return" && 
        getTeam(%this) != getTeam(%who))
    {
        order($rebel.harabec, Attack, %who);
        
        if(!$playerNum.attacked)
        {   
            schedule("actorTalks($rebel.harabec, IDSTR_GEN_HAR11, \"GEN_HAR11.wav\");", randomInt(1,2));
            $playerNum.attacked = true; 
        }
    }
}

function vehicle::onDestroyed(%this, %who)
{
    if(%this == $playerNum.id)
        $playerNum.missionFailed = true;
}

function actorTalks(%actorId, %txt, %snd)
{
    if(!($playerNum.missionFailed))
    {
        say(0,%actorId, *(%txt), %snd);     
    }    
}

function checkImperialsDestroyed()
{
    if($patrolA.count == 0 && $patrolB.count == 0 && $convoy.count == 0)
    {
        missionObjective3.status = *IDSTR_OBJ_COMPLETED;
        playSound(0, "GEN_OC01.wav", IDPRF_2D);
    }
}

function playerDistanceCheck(%arg)
{
    db("playerDistanceCheck = " @ %arg);
    
    if(%arg == boundaryWarning)
    {
        if(getDistance($playerNum.id, $rebel.harabec) > 3500 ) 
            actorTalks(0, IDSTR_GEN_TCM1, "GEN_TCM01.wav");
        
        else
            schedule("playerDistanceCheck(boundaryWarning);", 8.0);
    }
    
    else if(%arg == boundaryFail)
    {
        if(getDistance($playerNum.id, $rebel.harabec) > 4500 )
        {
            actorTalks(0, IDSTR_GEN_TCM2, "GEN_TCM02.wav");
            schedule("forceToDebrief();", 10.0);
        }
        else
            schedule("playerDistanceCheck(boundaryFail);", 8.0);    
    }
    
    else if(%arg == navAlpha)
    {
        if(getDistance($playerNum.id, getObjectId("MissionGroup/navMarkers/navAlpha")) < 200 &&
            isSafe(*IDSTR_TEAM_YELLOW, $playerNum.id, 750) &&
            isSafe(*IDSTR_TEAM_YELLOW, $rebel.harabec,750))
        {
            missionObjective2.status = *IDSTR_OBJ_COMPLETED;
            missionObjective4.status = *IDSTR_OBJ_COMPLETED;
            //playSound(0, "GEN_OC01.wav", IDPRF_2D);
            updatePlanetInventory(ha1);
            schedule("forceToDebrief();", 3.0);
        }
        else 
            schedule("playerDistanceCheck(navAlpha);", 2.0);
    }
}

// REBEL FUNCTIONS
function initRebel()
{
    db("initRebel()");
    
    $rebel = getObjectId("MissionGroup/rebelGroup");
    $rebel.harabec      = getObjectId("MissionGroup/rebelGroup/harabec");
    $rebel.squadmate    = getObjectId("MissionGroup/rebelGroup/squadmate");
    $rebel.route1       = getObjectId("MissionGroup/rebelGroup/rebelRoute1");
    $rebel.route2       = getObjectId("MissionGroup/rebelGroup/rebelRoute2");
    $rebel.hitByPlayer  = 0;
    $rebel.attacked     = false;
    $rebel.kills        = 0;
    $rebel.returned     = false;
    $rebel.state        = enroute;  // enroute
                                    // holdfire
                                    // return
    
    order($rebel.harabec, MakeLeader, true);
    order($rebel, Formation, rebelFormation);
    order($rebel.harabec, Speed, High);
    schedule("order($rebel.harabec, HoldPosition, true);",1.0);
    
    schedule("order($rebel.harabec, Guard, $rebel.route1);", 1.0);
    
    schedule("harabecSlowsDown();",5.0);    
    // check if player gets too far away
    schedule("playerDistanceCheck(rebel);", 10.0);
    
    // check distance from convoy
    schedule("rebelDistanceCheck(convoy1);", 10.0);
    schedule("rebelDistanceCheck(convoy2);", 10.0);
}

function harabecSlowsDown()
{
     %navAlpha = getObjectId("MissionGroup/navMarkers/navAlpha"); 
     
     if(getDistance($rebel.harabec,$playerNum.id) > 100 && !$rebel.attacked &&
        getDistance($rebel.harabec, %navAlpha) < getDistance($playerNum.id,%navAlpha))
     {
        order($rebel.harabec, Speed, Medium);
        order($convoy.leader, Speed, Low);  
     } 
     else
     {
        order($rebel.harabec,Speed, High);
        order($convoy.leader, Speed, medium);
     }
     schedule("harabecSlowsDown();",4.0);   
}

function rebelDistanceCheck(%arg)
{
    db("rebelDistanceCheck() = " @ %arg);
    if(%arg == convoy1)
    {
        if(getDistance($rebel.harabec, $convoy.leader) < 1200)
        {
            actorTalks($rebel.squadmate, IDSTR_HA1_1SQ01, "HA1_1SQ01.wav" );
            schedule("actorTalks($rebel.harabec, IDSTR_HA1_HAR02, \"HA1_HAR02.wav\" );", 5.0);
        }
        else 
            schedule("rebelDistanceCheck(convoy1);", 3.0);
            
    }
    else if(%arg == convoy2)
    {
        if(getDistance($rebel.harabec, $convoy.leader) < 450)
        {
            convoyAttacked();
        }
        else 
            schedule("rebelDistanceCheck(convoy2);", 3.0);
    }
    
    else if(%arg == flyer)
    {
        if(getDistance($rebel.harabec, $flyer.id1) < 500 &&
             isSafe(*IDSTR_TEAM_YELLOW, $playerNum.id, 650) &&
             !isGroupDestroyed($flyer.id1))
        {
            setDominantCamera($flyer.id1, $rebel.harabec);
            schedule("setOrbitCamera($patrolA.leader);",7.0);
            schedule("setPlayerCamera();", 10.5);
                
            actorTalks($flyer.id1, IDSTR_HA1_1IF01, "HA1_1IF01.wav" );
            actorTalks($flyer.id1, IDSTR_HA1_2IP02, "HA1_2IP02.wav" );
            order($patrolA.leader, Guard, $playerNum.id);    
        }
        
        else
            schedule("rebelDistanceCheck(flyer);", 2.0);
    }
} 

function rebel::vehicle::onAttacked(%this, %who)
{
    if(%who == $playerNum.id)
    {
        $rebel.hitByPlayer++;
        
        if($rebel.hitByPlayer == 2 && !isGroupDestroyed($rebel.squadmate))
        {
            if($rebel.harabec == %this)
                schedule("actorTalks($rebel.harabec, IDSTR_GEN_HAR4, \"GEN_HAR04.wav\");", 1.0);
            else
                schedule("actorTalks($rebel.harabec, IDSTR_HA1_1SQ02, \"HA1_1SQ02.wav\");", 1.0);    
        }
        
        if($rebel.hitByPlayer == 5)
        {
            schedule("actorTalks($rebel.harabec, IDSTR_GEN_HAR5, \"GEN_HAR05.wav\");", 1.0);
            order($rebel, Attack, %who);
            $playerNum.missionFailed = true;
        }        
    }
    
    else if(!($rebel.attacked) && getTeam(%this) != getTeam(%who))
    {
        schedule("actorTalks($rebel.squadmate, IDSTR_HA1_1SQ04, \"HA1_1SQ04.wav\" );", 1.0);
        order($rebel.squadmate, Attack, %who);
        $rebel.attacked = true;
    }
    else if(($rebel.state) == "return" && 
        getTeam(%this) != getTeam(%who))
    {
        order($rebel, Attack, %who);
        schedule("actorTalks($rebel.harabec, IDSTR_GEN_HAR8,\"GEN_HAR08.wav\");",1.0);
        $rebel.state = "attack";
    }
   
}

function rebel::vehicle::onArrived(%this, %where)
{
    if(%where == getObjectId("MissionGroup/rebelGroup/Rebelroute2/marker5") && !($rebel.returned) &&
       getDistance($playerNum.id, %where) > 400)
    {
        order($rebel.harabec, Guard, "MissionGroup/rebelGroup/Rebelroute2/tunnelMarker");
        $rebel.returned = true;
    }
    
    else if(%where == getObjectId("MissionGroup/rebelGroup/rebelRoute1/marker6"))
    {
        order($rebel.harabec, Attack, $convoy.leader);    
        order($rebel.harabec, Holdfire, true);
    }
}

function rebel::vehicle::onDestroyed(%this, %who)
{
    killChannel(%this);
    
    if(%this == $rebel.harabec)
    {
        $playerNum.missionFailed = true;
        setDominantCamera($rebel.harabec, $playerNum.id);
        schedule("forceToDebrief();", 5.0);
        return;
    }
    else if(%who != $playerNum.id)
    {    
        order( $rebel.harabec, Attack, %who);
        playSound(0, "HA1_1SQ06.wav", IDPRF_2D);                   
    }
}

function rebelKills()
{
    $rebel.kills++;
    if($rebel.kills == 4 && !isGroupDestroyed($rebel.squadmate))
        actorTalks($rebel.squadmate, IDSTR_HA1_1SQ05 , "HA1_1SQ05.wav" );
        
    if($rebel.kills == 9)
        actorTalks($rebel.harabec, IDSTR_GEN_HAR16, "GEN_HAR16.wav");        
}    
    

function rebelReturns()
{
    if(isSafe(*IDSTR_TEAM_YELLOW,$rebel.harabec, 650))
    {
        actorTalks($rebel.harabec, IDSTR_HA1_HAR04, "HA1_HAR04.wav" );
        order($rebel.harabec, Guard, $rebel.route2);
        setNavMarker("MissionGroup/navMarkers/navAlpha", true, -1);
        playerDistanceCheck(navAlpha);
        $rebel.state = "return";
    }
    
    else
        schedule("rebelReturns();", 5.0);
}

// CONVOY FUNCTIONS
function initConvoy()
{
    db("initConvoy()");
    
    $convoy = getObjectId("MissionGroup/convoyGroup");
    $convoy.leader = getObjectId("MissionGroup/convoyGroup/convoy1");
    $convoy.attacked = false;
    $convoy.count = 6;
    $convoy.route = getObjectId("MissionGroup/convoyGroup/convoyRoute");
    
    order($convoy.leader, MakeLeader, true);
    order($convoy, Formation, convoyFormation);
    order($convoy.leader, Speed, medium);
    schedule("order($convoy.leader, Guard, $convoy.route);", 1.0);
    schedule("order($convoy.leader, HoldPosition, true);", 2.0);
}

function convoy::vehicle::onAttacked(%this, %who)
{
    if(getTeam(%this) != getTeam(%who))
        convoyAttacked();    
}

function convoy::vehicle::onDestroyed(%this, %who)
{
    $convoy.count--;
    
    if(getTeam(%who) == *IDSTR_TEAM_YELLOW )
        rebelKills();
    if(%this == $convoy.leader)
    {
        order(getObjectId("MissionGroup/convoyGroup/convoy4"), guard, $convoy.route);
        order(getObjectId("MissionGroup/convoyGroup/convoy4"), HoldPosition, true); 
    }           
        
    if($convoy.count == 1)
    {
        schedule("order($patrolB.leader, Guard, $playerNum.id);", 14.0);  
    }
    if($convoy.count == 2)
        schedule("startFlyer();",0.5);
    
    if($convoy.count == 0)
    {        
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        playSound(0, "GEN_OC01.wav", IDPRF_2D);
                            
        if($patrolA.count > 0)
        {    
            schedule("order($patrolA.leader, Guard, $playerNum.id);", 20.0);
            order($patrolA.leader, Speed, Medium);
        }
         
        checkImperialsDestroyed();
        rebelReturns();     
    }           
}                         

function convoy::onNewLeader(%this)
{
    $convoy.leader = %this;
    order(%this, MakeLeader, true);
    order(%this, Guard, $convoy.route);    
}

function convoyAttacked()
{
    if(!($convoy.attacked))
    {
        order($rebel.harabec, HoldPosition,false);
        order($rebel.squadmate, HoldPosition,false);
        
        $convoy.attacked = true;
        actorTalks($rebel.harabec, IDSTR_HA1_HAR03, "HA1_HAR03.wav" );
        schedule("actorTalks(0, IDSTR_HA1_CON01, \"HA1_CON01.wav\" );", 3.0);
        schedule("order($rebel.harabec, Holdfire,false);",1.0);
        schedule("order($rebel.harabec, attack, $convoy);", 1.5);
        
        schedule("actorTalks($patrolA.leader, IDSTR_HA1_1IP01, \"HA1_1IP01.wav\" );", 8.0);
        schedule("actorTalks($patrolB.leader, IDSTR_HA1_2IP01, \"HA1_2IP01.wav\" );", 13.0);    
    }
} 

//--PATROL FUNCTIONS
function initPatrol()
{
    db("initPatrol()");
    
    $patrolA = getObjectId("MissionGroup/patrolAGroup");
    $patrolB = getObjectId("MissionGroup/patrolBGroup");
    
    $patrolA.count = 2;
    $patrolA.attacked = false;
    $patrolB.count = 3;
    $patrolA.attacked = false;
    $patrolA.route = getObjectId("MissionGroup/patrolAGroup/route");
    
    $patrolA.leader = getObjectId("MissionGroup/patrolAGroup/patrolA1");
    order($patrolA.leader, MakeLeader, true);
    order($patrolA.leader, Speed, High);
    order($patrolA, Formation, patrolAFormation);
    schedule("order($patrolA.leader, guard, $patrolA.route);",30.0);
    
    $patrolB.leader = getObjectId("MissionGroup/patrolBGroup/patrolB1");
    order($patrolB.leader, MakeLeader, true);
    order($patrolB.leader, Speed, High);
    order($patrolB, Formation, patrolBFormation);
    schedule("patrolBSweep();", 10.0);    
} 

function patrolA::vehicle::onDestroyed(%this, %who)
{
    $patrolA.count--;
    vehicle::salvage(%this);
    
    if( getTeam(%this) != getTeam(%who) )
        rebelKills();
    checkImperialsDestroyed();
}

function patrolB::vehicle::onDestroyed(%this, %who)
{
    $patrolB.count--;
    
    if(%this == $patrolB.leader)
        order($patrolB, Attack, $rebel);
    
    if($patrolB.count == 0)
    {
        order($rebel.harabec, HoldPosition, true);
        order($rebel.squadmate, HoldPosition, true);
    }
    
    if( getTeam(%this) != getTeam(%who) )
        rebelKills();       
    checkImperialsDestroyed();
}

function patrolA::vehicle:onAttacked(%this, %who)
{
    if(!($patrolA.attacked) && getTeam(%this) != getTeam(%who))
        $patrolA.attacked = true;
}

function patrolB::vehicle::onAttacked(%this, %who)
{
    if(!($patrolB.attacked) && getTeam(%this) != getTeam(%who))
    {
        $patrolB.attacked = true;
    }
}

function patrolASweep()
{
    if(getDistance($playerNum.id, $patrolA.leader) < 700)
        order($patrolA.leader, Attack, $playerNum.id);
    
    else if(!($patrolA.attacked))
        schedule("patrolASweep();", 10.0);
}

function patrolBSweep()
{
    if(getDistance($playerNum.id, $patrolB.leader) < 700)
        order($patrolB.leader, Attack, $playerNum.id);
    
    else if(!($patrolB.attacked))
        schedule("patrolBSweep();", 10.0);    
}

//--FLYER FUNCTION
function initFlyer()
{
    db("initFlyer()");
    
    $flyer      = getObjectId("MissionGroup/flyerGroup");
    $flyer.id1  = getObjectId("MissionGroup/flyerGroup/flyer1");
    $flyer.route = getObjectId("MissionGroup/flyerGroup/flyerRoute");
    order($flyer.id1, Speed, high);
    schedule("setHercOwner( $rebel,  $flyer.id1);", 5.0);
    setVehicleRadarVisible($flyer.id1,false);
}

function flyer::vehicle::onArrived(%this, %where)
{
    if(%where == getObjectId("MissionGroup/flyerGroup/route/last"))
        order(%this, Guard, %where);    
}

function startFlyer()
{
     schedule("order($flyer.id1, Guard, $flyer.route);",10.0);
     schedule("rebelDistanceCheck(flyer);", 10.0);     
     setVehicleRadarVisible($flyer.id1,true); 
}