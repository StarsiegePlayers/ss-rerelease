//Mission Script for CE2: Heresy//Corruption


//Briefing info
//--------------------------------------------------------------------

MissionBriefInfo CE2
{
    title =                 *IDSTR_CE2_TITLE;
    planet =                *IDSTR_PLANET_DESERT;           
    campaign =              *IDSTR_CE2_CAMPAIGN;   
    dateOnMissionEnd =      *IDSTR_CE2_DATE;  
    shortDesc =             *IDSTR_CE2_SHORTBRIEF;   
    longDescRichText =      *IDSTR_CE2_LONGBRIEF;   
    media =                 *IDSTR_CE2_MEDIA;
    nextMission =           *IDSTR_CE2_NEXTMISSION;
    successDescRichText =   *IDSTR_CE2_DEBRIEF_SUCC;
    failDescRichText =      *IDSTR_CE2_DEBRIEF_FAIL;
    location =              *IDSTR_CE2_LOCATION;
    
    soundvol =              "CE2.vol";
    successWavFile =        "CE2_debriefing.wav";
};

// Primary and Secondary Mission Objectives
//-----------------------------------------------------------------------

MissionBriefObjective missionObjective1  //Destroy Metagens       
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_ACTIVE;
   shortTxt     = *IDSTR_CE2_OBJ1_SHORT;
   longTxt      = *IDSTR_CE2_OBJ1_LONG;
   bmpName      = *IDSTR_CE2_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2 //Prevent Metagen Escape      
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_IGNORE;
   shortTxt     = *IDSTR_CE2_OBJ2_SHORT;
   longTxt      = *IDSTR_CE2_OBJ2_LONG;
   bmpName      = *IDSTR_CE2_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3  //Destroy Convoy     
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_ACTIVE;
   shortTxt     = *IDSTR_CE2_OBJ3_SHORT;
   longTxt      = *IDSTR_CE2_OBJ3_LONG;
   bmpName      = *IDSTR_CE2_OBJ3_BMPNAME;
};

MissionBriefObjective missionObjective4  //Destroy Plantary Defences.      
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_IGNORE;
   shortTxt     = *IDSTR_CE2_OBJ4_SHORT;
   longTxt      = *IDSTR_CE2_OBJ4_LONG;
   bmpName      = *IDSTR_CE2_OBJ4_BMPNAME;
};

MissionBriefObjective missionObjective5       
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_IGNORE;
   shortTxt     = *IDSTR_CE2_OBJ5_SHORT;
   longTxt      = *IDSTR_CE2_OBJ5_LONG;
   bmpName      = *IDSTR_CE2_OBJ5_BMPNAME;
};

MissionBriefObjective missionObjective6       
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_IGNORE;
   shortTxt     = *IDSTR_CE2_OBJ6_SHORT;
   longTxt      = *IDSTR_CE2_OBJ6_LONG;
   bmpName      = *IDSTR_CE2_OBJ6_BMPNAME;
};

MissionBriefObjective missionObjective7  //rendezvous with da dropship       
{
   isPrimary    = true;
   status       = *IDSTR_OBJ_IGNORE;
   shortTxt     = *IDSTR_CE2_OBJ7_SHORT;
   longTxt      = *IDSTR_CE2_OBJ7_LONG;
   bmpName      = *IDSTR_CE2_OBJ7_BMPNAME;
};


//Custom Pilots

Pilot pilotAveCaut1000
{
   id = 25;
   
   skill = 0.4;
   accuracy = 0.4;
   aggressiveness = 0.2;
   activateDist = 1000.0;
   deactivateBuff = 600.0;
   targetFreq = 5.4;
   trackFreq = 1.0;
   fireFreq = 2.0;
   LOSFreq = 0.8;
   orderFreq = 3.5;
};

Pilot pilotAveCaut600
{
   id = 26;
   
   skill = 0.4;
   accuracy = 0.4;
   aggressiveness = 0.2;
   activateDist = 600.0;
   deactivateBuff = 400.0;
   targetFreq = 5.4;
   trackFreq = 1.0;
   fireFreq = 2.0;
   LOSFreq = 0.8;
   orderFreq = 3.5;
};

Pilot pilotAveAgres600
{
   id = 26;
   
   skill = 0.4;
   accuracy = 0.4;
   aggressiveness = 0.7;
   activateDist = 600.0;
   deactivateBuff = 400.0;
   targetFreq = 5.4;
   trackFreq = 1.0;
   fireFreq = 2.0;
   LOSFreq = 0.8;
   orderFreq = 3.5;
};

Pilot artillery
{
   id = 27;
   
   skill = 0.1;
   accuracy = 0.0;
   aggressiveness = 0.2;
   activateDist = 10000.0;
   deactivateBuff = 400.0;
   targetFreq = 5.4;
   trackFreq = 1.0;
   fireFreq = 2.0;
   LOSFreq = 0.8;
   orderFreq = 3.5;
};

//Map config
$server::HudMapViewOffsetX =  -2000;
$server::HudMapViewOffsetY =  0;

//-----------------------------------------------------------------------------
function onMissionStart()
{
    initVariables();
    initFormations();
    clearGeneralOrders();
    cdAudioCycle(Newtech, Mechsoul, Purge);
}
function onSPClientInit()
{
    initActors();
}
//----------------------------------------------------------------------------
function initVariables()
{
    $convoyFirstAttack          = true;
    $conArrived                 = false;
    $navMetaOn                  = false;
    $navConvoyOn                = false;
    $metaAttacked               = false;
    $flyerAttacked              = false;
    $dshipChanged               = false;
    $traitorLine                = false;
    
    $traitor[1]                 = 0;
    $traitor[2]                 = 0;
    $traitor[3]                 = 0;
    $PI                         = 3.14;
}

//-----------------------------------------------------------------------------
function initFormations()
{
    newFormation( wedge,   0,   0,0,    // leader
                          -20,-20,0,    // 1st
                           20,-20,0,
                          -30,-30,0  );                           
                           
    newFormation( line,    0,   0, 0,   //leader                           
                           0, -30, 0,   //1st
                           0, -90, 0,
                           0, -150,0  );
                           
}

function player::onAdd(%this)
{
    $playerNum = %this;
}

//get playerVehicleId (playerId) as global var from playerNum
function vehicle::onAdd(%this)
{
    if(playerManager::vehicleIdToPlayerNum(%this) == $playerNum) 
    {
        $playerId = playerManager::playerNumToVehicleId($playerNum);
    }
}

//
//===Distance Checks==================================================================
function boundCheck()
{
    if($missionEnd){ return; }
    %dist = getDistance(myGetObjectId("MissionGroup/Center"), $playerId);
    if (%dist > 4000)
    {
        say(0,1, *IDSTR_CYB_NEX02, "CYB_NEX02.wav");
        forceToDebrief();
        $missionEnd = true;
    }
    else if (%dist > 3500)
    {
        if(!($bound1))
        {
            $bound1 = true;
            schedule("$bound1 = false;",60);
            say(0,1, *IDSTR_CYB_NEX01, "CYB_NEX01.wav");
        }
    }
    
    //make sure the metagen stay within a reasonable distance
    if(getDistance( myGetObjectId("MissionGroup/Metagens/Alpha/Meta1"), myGetObjectId("MissionGroup/Center")) > 5000 )
    {
        order("MissionGroup/Metagens/Alpha", guard, playerSquad);
    }
    schedule("boundCheck();", 15);
    
    
    //----------------------------------extra mission driven checks

    %dist3 = getDistance($playerId, myGetObjectId("MissionGroup/Nav/NavHeretic"));
    if( %dist3 < 400 && %dist3 != 0)
    { setNavMarker(myGetObjectId("MissionGroup/NAV/NavHeretic"), false); }
    
    %dist4 = getDistance($playerId, myGetObjectId("MissionGroup/NAV/Nav001"));
    if( %dist4 < 400 && %dist4 != 0)
    { setNavMarker(myGetObjectId("MissionGroup/NAV/Nav001"), false); }
      
    //if the metagens are far enough from the player and close enough to the dropship
    %dist5 = getDistance(getLeader("MissionGroup/Metagens/Alpha"), "MissionGroup/Imps/Flyers/KnightsDropShip" );
    if(  %dist5 < 300 && %dist5 != 0 )
    {
        metagensAreEscaping();
    }
    
    //Put the "Cyb_me19.wav==in the perimetere here"    
}

//Init Actors + Client   ============================================================
function initActors()
{
    initConvoy();
    
    order("MissionGroup/Imps/Flyers/TerranBanshee", height,500,700);
    order("MissionGroup/Imps/Flyers/TerranBanshee", flyThrough, true);
    flyerOrders();
    
    //==============Imps
    // +Outpost
    order("MissionGroup/Scenario/Outpost/Turrets", ShutDown, true);
    // +Guards
    reloadObject(myGetObjectId("MissionGroup/Imps/Guards"), -100);
    // +Depot
    order(myGetObjectId("MissionGroup/Imps/Depot/Myr1"), MakeLeader, true );
    order(myGetObjectId("MissionGroup/Imps/Depot"), Speed, High);
    order(myGetObjectId("MissionGroup/Imps/Depot"), Formation, line);
    order(myGetObjectId("MissionGroup/Imps/Depot/Myr1"), Guard, myGetObjectId("MissionGroup/Scenario/RefuelDepot/Silo") );
    // +patrol1
    order(myGetObjectId("MissionGroup/Imps/Patrol1/Disruptor"), Speed, Medium);
    order(myGetObjectId("MissionGroup/Imps/Patrol1/Disruptor"), Guard, myGetObjectId("MissionGroup/Paths/Road") );
    // +patrol2
    initKnightPatrol2();
    
    // +patrol3 (Brutes) 
    order(myGetObjectId("MissionGroup/Imps/Patrol3/Gorg1"), MakeLeader, true );
    if(randomInt(1,2) == 1)
    {order("MissionGroup/Imps/Patrol3", Speed, high);}
    else
    {order(myGetObjectId("MissionGroup/Imps/Patrol3"), Speed, meduim);}
    order(myGetObjectId("MissionGroup/Imps/Patrol3"), Formation, wedge);
    order(myGetObjectId("MissionGroup/Imps/Patrol3"), Guard, myGetObjectId( pick("playerSquad","MissionGroup/Cybrids") ) );
    
    //=============Metagens
    setHercOwner("MissionGroup/Metagens/Alpha", "MissionGroup/Convoy");      //keep the metas from killing the convoy   
    setHercOwner("MissionGroup/Metagens/Alpha", "MissionGroup/Imps/Flyers");  //keep the metas from killing the escape
    // +Alpha
    order(myGetObjectId("MissionGroup/Metagens/Alpha/Meta1"), MakeLeader, true );
    order(myGetObjectId("MissionGroup/Metagens/Alpha"), Speed, Medium);
    order(myGetObjectId("MissionGroup/Metagens/Alpha"), Formation, wedge);
    order(myGetObjectId("MissionGroup/Metagens/Alpha/Meta1"), Guard, myGetObjectId("MissionGroup/Imps/Flyers/KnightsDropShip") );
    // +patrol1
    initMetaPatrol1();
    // +patrol2 
    order(myGetObjectId("MissionGroup/Metagens/Patrol2/Shep"), MakeLeader, true );
    if(randomInt(1,2) == 1){order("MissionGroup/Metagens/Patrol2", Speed, low);}
    else order(myGetObjectId("MissionGroup/Metagens/Patrol2"), Speed, meduim);
    order(myGetObjectId("MissionGroup/Metagens/Patrol2/Shep"), Guard, myGetObjectId( pick("playerSquad") ));

    //=============Cybrids
    // +patrol1
    order(myGetObjectId("MissionGroup/Cybrids/Patrol1/Goad1"), MakeLeader, true );
    order(myGetObjectId("MissionGroup/Cybrids/Patrol1"), Speed, Medium);
    order(myGetObjectId("MissionGroup/Cybrids/Patrol1"), Formation, line);
    order(myGetObjectId("MissionGroup/Cybrids/Patrol1/Goad1"), Attack, myGetObjectId( pick("MissionGroup/Scenario/RefuelDepot","MissionGroup/Scenario/Outpost") ) );
    schedule("dropPod(myGetObjectId(\"MissionGroup/Cybrids/Patrol1/Goad1\"),randomInt(1,3));", randomInt(1,30));
    // +patrol2 
    order(myGetObjectId("MissionGroup/Cybrids/Patrol2/Bolo1"), MakeLeader, true );
    order(myGetObjectId("MissionGroup/Cybrids/Patrol2"), Speed, low);
    order(myGetObjectId("MissionGroup/Cybrids/Patrol2"), Formation, line);
    order(myGetObjectId("MissionGroup/Cybrids/Patrol2/Bolo1"), Guard, myGetObjectId("MissionGroup/Imps/Flyers/KnightsDropShip") );
    schedule("dropPod(myGetObjectId(\"MissionGroup/Cybrids/Patrol2/Bolo1\"),randomInt(1,3));", randomInt(1,30));
    

    //===============Begin Stuff
    
    setNavMarker("MissionGroup/NAV/NavHeretic", true, -1);
    $navMetaOn = true;
    navTheMetagen();
    $navConvoyOn = true;
    navTheConvoy();
    
    desertSounds();
    
    loadLines();
    
    schedule("boundCheck();",40);
    
    cinematicDrop();
}
function initConvoy()              //==============convoy
{
    order("MissionGroup/Convoy/Leader", MakeLeader, true );
    order("MissionGroup/Convoy", Speed, Medium);
    order("MissionGroup/Convoy", Formation, Line);
    order("MissionGroup/Convoy/Leader", Guard, "MissionGroup/Paths/Road/ConvoyEnd");
}
function initKnightPatrol2()      //==============Knight patrol2
{
    order(myGetObjectId("MissionGroup/Imps/Patrol2/Paladin1"), MakeLeader, true );
    if(randomInt(1,2) == 1){order(myGetObjectId("MissionGroup/Imps/Patrol2"), Speed, low);}
    else order(myGetObjectId("MissionGroup/Imps/Patrol2"), Speed, meduim);
    order(myGetObjectId("MissionGroup/Imps/Patrol2"), Formation, wedge);
    order(myGetObjectId("MissionGroup/Imps/Patrol2/Paladin1"), Guard, myGetObjectId("MissionGroup/Paths/FlyerBasePatrol") );
}
function initMetaPatrol1()         //==============Metapatrol1
{
    order(myGetObjectId("MissionGroup/Metagens/Patrol1/Seek1"), MakeLeader, true );
    order(myGetObjectId("MissionGroup/Metagens/Patrol1"), Speed, Medium);
    order(myGetObjectId("MissionGroup/Metagens/Patrol1"), Formation, line);
    order(myGetObjectId("MissionGroup/Metagens/Patrol1/Seek1"), Guard, myGetObjectId("MissionGroup/Paths/WideCityPatrol") );
}


//Mission functions===================================================================
//-------------Talking------------------------------

function vehicle::onAttacked(%attacked, %attacker)
{
    %random = randomInt(1,100);
    if(randomInt(1,100) == 1)
    {
        //%lineNum = randomInt(1,3);
        %line=dataRetrieve(%attacked, "line");
        if(%line == ""){return;}
        dataStore(%attacked, "line", "");
        say(0,1,%line);
    }
}

function loadLines()
{
    dataStore(myGetObjectId("MissionGroup/Imps/Guards/Apoc1"),      "line", "CYB_EA14.wav");
    //say(0,1,dataRetrieve(myGetObjectId("MissionGroup/Imps/Patrol3/Gorgon1"),"line"));
    dataStore(myGetObjectId("MissionGroup/Imps/Guards/Mino1"),      "line", "CYB_GN01.wav");
    dataStore(myGetObjectId("MissionGroup/Imps/Guards/Gorg1"),      "line", "CYB_RCCA15.wav");
    dataStore(myGetObjectId("MissionGroup/Imps/Depot/Myr1"),        "line", "CYB_GN26.wav");
    dataStore(myGetObjectId("MissionGroup/Imps/Depot/Myr2"),        "line", "CYB_GN41.wav");
    dataStore(myGetObjectId("MissionGroup/Imps/Patrol1/Disruptor"), "line", "CYB_GN03.wav");
    dataStore(myGetObjectId("MissionGroup/Imps/Patrol2/Paladin1"),  "line", "CYB_GN14.wav");
    dataStore(myGetObjectId("MissionGroup/Imps/Patrol2/Paladin2"),  "line", "CYB_GN41.wav");
    dataStore(myGetObjectId("MissionGroup/Imps/Patrol3/Gorg1"),     "line", "CYB_RCCA14.wav");
    dataStore(myGetObjectId("MissionGroup/Imps/Patrol3/Gorg2"),     "line", "CYB_1CCA15.wav");
}

//-------------Metagens------------------------------
function findMetagenSquadMate(%squadMate)
{
    %vehicle = myGetObjectId("playerSquad/squadMate" @ %squadMate);
    //say(0,1,"Debug::Looking at "@ %vehicle);
    if( getVehicleHudName( %vehicle ) == *IDPLT_NAME_TYRR )
    {
        vehicleMayTurn(%vehicle, 80, %squadMate);                                
    }    
    else if( getVehicleHudName( %vehicle ) == *IDPLT_NAME_GENERIC_CY_8 )   
    {
        vehicleMayTurn(%vehicle, 35, %squadMate);                                
    }    
    else if( getVehicleHudName( %vehicle ) == *IDPLT_NAME_GENERIC_CY_13 )   
    {
        vehicleMayTurn(%vehicle, 45, %squadMate);                                
    }    
    else if( getVehicleHudName( %vehicle ) == *IDPLT_NAME_GENERIC_CY_14 )   
    {
        vehicleMayTurn(%vehicle, 40, %squadMate);                                
    }    
    else if( getVehicleHudName( %vehicle ) == *IDPLT_NAME_GENERIC_CY_15 )   
    {
        vehicleMayTurn(%vehicle, 45, %squadMate);                                
    }    
    else if( getVehicleHudName( %vehicle ) == *IDPLT_NAME_GENERIC_CY_18 )   
    {
        vehicleMayTurn(%vehicle, 65, %squadMate);                                
    }    
    else if( getVehicleHudName( %vehicle ) == *IDPLT_NAME_GENERIC_CY_20 )   
    {
        vehicleMayTurn(%vehicle, 70, %squadMate);                                
    }    
    else if( getVehicleHudName( %vehicle ) == *IDPLT_NAME_GENERIC_CY_23 )   
    {
        vehicleMayTurn(%vehicle, 85, %squadMate);                                
    }    
    else if( getVehicleHudName( %vehicle ) == *IDPLT_NAME_GENERIC_CY_24 )   
    {
        vehicleMayTurn(%vehicle, 50, %squadMate);                                
    }    
    else if( getVehicleHudName( %vehicle ) == *IDPLT_NAME_GENERIC_CY_27 )   
    {
        vehicleMayTurn(%vehicle, 60, %squadMate);                                
    }    
    else if( getVehicleHudName( %vehicle ) == *IDPLT_NAME_GENERIC_CY_31 )   
    {
        vehicleMayTurn(%vehicle, 40, %squadMate);                                
    }    
}                                     
function vehicleMayTurn(%vehicle, %percentage, %squadMate)
{
    //say(0,1,"Debug: "@ %vehicle @" has a "@ %percentage @"% chance!");
    
    if( %percentage > randomInt(1, 100) )
    {
        if(!($traitorLine))
        {
            say(0,1, *IDSTR_CE2_RDF01, "CE2_RDF01.wav");
            $traitorLine = true;
        }
        setTeam(%vehicle, *IDSTR_TEAM_PURPLE);
        squawkEnabled( %vehicle, false );
        addToSet( myGetObjectId( "MissionGroup/Metagens" ), %vehicle );
        setHercOwner( %vehicle, "MissionGroup/Convoy");  //keep the metas from killing the convoy   
        setHercOwner( %vehicle, "MissionGroup/Imps/Flyers");  //keep the metas from killing the escape
        order( %vehicle, guard, "MissionGroup/Metagens/Patrol2/Shep");
        order( "MissionGroup/Metagens/Patrol2/Shep", guard, %vehicle);
        $traitor[%squadMate] = %vehicle;
        //say(0,1,"Debug::SquadMate is "@%squadMate);   
        //say(0,1,"Debug::traitor["@%squadMate@"] set");   
    }
}

function Meta::vehicle::onAttacked(%attacked, %attacker)
{
    if(%attacker == $playerId &&
       ( !($metaAttacked) )    )
       {
            findMetagenSquadMate(1);
            findMetagenSquadMate(2);
            findMetagenSquadMate(3);
            $metaAttacked = true;
            reloadObject("MissionGroup/Imps/Artillery", 100);
            order("MissionGroup/Imps/Artillery", attack, pick(playerSquad, %attacked) );
            schedule("say(0,1,\"CYB_GN45.wav\");", 4);
       }
}

function Meta::vehicle::onDestroyed()
{
    if(isGroupDestroyed(myGetObjectId("MissionGroup/Metagens/Alpha")))
    {
        $navMetaOn = false;
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;
        setNavMarker("MissionGroup/NAV/NavHeretic", false);
        if(missionObjective1.status != *IDSTR_OBJ_COMPLETED)
        {
            setNavMarker("MissionGroup/NAV/Nav001", true, -1);
            $navConvoyOn = true;
            navTheConvoy();
            say(0,1, *IDSTR_CE2_NEX01, "CE2_NEX01.wav");
        }
        else if(missionObjStatus() == ALL_COMPLETED)
        { endWhenSafe(); }
    }
}

function Dropship::vehicle::onDisabled(%this)
{
    damageObject(%this,50000);
}

function Dropship::vehicle::onDestroyed(%destroyed, %destroyer)  //The metas retaliate if you take thier ride.
{
    //say(0,1,"Debug::Dropship has been destroyed.");
    setHudTimer( 0, -1, "", 1);
    if( missionObjective2.status == *IDSTR_OBJ_ACTIVE ||
        missionObjective2.status == *IDSTR_OBJ_FAILED )
    {
        missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    }  
    if(missionObjStatus() == ALL_COMPLETED){endWhenSafe();}    
    else if(!(isGroupDestroyed(myGetObjectId("MissionGroup/Metagens/Alpha"))))
    {
        order("MissionGroup/Metagens/Alpha", Attack, pick("playerSquad"));
    }
}

function Dropship::vehicle::onArrived(%who, %where)
{
    if(%where == "MissionGroup/AwayPoint2"){forceToDebrief();}
}

function metagensAreEscaping()
{
    //say(0,1,"Debug::Escaping Called");
    if(getDistance($playerId, "MissionGroup/Imps/Flyers/KnightsDropShip" ) < 1400 &&
       getDistance($playerId, "MissionGroup/Imps/Flyers/KnightsDropShip" ) != 0 )  //the player is too close to load the metas
    {
        //say(0,1,"Debug::Player too close....attacking!");
        order("MissionGroup/Metagens/Alpha", attack, pick(playerSquad));
    }
    else if(!($dshipChanged))
    {
        if( missionObjective1.status == *IDSTR_OBJ_ACTIVE )
        {
            missionObjective1.status = *IDSTR_OBJ_IGNORE;
            missionObjective2.status = *IDSTR_OBJ_ACTIVE;
        }
        say(0,1, *IDSTR_CE2_RDF02, "CE2_RDF02.wav");
        $navMetaOn = false; 
        setTeam("MissionGroup/Imps/Flyers/KnightsDropShip", *IDSTR_TEAM_PURPLE);
        $dshipChanged = true;
        
        if(getDistance(myGetObjectId("MissionGroup/Metagens/Alpha/Meta1"), $playerId) > 1500)
        {damageObject(myGetObjectId("MissionGroup/Metagens/Alpha/Meta1"), 50000);}
        if(getDistance(myGetObjectId("MissionGroup/Metagens/Alpha/Meta2"), $playerId) > 1500)
        {damageObject(myGetObjectId("MissionGroup/Metagens/Alpha/Meta2"), 50000);}
        if(getDistance(myGetObjectId("MissionGroup/Metagens/Alpha/Meta3"), $playerId) > 1500)
        {damageObject(myGetObjectId("MissionGroup/Metagens/Alpha/Meta3"), 50000);}
        if(getDistance(myGetObjectId("MissionGroup/Metagens/Alpha/Meta4"), $playerId) > 1500)
        {damageObject(myGetObjectId("MissionGroup/Metagens/Alpha/Meta4"), 50000);}
        
        setHudTimer( 45, -1, *IDSTR_TIMER_CE2_1, 1);
        schedule("checkDropShip();",45);
    }
}

function checkDropShip()
{
    if( !(isGroupDestroyed("MissionGroup/Imps/Flyers/KnightsDropShip")) )
    {
        order("MissionGroup/Imps/Flyers/KnightsDropShip", Guard, "MissionGroup/AwayPoint2");
        setNavMarker("MissionGroup/NavHeretic", false);
        if(missionObjective2.status != *IDSTR_OBJ_COMPLETED)
        {missionObjective2.status = *IDSTR_OBJ_FAILED;}
        schedule("checkMissionFailure();", 10);
    }
}

function checkMissionFailure()
{
    if(missionObjective2.status == *IDSTR_OBJ_FAILED)
    {forceToDebrief();}
}

function navTheMetagen()
{
    if($navMetaOn)
    {
        %metagen = myGetObjectId("MissionGroup/Metagens/Alpha/Meta1");
        setPosition("MissionGroup/NAV/NavHeretic", getPosition(%metagen, x), getPosition(%metagen, y), getPosition(%metagen, z));
        schedule("navTheMetagen();", 0.1);
    }
}

//-------------Convoy------------------------------
function Convoy::vehicle::onArrived(%who, %where)
{
    //say(0,1,"Debug:: CONVOY onArrived Called for "@ %who @" @ "@ %where);
    if( %where == myGetObjectId("MissionGroup/Paths/Road/ConvoyEnd") )
        {convoyOrders();}
    else if(%where == myGetObjectId("MissionGroup/ConvoyObjectives/PersonnelReload") &&
            %who = myGetObjectId("MissionGroup/Convoy/PersonnelCargo"))
         {
            playAnimSequence(myGetObjectId("MissionGroup/Scenario/Outpost/PD/Defense1"), 0);
            playAnimSequence(myGetObjectId("MissionGroup/Scenario/Outpost/PD/Defense2"), 0);
            playAnimSequence(myGetObjectId("MissionGroup/Scenario/Outpost/PD/Defense3"), 0);
         }   
    else if(%where == myGetObjectId("MissionGroup/ConvoyObjectives/AmmoReload") &&
            %who = myGetObjectId("MissionGroup/Convoy/AmmoCargo"))
         {
            order("MissionGroup/Scenario/Outpost/Turrets", ShutDown, false);
         }   
    else if(%where == myGetObjectId("MissionGroup/ConvoyObjectives/FuelReload") &&
            %who = myGetObjectId("MissionGroup/Convoy/FuelCargo"))
         {
             reloadObject(myGetObjectId("MissionGroup/Imps/Guards"), 100);
         }   
}

function Convoy::vehicle::onAttacked(%attacked, %attacker)
{
    if($convoyFirstAttack)
    {
        reloadObject("MissionGroup/Imps/Artilley", 100);
        
        say(0,1,"CYB_CVb01.wav");                               //"We need help!"
        $convoyFirstAttack = false;
        order("MissionGroup/Convoy", Retreat);
        schedule("order(\"MissionGroup/Convoy\", Guard, \"MissionGroup/Paths/Road/ConvoyEnd\");", 40);
        if( ( !(isGroupDestroyed("MissionGroup/Imps/Depot") ) ) &&
            (missionObjective3.status == *IDSTR_OBJ_ACTIVE)    )
        {
            order("MissionGroup/Imps/Depot", Guard, %attacked);
            say(0,1,"GEN_ICCa02.wav");
        }                                                       // "Moving to cover"
        if( ( !(isGroupDestroyed("MissionGroup/Imps/Patrol3") ) ) &&
            (missionObjective3.status == *IDSTR_OBJ_ACTIVE)    )
        {
            order("MissionGroup/Imps/Patrol3", Guard, %attacked);
            say(0,1,"CYB_EA07.wav");
        }                                                       // "On our way"
    }
}

function Convoy::vehicle::onDestroyed()
{
    if( isGroupDestroyed( myGetObjectId( "MissionGroup/Convoy/PersonnelCargo" ) ) &&
        isGroupDestroyed( myGetObjectId( "MissionGroup/Convoy/AmmoCargo" ) ) &&
        isGroupDestroyed( myGetObjectId( "MissionGroup/Convoy/FuelCargo" ) ) &&
        missionObjective3.status == *IDSTR_OBJ_ACTIVE )
    {
        missionObjective3.status = *IDSTR_OBJ_COMPLETED;
        if(missionObjective1.status != *IDSTR_OBJ_COMPLETED)
        {
            setNavMarker("MissionGroup/NAV/Nav001", false);
            setNavMarker("MissionGroup/NAV/Metagen", true, -1);
            $navConvoyOn = false;
            say(0,1, *IDSTR_CYB_NEX04, "CYB_NEX04.wav");
        }
        if(missionObjStatus() == ALL_COMPLETED)
        { endWhenSafe();}
    }
}

function convoyOrders()
{
    //say(0,1,"Debug::Convoy Orders Recieved");     
    $conArrived = true;
    order("MissionGroup/Convoy/Leader", MakeLeader, false);
    order("MissionGroup/Convoy/Leader", Guard, "MissionGroup/ConvoyObjectives/LeaderPark");    
    order("MissionGroup/Convoy/PersonnelCargo", Guard, "MissionGroup/ConvoyObjectives/PersonnelReload");    
    order("MissionGroup/Convoy/AmmoCargo", Guard, "MissionGroup/ConvoyObjectives/AmmoReload");    
    order("MissionGroup/Convoy/FuelCargo", Guard, "MissionGroup/ConvoyObjectives/FuelReload");
    
    order("MissionGroup/Imps/Guards", Guard,"MissionGroup/Paths/InnerCityPatrol");
    
    if(missionObjective3.status != *IDSTR_OBJ_COMPLETED)
    { obj4(); }
}
function obj4()
{
    if(missionObjective4.status != *IDSTR_OBJ_ACTIVE)
    {
        $navConvoyOn = false;
        say(0,1, *IDSTR_CE2_RDF03, "CE2_RDF03.wav");
        missionObjective3.status = *IDSTR_OBJ_IGNORE;
        missionObjective4.status = *IDSTR_OBJ_ACTIVE;
    }
    
}
function navTheConvoy()
{
    if($navConvoyOn)
    {
        %convoy = myGetObjectId("MissionGroup/Convoy/Leader");
        setPosition("MissionGroup/NAV/Nav001", getPosition(%convoy, x), getPosition(%convoy, y), getPosition(%convoy, z));
        schedule("navTheConvoy();", 0.1);
    }
}

//-------------Outpost------------------------------

function turret::onDestroyed()
{
    //say(0,1,"Debug::A turret has been destroyed.");
}

function PD::structure::onDestroyed()
{
    //say(0,1,"CYB_EA06.wav");
    if(isGroupDestroyed(myGetObjectId("MissionGroup/Scenario/Outpost/PD")))
    {
        //say(0,1,"CYB_EA20.wav");
        if(missionObjective4.status == *IDSTR_OBJ_ACTIVE)
        {
            missionObjective4.status = *IDSTR_OBJ_COMPLETED;
            if(missionObjStatus() == ALL_COMPLETED)
            { endWhenSafe(); }
        }
    }
}

//==============Imps============================

function flyer::vehicle::onArrived(%who, %where)
{
    if( %where == myGetObjectId("MissionGroup/BansheeLanding/Pad1") &&
        !($flyerLanded)){
        
    $flyerAttacked = false;
    $flyerLanded = false;
    schedule("flyerOrders();",40);}
}
function flyer::vehicle::onAttacked()
{
    if(!($flyerAttacked))
    {
        $flyerAttacked = true;
        flyerOrders();
    }
}

function flyerOrders()
{
    order(myGetObjectId("MissionGroup/Imps/Flyers/TerranBanshee"), Guard, myGetObjectId(pick("MissionGroup/Metagens","MissionGroup/Cybrids","playerSquad")) );
    schedule("order(\"MissionGroup/Imps/Flyers/TerranBanshee\", Guard, \"MissionGroup/BansheeLanding/Pad1\");", 200);
    schedule("$flyerLanded = false;", 10);
}

//Misc functions============================================================
function myGetObjectId(%this)
{
    %id = getObjectId(%this);
    if (%id == "")
    {
        //say(0,1,"Problem with id for " @ %this);
    }
    else
    {return %id;}    
}

function endWhenSafe(%lastObjective)
{
    if(isSafe(*IDSTR_TEAM_YELLOW, $playerId, 1000))
    {
        killRemainingTraitors();
        forceToDebrief();
    }
    else{schedule("endWhenSafe();",10);}
}

function killRemainingTraitors()
{
    focusServer();
    if($traitor[1] != 0 ) {damageObject($traitor[1], 30000);}
    if($traitor[2] != 0 ) {damageObject($traitor[2], 30000);}
    if($traitor[3] != 0 ) {damageObject($traitor[3], 30000);}
    focusClient();
}

function cinematicDrop()
{
   %next    = randomInt(10, 30);
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
   setDropPodParams(0, 0, -0.935, ( (getTerrainHeight(%x, %y) ) + 1500 )  );
   dropPod( %x, %y, getTerrainHeight(%x, %y) );
}

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    missionObjective3.status = *IDSTR_OBJ_COMPLETED;
    missionObjective4.status = *IDSTR_OBJ_COMPLETED;
    killRemainingTraitors();
    updatePlanetInventory(CE2);
    forceToDebrief();
}
