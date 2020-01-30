$enteredCrater = False;

Pilot Prometheus
{
   id = 50;
   
   name = "Prometheus";
   skill = 2.0;
   accuracy = 0.9;
   aggressiveness = 2.0;
   activateDist = 1200.0;
   deactivateBuff = 300.0;
   targetFreq = 0.4;
   trackFreq = 0.4;
   fireFreq = 0.5;
   LOSFreq = 0.4;
   orderFreq = 2.0;
};

Pilot Caanon
{
   id = 51;
   
   name = "Caanon";
   skill = 0.6;
   accuracy = 0.8;
   aggressiveness = 2.0;
   activateDist = 700.0;
   deactivateBuff = 200.0;
   targetFreq = 0.7;
   trackFreq = 0.4;
   fireFreq = 0.1;
   LOSFreq = 0.4;
   orderFreq = 2.0;
};

MissionBriefInfo missionData
{
   title                =  *IDSTR_HE3_TITLE;
   planet               =  *IDSTR_PLANET_PLUTO;           
   campaign             =  *IDSTR_HE3_CAMPAIGN;           
   dateOnMissionEnd     =  *IDSTR_HE3_DATE;              
   shortDesc            =  *IDSTR_HE3_SHORTBRIEF;     
   longDescRichText     =  *IDSTR_HE3_LONGBRIEF;          
   media                =  *IDSTR_HE3_MEDIA;
   nextMission          =  *IDSTR_HE3_NEXTMISSION;
   successDescRichText  =  *IDSTR_HE3_DEBRIEF_SUCC;
   failDescRichText     =  *IDSTR_HE3_DEBRIEF_FAIL;
   location             =  *IDSTR_HE3_LOCATION;
   soundvol             =  "HE3.vol";
};

MissionBriefObjective missionObjective1
{
   isPrimary   = TRUE;
   status      = *IDSTR_OBJ_ACTIVE;
   shortTxt    = *IDSTR_HE3_OBJ1_SHORT;
   longTxt     = *IDSTR_HE3_OBJ1_LONG;
   bmpname     = *IDSTR_HE3_OBJ1_BMPNAME;
}; 

DropPoint drop1
{
   name = "Mountain Pass";
   desc = "Mountain Pass";
};

//-----------------------------------------------------------------------------
function onMissionStart()
{
   cdAudioCycle( CloudBurst, Terror );
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
   plutoSounds();
}

//-----------------------------------------------------------------------------
function init()
{
   $caanon = "MissionGroup/caanon";
   $bigZedShape = "MissionGroup/cprombase1";
   $cybrids = "MissionGroup/cybrids";
   $cybrid1 = "MissionGroup/cybrids/cybrid1";
   $cybrid2 = "MissionGroup/cybrids/cybrid2";
   $cybrid3 = "MissionGroup/cybrids/cybrid3";
   $cybrid4 = "MissionGroup/cybrids/cybrid4";
   $cybrid5 = "MissionGroup/cybrids/cybrid5";
   $cybrid6 = "MissionGroup/cybrids/cybrid6";
   $cybrid7 = "MissionGroup/cybrids/cybrid7";
   $cybrid8 = "MissionGroup/cybrids/cybrid8";
   $cybrid9 = "MissionGroup/cybrids/cybrid9";
   $cybrid10 = "MissionGroup/cybrids/cybrid10";
   $cybrid11 = "MissionGroup/cybrids/cybrid11";
   $cybrid12 = "MissionGroup/cybrids/cybrid12";
   $cybrid13 = "MissionGroup/cybrids/cybrid13";
   $cybrid14 = "MissionGroup/cybrids/cybrid14";
   $cybrid15 = "MissionGroup/cybrids/cybrid15";
   $cybrid16 = "MissionGroup/cybrids/cybrid16";
   $markers = "MissionGroup/markers";
   $hut     = "MissionGroup/hut";

   order( $cybrid1, Speed, High );
   order( $cybrid2, Speed, High );
   order( $cybrid3, Speed, High );
   order( $cybrid4, Speed, High );
   order( $cybrid5, Speed, High );
   order( $cybrid6, Speed, High );
   order( $cybrid7, Speed, High );
   order( $cybrid8, Speed, High );
   order( $cybrid9, Speed, High );
   order( $cybrid10, Speed, High );
   order( $cybrid11, Speed, High );
   order( $cybrid12, Speed, High );
   order( $cybrid13, Speed, High );
   order( $cybrid14, Speed, High );
   order( $cybrid15, Speed, High );
   order( $cybrid16, Speed, High );

   forceScope( $cybrid1, 9999 );
   forceScope( $cybrid2, 9999 );
   forceScope( $cybrid3, 9999 );
   forceScope( $cybrid4, 9999 );
   forceScope( $cybrid5, 9999 );
   forceScope( $cybrid6, 9999 );
   forceScope( $cybrid7, 9999 );
   forceScope( $cybrid8, 9999 );
   forceScope( $cybrid9, 9999 );
   forceScope( $cybrid10, 9999 );
   forceScope( $cybrid11, 9999 );
   forceScope( $cybrid12, 9999 );
   forceScope( $cybrid13, 9999 );
   forceScope( $cybrid14, 9999 );
   forceScope( $cybrid15, 9999 );
   forceScope( $cybrid16, 9999 );

   // pick from a pool of 6 prometheus'sss 
   $prometheus = "MissionGroup/proms/p" @ randomInt( 1, 6 );
   forceScope( $prometheus, 9999 );
   order( $prometheus, Speed, High );
   
   checkBoundary( enter, $playerId, "MissionGroup/NavPoints/NavAlpha", 160, onArrivedNavAlpha );
   checkBoundary( enter, $playerId, $prometheus, 1300, onArrivedPrometheus );
   checkBoundary( enter, $playerId, $prometheus, 1500, onNearPrometheus );

   schedule( "dropPod( \"MissionGroup/markers/marker1\", $cybrid1 );", 4 );
   schedule( "order( $cybrid1, Attack, Pick( PlayerSquad ) );", 5 );
   schedule( "dropPod( \"MissionGroup/markers/marker2\", $cybrid2 );", 6 );
   schedule( "order( $cybrid2, Attack, Pick( PlayerSquad ) );", 7 );
   
   schedule( "order( $caanon, Attack, $cybrid1 );", 5.7 );
   
                                                                                             
   order( $caanon, Speed, High );                                                            
   schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA01, \"HE3_CAA01.wav\" );", 2 );  

   setNavMarker( "MissionGroup/NavPoints/NavAlpha", True, -1 );

   // TEMPORARY!
   schedule( "tellPlayerToGo();", 60 );
   schedule( "tellPlayerToGo2();", 180 );
   schedule( "tellPlayerToGo3();", 260 );
  
   setPosition( $prometheus, -136.82, -1748, 461 );

   removeSquadmateOrders();

   // dirty way to keep your squadmates from following you into the hole
   checkBoundary( enter, "PlayerSquad/SquadMate1", $prometheus, 1800, orderSquadmatesToGuardCaanon );
   checkBoundary( enter, "PlayerSquad/SquadMate2", $prometheus, 1800, orderSquadmatesToGuardCaanon );
   checkBoundary( enter, "PlayerSquad/SquadMate3", $prometheus, 1800, orderSquadmatesToGuardCaanon );
}

// prometheus & caanon talk shit during the Big Fight
function vehicle::onAttacked( %this, %attacker )
{
   if( %this == getObjectId( $prometheus ) )                                                      
   {                                                                                              
      if( $prometheus.taunting != True )                                                        
      {                                                                                           
         $prometheus.taunting = True;                                                            
         schedule( "$prometheus.taunting = False;", 9 );                                        
                                                                                                 
         $prometheus.taunt_num++;
         if( $prometheus.taunt_num == 1 )
         {
            // schedule( "Say( 0, getObjectId( $prometheus ), *IDSTR_HE3_BAD01, \"HE3_BAD01.wav\" );", 1 );
         }
         if( $prometheus.taunt_num == 2 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA06, \"HE3_CAA06.wav\" );", 1 );
         }
         if( $prometheus.taunt_num == 3 )
         {
            schedule( "Say( 0, getObjectId( $prometheus ), *IDSTR_HE3_BAD02, \"HE3_BAD02.wav\" );", 1 );
         }
         if( $prometheus.taunt_num == 4 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), \"HE3_CAA07.wav\" );", 1 );
         }
         if( $prometheus.taunt_num == 5 )
         {
            schedule( "Say( 0, getObjectId( $prometheus ), *IDSTR_HE3_BAD05, \"HE3_BAD05.wav\" );", 1 );
         }
         if( $prometheus.taunt_num == 6 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA07, \"HE3_CAA07.wav\" );", 1 );
         }
         if( $prometheus.taunt_num == 7 )
         {
            schedule( "Say( 0, getObjectId( $prometheus ), *IDSTR_HE3_BAD06, \"HE3_BAD06.wav\" );", 1 );
         }
         if( $prometheus.taunt_num == 8 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA08, \"HE3_CAA08.wav\" );", 1 );
         }
         if( $prometheus.taunt_num == 9 )
         {
            schedule( "Say( 0, getObjectId( $prometheus ), *IDSTR_HE3_BAD04, \"HE3_BAD04.wav\" );", 1 );
         }
         if( $prometheus.taunt_num == 10 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA09, \"HE3_CAA09.wav\" );", 1 );
         }
         if( $prometheus.taunt_num == 11 )
         {
            schedule( "Say( 0, getObjectId( $prometheus ), *IDSTR_HE3_BAD08, \"HE3_BAD08.wav\" );", 1 );
         }
      }                                                                                          
   }    

   // generic caanon shit-talkin
   if( %attacker == getObjectId( $caanon ) )
   {
 
      if( $caanon.taunting != True )
      {
         $caanon.taunting = True;
         
         schedule( "$caanon.taunting = False;", 25 );
         
         $caanon.taunts++;
         
         if( $caanon.taunts == 2 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA02, \"HE3_CAA02.wav\" );", 1 );
         }
         if( $caanon.taunts == 3 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA03, \"HE3_CAA03.wav\" );", 1 );
         }
         if( $caanon.taunts == 4 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA04, \"HE3_CAA04.wav\" );", 1 );
         }
         if( $caanon.taunts == 5 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA05, \"HE3_CAA05.wav\" );", 1 );
         }
         if( $caanon.taunts == 6 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA06, \"HE3_CAA06.wav\" );", 1 );
         }
         if( $caanon.taunts == 7 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA07, \"HE3_CAA07.wav\" );", 1 );
         }
         if( $caanon.taunts == 8 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA08, \"HE3_CAA08.wav\" );", 1 );
         }
         if( $caanon.taunts == 9 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA09, \"HE3_CAA09.wav\" );", 1 );
         }
         if( $caanon.taunts == 10 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA10, \"HE3_CAA10.wav\" );", 1 );
         }
         if( $caanon.taunts == 11 )
         {
            schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA10, \"HE3_CAA10.wav\" );", 1 );
         }
      }
   }

   if( %this == getObjectId( $caanon ) )
   {
      // Caanon is indestructible in this mission
      healObject( getObjectId( $caanon ), 5000 );
   }
}                                                                                                      
                                                                                                        
//-----------------------------------------------------------------------------                        
function vehicle::onDestroyed(%this, %who)                                                             
{                                                                                                      
    if( getTeam( %this ) == *IDSTR_TEAM_RED )                                                           
    {     
      %num = randomInt( 1, 5 );
    
       if( %this == getObjectId( $cybrid1 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid3 );
          order( $cybrid3, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid2 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid4 );
          order( $cybrid4, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid3 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid5 );
          order( $cybrid5, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid4 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid6 );
          order( $cybrid6, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid5 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid7 );
          order( $cybrid7, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid6 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid8 );
          order( $cybrid8, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid7 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid9 );
          order( $cybrid9, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid8 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid10 );
          order( $cybrid10, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid9 ) )                                                           
       {                                                                                               
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid11 );
          order( $cybrid11, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid10 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid12 );
          order( $cybrid12, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid11 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid13 );
          order( $cybrid13, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid12 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid14 );
          order( $cybrid14, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid13 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid15 );
          order( $cybrid15, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
       if( %this == getObjectId( $cybrid14 ) )                                                           
       {                                                                                                
          dropPod( "MissionGroup/markers/marker" @ %num, $cybrid16 );
          order( $cybrid16, Attack, Pick( PlayerSquad ) );                                                       
       }                                                                                                
    }                                                                                                   

   if( %this == getObjectId( $prometheus ) )                                                  
   {                                                                                            
      schedule( "fadeEvent( 0, out, 2.5, 1, 1, 1 );", 1.0 );                                    
      schedule( "setDominantCamera( $playerId, $prometheus );", 3.9 );                              
      schedule( "fadeEvent( 0, in, 1.0, 1, 1, 1 );", 4.5 );                                     
                                                                  
      shutup();                                                                     
                                                                                                
      schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA12, \"HE3_CAA12.wav\" );", 2 );
      schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA13, \"HE3_CAA13.wav\" );", 6 );
      schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_LON01, \"HE3_LON01.wav\" );", 8 );
      schedule( "Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA14, \"HE3_CAA14.wav\" );", 12 );
      

      schedule( "forceToDebrief();", 25 );                                                      
                                                                                                
      missionObjective1.status = *IDSTR_OBJ_COMPLETED;                                          

      killChannel( getObjectId( $prometheus ) );
   }                                             
   
   if( %this == getObjectId( $caanon ) )
   {
      killChannel( getObjectId( $caanon ) );
   }                                               
}

//-----------------------------------------------------------------------------
function onArrivedNavAlpha()
{
   if( $playerId.arrivedNavAlpha != True )
   {
      $playerId.arrivedNavAlpha = True;
      setNavMarker( "MissionGroup/NavPoints/NavAlpha", False );
   }
}

function onNearPrometheus()
{
   if( $playerId.nearedPrometheus != True )
   {
      $playerId.nearedPrometheus = True;

      order( "PlayerSquad/SquadMate1", Clear, True );
      order( "PlayerSquad/SquadMate2", Clear, True );
      order( "PlayerSquad/SquadMate3", Clear, True );
      order( "PlayerSquad/SquadMate1", Attack, $cybrids );
      order( "PlayerSquad/SquadMate2", Attack, $cybrids );
      order( "PlayerSquad/SquadMate3", Attack, $cybrids );
      order( $caanon, Attack, $cybrids );
      order( $cybrids, Clear, True );

      setHercOwner( $playerId, $cybrid1 );
      setHercOwner( $playerId, $cybrid2 );
      setHercOwner( $playerId, $cybrid3 );
      setHercOwner( $playerId, $cybrid4 );
      setHercOwner( $playerId, $cybrid5 );
      setHercOwner( $playerId, $cybrid6 );
      setHercOwner( $playerId, $cybrid7 );
      setHercOwner( $playerId, $cybrid8 );
      setHercOwner( $playerId, $cybrid9 );
      setHercOwner( $playerId, $cybrid10 );
      setHercOwner( $playerId, $cybrid11 );
      setHercOwner( $playerId, $cybrid12 );
      setHercOwner( $playerId, $cybrid13 );
      setHercOwner( $playerId, $cybrid14 );
      setHercOwner( $playerId, $cybrid15 );
      setHercOwner( $playerId, $cybrid16 );

      // make sure our squadmates don't follow the player into the crater
      order( "PlayerSquad/squadMate1", guard, $Caanon );
      order( "PlayerSquad/squadMate2", guard, $Caanon );
      order( "PlayerSquad/squadMate3", guard, $Caanon );
   
      order( "PlayerSquad/squadMate1", holdPosition, True );
      order( "PlayerSquad/squadMate2", holdPosition, True );
      order( "PlayerSquad/squadMate3", holdPosition, True );

   }
}

function onArrivedPrometheus()
{
   if( $playerId.arrivedPrometheus != True )
   {
      $playerId.arrivedPrometheus = True;

      cameraLockFocus( true );
      setOrbitCamera( $hut, 100, 90.6, 0 );
      schedule( "say( 0, 1234, \"sfx_bigbeam_fire.wav\" );", 0.9 );
      schedule( "say( 0, 1235, \"sfx_avalanche.wav\" );", 5.9 );
      schedule( "playAnimSequence( $hut, 0, 1.0 );", 1 );
      schedule( "Say( 0, getObjectId( $prometheus ), *IDSTR_HE3_BAD01, \"HE3_BAD01.wav\" );", 5 );
      schedule( "setPlayerCamera();", 8 );
      schedule( "cameraLockFocus( false );", 10.1 );
      schedule( "order( $prometheus, shutdown, false );", 1 );
      schedule( "order( $prometheus, guard, $playerId );", 3.1 );
      schedule( "order( $prometheus, attack, $playerId );", 14.1 );
      schedule( "localNavIgnoreObject($bigZedShape);", 8.1 );
      schedule( "localNavIgnoreObject($hut);", 8.1 );
   }
}

// make sure player realizes that he can't win against the endless cybrid waves
function tellPlayerToGo()
{
   if( $playerId.nearedPrometheus != True )
   {
      $player.nearedPrometheus = True;

      // player no longer commands the squad
      removeSquadmateOrders();
   }
}

function tellPlayerToGo2()
{
   if( $playerId.nearedPrometheus != True )
   {
      Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA16 );
   }
}

function tellPlayerToGo3()
{
   if( $playerId.nearedPrometheus != True )
   {
      Say( 0, getObjectId( $caanon ), *IDSTR_HE3_CAA17 );
      schedule( "damageObject( getObjectId( $caanon ), 20000 );", 2 );
      schedule( "forceToDebrief();", 4 );
   }
}

function win()
{
   missionObjective1.status = *IDSTR_OBJ_COMPLETED;
   updatePlanetInventory(he3);
   schedule("forceToDebrief();", 3.0);
}

function orderSquadmatesToGuardCaanon()
{
   order( "PlayerSquad/squadMate1", guard, $Caanon );
   order( "PlayerSquad/squadMate2", guard, $Caanon );
   order( "PlayerSquad/squadMate3", guard, $Caanon );
}