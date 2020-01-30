
$olympianInFertilizer = False;

Pilot DiMarco
{
   id = 30;

   name = "DiMarco";
   skill = 1.0;
   accuracy = 0.6;
   aggressiveness = 0.8;
   activateDist = 400.0;
   deactivateBuff = 200.0;
   targetFreq = 1.6;
   trackFreq = 1.2;
   fireFreq = 1.0;
   LOSFreq = 0.1;
   orderFreq = 6.0;
};

MissionBriefInfo missionData
{
   title                =  *IDSTR_HA3_TITLE;
   planet               =  *IDSTR_PLANET_MARS;           
   campaign             =  *IDSTR_HA3_CAMPAIGN;           
   dateOnMissionEnd     =  *IDSTR_HA3_DATE;              
   shortDesc            =  *IDSTR_HA3_SHORTBRIEF;     
   longDescRichText     =  *IDSTR_HA3_LONGBRIEF;          
   media                =  *IDSTR_HA3_MEDIA;
   nextMission          =  *IDSTR_HA3_NEXTMISSION;
   successDescRichText  =  *IDSTR_HA3_DEBRIEF_SUCC;
   failDescRichText     =  *IDSTR_HA3_DEBRIEF_FAIL;
   location             =  *IDSTR_HA3_LOCATION;
   successWavFile       =  "HA3_Debriefing.wav";
   soundvol             =  "ha3.vol";
};

MissionBriefObjective missionObjective1
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HA3_OBJ1_SHORT;
   longTxt     = *IDSTR_HA3_OBJ1_LONG;
   bmpname     = *IDSTR_HA3_OBJ1_BMPNAME;
}; 
   
MissionBriefObjective missionObjective2
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HA3_OBJ2_SHORT;
   longTxt     = *IDSTR_HA3_OBJ2_LONG;
   bmpname     = *IDSTR_HA3_OBJ2_BMPNAME;
}; 

MissionBriefObjective missionObjective3
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HA3_OBJ3_SHORT;
   longTxt     = *IDSTR_HA3_OBJ3_LONG;
   bmpname     = *IDSTR_HA3_OBJ3_BMPNAME;
}; 

MissionBriefObjective missionObjective4
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HA3_OBJ4_SHORT;
   longTxt     = *IDSTR_HA3_OBJ4_LONG;
   bmpname     = *IDSTR_HA3_OBJ4_BMPNAME;
}; 

MissionBriefObjective missionObjective5
{
   isPrimary   = FALSE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HA3_OBJ5_SHORT;
   longTxt     = *IDSTR_HA3_OBJ5_LONG;
   bmpname     = *IDSTR_HA3_OBJ5_BMPNAME;
}; 

DropPoint drop1
{
   name = "Mountain";
   desc = "Mountain";
};

//-----------------------------------------------------------------------------
function onMissionStart()
{
   newFormation( Delta, 0,0,0, -60,-60,0, 60,-45,0, 0,-120,0 ); 
   newFormation( Line,  0,0,0, 0,-40,0, 0,-80,0, 0,-120,0 );

   cdAudioCycle( Cloudburst, ss4);

   clearGeneralOrders();
   schedule( "order( \"PlayerSquad\", Formation, Line );", 1 );
}

//-----------------------------------------------------------------------------
function player::onAdd(%this)
{
   $playerNum = %this;
}

//-----------------------------------------------------------------------------
function vehicle::onAdd( %this )
{
   %num = playerManager::vehicleIdToPlayerNum( %this );
   if( %num == $playerNum )
   {
      $playerId = playerManager::playerNumToVehicleId( $playerNum );                                                                                    
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
   // global names
   $impPatrol1 = "MissionGroup/impPatrol1";
   $impPatrol2 = "MissionGroup/impPatrol2";
   $imp1       = "MissionGroup/imps/imp1";
   $imp2       = "MissionGroup/imps/imp2";
   $imp3       = "MissionGroup/imps/imp3";
   $imp4       = "MissionGroup/imps/imp4";
   $imp5       = "MissionGroup/imps/imp5";
   $imp6       = "MissionGroup/imps/imp6";
   $imp7       = "MissionGroup/imps/imp7";
   $imp8       = "MissionGroup/imps/imp8";
   $imps       = "MissionGroup/imps";
   $dropship   = "MissionGroup/dropShipGroup/dropship";
   $olympian   = "MissionGroup/olympian/olympian";
   $fertPath   = "MissionGroup/fertPath";

   order( $olympian, speed, high );

   setVehicleRadarVisible( getObjectId( $olympian ), False );

   order( $impPatrol1, Formation, Line );
   order( $impPatrol1, Speed, High );
   order( "MissionGroup/impPatrol1/imp1", MakeLeader, True );
   setVehicleRadarVisible( getObjectId( "MissionGroup/impPatrol1/imp1" ), False );
   setVehicleRadarVisible( getObjectId( "MissionGroup/impPatrol1/imp2" ), False );
   
   order( $impPatrol2, Formation, Delta );
   order( $impPatrol2, Speed, Low );
   order( "MissionGroup/impPatrol2/imp1", MakeLeader, True );
   setVehicleRadarVisible( getObjectId( "MissionGroup/impPatrol2/imp1" ), False );
   setVehicleRadarVisible( getObjectId( "MissionGroup/impPatrol2/imp2" ), False );

   order( $dropship, Speed, High );
   schedule( "order( $dropship, Guard, \"MissionGroup/dropShipPath1\" );", 1.5 );

   // Our Imps should be "sleeping" and not visible on radar
   order( $impPatrol1, shutdown, True );
   order( $impPatrol2, shutdown, True );
   order( $imp1, shutdown, True );
   order( $imp2, shutdown, True );
   order( $imp3, shutdown, True );
   order( $imp4, shutdown, True );
   order( $imp5, shutdown, True );
   order( $imp6, shutdown, True );
   order( $imp7, shutdown, True );
   order( $imp8, shutdown, True );
 
   setVehicleRadarVisible( getObjectId( $imp1 ), False );
   setVehicleRadarVisible( getObjectId( $imp2 ), False );
   setVehicleRadarVisible( getObjectId( $imp3 ), False );
   setVehicleRadarVisible( getObjectId( $imp4 ), False );
   setVehicleRadarVisible( getObjectId( $imp5 ), False );
   setVehicleRadarVisible( getObjectId( $imp6 ), False );
   setVehicleRadarVisible( getObjectId( $imp7 ), False );
   setVehicleRadarVisible( getObjectId( $imp8 ), False );
                                                                                                                                              
   // when the player nears the action
   checkBoundary( enter, $playerId, "MissionGroup/NavPoints/NavAlpha", 250, onArrivedNavAlpha );                                                                                                                                                     
   checkBoundary( enter, $playerId, "MissionGroup/NavPoints/NavBravo", 250, onArrivedNavBravo );                                                                                                                                                     
   
   // global bounds checking
   checkBoundary( leave, $playerId, "MissionGroup/NavPoints/NavAlpha", 5500, leavingArea );
   checkBoundary( leave, $playerId, "MissionGroup/NavPoints/NavAlpha", 6000, leftArea );

   // when Olympian is visibly sighted
   checkBoundary( enter, $playerId, $olympian, 530, nearingOlympian );
   checkBoundary( enter, $playerId, $olympian, 200, arrivedAtOlympian );
   
   // olympian nears NAV Bravo--we'll have her follow a path so she doesn't get too stuck
   checkBoundary( enter, getObjectId( $olympian ), "MissionGroup/NavPoints/NavBravo", 700, olympianNearsNavBravo );
   checkBoundary( enter, getObjectId( $olympian ), "MissionGroup/NavPoints/NavBravo", 100, olympianAtNavBravo );

   // If the player approaches any of these Hercs, they will "wake up" and attack him
   $playerSquadId = getObjectId("playerSquad");
   if ($playerSquadId == 0)
      echo("------------------------ OH SHIT NO PLAYER SQUAD ID!!!");
   checkBoundary( enter, $playerSquadId, "MissionGroup/impPatrol1/imp1", 500, wakeupImpPatrol1 );
   checkBoundary( enter, $playerSquadId, "MissionGroup/impPatrol2/imp1", 500, wakeupImpPatrol2 );
   checkBoundary( enter, $playerSquadId, $imp1, 600, wakeupImp1 );
   checkBoundary( enter, $playerSquadId, $imp2, 850, wakeupImp2 );
   checkBoundary( enter, $playerSquadId, $imp3, 600, wakeupImp3 );
   checkBoundary( enter, $playerSquadId, $imp4, 600, wakeupImp4 );
   checkBoundary( enter, $playerSquadId, $imp5, 600, wakeupImp5 );
   checkBoundary( enter, $playerSquadId, $imp6, 600, wakeupImp6 );
   checkBoundary( enter, $playerSquadId, $imp7, 600, wakeupImp7 );
   checkBoundary( enter, $playerSquadId, $imp8, 800, wakeupImp8 );

   // randomly choose a location for our olympian
   %num = randomInt( 1, 3 );
   if( %num == 1 )
   {
      setPosition( getObjectId( $olympian ), 168, 2426, 455 );  
   }
   else if( %num == 2 )
   {
      $olympianInFertilizer = True;
      setPosition( getObjectId( $olympian ), -381, 2701, 455 );  
   }
   else if( %num == 3 )
   {
      setPosition( getObjectId( $olympian ), -905, 1874, 455 );  
   }

   // Our Olympian is supposed to be wounded...
   damageObject( getObjectId( $olympian ), 8000 );
  
   forceScope( getObjectId( $dropship ), 9999 );
   
   setNavMarker( "MissionGroup/NavPoints/NavAlpha", True, $playerId );                                                                                 

   setVehicleRadarVisible( getObjectId( "MissionGroup/stuff/v1" ), False );
   setVehicleRadarVisible( getObjectId( "MissionGroup/stuff/v2" ), False );
   setVehicleRadarVisible( getObjectId( "MissionGroup/stuff/v3" ), False );
   setVehicleRadarVisible( getObjectId( "MissionGroup/stuff/v4" ), False );
   setVehicleRadarVisible( getObjectId( "MissionGroup/stuff/v5" ), False );
   setVehicleRadarVisible( getObjectId( "MissionGroup/stuff/v6" ), False );
   setVehicleRadarVisible( getObjectId( "MissionGroup/stuff/v7" ), False );
   setVehicleRadarVisible( getObjectId( "MissionGroup/stuff/v8" ), False );
   setVehicleRadarVisible( getObjectId( "MissionGroup/stuff/v9" ), False );
   setVehicleRadarVisible( getObjectId( "MissionGroup/stuff/v10" ), False );
   setVehicleRadarVisible( getObjectId( "MissionGroup/stuff/v11" ), False );

   order( $olympian, shutdown, True );
}

//-----------------------------------------------------------------------------
function vehicle::onArrived( %who, %where )
{
   // dropship goes into the boonies then shutsdown
   //if( %who == getObjectId( $dropship ) && %where == getObjectId( "MissionGroup/dropShipPath1/marker2" ) )
   //{
   //  IF YOU SHUTDOWN THE FLYER, IT SMACKS INTO THE GROUND
   //   order( $dropship, shutdown, true );
   //}

   if( %who == getObjectId( $dropship ) && %where == getObjectId( "MissionGroup/dropShipPath2/marker1" ) )
   {
      $dropship.arrived = True;
   }

   if( %who == getObjectId( $olympian ) && %where == getObjectId( "MissionGroup/olympianPath/endMmarker" ) )
   {
      order( $olympian, Guard, "MissionGroup/NavPoints/NavBravo" );
      order( $olympian, HoldPosition, True );
   }

   if( %who == getObjectId( "PlayerSquad/SquadMate1" ) && %where == getObjectId( "MissionGroup/squadmatePath/marker3" ) )
   {
      order( "PlayerSquad/SquadMate1", Formation );
   }

   if( $olympianInFertilizer == True )
   {
      if( %who == getObjectId( $olympian ) && %where == getObjectId( "MissionGroup/fertPath/marker1" ) )
      {
         order( $olympian, Guard, $playerId );
         order( $olympian, holdPosition, True );
      }
   }
}

function vehicle::onAttacked( %this, %attacker )
{
   // player is attacking olympian
   if( %this == getObjectId( $olympian ) && %attacker == $playerId )
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
               schedule( "Say( 0, getObjectId( $olympian ), *IDSTR_HA3_OLY02, \"HA3_OLY02.wav\" );", 1 );
            }
            else if( getObjectId( $olympian ).pissed != True )
            {
               $beenAttacked = True;
               $olympian.pissed = True;
               order( $olympian, Attack, $playerId );
               schedule( "Say( 0, getObjectId( $olympian ), *IDSTR_HA3_OLY03, \"HA3_OLY03.wav\" );", 0.8);
            }
         }
      }
   }

   // Imp is attacking olympian
   if( %this == getObjectId( $olympian ) && %attacker != $playerId )
   {
      $olympian.attacked++;
      
      if( $olympian.attacked == 10 )
      {
         Say( 0, getObjectId( $olympian ), *IDSTR_HA3_OLY04, "HA3_OLY04.wav" );
      }
      else if( $olympian.attacked == 35 )
      {
         Say( 0, getObjectId( $olympian ), *IDSTR_HA3_OLY05, "HA3_OLY05.wav" );
      }
      else if( $olympian.attacked == 40 )
      {
         Say( 0, getObjectId( $olympian ), *IDSTR_HA3_OLY06, "HA3_OLY06.wav" );
      }
   }

   // someone is shooting up our dropship
   if( %this == getObjectId( $dropship ) )
   {
      $dropship.attacked++;
      
      if( $dropship.attacked == 10 )
      {
         Say( 0, getObjectId( $dropship ), *IDSTR_GEN_DS11, "GEN_DS1.wav" );
         
         fadeEvent( 0, out, 1.5, 0, 0, 0 );
         schedule( "fadeEvent( 0, in, 1.5, 0, 0, 0 );", 1.5 );
         schedule( "setFlybyCamera( $dropship, -100, 60, 190 );", 1.5 ); 
     
         missionObjective4.status = *IDSTR_OBJ_FAILED;
         schedule( "forceToDebrief( *IDSTR_MISSION_FAILED );", 8 );
      }
   }
   
   // if a sleeping Herc gets hit, wake 'em up
   if( getTeam( %this ) == *IDSTR_TEAM_RED )
   {
      order( %this, ShutDown, False );
   }
}

//-----------------------------------------------------------------------------
function vehicle::onDestroyed(%this, %who)
{
   // Olympian gets whacked
   if( %this == getObjectId( $olympian ) )
   {
      missionObjective2.status = *IDSTR_OBJ_FAILED;
      missionObjective3.status = *IDSTR_OBJ_FAILED;

      $missionFailed = True;
      
      $olympian.killer = %who;

      setDominantCamera( $olympian, $olympian.killer ); 
      schedule( "forceToDebrief( *IDSTR_MISSION_FAILED );", 3 );
   }

   // all enemies in the area have been destroyed... Secondary objective satisfied
   if( isSafe( *IDSTR_TEAM_YELLOW, $playerId, 20000 ) == True )
   {
      missionObjective5.status = *IDSTR_OBJ_COMPLETED;
      schedule( "Say( 0, 1234, \"GEN_OC01.wav\" );", 1 );
   }
}


//-----------------------------------------------------------------------------
function onArrivedNavAlpha()
{
   if( $playerId.arrivedNavAlpha != True )
   {
      $playerId.arrivedNavAlpha = True;
   }
}

//-----------------------------------------------------------------------------
function onArrivedNavBravo()
{
   if( $playerId.arrivedAtOlympian == True )
   {
      if( $playerId.arrivedNavBravo != True && getDistance( $playerId, getObjectId( $olympian ) ) <= 400 )
      {
         $playerId.arrivedNavBravo = True;

         // The dropship may now be called into NAV Bravo
         missionObjective3.status = *IDSTR_OBJ_COMPLETED;
         schedule( "Say( 0, 1234, \"GEN_OC01.wav\" );", 1 );
         addGeneralOrder( "Call in dropship", "callInDropship();" );
         
         // keep calling this order box if the player ignores it
         repeatGeneralOrder(PlayerManager::playerNumToVehicleId(2049), *IDSTR_ORDER_HA3_1);
         
         setNavMarker( "MissionGroup/NavPoints/NavBravo", False );
      }
   }
}
                          
//-----------------------------------------------------------------------------
function nearingOlympian()
{
   if( $playerId.nearingOlympian != True )
   {
      $playerId.nearingOlympian = True;
      
      // the Olympian now shows up on the player's radar--makes him a little easier to find
      setVehicleRadarVisible( getObjectId( $olympian ), True );
   }
}

//-----------------------------------------------------------------------------
function arrivedAtOlympian()
{
   if( $playerId.arrivedAtOlympian != True )
   {
      $playerId.arrivedAtOlympian = True;

      missionObjective1.status = *IDSTR_OBJ_COMPLETED;
      schedule( "Say( 0, 1234, \"GEN_OC01.wav\" );", 1 );
            
      // The olympian thanks and joins the player
      Say( 0, getObjectId( $olympian ), *IDSTR_HA3_OLY01, "HA3_OLY01.wav" );
      order( $olympian, shutdown, false );
      
      setNavMarker( "MissionGroup/NavPoints/NavAlpha", False );
      setNavMarker( "MissionGroup/NavPoints/NavBravo", True, $playerId );

      // if the olympian is inside a fertilizer, make sure she has a clear path out
      if( $olympianInFertilizer == True )
      {
         order( $olympian, Guard, $fertPath );
      }
      else
      {
         order( $olympian, Guard, $playerId );
      }
      order( $olympian, holdPosition, True );
   
      
      // impPatrol1 attacks from the mountain pass near NAV Bravo
      schedule( "order( $impPatrol1, shutdown, False );", 30 );
      schedule( "order( $impPatrol1, attack, Pick( PlayerSquad ) );", 30 );

      // start "waking" random Imps in the area to attack the player's squad
      schedule( "wakeupRandomImps();", 90 );

      setNavMarker( "MissionGroup/NavPoints/NavBravo", True, $playerId );
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
function callInDropship()
{
   if( isSafe( *IDSTR_TEAM_YELLOW, "MissionGroup/dropShipPath2/marker1", 200 ) == False )
   {                      
      // replace with generic dropship chatter
      Say( 0, getObjectId( $dropship ), *IDSTR_HA3_1DS02, "HA3_1DS02.wav", "HA3_1DS03.wav", "HA3_1DS04.wav" );
   }
   else
   {
      dataStore(PlayerManager::playerNumToVehicleId(2049), *IDSTR_ORDER_HA3_1, True);
      removeGeneralOrder( *IDSTR_ORDER_HA3_1 );

      $playerId.missionDone = True;
      
      order( $dropship, shutDown, False );
      order( $dropship, speed, High );
      order( $dropship, guard, "MissionGroup/dropShipPath2/marker1" );
      
      schedule( "setOrbitCamera( $dropship, 205, 140, 50 );", 3 );
      // replace with generic dropship chatter
      schedule( "Say( 0, getObjectId( $dropShip ), *IDSTR_HA3_1DS01, \"HA3_1DS01.wav\" );", 5 );
      schedule( "forceToDebrief( *IDSTR_MISSION_SUCCESSFUL );", 8 );
      missionObjective2.status = *IDSTR_OBJ_COMPLETED;
      schedule( "Say( 0, 1234, \"GEN_OC01.wav\" );", 1 );
      updatePlanetInventory( ha3 );
     
      missionObjective4.status = *IDSTR_OBJ_COMPLETED;
   }
}

//-----------------------------------------------------------------------------
function olympianNearsNavBravo()
{
   if( $olympian.nearsNavBravo != True )
   {
      $olympian.nearsNavBravo = True;
   
      order( $olympian, Guard, "MissionGroup/olympianPath" );
   }
}

//-----------------------------------------------------------------------------
function wakeupImpPatrol1()
{
   if( $playerId.nearedImpPatrol1 != True )
   {
      echo( "DEBUG: waking up ImpPatrol1!" );

      $playerId.nearedImpPatrol1 = True;

      order( $impPatrol1, shutdown, False );
      order( $impPatrol1, attack, Pick( PlayerSquad ) );
      setVehicleRadarVisible( getObjectId( "MissionGroup/impPatrol1/imp1" ), True );
      setVehicleRadarVisible( getObjectId( "MissionGroup/impPatrol1/imp2" ), True );
   }
}

//-----------------------------------------------------------------------------
function wakeupImpPatrol2()
{
   if( $playerId.nearedImpPatrol2 != True )
   {
      echo( "DEBUG: waking up ImpPatrol2!" );

      $playerId.nearedImpPatrol2 = True;

      order( $impPatrol2, shutdown, False );
      order( $impPatrol2, attack, Pick( PlayerSquad ) );
      setVehicleRadarVisible( getObjectId( "MissionGroup/impPatrol2/imp1" ), True );
      setVehicleRadarVisible( getObjectId( "MissionGroup/impPatrol2/imp2" ), True );
   }
}

//-----------------------------------------------------------------------------
function wakeupImp1()
{
   if( $playerId.nearedImp1 != True )
   {
      echo( "DEBUG: waking up ImpP1!" );

      $playerId.nearedImp1 = True;
      order( $imp1, shutdown, False );
      order( $imp1, attack, Pick( PlayerSquad ) );
      setVehicleRadarVisible( getObjectId( $imp1 ), True );
   }
}

//-----------------------------------------------------------------------------
function wakeupImp2()
{
   if( $playerId.nearedImp2 != True )
   {
      echo( "DEBUG: waking up ImpP2!" );

      $playerId.nearedImp2 = True;
      order( $imp2, shutdown, False );
      order( $imp2, attack, Pick( PlayerSquad ) );
      order( $imp8, shutdown, False );
      order( $imp8, attack, Pick( PlayerSquad ) );
      setVehicleRadarVisible( getObjectId( $imp2 ), True );
      setVehicleRadarVisible( getObjectId( $imp8 ), True );
   }  
}

//-----------------------------------------------------------------------------
function wakeupImp3()
{
   if( $playerId.nearedImp3 != True )
   {
      echo( "DEBUG: waking up Imp3!" );

      $playerId.nearedImp3 = True;
      order( $imp3, shutdown, False );
      order( $imp3, attack, Pick( PlayerSquad ) );
      setVehicleRadarVisible( getObjectId( $imp3 ), True );
   }
}

//-----------------------------------------------------------------------------
function wakeupImp4()
{
   if( $playerId.nearedImp4 != True )
   {
      echo( "DEBUG: waking up Imp4!" );

      $playerId.nearedImp4 = True;
      order( $imp4, shutdown, False );
      order( $imp4, attack, Pick( PlayerSquad ) );
      setVehicleRadarVisible( getObjectId( $imp4 ), True );
   }
}

//-----------------------------------------------------------------------------
function wakeupImp5()
{
   if( $playerId.nearedImp5 != True )
   {
      echo( "DEBUG: waking up Imp5!" );

      $playerId.nearedImp5 = True;
      order( $imp5, shutdown, False );
      order( $imp5, attack, Pick( PlayerSquad ) );
      setVehicleRadarVisible( getObjectId( $imp5 ), True );
   }
}

//-----------------------------------------------------------------------------
function wakeupImp6()
{
   if( $playerId.nearedImp6 != True )
   {
      echo( "DEBUG: waking up Imp6!" );

      $playerId.nearedImp6 = True;
      order( $imp6, shutdown, False );
      order( $imp6, attack, Pick( PlayerSquad ) );
      setVehicleRadarVisible( getObjectId( $imp6 ), True );
   }
}

//-----------------------------------------------------------------------------
function wakeupImp7()
{
   if( $playerId.nearedImp7 != True )
   {
      echo( "DEBUG: waking up Imp7!" );

      $playerId.nearedImp7 = True;
      order( $imp7, shutdown, False );
      order( $imp7, attack, Pick( PlayerSquad ) );
      setVehicleRadarVisible( getObjectId( $imp7 ), True );
   }
}

//-----------------------------------------------------------------------------
function wakeupImp8()
{
   if( $playerId.nearedImp8 != True )
   {
      echo( "DEBUG: waking up Imp8!" );

      $playerId.nearedImp8 = True;
      order( $imp2, shutdown, False );
      order( $imp2, attack, Pick( PlayerSquad ) );
      order( $imp8, shutdown, False );
      order( $imp8, attack, Pick( PlayerSquad ) );
      setVehicleRadarVisible( getObjectId( $imp2 ), True );
      setVehicleRadarVisible( getObjectId( $imp2 ), True );
   }
}

//-----------------------------------------------------------------------------
function wakeupRandomImps()
{
   %herc = Pick( "MissionGroup/imps" );
   
   order( %herc, shutdown, False );
   order( %herc, attack, Pick( PlayerSquad ) );
   setVehicleRadarVisible( %herc, True );

   schedule( "wakeupRandomImps();", 60 );
}


//-----------------------------------------------------------------------------
function olympianAtNavBravo()
{
   if( $olympian.atNavBravo != True )
   {
      $olympian.atNavBravo = True;
      
      // make sure the olympian doesn't keep treking along her path
      order( $olympian, shutdown, True );
      order( $olympian, clear, True );
   }
}

//-----------------------------------------------------------------------------
function win()
{
   missionObjective1.status = *IDSTR_OBJ_COMPLETED;
   missionObjective2.status = *IDSTR_OBJ_COMPLETED;
   missionObjective3.status = *IDSTR_OBJ_COMPLETED;
   missionObjective4.status = *IDSTR_OBJ_COMPLETED;
   missionObjective5.status = *IDSTR_OBJ_COMPLETED;
   updatePlanetInventory(ha3);
   schedule("forceToDebrief();", 3.0);
}

