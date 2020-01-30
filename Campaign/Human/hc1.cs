//---- HC1 script file
//===========================================


DropPoint dropPoint1
{
	name = "foo";
	desc = "foo";
};

$server::HudMapViewOffsetX = 3500;
$server::HudMapViewOffsetY = 1500; 


MissionBriefInfo missionData
{
	title = 			    *IDSTR_HC1_TITLE;
    planet =                *IDSTR_PLANET_VENUS;           
	campaign = 			    *IDSTR_HC1_CAMPAIGN;		   
	dateOnMissionEnd =      *IDSTR_HC1_DATE; 			  
	shortDesc = 		    *IDSTR_HC1_SHORTBRIEF;	   
	longDescRichText = 	    *IDSTR_HC1_LONGBRIEF;		   
	media = 		 	    *IDSTR_HC1_MEDIA;
    nextMission =           *IDSTR_HC1_NEXTMISSION;
    successDescRichText =   *IDSTR_HC1_DEBRIEF_SUCC;
    failDescRichText =      *IDSTR_HC1_DEBRIEF_FAIL;
    location =              *IDSTR_HC1_LOCATION;
    successWavFile =        "HC1_Debriefing.wav";
    soundvol =              "hc1.vol";
};

MissionBriefObjective missionObj1   //protect transports from Cybrid attack
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HC1_OBJ1_SHORT;
	longTxt		= *IDSTR_HC1_OBJ1_LONG;
	bmpname		= *IDSTR_HC1_OBJ1_BMPNAME;
}; 

MissionBriefObjective missionObj2   //protect station
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HC1_OBJ2_SHORT;
	longTxt		= *IDSTR_HC1_OBJ2_LONG;
	bmpname		= *IDSTR_HC1_OBJ2_BMPNAME;
}; 

MissionBriefObjective missionObj3   //destroy cybrid artillery
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_IGNORE;
	shortTxt	= *IDSTR_HC1_OBJ3_SHORT;
	longTxt		= *IDSTR_HC1_OBJ3_LONG;
	bmpname		= *IDSTR_HC1_OBJ3_BMPNAME;
};

MissionBriefObjective missionObj4   //destroy all Cybrids encountered
{
	isPrimary 	= false;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HC1_OBJ4_SHORT;
	longTxt		= *IDSTR_HC1_OBJ4_LONG;
	bmpname		= *IDSTR_HC1_OBJ4_BMPNAME;
}; 
 
function win()
{
    missionObj1.status = *IDSTR_OBJ_COMPLETED;
    missionObj2.status = *IDSTR_OBJ_COMPLETED;
    missionObj3.status = *IDSTR_OBJ_COMPLETED;
    missionObj4.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(hc1);
    schedule("forceToDebrief();", 5.0);
}

//==ACTORS=====================================================================

// 1. player
// 2. Cybrid A ( 2 Artillery) 
// 3. Cybrid B
// 4. Cybrid C
// 5. Cybrid D
// 6. Cybrid E
// 7. Cybrid F
// 8. Dead Transport
// 9. Transport A
// 10.Transport B
// 11.Transport C
// 12.Transport D
 
//-----------------------------------------------------------------------------
function onMissionStart()
{
    cdAudioCycle(ss4,Gnash,NewTech); 
}

function onSPClientInit()
{
    initFormations();
    initActors();
    venusSounds();   
}

//-----------------------------------------------------------------------------
function initActors()
{
    initPlayer();
    initBase();
    initCybrid();
    initTransport();
}

//-----------------------------------------------------------------------------
function initFormations()
{
    $DB = true;
    newFormation(gen_form, 0,0,0,
                           -20,0,0,
                            20,0,0);
}

function db(%msg)
{
    if($DB)
        echo(%msg);
}

//--PLAYER FUNCTIONS
function initPlayer()
{
    db("initPlayer()");
    $playerNum.missionFailed = false;
    schedule("$navAlpha = getObjectId(\"MissionGroup/navMarkers/navAlpha\");",2.0);
    actorTalks(0, IDSTR_HC1_WU01, "HC1_WU01.wav");
    
    schedule("boundaryCheck(warn);", 10.0);
    schedule("boundaryCheck(fail);", 15.0);
    schedule("distCheck(artilleryWarn);",5.0);
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
        $playerNum.missionFailed = true;    
}

function actorTalks(%id, %txt, %snd)
{
    if(!$playerNum.missionFailed)
        say(0,%id, *(%txt), %snd);
}

function artilleryDetected()
{    
    missionObj3.status = *IDSTR_OBJ_ACTIVE;
    actorTalks($playerNum.id,IDSTR_HC1_WU02,"HC1_WU02.wav");      
}

function distCheck(%arg)
{
    //%name = getObjectName(%arg);
    //db("distCheck() = " @ %arg);
          
    if(%arg == $transport.dead)
    {
        if(getDistance($transport.dead.entry, $transport.dead) < 1000)
        {
            //destroy dead transport
            actorTalks($transport.dead, IDSTR_HC1_DTP01, "HC1_DTP01.wav");
				// knock off a couple feet and turn yellow
            schedule("damageArea($transport.dead,  50,50,0, 60, 10000);", 1.0);
            schedule("order($transport.dead, Shutdown, true);", 1.5);
            
            //start transport A
            schedule("order($transport.a, Guard, $transport.a.entry);", 8.0);
            schedule("actorTalks($transport.a, IDSTR_HC1_1DS01, \"HC1_1DS01.wav\");", 15.0);
            distCheck($transport.a);    
        }
        
        else
            schedule("distCheck($transport.dead);", 5.0);
    }
    
    else 
    {
        %dist = getDistance(%arg.entry, %arg);
        //db(getObjectName(%arg), %dist);
        
        if(%dist < 900)
        {
            %n = randomInt(1,3);
            %txt = %snd = "";
        
            if(%n == 1)
            {
                %txt = IDSTR_GEN_DS24;
                %snd = "GEN_2DSA04.wav";    
            }
            if(%n == 2)
            {
                %txt = IDSTR_GEN_DS25;
                %snd = "GEN_2DSA05.wav";    
            }   
            if(%n == 3)
            {
                %txt = IDSTR_GEN_DS26;
                %snd = "GEN_2DSA06.wav";    
            }
            
            actorTalks(%arg, %txt, %snd);
        }
        else
            schedule("distCheck(" @ %arg @ ");", 5.0);           
    }
    
} 

function checkWin()
{
    if(!$playerNum.missionFailed)
    {
        if(missionObj2.status != *IDSTR_OBJ_FAILED)
        {
            missionObj2.status = *IDSTR_OBJ_COMPLETED;
            playSound(0, "GEN_OC01.wav", IDPRF_2D);
        }
        updatePlanetInventory(hc1);
        schedule("forceToDebrief();",14.0);
   }
}

function boundaryCheck(%arg)
{
    if(%arg == warn)
    {
        if(getDistance($playerNum.id, $navAlpha) > 2000)
            actorTalks(0,IDSTR_GEN_TCM1, "GEN_TCMA01.wav");
        else
            schedule("boundaryCheck(warn);", 10.0);    
    }
    
    if(%arg == fail)
    {
        if(getDistance($playerNum.id, $navAlpha) > 3500)
            actorTalks(0, IDSTR_GEN_TCM2, "GEN_TCMA02.wav");
        else
            schedule("boundaryCheck(fail);", 15.0);
    }
}

//TRANSPORT FUNCTIONS
function initTransport()
{
    db("initTransport()");
    
    $transport = getObjectId("MissionGroup/transport");
    $transport.dead = getObjectId("MissionGroup/transport/dead");
    $transport.a = getObjectId("MissionGroup/transport/transporta");
    $transport.b = getObjectId("MissionGroup/transport/transportb");
    $transport.c = getObjectId("MissionGroup/transport/transportc");
    $transport.d = getObjectId("MissionGroup/transport/transportd");
    
    $transport.dead.entry = getObjectId("MissionGroup/transport/route/lz1");
    $transport.dead.next = $transport.a;
    
    $transport.a.entry = getObjectId("MissionGroup/transport/route/lz1");
    $transport.a.exit  = getObjectId("MissionGroup/transport/route/escapeA");
    $transport.a.attacked = false;
    $transport.a.next = $transport.b;
    
    $transport.b.entry = getObjectId("MissionGroup/transport/route/lz2");
    $transport.b.exit  = getObjectId("MissionGroup/transport/route/escapeB");
    $transport.b.attacked = false;
    $transport.b.next = $transport.c;
    
    $transport.c.entry = getObjectId("MissionGroup/transport/route/lz1");
    $transport.c.exit  = getObjectId("MissionGroup/transport/route/escapeC");
    $transport.c.attacked = false;
    $transport.c.next = $transport.d;
    
    $transport.d.entry = getObjectId("MissionGroup/transport/route/lz2");
    $transport.d.exit  = getObjectId("MissionGroup/transport/route/escapeD");
    $transport.d.attacked = false;
    $transport.d.next = false;
        
    $transport.escaped = 0;
    $transport.count = 5;
    $transport.curTransport = 0;
     
    order($transport.dead, Speed, high);
    order($transport.a, Speed, high);
    order($transport.b, Speed, high);
    order($transport.c, Speed, high);
    order($transport.d, Speed, high);
    
    //order($transport.dead, Guard, $transport.dead.entry);
    schedule("order($transport.dead, ShutDown, true);",12.0);
    schedule("distCheck($transport.dead);", 15.0);
}

function transportEscapes(%id)
{
    db("transportEscapes() = " @ $transport.escaped);
    if(getDistance(%id, $navAlpha) > 1200)
    {
        $transport.escaped++;   
        if($transport.escaped >= 4)
        {
            missionObj1.status = *IDSTR_OBJ_COMPLETED;
        }
    }
    else
        schedule("transportEscapes(" @ %id @ ");",8.0);
}

function transport::vehicle::onAttacked(%this, %who)
{
    if(getTeam(%this) != getTeam(%who) && !%this.attacked)
    {
        %this.attacked = true;
        %i = randomInt(1,3);
        %snd = "";
        %txt = "";
        
        if(%i == 1)
        {
            %snd = "GEN_1DSA01.wav";
            %txt = IDSTR_GEN_DS11;
        }
        
        if(%i == 2)
        {
            %snd = "GEN_2DSA01.wav";
            %txt = IDSTR_GEN_DS21;
        }

        if(%i == 3)
        {
            %snd = "GEN_2DSA03.wav";
            %txt = IDSTR_GEN_DS23;
        }
        
        actorTalks(%this, %txt, %snd);        
    }
}

function transport::vehicle::onDestroyed(%this, %who)
{
    killChannel(%this);
    $transport.count--;
    
    if($transport.count < 4)
    {
        missionObj1.status = *IDSTR_OBJ_FAILED;
        schedule("forceToDebrief();", 5.0);
        setHudTimer(-1, 0, *IDSTR_TIMER_HC1_1, 1);
    }    
}

function transport::vehicle::onArrived(%this, %where)
{
    db("onArrived()");
    if(%where == %this.entry)
    {        
        $transport.curTransport = %this;
        
        //schedule current dropship to leave
        schedule("order(" @ %this @ ",Guard," @ %this.exit @ ");", 30.0);
        schedule("actorTalks(" @ %this @ ", IDSTR_GEN_DS27, \"GEN_2DSA07.wav\");", 30.0);
        setHudTimer(30, -1,*IDSTR_TIMER_HC1_1, 1);
        schedule("transportEscapes(" @ %this @ ");", 5.0);
        
        schedule("$transport.curTransport = 0;", 30.0);
        
        // start next dropship descent
        db(%this.next);
        if(%this.next)
        {
            schedule("order(" @ %this.next @ ",Guard," @ %this.next.entry @ ");", 21.0);
            distCheck(%this.next);            
        }                         
    }
}


//CYBRID FUNCTIONS
function initCybrid()
{
    db("initCybrid()");
    $cybrid = getObjectId("MissionGroup/cybrid");
    $cybrid.count = 11;
    $cybrid.curCybrid = 0;
    
    $cybrid.a = getObjectId("MissionGroup/cybrid/A");
    $cybrid.b = getObjectId("MissionGroup/cybrid/B");
    $cybrid.c = getObjectId("MissionGroup/cybrid/C");
    $cybrid.d = getObjectId("MissionGroup/cybrid/D");
    $cybrid.e = getObjectId("MissionGroup/cybrid/E");
    $cybrid.f = getObjectId("MissionGroup/cybrid/F");
    
    $cybrid.a.id1  = getObjectId("MissionGroup/cybrid/A/a1");
    $cybrid.a.id2  = getObjectId("MissionGroup/cybrid/A/a2");
    $cybrid.a.attacked = false;
    $cybrid.a.next = $cybrid.b;
    $cybrid.a.count = 2;
    $cybrid.a.timeBuffer = 30;
    
    $cybrid.b.attacked = false;
    $cybrid.b.next = $cybrid.c;
    $cybrid.b.timeBuffer = 35;
    
    $cybrid.c.attacked = false;
    $cybrid.c.next = $cybrid.d;
    $cybrid.c.timeBuffer = 35;
    
    $cybrid.d.attacked = false;
    $cybrid.d.next = $cybrid.e;
    $cybrid.d.timeBuffer = 35;
    
    $cybrid.e.attacked = false;
    $cybrid.e.next = $cybrid.f;
    $cybrid.e.timeBuffer = 35;
    
    $cybrid.f.attacked = false;
    $cybrid.f.next = false; 
    
    //start Artillery (cybridA) 
    schedule("startCybrid($cybrid.a);", 40.0);
}

function startCybrid(%id)
{
    cybridDist(%id);
       
    if(%id == $cybrid.b || %id == $cybrid.c || %id == $cybrid.f || 
       %id == $cybrid.d || %id == $cybrid.e )
    {
        %count = %cm = 0;
        %drop = "MissionGroup/dropsites/drop";
        
        while(%cm = getNextObject(%id,%cm))
        {
             %hash[%count] = "";
             %count++;
        }
                               
        %i = 0;
        while(%i<%count)
        { 
            %tmp = getObjectId(%drop @ randomInt(1,15));
            if(%hash[%tmp % %count] == "")
            {
                %hash[%tmp % %count] = %tmp;
                %i++;
            }
            db("hash["@ %tmp % %count @ "] contains dropsite id = " @ %tmp);
        }
          
        %i = 0;
        %cm = 0;
        while(%cm = getNextObject(%id,%cm))
        {
            droppod(%hash[%i],%cm);             
            %i++;
        }
    }
     
    if(%id == $cybrid.a)
    {
        %drop1 = getObjectId("MissionGroup/dropsites/dropa1");
        %drop2 = getObjectId("MissionGroup/dropsites/dropa2");
        
        %n = randomInt(1,2);
        if(%n == 1)
        {
            %tmp = %drop1;
            %drop1 = %drop2;
            %drop2 = %tmp;    
        }
        
        droppod(%drop1,%id.id1);
        %str = "droppod(" @ %drop2 @ "," @ %id.id2 @ ");";
        schedule(%str,randomFloat(0.5,2.5));
        schedule("artilleryDetected();",9.5);
    }             
}

function cybridDist(%id)
{
    %dist1 = getDistance(%id.id1, $navAlpha);
    
    if(%dist1 < 530  && !%id.attacked)
        cybridAttack(%id);
    
    else
        schedule("cybridDist(" @ %id @ ");", 5.0);
   
}

function cybridAttack(%id)
{
    if(%id.next)
        schedule("startCybrid(" @ %id.next @ ");", %id.timeBuffer);
    
    %id.attacked = true;
    if(%id == $cybrid.a)
    {
        order($cybrid.a.id1, Attack, pick($base.turret));
        order($cybrid.a.id2, Attack, pick($base.turret));
    }
    
    else
    {
        %killer = pick(%id);
        order(%id, Attack, pick("playersquad"));
        if($transport.curTransport)
        {
            order(%killer,Attack,$transport.curTransport);
            order(%killer, Holdposition,true);
        }
        else
            order(%killer,Attack,pick($base));
    }
    
}                     
    
function cybrid::vehicle::onAttacked(%this, %who)
{
    if(!getGroup(%this).attacked && getTeam(%this) != getTeam(%who) )
    {
        cybridAttack(getGroup(%this));    
    }
}

function cybrid::vehicle::onDestroyed(%this, %who)
{
    $cybrid.count--;
    getGroup(%this).count--;
    if($cybrid.count == 0)
    {
        missionObj4.status = *IDSTR_OBJ_COMPLETED;
        missionObj1.status = *IDSTR_OBJ_COMPLETED;
        InventoryWeaponAdjust(-1,118,2); #EMC 
        playSound(0, "GEN_OC01.wav", IDPRF_2D);
        checkWin();
    }
}

function artillery::vehicle::onDestroyed(%this, %who)
{
    $cybrid.a.count--;
       
    if($cybrid.a.count == 0)
    {
        missionObj3.status = *IDSTR_OBJ_COMPLETED;
        playSound(0, "GEN_OC01.wav", IDPRF_2D);
    }
}

function artillery::vehicle::onAttacked(%this, %who)
{
    db(getGroup(%this));
    db("artillery onAttacked() = " @ getGroup(%this).attacked);
    if(!getGroup(%this).attacked && getTeam(%this) != getTeam(%who))
    {
        cybridAttack(getGroup(%this));    
    }
}

// BASE FUNCTIONS
function initBase()
{
    $base = getObjectId("MissionGroup/base");
    $base.turret = getObjectId("MissionGroup/base/turret");
    $base.station = getObjectId("MissionGroup/base/station");
    $base.attacked = false;
    $base.destroyed = 0;
    
    $base.turret.count = 2;
}

function station::structure::onAttacked(%this, %who)
{
    if(!$base.attacked && getTeam(%this) != getTeam(%who))
    {
        $base.attacked = true;
        schedule("$base.attacked = false;", 10.0);
        
        %chance = randomInt(1,10);
        
        if(%chance > 5)
            actorTalks($base, IDSTR_HC1_WU03, "HC1_WU03.wav");
    }
}

function station::structure::onDestroyed(%this, %who)
{
    $base.destroyed++;
    if($base.destroyed == 3)
    {
        missionObj2.status = *IDSTR_OBJ_FAILED;
        schedule("forceToDebrief();", 5.0);
    }
}

function base::turret::onDestroyed(%this, %who)
{
    $base.turret.count--;
    
    if($base.turret.count == 0 && $cybrid.a.count > 0)
        order($cybrid.a, Attack, $transport.curTransport);
}