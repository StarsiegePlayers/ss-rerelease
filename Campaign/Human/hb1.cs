$rebWasKilled = False;

MissionBriefInfo missionData
{
   title                =  *IDSTR_HB1_TITLE;
   planet               =  *IDSTR_PLANET_MARS;           
   campaign             =  *IDSTR_HB1_CAMPAIGN;           
   dateOnMissionEnd     =  *IDSTR_HB1_DATE;              
   shortDesc            =  *IDSTR_HB1_SHORTBRIEF;     
   longDescRichText     =  *IDSTR_HB1_LONGBRIEF;          
   media                =  *IDSTR_HB1_MEDIA;
   nextMission          =  *IDSTR_HB1_NEXTMISSION;
   successDescRichText  =  *IDSTR_HB1_DEBRIEF_SUCC;
   failDescRichText     =  *IDSTR_HB1_DEBRIEF_FAIL;
   location             =  *IDSTR_HB1_LOCATION;
   successWavFile       =  "HB1_Debriefing.wav";
   soundvol             =  "HB1.vol";
};

MissionBriefObjective missionObjective1
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HB1_OBJ1_SHORT;
   longTxt     = *IDSTR_HB1_OBJ1_LONG;
   bmpname     = *IDSTR_HB1_OBJ1_BMPNAME;
}; 
   
MissionBriefObjective missionObjective2
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HB1_OBJ2_SHORT;
   longTxt     = *IDSTR_HB1_OBJ2_LONG;
   bmpname     = *IDSTR_HB1_OBJ2_BMPNAME;
}; 
      
MissionBriefObjective missionObjective3
{
   isPrimary   = FALSE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HB1_OBJ3_SHORT;
   longTxt     = *IDSTR_HB1_OBJ3_LONG;
   bmpname     = *IDSTR_HB1_OBJ3_BMPNAME;
}; 

DropPoint drop1
{
   name = "Mountain Pass";
   desc = "Mountain Pass";
};

//-----------------------------------------------------------------------------
function onMissionStart()
{
   newFormation( Delta, 0,0,0, -60,-60,0, 60,-45,0, 0,-120,0 ); 
   newFormation( Line,  0,0,0, 0,-40,0, 0,-80,0, 0,-120,0 );
   newFormation( WideLine, 0,0,0, -40,0,0, 40,0,0, -80,0,0 );

   // cdAudioCycle( 12, 10, 3 );
   cdAudioCycle( Watching, Mechsoul, Terror );
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
   $impPatrol3 = "MissionGroup/impPatrol3";
   $impPatrol4 = "MissionGroup/impPatrol4";
   $impPatrol5 = "MissionGroup/impPatrol5";
   $dropship   = "MissionGroup/dropshipGroup/dropship";
   $reb        = "MissionGroup/rebGroup/reb1";
   $orphir     = "MissionGroup/orphir";

   order( "MissionGroup/impPatrol1/imp1", MakeLeader, True );
   order( $impPatrol1, Formation, WideLine );
   order( $impPatrol1, Speed, High );
   
   order( "MissionGroup/impPatrol2/imp1", MakeLeader, True );
   order( $impPatrol2, Formation, Delta );
   order( $impPatrol2, Speed, Medium );

   // order( "MissionGroup/impPatrol3/imp1", MakeLeader, True );
   // order( $impPatrol3, Formation, WideLine );
   // order( $impPatrol3, Speed, High );

   order( "MissionGroup/impPatrol4/imp1", MakeLeader, True );
   order( $impPatrol4, Formation, WideLine );
   order( $impPatrol4, Speed, High );

   order( "MissionGroup/impPatrol5/imp1", MakeLeader, True );
   order( $impPatrol5, Formation, WideLine );
   order( $impPatrol5, Speed, High );

   order( $dropship, Speed, Medium );
   order( $dropship, height, 115, 280 );
   order( $dropship, FlyThrough, True );
   schedule( "order( $dropship, Guard, \"MissionGroup/dropshipPath\" );", 0.8 );
                                                                                                                                              
   // when the player nears the action
   checkBoundary( enter, $playerId, "MissionGroup/NavPoints/NavAlpha", 700, onArrivedNavAlpha );                                                                                                                                                     
 
   // bring on the attackers
   order( $impPatrol1 @ "/imp1", Attack, Pick( $orphir ) );
   order( $impPatrol1 @ "/imp2", Attack, Pick( $orphir ) );
   order( $impPatrol1 @ "/imp3", Attack, Pick( PlayerSquad ) );
   order( $impPatrol1, holdPosition, True );
   
   schedule( "order( $impPatrol2, Attack, Pick( $orphir ) );", 50 );
   schedule( "setVehicleRadarVisible( getObjectId( \"MissionGroup/impPatrol2/imp1\" ), True );", 50 );
   schedule( "setVehicleRadarVisible( getObjectId( \"MissionGroup/impPatrol2/imp2\" ), True );", 50 );
   schedule( "order( $impPatrol2, holdPosition, True );", 51 );

//   schedule( "order( $impPatrol3, Attack, Pick( $orphir ) );", 80 );
//   schedule( "setVehicleRadarVisible( getObjectId( \"MissionGroup/impPatrol3/imp1\" ), True );", 80 );
//   schedule( "setVehicleRadarVisible( getObjectId( \"MissionGroup/impPatrol3/imp2\" ), True );", 80 );
//   schedule( "order( $impPatrol3, holdPosition, True );", 80 );

   schedule( "order( $impPatrol4, Attack, Pick( $orphir ) );", 82 );
   schedule( "setVehicleRadarVisible( getObjectId( \"MissionGroup/impPatrol4/imp1\" ), True );", 82 );
   schedule( "order( $impPatrol4, holdPosition, True );", 82 );

   schedule( "order( $impPatrol5, Attack, Pick( $orphir ) );", 83 );
   schedule( "setVehicleRadarVisible( getObjectId( \"MissionGroup/impPatrol5/imp1\" ), True );", 83 );
   schedule( "order( $impPatrol5, holdPosition, True );", 83 );

   // start our Olympian guarding the place
   order( $reb, Speed, High );
   order( $reb, Attack, Pick( $impPatrol1 ) );

   setNavMarker( "MissionGroup/NavPoints/NavAlpha", True, $playerId );                                                                                 

   schedule( "Say( 0, $playerId, *IDSTR_HB1_DRP01, \"HB1_DRP01.wav\" );", 3 );
   schedule( "Say( 0, $playerId, *IDSTR_HB1_TCM01, \"HB1_TCM1.wav\" );", 7 );

   // randomize impPatrol2 ( 1, leave 'em the same )
   %num = randomInt( 1, 3 );                                             
   if( %num == 2 )                                                       
   {                                                                     
      setPosition( "MissionGroup/impPatrol2/imp1", -1547, -1399, 660 );  
      setPosition( "MissionGroup/impPatrol2/imp2", -1530, -1399, 660 );  
   }                                                                     
   else if( %num == 3 )                                                  
   {                                                                     
      setPosition( "MissionGroup/impPatrol2/imp1", -1881, -1995, 717 );  
      setPosition( "MissionGroup/impPatrol2/imp2", -1871, -1985, 717 );  
   }                                                                     
                                                                         
   // randomize impPatrol3 ( 1, leave 'em the same )                     
//    %num = randomInt( 1, 3 );                                              
//    if( %num == 2 )                                                        
//    {                                                                      
//       setPosition( "MissionGroup/impPatrol3/imp1", 3929, -2506, 887 );    
//       setPosition( "MissionGroup/impPatrol3/imp2", 3919, -2490, 887 );    
//    }                                                                      
//    else if( %num == 3 )                                                   
//    {                                                                      
//       setPosition( "MissionGroup/impPatrol3/imp1", 2764, 649, 747 );      
//       setPosition( "MissionGroup/impPatrol3/imp2", 2744, 639, 747 );      
//    }                                                                      
                                                                         
   // randomize impPatrol4 ( 1, leave 'em the same )                     
   %num = randomInt( 1, 3 );                                             
   if( %num == 2 )                                                       
   {                                                                     
      setPosition( "MissionGroup/impPatrol4/imp1", 3530, -4470, 607 );   
   }                                                                     
   else if( %num == 3 )                                                  
   {                                                                     
      setPosition( "MissionGroup/impPatrol4/imp1", 737, 1545, 727 );     
   }                                                                     
                                                                         
   // randomize impPatrol5 ( 1, leave 'em the same )                     
   %num = randomInt( 1, 3 );                                             
   if( %num == 2 )                                                       
   {                                                                     
      setPosition( "MissionGroup/impPatrol5/imp1", 3510, -4460, 607 );   
   }                                                                     
   else if( %num == 3 )                                                  
   {                                                                     
      setPosition( "MissionGroup/impPatrol5/imp1", 717, 1525, 730 );     
   }                                                                     
}                                                                           

//-----------------------------------------------------------------------------
// Our olympian will try to protect the base by attacking a structure's attacker, 
// but will only attempt to grab a new target every 5 seconds
function structure::onAttacked( %this, %attacker )
{
   if( $reb.isAttackingSomeone != True )
   {
      $reb.isAttackingSomeone = True;
      
      if( getTeam( %attacker ) == *IDSTR_TEAM_RED )
      {
          order( $reb, Attack, %attacker );
          schedule( "$reb.isAttackingSomeone = False;", 5 );
      }
   }
}

//-----------------------------------------------------------------------------
function structure::onDestroyed( %this, %destroyer )
{
   $orphir.buildingsDestroyed++;

   if( $orphir.buildingsDestroyed == 7 )
   {
      missionObjective2.status = *IDSTR_OBJ_FAILED;
      forceToDebrief( *IDSTR_MISSION_FAILED );
   }  
}

//-----------------------------------------------------------------------------
function vehicle::onDestroyed( %this, %destroyer )
{
   // check to see if the area is "safe" after each kill   
   if( %this != $playerId )
   {
      if( isSafe( *IDSTR_TEAM_YELLOW, $playerId, 3000 ) == True && $orphir.buildingsDestroyed < 7 )
      {
         schedule( "Say( 0, $playerId, *IDSTR_HB1_TCM02, \"HB1_TCM2.wav\" );", 1 );
         
         missionObjective2.status = *IDSTR_OBJ_COMPLETED;
         if( $rebWasKilled != True )
         {
            missionObjective3.status = *IDSTR_OBJ_COMPLETED;
            
            InventoryVehicleAdjust(	  mars,	33,	1	);	
            InventoryweaponAdjust(	  mars,	119,	2	);	
            InventoryweaponAdjust(	  mars,	108,	2	);	
            InventoryweaponAdjust(	  mars,	126,	2	);	
            InventoryComponentAdjust(  mars,		111,	1	);	
            InventoryComponentAdjust(  mars,		204,	1	);	
            InventoryComponentAdjust(  mars,		801,	1	);	
            InventoryComponentAdjust(  mars,		306,	1	);	
            InventoryComponentAdjust(  mars,		927,	1	);	
            InventoryComponentAdjust(  mars,		412,	1	);	
            InventoryComponentAdjust(  mars,		900,	1	);	
            InventoryComponentAdjust(  mars,		810,	1	);	
         }

         schedule( "forceToDebrief( *IDSTR_MISSION_SUCCESSFUL );", 3 );
         updatePlanetInventory( hb1 );
      }
   }

   // 'ol Reb was killed
   if( %this == getObjectId( $reb ) )
   {
      $rebWasKilled = True;
      missionObjective3.status = *IDSTR_OBJ_FAILED;
   }

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
function impPatrol1::vehicle::onMessage( %this, %message )
{
   // make sure our attackers keep picking new buildings if theirs is destroyed
   if( %message == "TargetDestroyed" )
   {
      %newTarget = Pick( $orphir );
      %orderTxt = "order( " @ %this @ ", Attack, " @ %newTarget @ " );";
      schedule( %orderTxt, 1 );
   }
}

//-----------------------------------------------------------------------------
function impPatrol2::vehicle::onMessage( %this, %message )
{
   // make sure our attackers keep picking new buildings if theirs is destroyed
   if( %message == "TargetDestroyed" )
   {
      %newTarget = Pick( $orphir );
      %orderTxt = "order( " @ %this @ ", Attack, " @ %newTarget @ " );";
      schedule( %orderTxt, 1 );
   }
}

//-----------------------------------------------------------------------------
// function impPatrol3::vehicle::onMessage( %this, %message )                      
// {                                                                               
//    // make sure our attackers keep picking new buildings if theirs is destroyed 
//    if( %message == "TargetDestroyed" )                                          
//    {                                                                            
//       %newTarget = Pick( $orphir );                                             
//       %orderTxt = "order( " @ %this @ ", Attack, " @ %newTarget @ " );";        
//       schedule( %orderTxt, 1 );                                                 
//    }                                                                            
// }                                                                               

//-----------------------------------------------------------------------------
function impPatrol4::vehicle::onMessage( %this, %message )
{
   // make sure our attackers keep picking new buildings if theirs is destroyed
   if( %message == "TargetDestroyed" )
   {
      %newTarget = Pick( $orphir );
      %orderTxt = "order( " @ %this @ ", Attack, " @ %newTarget @ " );";
      schedule( %orderTxt, 1 );
   }
}

//-----------------------------------------------------------------------------
function impPatrol5::vehicle::onMessage( %this, %message )
{
   // make sure our attackers keep picking new buildings if theirs is destroyed
   if( %message == "TargetDestroyed" )
   {
      %newTarget = Pick( $orphir );
      %orderTxt = "order( " @ %this @ ", Attack, " @ %newTarget @ " );";
      schedule( %orderTxt, 1 );
   }
}

//-----------------------------------------------------------------------------
function onArrivedNavAlpha()
{
   if( $playerId.arrivedNavAlpha != True )
   {
      $playerId.arrivedNavAlpha = True;
      setNavMarker( "MissionGroup/NavPoints/NavAlpha", False );
   
      Say( 0, getObjectId( $reb ), *IDSTR_HB1_RHD01, "HB1_RHD01.wav" );
      
      missionObjective1.status = *IDSTR_OBJ_COMPLETED;
   }
}

//-----------------------------------------------------------------------------
function win()
{
   missionObjective1.status = *IDSTR_OBJ_COMPLETED;
   missionObjective2.status = *IDSTR_OBJ_COMPLETED;
   missionObjective3.status = *IDSTR_OBJ_COMPLETED;
   updatePlanetInventory(hb1);
   schedule("forceToDebrief();", 3.0);
}




