// Locate amd disable Imperial transport ship

// for our passing patrol
Pilot PatrolPilot
{
   id = 28;
   
   skill = 0.5;
   accuracy = 0.4;
   aggressiveness = 0.2;
   activateDist = 450.0;
   deactivateBuff = 300.0;
   targetFreq = 4.0;
   trackFreq = 0.2;
   fireFreq = 0.7;
   LOSFreq = 0.4;
   orderFreq = 4.0;
};
   
Pilot Harabec
{
   id = 29;
   
   name = "Harabec";
   skill = 0.4;
   accuracy = 0.7;
   aggressiveness = 0.5;
   activateDist = 400.0;
   deactivateBuff = 800.0;
   targetFreq = 2.8;
   trackFreq = 2.0;
   fireFreq = 0.3;
   LOSFreq = 0.4;
   orderFreq = 4.0;
};

MissionBriefInfo missionData
{
   title                =  *IDSTR_HA2_TITLE;
   planet               =  *IDSTR_PLANET_MARS;           
   campaign             =  *IDSTR_HA2_CAMPAIGN;           
   dateOnMissionEnd     =  *IDSTR_HA2_DATE;              
   shortDesc            =  *IDSTR_HA2_SHORTBRIEF;     
   longDescRichText     =  *IDSTR_HA2_LONGBRIEF;          
   media                =  *IDSTR_HA2_MEDIA;
   nextMission          =  *IDSTR_HA2_NEXTMISSION;
   successDescRichText  =  *IDSTR_HA2_DEBRIEF_SUCC;
   failDescRichText     =  *IDSTR_HA2_DEBRIEF_FAIL;
   location             =  *IDSTR_HA2_LOCATION;
   successWavFile       =  "HA2_Debriefing.wav";
   soundvol             =  "ha2.vol";
};

MissionBriefObjective missionObjective1
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HA2_OBJ1_SHORT;
   longTxt     = *IDSTR_HA2_OBJ1_LONG;
   bmpname     = *IDSTR_HA2_OBJ1_BMPNAME;
}; 
   
MissionBriefObjective missionObjective2
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HA2_OBJ2_SHORT;
   longTxt     = *IDSTR_HA2_OBJ2_LONG;
   bmpname     = *IDSTR_HA2_OBJ2_BMPNAME;
}; 

MissionBriefObjective missionObjective3
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HA2_OBJ3_SHORT;
   longTxt     = *IDSTR_HA2_OBJ3_LONG;
   bmpname     = *IDSTR_HA2_OBJ3_BMPNAME;
}; 

MissionBriefObjective missionObjective4
{
   isPrimary   = FALSE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HA2_OBJ4_SHORT;
   longTxt     = *IDSTR_HA2_OBJ4_LONG;
   bmpname     = *IDSTR_HA2_OBJ4_BMPNAME;
}; 

DropPoint drop1
{
   name = "Generic";
   desc = "Generic";
};

//-----------------------------------------------------------------------------
function onMissionStart()
{
   newFormation( Delta, 0,0,0, -60,-60,0, 60,-45,0, 0,-120,0 ); 
   newFormation( Line,  0,0,0, 0,-40,0, 0,-80,0, 0,-120,0 );

   clearGeneralOrders();
   cdAudioCycle( Purge, Terror, gnash);
}

//-----------------------------------------------------------------------------
function player::onAdd(%this)
{
   $playerNum = %this;
}

function vehicle::onAdd(%this)                                                                                                                        
{                                                                                                                                                     
   %num = playerManager::vehicleIdToPlayerNum(%this);                                                                                                 
                                                                                                                                                      
   if(%num == $playerNum)                                                                                                                             
   {                                                                                                                                                  
      $playerId = playerManager::playerNumToVehicleId($playerNum);                                                                                    
   }                                                                                                                                                  
}                                                                                                                                                     

function onSPClientInit()
{
   init();
   marsSounds();
   windSounds();
}

//-----------------------------------------------------------------------------
function init()
{
   $impPatrol1 = "MissionGroup/impPatrol1";
   $impPatrol2 = "MissionGroup/impPatrol2";
   $impPatrol3 = "MissionGroup/impPatrol3";
   $harabec    = "MissionGroup/harGroup/harabec";
   $cargoShip  = "MissionGroup/impBase/cargoShip";
   $harPath1   = "MissionGroup/harPath1";
   $harPath2   = "MissionGroup/harPath2";
   $pickupTeam = "MissionGroup/pickupTeam";
   $dropship   = "MissionGroup/dropShipGroup";
   $truck1     = "MissionGroup/workers/truck1";
   $truck2     = "MissionGroup/workers/truck2";
   $truck3     = "MissionGroup/workers/truck3";
   $truck4     = "MissionGroup/workers/truck4";
   $truck5     = "MissionGroup/workers/truck5";
   $truck6     = "MissionGroup/workers/truck6";
   $truck7     = "MissionGroup/workers/truck7";
   $truckPath1 = "MissionGroup/truckPath1/marker1";
   $truckPath2 = "MissionGroup/truckPath2/marker1";

   $harabecId  = getObjectId( $harabec );
   $harabecId.arrivedBase = False;
   $harabecId.pissed = False;
   $harabecId.attackedByPlayer = 0;

   $playerId.failedMission = false;

   order( "MissionGroup/impPatrol1/imp1", MakeLeader, True );
   order( $impPatrol1, Speed, High );
   order( $impPatrol1, Formation, Line );

   // sometimes the Basilisk patrol comes from the west, sometimes from the east
   if( randomInt( 1, 2 ) == 1 )
   {
      setPosition( "MissionGroup/impPatrol1/imp1", -412.0, 300.0, 469.0 );
      setPosition( "MissionGroup/impPatrol1/imp2", -412.0, 290.0, 469.0 );

      setPosition( "MissionGroup/impPatrol3/imp1", 2544.0, 3641.0, 605.0 );
      setPosition( "MissionGroup/impPatrol3/imp2", 2534.0, 3641.0, 605.0 );

      $impPatrol1.useAlternateRoute = True;
   }
   

   
   order( $impPatrol2, Formation, Delta );
   order( $impPatrol2, Speed, Low );
   order( "MissionGroup/impPatrol2/imp1", MakeLeader, True );
   order( $impPatrol2, Guard, "MissionGroup/impPath2" );
   order( $impPatrol2, holdPosition, True );

   order( $impPatrol3, Formation, Delta );
   order( $impPatrol3, Speed, Medium );
   order( "MissionGroup/impPatrol3/imp1", MakeLeader, True );
   
   // randomize stuff
   if( $impPatrol1.useAlternateRoute != True )
   {
      order( $impPatrol3, Guard, "MissionGroup/impPath3" );
   }
   
   order( $impPatrol3, holdPosition, True );
   
   order( $pickupTeam, Formation, Delta );
   order( $pickupTeam, Speed, Medium );
   order( "MissionGroup/pickupTeam/reb1", MakeLeader, True );

   order( $dropship, Speed, High );
   order( $dropship, Height, 15, 55 );
   order( $dropship, FlyThrough, True );
   order( $dropship, Guard, "MissionGroup/dropShipPath" );

   order( $harabec, Formation, Delta );
   order( $harabec, Speed, High );
   order( $harabec, MakeLeader, True );

   setNavMarker( "MissionGroup/NavPoints/NavAlpha", True, $playerId );                                                                                 
                                                                                                                                                 
   // when the player nears the action
   checkBoundary( enter, $playerId, "MissionGroup/NavPoints/NavAlpha", 250, onArrivedNavAlpha );                                                                                                                                                     
   checkBoundary( enter, $playerId, "MissionGroup/NavPoints/NavAlpha", 1150, onArrivedBase );                                                                
   echo( "PLAYERID: ", $playerId );
   echo( "NAV ALPHA: ", getObjectId( "MissionGroup/NavPoints/NavAlpha" ));
   
   // global bounds checking
   checkBoundary( leave, $playerId, "MissionGroup/NavPoints/NavAlpha", 3800, leavingArea );
   checkBoundary( leave, $playerId, "MissionGroup/NavPoints/NavAlpha", 4200, leftArea );
   
   // keep these objects in scope for camera stuff
   forceScope( getObjectId( "MissionGroup/impBase/cargoShip" ), 9999 );
   forceScope( getObjectId( "MissionGroup/pickupTeam/reb1" ), 9999 );
   forceScope( getObjectId( "MissionGroup/pickupTeam/reb2" ), 9999 );
   forceScope( getObjectId( "MissionGroup/pickupTeam/reb3" ), 9999 );
   forceScope( getObjectId( "MissionGroup/pickupTeam/reb4" ), 9999 );
   
   // don't let that cargo ship take off!
   order( $cargoShip, shutdown, True );
   schedule( "order( $cargoShip, Guard, \"MissionGroup/cargoShipPath\" );", 255 );
   schedule( "flierGotAway();", 280 );
   schedule( "checkForCargoShipSlide();", 280.5 );
   
   // Harabec talks and moves
   schedule( "order( $harabec, Guard, \"MissionGroup/harPath1\" );", 1 );
   schedule( "Say( 0, $harabecId, *IDSTR_HA2_HAR01, \"HA2_HAR01.wav\" );", 3 );

   forceScope( "MissionGroup/impPatrol1/imp1", 9999 );
   forceScope( "MissionGroup/impPatrol1/imp2", 9999 );

   setHercOwner( getObjectId( $harabec ), getObjectId( $cargoShip ) );

   $buildingsDestroyed = 0;
}





//-----------------------------------------------------------------------------
function vehicle::onArrived( %who, %where )
{
   // start the imp patrol passing
   if( %who == $harabecId && %where == getObjectId("MissionGroup/harPath1/marker3") )
   {
      if( $impPatrol1.useAlternateRoute != True )
      {
         order( $impPatrol1 @ "/imp1", Guard, "MissionGroup/impPath1" );
      }
      else
      {
         order( $impPatrol1 @ "/imp1", Guard, "MissionGroup/impPath1_alt" );
      }  
      
   }

   // Harabec tells player to wait out the passing patrol ( slows down and cloaks )
   if( %who == $harabecId && %where == getObjectId("MissionGroup/harPath1/marker4") )
   {
      order( $harabec, clear, True );
      Say( 0, $harabecId, *IDSTR_HA2_HAR02, "HA2_HAR02.wav" );

      schedule( "order( $harabec, cloak, True );", 4 );
   }

   // has the imp patrol has passed without altercation?
   if( %who == getObjectId( "MissionGroup/impPatrol1/imp1" ) &&        
       (%where == getObjectId( "MissionGroup/impPath1/marker1" ) || %where == getObjectId( "MissionGroup/impPath1_alt/marker1" ) ))
   {
      order( $harabec, speed, high );
      order( $harabec, Guard, $harPath2 );
      order( $harabec, cloak, false );

      if( $playerId.beenAttacked != True )
      {
         Say( 0, $harabecId, *IDSTR_HA2_HAR04, "HA2_HAR04.wav" );
      }
   }

   if( %who == getObjectId( $dropship ) &&        
       %where == getObjectId( "MissionGroup/dropShipPath/marker2" ) )
   {
      order( $dropship, Speed, High );
   }

   if( %who == getObjectId( $dropship ) &&        
       %where == getObjectId( "MissionGroup/dropShipPath/marker3" ) )
   {
      order( $dropship, Shutdown, True );
   }

}

//-----------------------------------------------------------------------------
function vehicle::onAttacked( %this, %attacker )
{
   // someone gets attacked for the first time
   if( %this == $harabecId || %this == $playerId )
   {
      if( (getTeam( %attacker ) == *IDSTR_TEAM_RED) && ($playerId.beenAttacked != True) && ($harabecId.arrivedBase != True) ) 
      {
         $playerId.beenAttacked = True;
      
         Say( 0, $harabecId, *IDSTR_HA2_HAR03, "HA2_HAR03.wav" );
      }
   }

   // Make sure player isn't being attacked by a rock...
   if( %this == $playerId && %attacker != $harabecId && %attacker != $playerId && %attacker != getObjectId( $cargoShip ) )
   {
      $playerId.beenAttacked = True;
         
      order( $harabec, Attack, %attacker );
   }
   
   if( %this == $harabecId && %attacker == $playerId )
   {
      // $harabec.attackedByPlayer can only be incremented once per 2 seconds
      
      if( $harabec.pissed != True )
      {
         if( $harabec.dontWarnPlayerFlag != True )
         {
            $harabec.dontWarnPlayerFlag = True;
            schedule( "$harabec.dontWarnPlayerFlag = False;", 2 );
         
            $harabec.attackedByPlayer++;
            
            if( $harabec.attackedByPlayer <= 3 )
            {
               // Harabec warns player to "Watch your fire!"
               schedule( "Say( 0, $harabecId, \"GEN_HAR01.wav\", \"GEN_HAR02.wav\", \"GEN_HAR03.wav\", \"GEN_HAR04.wav\" );", 1 );
            }
            else if( $harabecId.pissed != True )
            {
               $playerId.beenAttacked = True;
               $harabecId.pissed = True;

               order( $harabec, Attack, $playerId );
               
               schedule( "Say( 0, $harabecId, \"GEN_HAR05.wav\", \"GEN_HAR06.wav\", \"GEN_HAR07.wav\", \"GEN_HAR08.wav\" );", 0.8);
               
               // the mission is over... 
               schedule( "forceToDebrief( *IDSTR_MISSION_FAILED );", 4 );
            }
         }
      }
   }
}

//-----------------------------------------------------------------------------
function structure::onDestroyed(%this, %who)
{
   $buildingsDestroyed++;
   
   if( $buildingsDestroyed == 5 )
   {
      alarmSoundsOff( getObjectId( "MissionGroup/impBase/hq" ) );
   }
      
}
//-----------------------------------------------------------------------------
function vehicle::onDestroyed(%this, %who)
{
   // Harabec died
   if( %this == getObjectId( $harabec ) )
   {
      missionObjective3.status = *IDSTR_OBJ_FAILED;
      $playerId.failedMission = True;
      
      $harabec.killer = %who;

      fadeEvent( 0, out, 1.5, 0, 0, 0 );
      schedule( "fadeEvent( 0, in, 1.5, 0, 0, 0 );", 1.5 );
      schedule( "setDominantCamera( $harabec, $harabec.killer );", 1.5 ); 

      schedule( "forceToDebrief( *IDSTR_MISSION_FAILED );", 5 );
   
      killChannel( $harabecId );
   }

   // player dESTROYED cargo ship
   if( %this == getObjectId( "MissionGroup/impBase/cargoShip" ) )
   {
      missionObjective1.status = *IDSTR_OBJ_FAILED;
      $playerId.failedMission = True;
      schedule( "forceToDebrief( *IDSTR_MISSION_FAILED );", 3 );
   }

   // plain old enemy was killed
   if( %this != $playerId && %this != getObjectId( "MissionGroup/impBase/cargoShip" ) && this != $harabecId )
   {
      if( $cargoShip.disabled == True && isSafe( *IDSTR_TEAM_YELLOW, $playerId, 9999 ) )
      {
         schedule( "Say( 0, $harabecId, *IDSTR_HA2_HAR06, \"HA2_HAR06.wav\" );", 2 );
         order( $harabec, clear, True );
         addGeneralOrder( *IDSTR_ORDER_HA2_1, "radioRecoveryTeam();" );

         schedule( "repeatGeneralOrder(PlayerManager::playerNumToVehicleId(2049), *IDSTR_ORDER_HA2_1);", 4 );

         missionObjective4.status = *IDSTR_OBJ_COMPLETED;
         schedule( "Say( 0, 1234, \"GEN_OC01.wav\" );", 1 );
      }
   
      // give salvage appx. 25% of the time...
      %num = randomInt( 1, 4 );
      if( %num == 1 )
      {
         vehicle::salvage( %this );
      }
   }

   
   // The first patrol was encountered and destroyed... on with business
   if( %this == getObjectId( "MissionGroup/impPatrol1/imp1" ) ||
       %this == getObjectId( "MissionGroup/impPatrol1/imp2" ) )
   {
      $impPatrol1.killed++;
      
      if( $impPatrol1.killed >= 2 && $harabecId.arrivedBase == False )
      {
         order( $harabec, speed, high );
         order( $harabec, cloak, True );
         order( $harabec, Guard, $harPath2 );

         Say( 0, $harabecId, *IDSTR_HA2_HAR04, "HA2_HAR04.wav" );
      }
   }
   
   // occasionaly have Harabec congratulate the player on a kill
   if( randomInt( 1, 4 ) == 1 )
   {
      if( %who == $playerId && isGroupDestroyed( $harabec ) == False && %this != getObjectId( $cargoShip ) )
      {
         Say( 0, $harabecId, "GEN_HAR10.wav", "GEN_HAR10.wav" );
      }  
   }
}

//-----------------------------------------------------------------------------
function cargoShip::vehicle::onDisabled()
{
   $cargoShip.disabled = True;
   
   missionObjective1.status = *IDSTR_OBJ_COMPLETED;
   Say( 0, 1234, "GEN_OC01.wav" );

   // make sure everyone goes straight for the player when he disabled the ship
   order( $impPatrol1, Attack, $playerId );
   order( $impPatrol2, Attack, $playerId );
   order( $impPatrol3, Attack, $playerId );

   if( iSafe( *IDSTR_TEAM_YELLOW, $playerId, 9999 ) )
   {
      addGeneralOrder( *IDSTR_ORDER_HA2_1, "radioRecoveryTeam();" );
      schedule( "forceCommandPopup();", 4 );
      schedule( "Say( 0, $harabecId, *IDSTR_HA2_HAR06, \"HA2_HAR06.wav\" );", 2 );
      order( $harabec, clear, True );
   }
}

//-----------------------------------------------------------------------------
function radioRecoveryTeam()
{
   if( isSafe( *IDSTR_TEAM_YELLOW, $playerId, 9999 ) == False )
   {
      Say( 0, 1234, *IDSTR_HA2_RECA01, "HA2_REC01.wav" );
   }
   else
   {
      dataStore(PlayerManager::playerNumToVehicleId(2049), *IDSTR_ORDER_HA2_1, True);
      removeGeneralOrder(*IDSTR_ORDER_HA2_1);
      
      order( $pickupTeam, Guard, "MissionGroup/NavPoints/NavAlpha" );
      schedule( "setFlybyCamera( \"MissionGroup/pickupTeam/reb4\", -20, 80, 30 );", 3 ); 
      schedule( "Say( 0, 1234, *IDSTR_HA2_RECA02, \"HA2_REC02.wav\" );", 3.5 );

      missionObjective2.status = *IDSTR_OBJ_COMPLETED;            
      missionObjective3.status = *IDSTR_OBJ_COMPLETED;
      Say( 0, 1234, "GEN_OC01.wav" );
      schedule( "forceToDebrief( *IDSTR_MISSION_SUCCESSFUL );", 8 ); 
      updatePlanetInventory( ha2 );
   }
}                                                                    


//-----------------------------------------------------------------------------
function onArrivedNavAlpha()
{
   if( $playerId.arrivedNavAlpha == False )
   {
      $playerId.arrivedNavAlpha = True;
      setNavMarker( "MissionGroup/NavPoints/NavAlpha", False );
   }
}

//-----------------------------------------------------------------------------
function onArrivedBase()
{
   if( $harabecId.arrivedBase == False )
   {            
      $harabecId.arrivedBase = True;
      
//      playSound( 0, "alarm2.wav", IDPRF_FIRE, 1191, 2429, 500 );

      order( $truck1, guard, $truckPath1 );
      order( $truck2, guard, $truckPath1 );
      order( $truck3, guard, $truckPath1 );
      order( $truck4, guard, $truckPath2 );
      order( $truck5, guard, $truckPath2 );
      order( $truck6, guard, $truckPath2 );
      order( $truck7, guard, $truckPath2 );

      alarmSoundsOn( getObjectId( "MissionGroup/impBase/hq" ), "alarm2.wav" );

      order( $harabecId, guard, $playerId );
      order( $harabecId, Attack, $impPatrol2 );
      schedule( "Say( 0, $harabecId, *IDSTR_HA2_HAR05, \"HA2_HAR05.wav\" );", 5 );
      schedule( "order( $harabec, Attack, $impPatrol2 );", 5 );
      
      schedule( "order( $impPatrol1, Attack, $playerId );", 55 );
      schedule( "order( $impPatrol2, Attack, $playerId );", 3 );
      schedule( "order( $impPatrol3, Attack, $playerId );", 35 );
   }
}

//-----------------------------------------------------------------------------
function flierGotAway()
{
   if( isGroupDestroyed( "MissionGroup/impBase/cargoShip" ) == False &&
       $cargoShip.disabled != True )
   {
      fadeEvent( 0, out, 1.5, 0, 0, 0 );
      schedule( "fadeEvent( 0, in, 1.5, 0, 0, 0 );", 1.5 );
      schedule( "setFlybyCamera( \"MissionGroup/impBase/cargoShip\", -100, 60, 190 );", 1.5 ); 
     
      missionObjective1.status = *IDSTR_OBJ_FAILED;
      schedule( "forceToDebrief( *IDSTR_MISSION_FAILED );", 8 );
   
   }
}

//-----------------------------------------------------------------------------
function leavingArea()
{
   if( $playerId.leavingArea != True )
   {
      $playerId.leavingArea = True;
      
      Say( 0, 9999, *IDSTR_GEN_TCM1, "GEN_TCM01.wav" );
   }
}

//-----------------------------------------------------------------------------
function leftArea()
{
   if( $playerId.leftArea != True )
   {
      $playerId.leftArea = True;
      
      Say( 0, 9999, *IDSTR_GEN_TCM2, "GEN_TCM02.wav" );
   
      forceToDebrief( *IDSTR_MISSION_FAILED );
   }
}

//-----------------------------------------------------------------------------
function checkForCargoShipSlide()
{
   // sometimes the cargo ship goes 'a sliding... If it gets too far away from its 
   // original point, nuke it to prevent visual weirdness...
   
   %cargoZ = getPosition( getObjectId( $cargoShip ), z );
   %terrainZ = getTerrainHeight( getPosition( getObjectId("MissionGroup/impBase/cargoMarker"), x ), getPosition( getObjectId("MissionGroup/impBase/cargoMarker"), y ) );
   
   if( getDistance( $cargoship, "MissionGroup/impBase/cargoMarker" ) > 25 && ((%cargoZ - %terrainZ) < 5))
   {
      damageObject( $cargoShip, 9000 );
      echo( "DEBUG: Cargo ship was sliding--destroyed it." );
   } 
   else
   {
      echo( "DEBUG: Cargo ship was not sliding..." );
   }
   
   schedule( "checkForCargoShipSlide();", 1 );
}
//-----------------------------------------------------------------------------
function win()                                                                            
{
   missionObjective1.status = *IDSTR_OBJ_COMPLETED;
   missionObjective2.status = *IDSTR_OBJ_COMPLETED;
   missionObjective3.status = *IDSTR_OBJ_COMPLETED;
   missionObjective4.status = *IDSTR_OBJ_COMPLETED;
   updatePlanetInventory(ha2);
   schedule("forceToDebrief();", 3.0);
}
