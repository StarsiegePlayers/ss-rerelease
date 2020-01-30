// HA6

//--Mission Briefing and Objectives

// 1.Take out the remaining turret defenses
// 2.Secure the Headquarters by destroying all remaining troops
// 3.Locate building Navare is held up in
// 4.Locate and Secure Imperial weapons cache (Secondary)

DropPoint dropPoint1
{
	name = "DropPointA";
	desc = "DropPointA";
};

MissionBriefInfo missionData
{
	title = 			*IDSTR_HA6_TITLE;
    planet =            *IDSTR_PLANET_MARS;           
	campaign = 			*IDSTR_HA6_CAMPAIGN;		   
	dateOnMissionEnd =  *IDSTR_HA6_DATE; 			  
	shortDesc = 		*IDSTR_HA6_SHORTBRIEF;	   
	longDescRichText = 	*IDSTR_HA6_LONGBRIEF;		   
	media = 		 	*IDSTR_HA6_MEDIA;
    nextMission =       *IDSTR_HA6_NEXTMISSION;
    successDescRichText = *IDSTR_HA6_DEBRIEF_SUCC;
    failDescRichText =  *IDSTR_HA6_DEBRIEF_FAIL;
    location =          *IDSTR_HA6_LOCATION;
    successWavFile       = "HA6_Debriefing.wav";
    endCinematicRec	 = "cinHB.rec";
    endCinematicSmk      = "cin_HB.smk";
    soundvol             = "ha6.vol";
};

MissionBriefObjective missionObjective1
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HA6_OBJ1_SHORT;
	longTxt		= *IDSTR_HA6_OBJ1_LONG;
	bmpname		= *IDSTR_HA6_OBJ1_BMPNAME;
}; 

MissionBriefObjective missionObjective2
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HA6_OBJ2_SHORT;
	longTxt		= *IDSTR_HA6_OBJ2_LONG;
	bmpname		= *IDSTR_HA6_OBJ2_BMPNAME;
}; 

MissionBriefObjective missionObjective3
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HA6_OBJ3_SHORT;
	longTxt		= *IDSTR_HA6_OBJ3_LONG;
	bmpname		= *IDSTR_HA6_OBJ3_BMPNAME;
};


function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    
    updatePlanetInventory(ha6);
    schedule("forceToDebrief();", 5.0);
}

function onMissionStart()
{
        cdAudioCycle(Cloudburst, Watching, Terror);  
}

function onSPClientInit()
{
    $DB = false;
    newFormation(form, 0,0,0, 
                       20,0,0,
                       -20,0,0,
                       40,0,0,
                       -40,0,0);

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
    initArtillery();
    initPatrolA();
    initPatrolB();
    initPatrolC();
    initPatrolD();
    initPatrolE();
    initTurret();   
}

//--PLAYER STUFF

function initPlayer()
{
    db("initPlayer()");
    schedule("setNavMarker(getObjectId(\"MissionGroup/navMarkers/navAlpha\"), true, -1);", 1.0);
    $playerNum.missionFailed = false;
    schedule("actorTalks($playerNum.id,IDSTR_HA6_TCM01, \"HA6_TCM01.wav\");", 5.0);
    schedule("distCheck(minefield);",10.0);
    schedule("$mineMarker = getObjectId(\"MissionGroup/mineMarker\");",5.0);
    schedule("distCheck(boundaryWarn);",10.0);
    schedule("distCheck(boundaryFail);",20.0);
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

function vehicle::onDestroyed(%this, %who)
{
    if(%this == $playerNum.id)
        $playerNum.missionfailed = true;
}

function distCheck(%obj)
{
    //db("distCheck() = " @ %obj);
    
    if(%obj == boundaryWarn)
    {
        if(getDistance($playerNum.id, getObjectId("MissionGroup/navMarkers/navAlpha") ) > 2500)
        {
            //generic warning
            actorTalks(0, IDSTR_GEN_TCM1, "GEN_TCM01.wav");
        }
        else 
            schedule("distCheck(boundaryWarn);", 10.0);
    }
    
    else if(%obj == boundaryFail)
    {
        if(getDistance($playerNum.id, getObjectId("MissionGroup/navMarkers/navAlpha")) > 4000)
        {
            // generic fail
            actorTalks(0, IDSTR_GEN_TCM2, "GEN_TCM02.wav");
            schedule("forceToDebrief();", 5.0);
        }
         else 
            schedule("distCheck(boundaryFail);", 10.0);
    }
    
    //triggered when player gets near patrolA
    else if(%obj == patrolA)
    {
        if(getDistance($playerNum.id, $patrolA.leader) < 350)
            patrolAAttacksPlayer();
        else
            schedule("distCheck(patrolA);", 5.0);    
    }
    
    //triggered when player approaches city
    else if(%obj == patrolB)
    {
        if(getDistance($playerNum.id, $patrolB.leader) < 750)
            patrolBAttacksPlayer();
        else
            schedule("distCheck(patrolB);", 5.0);   
    }
    
    //triggered when player enters city
    else if(%obj == patrolC)
    {
        if(getDistance($playerNum.id, $patrolC.leader) < 500)
            patrolCAttacksPlayer();
        else
            schedule("distCheck(patrolC);", 5.0);      
    }
    
    //triggered when player approaches Navares building
    else if(%obj == patrolD)
    {
        if(getDistance($playerNum.id, $patrolD.leader) < 250)
            patrolDAttacksPlayer();
        else
            schedule("distCheck(patrolD);", 5.0);  
    }
    
    //triggered when player approaches patrolE
    else if(%obj == patrolE)
    {
        if(getDistance($playerNum.id, $patrolE.leader) < 400)
            patrolEAttacksPlayer();
        else
            schedule("distCheck(patrolE);", 5.0);  
    }
    
    else if(%obj == minefield)
    {
        if(getDistance($playerNum.id, $mineMarker)<200)
            playSound(0, "mine_field_det.wav", IDPRF_2D);
        else
            schedule("distCheck(minefield);",5.0);       
    }
}

function actorTalks(%actorId, %txt, %snd)
{
    if(!$playerNum.missionfailed)
        say(0, %actorId, *(%txt), %snd);
}

function checkMissionWon()
{
    if(missionObjective1.status == *IDSTR_OBJ_COMPLETED &&
       missionObjective2.status == *IDSTR_OBJ_COMPLETED &&
       missionObjective3.status == *IDSTR_OBJ_COMPLETED)
    {
        updatePlanetInventory(ha6);
        schedule("actorTalks($playerNum.id,IDSTR_HA6_TCM02, \"HA6_TCM02.wav\");", 1.0);
        schedule("forceToDebrief();", 5.0);
        
    } 
    else
        schedule("checkMissionWon();", 8.0); 
}

function checkHercsDestroyed()
{    
    if($patrolA.count == 0 && $patrolB.count == 0 && $patrolC.count == 0 && $patrolD.count == 0 &&
        $patrolE.count == 0)
    {
        missionObjective2.status = *IDSTR_OBJ_COMPLETED;
        playSound(0, "GEN_OC01.wav", IDPRF_2D);
    }  
}

//--ARTILLERY STUFF
function initArtillery()
{
    db("initArtillery()");
    $artillery = getObjectId("MissionGroup/Artillery");
    $artillery.a1 = getObjectId("MissionGroup/Artillery/artillery1");
    $artillery.a2 = getObjectId("MissionGroup/Artillery/artillery2");
    $artillery.a3 = getObjectId("MissionGroup/Artillery/artillery3");
    $artillery.a4 = getObjectId("MissionGroup/Artillery/artillery4");
        
    $artillery.curTarget = "";
    $artillery.index = 0;
    $artillery.count = 4;    

    setVehicleRadarVisible( $artillery.a1, False );
    setVehicleRadarVisible( $artillery.a2, False );
    setVehicleRadarVisible( $artillery.a3, False );
    setVehicleRadarVisible( $artillery.a4, False );
}

function hitByArtillery(%who)
{
    if(%who == $artillery.a1 || %who == $artillery.a2 || %who == $artillery.a3 || %who == $artillery.a4)
        return true;
    else
        return false;
}

function vehicle::onSpot(%this, %target)
{
    db("vehicle::onSpot() = " @ %target);
    $artillery.playerSpotted = true;
    
    %n = randomInt(1,4);
    
    if(%target == "")
        clearArtilleryTarget();
    else
    {
        setArtilleryTarget(%target);
        if(%n == 1)
            actorTalks($artillery,IDSTR_HA6_1AR01,"HA6_1AR01.wav");
        
        if(%n == 2)
            actorTalks($artillery,IDSTR_HA6_2AR01,"HA6_2AR01.wav");

        if(%n == 3)
            actorTalks($artillery,IDSTR_HA6_3AR01,"HA6_3AR01.wav");

        if(%n == 4)
            actorTalks($artillery,IDSTR_HA6_4AR01,"HA6_4AR01.wav");
    }
}

function clearArtilleryTarget()
{
    db("clearArtilleryTarget()");
    $artillery.curTarget = "";
    
    order($artillery.a1, Clear);
    order($artillery.a2, Clear);
    order($artillery.a3, Clear);
    order($artillery.a4, Clear);    
}

function setArtilleryTarget(%target)
{
    db("setArtilleryTarget()");
                   
    $artillery.curTarget = %target;
    db("curTarget = " @ $artillery.curTarget);

    schedule("order($artillery.a1, Attack, $artillery.curTarget);", randomInt(1,4) );
    schedule("order($artillery.a2, Attack, $artillery.curTarget);", randomInt(1,4) );    
    schedule("order($artillery.a3, Attack, $artillery.curTarget);", randomInt(1,4) );    
    schedule("order($artillery.a4, Attack, $artillery.curTarget);", randomInt(1,4) );        
}

function vehicle::onMessage(%this, %msg)
{
    db(getObjectName(%this) @ "onMessage() = " @ %msg);
    if(%msg == "ArtilleryOutOfAmmo" && $artillery.count != 0)
    {
        $artillery.count--;                                    
    }
}

//PATROL A STUFF
function initPatrolA()
{
    db("initPatrolA()");
    $patrolA = getObjectId("MissionGroup/patrolA");
    $patrolA.leader = getObjectId("MissionGroup/patrolA/a1");
    $patrolA.route =  getObjectId("MissionGroup/patrolA/route");
    $patrolA.attacked = false; 
    $patrolA.count = 2;
    $patrolA.start = getObjectId("MissionGroup/patrolA/start/marker" @ randomInt(1,4));
    
    order($patrolA.leader, MakeLeader, true);
    order($patrolA.leader, Speed, medium);
    order($patrolA, Formation, form);
    
    db(getObjectName($patrolA.start));
    %x = getPosition($patrolA.start,x);
    %y = getPosition($patrolA.start,y);
    %z = getPosition($patrolA.start,z);

    //setPosition($patrolA.leader, %x, %y + 10, %z);
    
    schedule("order($patrolA.leader, Guard, $patrolA.route);", 3.0);
    schedule("distCheck(patrolA);", 10.0); 
}

function patrolAAttacksPlayer()
{
    if(!$patrolA.attacked)
    {
        order($patrolA, Attack, pick("playersquad"));
        $patrolA.attacked = true;
    }
}

function a::vehicle::onAttacked(%this, %who)
{    
    if(getTeam(%this) != getTeam(%who))
        schedule("patrolAAttacksPlayer();",1.0);    
}

function a::vehicle::onDestroyed(%this, %who)
{
    $patrolA.count--;
    if($patrolA.count == 0)
        checkHercsDestroyed();        
}

//PATROL B STUFF
function initPatrolB()
{
    db("initPatrolB()");
    $patrolB = getObjectId("MissionGroup/patrolB");
    $patrolB.leader = getObjectId("MissionGroup/patrolB/b1");
    $patrolB.route =  getObjectId("MissionGroup/patrolB/route");
    $patrolB.attacked = false; 
    $patrolB.count = 3;
    $patrolB.start = getObjectId("MissionGroup/patrolB/start/marker" @ randomInt(1,4));
    
    order($patrolB.leader, MakeLeader, true);
    order($patrolB.leader, Speed, High);
    order($patrolB, Formation, form);
    
    db(getObjectName($patrolB.start));
    %x = getPosition($patrolB.start,x);
    %y = getPosition($patrolB.start,y);
    %z = getPosition($patrolB.start,z);

    //setPosition($patrolB.leader, %x, %y + 10, %z);

    schedule("order($patrolB.leader, Guard, $patrolB.route);", 3.0); 
    schedule("distCheck(patrolB);", 10.0);
}

function patrolBAttacksPlayer()
{
    if(!$patrolB.attacked )
    {
        actorTalks($patrolB.leader, IDSTR_HA6_SQB01, "HA6_SQB01.wav");
        order($patrolB, Attack, pick("playersquad"));
        $patrolB.attacked = true;
    }
}

function b::vehicle::onAttacked(%this, %who)
{
    if(getTeam(%this) != getTeam(%who) )
        schedule("patrolBAttacksPlayer();",1.0);    
}

function b::vehicle::onDestroyed(%this, %who)
{
    $patrolB.count--;
    if($patrolB.count == 0)
        checkHercsDestroyed();        
}

//PATROL C STUFF
function initPatrolC()
{
    db("initPatrolC()");
    $patrolC = getObjectId("MissionGroup/patrolC");
    $patrolC.leader = getObjectId("MissionGroup/patrolC/c1");
    $patrolC.route =  getObjectId("MissionGroup/patrolC/route");
    $patrolC.attacked = false; 
    $patrolC.count = 2;
    
    order($patrolC.leader, MakeLeader, true);
    order($patrolC.leader, Speed, High);
    order($patrolC, Formation, form);
    schedule("order($patrolC.leader, Guard, $patrolC.route);", 3.0);
    schedule("distCheck(patrolC);", 10.0); 
}

function patrolCAttacksPlayer()
{
    if(!$patrolC.attacked )
    {
        actorTalks($patrolC.leader, IDSTR_HA6_SQC01, "HA6_SQC01.wav");
        order($patrolC, Attack, pick("playersquad"));
        $patrolC.attacked = true;
    }
}

function c::vehicle::onAttacked(%this, %who)
{
    if(getTeam(%this) != getTeam(%who))
        schedule("patrolCAttacksPlayer();",1.0);    
}

function c::vehicle::onDestroyed(%this, %who)
{
    $patrolC.count--;
    if($patrolC.count == 0)
        checkHercsDestroyed();        
}

//PATROL D STUFF
function initPatrolD()
{
    db("initPatrolD()");
    $patrolD = getObjectId("MissionGroup/patrolD");
    $patrolD.leader = getObjectId("MissionGroup/patrolD/d1");
    $patrolD.route =  getObjectId("MissionGroup/patrolD/route");
    $patrolD.attacked = false; 
    $patrolD.count = 1;
    
    order($patrolD.leader, MakeLeader, true);
    schedule("order($patrolD.leader, Guard, $patrolD.route);", 3.0);
    schedule("distCheck(patrolD);", 10.0); 
}

function patrolDAttacksPlayer()
{
    if(!$patrolD.attacked)
    {
        actorTalks($patrolD.leader, IDSTR_HA6_HCD01, "HA6_HCD01.wav");
        order($patrolD, Attack, pick("playersquad"));
        $patrolD.attacked = true;
    }
}

function d::vehicle::onAttacked(%this, %who)
{
    if(getTeam(%this) != getTeam(%who))
        schedule("patrolDAttacksPlayer();",1.0);    
}

function d::vehicle::onDestroyed(%this, %who)
{
    $patrolD.count--;
    if($patrolD.count == 0)
        checkHercsDestroyed();        
}

//PATROL E STUFF
function initPatrolE()
{
    db("initPatrolE()");
    $patrolE = getObjectId("MissionGroup/patrolE");
    $patrolE.leader = getObjectId("MissionGroup/patrolE/e1");
    $patrolE.route =  getObjectId("MissionGroup/patrolE/route");
    $patrolE.attacked = false; 
    $patrolE.count = 2;
    
    order($patrolE.leader, MakeLeader, true);
    order($patrolE.leader, Speed, High);
    order($patrolE, Formation, form);
    schedule("order($patrolE.leader, Guard, $patrolE.route);", 3.0);
    schedule("distCheck(patrolE);", 10.0); 
}

function patrolEAttacksPlayer()
{
    if(!$patrolE.attacked)
    {
        actorTalks($patrolE.leader, IDSTR_HA6_PTE01, "HA6_PTE01.wav");
        order($patrolE, Attack, pick("playersquad"));
        $patrolE.attacked = true;
    }
}

function e::vehicle::onAttacked(%this, %who)
{
    if(getTeam(%this) != getTeam(%who))
        schedule("patrolEAttacksPlayer();",1.0);    
}

function e::vehicle::onDestroyed(%this, %who)
{
    $patrolE.count--;
    if($patrolE.count == 0)
        checkHercsDestroyed();        
}

//TURRET STUFF
function initTurret()
{
    db("initTurret()");
    $turret = getObjectId("MissionGroup/Turrets");
    $turret.count = 4;
}

function c::turret::onAttacked(%this, %who)
{
    db("c::turret::onAttacked()");
    if(%who != $artillery.a1 && %who != $artillery.a2 && %who != $artillery.a3 && %who != $artillery.a4)
        schedule("patrolCAttacksPlayer();", 1.0);
}

function e::turret::onAttacked(%this, %who)
{
    db("e::turret::onAttacked()");
    if(%who !=$artillery.a1 && %who != $artillery.a2 && %who != $artillery.a3 && %who != $artillery.a4)
        schedule("patrolEAttacksPlayer();", 1.0);
}

function turretDestroyed()
{
    $turret.count--;
    if($turret.count == 0)             // did player win ?
    {
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        playSound(0, "GEN_OC01.wav", IDPRF_2D);   
        checkMissionWon();
    }    
    db("turret count = " @ $turret.count);
}

function c::turret::onDestroyed(%this, %who)
{
    turretDestroyed();
}

function e::turret::onDestroyed(%this, %who)
{
    turretDestroyed();   
}

//SCANNING STUFF
function navare::structure::onScan(%this, %scanner, %string)
{
    db("navare::structure::onScan()");
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    playSound(0, "GEN_OC01.wav", IDPRF_2D);
}

function navare::structure::onDestroyed(%this, %who)
{
    if(missionObjective3.status != *IDSTR_OBJ_COMPLETED)
    {
        missionObjective3.status = *IDSTR_OBJ_FAILED;
        schedule("forceToDebrief();",5.0);
    }
}

function cache::structure::onScan(%this, %scanner, %string)
{
    db("cache::structure::onScan()");
    
    //give the player some cool stuff
    InventoryWeaponAdjust(	-1,	102,	2	);	#Heavy Laser
    InventoryWeaponAdjust(	-1,	105,	2	);	#Emp
    InventoryWeaponAdjust(	-1,	110,	2	);	#Plasma
    InventoryWeaponAdjust(	-1,	120,	2	);	#Hvy Blast Can
    InventoryWeaponAdjust(	-1,	127,	2	);	#Sparrow 10
    InventoryWeaponAdjust(	-1,	128,	2	);	#SWARM 6
    InventoryWeaponAdjust(	-1,	129,	2	);	#Minion
    InventoryWeaponAdjust(	-1,	136,	2	);	#Proximity 15     
    
}  
 