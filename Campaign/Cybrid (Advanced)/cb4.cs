//-----------------------------------------------------------------------------
//
// Mission script for CB4 ( "Ground//Disrupt" )
//
// Synopsis: Lay waste to a Human SpacePort
//
// Mission Objectives:
// 
//  1) Destroy the Human SpacePort near NAV 001
//  2) Destroy all enemies in the area
//
//-----------------------------------------------------------------------------


//-----------------------------------------------------------------------------
// Initialize mission data
//-----------------------------------------------------------------------------
MissionBriefInfo missionData
{
   title                =  *IDSTR_CB4_TITLE;
   planet               =  *IDSTR_PLANET_MOON;           
   campaign             =  *IDSTR_CB4_CAMPAIGN;           
   dateOnMissionEnd     =  *IDSTR_CB4_DATE;              
   shortDesc            =  *IDSTR_CB4_SHORTBRIEF;     
   longDescRichText     =  *IDSTR_CB4_LONGBRIEF;          
   media                =  *IDSTR_CB4_MEDIA;
   nextMission          =  *IDSTR_CB4_NEXTMISSION;
   successDescRichText  =  *IDSTR_CB4_DEBRIEF_SUCC;
   failDescRichText     =  *IDSTR_CB4_DEBRIEF_FAIL;
   location             =  *IDSTR_CB4_LOCATION;
   soundvol             =  "cb4.vol";
   successWavFile       =  "CB4_Debriefing.wav";
   endCinematicRec	   =  "cinCC.rec";
   endCinematicSmk      =  "cin_CC.smk";
};

MissionBriefObjective missionObjective1
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_CB4_OBJ1_SHORT;
   longTxt     = *IDSTR_CB4_OBJ1_LONG;
   bmpname     = *IDSTR_CB4_OBJ1_BMPNAME;
}; 
   
MissionBriefObjective missionObjective2
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_CB4_OBJ2_SHORT;
   longTxt     = *IDSTR_CB4_OBJ2_LONG;
   bmpname     = *IDSTR_CB4_OBJ2_BMPNAME;
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
   $missionObject.dist_NavOne                = 300;
   $missionObject.dist_safeDistance          = 1000;
   $missionObject.dist_warnLeaving           = 3000;
   $missionObject.dist_failLeaving           = 3200;
   $missionObject.dist_safeDistance          = 1100;
   $missionObject.dist_patrol1Activate       = 1300;  
   $missionObject.dist_patrol2Activate       = 1300;
   $missionObject.dist_convoyActivate        = 700;
   $missionObject.dist_escortsActivate       = 500;
   
   $missionObject.destroyedBuildingCount     = 12;

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
   moonSounds();
}

function onMissionStart()
{
   cdAudioCycle( Mechsoul, Terror );
}

//-----------------------------------------------------------------------------
function init()
{
   $missionObject.impPatrol1 = "MissionGroup/impPatrol1";
   $missionObject.impPatrol2 = "MissionGroup/impPatrol2";
   $missionObject.impBase    = "MissionGroup/impBase";
   $missionObject.impConvoy  = "MissionGroup/impConvoy";
   $missionObject.impEscorts = "MissionGroup/impEscorts";
   $missionObject.NavOne     = "MissionGroup/NavPoints/Nav001";

   newFormation( delta, 0,0,0, -40,-40,0, 40,-40,0, -80,-80,0 ); 
   newFormation( line,  0,0,0, 0,-40,0, 0,-80,0 ,0,-120,0  );
 
   initializeGroup( $missionObject.impPatrol1, $missionObject.impPatrol1 @ "/imp1", delta, high );  
   initializeGroup( $missionObject.impPatrol2, $missionObject.impPatrol2 @ "/imp1", delta, medium );  
   initializeGroup( $missionObject.impEscorts, $missionObject.impEscorts @ "/imp1", delta, high );  
   initializeGroup( $missionObject.impConvoy, $missionObject.impConvoy @ "/truck1", delta, medium );  

   // Nav Points
   checkBoundary( enter, $missionObject.playerId, "MissionGroup/NavPoints/Nav001", $missionObject.dist_NavOne, onArrivedNav001 );
   
   // generic boundary checking
   checkBoundary( leave, $missionObject.playerId, "MissionGroup/NavPoints/Nav001", $missionObject.dist_warnLeaving, onWarnLeaving );
   checkBoundary( leave, $missionObject.playerId, "MissionGroup/NavPoints/Nav001", $missionObject.dist_failLeaving, onLeftArea );

   checkBoundary( enter, $missionObject.playerId, "MissionGroup/impPatrol1/imp1", $missionObject.dist_patrol1Activate, onNearsPatrol1 );
   checkBoundary( enter, $missionObject.playerId, "MissionGroup/impPatrol2/imp1", $missionObject.dist_patrol2Activate, onNearsPatrol2 );
   checkBoundary( enter, $missionObject.playerId, "MissionGroup/impEscorts/imp1", $missionObject.dist_escortsActivate, onNearsEscorts );
   checkBoundary( enter, $missionObject.playerId, "MissionGroup/impConvoy/truck1", $missionObject.dist_convoyActivate, onNearsConvoy );

   // self-scheduling function to check the area around the SpacePort for enemies
   isTheAreaSafe();

   // fliers and worker trucks scrambling about
   forceScope( "MissionGroup/fliers/f1", 9999 );
   forceScope( "MissionGroup/fliers/f2", 9999 );
   forceScope( "MissionGroup/fliers/f3", 9999 );
   forceScope( "MissionGroup/fliers/f4", 9999 );

   order( "MissionGroup/fliers/f1", speed, Medium );
   order( "MissionGroup/fliers/f1", speed, Low );
   order( "MissionGroup/fliers/f1", speed, High );
   order( "MissionGroup/fliers/f1", speed, High );
   
   order( "MissionGroup/fliers/f1", flyThrough, True );
   order( "MissionGroup/fliers/f2", flyThrough, True );
   order( "MissionGroup/fliers/f3", flyThrough, True );
   order( "MissionGroup/fliers/f4", flyThrough, True );
   
   order( "MissionGroup/fliers/f1", guard, "MissionGroup/flyerPath1" );
   order( "MissionGroup/fliers/f2", guard, "MissionGroup/flyerPath1" );
   order( "MissionGroup/fliers/f3", guard, "MissionGroup/flyerPath2" );
   order( "MissionGroup/fliers/f4", guard, "MissionGroup/flyerPath2" );

   order( "MissionGroup/workers/t1", guard, "MissionGroup/truckPath1" );
   order( "MissionGroup/workers/t2", guard, "MissionGroup/truckPath1" );
   order( "MissionGroup/workers/t3", guard, "MissionGroup/truckPath2" );
   order( "MissionGroup/workers/t4", guard, "MissionGroup/truckPath2" );

   selectNavMarker( $missionObject.NavOne );
}

//-----------------------------------------------------------------------------
// Set up a group ( speed, formation[delta], leader, etc. )
//-----------------------------------------------------------------------------
function initializeGroup( %group, %leader, %formation, %speed )
{
   order( %leader, makeLeader, True );
   order( %group, formation, %formation );
   order( %group, Speed, %speed );
}

//-----------------------------------------------------------------------------
// Assign random enemy vehicle salvage
//-----------------------------------------------------------------------------
function vehicle::onDestroyed(%this, %who)
{
   if( getTeam( %this ) == *IDSTR_TEAM_RED )
   {
      %num = randomInt( 1, 4 );
      if( %num == 1 )
      {
         vehicle::salvage( %this );
      }
   }
}

//-----------------------------------------------------------------------------
function structure::onDestroyed( %this, %who )
{
   if( getTeam( %this ) == *IDSTR_TEAM_RED )
   {
      $missionObject.structuresDestroyed++;
      
      if( $missionObject.structuresDestroyed >= $missionObject.destroyedBuildingCount )
      {
         missionObjective1.status = *IDSTR_OBJ_COMPLETED;
      
         if( isSafe( *IDSTR_TEAM_YELLOW, $missionObject.playerId, $missionObject.dist_safeDistance ) == True )
         {
            missionObjective2.status = *IDSTR_OBJ_COMPLETED;
            say( 0, 9999, *IDSTR_GEN_OC01, "gen_oc01.wav", 1 );

            schedule( "forceToDebrief( *IDSTR_MISSION_SUCCESSFUL );", 3 );
            updatePlanetInventory( CB4 );
         }
      }
   }

}

//-----------------------------------------------------------------------------
// The convoy trucks will either reach the base, or--if they panic--reach
// the "escape marker" which is out in the boonies. In both cases, they will
// shut down.
//-----------------------------------------------------------------------------
function vehicle::onArrived( %who, %where )
{
   if( %who == getObjectId( "MissionGroup/impConvoy/truck1" ) && 
       %where == getObjectId( "MissionGroup/impConvoyPath/marker2" ) )
   {
      order( $missionObject.impConvoy, shutDown, True );
   }

   if( %who == getObjectId( "MissionGroup/impConvoy/truck1" ) && 
       %where == getObjectId( "MissionGroup/impConvoyPath/escapeMarker" ) )
   {
      order( $missionObject.impConvoy, shutDown, True );
   }
}

//-----------------------------------------------------------------------------
function onArrivedNav001()
{
   if( $missionObject.arrivedNav001 != True )
   {
      $missionObject.arrivedNav001 = True;
      deselectNavMarker( $missionObject.NavTwo );

      // Generic Human squabling...
      schedule( "Say( 0, 1234, \"CYB_LU01.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU02.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU03.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU04.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU05.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU06.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU07.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU08.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU09.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU10.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU11.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU12.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU13.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU14.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU15.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU16.wav\" );", randomInt(5, 150) );
      schedule( "Say( 0, 1234, \"CYB_LU17.wav\" );", randomInt(5, 150) );

      order( $missionObject.impPatrol1, Attack, Pick( PlayerSquad ) );
      order( $missionObject.impPatrol2, Attack, Pick( PlayerSquad ) );
      order( $missionObject.escorts, Attack, Pick( PlayerSquad ) );

      schedule( "order( \"MissionGroup/fliers/f1\", guard, \"MissionGroup/endMarker\" );", randomInt( 15, 70 ) );
      schedule( "order( \"MissionGroup/fliers/f2\", guard, \"MissionGroup/endMarker\" );", randomInt( 15, 70 ) );
      schedule( "order( \"MissionGroup/fliers/f3\", guard, \"MissionGroup/endMarker\" );", randomInt( 15, 70 ) );
      schedule( "order( \"MissionGroup/fliers/f4\", guard, \"MissionGroup/endMarker\" );", randomInt( 15, 70 ) );
      
      schedule( "order( \"MissionGroup/workers/t1\", guard, \"MissionGroup/endMarker\" );", randomInt( 15, 70 ) );
      schedule( "order( \"MissionGroup/workers/t2\", guard, \"MissionGroup/endMarker\" );", randomInt( 15, 70 ) );
      schedule( "order( \"MissionGroup/workers/t3\", guard, \"MissionGroup/endMarker\" );", randomInt( 15, 70 ) );
      schedule( "order( \"MissionGroup/workers/t4\", guard, \"MissionGroup/endMarker\" );", randomInt( 15, 70 ) );

      alarmSoundsOn( 8351, "alarm2.wav" );
   }
}

//-----------------------------------------------------------------------------
// Player is warned once when leaving mission boundaries, then fails mission.
//-----------------------------------------------------------------------------
function onWarnLeaving()
{
   if( $missionObject.warnedLeaving != True )
   {
      $missionObject.warnedLeaving = True;
      
      Say( 0, 1234, *IDSTR_CYB_NEX01, "CYB_NEX01.wav" );
   }
}

//-----------------------------------------------------------------------------
function onLeftArea()
{
   if( $missionObject.playerLeftArea != True )
   {
      $missionObject.playerLeftArea = True;

      Say( 0, 1234, *IDSTR_CYB_NEX02, "CYB_NEX02.wav" );
      schedule( "forceToDebrief( *IDSTR_MISSION_FAILED );", 2 );
   }
}

//-----------------------------------------------------------------------------
// Make sure the AI wakes up and attacks the player and his squad
//-----------------------------------------------------------------------------
function onNearsPatrol1()
{
   if( $missionObject.nearsPatrol1 != True )
   {
      $missionObject.nearsPatrol1 = True;

      order( $missionObject.impPatrol1, Attack, Pick( PlayerSquad ) );
   }
}

//-----------------------------------------------------------------------------
function onNearsPatrol2()
{
   if( $missionObject.nearsPatrol2 != True )
   {
      $missionObject.nearsPatrol2 = True;

      order( $missionObject.impPatrol2, Attack, Pick( PlayerSquad ) );
   }
}

//-----------------------------------------------------------------------------
// The convoy's escorts will protect it
//-----------------------------------------------------------------------------
function onNearsEscorts()
{
   if( $missionObject.nearsEscorts != True )
   {
      $missionObject.nearsEscorts = True;

      order( $missionObject.impEscorts, Attack, Pick( PlayerSquad ) );
   }
}

//-----------------------------------------------------------------------------
// If the player gets too close to the convoy, all of its trucks will power
// on, and head for the escapeMarker ( out in the boonies )--they will 
// eventually stop and shutdown.
//-----------------------------------------------------------------------------
function onNearsConvoy()
{
   if( $missionObject.nearsConvoy != True )
   {
      $missionObject.nearsConvoy = True;

      order( $missionObject.impConvoy, ShutDown, False );
      order( $missionObject.impConvoy, MakeLeader, "MissionGroup/impConvoy/truck1" );
      order( $missionObject.impConvoy, Guard, "MissionGroup/impConvoyPath/escapeMarker" );
   }
}

//-----------------------------------------------------------------------------
// Self-scheduling function checks the area around NAV 001 for enemies. As 
// soon as the area is safe, an objective is satisfied.
//-----------------------------------------------------------------------------
function isTheAreaSafe()
{
   
   if( isSafe( *IDSTR_TEAM_YELLOW, "MissionGroup/NavPoints/Nav001", $missionObject.dist_safeDistance ) == True )
   {
      missionObjective2.status = *IDSTR_OBJ_COMPLETED;
      say( 0, 9999, *IDSTR_GEN_OC01, "gen_oc01.wav", 1 );

      if( $missionObject.structuresDestroyed >= $missionObject.destroyedBuildingCount )
      {
         schedule( "forceToDebrief( *IDSTR_MISSION_SUCCESSFUL );", 3 );
         updatePlanetInventory( CB4 );
      }
   }
   else
   {
      // keep calling itself
      schedule( "isTheAreaSafe();", 3.0 );
   }
}

//-----------------------------------------------------------------------------
function win()
{
   missionObjective1.status = *IDSTR_OBJ_COMPLETED;
   missionObjective2.status = *IDSTR_OBJ_COMPLETED;
   updatePlanetInventory(cb4);
   schedule("forceToDebrief();", 3.0);
}
