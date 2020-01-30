

//////////////////////////////////////////////////////////
// CE3 Isolate/Inoculate/Annihilate                     //
//                                                      //
// Primary Goals                                        //
// 1.  Destroy defesive outpost at Nav001               //
//  Secondary Goals                                     //
// 3.  Destroy all resistance                           //
//////////////////////////////////////////////////////////



MissionBriefInfo missionBriefInfo               
{                                               
	campaign            = *IDSTR_CE3_CAMPAIGN;     
	title               = *IDSTR_CE3_TITLE;        
	planet              = *IDSTR_PLANET_DESERT;      
	location            = *IDSTR_CE3_LOCATION;     
	dateOnMissionEnd    = *IDSTR_CE3_DATE;         
	media               = *IDSTR_CE3_MEDIA;        
	nextMission         = *IDSTR_CE3_NEXTMISSION;  
	successDescRichText = *IDSTR_CE3_DEBRIEF_SUCC; 
	failDescRichText    = *IDSTR_CE3_DEBRIEF_FAIL; 
	shortDesc           = *IDSTR_CE3_SHORTBRIEF;   
	longDescRichText    = *IDSTR_CE3_LONGBRIEF;
    successWavFile      = "CE3_Debriefing.wav";
    soundVol = "CE3.vol";    
};                                              
                                                
MissionBriefObjective  missionObjective1         //    DESTROY OUTPOST
{
	isPrimary           = True;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CE3_OBJ1_SHORT;
	longTxt             = *IDSTR_CE3_OBJ1_LONG;
    bmpname             = *IDSTR_CE3_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2         // KILL EVERYONE
{
	isPrimary           = False;
	status              = *IDSTR_OBJ_ACTIVE;
	shortTxt            = *IDSTR_CE3_OBJ2_SHORT;
	longTxt             = *IDSTR_CE3_OBJ2_LONG;
    bmpname             = *IDSTR_CE3_OBJ2_BMPNAME;
};

// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// ------------------------------------------------------------------------------
// Script
// ------------------------------------------------------------------------------

function player::onAdd(%this)
{
    $thePlayerNum = %this;
    $playerAlive = True;
    //setDropPodParams(dir.x,dir.y,dir.z,[dropHeight],[dropSpeed],[dropNearDist]);
    setDropPodParams(0.25,0.25,-0.7, 5000, 300, 2000);
}

// ------------------------------------------------------------------------------

function onMissionStart()
{
    defineActors();
    defineCounts();
    defineRoutes();
    initFormations();
    desertSounds();
    cdAudioCycle(Purge, SS3, Cyberntx);
}

// ------------------------------------------------------------------------------

function defineActors()
{   
    //----Cybrid----------------------------------------
    
    $cFlyerGroup =  "MissionGroup/vehicles/cybrid/flyerGroup";
    $cFlyer1 =  "MissionGroup/vehicles/cybrid/flyerGroup/flyer1";
    $cFlyer2 =  "MissionGroup/vehicles/cybrid/flyerGroup/flyer2";
    $cFlyer3 =  "MissionGroup/vehicles/cybrid/flyerGroup/flyer3";
    $fodder =   "MissionGroup/vehicles/cybrid/fodderGroup";
    $fodder1 =   "MissionGroup/vehicles/cybrid/fodderGroup/fodder1";
    $fodder2 =   "MissionGroup/vehicles/cybrid/fodderGroup/fodder2";
    $fodder3 =   "MissionGroup/vehicles/cybrid/fodderGroup/fodder3";
    $help   =   "MissionGroup/vehicles/cybrid/helpGroup";
    $help1  =   "MissionGroup/vehicles/cybrid/helpGroup/help1";
    $help2  =   "MissionGroup/vehicles/cybrid/helpGroup/help2";
    $help3  =   "MissionGroup/vehicles/cybrid/helpGroup/help3";

    
    //----Human----------------------------------------  
    
    $hs1       =   "MissionGroup/vehicles/human/hs1";
    $hs1a      =   "MissionGroup/vehicles/human/hs1/hs1a";
    $hs1b      =   "MissionGroup/vehicles/human/hs1/hs1b";
    $hs1c      =   "MissionGroup/vehicles/human/hs1/hs1c";
    $hs2       =   "MissionGroup/vehicles/human/hs2";
    $hs2a      =   "MissionGroup/vehicles/human/hs2/hs2a";
    $hs2b      =   "MissionGroup/vehicles/human/hs2/hs2b";
    $hs3       =   "MissionGroup/vehicles/human/hs3";
    $hs3a      =   "MissionGroup/vehicles/human/hs3/hs3a";
    $hs3b      =   "MissionGroup/vehicles/human/hs3/hs3b";
    $hs3c      =   "MissionGroup/vehicles/human/hs3/hs3c";
    $hs4       =   "MissionGroup/vehicles/human/hs4";
    $hs4a      =   "MissionGroup/vehicles/human/hs4/hs4a";
    $hs4b      =   "MissionGroup/vehicles/human/hs4/hs4b";
    $hs4c      =   "MissionGroup/vehicles/human/hs4/hs4c";
    $hs5       =   "MissionGroup/vehicles/human/hs5";
    $hs5a      =   "MissionGroup/vehicles/human/hs5/hs5a";
    $hs5b      =   "MissionGroup/vehicles/human/hs5/hs5b";
    $hs5c      =   "MissionGroup/vehicles/human/hs5/hs5c";
    
    $drop      =   "MissionGroup/vehicles/human/flyers/drop";
    $con       =   "MissionGroup/vehicles/human/flyers/conveyor";
    $flyer1    =   "MissionGroup/vehicles/human/flyers/flyer1";
    
    $artillery =  "MissionGroup/vehicles/human/artillery1";
    $art1      =   getObjectId("MissionGroup/vehicles/human/artillery1/art1");
    $art2      =   getObjectId("MissionGroup/vehicles/human/artillery1/art2");
    $art3      =   getObjectId("MissionGroup/vehicles/human/artillery1/art3");
    $ammo1     =   "MissionGroup/vehicles/human/drones/ammo1";
    $ammo2     =   "MissionGroup/vehicles/human/drones/ammo2"; 
    $ambush    =   "MissionGroup/vehicles/human/ambushGroup";
    $bush1     =   "MissionGroup/vehicles/human/ambushGroup/bush1";
    $bush2     =   "MissionGroup/vehicles/human/ambushGroup/bush2";
    $bush3     =   "MissionGroup/vehicles/human/ambushGroup/bush3";
    $artTargets =  "MissionGroup/extras/artTargets";
    
    $nav1      =   "MissionGroup/navMarkers/nav1";
    $point     =   "MissionGroup/extras/point";
}

// ------------------------------------------------------------------------------

function defineCounts()
{
    $hs1Attacking   =   false;
    $hs2Attacking   =   false;
    $hs3Attacking   =   false;
    $hs4Attacking   =   false;
    $hs5Attacking   =   false;
    $hsCount        =   16;
    $primeCount     =   8;
    $cybridsAttacked =  0;
    $artAttacking   =   false;
    $warnTalk       =   false;
    $failTalk       =   false;
    $warn2Talk      =   false;
    $fail2Talk      =   false;
    $deadCount      =   0;
    $start          =   false;
    $artRetreating  =   false;
    $reinforce      =   false;
    $checks         =   false;
    $hercsHurt      =   false;
    $dissing        =   false;
    $conDestroyed   =   false;
}

// ------------------------------------------------------------------------------

function defineRoutes()
{
    //----Cybrid----------------------------------------
    
    $flyerRoute1    =   "MissionGroup/routes/cybrid/flyerRoute1";
    $flyerRoute2    =   "MissionGroup/routes/cybrid/flyerRoute2";
    $flyerRoute3    =   "MissionGroup/routes/cybrid/flyerRoute3";
    $marker3        =   "MissionGroup/routes/cybrid/flyerRoute1/marker3";
    $bomber1Route   =   "MissionGroup/routes/cybrid/bomber1Route";
    $bomber2Route   =   "MissionGroup/routes/cybrid/bomber2Route";
    $b1Marker3      =   "MissionGroup/routes/cybrid/bomber1Route/marker3";
    $b2Marker3      =   "MissionGroup/routes/cybrid/bomber2Route/marker3";
    
    //----Human----------------------------------------
    
    $flyer1Route    =   "MissionGroup/routes/human/flyer1Route";
    $flyer2Route    =   "MissionGroup/routes/human/flyer2Route";
    $hs1Route       =   "MissionGroup/routes/human/hs1Route";
}

// ------------------------------------------------------------------------------

function initFormations()
{
    newFormation( wall,   0,0,0, 
                          20,0,0, 
                          40,0,0 );
    newFormation( line,   0,0,0,
                          0,-30,0,
                          0,-60,0 );
    newFormation( wedge,   0,0,0,
                          -30,-20,0,
                          30,-20,0 );
    newFormation( diamond, 0,0,0,
                           -40,-40,0,
                           0,-80,0,
                           -40,40,0 );                           
}

// ------------------------------------------------------------------------------

function vehicle::onAdd(%this)
{
    if($thePlayerNum == playerManager::vehicleIdToPlayerNum(%this))
    {
        $thePlayer = %this;
        setVehicleRadarVisible(getObjectId($bush1), false);
        setVehicleRadarVisible(getObjectId($bush2), false);
        setVehicleRadarVisible(getObjectId($bush3), false);
    }
}

// ------------------------------------------------------------------------------

function onSPClientInit()
{
    initHumanPatrols();
    initCybrids();
    schedule( "setNavMarker( getObjectId($nav1), true, -1 );", 2 );
    schedule( "startAmbush();", 3 );
}

// ------------------------------------------------------------------------------

function startAmbush()
{
    order( $ambush, cloak, true );
    order( $ambush, guard, playerSquad );
    schedule( "order( $ambush, attack, playerSquad );", 10 );
    checkBoundary( enter, $thePlayer, getObjectId($bush1), 300, radar );
}

// ------------------------------------------------------------------------------

function radar()
{
    setVehicleRadarVisible(getObjectId($bush1), true);
    setVehicleRadarVisible(getObjectId($bush2), true);
    setVehicleRadarVisible(getObjectId($bush3), true);
}

// ------------------------------------------------------------------------------
                               //  CYBRID  //
// ------------------------------------------------------------------------------

function initCybrids()
{
    CybridDistanceChecks();
    // human owns:
    setHercOwner( "playerSquad/squadMate1", $artillery );
    setHercOwner( "playerSquad/squadMate2", $artillery );
    setHercOwner( "playerSquad/squadMate3", $artillery );
}

// ------------------------------------------------------------------------------

function initCybridPatrols()
{
    setVehicleRadarVisible(getObjectId($bush1), true);
    setVehicleRadarVisible(getObjectId($bush2), true);
    setVehicleRadarVisible(getObjectId($bush3), true);
}

// ------------------------------------------------------------------------------

function cybridDistanceChecks()
{
    if( $checks == false )
    {
        checkBoundary( enter, $thePlayer, getObjectId($fodder1), 750, fodderRelease);
    } 
}

// ------------------------------------------------------------------------------

function hs::vehicle::onAttacked(%attd,%attr)
{
    if( ((%attd == getObjectId($bush1)) || (%attd == getObjectId($bush3)) || 
        (%attd == getObjectId($bush3))) && (getTeam(%attr) == *IDSTR_TEAM_YELLOW) )
    {
        initCybridPatrols();
    }
}

// ------------------------------------------------------------------------------

function cybrid::vehicle::onArrived(%who, %where)
{
    
    if( (%who == getObjectId($cFlyer1)) && (%where == getObjectId($marker3)) )
    {
        schedule( "damageArea(getObjectId($cFlyer1), 0,0,0,5,7000);", 5 );
    }
    if( (%who == getObjectId($cFlyer2)) && (%where == getObjectId($b1marker3)) )
    {
        order( $cFlyer2, guard, $flyerRoute2 );
        schedule( "damageArea(getObjectId($ammo1), 0,0,0,5,5000);", 1 );
        schedule( "damageArea(getObjectId($cFlyer2), 0,0,0,5,7000);", 5 );
    }
    if( (%who == getObjectId($cFlyer3)) && (%where == getObjectId($b2marker3)) )
    {
        order( $cFlyer3, guard, $flyerRoute3 );
        schedule( "damageArea(getObjectId($ammo2), 0,0,0,5,5000);", 1 );
        schedule( "damageArea(getObjectId($cFlyer3), 0,0,0,5,7000);", 6 );
    }
}

// ------------------------------------------------------------------------------

function fodderRelease()
{
    if( $hercsHurt != true )
    {
        say( 0, 1, *IDSTR_CE3_SUB01, "CE3_SUB01.WAV" );
        schedule( "hs1Talks( \"CYB_GN08.wav\" );", 2 );   // they're coming
        damageHercs();
        // fodder
        order($fodder, attack, $artillery);
        schedule( "damageArea(getObjectId($fodder1), 6,4,0,5,2000);", 25 );
        schedule( "damageArea(getObjectId($fodder2), 5,5,10,12,2000);", 45 );
        schedule( "damageArea(getObjectId($fodder3), 4,6,8,10,2000);", 50 );
    
        $hercsHurt = true;
        schedule( "order( $cFlyer1, guard, $flyerRoute1 );", 20 );
        schedule( "order( $cFlyer1, flyThrough, true );", 30 );
        
        schedule( "order( $cFlyer2, guard, $bomber1Route );", 15 );
        schedule( "order( $cFlyer2, flyThrough, true );", 20 );
        
        schedule( "order( $cFlyer3, guard, $bomber2Route );", 15 );
        schedule( "order( $cFlyer3, flyThrough, true );", 20 );
    }    
}

// ------------------------------------------------------------------------------

function cavalrySet()
{
    setPosition( getObjectId($help1), -99,-1859,92 );      
    setPosition( getObjectId($help2), -487,-1488,210 );
    setPosition( getObjectId($help3), -451,-1499,210 );
    order( $help, guard, $thePlayer );
    order( $help, holdposition, true );
    order( $help, speed, high );
    say( 0, 1, *IDSTR_CE3_SUB02, "CE3_SUB02.WAV" );
}

// ------------------------------------------------------------------------------
                    // HUMAN //
// ------------------------------------------------------------------------------

function initHumanPatrols()
{
    humanDistanceChecks();
    order( $hs1a, makeLeader, true );
    order( $hs1, guard, $hs1Route );
    
    order( $hs2a, makeleader, true );
    order( $hs3a, makeleader, true );
    order( $hs4a, makeleader, true );
    order( $hs5a, makeleader, true );
    
    order( $artillery, shutdown, true );
    
    order( $drop2, shutdown, true );
    order( $drop3, shutdown, true );
}

// ------------------------------------------------------------------------------

function damageHercs()
{
    damageArea(getObjectId("MissionGroup/vehicles/human/hs1/hs1b"), 0,14,5,16,2000);
    damageArea(getObjectId("MissionGroup/vehicles/human/hs1/hs1c"), 0,-10,0,14,3000);
    damageArea(getObjectId("MissionGroup/vehicles/human/hs3/hs3b"), 5,5,5,8,2000);
    damageArea(getObjectId("MissionGroup/vehicles/human/hs4/hs4b"), 0,5,5,8,2000);    
}

// ------------------------------------------------------------------------------

function humanDistanceChecks()
{
    checkBoundary( enter, $thePlayer, getObjectId($nav1), 2500, initArtAttack );
    checkBoundary( enter, $thePlayer, getObjectId($nav1), 1800, hs1Attack );
    checkBoundary( enter, $thePlayer, getObjectId($nav1), 800, artRetreat );
    checkBoundary( leave, $thePlayer, getObjectId($nav1), 5000, warn );
    checkBoundary( leave, $thePlayer, getObjectId($nav1), 5500, fail );
}

// ------------------------------------------------------------------------------

function initArtAttack(%this)
{
    if( isSafe(*IDSTR_TEAM_YELLOW, $thePlayer, 800) && ($hs1Attacking == false) )
    {
        order( $artillery, shutdown, false );
        artAttack($art1);
        artAttack($art2);
        artAttack($art3);
        conStart();
        $checks = true;
        if( $start != true )
        {
            schedule( "hs1Talks( \"GEN_RCCb03.WAV\" );", 2 );   // more coming in
            $start = true;
        }
        if( isGroupDestroyed($hs1) == false )
        {
            order( $hs1a, guard, "MissionGroup/routes/human/hs1Route/marker1" );
            order( $hs1b, guard, "MissionGroup/routes/human/hs1Route/marker2" );
            order( $hs1c, guard, "MissionGroup/routes/human/hs1Route/marker3" );
        }    
    }
    else if( $artRetreating != true )
    {
        order( $artillery, shutdown, true );
    }
}

// ------------------------------------------------------------------------------

function artRetreat()
{
    if( $artRetreating != true )
    {
        artTalks( "CYB_ARA03.wav" );                // could use some defence 
        schedule( "artTalks(\"CYB_ARB04.wav\");", 10 );
        order( $artillery, shutdown, false );
        order( $artillery, clear, true );
        order( $artillery, guard, "MissionGroup/extras/guard" );
        order( $artillery, speed, high );
        order( $artillery, holdposition, true );  
        droneRetreat();
        $artRetreating = true;
    }
}

// ------------------------------------------------------------------------------

function droneRetreat()
{
    order( $ammo, guard, "MissionGroup/extras/guard" );
    order( $ammo, speed, high );  
}

// ------------------------------------------------------------------------------

function artAttack(%this)
{
    order(%this, Attack, playerSquad );
}

// ------------------------------------------------------------------------------

function vehicle::onMessage(%this, %message, %_1, %_2, %_3, %_4, %_5, %_6, %_7, %_8, %_9)
{
   // at this point the equality %this == getId($artillery) should hold
   if (%message == "ArtilleryOutOfAmmo") 
   {
        artilleryClearTarget(%this);
        reloadObject(%this, 30 );
        
   }
   else if (%message == "ArtilleryOutOfRange")
   {
        artilleryClearTarget(%this);
   }
}

// ------------------------------------------------------------------------------

function artilleryClearTarget(%this)
{
   order(%this, clear, true);
   schedule( "initArtAttack(%this);", 2 );
}

// ------------------------------------------------------------------------------

function hs1Attack()
{
    if( $hs1Attacking == false )
    {
        
        order($artillery, clear, true);
        order( $hs1, attack, playerSquad );
        order( $hs1, speed, high );
        hs1Talks( "CYB_GN15.wav" );         //   converg on their push
        schedule( "hs1Talks( \"CYB_GN03.wav\" );", 3 );      // I'm on leader
        schedule( "hs1Talks( \"CYB_GN09.wav\" );", 8 );       // watch flank
        schedule( "hs1Talks( \"CYB_EA02.wav\" );", 15 );       // where's reinforcements
        schedule( "hs4Talks( \"CYB_EA07.wav\" );", 18 );       // on the way
        schedule( "hs1Talks( \"CYB_GN34.wav\" );", 20 );       // all over me
        $hs1Attacking = true;
    }
}

// ------------------------------------------------------------------------------

function hs2Attack()
{
    if( $hs2Attacking == false )
    {
        hs2Talks( "CYB_GN31.wav" );                          // wait for it
        schedule( "hs2Talks( \"CYB_GN38.wav\" );", 5 );      // screw protocol, going in
        schedule( "hs2Talks( \"CYB_EA08.wav\" );", 10 );      // keep them off high ground
        schedule( "hs2Talks( \"CYB_GN14.wav\" );", 15 );     // were's that artillery
        schedule( "hs2Talks( \"CYB_GN10.wav\" );", 18 );     // coming in too fast
        order( $hs2, attack, playerSquad );
        $hs2Attacking = true;
        schedule( "cavalrySet();", 60 );      
    }
}

// ------------------------------------------------------------------------------

function hs3Attack()
{
    if( $hs3Attacking == false )
    {
        hs3Talks( "CYB_GN25.wav" );                            // cluster and move
        schedule( "hs3Talks( \"CYB_GN03.wav\" );", 6 );        // i'm on the leader
        schedule( "hs3Talks( \"CYB_GN26.wav\" );", 15 );        // maintain cover
        schedule( "hs3Talks( \"CYB_GN10.wav\" );", 17 );        // coming in too fast
        schedule( "hs3Talks( \"CYB_EA10.wav\" );", 20 );        // keep them away from quarters
        order( $hs3, guard, $point );
        order( $hs3, speed, high );
        $hs3Attacking = true;   
    }
}

// ------------------------------------------------------------------------------

function hs4Attack()
{
    if( $hs4Attacking == false )
    {
        schedule( "hs4Talks( \"CYB_EA15.wav\" );", 4 );     // by the numbers, go
        schedule( "hs4Talks( \"CYB_GN12.wav\" );", 9 );     // keep pushing
        schedule( "hs4Talks( \"CYB_EA21.wav\" );", 15 );    // where's the knights
        schedule( "hs4Talks( \"CYB_GN07.wav\" );", 23 );    // get them off my six
        schedule( "hs4Talks( \"CYB_EA05.wav\" );", 27 );    // i got ya, buddy
        order( $hs4, attack, playerSquad );
        order( $hs4, holdposition, true );
        
        $hs4Attacking = true;      
    }
}

// ------------------------------------------------------------------------------

function hs5Attack()
{
    if( $hs5Attacking == false )
    {
        order( $hs5, guard, $point );
        order( $hs5, speed, high );
        $hs5Attacking = true;      
    }
}

// ------------------------------------------------------------------------------

function hs::vehicle::onArrived(%who, %where)
{
    if( (%who == getObjectId($con)) && (%where == getObjectId("MissionGroup/routes/human/drop2Route/marker3")) )
    {
        order( $con, speed, slow );
        schedule( "order( $con, shutdown, true );", 3 );
        setPosition( getObjectId($hs4a), -99,-1859,92 );      
        setPosition( getObjectId($hs4b), -487,-1488,210 );
        setPosition( getObjectId($hs4c), -451,-1499,210 );
        schedule( "hs4Attack();", 1 );
        schedule( "order($con, shutdown, false);", 5 );
        schedule( "order($con, guard, \"MissionGroup/routes/human/drop1Route/marker1\");", 5.1 );
        if( $conArrived != true )
        {
            conTalks( "CYB_DS11.wav" );
            $conArrived = true;
        }
    }
    if( (%who == getObjectId($drop)) && (%where == getObjectId("MissionGroup/routes/human/drop1Route/marker2")) )
    {
        order( $drop, speed, slow );
        schedule( "order( $drop, shutdown, true );", 3 );
        setPosition( getObjectId($hs3a), -131,1191,142 );
        setPosition( getObjectId($hs3b), -150,1200,142 );
        setPosition( getObjectId($hs3c), -100,1200,142 );
        schedule( "order( $hs3, guard, $point );", 1);
        schedule( "order($drop, shutdown, false);", 5 );
        schedule( "order($drop, guard, \"MissionGroup/routes/human/drop1Route/marker1\");", 5.1 );
    }
    if( (%who == getObjectId($flyer1)) && (%where == getObjectId("MissionGroup/routes/human/drop3Route/marker2")) )
    {
        order( $flyer, speed, slow );
        schedule( "order( $flyer1, shutdown, true );", 3 );
        setPosition( getObjectId($hs5a), -155, 2678, 110 );
        setPosition( getObjectId($hs5b), -137, 2705, 111 );
        setPosition( getObjectId($hs5c), -185, 2716, 110 );
        schedule( "order($flyer1, shutdown, false);", 5 );
        schedule( "order($flyer1, guard, \"MissionGroup/routes/human/drop3Route/marker1\");", 5.1 );
    }
    if( (%who == getObjectId($flyer1)) && (%where == getObjectId("MissionGroup/routes/human/drop4Route/marker2")) )
    {
        order( $flyer, speed, slow );
        schedule( "order( $flyer1, shutdown, true );", 3 );
        setPosition( getObjectId($hs5a), 1004,2155,174 );
        setPosition( getObjectId($hs5b), 1034,2155,174 );
        setPosition( getObjectId($hs5c), 1034,2185,174 );
        schedule( "order($flyer1, shutdown, false);", 5 );
        schedule( "order($flyer1, guard, \"MissionGroup/routes/human/drop4Route/marker1\");", 5.1 );
    }
}

// ------------------------------------------------------------------------------

function hs::vehicle::onDestroyed(%destroyed, %destroyer)
{
    $hsCount--;
    if( $destroyed == getObjectId($con) )
    {
        $conDestroyed = true;
    }
    if( (isGroupDestroyed($ambush)) && ($dissing != true) )
    {
        initCybrids();
        $dissing = true;
    }
    if( (%destroyed == getObjectId($hs1a)) || (%destroyed == getObjectId($hs1b)) || (%destroyed == getObjectId($hs1c)) )
    {
        order( $flyer1, guard, "MissionGroup/routes/human/drop3Route/marker2");  //, "MissionGroup/routes/human/drop4Route"));
        dropStart();
        
    }
    if( isGroupDestroyed($hs1) )
    {
        hs2Attack();            
    }
    if( isGroupDestroyed($hs2 ))
    {
        hs3Attack();
        if( $conDestroyed == true )
            hs5Attack();
    }
    if( isGroupDestroyed($hs3) )
    {
        hs5Attack();            
    }
    if( $hsCount <= 0 )
        missionObjective2.status = *IDSTR_OBJ_COMPLETED;
}

// ------------------------------------------------------------------------------

function prime::structure::onDestroyed(%destroyed, %this)
{
    $primeCount--;
    if( $primeCount <= 0 )
    {
        safeCheck();
    }
}

// ------------------------------------------------------------------------------

function prime::turret::onDestroyed(%destroyed, %this)
{
    $primeCount--;
    if( $primeCount <= 0 )
    {
        
        safeCheck();
    }
}

// ------------------------------------------------------------------------------

 function dropStart()                                                            
 {                                                                                 
     order( $drop, guard, "MissionGroup/routes/human/drop1Route/marker2" );
     order( $drop, speed, high );                                        
 }                                                                                 
                                                                                   
 // ------------------------------------------------------------------------------ 
                                                                                   
 function conStart()                                                           
 {                                                                                 
     order( $con, guard, "MissionGroup/routes/human/drop2Route/marker3" );
     order( $con, speed, high );          
}                                                                                 

// ------------------------------------------------------------------------------

function warn()
{
    if( $warnTalk == 0 )
    {
        nexTalks( *IDSTR_CYB_NEX01, "CYB_NEX01.WAV" );  // Off course
        $warnTalk++;
    }
}

// ------------------------------------------------------------------------------

function fail()
{
    if( $failTalk == 0 )
    {
        nexTalks( *IDSTR_CYB_NEX02, "CYB_NEX02.WAV" );    // mission failed
        schedule( "forceToDebrief(*IDSTR_MISSION_FAILED);", 2 );
        $failTalk++;
    }    
}

// ------------------------------------------------------------------------------

function safeCheck()
{
    if( isSafe(*IDSTR_TEAM_YELLOW, getObjectId($nav1), 1500) )
    {
        missionObjective1.status = *IDSTR_OBJ_COMPLETED;   
        say( 0, 1, *IDSTR_CYB_NEX04, "CYB_NEX04.wav" );
        youWin();
    }
    else
        schedule( "safeCheck();", 2 );
}

// ------------------------------------------------------------------------------

function youWin()
{
    if( missionObjective1.status == *IDSTR_OBJ_COMPLETED )
    {       
        updatePlanetInventory(ce3);
        schedule( "artTalks(\"CYB_EA19.wav\");", 3 );
        schedule( "artTalks(\"CYB_EA18.wav\");", 7 );
        schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 7.0);
    }
    else
        safeCheck();
}

// ------------------------------------------------------------------------------

function win()
{
    missionObjective1.status = *IDSTR_OBJ_COMPLETED;
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    updatePlanetInventory(ce3);
    schedule( "forceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 3.0);    
}

// ------------------------------------------------------------------------------

function nexTalks(%text, %wave)
{
    if( $playerAlive  )
        say( 0, 1, %text, %wav );
    else
    {
        say( 0, 1, "" );
    }        
}

// ------------------------------------------------------------------------------

function artTalks(%wav)
{
    if( $playerAlive  )
        say( 0, 2, %wav );
    else
    {
        killChanel(2);
    }
}

// ------------------------------------------------------------------------------

function hs1Talks(%wav)
{
    if( isGroupDestroyed($hs1) == false )
        say( 0, 3, %wav );
    else
    {
        killChanel(3);
    }
}

// ------------------------------------------------------------------------------

function hs2Talks(%wav)
{
    if( isGroupDestroyed($hs2) == false )
        say( 0, 4, %wav );
    else
    {
        killChanel(4);
    }
}

// ------------------------------------------------------------------------------

function hs3Talks(%wav)
{
    if( isGroupDestroyed($hs3) == false )
        say( 0, 5, %wav );
    else
    {
        killChanel(5);
    }
}

// ------------------------------------------------------------------------------

function hs4Talks(%wav)
{
    if( isGroupDestroyed($hs4) == false )
        say( 0, 6, %wav );
    else
    {
        killChanel(6);
    }
}

// ------------------------------------------------------------------------------

function conTalks(%wav)
{
    if( $playerAlive ) 
        say( 0, 8, %wav );
    else
    {
        killChanel(8);
    }
}

// ------------------------------------------------------------------------------

function windTalks(%wav)
{
    if( $playerAlive ) 
        say( 0, 9, %wav );
}

function wind2Talks(%wav)
{
    if( $playerAlive ) 
        say( 0, 10, %wav );
}

// ------------------------------------------------------------------------------
