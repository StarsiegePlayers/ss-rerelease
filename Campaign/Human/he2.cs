//HE2==========================================================


MissionBriefInfo missionBriefInfo
{
    campaign                = *IDSTR_HE2_CAMPAIGN;
    title                   = *IDSTR_HE2_TITLE;
    planet                  = *IDSTR_PLANET_PLUTO;
    location                = *IDSTR_HE2_LOCATION;
    dateOnMissionEnd        = *IDSTR_HE2_DATE;

    shortDesc               = *IDSTR_HE2_SHORTBRIEF;
    longDescRichText        = *IDSTR_HE2_LONGBRIEF;        

    media                   = *IDSTR_HE2_MEDIA;
    soundVol                = "HE2.vol";

    successDescRichText     = *IDSTR_HE2_DEBRIEF_SUCC;
    failDescRichText        = *IDSTR_HE2_DEBRIEF_FAIL;
    successWavFile          = "HE2_debriefing.wav";
    
    nextMission             = *IDSTR_HE2_NEXTMISSION;
    endCinematicRec         = "cinHE2.rec";
    endCinematicSmk         = "cin_HE2.smk";

    scrambleNextMission     = true; 
};
 
 // OBJECTIVES --------------------------------------------------------------------

MissionBriefObjective missionObjective1   //goto nav omega 
{
    isPrimary       = true;
    status          = *IDSTR_OBJ_ACTIVE;
    shortTxt        = *IDSTR_HE2_OBJ1_SHORT;
    longTxt         = *IDSTR_HE2_OBJ1_LONG;
    bmpName         = *IDSTR_HE2_OBJ1_BMPNAME;    
};

MissionBriefObjective missionObjective2   //destroy cybrids
{
    isPrimary       = true;
    status          = *IDSTR_OBJ_ACTIVE;
    shortTxt        = *IDSTR_HE2_OBJ2_SHORT;
    longTxt         = *IDSTR_HE2_OBJ2_LONG;
    bmpName         = *IDSTR_HE2_OBJ2_BMPNAME;  
};

MissionBriefObjective missionObjective3  // caanon must survive
{
    isPrimary       = true;
    status          = *IDSTR_OBJ_ACTIVE;
    shortTxt        = *IDSTR_HE2_OBJ3_SHORT;
    longTxt         = *IDSTR_HE2_OBJ3_LONG;
    bmpName         = *IDSTR_HE2_OBJ3_BMPNAME; 
};

 
//==========================================================================
//
// SCRIPT ------------------------------------------------------------------

function player::onAdd( %this )
{
    $playerNum = %this;
}

function vehicle::onAdd( %this )
{
    if( playerManager::vehicleIdToPlayerNum( %this ) == $playerNum )
    {
        $playerId = %this;
    }
}

function onMissionStart()
{
    // channels

    //flags
    $missionEnd         = false;
    $getAwkward         = false;
    $banter1            = false;
    $banter2            = false;
    $drop2              = false;
    
    //variables
    $Caanon = "";
    
    // Formations
    newFormation( Nexus,   0,0,0,   -60,60,0,  60,60,0, 0,-80,0 );  //its kind of a Y shape
    cdAudioCycle(Cloudburst, Watching, Terror);
    plutoSounds();
}
function onSPClientInit()
{
    initActors();
}

//Custom Pilots

Pilot drawkward
{
   id = 26;
   
   name = "DrAwkward";
   skill = 1.0;
   accuracy = 1.0;
   aggressiveness = 0.7;
   activateDist = 1000.0;
   deactivateBuff = 200.0;
   targetFreq = 0.6;
   trackFreq = 0.6;
   fireFreq = 0.2;
   LOSFreq = 0.4;
   orderFreq = 0.4;
};

Pilot caanon
{
   id = 27;
   
   name = "Caanon Weathers";
   skill = 1.0;
   accuracy = 1.0;
   aggressiveness = 0.7;
   activateDist = 1000.0;
   deactivateBuff = 200.0;
   targetFreq = 0.6;
   trackFreq = 0.6;
   fireFreq = 0.2;
   LOSFreq = 0.1;
   orderFreq = 1.0;
};

//Map config
$server::HudMapViewOffsetX =  1000;
$server::HudMapViewOffsetY = -4000;

function initActors()
{
    initCaanon();
    initPlayer();
    initCybridC();
    boundryCheck();
}

function initCybridC()
{
    order("MissionGroup/CybridC", Guard, "MissionGroup/Nexus");
}

function initCaanon()
{
    $Caanon = myGetObjectId("MissionGroup/Caanon");
    order($Caanon, Speed, Low);
    schedule("order($Caanon, Guard, \"MissionGroup/NexusPoint\");",5);
}

function Caanon::vehicle::onDestroyed(%destroyed, %destroyer)
{
    killChannel($Caanon);
    setDominantCamera(%destroyed, %destroyer);
    $missionEnd = true;
    missionObjective3.status = *IDSTR_OBJ_FAILED;
    forceToDebrief();
}

function Caanon::vehicle::onAttacked(%this, %who)
{
    if(%who == $playerId)
    {
        $Caanon.hitByPlayer++;
        
        if($Caanon.hitByPlayer == 3)
            caanonSpeaks(IDSTR_GEN_CAA04, GEN_CAA04);
        
        if($Caanon.hitByPlayer == 6)
        {
            caanonSpeaks(IDSTR_GEN_CAA05, GEN_CAA05);
            order($Caanon, Attack, $playerId);
            missionObjective3.status = *IDSTR_OBJ_FAILED;
            schedule("forceToDebrief();", 20.0);
        }        
    }
}

function vehicle::onDestroyed()
{

}

function Cybrid::vehicle::onDestroyed()
{
    if( isGroupDestroyed("MissionGroup/CybridA") && (!($banter1)))
    {
        $banter1 = true;
        schedule("caanonSpeaks(IDSTR_HE2_CAA4, HE2_CAA04);",4);
        schedule("banter1();",14);
    }
    if( isGroupDestroyed("MissionGroup/CybridB") && (!($banter2)))
    {
        $banter2 = true;
        schedule("caanonSpeaks(IDSTR_HE2_CAA6, HE2_CAA06);",2);
        schedule("harabecSpeaks(IDSTR_HE2_HAR3, HE2_HAR03);",10);
        schedule("banter2();",17);
    }
    if( isGroupDestroyed("MissionGroup/CybridA") &&
        isGroupDestroyed("MissionGroup/CybridB") &&
        isGroupDestroyed("MissionGroup/CybridC") &&
        isGroupDestroyed("MissionGroup/Nexus") &&
        (!( isGroupDestroyed($Caanon) )) )
        {
            schedule("atEnd();",10);
        }
}

function DrAwkward::vehicle::onDestroyed(%destroyed, %destroyer)
{
    if(%destroyer == $playerId)
        win();
}

function initPlayer()
{
    setNavMarker("MissionGroup/NavOmega", true, -1);
    caanonSpeaks(IDSTR_HE2_CAA1, HE2_CAA01);
    schedule("dropCybridA();",20);
}

function banter1()
{
    harabecSpeaks(IDSTR_HE2_HAR1, HE2_HAR01);
    caanonSpeaks(IDSTR_HE2_CAA2, HE2_CAA02);
    harabecSpeaks(IDSTR_HE2_HAR2, HE2_HAR02);
    caanonSpeaks(IDSTR_HE2_CAA3, HE2_CAA03);
    
    schedule("dropCybridB();", 40);
}

function banter2()
{
    caanonSpeaks(IDSTR_HE2_CAA9, HE2_CAA09);
    harabecSpeaks(IDSTR_HE2_HAR4, HE2_HAR04);
    caanonSpeaks(IDSTR_HE2_CAA10, HE2_CAA10);
    harabecSpeaks(IDSTR_HE2_HAR5, HE2_HAR05);
}

function atEnd()
{
    $missionEnd = true;
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    order($Caanon, Guard, "MissionGroup/NavOmega");
    schedule("forceToDebrief();",2.2);
    updatePlanetInventory(he2);
}

function dropCybridA()
{
    %delay = randomInt(1,3);
    %point1 = randomInt(1,4);
    %point2 = randomInt(1,4);
    if(%point1 == %point2){ %delay = %delay + randomInt(3,5); }
    dropPod("MissionGroup/DropIn/Spot" @ %point1, "MissionGroup/CybridA/Adj1");
    schedule("dropPod(\"MissionGroup/DropIn/Spot" @ %point2 @"\",\"MissionGroup/CybridA/Adj2\");", %delay);
    schedule("order(\"MissionGroup/CybridA\",Attack, $Caanon);",8);
}

function dropCybridB()
{
    if(!($drop2))
    {
        $drop2 = true;
        
        %delay = randomInt(1,3);
        %point1 = randomInt(5,8);
        %point2 = randomInt(5,8);
        if(%point1 == %point2){%delay = %delay + randomInt(3,5);}
        dropPod("MissionGroup/DropIn/Spot" @ %point1, "MissionGroup/CybridB/Ex1");
        schedule("dropPod(\"MissionGroup/DropIn/Spot" @ %point2 @"\",\"MissionGroup/CybridB/Ex2\");", %delay);
        schedule("order(\"MissionGroup/CybridB\",Attack, pick(playerSquad) );",8);
    }
}       

//========================================================================
//BoundryCheck
function boundryCheck()
{
    //say(0,1,"debug: bound check called");
    if($missionEnd){ return; }
    if($getAwkward){ return; }
    
    %dist =  getDistance( $playerId, $Caanon );
    //if( %dist > 1500 ) { forceToCaanonDeath(); }   //turn on for severy boundry check
    //say(0,1,"Debug::distance from player to Caanon="@%dist);
    if(%dist < 200)
    {order($Caanon, Speed, Medium);}
    else if( getDistance($Caanon, myGetObjectId( "MissionGroup/NavOmega" )) > getDistance($playerId, "MissionGroup/NavOmega") )
    {order($Caanon, Speed, High);}
    else
    {order($Caanon, Speed, Low);}
        
    schedule("boundryCheck();", 5);
    
    //Extra mission driven checks
    if( getDistance($Caanon, "MissionGroup/Misc/2ndAttack") < 300 )
    {
        schedule("dropCybridB();", 8);
    }
}

function forceToCaanonDeath()
{
    CaanonSpeaks(IDSTR_HE2_CAA8, HE2_CAA08);
    $getAwkward = true;
    setPosition( $Caanon, -3220, 680,300);
    schedule("setDominantCamera($Caanon, \"MissionGroup/Misc/Shep\");",2);
    order("MissionGroup/Misc/Shep", Attack, $Caanon);
    schedule("damageObject($Caanon,20000);",8);
}

//====================================================================================
//Misc

function myGetObjectId( %this )
{
    %id = getObjectId( %this );
    if ( %id == "" ){
        //say(0, 1 ,"Debug: Problem with Id for " @ %this );
        }
    else return %id;
}

function getAwkward()
{
    fadeEvent(0, out, 2, 250,250,250 );
    schedule("fadeEvent(0, in, , 0,0,0 );",4);
    schedule("setPosition( $playerId, -3220, 680,300);",2);
    order("MissionGroup/Misc/Shep", Attack, $playerId);
    $getAwkward = true;
}

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    forceToDebrief();
}
function caanonSpeaks(%line, %wav)
{
    say(0, $Caanon, *(%line), %wav @".wav");
}
function harabecSpeaks(%line, %wav)
{
    say(0, $Caanon, *(%line), %wav @".wav");

}
