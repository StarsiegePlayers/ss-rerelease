//---- CA3 script file
//===========================================


DropPoint dropPoint1
{
	name = "foo";
	desc = "foo";
};

$server::HudMapViewOffsetX = 2000;
$server::HudMapViewOffsetY = -4500; 

MissionBriefInfo missionData
{
	title = 			    *IDSTR_CA3_TITLE;
    planet =                *IDSTR_PLANET_MERCURY;           
	campaign = 			    *IDSTR_CA3_CAMPAIGN;		   
	dateOnMissionEnd =      *IDSTR_CA3_DATE; 			  
	shortDesc = 		    *IDSTR_CA3_SHORTBRIEF;	   
	longDescRichText = 	    *IDSTR_CA3_LONGBRIEF;		   
	media = 		 	    *IDSTR_CA3_MEDIA;
    nextMission =           "ca4";
    successDescRichText =   *IDSTR_CA3_DEBRIEF_SUCC;
    failDescRichText =      *IDSTR_CA3_DEBRIEF_FAIL;
    location =              *IDSTR_CA3_LOCATION;
    soundvol =              "ca3.vol";
    successWavFile =        "CA3_debriefing.wav";
};

MissionBriefObjective missionObj1   //proceed to Nav 001 and destroy the forward scouts
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_CA3_OBJ1_SHORT;
	longTxt		= *IDSTR_CA3_OBJ1_LONG;
	bmpname		= *IDSTR_CA3_OBJ1_BMPNAME;
};

MissionBriefObjective missionObj2   //proceed to Nav 002 and destroy com uplink
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_CA3_OBJ2_SHORT;
	longTxt		= *IDSTR_CA3_OBJ2_LONG;
	bmpname		= *IDSTR_CA3_OBJ2_BMPNAME;
}; 

MissionBriefObjective missionObj3   //return to nav003 for extraction
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_CA3_OBJ3_SHORT;
	longTxt		= *IDSTR_CA3_OBJ3_LONG;
	bmpname		= *IDSTR_CA3_OBJ3_BMPNAME;
}; 

MissionBriefObjective missionObj4   // defend nexus 
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_IGNORE;
	shortTxt	= *IDSTR_CA3_OBJ4_SHORT;
	longTxt		= *IDSTR_CA3_OBJ4_LONG;
	bmpname		= *IDSTR_CA3_OBJ4_BMPNAME;
}; 
  
function win()
{
    missionObj1.status = *IDSTR_OBJ_COMPLETED;
    missionObj2.status = *IDSTR_OBJ_COMPLETED;
    missionObj3.status = *IDSTR_OBJ_COMPLETED;
    missionObj4.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(ca3);
    schedule("forceToDebrief();", 5.0);
}


//ACTORS
//===============
//  1. CYBRID
//      a.cs1
//      b.nexus
//  2. IMPERIAL
//      a.hs1
//      b.hs2
//      c.hs3
//  3. PLAYER

function onMissionStart()
{
    cdAudioCycle(Newtech, ss4, Gnash); //placeholder    
}

function onSPClientInit()
{
    newFormation(genform, 0,0,0,
                          35,0,0,
                          -35,0,0);
    $DB = true;
    initActors();
    mercurySounds();
    earthquakeSounds();
}

function initActors()
{
    initPlayer();
    initCybrid();
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
    //$playerNum.misType = randomInt(1,3);
    $playerNum.misType = 2;
               
    $nav000 = getObjectId("MissionGroup/navMarkers/nav000");
    $nav001 = getObjectId("MissionGroup/navMarkers/nav001");
    $nav002 = getObjectId("MissionGroup/navMarkers/nav002");
    $nav003 = getObjectId("MissionGroup/navMarkers/nav003");
       
    //schedule("distCheck($playerNum.id,$nav001,2000,startMission,10,close);",10.0); 
    schedule("startMission();",randomInt(20,25));
    if($playerNum.misType != 1)
    {
        schedule("distCheck($playerNum.id, $nav000, 4000, boundaryWarn, 15, far);", 10.0);
        schedule("distCheck($playerNum.id, $nav000, 6000, boundaryFail, 20, far);", 15.0);
    }
    else
    {
        schedule("distCheck($playerNum.id, $nav001, 4000, boundaryWarn, 15, far);", 10.0);
        schedule("distCheck($playerNum.id, $nav001, 6000, boundaryFail, 20, far);", 15.0);    
    }
    
    setNavMarker($nav000,false,-1); 
    schedule("setNavMarker($nav001,true,-1);",1.0);
    setNavMarker($nav002,false,-1);
    setNavMarker($nav003,false,-1);
                
    schedule("distCheck($playerNum.id, $imperial.hs4.drop, 1200, startHS4,4,close);", 5.0);
    //playsound proceed to nav001
    playSound(0, "CYB_NEX08.wav", IDPRF_2D);
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
    
    if(%direction == close)
    {
        if(%dist < %maxDist)
            schedule(%callBack @ "();",1.0);    
       
        else
            schedule("distCheck(" @ %id1 @ "," @ %id2 @ "," @ %maxDist @ "," @ %callBack @ "," @ %time @ "," @ %direction @ ");", %time);
    }    
    if(%direction == far)
    {
        if(%dist > %maxDist)
            schedule(%callBack @ "();",1.0);    
       
        else
            schedule("distCheck(" @ %id1 @ "," @ %id2 @ "," @ %maxDist @ "," @ %callBack @ "," @ %time @ "," @ %direction @ ");", %time);
    }
}

//CYBRID FUNCTIONS
function initCybrid()
{
    db("initCybrid()");
    $cybrid = getObjectId("MissionGroup/cybrid");
    $cybrid.attacked = false;
    $cybrid.nexus = getObjectId("MissionGroup/cybrid/nexus");
    $cybrid.guard = getObjectId("MissionGroup/cybrid/guard");
    $cybrid.guard.route = getObjectId("MissionGroup/cybrid/guard/route");
}

function startMission()
{
    
    db("startMission()");
    if($playerNum.misType == 1) // attack com base
    {
        distCheck($playerNum.id, $nav001, 100, setNav001, 4, close);
        distCheck($playerNum.id, $nav002, 900,startHS1AndHS3,5,close);
        return;
    }
          
    //distCheck($playerNum.id, $nav000, 500, startHS1AndHS3, 4, close);
    
    //order hs2 squad to guard nexus
    schedule("order($imperial.hs2.leader, guard, $cybrid.nexus);",1.0);
    distCheck($imperial.hs2.leader, $cybrid.nexus,450,startHS2,4,close);
    
    playSound(0, "CYB_NEX07.wav", IDPRF_2D);
    schedule("actorTalks($cybrid.nexus, IDSTR_CA3_NEX01, \"CA3_NEX01.wav\");",4.0);
    missionObj1.status = *IDSTR_OBJ_IGNORE;
    missionObj2.status = *IDSTR_OBJ_IGNORE;
    missionObj3.status = *IDSTR_OBJ_IGNORE;
    missionObj4.status = *IDSTR_OBJ_ACTIVE; 
                           
    setNavMarker($nav000, true, -1);
    setNavMarker($nav001, false);
    setNavMarker($nav002, false);
    setNavMarker($nav003, false);
    
    distCheck($playerNum.id, $nav000, 200, setNav000,5, close);
}

function setNav000()
{
    setNavMarker($nav000,false);
}
function setNav001()
{
    setNavMarker($nav001,false);
    setNavMarker($nav002,true,-1);
}
function setNav002()
{
    setNavMarker($nav002,false);
    setNavMarker($nav003,true,-1);
}
function setNav003()
{
    setNavMarker($nav003,false);
}

function cybrid::vehicle::onAttacked(%this, %who)
{
    if(!$cybrid.attacked && getTeam(%this) != getTeam(%who))
    {
        $cybrid.attacked = true;
        schedule("actorTalks($cybrid,IDSTR_CA3_NEX02, \"CA3_NEX02.wav\");", 5.0);
        schedule("order($cybrid.guard, Attack, %who);",3.0);
    }
}

function cybrid::vehicle::onDestroyed(%this, %who)
{
    if(%this == $cybrid.nexus)
    {
        missionObj4.status = *IDSTR_OBJ_FAILED;
        schedule("forceToDebrief();", 5.0);
    }
}

//IMPERIAL FUNCTIONS

function initImperial()
{
    db("initImperial()");
    $imperial = getObjectId("MissionGroup/imperial");
    $imperial.hs1 = getObjectId("MissionGroup/imperial/hs1");
    $imperial.hs2 = getObjectId("MissionGroup/imperial/hs2");
    $imperial.hs3 = getObjectId("MissionGroup/imperial/hs3");
    
    $imperial.hs4 = getObjectId("MissionGroup/imperial/hs4");
    $imperial.hs4.id1 = getObjectId("MissionGroup/imperial/hs4/1hs4");
    %n = 1;
    if($playerNum.misType != 1) %n = 2;
    $imperial.hs4.drop = getObjectId("MissionGroup/imperial/hs4/drop"@%n);
    
    $imperial.hs1.leader = getObjectId("MissionGroup/imperial/hs1/1hs1");
    order($imperial.hs1.leader, MakeLeader,true);
    order($imperial.hs1,Formation, genform);
    order($imperial.hs1.leader, Speed, high);
    
    $imperial.hs2.leader = getObjectId("MissionGroup/imperial/hs2/1hs2");
    order($imperial.hs2.leader, MakeLeader,true);
    order($imperial.hs2,Formation, genform);
    order($imperial.hs2.leader, Speed, high);

    $imperial.hs3.leader = getObjectId("MissionGroup/imperial/hs3/1hs3");
    order($imperial.hs3.leader, MakeLeader,true);
    order($imperial.hs3,Formation, genform);
    order($imperial.hs3.leader, Speed, high);

    $imperial.hs1.attacked = false;
    $imperial.hs2.attacked = false;
    $imperial.hs3.attacked = false;
    $imperial.hs4.attacked = false;
    
    $imperial.count = 8; 
    $imperial.structs = 5;
    $imperial.index = 0;
    
    $imperial.chatter[0] = "cyb_gn21.wav";
    $imperial.chatter[1] = "cyb_gn01.wav";
    $imperial.chatter[2] = "cyb_gn37.wav";
    $imperial.chatter[3] = "cyb_gn36.wav";   
}

function startHS2()
{
    if($playerNum.misType == 1)
    {
        dropGroup($imperial.hs2);
        order($imperial.hs2,Attack, $playerNum.id);
        return;
    }
    
    order($imperial.hs2, Attack, $cybrid.nexus);
}

function dropGroup(%groupId)
{
    %curMem = 0;
    while(%curMem = getNextObject(%groupId,%curMem))
        dropNearPlayer(%curMem,false,800);  
}

function dropNearPlayer(%id,%vis,%dist)
{
    %pi = 3.1415926;
    %x = getPosition($playerNum.id,x);
    %y = getPosition($playerNum.id,y);
    %rot  = getPosition(%id,rot); 
    %inc = %dist + randomInt(-200,200); 
    %juke = randomInt(-1000,1000);   
    
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
    db(%x @ ", " @ %y @ ", " @ %z);
    setPosition(%id,%x,%y,%z);               
}

function startHS4()
{
    db("startHS4()");
    %x = getPosition($imperial.hs4.drop,x);
    %y = getPosition($imperial.hs4.drop,y);
    %z = getPosition($imperial.hs4.drop,z);
    
    setPosition($imperial.hs4.id1,%x,%y,%z);
    order($imperial.hs4.id1, Attack, $playerNum.id);    
}

function startHS1()
{
    db("startHS1");
    
    if($playerNum.misType == 1)
    {
        dropGroup($imperial.hs1);
        order($imperial.hs1,Attack,$playerNum.id);        
        return;
    }
    
    order($imperial.hs1.leader,Speed,High);
    order($imperial.hs1.leader, Attack,  $cybrid.nexus);   
}

function imperial::vehicle::onDestroyed(%this, %who)
{
    if($playerNum.misType == 1) 
    {
        if(%this == $imperial.hs4.id1)
        {
            missionObj1.status = *IDSTR_OBJ_COMPLETED;
            //playSound proceed to nav002
            playSound(0, "CYB_NEX17.wav", IDPRF_2D);
            schedule("playSound(0, \"CYB_NEX09.wav\", IDPRF_2D);",3.0);
            setNavMarker($nav002,true,-1);
            distCheck($playerNum.id, $nav002, 200, setNav001,5,close);
        }
        return;
    }
    $imperial.count--;
    
    if($imperial.count == 0)
    {
        schedule("actorTalks($cybrid.nexus, IDSTR_CA2_NEX03, \"CA2_NEX03.wav\");",1.0);
        schedule("forceToDebrief();", 5.0);
        missionObj4.status = *IDSTR_OBJ_COMPLETED;
        updatePlanetInventory(ca3);
    } 
    if($imperial.count == 6)
    {
        schedule("dropGroup($imperial.hs3);",%n = randomInt(10,15));
        schedule("order($imperial.hs3,Attack, $cybrid.nexus);",%n+1);
        playSound(0,"cyb_gn62.wav",IDPRF_2D);
    } 
    if($imperial.count == 4)
        startHS1();    
}

function imperial::vehicle::onAttacked(%this, %who)
{
    if(!getGroup(%this).attacked)
    {
        getGroup(%this).attacked = true;
        playSound(0,$imperial.chatter[$imperial.index++],IDPRF_2D);
    }
}

function imperial::structure::onDestroyed(%this, %who)
{
    if($playerNum.misType != 1) return;
    $imperial.structs--;
    if($imperial.structs == 0)
    {
        missionObj2.status = *IDSTR_OBJ_COMPLETED;
        setNavMarker($nav003,true,-1);
        distCheck($playerNum.id, $nav003, 200, setNav003,  5, close);
        distCheck($playerNum.id, $nav003, 200, extraction, 6, close);
        //play sound of nexus goto nav003 for extraction  
        playSound(0, "CYB_NEX17.wav", IDPRF_2D);
        schedule("playSound(0, \"CYB_NEX10.wav\", IDPRF_2D);",4.0); 
    } 
}


function extraction()
{
    missionObj3.status = *IDSTR_OBJ_COMPLETED;
    schedule("forceToDebrief();",5.0);
    //playSound mission complete
    playSound(0, "CYB_NEX04.wav", IDPRF_2D);   
}
