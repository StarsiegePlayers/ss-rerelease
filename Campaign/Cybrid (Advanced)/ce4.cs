//---- CE4  script file
//===========================================


DropPoint dropPoint1
{
	name = "foo";
	desc = "foo";
};

Pilot Harabec
{
   id = 29;
   
   name = Harabec;
   skill = 1.0;
   accuracy = 1.0;
   aggressiveness = 0.9;
   activateDist = 700.0;
   deactivateBuff = 100.0;
   targetFreq = 2.0;
   trackFreq = 1.0;
   fireFreq = 0.2;
   LOSFreq = 0.1;
   orderFreq = 3.0;    
};

MissionBriefInfo missionData
{
	title = 			    *IDSTR_CE4_TITLE;
    planet =                *IDSTR_PLANET_DESERT;           
	campaign = 			    *IDSTR_CE4_CAMPAIGN;		   
	dateOnMissionEnd =      *IDSTR_CE4_DATE; 			  
	shortDesc = 		    *IDSTR_CE4_SHORTBRIEF;	   
	longDescRichText = 	    *IDSTR_CE4_LONGBRIEF;		   
	media = 		 	    *IDSTR_CE4_MEDIA;
    nextMission =           *IDSTR_CE4_NEXTMISSION;
    successDescRichText =   *IDSTR_CE4_DEBRIEF_SUCC;
    failDescRichText =      *IDSTR_CE4_DEBRIEF_FAIL;
    location =              *IDSTR_CE4_LOCATION;
    soundvol =              "ce4.vol";
};

MissionBriefObjective missionObj1   
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_CE4_OBJ1_SHORT;
	longTxt		= *IDSTR_CE4_OBJ1_LONG;
	bmpname		= *IDSTR_CE4_OBJ1_BMPNAME;
};

function win()
{
    missionObj1.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(ce4);
    schedule("forceToDebrief();", 5.0);
}

//ACTORS
//===============
function onMissionStart()
{    
    cdAudioCycle(YouGot,Newtech, Gnash); //placeholder
}

function onSPClientInit()
{
    newFormation(genform, 0,0,0,
                          35,0,0,
                          -35,0,0,
                          70,0,0,
                          -70,0,0);
    $DB = false;
    initActors();
    desertSounds();
    schedule("ambientFX();",5.0);    
}

function ambientFX()
{
    ambDrop($playerNum.id);
    schedule("ambientFX();",randomInt(10,20));
}

function initActors()
{
    initPlayer();
    initHarabec();
    initRuins();      
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
    schedule("$nav001 = getObjectId(\"MissionGroup/navMarkers/nav001\");",1.0);
    schedule("setNavMarker($nav001, true, -1);",2.0);
    schedule("distCheck($playerNum.id, $nav001, 4000, boundaryWarn, 15, far);", 10.0);
    schedule("distCheck($playerNum.id, $nav001, 7000, boundaryFail, 20, far);", 15.0); 
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

function distCheck(%id1, %id2, %maxDist, %callBack, %time, %direction)
{
    %dist = getDistance(%id1, %id2);
    db(getObjectName(%id1) @ " is " @ %dist @ " from " @ getObjectName(%id2));
    // we're interested in how close id1 is to id2
    if(%direction == close)
    {
        if(%dist < %maxDist)
            schedule(%callBack @ "();",1.0);    
       
        else
            schedule("distCheck(" @ %id1 @ "," @ %id2 @ "," @ %maxDist @ "," @ %callBack @ "," @ %time @ "," @ %direction @ ");", %time);
    }
        
    // we're interested in how far id1 is from id2
    if(%direction == far)
    {
        if(%dist > %maxDist)
            schedule(%callBack @ "();",1.0);    
       
        else
            schedule("distCheck(" @ %id1 @ "," @ %id2 @ "," @ %maxDist @ "," @ %callBack @ "," @ %time @ "," @ %direction @ ");", %time);
    }
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

//HARABEC FUNCTIONS
function initHarabec()
{
    $harabec = getObjectId("MissionGroup/harabec/Harabec");
    $harabec.route = getObjectId("MissionGroup/harabec/route");
    order($harabec, Guard, $harabec.route);
    $harabec.attacked = false;
    $buddyGroup = getObjectId("MissionGroup/Harabec/buddyGroup");
    $buddyGroup.count = 0;
    order($harabec,Cloak,true);
    setHarabecSquad();
}

function winCheck()
{
    if(isSafe(*IDSTR_TEAM_YELLOW,$playerNum.id,600))
    {
        missionObj1.status = *IDSTR_OBJ_COMPLETED;
        schedule("forceToDebrief();",8.0);   
    }
    else
        schedule("winCheck();",5.0);    
}

function harabec::vehicle::onDestroyed(%this, %who)
{
    schedule("winCheck();",5.0);
}

function harabec::vehicle::onAttacked(%this,%who)
{
    if(!$harabec.attacked)
    {
        $harabec.attacked = true;
        order($harabec,cloak,false);
        order($harabec,Attack, %who);
    }
}

function setHarabecSquad()
{
    %curMem = 0;
    %n = 0;
    while(%curMem = getNextObject(getObjectId("PLAYERSQUAD"),%curMem))
        %n++;
     
    if(%n-1) // player brought some buddies
    {
        %x = getPosition($harabec,x);
        %y = getPosition($harabec,y);
       
        %curMem = getNextObject($buddyGroup, 0);
        
        // drop a buddy for every pilot in player's squad
        for(%i = 0; %i <%n;%i++)
        {      
            %x = %x + (%i * 40) + 80;
            %y = %y - (%i * 40) - 80;
            %z= getTerrainHeight(%x,%y) + 10;
            setPosition(%curMem,%x,%y,%z); 
            order(%curMem,Guard,$harabec);
            %curMem = getNextObject($buddyGroup, %curMem);
            $buddyGroup.count++;
        }    
    }    
}
   
//RUIN FUNCTIONS
function initRuins()
{
    $altar = getObjectId("MissionGroup/ruins/altar");
    $hall1 = getObjectId("MissionGroup/ruins/hall1");
    $hall2 = getObjectId("MissionGroup/ruins/hall2");
    
    $altar.count = 0;
    $hall1.count = 0;
    $hall2.count = 0;
}

function destroyGroup(%group)
{
    %curMem = 0;    
    while(%curMem = getNextObject(%group,%curMem))
        damageObject(%curMem,10000);   
}

function node::structure::onDestroyed(%this, %who)
{
    %group = getGroup(%this);
    %group.count++;
    if(%group.count == 4)
        destroyGroup(%group);
}

function base::structure::onDestroyed(%this,%who)
{
    %base = getNextObject(getGroup(%this),0);
    db(getObjectName(%base));
    
    %str = "damageArea(" @ %this @ ",0,0,0,80,2000);";
    destroyGroup(getGroup(%this));
    schedule(%str,1.5);      
}

function buddy::vehicle::onDestroyed(%this,%who)
{
    $buddyGroup.count--;    
}