//-----------------------------------------------------------------------------
//
// Mission script for CC3 ( "Escort//Insert" )
//
// Synopsis: Escort the Cybrid Nexus ( OmniCrawler ) safely to NAV 003
//
// Mission Objectives:
// 
//  1) Escort mobile nexus to Nav001 and await instructions
//  2) Escort mobile nexus to Nexus Prime at Nav002 [changes en route]
//  3) Escort mobile nexus to Nav003 for emergency extraction [added in-mission]
//  4) Ensure that mobile nexus is not destroyed
//  5) Destroy all enemies in the area
//
//-----------------------------------------------------------------------------


//-----------------------------------------------------------------------------
// Initialize mission data
//-----------------------------------------------------------------------------
MissionBriefInfo missionData   
{
   title                =  *IDSTR_CC3_TITLE;
   planet               =  *IDSTR_PLANET_ICE;           
   campaign             =  *IDSTR_CC3_CAMPAIGN;           
   dateOnMissionEnd     =  *IDSTR_CC3_DATE;              
   shortDesc            =  *IDSTR_CC3_SHORTBRIEF;     
   longDescRichText     =  *IDSTR_CC3_LONGBRIEF;          
   media                =  *IDSTR_CC3_MEDIA;
   nextMission          =  *IDSTR_CC3_NEXTMISSION;
   successDescRichText  =  *IDSTR_CC3_DEBRIEF_SUCC;
   failDescRichText     =  *IDSTR_CC3_DEBRIEF_FAIL;
   location             =  *IDSTR_CC3_LOCATION;
   soundvol             =  "cc3.vol";
   successWavFile       =  "CC3_Debriefing.wav";
   endCinematicRec	   =  "cinCD.rec";
   endCinematicSmk      =  "cin_CD.smk";
};

MissionBriefObjective missionObjective1
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_CC3_OBJ1_SHORT;
   longTxt     = *IDSTR_CC3_OBJ1_LONG;
   bmpname     = *IDSTR_CC3_OBJ1_BMPNAME;
}; 
   
MissionBriefObjective missionObjective2
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_IGNORE;
   shortTxt    = *IDSTR_CC3_OBJ2_SHORT;
   longTxt     = *IDSTR_CC3_OBJ2_LONG;
   bmpname     = *IDSTR_CC3_OBJ2_BMPNAME;
}; 

MissionBriefObjective missionObjective3
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_IGNORE;
   shortTxt    = *IDSTR_CC3_OBJ3_SHORT;
   longTxt     = *IDSTR_CC3_OBJ3_LONG;
   bmpname     = *IDSTR_CC3_OBJ3_BMPNAME;
}; 

MissionBriefObjective missionObjective4
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_CC3_OBJ4_SHORT;
   longTxt     = *IDSTR_CC3_OBJ4_LONG;
   bmpname     = *IDSTR_CC3_OBJ4_BMPNAME;
}; 

MissionBriefObjective missionObjective5
{
   isPrimary   = FALSE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_CC3_OBJ5_SHORT;
   longTxt     = *IDSTR_CC3_OBJ5_LONG;
   bmpname     = *IDSTR_CC3_OBJ5_BMPNAME;
}; 


DropPoint drop1
{
   name = "";
   desc = "";
};

//-----------------------------------------------------------------------------
// Store $playerNum and his vehicle id ( $missionObject.playerId )
//-----------------------------------------------------------------------------
function player::onAdd(%this)
{
   // The main "object" used for associating mission data ( all would-be 
   // globals are stored as associated variables of $missionObject )
   $missionObject = getObjectId(MissionGroup);


   //-----------------------------------------------------------------------------
   // Global "defines"
   //-----------------------------------------------------------------------------
   $missionObject.dist_NavOne                = 200;
   $missionObject.dist_NavTwo                = 3000;
   $missionObject.dist_NavThree              = 200;
   $missionObject.dist_safeDistance          = 1000;
   $missionObject.dist_patrol1Activate       = 1300;  
   $missionObject.dist_patrol2Activate       = 2400;  
   $missionObject.dist_patrol3Activate      = 1400;  
   $missionObject.dist_patrol4Activate      = 1200;  
   $missionObject.dist_warnLeaving           = 9000;
   $missionObject.dist_failLeaving           = 9200;

   $missionObject.playerNum = %this;
}

function vehicle::onAdd(%this)
{
   %num = playerManager::vehicleIdToPlayerNum(%this);
      
   if(%num == $missionObject.playerNum)
   {
      $missionObject.playerId = playerManager::playerNumToVehicleId($missionObject.playerNum);
   }
}

//-----------------------------------------------------------------------------
// Main initialization function -- comment out to disable all mission logic
//-----------------------------------------------------------------------------
function onSPClientInit()
{
   init();
   iceSounds();
   windSounds();
   cdAudioCycle( SS4, Purge );
}

//-----------------------------------------------------------------------------
function init()
{
   // shortcuts to object/group names within the MissionGroup tree
   $missionObject.impPatrol1 = "MissionGroup/impPatrol1";
   $missionObject.impPatrol2 = "MissionGroup/impPatrol2";
   $missionObject.impPatrol3 = "MissionGroup/impPatrol3";
   $missionObject.impPatrol4 = "MissionGroup/impPatrol4";
   $missionObject.omniCrawler = "MissionGroup/convoy/nexus";
   $missionObject.NavOne     = "MissionGroup/NavPoints/NavOne";
   $missionObject.NavTwo     = "MissionGroup/NavPoints/NavTwo";
   $missionObject.NavThree   = "MissionGroup/NavPoints/NavThree";
   $missionObject.convoy     = "MissionGroup/convoy";
   $missionObject.transports = "MissionGroup/transports";
//   $missionObject.convoyLeader = "MissionGroup/convoy/truck3";
   $missionObject.convoyLeader = $missionObject.omniCrawler;

   newFormation( delta, 0,0,0, -40,-40,0, 40,-40,0, -80,-80,0 ); 
   newFormation( line,  0,0,0, 0,-40,0, 0,-80,0 ,0,-120,0  );
   newFormation( convoyFormation, 0,0,0, 60,-50,0, -30,-90,0 ,0,-120,20, -50,-150,0  );
 
   // set up all the actors
   initializeGroup( $missionObject.impPatrol1, $missionObject.impPatrol1 @ "/imp1", delta, high );  
   initializeGroup( $missionObject.impPatrol2, $missionObject.impPatrol2 @ "/imp1", delta, medium );  
   initializeGroup( $missionObject.impPatrol3, $missionObject.impPatrol3 @ "/imp1", delta, medium );  
   initializeGroup( $missionObject.impPatrol4, $missionObject.impPatrol4 @ "/imp1", delta, high );  
   initializeGroup( $missionObject.convoy, $missionObject.convoyLeader, convoyFormation, medium );  

   order( $missionObject.transports, Speed, Medium );

   checkBoundary( enter, $missionObject.convoyLeader, $missionObject.NavOne, $missionObject.dist_NavOne, onArrivedNavOne );
   checkBoundary( enter, $missionObject.playerId, $missionObject.NavTwo, $missionObject.dist_NavTwo, onArrivedNavTwo );
   checkBoundary( enter, $missionObject.omniCrawler, $missionObject.NavThree, $missionObject.dist_NavThree, onArrivedNavThree );
   
   // generic boundary checking
   checkBoundary( leave, $missionObject.playerId, $missionObject.NavTwo, $missionObject.dist_warnLeaving, warnLeaving );
   checkBoundary( leave, $missionObject.playerId, $missionObject.NavTwo, $missionObject.dist_failLeaving, leftArea );

   checkBoundary( enter, $missionObject.convoyLeader, "MissionGroup/impPatrol1/imp1", $missionObject.dist_patrol1Activate, onNearsPatrol1 );
//   checkBoundary( enter, $missionObject.convoyLeader, "MissionGroup/impPatrol2/imp1", $missionObject.dist_patrol2Activate, onNearsPatrol2 );
   checkBoundary( enter, $missionObject.convoyLeader, "MissionGroup/impPatrol3/imp1", $missionObject.dist_patrol3Activate, onNearsPatrol3 );
   checkBoundary( enter, $missionObject.convoyLeader, "MissionGroup/impPatrol4/imp1", $missionObject.dist_patrol4Activate, onNearsPatrol4 );
   checkBoundary( enter, $missionObject.playerId, "MissionGroup/impPatrol1/imp1", $missionObject.dist_patrol1Activate, onNearsPatrol1 );
   checkBoundary( enter, $missionObject.playerId, "MissionGroup/impPatrol2/imp1", $missionObject.dist_patrol2Activate, onNearsPatrol2 );
   checkBoundary( enter, $missionObject.playerId, "MissionGroup/impPatrol3/imp1", $missionObject.dist_patrol3Activate, onNearsPatrol3 );
   checkBoundary( enter, $missionObject.playerId, "MissionGroup/impPatrol4/imp1", $missionObject.dist_patrol4Activate, onNearsPatrol4 );
   
   selectNavMarker( $missionObject.NavOne );

   // help the omniCrawler to avoid "jittery" behaviour
   // localNavIgnoreEverything( $missionObject.omniCrawler, True );
   order( $missionObject.omniCrawler, zigZag, False );

   order( $missionObject.convoyLeader, guard, $missionObject.NavOne );
}

//-----------------------------------------------------------------------------
// Set up a group ( speed, formation[delta], leader, etc. )
//-----------------------------------------------------------------------------
function initializeGroup( %group, %leader, %formation, %speed )
{
   order( %leader, makeLeader, true );
   order( %group, formation, %formation );
   order( %group, Speed, %speed );
}

//-----------------------------------------------------------------------------
function vehicle::onDestroyed(%this, %who)
{
   if( getTeam( %this ) == *IDSTR_TEAM_RED )
   {
      if( isSafe( *IDSTR_TEAM_YELLOW, $missionObject.playerId, 10000 ) == True )
      {
         missionObjective5.status = *IDSTR_OBJ_COMPLETED;
         say( 0, 9999, *IDSTR_GEN_OC01, "gen_oc01.wav", 1 );
      }

      %num = randomInt( 1, 4 );
      if( %num == 1 )
      {
         vehicle::salvage( %this );
      }
   }

   if( %this == getObjectId( $missionObject.omniCrawler ) )
   {
      missionObjective4.status = *IDSTR_OBJ_FAILED;
      setOrbitCamera( $missionObject.omniCrawler, 100, 45 );
      schedule( "forceToDebrief();", 4 );
   }

   if( %this == getObjectId( $missionObject.convoyLeader ) )
   {
      // designate a new leader if the original one is destroyed
      $missionObject.convoyLeader = $missionObject.omniCrawler;
      order( $missionObject.convoyLeader, makeLeader, True );
   }
}

//-----------------------------------------------------------------------------
function vehicle::onMessage( %this, %message )
{
   // make sure our attackers keep picking new buildings if theirs is destroyed
   if( getTeam( %this == *IDSTR_TEAM_RED ) )
   {
      if( %message == "TargetDestroyed" )
      {
         %newTarget = Pick( PlayerSquad );
         %orderTxt = "order( " @ %this @ ", Attack, " @ %newTarget @ " );";
         schedule( %orderTxt, 1 );
      }
   }
}

//-----------------------------------------------------------------------------
function vehicle::onArrived( %who, %where )
{
   if( %who == getObjectId( $missionObject.convoyLeader ) && %where == getObjectId( "MissionGroup/nexusPath/marker1" ) )
   {
      order( $missionObject.convoyLeader, Guard, "MissionGroup/nexusPath/marker2" );
   }
   if( %who == getObjectId( $missionObject.convoyLeader ) && %where == getObjectId( "MissionGroup/nexusPath/marker2" ) )
   {
      order( $missionObject.convoyLeader, Guard, "MissionGroup/nexusPath/marker3" );
   }
   if( %who == getObjectId( $missionObject.convoyLeader ) && %where == getObjectId( "MissionGroup/nexusPath/marker3" ) )
   {
      order( $missionObject.convoyLeader, Guard, $missionObject.NavThree );
   }

   if( %who == getObjectId( $missionObject.convoyLeader ) && %where == getObjectId( $missionObject.NavThree ) )
   {
      order( $missionObject.transports, Guard, "MissionGroup/transports/marker1" );
      schedule( "orbitCamera( \"MissionGroup/transports/ds4\", 120, 45 );", 2 );
      missionObjective1.status = *IDSTR_OBJ_COMPLETED;
      schedule( "forceToDebrief();", 6 );  
   }
}

//-----------------------------------------------------------------------------
function onArrivedNavOne()
{
   if( $missionObject.arrivedNavOne != True )
   {
      $missionObject.arrivedNavOne = True;
      selectNavMarker( $missionObject.NavTwo );

      missionObjective1.status = *IDSTR_OBJ_COMPLETED;
      missionObjective2.status = *IDSTR_OBJ_ACTIVE;

      schedule( "Say( 0, 1234, *IDSTR_CC3_NEX01, \"CC3_NEX01.wav\" );", 2 );

      schedule( "order( $missionObject.convoyLeader, Guard, $missionObject.NavTwo );", 1 );
      schedule( "redirectPlayerToNav3();", 38 );
   }
}

//-----------------------------------------------------------------------------
function onArrivedNavTwo()
{
   if( $missionObject.arrivedNavTwo != True )
   {
      $missionObject.arrivedNavTwo = True;

      schedule( "Say( 0, 1234, *IDSTR_CC3_NEX03, \"CC3_NEX03.wav\" );", 2 );
      schedule( "checkNearNav2_1();", 10 );
   }
}

//-----------------------------------------------------------------------------
// The mission is successful if a) the omniCrawler reaches NAV 003 and b) the
// area around the omniCrawler is safe from enemies
//-----------------------------------------------------------------------------
function onArrivedNavThree()
{
   if( isSafe( *IDSTR_TEAM_YELLOW, $missionObject.omniCrawler, $missionObject.dist_safeDistance ) == True )
   {
      if( $missionObject.arrivedNavThree != True )
      {
         $missionObject.arrivedNavThree = True;
         deselectNavMaker( $missionObject.NavThree );
         
         order( $missionObject.omniCrawler, Shutdown, True );

         // You win!
         schedule( "Say( 0, 1234, *IDSTR_CC3_NEX04, \"CC3_NEX04.wav\" );", 2 );

         missionObjective1.status = *IDSTR_OBJ_COMPLETED;
         missionObjective2.status = *IDSTR_OBJ_COMPLETED;
         missionObjective3.status = *IDSTR_OBJ_COMPLETED;
         missionObjective4.status = *IDSTR_OBJ_COMPLETED;

         order( $missionObject.transports @ "/ds4", Guard, $missionObject.NavOne );
         order( $missionObject.transports @ "/ds1", Guard, $missionObject.NavTwo );
         order( $missionObject.transports @ "/ds2", Guard, $missionObject.NavThree );
         order( $missionObject.transports @ "/ds3", Guard, $missionObject.playerId );
         $ts = $missionObject.transports @ "/ds4";
         schedule( "setOrbitCamera( $ts, 100, 195 );", 2 );
         schedule( "forceToDebrief();", 6 );
      }
   }
   else
   {
      if( $missionObject.beenToldToClearArea != True )
      {
         $missionObject.beenToldToClearArea = True;
         schedule( "Say( 0, 1234, *IDSTR_CC3_NEX05, \"CC3_NEX05.wav\" );", 2 );
      }
   }
}

//-----------------------------------------------------------------------------
function warnLeaving()
{
   if( $missionObject.warnedLeaving != True )
   {
      $missionObject.warnedLeaving = True;
      
      schedule( "Say( 0, 1234, *IDSTR_CYB_NEX01, \"CYB_NEX01.wav\" );", 2 );
   }
}

//-----------------------------------------------------------------------------
function leftArea()
{
   if( $missionObject.leftArea != True )
   {
      $missionObject.leftArea = True;
      
      schedule( "Say( 0, 1234, *IDSTR_CYB_NEX02, \"CYB_NEX02.wav\" );", 2 );
   }
}

//-----------------------------------------------------------------------------
function onNearsPatrol1()
{
   if( $missionObject.nearsPatrol1 != True )
   {
      $missionObject.nearsPatrol1 = True;

      order( $missionObject.impPatrol1, Attack, Pick( $missionObject.convoy ) );
   }
}

function onNearsPatrol2()
{
   if( $missionObject.nearsPatrol2 != True )
   {
      $missionObject.nearsPatrol2 = True;

      order( $missionObject.impPatrol2, Attack, Pick( $missionObject.convoy ) );
   }
}

function onNearsPatrol3()
{
   if( $missionObject.nearsPatrol3 != True )
   {
      $missionObject.nearsPatrol3 = True;

      order( $missionObject.impPatrol3, Attack, Pick( $missionObject.convoy ) );
   }
}

function onNearsPatrol4()
{
   if( $missionObject.nearsPatrol4 != True )
   {
      $missionObject.nearsPatrol4 = True;

      order( $missionObject.impPatrol4, Attack, Pick( $missionObject.convoy ) );
   }
}

//-----------------------------------------------------------------------------
// Check to see if the player is getting too close to NAV 002
//-----------------------------------------------------------------------------
function checkNearNav2_1()
{
   if( getDistance( $missionObject.playerId, $missionObject.NavTwo ) < 3000 )
   {
      schedule( "Say( 0, 1234, *IDSTR_CC3_NEX03, \"CC3_NEX03.wav\" );", 2 );
      schedule( "checkNearNav2_2();", 10 );
   
   }
}

function checkNearNav2_2()
{
   if( getDistance( $missionObject.playerId, $missionObject.NavTwo ) < 3000 )
   {
      // player done fucked up--nuke 'em
      schedule( "damageObject( \"PlayerSquad/SquadMate1\", 50000 );", 1 );
      schedule( "damageObject( \"PlayerSquad/SquadMate2\", 50000 );", 2 );
      schedule( "damageObject( \"PlayerSquad/SquadMate3\", 50000 );", 3 );
      schedule( "damageObject( $missionObject.playerId, 50000 );", 5 );

      missionObjective4.status = *IDSTR_OBJ_FAILED;
   }
}

//-----------------------------------------------------------------------------
// En route to NAV 002, the player is told to go to NAV 003
//-----------------------------------------------------------------------------
function redirectPlayerToNav3()
{
      schedule( "Say( 0, 1234, *IDSTR_CC3_NEX02, \"CC3_NEX02.wav\" );", 2 );

      missionObjective2.status = *IDSTR_OBJ_IGNORE;
      missionObjective3.status = *IDSTR_OBJ_ACTIVE;

      enableNavMarker( $missionObject.NavThree );
      selectNavMarker( $missionObject.NavThree );

      order( $missionObject.convoyLeader, Guard, "MissionGroup/nexusPath/marker1" );
}




//-----------------------------------------------------------------------------
function win()
{
   missionObjective1.status = *IDSTR_OBJ_COMPLETED;
   missionObjective2.status = *IDSTR_OBJ_COMPLETED;
   missionObjective3.status = *IDSTR_OBJ_COMPLETED;
   missionObjective4.status = *IDSTR_OBJ_COMPLETED;
   missionObjective5.status = *IDSTR_OBJ_COMPLETED;
   updatePlanetInventory(CC3);
   schedule("forceToDebrief();", 1.0);
}
