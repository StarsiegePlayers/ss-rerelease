//---- CD3 script file
//===========================================


DropPoint dropPoint1
{
	name = "foo";
	desc = "foo";
};

Pilot Nexus3220 
{
   id = 29;
   
   name = "Nexus-3220";
   skill = 0.7;
   accuracy = 0.1;
   aggressiveness = 0.2;
   activateDist = 300.0;
   deactivateBuff = 100.0;
   targetFreq = 4.0;
   trackFreq = 1.0;
   fireFreq = 4.0;    
   LOSFreq = 0.4;
   orderFreq = 4.0;
};



MissionBriefInfo missionData
{
	title = 			    *IDSTR_CD3_TITLE;
    planet =                *IDSTR_PLANET_TEMPERATE;           
	campaign = 			    *IDSTR_CD3_CAMPAIGN;		   
	dateOnMissionEnd =      *IDSTR_CD3_DATE; 			  
	shortDesc = 		    *IDSTR_CD3_SHORTBRIEF;	   
	longDescRichText = 	    *IDSTR_CD3_LONGBRIEF;		   
	media = 		 	    *IDSTR_CD3_MEDIA;
    nextMission =           *IDSTR_CD3_NEXTMISSION;
    successDescRichText =   *IDSTR_CD3_DEBRIEF_SUCC;
    failDescRichText =      *IDSTR_CD3_DEBRIEF_FAIL;
    location =              *IDSTR_CD3_LOCATION;
    soundvol =              "cd3.vol";
    successWavFile =        "CD3_debriefing.wav";
};

MissionBriefObjective missionObj1  // Clear path for the advancing Nexus to Nav001
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_CD3_OBJ1_SHORT;
	longTxt		= *IDSTR_CD3_OBJ1_LONG;
	bmpname		= *IDSTR_CD3_OBJ1_BMPNAME;
};

MissionBriefObjective missionObj2  // Destroy all resistance encountered
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_CD3_OBJ2_SHORT;
	longTxt		= *IDSTR_CD3_OBJ2_LONG;
	bmpname		= *IDSTR_CD3_OBJ2_BMPNAME;
}; 

MissionBriefObjective missionObj3   // Protect nexus
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_CD3_OBJ3_SHORT;
	longTxt		= *IDSTR_CD3_OBJ3_LONG;
	bmpname		= *IDSTR_CD3_OBJ3_BMPNAME;
}; 
  
function win()
{
    missionObj1.status = *IDSTR_OBJ_COMPLETED;
    missionObj2.status = *IDSTR_OBJ_COMPLETED;
    missionObj3.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(cd3);
    schedule("forceToDebrief();", 1.0);
}

function onMissionStart()
{
     cdAudioCycle(Newtech, ss4, Gnash); // placeholder     
}

function onSPClientInit()
{
    newFormation(genform, 0,0,0,
                          15,0,0,
                         -15,0,0,
                         -30,0,0,
                          30,0,0);                            
    $DB = true;
    initActors();
    temperateSounds();       
}

function initActors()
{
    initPlayer();
    initCybrid();
    initBase();
    initFlyer();
    initAmbush();
    initImperial();
}

function db(%msg)
{
    if($DB)
        echo(%msg);
}

//PLAYER FUNCTIONS
function initPlayer()
{
    db("initPlayer()");
    $playerNum.killed = false;
    schedule("ambDrop();",5.0);
    
    $nav001 = getObjectId("MissionGroup/navMarkers/nav001");
    setNavMarker($nav001,true,-1);
    distCheck($playerNum.id, $nav001, 200, turnOffNavMarker, 5, close, $nav001);
}

function turnOffNavMarker(%navId)
{
    setNavMarker(%navId,false,-1);
}

function ambDrop()
{
    dropNearPlayer(0,true,2000);
    schedule("ambDrop();",randomInt(15,20));    
}

function boundaryWarn()
{
    db("boundaryWarn()");
    actorTalks(0,IDSTR_CYB_NEX01,"CYB_NEX01.wav");    
}

function boundaryFail()
{
    db("boundaryFail()");
    actorTalks(0,IDSTR_CYB_NEX02,"CYB_NEX02.wav");
    schedule("forceToDebrief();",5.0);
}

function player::onAdd(%this)
{
    $playerNum = %this;
}

function vehicle::onAdd(%this)
{
    if($playerNum == playerManager::vehicleIdToPlayerNum(%this))
        $playerNum.id = %this;   
}

function vehicle::onDestroyed(%this, %who)
{
    if(%this == $playerNum.id)
        $playerNum.killed = true;
}

function actorTalks(%id, %txt, %snd)
{
    if(!$playerNum.killed)
        say(0,%id, *(%txt), %snd);
}

function setGroupTeam(%group,%team)
{
    //%team needs to be a non-dereferenced tag
    %cm = 0;
    while(%cm = getNextObject(%group,%cm))
        setTeam(%cm,*%team);
}

function compare(%a,%b,%op)
{
    //db(%a @", " @ %b);
    if(%op == ">")
    {
        if(%a > %b) return true;
        else return false;
    }
    
    if(%op == ">=")
    {
        if(%a >= %b) return true;
        else return false;
    }

    
    if(%op == "<")
    {
        if(%a<%b) return true;
        else return false;
    }
    
    if(%op == "<=")
    {
        if(%a <= %b) return true;
        else return false;
    }
    
    if(%op == "==")
    {
        if(%a == %b) return true;
        else return false;
    }
    else
        db(%op @ " is not a valid operator!");
}

function distCheck(%id1, %id2, %maxDist, %callBack, %time, %dir, %params)
{
    %dist = getDistance(%id1,%id2);  
    if(%dir == close) %op = "<";
    if(%dir == far) %op = ">"; 
    if(compare(%dist,%maxDist,%op)) schedule(%callBack @ "("@%params@");",1.0);
    else
         schedule("distCheck(" @ %id1 @ "," @ %id2 @ "," @ %maxDist @ "," @ %callBack @ "," @ %time @ "," @ %dir @ ");", %time);                        
}

function dropNearPlayer(%id,%vis,%dist)
{
    %pi = 3.1415926;
    %x = getPosition($playerNum.id,x);
    %y = getPosition($playerNum.id,y);
    %rot  = getPosition(%id,rot); 
    %inc = %dist + randomInt(-%dist/5,%dist/5); 
    %juke = randomInt(-%dist/2,%dist/2);   
    
    if(!%vis) %inc = -%inc;
    
    if(%rot >= -(%pi/4) && %rot <= %pi/4)
    {               
        %x -= %juke;
        %y += %inc;
    }   
    
    else if(%rot >= %pi/4 && %rot <= %pi*3/4)
    {
        %x -= %inc;
        %y += %juke;
    }
    else if(%rot >= %pi*3/4 || %rot <= -(%pi*3/4)) 
    {
        %x += %juke;
        %y -= %inc;
    }
    else if(%rot <= -(%pi/4) && %rot >= -(%pi*3/4))
    {
        %x += %inc;
        %y += %juke;
    }
    %z = getTerrainHeight(%x, %y);
    
    
    if(!%id) droppod(%x,%y,%z);               
    else setPosition(%id,%x,%y,%z);
}

function checkWin()
{
    if(missionObj1.status == *IDSTR_OBJ_COMPLETED && missionObj2.status == *IDSTR_OBJ_COMPLETED &&
        !isGroupDestroyed($cybrid.nexus))
    {
        playSound(0,"CYB_NEX04.wav",IDPRF_2D);
        missionObj3.status = *IDSTR_OBJ_COMPLETED;
        schedule("forceToDebrief();",5.0);
        updatePlanetInventory(cd3);
    }
    else schedule("checkWin();");        
}

//CYBRID STUFF
function initCybrid()
{
    db("initCybrid()");
    $cybrid = getObjectId("MissionGroup/cybrid");
    $cybrid.nexus = getObjectId("MissionGroup/cybrid/nexus");
    $cybrid.attacked = false;
    $cybrid.convertedTurrets = false;
    $cybrid.route = getObjectId("MissionGroup/cybrid/route");
    $cybrid.last = getObjectId("MissionGroup/cybrid/route/last");
    
    order($cybrid.nexus,Speed,Low);
    order($cybrid.nexus,Guard,$cybrid.route);
    
    distCheck($cybrid.nexus, $cybrid.last, 100, beginConversion, 5, close);
    schedule("distCheck($cybrid.nexus, $cybrid.last, 800, triggerASquad, 6, close);",5.0);
    
    actorTalks($cybrid,IDSTR_CD3_MBL01,"CD3_MBL01.wav");
    order($cybrid.nexus,zigzag,false);
}

function beginConversion()
{
    db("beginConversion()");
    order($cybrid.nexus,clear);
    schedule("finishConversion();",20.0); 
    setHudTimer(20, -1, *IDSTR_TIMER_CD3_1, 1);
    
    actorTalks($cybrid,IDSTR_CD3_MBL02,"CD3_MBL02.wav");    
}

function finishConversion()
{
    // if the nexus is still alive, and the turrets haven't been offlined 
    if(!isGroupDestroyed($cybrid.nexus) && !$base.turretsOfflined && $base.turretCount)
    {
        setGroupTeam($base.turret,IDSTR_TEAM_YELLOW);
        $cybrid.convertedTurrets = true;
        addGeneralOrder(*IDSTR_ORDER_CD3_1,"directTurretFire();");
        actorTalks($cybrid,IDSTR_CD3_MBL03,"CD3_MBL03.wav");
    }    
} 

function directTurretFire()
{
    order($base.turret,Attack, getTargetId($playerNum.id));
}

function cybridNotify()
{
    playSound(0,"cyb_nex06.wav",IDPRF_2D);    
}

function cybrid::vehicle::onDestroyed(%this,%who)
{
    missionObj3.status = *IDSTR_OBJ_FAILED;
    schedule("forceToDebrief();",8.0);
    if($cybrid.convertedTurrets)
    {
        setTeam($base.turret,IDSTR_TEAM_RED);
        removeGeneralOrder(*IDSTR_ORDER_CD3_1);
        setHudTimer(-1, 0, *IDSTR_TIMER_CD3_1,1);
    }
}

function cybrid::vehicle::onAttacked(%ths,%who)
{
    if(getTeam(%this) != getTeam(%who) && !$cybrid.attacked)
    {
        order($cybrid.nexus,Speed,Medium);
        $cybrid.attacked = true;
    }
}

// BASE STUFF
function initBase()
{
    db("initBase()");
    $base = getObjectId("MissionGroup/base");
    $base.turret = getObjectId("MissionGroup/base/turret");
    $base.turretCount = 3;
    $base.turretsOfflined = false; 
}

function turret::onDestroyed(%this,%who)
{
    $base.turretCount--;
    if(!$base.turretCount && $cybrid.convertedTurrets)  
        removeGeneralOrder(*IDSTR_ORDER_CD3_1);
}

function turretcontrol::structure::onDestroyed(%this,%who)
{
    if($cybrid.convertedTurrets)
        removeGeneralOrder(*IDSTR_ORDER_CD3_1);
        
    $base.turretsOfflined = true;
    %cm = 0;
    while(%cm = getNextObject($base.turret,%cm))
    {
        //setStaticShapeName(%cm,IDACS_C_NOPOWERTURRET);
        setTeam(%cm,*IDSTR_TEAM_NEUTRAL);
        order(%cm,clear,true);
    }    
}

// FLYER STUFF
function initFlyer()
{
    db("initFlyer()");
    $flyer = getObjectId("MissionGroup/flyer");
    $flyer.id = getObjectId("MissionGroup/flyer/flyer1");
    $flyer.id2 = getObjectId("MissionGroup/flyer/flyer2");
    $flyer.last = getObjectId("MissionGroup/flyer/last");
    $flyer.spottedNexus = false;
    order($flyer.id,Shutdown,false);
    schedule("order($flyer.id,Guard,$cybrid.nexus);",7.0);
    schedule("order($flyer.id2,Guard,$cybrid.nexus);",8.5);
    
    schedule("distCheck($cybrid.nexus, $flyer.id, 300, flyerSpotsNexus,5, close);",2.0);
    setHercOwner(getObjectId("PLAYERSQUAD"),$flyer.id);
}

function flyerSpotsNexus()
{
    if($flyer.spottedNexus)
        return;
        
    db("flyerSpotsNexus()");    
    schedule("ambushNexus();",2.0);
    order($flyer.id,Speed,Medium);
    order($flyer.id, Guard,$flyer.last);
    order($flyer.id2,Speed,Medium);
    order($flyer.id2, Guard,$flyer.last);   
    schedule("actorTalks($flyer,IDSTR_CD3_BMB01,\"CD3_BMB01.wav\");",randomFloat(0.5,1.0));
    $flyer.spottedNexus = true;    
}

function flyer::vehicle::onAttacked(%this,%who)
{
    if(getTeam(%this)!=getTeam(%who))
    {
        flyerSpotsNexus();
        $flyer.spottedNexus = true; 
    }
}

// AMBUSH STUFF
function initAmbush()
{
    $ambush = getObjectId("MissionGroup/ambush");
    $ambush.count = 2;
    $ambush.attacked = false;
    
    %cm = 0;
    while(%cm = getNextObject($ambush,%cm))
        setVehicleRadarVisible(%cm,false);
}

function ambush::vehicle::onAttacked(%this,%who)
{
    if(!$ambush.attacked && !$flyer.spottedNexus)
    {
        ambushNexus();
        $ambush.attacked = true;
    }
}

function ambush::vehicle::onDestroyed(%this,%who)
{
    $ambush.count--;
}

function ambushNexus()
{
    if($ambush.attacked)
        return;

    db("ambushNexus()");
        
    %cm = 0;
    while(%cm = getNextObject($ambush,%cm))
    {
        setVehicleRadarVisible(%cm,true);
        order(%cm,Speed,High);
        order(%cm, Attack, $cybrid.nexus);
    }
    schedule("actorTalks($ambush,IDSTR_CD3_PLT01,\"CD3_PLT01.wav\");",randomFloat(1.0,1.5)); 
}

// IMPERIAL STUFF
function initImperial()
{
    db("initImperial()");
    $imperial = getObjectId("MissionGroup/imperial");
    $imperial.a = getObjectId("MissionGroup/imperial/a");
    $imperial.b = getObjectId("MissionGroup/imperial/b");
    $imperial.c = getObjectId("MissionGroup/imperial/c");
    $imperial.d = getObjectId("MissionGroup/imperial/d");
    
    $imperial.a.route = getObjectId("MissionGroup/imperial/a/route");
    $imperial.b.route = getObjectId("MissionGroup/imperial/b/drop");
    $imperial.c.route = getObjectId("MissionGroup/imperial/c/drop");
    $imperial.d.route = getObjectId("MissionGroup/imperial/d/drop");
    
    order($imperial.a,Guard, $imperial.a.route);
    
    $imperial.count = 10; 
}

function imperial::vehicle::onDestroyed(%this,%who)
{
    $imperial.count--;
    
    if($imperial.count == 9)
        schedule("playSound(0,\"cyb_gn61.wav\",IDPRF_2D);",randomFloat(1,2));
    
    if($imperial.count == 8)
    {
        schedule("dropGroup($imperial.b);",randomInt(2,5));
        missionObj1.status = *IDSTR_OBJ_COMPLETED;
        playSound(0,"CYB_NEX17.wav",IDPRF_2D);
    }
    
    if($imperial.count == 5)
        schedule("dropGroup($imperial.c);",randomInt(8,12));
        
    if($imperial.count == 3)
        schedule("dropGroup($imperial.d);",randomInt(8,12));
    
    if(!$imperial.count)
    {
        missionObj2.status = *IDSTR_OBJ_COMPLETED;
        playSound(0,"CYB_NEX17.wav",IDPRF_2D);
        checkWin();
    }
}
     
function triggerASquad()
{
    order($imperial.a,Guard,$playerNum.id);
    //order($imperial.a,Holdposition,true);
    playSound(0,"gen_icca03.wav",IDPRF_2D);
    schedule("playSound(0,\"gen_icca10.wav\",IDPRF_2D);",randomFloat(2.5,3.5));
}

function dropGroup(%group)
{
    %pos  = getNextObject(%group.route,0);
    %tmp = %pos;
    
    // find dropsite furthest away from player
    while(%tmp = getNextObject(%group.route,%tmp))
    {
        if(getDistance(%tmp,$playerNum.id) > getDistance(%pos,$playerNum.id))
            %pos = %tmp;
    }
    
    %cm   = 0;
    %i    = 0;

    while(%cm = getNextObject(%group,%cm))
    {
        %x = getPosition(%pos,x) + (%i * 20);
        %y = getPosition(%pos,y) + (%i * 20);
        %z = getTerrainHeight(%x,%y);
        
        setPosition(%cm,%x,%y,%z);
        %i++;
    }
    
    distCheck($cybrid.nexus,getNextObject(%group,0),800,cybridNotify,3,close);    
    order(%group,Guard, $playerNum.id);
} 

