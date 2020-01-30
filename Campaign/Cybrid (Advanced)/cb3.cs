//---- CB3 script file
//===========================================


DropPoint dropPoint1
{
	name = "foo";
	desc = "foo";
};

Pilot Supply 
{
   id = 28;
   
   name = "Turret supplies";
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

Pilot Priority 
{
   id = 29;
   
   name = "Priority supplies";
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
	title = 			    *IDSTR_CB3_TITLE;
    planet =                *IDSTR_PLANET_MOON;           
	campaign = 			    *IDSTR_CB3_CAMPAIGN;		   
	dateOnMissionEnd =      *IDSTR_CB3_DATE; 			  
	shortDesc = 		    *IDSTR_CB3_SHORTBRIEF;	   
	longDescRichText = 	    *IDSTR_CB3_LONGBRIEF;		   
	media = 		 	    *IDSTR_CB3_MEDIA;
    nextMission =           *IDSTR_CB3_NEXTMISSION;
    successDescRichText =   *IDSTR_CB3_DEBRIEF_SUCC;
    failDescRichText =      *IDSTR_CB3_DEBRIEF_FAIL;
    location =              *IDSTR_CB3_LOCATION;
    soundvol =              "cB3.vol";
    successWavFile =        "CB3_debriefing.wav";
};

MissionBriefObjective missionObj1   //Destroy weapons depot at Nav001
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_CB3_OBJ1_SHORT;
	longTxt		= *IDSTR_CB3_OBJ1_LONG;
	bmpname		= *IDSTR_CB3_OBJ1_BMPNAME;
};

MissionBriefObjective missionObj2   //Destroy the escaping depot convoy
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_IGNORE;
	shortTxt	= *IDSTR_CB3_OBJ2_SHORT;
	longTxt		= *IDSTR_CB3_OBJ2_LONG;
	bmpname		= *IDSTR_CB3_OBJ2_BMPNAME;
}; 

MissionBriefObjective missionObj3   //Destroy all convoys you encounter
{
	isPrimary 	= false;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_CB3_OBJ3_SHORT;
	longTxt		= *IDSTR_CB3_OBJ3_LONG;
	bmpname		= *IDSTR_CB3_OBJ2_BMPNAME;
}; 
 
function winCheck()
{
    if(missionObj1.status == *IDSTR_OBJ_COMPLETED && 
        missionObj2.status == *IDSTR_OBJ_COMPLETED && isSafe(*IDSTR_TEAM_YELLOW,$playerNum.id,700))
    {
        updatePlanetInventory(cb3);
        forceToDebrief();    
    }
    else
        schedule("winCheck();",5.0);
} 
 
function win()
{
    missionObj1.status = *IDSTR_OBJ_COMPLETED;
    missionObj2.status = *IDSTR_OBJ_COMPLETED;
    missionObj3.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(cb3);
    schedule("forceToDebrief();", 5.0);
}


function onMissionStart()
{    
     cdAudioCycle(Newtech, ss4, Gnash); // placeholder   
}

function onSPClientInit()
{
    newFormation(genform, 0,0,0,
                          35,0,0,
                          -35,0,0);
    $DB = true;
    initActors(); 
    moonSounds();
    meteorSounds();    
}

function initActors()
{
    initPlayer();
    ambientFX();    
    initConvoy();
    initHs1();
    initDepot();   
}

function db(%msg)
{
    if($DB)
        echo(%msg);
}

function ambientFX()
{
    ambDrop($playerNum.id);
    schedule("ambientFX();",randomInt(10,20));
    //shutup();
}

//PLAYER FUNCTIONS
function initPlayer()
{
    db("initPlayer()");
    $playerNum.killed = false;
    $nav001 = getObjectId("MissionGroup/navMarkers/nav001");
    schedule("setNavMarker($nav001, true,-1);",3.0); 
    schedule("distCheck($playerNum.id, $nav001, 300, arrivedAtNav1, 5, close);", 10.0);
    schedule("distCheck($playerNum.id, $nav001, 5500, boundaryWarn, 10,far);",10.0);
    schedule("distCheck($playerNum.id, $nav001, 8000, boundaryWarn, 10,far);",15.0);  
    //playsound proceed to nav001
    playSound(0, "CYB_NEX08.wav", IDPRF_2D);
}

function arrivedAtNav1()
{
    db("arrivedAtNav1()");
    setNavMarker($nav001, false);
    startConvoy($convoy.id3);
}

function boundaryWarn()
{
    db("boundaryWarn()");
    //generic boundary warning
    actorTalks(0,IDSTR_CYB_NEX01,"CYB_NEX01.wav");
}

function boundaryFail()
{
    db("boundaryFail()");
    //generic fail
    actorTalks(0,IDSTR_CYB_NEX02,"CYB_NEX02.wav");
    schedule("forceToDebrief();",5.0);
}
function ambDrop(%id)
{
    db("ambDrop() = " @ %id);
    %pi = 3.1415926;
    %x = getPosition(%id,x);
    %y = getPosition(%id,y);
    %rot  = getPosition(%id,rot); 
    %inc = 2000 + randomInt(-500,500); 
    %juke = randomInt(-1000,1000);   
    
    db(%rot);
    if(%rot >= -(%pi/4) && %rot <= %pi/4)
    {   db(1);
        %x -= %juke;
        %y += %inc;
    }   
    
    else if(%rot >= %pi/4 && %rot <= %pi*3/4)
    {
        db(2);
        %x -= %inc;
        %y += %juke;
    }
    else if(%rot >= %pi*3/4 || %rot <= -(%pi*3/4)) 
    {
        db(3);
        %x += %juke;
        %y -= %inc;
    }
    else if(%rot <= -(%pi/4) && %rot >= -(%pi*3/4))
    {
        db(4);
        %x += %inc;
        %y += %juke;
    }
    
    %z = getTerrainHeight(%x, %y);
    db(%x @ ", " @ %y @ ", " @ %z);
    droppod(%x,%y,%z);               
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
    
    db(getObjectName(getGroup(%id1)) @ " is " @ %dist @ " from " @ getObjectName(getGroup(%id2))); 
    if(compare(%dist,%maxDist,%op)) schedule(%callBack @ "("@%params@");",1.0);
    else
         schedule("distCheck(" @ %id1 @ "," @ %id2 @ "," @ %maxDist @ "," @ %callBack @ "," @ %time @ "," @ %dir @ ");", %time);                        
}

//CONVOY FUNCTIONS
function initConvoy()
{
    db("initConvoy()");
    $convoy = getObjectId("MissionGroup/convoy");
    $convoy.id1 = getObjectId("MissionGroup/convoy/c1/convoy1");
    $convoy.id2 = getObjectId("MissionGroup/convoy/c2/convoy1");
    $convoy.id3 = getObjectId("MissionGroup/convoy/c3/convoy1");
    $convoy.id4 = getObjectId("MissionGroup/convoy/c4/convoy1");
    $convoy.id5 = getObjectId("MissionGroup/convoy/c5/convoy1");
    $convoy.id6 = getObjectId("MissionGroup/convoy/c6/convoy1");
    $convoy.id7 = getObjectId("MissionGroup/convoy/c7/convoy1");
    
    $convoy.route = getObjectId("MissionGroup/convoy/route/marker1");
    $convoy.escaped = 0;
    $convoy.destroyed = 0;
    $mission::suppliesDestroyed = 0;
        
    $convoy.id1.attacked = false;
    $convoy.id4.attacked = false;
    
    $convoy.id1.dist = 2000;
    $convoy.id2.dist = 2000;
    $convoy.id3.dist = 2000;
    $convoy.id4.dist = 2000;
    $convoy.id5.dist = 2000;
    $convoy.id6.dist = 2000;
    $convoy.id7.dist = 2000;
    
    $convoy.id2.started = false;
    $convoy.id3.started = false;
    $convoy.id2.attacked = false;
    $convoy.id5.attacked = false;
    
    order($convoy.id1, MakeLeader, true);
    order("MissionGroup/convoy/c1", Formation, genform);
    
    order($convoy.id4, MakeLeader, true);
    order("MissionGroup/convoy/c4", Formation, genform);
    
    order($convoy.id5, MakeLeader, true);
    order("MissionGroup/convoy/c5", Formation, genform);

    order($convoy.id6, MakeLeader, true);
    order("MissionGroup/convoy/c6", Formation, genform);
    
    order($convoy.id7, MakeLeader, true);
    order("MissionGroup/convoy/c7", Formation, genform);
 
    order($convoy.id1, Speed, Low);
    order($convoy.id2, Speed, Low);
    order($convoy.id3, Speed, High);
    order($convoy.id4, Speed, Low);
    order($convoy.id5, Speed, Medium);
    order($convoy.id6, Speed, Medium);
    order($convoy.id7, Speed, Low);
    
    // convoys encountered enroute
    schedule("startConvoy($convoy.id1);", 80);
    schedule("startConvoy($convoy.id4);", 60);
    schedule("startConvoy($convoy.id5);", 40);
    schedule("startConvoy($convoy.id6);", 10);
    schedule("startConvoy($convoy.id7);", 1);

    // important convoy
    schedule("startConvoy($convoy.id2);", 180); 
}

function startConvoy(%id)
{   
    db("startConvoy() = " @ getObjectName($convoy.id2));
    if(%id == $convoy.id2)
    {
        if(%id.started)
            return;
            
        %id.started = true;    
        actorTalks(0,IDSTR_CB3_NEX01, "CB3_NEX01.wav");
        order($convoy.id2,Speed,Low);
        schedule("order($convoy.id2, Guard, $convoy.route);",1.0); 
        missionObj2.status = *IDSTR_OBJ_ACTIVE;
    }
    
    if(%id == $convoy.id3)
    {
        if(%id.started)
            return;
        %id.started = true;
        order(%id, Guard, $convoy.route); 
    }
    
    else
        order(%id, Guard, $convoy.route);
    
    //check to see if convoy has escaped
    distCheck($playerNum.id, %id, %id.dist, escapeConvoy, 10, far, %id);
}

function escapeConvoy(%id)
{
    // important convoy escaped
    db("escapeConvoy()");
    if(isGroupDestroyed(%id))
        return;
    
    if(%id == $convoy.id2 )
    {
            schedule("forceToDebrief();", 5.0);
            missionObj2.status = *IDSTR_OBJ_FAILED;           
    } 
    
    else
    {
        //droppod($convoy.route,%id);
        $convoy.escaped++;
    } 
}

function convoy::vehicle::onAttacked(%this,%who)
{
    if(%this.attacked)
        return;
    
    if(%this == $convoy.id2)
        playSound(0,"cyb_cvc03.wav",IDPRF_2D);
        
    if(%this == $convoy.id5)
        playSound(0,"cyb_cva01.wav",IDPRF_2D);
        
    %this.attacked = true;
}

function convoy::vehicle::onDestroyed(%this, %who)
{
    $mission::suppliesDestroyed++;
    db("supplies destroyed = " @ $mission::suppliesDestroyed);
    $convoy.destroyed++;
    if($convoy.destroyed == 6)
    {
        playSound(0, "CYB_NEX17.wav", IDPRF_2D);
        missionObj3.status = *IDSTR_OBJ_COMPLETED;
    }
    
    if(%this == $convoy.id2)
    {
        missionObj2.status = *IDSTR_OBJ_COMPLETED;
        //actorTalks "Mission Complete"
        playSound(0, "CYB_NEX17.wav", IDPRF_2D);
    } 
    order(getGroup(%this),Attack, %who);   
}
// HS1 FUNCTION
function initHs1()
{
    db("initHs1()");
    $hs1 = getObjectId("MissionGroup/hs1");
    $hs1.leader = getObjectId("MissionGroup/hs1/1hs1");
    $hs1.attacked = false;
    $hs1.route = getObjectId("MissionGroup/hs1/route");
    
    order($hs1.leader, MakeLeader,true);
    order($hs1.leader, Speed, High);
    order($hs1, Formation, genform);
    order($hs1.leader,Guard, $hs1.route);
}

function hs1::vehicle::onAttacked(%this, %who)
{
    if(!$hs1.attacked && getTeam(%this) != getTeam(%who))
    {
        startConvoy($convoy.id3);
        $hs1.attacked = true;
        actorTalks($hs1.leader,IDSTR_GEN_ICC3, "GEN_ICCA03.wav");
        actorTalks($hs1.leader,IDSTR_GEN_ICC10, "GEN_ICCA10.wav");
        order($hs1.leader, Attack, pick("PLAYERSQUAD"));
    }
}

// DEPOT FUNCTIONS
function initDepot()
{
    db("initDepot()");
    $depot = getObjectId("MissionGroup/base");
    $depot.destroyed = 0;
    $depot.buildings = 0;
}

function depot::structure::onDestroyed(%this,%who)
{
    $depot.destroyed++;
    db();
    if($depot.destroyed == 5)
    {
        missionObj1.status = *IDSTR_OBJ_COMPLETED;
        playSound(0, "CYB_NEX17.wav", IDPRF_2D);
        schedule("winCheck();",5.0);
    }
}



 




