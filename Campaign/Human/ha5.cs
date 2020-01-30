//Script for HA5_UnderTheGun 



//Briefing info
//--------------------------------------------------------------------

MissionBriefInfo A5_UnderTheGun
{
    title =                 *IDSTR_HA5_TITLE;
    planet =                *IDSTR_PLANET_MARS;           
    campaign =              *IDSTR_HA5_CAMPAIGN;   
    dateOnMissionEnd =      *IDSTR_HA5_DATE;  
    shortDesc =             *IDSTR_HA5_SHORTBRIEF;   
    longDescRichText =      *IDSTR_HA5_LONGBRIEF;   
    media =                 *IDSTR_HA5_MEDIA;
    nextMission =           *IDSTR_HA5_NEXTMISSION;
    successDescRichText =   *IDSTR_HA5_DEBRIEF_SUCC;
    failDescRichText =      *IDSTR_HA5_DEBRIEF_FAIL;
    location =              *IDSTR_HA5_LOCATION;
    successWavFile          ="HA5_debriefing.wav";
    soundvol = "ha5.vol";
};

// Primary and Secondary Mission Objectives
//-----------------------------------------------------------------------

MissionBriefObjective missionObjective1    //locate safe house
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_ACTIVE;
   shortTxt     = *IDSTR_HA5_OBJ1_SHORT;
   longTxt      = *IDSTR_HA5_OBJ1_LONG;
   bmpName      = *IDSTR_HA5_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2    // call in rescue convoy
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_ACTIVE;
   shortTxt     = *IDSTR_HA5_OBJ2_SHORT;
   longTxt      = *IDSTR_HA5_OBJ2_LONG;
   bmpName      = *IDSTR_HA5_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3    // evacuate fugitives
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_ACTIVE;
   shortTxt     = *IDSTR_HA5_OBJ3_SHORT;
   longTxt      = *IDSTR_HA5_OBJ3_LONG;
   bmpName      = *IDSTR_HA5_OBJ3_BMPNAME;
};

MissionBriefObjective missionObjective4     //return to tunnels
{                                           
   isPrimary    = true;
   status       = *IDSTR_OBJ_ACTIVE;
   shortTxt     = *IDSTR_HA5_OBJ4_SHORT;
   longTxt      = *IDSTR_HA5_OBJ4_LONG;
   bmpName      = *IDSTR_HA5_OBJ4_BMPNAME;
};

MissionBriefObjective missionObjective5     //lead convoy to nav Bravo
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_IGNORE;
   shortTxt     = *IDSTR_HA5_OBJ5_SHORT;
   longTxt      = *IDSTR_HA5_OBJ5_LONG;
   bmpName      = *IDSTR_HA5_OBJ5_BMPNAME;
};

//Custom Pilots

Pilot pilot1
{
   id = 25;
   
   skill = 0.6;
   accuracy = 0.6;
   aggressiveness = 0.6;
   activateDist = 1000.0;
   deactivateBuff = 200.0;
   targetFreq = 1.6;
   trackFreq = 0.6;
   fireFreq = 2.2;
   LOSFreq = 0.8;
   orderFreq = 6.0;
};

Pilot BadArtillery
{
   id = 26;
   
   skill = 0.6;
   accuracy = 0.0;
   aggressiveness = 0.6;
   activateDist = 1000.0;
   deactivateBuff = 200.0;
   targetFreq = 1.6;
   trackFreq = 0.6;
   fireFreq = 2.2;
   LOSFreq = 0.8;
   orderFreq = 6.0;
};

//Map config
$server::HudMapViewOffsetX =  2500;
$server::HudMapViewOffsetY =  500;

//-----------------------------------------------------------------------------
function onMissionStart()
{
    initVariables();
    initFormations();
    clearGeneralOrders();
    cdAudioCycle(Watching, Mechsoul, ss3);
    marsSounds();
}
function onSPClientInit()
{
    initActors();
}

//-----------------------------------------------------------------------------
function initFormations()
{
    newFormation( wedge,   0,   0,0,    // leader
                          -20,-20,0,    // 1st
                           20,-20,0,
                          -30,-30,0  );                           
                           
    newFormation( line,    0,   0,0,   //leader                           
                           0, -40,0,   //1st
                           0, -80,0  );
                           
    newFormation( wall,    0,   0,0,   //leader
                         -50,   0,0,   //2nd
                        -100,   0,0,   //3rd                   
                        -150,   0,0  );
                         
}

//------------------------------------------------------------------------------
function initVariables()
{
    $ambushBegun                         = false;
    $artilleryAttachedCam                = false;
    $rescueOK                            = false;
    $bound1                              = false;
    $EvacChannel                         = 50;
    $TacComChannel                       = 51;
    $evacAttacked                        = false;
    $cnlChannel                          = 52;
    $missionEnd                          = false;
    $line6                               = false;
    $dist2                               = false;
    $dist2a                              = false;
    $reachedWarning                      = false;
    $reachedFail                         = false;
    $blastDoors                          = false;
    $fire                                = true;
    $SafeHouse                           = "";
}

//------------------------------------------------------------------------------
function initActors()
{
    $SafeHouse = myGetObjectId("MissionGroup/Scenario/TharsisCity/Locations/Safe" @ randomInt(1,5));
    setStaticShapeLongName( $SafeHouse, "IDSTR_HA5_SCN2" );
    //say(0,1,"Debug:: SafeHouse=" @ $SafeHouse);
    
    addGeneralOrder(*IDSTR_ORDER_HA5_1, "order($EvacLeader, Guard, $playerId);");
        
    schedule("setNavMarker(myGetObjectId(\"MissionGroup/NAV/NavAlpha\"), True, -1);", 2);
    
//Imps
    order(myGetObjectId("MissionGroup/Imps/Imp1/Pala1"), MakeLeader, true);
    order(myGetObjectId("MissionGroup/Imps/Imp1"), Speed, High);
    order(myGetObjectId("MissionGroup/Imps/Imp1"), Formation, line);
    setHercOwner("MissionGroup/Imps/Imp1", "MissionGroup/RebelEvac");    

    order(myGetObjectId("MissionGroup/Imps/Imp2/Mino1"), MakeLeader, true);
    order(myGetObjectId("MissionGroup/Imps/Imp2"), Speed, High);
    order(myGetObjectId("MissionGroup/Imps/Imp2"), Formation, wedge);
    setHercOwner("MissionGroup/Imps/Imp2", "MissionGroup/RebelEvac");
        
    order(myGetObjectId("MissionGroup/Imps/Imp3/Talon1"), MakeLeader, true);
    order(myGetObjectId("MissionGroup/Imps/Imp3"), Speed, High);
    order(myGetObjectId("MissionGroup/Imps/Imp3"), Formation, line);
    
//Convoy
    $EvacLeader = myGetObjectId("MissionGroup/RebelEvac/Evac1");
    order($EvacLeader, MakeLeader, True);
    order(myGetObjectId("MissionGroup/RebelEvac"), Formation, line);
    schedule("order($EvacLeader, Guard, \"MissionGroup/ConWait1\");", 5);
    
//Squadmates
    setHercOwner("playerSquad/squadMate1", "MissionGroup/Artillery");    
    setHercOwner("playerSquad/squadMate2", "MissionGroup/Artillery");    
    setHercOwner("playerSquad/squadMate3", "MissionGroup/Artillery");    
    
    schedule("boundCheck();", 15);
}

function player::onAdd (%this)
{
    $playerNum = %this;
}

//get playerVehicleId (playerId) as global var from playerNum/initiate mission by scheduling objective and mission events
function vehicle::onAdd(%this)
{
    %num = playerManager::vehicleIdToPlayerNum(%this);
    if(%num == $playerNum) 
    {
        $playerId = playerManager::playerNumToVehicleId($playerNum);
    }
}

//--common functions------------------------------------------------------------

function vehicle::onDestroyed()
{
    //dont use fancy death messages
}

function boundCheck()
{
    if($missionEnd){return;}
    %dist = getDistance(getObjectId("MissionGroup/Center"), $playerId);
    if (%dist > 4000)
    {
        if(!($bound1))
        {
            $bound1 = true;
            schedule("$bound1 = false;",60);
            TacComSpeaks(IDSTR_GEN_TCM1, 1);
        }
    }
    if (%dist > 4500)
    {
        TacComSpeaks(IDSTR_GEN_TCM2, 2);
        forceToDebrief();
        $missionEnd = true;
    }
    schedule("boundCheck();", 5);
                                                                                            
    
    //extra mission driven checks
    %dist2 = getDistance(getObjectId("MissionGroup/NAV/NavAlpha"), $EvacLeader);
    //if (%dist2 < 200){ setNavMarker(myGetObjectId("MissionGroup/NAV/NavAlpha"), False); }  //Nav Marker Goes??
    
    if( (%dist2 < 1000) && (%dist2 != 0) && (!($dist2)) )
    {
        $dist2 = true;
         if(!($ambushBegun))
            convoyLeaderSpeaks(IDSTR_HA5_CNL1, 1);
    }
    if( (%dist2 < 600) && (%dist2 != 0) && (!($dist2a)) )
    {
        $dist2a = true;
         if(!($ambushBegun))
            convoyLeaderSpeaks(IDSTR_HA5_CNL2, 2);
    }
    
    %dist3 = getDistance(getObjectId("MissionGroup/NAV/NavBravo"), $playerId);
    if( (%dist3 < 100) && (%dist3 != 0) ) {atEnd();}
    
    if(!(isGroupDestroyed(getObjectId("MissionGroup/Imps/Imp1/Pala1"))))
    {
        %dist5 = getDistance(getObjectId("MissionGroup/Imps/Imp1/Pala"), $playerId);
        if( (%dist5 < 300) && (%dist5 != 0) && (!($blastDoors)) ){ $blastDoors = true; convoyLeaderSpeaks(IDSTR_HA5_CNL6, 6);}
    }
}

function myGetObjectId(%this)
{
    %id = getObjectId(%this);
    if (%id == "")
    {
        //say(0,1,"Problem with id for " @ %this);
    }
    else return %id;    
}
//-------actor functions---------------------------------------------------------
function Evac::vehicle::onDestroyed(%destroyed, %destroyer)
{
    if(isGroupDestroyed(myGetObjectId("MissionGroup/RebelEvac")))
    {
        setDominantCamera(%destroyed,%destroyer);
        clearGeneralOrders();
        if(missionObjective5.status == *IDSTR_OBJ_ACTIVE){ missionObjective5.status = *IDSTR_OBJ_FAILED; }
        else missionObjective4.status = *IDSTR_OBJ_FAILED;
        killChannel($EvacChannel);
        killChannel($cnlChannel);
        forceToDebrief();
    }
}

function Evac::vehicle::onNewLeader(%this)
{
    $EvacLeader = %this;
    order("MissionGroup/RebelEvac", formation, line);
     if($ambushBegun)
        {order($EvacLeader, Guard, myGetObjectId("MissionGroup/ConWait2"));}
}

function Evac::vehicle::onAttacked(%attacked,%attacker)
{
    if(getTeam(%attacker) == *IDSTR_TEAM_YELLOW){return;}
    if($evacAttacked){return;}
    $evacAttacked = true;
    schedule("evacSpeaks(IDSTR_HA5_TRK2, 2);", 6);
    order($EvacLeader, Guard, myGetObjectId("MissionGroup/ConWait2"));
}

function TacComSpeaks(%line, %wav)
{
    if($playerId.SoundsHeard.TacComChannel[%wav]){return;}
    $playerId.SoundsHeard.TacComChannel[%wav] = true;
    say(0,$TacComChannel, *(%line), "GEN_TCM0"@%wav@".wav");
}


//------mission functions--------------------------------------------------------
function Abandoned::structure::onScan(%scanned, %scanner, %string)
{
    if(%scanned == $SafeHouse)
    {
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        schedule("beginAmbush();",2);
    }
}

function Abandoned::structure::onDestroyed(%destroyed, %destroyer)
{
    if( %destroyed == $SafeHouse && (!($ambushBegun)) )
    {
        missionObjective1.status = *IDSTR_OBJ_FAILED;
        forceToDebrief();
    }
}

function beginAmbush()
{
    if ($ambushBegun) {return;}
    //say(0,1,"Debug: ambush begun.");
    $ambushBegun = true;
    say(0,1,"Artillery_warn.wav");
    schedule("damageObject($SafeHouse, 20000);",4);
    say(0,1,"SM_Riana_DC1.WAV");
    
    Artillery(1);
    schedule("Artillery(2);", randomFloat(0.2,1.2) );
    schedule("Artillery(3);", randomFloat(0.2,1.2) );
    schedule("Artillery(4);", randomFloat(0.2,1.2) );
    schedule("Artillery(5);", randomFloat(0.2,1.2) );
    schedule("Artillery(6);", randomFloat(0.2,1.2) );
    schedule("$fire = false;", 60);
    
    schedule("convoyLeaderSpeaks(IDSTR_HA5_CNL4, 4);", 4);
    schedule("say(0,1,\"Mission_obj_new.wav\");", 7);
  
    missionObjective2.status = *IDSTR_OBJ_IGNORE;
    missionObjective3.status = *IDSTR_OBJ_IGNORE;
    missionObjective4.status = *IDSTR_OBJ_IGNORE;
    missionObjective5.status = *IDSTR_OBJ_ACTIVE;
    schedule("setNavMarker(myGetObjectId(\"MissionGroup/NAV/NavBravo\"), True, -1);", 2);
    order($EvacLeader, Guard, myGetObjectId("MissionGroup/ConWait2"));
    order(myGetObjectId("MissionGroup/Imps/Imp3/Talon1"), Attack, pick(playerSquad));
    
    order(myGetObjectId("MissionGroup/Imps/Imp1/Pala1"), Attack, pick(playerSquad));
    addGeneralOrder(*IDSTR_ORDER_HA5_2,"order($EvacLeader, Guard, \"MissionGroup/ConWait2\");");
    
    surprise();
}

function Artillery(%who)
{
    if(isGroupDestroyed("MissionGroup/Artillery/Artillery"@%who)){return;}
    %num = randomInt(1,5); 
    if(%num == 1 ){%target = "MissionGroup/Imps";}
    else if(%num == 2 ) { %target = "MissionGroup/Scenario/TharsisCity/Locations/Safe" @ randomInt(1,5);}
    else if(%num == 3 ) { %target = "MissionGroup/Target/rock" @ randomInt(1,6);}
    else %target = $playerId;
    order("MissionGroup/Artillery/Artillery" @ %who , Attack, %target);
    if($fire){schedule("Artillery(" @ %who @ ");", 4);}
    else{
    order("MissionGroup/Artillery/Artillery" @ %who , HoldFire, true);}
}

function vehicle::onArrived(%who, %where)
{
    if( (%where == myGetObjectId("MissionGroup/end")) && (%who == $playerId) ) {atEnd();}
}

function atEnd()
{
    if(!($ambushBegun)){return;}
    else if(getDistance( myGetObjectId("MissionGroup/Convoy/ConvoyLeader"), $EvacLeader ) < 400 )
    {
        missionObjective5.status = *IDSTR_OBJ_COMPLETED;
        if( missionObjStatus() == ALL_COMPLETED )
        {
            updatePlanetInventory(ha5);
            forceToDebrief();
            $missionEnd = true;
            order(myGetObjectId("MissionGroup/Imps/Imp1"), HoldPosition, true);
            order(myGetObjectId("MissionGroup/Imps/Imp1"), Guard, "MissionGroup/NAV/NavAlpha");
            order(myGetObjectId("MissionGroup/Imps/Imp2"), HoldPosition, true);
            order(myGetObjectId("MissionGroup/Imps/Imp2"), Guard, "MissionGroup/NAV/NavAlpha");
            order(myGetObjectId("MissionGroup/Imps/Imp3"), HoldPosition, true);
            order(myGetObjectId("MissionGroup/Imps/Imp3"), Guard, "MissionGroup/NAV/NavAlpha");
            order(myGetObjectId("MissionGroup/Artillery"), Attack, "MissionGroup/Scenario/Misc/Plaque");
         }
    }
}

function surprise()
{
    order("MissionGroup/Imps/Imp2/Mino1", Cloak, true);
    order("MissionGroup/Imps/Imp2/Pala1", Cloak, true);
    
    setPosition("MissionGroup/Imps/Imp2/Mino1", 2560, 360, 151);
    setPosition("MissionGroup/Imps/Imp2/Pala1", 2560, 311, 151);

    setPosition("MissionGroup/Artillery/Artillery1", 713, 2852, 198);
    setPosition("MissionGroup/Artillery/Artillery2", 1058, 2939, 166);
    setPosition("MissionGroup/Artillery/Artillery3", 1280, 2979, 192);
    setPosition("MissionGroup/Artillery/Artillery4", 3766, -2318, 389);
    setPosition("MissionGroup/Artillery/Artillery5", 3456, -2385, 363);
    setPosition("MissionGroup/Artillery/Artillery6", 3005, -2429, 378);

    setPosition("MissionGroup/Imps/Imp1/Pala1", 3500, -2500,369);
    setPosition("MissionGroup/Imps/Imp1/Mino2", 3520, -2500,369);
    setPosition("MissionGroup/Imps/Imp1/Pala3", 3500, -2520,369);

    order(myGetObjectId("MissionGroup/Imps/Imp2/Mino1"), MakeLeader, true);
    order(myGetObjectId("MissionGroup/Imps/Imp2"), Speed, High);
    order(myGetObjectId("MissionGroup/Imps/Imp2"), Formation, wedge);
    order(myGetObjectId("MissionGroup/Imps/Imp2/Mino1"), Attack, pick(playerSquad));
}

//a quick bit about monuments
function key::structure::onDestroyed(%destroyed,%destroyer)
{
    setShapeVisibility(myGetObjectId("MissionGroup/Scenario/Misc/Plaque"), true);
}

function evacSpeaks(%line, %wav)
{
    if(isGroupDestroyed(myGetObjectId("MissionGroup/RebelEvac"))) {killChannel($EvacChannel); return;}
    say(0,$EvacChannel, *(%line), "HA5_TRK0"@%wav@".wav");
}

function convoyLeaderSpeaks(%line, %wav)
{
    if(isGroupDestroyed(myGetObjectId("MissionGroup/RebelEvac"))) {killChannel($cnlChannel); return;}
    say(0,$cnlChannel, *(%line), "HA5_CNL0"@%wav@".wav");
}

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    missionObjective4.status = *IDSTR_OBJ_COMPLETED;
    missionObjective5.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(ha5);
    forceToDebrief();
}
