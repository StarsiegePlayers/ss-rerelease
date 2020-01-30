//---- HC3 script file
//===========================================

//  Primary objectives
//  1.Rendezvous with Imperial Defense force at NAV ALPHA
//  2.Defend Sa Thauri outpost from attacking Cybrids

//  Secondary objectives
//  1. Order repair truck to "online" all 4 turrets
//  2. Defend Power Generators
//  3. Defend repair truck

DropPoint dropPoint1
{
	name = "foo";
	desc = "foo";
};

$db = false;

function db(%msg)
{
    if($db)
        echo(%msg);
}

Pilot Captain
{
   id = 28;
   name = Captain;
   skill = 1.0;
   accuracy = 1.0;
   aggressiveness = 0.2;
   activateDist = 300.0;
   deactivateBuff = 100.0;
   targetFreq = 1.0;
   trackFreq = 2.0;
   fireFreq = 0.9;
   LOSFreq = 0.8;
   orderFreq = 2.0;
};


MissionBriefInfo missionData
{
	title = 			    *IDSTR_HC3_TITLE;
    planet =                *IDSTR_PLANET_VENUS;           
	campaign = 			    *IDSTR_HC3_CAMPAIGN;		   
	dateOnMissionEnd =      *IDSTR_HC3_DATE; 			  
	shortDesc = 		    *IDSTR_HC3_SHORTBRIEF;	   
	longDescRichText = 	    *IDSTR_HC3_LONGBRIEF;		   
	media = 		 	    *IDSTR_HC3_MEDIA;
    nextMission =           "hd1";
    successDescRichText =   *IDSTR_HC3_DEBRIEF_SUCC;
    failDescRichText =      *IDSTR_HC3_DEBRIEF_FAIL;
    location =              *IDSTR_HC3_LOCATION;
    successWavFile =        "HC3_Debriefing.wav";
    soundvol =              "hc3.vol";
    endCinematicRec =       "cinHD.rec";
};

MissionBriefObjective missionObj1   // protect base
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HC3_OBJ1_SHORT;
	longTxt		= *IDSTR_HC3_OBJ1_LONG;
	bmpname		= *IDSTR_HC3_OBJ1_BMPNAME;
}; 

MissionBriefObjective missionObj2   // online turrets
{
	isPrimary 	= false;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HC3_OBJ2_SHORT;
	longTxt		= *IDSTR_HC3_OBJ2_LONG;
	bmpname		= *IDSTR_HC3_OBJ2_BMPNAME;
}; 

MissionBriefObjective missionObj3   // protect power
{
	isPrimary 	= false;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HC3_OBJ3_SHORT;
	longTxt		= *IDSTR_HC3_OBJ3_LONG;
	bmpname		= *IDSTR_HC3_OBJ3_BMPNAME;
}; 

function win()
{
    missionObj1.status = *IDSTR_OBJ_COMPLETED;
    missionObj2.status = *IDSTR_OBJ_COMPLETED;
    missionObj3.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(hc3);
    schedule("forceToDebrief();", 5.0);
}

//==ACTORS=====================================================================

// 1. player
// 2. Police Squad           POC
// 3. Repair Caravan         CAR
// 4. Tactical Venus Command TCV
// 5. cybrid group A         
// 6. cybrid group B    
// 7. cybrid group C
// 8. cybrid group D
// 9. cybrid group E

//-----------------------------------------------------------------------------
function onMissionStart()
{
    cdAudioCycle(ss4, Mechsoul, Terror);
}

function onSPClientInit()
{
    initFormations();
    initActors(); 
    venusSounds();
    earthquakeSounds();       
}

//-----------------------------------------------------------------------------
function initActors()
{
    initCybrid();
    initGuard();
    initRepair();
    initPlayer();
    initBase();
    initMfac();
}

//-----------------------------------------------------------------------------
function initFormations()
{
    newFormation(guardFormation, 0,0,0,     // 1st 
                                 20,-30,0,    // 2nd
                                -20,-30,0    // 3rd
                                );
                                
    newFormation(wideForm,0,0,0,            //1st
                          100,-20,0,
                          -100,-20,0);
                                
    newFormation(genericFormation, 0,0,0,     // 1st 
                                  20,0,0,    // 2nd
                                 -20,0,0    // 3rd
                                );

}

//**==PLAYER CLASS==***********************************************************

//-----------------------------------------------------------------------------
function initPlayer()
{
    $playerId = playerManager::playerNumToVehicleId($playerNum);
    $missionWon = false;
    $playerKilled = false;
       
    addGeneralOrder(*IDSTR_ORDER_HC3_1, "selectTurret(getObjectId(\"MissionGroup/mfac/turret1\"),1);");
    addGeneralOrder(*IDSTR_ORDER_HC3_2, "selectTurret(getObjectId(\"MissionGroup/mfac/turret2\"),2);");
    addGeneralOrder(*IDSTR_ORDER_HC3_3, "selectTurret(getObjectId(\"MissionGroup/mfac/turret3\"),3);");
    addGeneralOrder(*IDSTR_ORDER_HC3_4, "selectTurret(getObjectId(\"MissionGroup/mfac/turret4\"),4);");
	order("MissionGroup/mfac/turret1", ShutDown, true);
	order("MissionGroup/mfac/turret2", ShutDown, true);
	order("MissionGroup/mfac/turret3", ShutDown, true);
	order("MissionGroup/mfac/turret4", ShutDown, true);
    
    setTeam("MissionGroup/mfac/turret1", *IDSTR_TEAM_NEUTRAL);
	setTeam("MissionGroup/mfac/turret2", *IDSTR_TEAM_NEUTRAL);
	setTeam("MissionGroup/mfac/turret3", *IDSTR_TEAM_NEUTRAL);
	setTeam("MissionGroup/mfac/turret4", *IDSTR_TEAM_NEUTRAL);
	
    schedule("playerDistanceCheck();", 25.0);
    guardTalks("HC3_CAP1.wav", IDSTR_HC3_CAP1);
    schedule("setNavMarker(\"MissionGroup/navMarkers/navAlpha\", true, -1);", 2.0);     
}

//-----------------------------------------------------------------------------
function playerDistanceCheck()
{  
    if((%dist = getDistance($playerId, getObjectId("MissionGroup/navMarkers/navAlpha"))) > 1000)
    {
        if(%dist > 2000)
        {
            actorTalks(0, IDSTR_GEN_TCV2, "GEN_TCV02.wav");
            schedule("forceToDebrief();",5.0);
        }
        else
        {
            actorTalks(0, IDSTR_GEN_TCV1, "GEN_TCV01.wav");
            schedule("playerDistanceCheck();", 25.0);
        }
    }
}

//-----------------------------------------------------------------------------
function player::onAdd(%this, %who)
{
    $playerNum = %this;
}

//-----------------------------------------------------------------------------
function vehicle::onDestroyed(%this, %who)
{
    if(%this == $playerId)
    {
        $playerKilled = true;
    }
}

function actorTalks(%id, %txt, %snd)
{
    if(!($playerKilled))
    {
        Say(0, %id, *(%txt), %snd);
    }
}

//**==GUARD CLASS==***********************************************************
function initGuard()
{
    db("initGuard()");
    
    $guard = getObjectId("MissionGroup/guard");
    $guard.leader = getObjectId("MissionGroup/guard/guard1");
    $guard.last = getObjectId("MissionGroup/guard/route/last");
       
    order($guard.leader, MakeLeader, true);
    order($guard.leader, Speed, High);
    order($guard, Formation, guardFormation);
    $guard.engaged = false;
    
    schedule("actorTalks($guard.leader, IDSTR_HC3_POC01, \"HC3_POC01.wav\");", 1.0);
    schedule("guardBegin();", 13.0);
}   
   
//-----------------------------------------------------------------------------   
function guardBegin()
{
    order($guard.leader, Guard, "MissionGroup/guard/route");
    repairBegin();
    guardDistCheck();
}

function guard::vehicle::onArrived(%this, %where)
{
    if(%where == $guard.last)
    {
        order($guard.leader, Guard, %where);
        order($guard,Formation, wideForm);
    }
    if(%where == getObjectId("MissionGroup/guard/route/marker2"))
        schedule("startCybridE();", 100.0);
}

//-----------------------------------------------------------------------------   
function guard::vehicle::onAttacked(%this, %who)
{    
    if(!($guard.engaged) && %this != %who)
    {
        if(%who == $playerId)
            schedule("forceToDebrief();", 10.0);
       
        $guard.engaged = true;
        order($guard.leader, Attack, %who);
    }
}

//-----------------------------------------------------------------------------   
function guardTalks(%snd, %txt)
{
    if($guard.count > 0)
        say(0, $guardChannel, *(%txt), %snd);        
}

//-----------------------------------------------------------------------------
function guardDistCheck()
{
    if(getDistance($guard.leader, $cybridE.leader) < 700)
    {
        actorTalks($guard.leader, IDSTR_HC3_POC02, "HC3_POC02.wav"); 
    }       
    else
        schedule("guardDistCheck();", 10.0);      
}
                                   
//**==MFAC CLASS==***********************************************************
function initMfac()
{
    $mfac = getObjectId("MissionGroup/mfac");
    $mfac.id1 = getObjectId("MissionGroup/mfac/turret1");
    $mfac.id2 = getObjectId("MissionGroup/mfac/turret2");
    $mfac.id3 = getObjectId("MissionGroup/mfac/turret3");
    $mfac.id4 = getObjectId("MissionGroup/mfac/turret4");    
}

function mfac::turret::onDestroyed(%this, %who)
{
    if(%this == $repair.turretSelected)
        stopRepair();   
}

// BASE FUNCTIONS
function initBase()
{
    $base = getObjectId("MissionGroup/base");
    $command = getObjectId("MissionGroup/base/command");
    $power = getObjectId("MissionGroup/base/power");
    $barracks = getObjectId("MissionGroup/base/barracks");
    
    $command.attacked   = false;   
    $power.attacked     = false;
    $barracks.attacked  = false;
    
    $power.id1 = getObjectId("MissionGroup/base/power/power1");
    $power.id2 = getObjectId("MissionGroup/base/power/power2");
    $power.id3 = getObjectId("MissionGroup/base/power/power3");
    $power.id4 = getObjectId("MissionGroup/base/power/power4");
    
    $base.destroyed = 0;    
}

function command::structure::onAttacked(%this, %who)
{
    if(!$command.attacked)
        baseAttacked(%this, %who, command);        
}

function power::structure::onAttacked(%this, %who)
{
    if(!$power.attacked)
        baseAttacked(%this, %who, power);     
}

function barracks::structure::onAttacked(%this, %who)
{
    if(!$barracks.attacked)
        baseAttacked(%this, %who, barracks);      
}

function command::structure::onDestroyed(%this, %who)
{
    $base.destroyed++;
    checkBaseDestroyed();
}

function power::structure::onDestroyed(%this, %who)
{
    $base.destroyed++;
    missionObj3.status = *IDSTR_OBJ_FAILED;
    
    if(%this == $power.id1)
    {
        removeOrder(1);
        damageObject($mfac.id1, 10000);
    }
    
    if(%this == $power.id2)             
    {
        removeOrder(2);
        damageObject($mfac.id2, 10000);
    }

    if(%this == $power.id3)
    {
        removeOrder(3);
        damageObject($mfac.id3, 10000);
    }

    if(%this == $power.id4)
    {
        removeOrder(4);
        damageObject($mfac.id4, 10000);
    }
    checkBaseDestroyed();
}

function barracks::structure::onDestroyed(%this, %who)
{
    $base.destroyed++;
    checkBaseDestroyed();
}

function baseAttacked(%this, %who, %class)
{
    %chance = randomInt(1,10);
    db("baseAttacked() = " @ %chance);
    if(%chance > 5 && getTeam(%this) != getTeam(%who))
    {
        %snd = "";
        %txt = "";
        
        //switch 
        if(%class == command)
        {
            db("class = " @ %class);

            %snd = "HC3_WU01.wav";
            %txt = IDSTR_HC3_WU01;
            $command.attacked = true;
            schedule("$command.attacked = false;", 15.0); 
        }
        if(%class == power)
        {
            db("class = " @ %class);

            %snd = "HC3_WU03.wav";
            %txt = IDSTR_HC3_WU03;
            $power.attacked = true;
            schedule("$power.attacked = false;", 10.0);
        }
        if(%class == barracks)
        {
            db("class = " @ %class);

            %snd = "HC3_WU02.wav";
            %txt = IDSTR_HC3_WU02; 
            $barracks.attacked = true;
            schedule("$barracks.attacked = false;", 10.0);
        }
        
        actorTalks(0,%txt, %snd);
    }
}

function checkBaseDestroyed()
{
    if($base.destroyed > 3)
    {
        missionObj1.status = *IDSTR_OBJ_FAILED;
        schedule("forceToDebrief();", 5.0);
    }
}

//**==REPAIR CLASS==***********************************************************
function initRepair()
{
    order("MissionGroup/repair/repair1", Speed, Medium);
    $repair = getObjectId("MissionGroup/repair"); 
    $repair.id = getObjectId("MissionGroup/repair/repair1"); 
    db("Repair.id == " @ $repair.id);
    
    $repair.turretSelected = 0;
    $repair.currentOrder   = 0;
    $repair.briefing       = false;
    $repair.count          = 1;
    $repair.engaged        = false;
    $repair.onliningTurret = false;
    $repair.turretsOnlined = 0;  
}

//-----------------------------------------------------------------------------
function repairBegin()
{
    if($repair.turretSelected == 0)
    {
        order($repair.id, guard, $playerId);
        repairDistanceCheck();
    }
    else
        $repair.briefing = true;
}

//-----------------------------------------------------------------------------
function stopRepair()
{
    $repair.turretSelected = 0;
    removeOrder($repair.currentOrder);
    $repair.currentOrder   = 0;
    $repair.onliningTurret = false;
    order($repair.id, ShutDown, false);
    setHudTimer(-1, 0, *IDSTR_TIMER_HC3_1, 1);
}

//-----------------------------------------------------------------------------
function repairDistanceCheck()
{
    if(!$repair.briefing && $repair.turretSelected == 0)
    {
        if(getDistance($repair.id, $playerId) < 80)
        {
            $repair.briefing = true;
            order($repair.id, ShutDown, true);  
        }
        else
            schedule("repairDistanceCheck();", 4.0);       
    }
    
}

//-----------------------------------------------------------------------------
function selectTurret(%turretId, %orderNum)
{   
    db("TurretId = " @ %turretId);
        
    if($repair.onliningTurret)
        actorTalks($repair.id, IDSTR_HC3_CAR03, "HC3_CAR03.wav");
                    
    else
    { 
        db("turrets onlined = " @ $repair.turretsOnlined);
        
        $repair.currentOrder = %orderNum;
        order($repair.id, ShutDown, false);
        
        db("current order = " @ $repair.currentOrder);
                
        actorTalks($repair.id, IDSTR_HC3_CAR01, "HC3_CAR01.wav");  
        order($repair.id, Guard, getTurretMarkerId($repair.currentOrder)); 
        $repair.turretSelected = %turretId;  
    }       
}

function getTurretMarkerId(%n)
{
    return getObjectId("MissionGroup/repair/route/marker" @ %n); 
}

//-----------------------------------------------------------------------------
function turretOnlined(%turretId)
{
    if($repair.count > 0 && $repair.turretSelected != 0 && $repair.currentOrder != 0)
    {               
        db("turretOnlined() = " @ %turretId);
        order(%turretId, ShutDown, false);
        setTeam(%turretId, *IDSTR_TEAM_YELLOW);
        
        $repair.turretsOnlined++;
        if($repair.turretsOnlined == 4)
        {
            missionObj2.status = *IDSTR_OBJ_COMPLETED;
            playSound(0, "GEN_OC01.wav", IDPRF_2D);
            InventoryWeaponAdjust(-1,113,2);
        }
        
        actorTalks($repair.id, IDSTR_HC3_CAR02, "HC3_CAR02.wav");
        stopRepair();        
    }
}

//-----------------------------------------------------------------------------
function removeOrder(%orderNum)
{
    if(%orderNum == 1)
        removeGeneralOrder(*IDSTR_ORDER_HC3_1);
    
    if(%orderNum == 2)
         removeGeneralOrder(*IDSTR_ORDER_HC3_2);
    
    if(%orderNum == 3)
         removeGeneralOrder(*IDSTR_ORDER_HC3_3);
        
    if(%orderNum == 4)
         removeGeneralOrder(*IDSTR_ORDER_HC3_4);   
}

function getTurretId(%n)
{
    return getObjectId("MissionGroup/mfac/turret" @ %n);
}

//-----------------------------------------------------------------------------
function repair::vehicle::onArrived(%this, %where)
{
    if(%where == getTurretMarkerId($repair.currentOrder) && 
       !($repair.onliningTurret) && %where != 0)
    {
        db("onArrived marker id = " @ %where);
	    
        %turretId = getTurretId($repair.currentOrder);
        schedule("turretOnlined(" @ %turretId @ ");",30.0);
        $repair.onliningTurret = true;
        order($repair, ShutDown, true);
        
        setHudTimer(30, -1, *IDSTR_TIMER_HC3_1, 1);    
    }
}

//-----------------------------------------------------------------------------
function repair::vehicle::onDestroyed(%this, %who)                                 
{       
    killChannel(%this);
    db(%this @ " " @ $repair.id);
    for(%i=0;%i<4;%i++)
        removeOrder(%i+1);
        
    if($repair.onliningTurret)
        setHudTimer(-1, 0, *IDSTR_TIMER_HC3_1, 1);     
}

function cybridAttacks(%id)
{
    //pick random target
    %n = randomInt(1,4);
    %target = $playerId;
    
    if(%n == 2)
        %target = $power;
    if(%n == 3)
        %target = $command;
    if(%n == 4)
        %target = $barracks;
        
    %name = getObjectName(%target);
    db(%id @ " attacking " @ %name);
    
    order(%id, Attack, %target);
    order(%id, HoldPosition, true);
}

//--CYBRID A
function initCybridA()
{
    $cybridA = getObjectId("MissionGroup/cybridA");
    $cybridA.count = 3;
    $cybridA.route = getObjectId("MissionGroup/cybridA/route");
    $cybridA.last = getObjectId("MissionGroup/cybridA/route/last");
    $cybridA.leader = getObjectId("MissionGroup/cybridA/a1");
    $cybridA.attacked = false;
    
    order($cybridA.leader, MakeLeader, true);
    order($cybridA.leader, Speed, High);
    order($cybridA, Formation, genericFormation);
}

function a::vehicle::onAttacked(%this, %who)
{
    if( getTeam(%this) != getTeam(%who) && !$cybridA.attacked)
    {
        $cybridA.attacked = true;
        schedule("startCybridB();", 0.5);        
    }
}

function a::vehicle::onDestroyed(%this, %who)
{
    $cybridA.count--;
    checkHercsDestroyed();
}

function a::vehicle::onArrived(%this, %where)
{
    if(%where == $cybridA.last)
        cybridAttacks($cybridA.leader);
}

function startCybridA()
{
    db("startingCybridA()");
    order($cybridA.leader, Guard, $cybridA.route);
    schedule("order($cybridA, Cloak, true);", 20.0);
}

//--CYBRID B
function initCybridB()
{
    $cybridB = getObjectId("MissionGroup/cybridB");
    $cybridB.count = 2;
    $cybridB.route = getObjectId("MissionGroup/cybridB/route");
    $cybridB.last = getObjectId("MissionGroup/cybridB/route/last");
    $cybridB.leader = getObjectId("MissionGroup/cybridB/b1");
    $cybridB.attacked = false;
    
    order($cybridB.leader, MakeLeader, true);
    order($cybridB.leader, Speed, High);
    order($cybridB, Formation, genericFormation);
}

function b::vehicle::onAttacked(%this, %who)
{
    if( getTeam(%this) != getTeam(%who) && !$cybridB.attacked)
    {
        $cybridB.attacked = true;
        schedule("startCybridC();", 1.0);        
    }
}

function b::vehicle::onDestroyed(%this, %who)
{
    $cybridB.count--;
    checkHercsDestroyed();
}

function b::vehicle::onArrived(%this, %where)
{
    if(%where == $cybridB.last)
        cybridAttacks($cybridB.leader);
}

function startCybridB()
{
    db("startingCybridB()");
    order($cybridB.leader, Guard, $cybridB.route);
}

//--CYBRID C
function initCybridC()
{
    $cybridC = getObjectId("MissionGroup/cybridC");
    $cybridC.count = 3;
    $cybridC.route = getObjectId("MissionGroup/cybridC/route");
    $cybridC.last = getObjectId("MissionGroup/cybridC/route/last");
    $cybridC.leader = getObjectId("MissionGroup/cybridC/c1");
    $cybridC.attacked = false;
    
    order($cybridC.leader, MakeLeader, true);
    order($cybridC.leader, Speed, High);
    order($cybridC, Formation, genericFormation);
}

function c::vehicle::onAttacked(%this, %who)
{
    if( getTeam(%this) != getTeam(%who) && !$cybridC.attacked)
    {
        $cybridC.attacked = true;
        schedule("startCybridD();", 1.0);        
    }
}

function c::vehicle::onDestroyed(%this, %who)
{
    $cybridC.count--;
    checkHercsDestroyed();
}

function c::vehicle::onArrived(%this, %where)
{
    if(%where == $cybridC.last)
        cybridAttacks($cybridC.leader);
}

function startCybridC()
{
    db("startingCybridC()");
    order($cybridC.leader, Guard, $cybridC.route);
}

//--CYBRID D
function initCybridD()
{
    $cybridD = getObjectId("MissionGroup/cybridD");
    $cybridD.count = 2;
    $cybridD.route = getObjectId("MissionGroup/cybridD/route");
    $cybridD.last = getObjectId("MissionGroup/cybridD/route/last");
    $cybridD.leader = getObjectId("MissionGroup/cybridD/d1");
    
    order($cybridD.leader, MakeLeader, true);
    order($cybridD.leader, Speed, High);
    order($cybridD, Formation, genericFormation);
}

function d::vehicle::onDestroyed(%this, %who)
{
    $cybridD.count--;
    checkHercsDestroyed();
}

function d::vehicle::onArrived(%this, %where)
{
    if(%where == $cybridD.last)
        cybridAttacks($cybridD.leader);  
}

function startCybridD()
{
    db("startingCybridD()");
    db("leader = " @ $cybridD.leader);
    db("route = " @ $cybridD.route);
    order($cybridD.leader, Guard, $cybridD.route);
}

//--CYBRID E
function initCybridE()
{
    $cybridE = getObjectId("MissionGroup/cybridE");
    $cybridE.count = 3;
    $cybridE.route = getObjectId("MissionGroup/cybridE/route");
    $cybridE.last = getObjectId("MissionGroup/cybridE/route/last");
    $cybridE.leader = getObjectId("MissionGroup/cybridE/e1");
    
    order($cybridE.leader, MakeLeader, true);
    order($cybridE.leader, Speed, High);
    order($cybridE, Formation, genericFormation); 
}

function e::vehicle::onDestroyed(%this, %who)
{
    $cybridE.count--;
    checkHercsDestroyed();
}

function e::vehicle::onArrived(%this, %where)
{
    if(%where == $cybridE.last)
        cybridAttacks($cybridE.leader);  
}

function startCybridE()
{
    db("startingCybridE()");
    db("leader = " @ $cybridE.leader);
    db("route = " @ $cybridE.route);
    order($cybridE.leader, Guard, $cybridE.route);
}


function checkHercsDestroyed()
{
    if($cybridA.count == 0 && $cybridB.count == 0 && $cybridC.count == 0 &&
       $cybridD.count == 0 && $cybridE.count == 0 )
    {
        if(missionObj3.status != *IDSTR_OBJ_FAILED)
            missionObj3.status = *IDSTR_OBJ_COMPLETED;
        
        missionObj1.status = *IDSTR_OBJ_COMPLETED;
        updatePlanetInventory(hc3);
        schedule("forceToDebrief();", 5.0);
    }       
}

function initCybrid()
{
    initCybridA();
    initCybridB();
    initCybridC();
    initCybridD();
    initCybridE();
    
    schedule("startCybridA();", 5.0);
}