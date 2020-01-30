// HB4
// MISSION INFO ------------------------------------------------------------------------

MissionBriefInfo missionBriefInfo
{
    campaign                = *IDSTR_HB4_CAMPAIGN;
    title                   = *IDSTR_HB4_TITLE;
    planet                  = *IDSTR_PLANET_MARS;
    location                = *IDSTR_HB4_LOCATION;
    dateOnMissionEnd        = *IDSTR_HB4_DATE;

    shortDesc               = *IDSTR_HB4_SHORTBRIEF;
    longDescRichText        = *IDSTR_HB4_LONGBRIEF;        

    soundVol                = "HB4.vol";
    media                   = *IDSTR_HB4_MEDIA;

    successDescRichText     = *IDSTR_HB4_DEBRIEF_SUCC;
    successWavFile          = "HB4_debriefing.wav";
    failDescRichText        = *IDSTR_HB4_DEBRIEF_FAIL;
    nextMission             = *IDSTR_HB4_NEXTMISSION;
    endCinematicRec         = "cinHC.rec";
    endCinematicSmk         = "cin_HC.smk";
};
 
 // OBJECTIVES --------------------------------------------------------------------

MissionBriefObjective missionObjective1
{
    isPrimary       = true;
    status          = *IDSTR_OBJ_ACTIVE;
    shortTxt        = *IDSTR_HB4_OBJ1_SHORT;
    longTxt         = *IDSTR_HB4_OBJ1_LONG;
    bmpName         = *IDSTR_HB4_OBJ1_BMPNAME;    
};

MissionBriefObjective missionObjective2
{
    isPrimary       = true;
    status          = *IDSTR_OBJ_ACTIVE;
    shortTxt        = *IDSTR_HB4_OBJ2_SHORT;
    longTxt         = *IDSTR_HB4_OBJ2_LONG;
    bmpName         = *IDSTR_HB4_OBJ2_BMPNAME;  
};

MissionBriefObjective missionObjective3
{
    isPrimary       = true;
    status          = *IDSTR_OBJ_IGNORE;
    shortTxt        = *IDSTR_HB4_OBJ3_SHORT;
    longTxt         = *IDSTR_HB4_OBJ3_LONG;
    bmpName         = *IDSTR_HB4_OBJ3_BMPNAME; 
};

MissionBriefObjective missionObjective4
{
    isPrimary       = false;
    status          = *IDSTR_OBJ_ACTIVE;
    shortTxt        = *IDSTR_HB4_OBJ4_SHORT;
    longTxt         = *IDSTR_HB4_OBJ4_LONG;
    bmpName         = *IDSTR_HB4_OBJ4_BMPNAME; 
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
   LOSFreq = 0.8;
   orderFreq = 1.0;
};

Pilot recluse
{
   id = 21;
   
   skill = 0.8;
   accuracy = 0.3;
   aggressiveness = 0.4;
   activateDist = 1.0;
   deactivateBuff = 100.0;
   targetFreq = 0.6;
   trackFreq = 0.6;
   fireFreq = 0.2;
   LOSFreq = 0.8;
   orderFreq = 1.0;
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
    $convoyChannel          = 20;
    $dropShipChannel        = 50;
    $caanonChannel          = 50;
    $TacComChannel          = 50;
    $thumperChannel         = 60;
    //flags
    $bound1                 = false;
    $bound2                 = false;
    $conArrived4            = false;
    $conArrived5            = false;
    $conArrived9            = false;
    $conArrived12           = false;
    $conArrived14           = false;
    $conArrived19           = false;
    $seekersAdded           = false;
    $thumpersMet            = false;
    $caanonLanded           = false;
    $dh1Attacked            = false;
    $thumperJustAttacked    = false;
    $playerId[Seeker1]      = false;  //yea, I shouldnt have to catch all these
    $playerId[Seeker2]      = false;     //but I really do
    $playerId[Goad2]        = false; 
    $playerId[Adjud1]       = false; 
    $playerId[Adjud2]       = false; 
    $playerId[Adjud3]       = false; 
    $playerId[Shep1]        = false; 
    $playerId[Shep2]        = false; 
    $playerId[Recluse1]     = false; 
    $playerId[Recluse2]     = false;
    $caanonBlather          = false;
    $caanonBlather2         = false;
    $thumpOn                = false;
    $showThump              = false;
    $convoyIsWaiting        = false;
    $thumpTime              = false;
    
    //variables
    $conLost                = 0;
    $PI                     = 3.14;  
    
    clearGeneralOrders();
    
    marsSounds();
    
    // Formations
    newFormation( Line,   0,0,0,   0,-40,0,  0,-80,0, 0,-120,0 );
    newFormation( Delta,  0,0,0, -20,-20,0, 20,-20,0, 0, -20,0 );
    cdAudioCycle(Purge, Mechsoul, Terror);
}
function onSPClientInit()
{
    initActors();
    TacComSpeaks(IDSTR_HB4_TCM1, HB4_TCM01);
}

function initActors()
{
    setNavMarker( myGetObjectId( "MissionGroup/NAV/NavAlpha" ), true, -1 ); 
    addGeneralOrder(*IDSTR_ORDER_HB4_1, "thumpOn();");
        
    $convoyLeader = "MissionGroup/Convoy/Convoy1";
    order($convoyLeader, MakeLeader, true );
    order("MissionGroup/Convoy", Shutdown, true );
    
    schedule( "convoySpeaks(IDSTR_HB4_CON3, 3);", 7);
    schedule("thumperSpeaks(IDSTR_HB4_THU1, 1);",15);
    schedule("if(!($thumpOn)){forceCommandPopup();}",19);
    schedule("missionObjective4.status = *IDSTR_OBJ_ACTIVE;",19);

    schedule("alarmSoundsOn(\"MissionGroup/rBase/meet\",\"Alarm2.wav\");",24);           //AlarmSound
    schedule( "conOrders();", randomInt(70,120) );    
    schedule( "cybridOrders();", 13);
    
    schedule("cinematicDrop();",3);
        
    schedule("boundCheck();",10);
    
    navTheConvoy();
}

// --------------------------------------------------------------------------------

function structure::onDestroyed(%destroyed, %destroyer)
{
    if(%destroyed == "MissionGroup/rBase/meet")
    {
        alarmSoundsOff("MissionGroup/rBase/meet");
    }
}

// CONVOY -------------------------------------------------------------------------
function conOrders()
{
    order( "MissionGroup/Convoy", Shutdown, false );
    order( "MissionGroup/Convoy", Formation, Line );
    order( $convoyLeader, Guard, "MissionGroup\\convoyPath" );
    convoySpeaks(IDSTR_HB4_CON2, 2);
    schedule( "setNavMarker( myGetObjectId( \"MissionGroup/NAV/NavBravo\" ), true, -1 );", 4 );
    //say(0,1,"Mission_obj_new.wav");
}

function con::vehicle::onDestroyed( %destroyed, %destroyer )
{
    $conLost = $conLost + 1;
    if( isGroupDestroyed( "MissionGroup/Convoy" ) || $conLost >= 2 )
    {
        killChannel($convoyChannel);
        missionObjective1.status = *IDSTR_OBJ_FAILED;
        forceToDebrief();        
        setDominantCamera(%destroyer, %destroyed);
    }
}

function con::vehicle::onArrived(%who, %where)
{
    if( (%where == myGetObjectId("MissionGroup/convoyPath/conWay4") )&&( !($conArrived4)) )
    {
        $conArrived4 = true;
        dropIn(2, E, Recluse2);
        schedule("dropIn(10, E, Recluse1);", 2);
        schedule("recluseActions();",10);
    }
    else if( %where == myGetObjectId("MissionGroup/convoyPath/conWay5") && !($conArrived5))
    {
        $conArrived5 = true;
        if($thumpOn)
        {
            order($convoyLeader, Guard, "MissionGroup/ConWait");
            $convoyIsWaiting = true;
        }
    }
    else if( %where == myGetObjectId("MissionGroup/convoyPath/conWay9") && !($conArrived9))
    {
        $conArrived8 = true;
        dropIn(4, F, Adjud3);
        schedule("dropIn(5, F, Adjud2);",1);
        schedule("adjudActions();",10);
    }
    else if (%where == myGetObjectId("MissionGroup/convoyPath/conWay12") && !($conArrived12) )
    {
        $conArrived12 = true;
        if ( isGroupDestroyed( myGetObjectId( "MissionGroup/dShip/dShip1" ) ) ) { return; }
        damageObject( "MissionGroup/dShip/dShip1", 20000 );
        order("MissionGroup/Convoy", HoldPosition, true);
        schedule("order(\"MissionGroup/Convoy\", HoldPosition, false );", 3);
    }
    else if (%where == myGetObjectId("MissionGroup/convoyPath/conWay14") && !($conArrived14) )
    {
        $conArrived14 = true;
        dropIn(6, G, Shep2 );
        schedule("dropIn(7, G, Shep1 );", 4);
        schedule("shepActions();",10);
    }
    else if ( %where == myGetObjectId("MissionGroup/convoyPath/conWay19") )
    {
        missionObjective3.status = *IDSTR_OBJ_COMPLETED;
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;    
        order($convoyLeader, Guard, "MissionGroup/convoyPath/conway19");
        checkVictory();
    }
}

function con::vehicle::onNewLeader( %newLeader )
{
    $convoyLeader = %newLeader;
    order("MissionGroup/Convoy", formation);
    order( $convoyLeader, Guard, "MissionGroup/convoyPath" );
}

function navTheConvoy()
{
    setPosition("MissionGroup/NAV/NavAlpha", getPosition($convoyLeader, x), getPosition($convoyLeader, y), getPosition($convoyLeader, z));
    schedule("navTheConvoy();", 0.1);
}


//----------------------------------------------------------------------------------
//DropShips=====================================================================

function DS1::vehicle::onAttacked(%attacked, %attacker)
{
    if( $dh1Attacked ) { return; }
    $dh1Attacked = true;
    dropshipSpeaks(IDSTR_HB4_DS11, 1);
    TacComSpeaks(IDSTR_HB4_TCM2, HB4_TCM02);
    order(%attacked, Guard, "MissionGroup/dShip/ds2land" );
}

function DS1::vehicle::onDestroyed(%destroyed, %destroyer)
{
    order( "MissionGroup/dShip/dShip2", Guard, "MissionGroup/dShip/ds2land");
    setNavMarker("MissionGroup/NAV/NavBravo", false );
    missionObjective2.status = *IDSTR_OBJ_IGNORE;
    missionObjective3.status = *IDSTR_OBJ_ACTIVE;
    schedule("setNavMarker(\"MissionGroup/NAV/NavCharlie\", true, -1 );",33);
    schedule("order( \"MissionGroup/CybridF/Adjud2\", Attack, \"MissionGroup/dShip/dShip2\" );", 60);
    TacComSpeaks(IDSTR_HB4_TCM3, HB4_TCM03);
    schedule("caanonSpeaks(IDSTR_HB4_CAA1, 1);", 13);
    schedule("TacComSpeaks(IDSTR_HB4_TCM4, HB4_TCM04);", 14);
    schedule("say(0,50,\"Mission_obj_new.wav\");" ,15);  //channel 50 used to aid banter
}

function DS2::vehicle::onArrived(%who, %where)
{
    if (getDistance( $convoyLeader,"MissionGroup/dShip/dShip2") < 800)
    {
        if($caanonBlather){return;}
        $caanonBlather = true;
        caanonSpeaks(IDSTR_HB4_CAA2, 2);
    }   
}
function DS2::vehicle::onDestroyed()
{
    missionObjective3.status = *IDSTR_OBJ_FAILED;  
    forceToDebrief();
}

//----------------------------------------------------------------------------------
//Thumpers=========================================================================
function ThumpOn()
{
    if(!($thumpOn))
    {
        addGeneralOrder(*IDSTR_ORDER_HB4_2, "thumpOff();");
        removeGeneralOrder(*IDSTR_ORDER_HB4_1);
        
        playAnimSequence( "MissionGroup/Thumper/Thump1", 2);
        //playAnimSequence( "MissionGroup/Thumper/ThumpEffect1", 0);
        
        playAnimSequence( "MissionGroup/Thumper/Thump2", 2);
        //playAnimSequence( "MissionGroup/Thumper/ThumpEffect2", 0);
        
        $thumpOn = true;
        schedule("playThumpSound();",0.7);
    }
    if (!($thumpTime))
        thumpTime();
}

function ThumpOff()
{
    if($thumpOn)
    {
        addGeneralOrder(*IDSTR_ORDER_HB4_1, "thumpOn();");
        removeGeneralOrder(*IDSTR_ORDER_HB4_2);
    
        stopAnimSequence( "MissionGroup/Thumper/Thump1", 2);
        //stopAnimSequence( "MissionGroup/Thumper/ThumpEffect1", 0);
        //setAnimSequence("MissionGroup/Thumper/ThumpEffect1",0,0); 
        
        stopAnimSequence( "MissionGroup/Thumper/Thump2", 2);
        //stopAnimSequence( "MissionGroup/Thumper/ThumpEffect2", 0);
        //setAnimSequence("MissionGroup/Thumper/ThumpEffect2",0,0); 
        
        missionObjective4.status = *IDSTR_OBJ_COMPLETED;
        
        $thumpOn = false;
        if($convoyIsWaiting){
            order($convoyLeader, Guard, "MissionGroup/convoyPath");
            $convoyIsWaiting = false;}
    }
}
function playThumpSound()
{
    if($thumpOn)
    {
        playSound(0, "sfx_prob.wav", IDPRF_2D);
        schedule("playThumpSound();", 3.3);
    }
}
function thumpTime()
{
    $thumpTime = true;
    order("MissionGroup/CybridI/IShep1", MakeLeader, true);    
    order("MissionGroup/CybridI", HoldPosition, true);    
    order("MissionGroup/CybridI", Formation, Line);
    order("MissionGroup/CybridI", Speed, High);
    order("MissionGroup/CybridI/IShep1", Guard, myGetObjectId("MissionGroup/ShepPath"));
    order("MissionGroup/CybridI", HoldFire, true);
    
    if(!($seekersAdded))
    {
        $seekersAdded = true;
        schedule("dropIn(1, A, Seeker2);",21);
        schedule("dropIn(9, A, Seeker1);",22);
        schedule("SeekerActions();",30);
    }    
}

function Thumper::vehicle::onDestroyed(%destroyed, %destroyer)
{
    $showThump = false;
    thumpOff();
    removeGeneralOrder( *IDSTR_ORDER_HB4_1 );
    removeGeneralOrder( *IDSTR_ORDER_HB4_2 );
    setAnimSequence("MissionGroup/Thumper/ThumpEffect1",0,0); 
    setAnimSequence("MissionGroup/Thumper/ThumpEffect2",0,0); 
    if( isGroupDestroyed( "MissionGroup/Thumper/Thump1" ) && 
        isGroupDestroyed( "MissionGroup/Thumper/Thump2" ) &&
        getTeam(%destroyer) != *IDSTR_TEAM_YELLOW )
    {
        thumperSpeaks(IDSTR_HB4_THU4, 4);        
    }
}

function ThumpTrigger::trigger::onEnter(%this, %who)
{
    if(!($showThump) && ($thumpOn))
    {
        if(isSafe(*IDSTR_TEAM_YELLOW, $playerId, 800)){%cameraIn = 0.5; %cameraOut = 6.5;}
        else{ %cameraIn = 2.5; %cameraOut = 4.5;}
        //say(0,1,"Debug::"@%cameraTime);
        $showThump = true;
        schedule("setDominantCamera("@%who@", myGetObjectId(\"MissionGroup/Thumper/Thump1\"), 0, -65, 22 );",%cameraIn);
        schedule("setPlayerCamera();", %cameraOut);
    }
}

function ThumpTrigger::trigger::onContact(%this, %who)
{
    if($thumpOn)
        damageObject( %who ,10000);
}


//Cybrids  ===========================================================================

function cybridOrders()
{
    %time = randomInt(80,130);
    schedule( "dropIn(3, D, Adjud1);", %time );
    schedule( "order( \"MissionGroup/CybridD/Adjud1\", Attack, \"MissionGroup/Convoy\");", %time );
    
    
    order( myGetObjectId("MissionGroup/CybridB/Goad1"), Attack, "MissionGroup/Convoy" );
    schedule("convoySpeaks(IDSTR_HB4_CON1, 1);", 13);
    
}

function DropIn(%spot, %group, %bird)
{
    if( $playerId[%bird] ){return;}
    $playerId[%bird] = true;
    dropPod(myGetObjectId( "MissionGroup/DropPodMarkers/Spot" @ %spot ), myGetObjectId( "MissionGroup/Cybrid"@ %group @"/"@  %bird) );
}

function Cybrid::vehicle::onDestroyed(%destroyed, %destroyer)
{
    if( %destroyed == myGetObjectId("MissionGroup/CybridB/Goad1") )
    {
        schedule("dropIn( 8, C, Goad2 );",25);
        schedule("order(\"MissionGroup/CybridC/Goad2\", Attack, " @ %destroyer @");",25);
    }
}

function seekerActions()
{
    order( "MissionGroup/CybridA/Seeker1", MakeLeader, true );
    order( "MissionGroup/CybridA", Speed, High );
    order( "MissionGroup/CybridA", Formation, Delta );
    order( "MissionGroup/CybridA/Seeker1", Attack, playerSquad );
}
function adjudActions()
{
    order( myGetObjectId("MissionGroup/CybridF/Adjud2"), MakeLeader, true );
    order( myGetObjectId("MissionGroup/CybridF"), Formation, Delta );
    order( myGetObjectId("MissionGroup/CybridF"), Speed, Low );
    order( myGetObjectId("MissionGroup/CybridF"), HoldPosition, true );
    order("MissionGroup/CybridF/Adjud2", Attack, myGetObjectId("MissionGroup/dShip/dShip1") );
}
function shepActions()
{
    order( myGetObjectId("MissionGroup/CybridG/Shep1"), MakeLeader, true );
    order( myGetObjectId("MissionGroup/CybridG"), Speed, High );
    order( myGetObjectId("MissionGroup/CybridG"), Formation, Line );
    order("MissionGroup/CybridG/Shep1", Attack, pick(playerSquad, "MissionGrop/Convoy") );
}
function recluseActions()
{
    order( myGetObjectId("MissionGroup/CybridE"), Speed, High );
    order( myGetObjectId("MissionGroup/CybridE"), Attack, "MissionGroup/Thumper");
}


//====================================================================================
//Boundry Checking
function boundCheck()
{
    %dist = getDistance(myGetObjectId("MissionGroup/Center"), $playerId);
    //say(0,1,"Debug: dist from center: "@ %dist);
    if (%dist > 4500)
    {
        TacComSpeaks(IDSTR_GEN_TCM2, GEN_TCM02);
        forceToDebrief();
    }
    if (%dist > 4000)
    {
        if(!($bound1))
        {
            $bound1 = true;
            schedule("$bound1 = false;",60);
            TacComSpeaks(IDSTR_GEN_TCM1, GEN_TCM01);
        }
    }
    schedule("boundCheck();", 15);
    
    //additional distance driven mission events
}

function checkVictory()
{
    if( isSafe( *IDSTR_TEAM_YELLOW, $convoyLeader, 600 ) &&
        isSafe( *IDSTR_TEAM_YELLOW, $playerId, 600 ) )
    {
        updatePlanetInventory(hb4);
        forceToDebrief();
    }
    else
    {
        schedule("checkVictory();",5);
        if(!($caanonBlather2))
        {
            $caanonBlather2 = true;
            caanonSpeaks(IDSTR_HB4_CAA3, 3);
        }
    }
}


//====================================================================================
//Misc

function vehicle::onDestroyed()
{
    //no fancy death messages
}

function myGetObjectId( %this )
{
    %id = getObjectId( %this );
    if ( %id == "" ){
        //say(0, 1 ,"Debug: Problem with Id for " @ %this );
        }
    else return %id;
}

//  Note: tacCom needs full wav others only need misson wav
function TacComSpeaks(%line, %wav)
{
    if($playerId.dialogHeard.TacComChannel[%wav]){return;}
    $playerId.dialogHeard.TacComChannel[%wav] = true;
    say(0,$TacComChannel, *(%line), %wav@".wav");
}
function convoySpeaks(%line, %wav)
{
    if($playerId.dialogHeard.convoyChannel[%wav]){return;}
    $playerId.dialogHeard.convoyChannel[%wav] = true;
    say(0,$convoyChannel, *(%line), "HB4_CON0"@%wav@".wav");
}
function dropShipSpeaks(%line, %wav)
{
    if($playerId.dialogHeard.dropShipChannel[%wav]){return;}
    $playerId.dialogHeard.dropShipChannel[%wav] = true;
    say(0,$dropShipChannel, *(%line), "HB4_1DS0"@%wav@".wav");
}
function caanonSpeaks(%line, %wav)
{
    if($playerId.dialogHeard.caanonChannel[%wav]){return;}
    $playerId.dialogHeard.caanonChannel[%wav] = true;
    say(0,$caanonChannel, *(%line), "HB4_CAA0"@%wav@".wav");
}
function thumperSpeaks(%line, %wav)
{
    if($playerId.dialogHeard.thumperChannel[%wav]){return;}
    $playerId.dialogHeard.thumperChannel[%wav] = true;
    say(0,$thumperChannel, *(%line), "HB4_THU0"@%wav@".wav");
}

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    missionObjective4.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(hb4);
    forceToDebrief();
}

//from Hughs Drop.cs for the dropPod random weirdness
function cinematicDrop()
{
   %next    = randomInt(2, 5);
   %num     = randomInt(1, 2);
   %delay   = 1;
   
   while ( %num > 0 )
   {
      schedule("calcAndDrop();", %delay);
      %num--;
      %delay++;
   }
   
   schedule("cinematicDrop();", %next);
}

function calcAndDrop()
{  
   
   %offset  = randomInt(-2500, 2500);  
   
   %x    = getPosition($playerId, x);
   %y    = getPosition($playerId, y);
   %z    = getPosition($playerId, z);
   %rot  = getPosition($playerId, r);
   
   if(     %rot >=   -($PI/4)    &&      %rot <    ($PI/4)   )
   {
       %y = %y + 7500;
       %x = %x + %offset;
   }
   else
   if(     %rot >=    ($PI/4)    &&      %rot <  3*($PI/4)   )
   {
       %x = %x - 7500;
       %y = %y + %offset;
   }
   else
   if(     %rot >=  3*($PI/4)    ||      %rot < -3*($PI/4)   )
   {
       %y = %y - 7500;
       %x = %x + %offset;
   }
   else
   if(     %rot >= -3*($PI/4)    &&      %rot <   -($PI/4)   )
   {
       %x = %x + 7500;
       %y = %y + %offset;
   }
   
   setDropPodParams(0, 0, -0.935, ( (getTerrainHeight(%x, %y) ) + 2000 )  );
   dropPod( %x, %y, getTerrainHeight(%x, %y) );
}

