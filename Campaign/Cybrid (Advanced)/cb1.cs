//-----------------------------------------------------------------------------
//
// Mission script for CB1 ( "Disarm//Neutralize" )
//
// Synopsis: Destroy 4 generators powering Human Anti-Orbit guns
//
// Mission Objectives:
// 
//  1) Destroy all 4 power generators near NAV 001
//  2) Destroy all enemies in the area
//
//-----------------------------------------------------------------------------


//-----------------------------------------------------------------------------
// Initialize mission data
//-----------------------------------------------------------------------------
MissionBriefInfo missionData
{
   title                =  *IDSTR_CB1_TITLE;
   planet               =  *IDSTR_PLANET_MOON;           
   campaign             =  *IDSTR_CB1_CAMPAIGN;           
   dateOnMissionEnd     =  *IDSTR_CB1_DATE;              
   shortDesc            =  *IDSTR_CB1_SHORTBRIEF;     
   longDescRichText     =  *IDSTR_CB1_LONGBRIEF;          
   media                =  *IDSTR_CB1_MEDIA;
   nextMission          =  *IDSTR_CB1_NEXTMISSION;
   successDescRichText  =  *IDSTR_CB1_DEBRIEF_SUCC;
   failDescRichText     =  *IDSTR_CB1_DEBRIEF_FAIL;
   location             =  *IDSTR_CB1_LOCATION;
   soundvol             =  "cb1.vol";
   successWavFile       =  "CB1_Debriefing.wav";
};

MissionBriefObjective missionObjective1
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_CB1_OBJ1_SHORT;
   longTxt     = *IDSTR_CB1_OBJ1_LONG;
   bmpname     = *IDSTR_CB1_OBJ1_BMPNAME;
}; 
   
MissionBriefObjective missionObjective2
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_CB1_OBJ2_SHORT;
   longTxt     = *IDSTR_CB1_OBJ2_LONG;
   bmpname     = *IDSTR_CB1_OBJ2_BMPNAME;
}; 

MissionBriefObjective missionObjective3
{
   isPrimary   = FALSE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_CB1_OBJ3_SHORT;
   longTxt     = *IDSTR_CB1_OBJ3_LONG;
   bmpname     = *IDSTR_CB1_OBJ3_BMPNAME;
}; 

//-----------------------------------------------------------------------------
// The player can choose to drop near, medium, or far.
// Depending on where the player drops, the enemies start at different 
// distances from the base. Players in fast vehicles can--in theory--sneak
// into the base, do some damage, and get out before all enemies show up. 
//-----------------------------------------------------------------------------
DropPoint drop1
{
   name = "MissionGroup/dropPointGroup/close";
   desc = "Base perimeter (close)";
};

DropPoint drop2
{
   name = "MissionGroup/dropPointGroup/medium";
   desc = "Outskirts of base (medium)";
};

DropPoint drop3
{
   name = "MissionGroup/dropPointGroup/far";
   desc = "Near listening depot (far)";
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
   $missionObject.dist_NavTwo                = 300;
   $missionObject.dist_safeDistance          = 1000;
   $missionObject.dist_warnLeaving           = 3000;
   $missionObject.dist_failLeaving           = 3200;
   $missionObject.dist_safeDistance          = 1500;
   $missionObject.dist_patrol1Activate       = 800;  
   $missionObject.dist_patrol2Activate       = 860;
   $missionObject.dist_patrol3Activate       = 860;
   
   $missionObject.timer                      = 380;
   $missionObject.generatorsToBeDestroyed    = 4;
      
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
   cdAudioCycle( Newtech, CloudBurst, Mechsoul );
}


//-----------------------------------------------------------------------------
function init()
{
   $missionObject.impPatrol1 = "MissionGroup/impPatrol1";
   $missionObject.impPatrol2 = "MissionGroup/impPatrol2";
   $missionObject.impPatrol3 = "MissionGroup/impPatrol3";
   $missionObject.impBase    = "MissionGroup/impBase";
   $missionObject.NavOne     = "MissionGroup/NavPoints/Nav001";
   $missionObject.NavTwo     = "MissionGroup/NavPoints/Nav002";

   newFormation( delta, 0,0,0, -40,-40,0, 40,-40,0, -80,-80,0 ); 
   newFormation( line,  0,0,0, 0,-40,0, 0,-80,0 ,0,-120,0  );
 
   initializeGroup( $missionObject.impPatrol1, $missionObject.impPatrol1 @ "/imp1", delta, low );  
   initializeGroup( $missionObject.impPatrol2, $missionObject.impPatrol2 @ "/imp1", delta, low );  
   initializeGroup( $missionObject.impPatrol3, $missionObject.impPatrol3 @ "/imp1", delta, low );  

   selectNavMarker( $missionObject.NavOne );

   // the player must destroy the generators before the time runs out
   setHudTimer( $missionObject.timer, -1, "Time Remaining", 1 );
   schedule( "timeRanOut();", 380 );

   // Nav Points
   checkBoundary( enter, $missionObject.playerId, $missionObject.NavOne, $missionObject.dist_NavOne, onArrivedNav001 );
   checkBoundary( enter, $missionObject.playerId, $missionObject.NavTwo, $missionObject.dist_NavTwo, onArrivedNav002 );
   
   // generic boundary checking
   checkBoundary( leave, $missionObject.playerId, $missionObject.NavOne, $missionObject.dist_warnLeaving, warnLeaving );
   checkBoundary( leave, $missionObject.playerId, $missionObject.NavOne, $missionObject.dist_failLeaving, leftArea );

   checkBoundary( enter, $missionObject.playerId, "MissionGroup/impPatrol1/imp1", $missionObject.dist_patrol1Activate, onNearsPatrol1 );
   checkBoundary( enter, $missionObject.playerId, "MissionGroup/impPatrol3/imp1", $missionObject.dist_patrol3Activate, onNearsPatrol3 );
   checkBoundary( enter, $missionObject.playerId, "MissionGroup/impPatrol2/imp1", $missionObject.dist_patrol2Activate, onNearsPatrol2 );
   checkBoundary( enter, $missionObject.playerId, "MissionGroup/impPatrol2/imp3", $missionObject.dist_patrol2Activate, onNearsPatrol2 );

   // other Cybrid dropPods in the distance ( ambience )
   schedule( "dropPod( \"MissionGroup/dropPods/marker1\" );", 5 );
   schedule( "dropPod( \"MissionGroup/dropPods/marker2\" );", 14 );
   schedule( "dropPod( \"MissionGroup/dropPods/marker3\" );", 22 );
   schedule( "dropPod( \"MissionGroup/dropPods/marker4\" );", 27 );
   schedule( "dropPod( \"MissionGroup/dropPods/marker5\" );", 36 );

   // other Cybrid squads reporting ( ambient chatter )
   schedule( "Say( 0, 1234, *IDSTR_CB1_NEX02, \"CB1_NEX02.wav\" );", 165 );
   schedule( "Say( 0, 1234, *IDSTR_CB1_NEX05, \"CB1_NEX05.wav\" );", 195 );
   schedule( "Say( 0, 1234, *IDSTR_CB1_NEX04, \"CB1_NEX04.wav\" );", 225 );
   schedule( "Say( 0, 1234, *IDSTR_CB1_NEX01, \"CB1_NEX01.wav\" );", 300 );
   schedule( "Say( 0, 1234, *IDSTR_CB1_NEX06, \"CB1_NEX06.wav\" );", 327 );
   schedule( "Say( 0, 1234, *IDSTR_CB1_NEX06, \"CB1_NEX03.wav\" );", 450 );
}

//-----------------------------------------------------------------------------
function vehicle::onMessage( %this, %message )
{
   if( %this == $missionObject.playerId && %message == "TargetDestroyed" )
   {
      // NEXUS: "Desecrator Quad Sector 7: Target destroyed//removed."
      schedule( "Say( 0, 1234, *IDSTR_CB1_NEX07, \"CB1_NEX07.wav\" );", 1 );
   }
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
function vehicle::onDestroyed(%this, %who)
{
   if( getTeam( %this ) == *IDSTR_TEAM_RED )
   {
      if( isSafe( *IDSTR_TEAM_YELLOW, $missionObject.playerId, $missionObject.dist_safeDistance ) == True )
      {
         missionObjective3.status = *IDSTR_OBJ_COMPLETED;
         say( 0, 9999, *IDSTR_GEN_OC01, "gen_oc01.wav", 1 );
 
         // if( $missionObject.generatorsDestroyed == $missionObject.generatorsToBeDestroyed )
      }

      %num = randomInt( 1, 4 );
      if( %num == 1 )
      {
         vehicle::salvage( %this );
      }
   }
}

//-----------------------------------------------------------------------------
function turret::onDestroyed(%this, %who)
{
   // player is attacking the base--send in all patrols
   if( $missionObject.generatorsDestroyed == 0 )
   {
      order( $missionObject.impPatrol1, speed, high );
      order( $missionObject.impPatrol2, speed, high );
      order( $missionObject.impPatrol3, speed, high );

      order( $missionObject.impPatrol1, attack, pick( PlayerSquad ) );
      order( $missionObject.impPatrol2, attack, pick( PlayerSquad ) );
      order( $missionObject.impPatrol3, attack, pick( PlayerSquad ) );
   }

   if( getTeam( %this ) == *IDSTR_TEAM_RED )
   {
      if( isSafe( *IDSTR_TEAM_YELLOW, $missionObject.playerId, $missionObject.dist_safeDistance ) == True )
      {
         missionObjective3.status = *IDSTR_OBJ_COMPLETED;
         say( 0, 9999, *IDSTR_GEN_OC01, "gen_oc01.wav", 1 );
      }

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
   // Make sure that EVERY enemy in the area bumrushes the player when the 
   // first generator is destroyed
   if( %this == getObjectId( "MissionGroup/impBase/power1" ) ||
       %this == getObjectId( "MissionGroup/impBase/power2" ) ||
       %this == getObjectId( "MissionGroup/impBase/power3" ) ||
       %this == getObjectId( "MissionGroup/impBase/power4" ) )
   {
      if( $missionObject.generatorsDestroyed == 0 )
      {
         order( $missionObject.impPatrol1, speed, high );
         order( $missionObject.impPatrol2, speed, high );
         order( $missionObject.impPatrol3, speed, high );

         schedule( "order( $missionObject.impPatrol1, attack, pick( PlayerSquad ) );", 12 );
         schedule( "order( $missionObject.impPatrol2, attack, pick( PlayerSquad ) );", 18 );
         schedule( "order( $missionObject.impPatrol3, attack, pick( PlayerSquad ) );", 24 );
      }
   }

   if( %this == getObjectId( "MissionGroup/impBase/power1" ) )
   {
      $missionObject.generatorsDestroyed++;
      damageObject( "MissionGroup/impBase/gun1", 80000 );

      order( "MissionGroup/turrets/t1", shutDown, True );

      if( $missionObject.generatorsDestroyed == $missionObject.generatorsToBeDestroyed )
      {
         missionObjective1.status = *IDSTR_OBJ_COMPLETED;
         say( 0, 9999, *IDSTR_GEN_OC01, "gen_oc01.wav", 1 );

         setHudTimer( -1, -1, "Time Remaining", -1 );

         enableNavMarker( $missionObject.NavTwo );
         selectNavMarker( $missionObject.NavTwo );
      }
   }
   if( %this == getObjectId( "MissionGroup/impBase/power2" ) )
   {
      $missionObject.generatorsDestroyed++;
      damageObject( "MissionGroup/impBase/gun2", 80000 );

      order( "MissionGroup/turrets/t2", shutDown, True );

      if( $missionObject.generatorsDestroyed == $missionObject.generatorsToBeDestroyed )
      {
         missionObjective1.status = *IDSTR_OBJ_COMPLETED;
         say( 0, 9999, *IDSTR_GEN_OC01, "gen_oc01.wav", 1 );

         setHudTimer( -1, -1, "Time Remaining", -1 );

         enableNavMarker( $missionObject.NavTwo );
         selectNavMarker( $missionObject.NavTwo );
      }
   }
   if( %this == getObjectId( "MissionGroup/impBase/power3" ) )
   {
      $missionObject.generatorsDestroyed++;
      damageObject( "MissionGroup/impBase/gun3", 80000 );

      order( "MissionGroup/turrets/t3", shutDown, True );

      if( $missionObject.generatorsDestroyed == $missionObject.generatorsToBeDestroyed )
      {
         missionObjective1.status = *IDSTR_OBJ_COMPLETED;
         say( 0, 9999, *IDSTR_GEN_OC01, "gen_oc01.wav", 1 );

         setHudTimer( -1, -1, "Time Remaining", -1 );
      
         enableNavMarker( $missionObject.NavTwo );
         selectNavMarker( $missionObject.NavTwo );
      }

   }
   if( %this == getObjectId( "MissionGroup/impBase/power4" ) )
   {
      $missionObject.generatorsDestroyed++;
      damageObject( "MissionGroup/impBase/gun4", 80000 );

      order( "MissionGroup/turrets/t4", shutDown, True );

      if( $missionObject.generatorsDestroyed == $missionObject.generatorsToBeDestroyed )
      {
         missionObjective1.status = *IDSTR_OBJ_COMPLETED;
         say( 0, 9999, *IDSTR_GEN_OC01, "gen_oc01.wav", 1 );

         setHudTimer( -1, -1, "Time Remaining", -1 );
      
         enableNavMarker( $missionObject.NavTwo );
         selectNavMarker( $missionObject.NavTwo );
      }

   }
}

//-----------------------------------------------------------------------------
function onArrivedNav001()
{
   if( $missionObject.arrivedNav001 != True )
   {
      $missionObject.arrivedNav001 = True;

      deselectNavMarker( $missionObject.NavOne );

      schedule( "Say( 0, getObjectId( $missionObject.impBase ), *IDSTR_CYB_GN63, \"CYB_GN63.wav\" );", 8 );
      schedule( "Say( 0, getObjectId( $missionObject.impBase ), *IDSTR_CYB_LU03, \"CYB_LU03.wav\" );", 29 );
      schedule( "Say( 0, getObjectId( $missionObject.impBase ), *IDSTR_CYB_LU13, \"CYB_LU13.wav\" );", 85 );
   }
}

//-----------------------------------------------------------------------------
function onArrivedNav002()
{
   if( missionObjective1.status == *IDSTR_OBJ_COMPLETED )
   {
      if( isSafe( *IDSTR_TEAM_YELLOW, $missionObject.playerId, $missionObject.dist_safeDistance ) )
      {
         if( $missionObject.arrivedNav002 != True )
         {
            $missionObject.arrivedNav002 = True;

            missionObjective2.status = *IDSTR_OBJ_COMPLETED;

            deselectNavMarker( $missionObject.NavTwo );
      
            schedule( "forceToDebrief();", 2 );
            updatePlanetInventory(CB1);
         }
      }
   }
}

//-----------------------------------------------------------------------------
function timeRanOut()
{
   if( missionObjective1.status != *IDSTR_OBJ_COMPLETED )
   {
      // don't leave the objective incomplete...
      missionObjective1.status = *IDSTR_OBJ_FAILED;
      forceToDebrief();
   }
}

//-----------------------------------------------------------------------------
function warnLeaving()
{
   if( $missionObject.warnedLeaving != True )
   {
      $missionObject.warnedLeaving = True;
      
      Say( 0, 1234, *IDSTR_CYB_NEX01, "CYB_NEX01.wav" );
   }
}

//-----------------------------------------------------------------------------
function leftArea()
{
   if( $missionObject.leftArea != True )
   {
      $missionObject.leftArea = True;
      
      Say( 0, 1234, *IDSTR_CYB_NEX02, "CYB_NEX02.wav" );
      schedule( "forceToDebrief();", 2 );
   }
}

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
function onNearsPatrol3()
{
   if( $missionObject.nearsPatrol3 != True )
   {
      $missionObject.nearsPatrol3 = True;

      order( $missionObject.impPatrol3, Attack, Pick( PlayerSquad ) );
   }
}

//-----------------------------------------------------------------------------
function win()
{
   missionObjective1.status = *IDSTR_OBJ_COMPLETED;
   missionObjective2.status = *IDSTR_OBJ_COMPLETED;
   missionObjective3.status = *IDSTR_OBJ_COMPLETED;
   updatePlanetInventory(cb1);
   schedule("forceToDebrief();", 3.0);
}
