// WAR, war, what is it good for? -- TSL

$missionName = "WAR_Martian_Standoff";

exec("multiplayerStdLib.cs");

///////////////////////////////////////////////////////////////////////////////////////////////////
// Lots O' Globals
///////////////////////////////////////////////////////////////////////////////////////////////////
$redKills = 0;
$yellowKills = 0;

$redBuildingsDestroyed = 0;
$yellowBuildingsDestroyed = 0;

$yellowHerc1RespawnTime = 0;
$yellowHerc2RespawnTime = 0;
$yellowHerc3RespawnTime = 0;
$redHerc1RespawnTime = 0;
$redHerc2RespawnTime = 0;
$redHerc3RespawnTime = 0;

$yellowHerc1Path = "MissionGroup/yellowHerc1Path";
$yellowHerc2Path = "MissionGroup/yellowHerc2Path";
$yellowHerc3Path = "MissionGroup/yellowHerc3Path";
$redHerc1Path = "MissionGroup/redHerc1Path";
$redHerc2Path = "MissionGroup/redHerc2Path";
$redHerc3Path = "MissionGroup/redHerc3Path";

$yellowHqDestroyed = false;
$redHqDestroyed = false;

///////////////////////////////////////////////////////////////////////////////////////////////////
// the Game Info tab uses these variables in the rules
///////////////////////////////////////////////////////////////////////////////////////////////////
$respawnDelay = 60;
$respawnDelayNoHq = 120;
$BUILDINGS_TO_DESTROY = 6;

///////////////////////////////////////////////////////////////////////////////////////////////////
// our base defender attributes
//
// The jump from 0.9 to 1.0 is a big one. At skill 1.0 and accuracy 1.0 the AI units will aim 
// first for the weapons, then the legs. A little too much for this mission.
//
///////////////////////////////////////////////////////////////////////////////////////////////////
Pilot YellowPilot
{
   id = 28;
   
   name = *IDMULT_WAR_YELLOW_DEFENDER;
   
   skill = 0.9;
   accuracy = 0.9;
   aggressiveness = 1.0;
   activateDist = 850.0;
   deactivateBuff = 300.0;
   targetFreq = 2.0;
   trackFreq = 0.0;
   fireFreq = 0.2;
   LOSFreq = 0.2;
   orderFreq = 2.0;
};

Pilot RedPilot
{
   id = 29;
   
   name = *IDMULT_WAR_RED_DEFENDER;
   
   skill = 0.9;
   accuracy = 0.9;
   aggressiveness = 1.0;
   activateDist = 850.0;
   deactivateBuff = 300.0;
   targetFreq = 2.0;
   trackFreq = 0.0;
   fireFreq = 0.2;
   LOSFreq = 0.2;
   orderFreq = 2.0;
};

function initGlobalVars()
{
   $scoringFreeze = false;
   
   %playerCount = playerManager::getPlayerCount();
	// clear all points for the players
   for (%p = 0; %p < %playerCount; %p++)
	{
		%player = playerManager::getPlayerNum(%p);
      %player.numKills = 0;
      %player.buildingsDestroyed = 0;
   }
}

function setDefaultMissionOptions()
{
	$server::TeamPlay = True;
	$server::AllowDeathmatch = False;
	$server::AllowTeamPlay = True;	
	
	$server::AllowTeamRed = true;
	$server::AllowTeamBlue = false;
	$server::AllowTeamYellow = true;
	$server::AllowTeamPurple = false;

   $server::disableTeamRed = false;
   $server::disableTeamBlue = true;
   $server::disableTeamYellow = false;
   $server::disableTeamPurple = true;
}

function setDefaultMissionItems()
{
   allowComponent(                                        830, FALSE  );      //Chameleon
   allowComponent(                                        831, FALSE  );      //Cuttlefish cloak
   allowComponent(                                        840, FALSE  );      //Shield Modulator
   allowComponent(                                        914, FALSE  );      //UAP
}

function onMissionLoad()
{
   cdAudioCycle("Purge", "Watching", "Mechsoul"); 

   // get the original ID for each AI Herc ( for use later when we clone them )
   $yellowHerc1 = getObjectId( "MissionGroup/yellowHercs/h1" );
   $yellowHerc2 = getObjectId( "MissionGroup/yellowHercs/h2" );
   $yellowHerc3 = getObjectId( "MissionGroup/yellowHercs/h3" );
   
   $redHerc1 = getObjectId( "MissionGroup/redHercs/h1" );
   $redHerc2 = getObjectId( "MissionGroup/redHercs/h2" );
   $redHerc3 = getObjectId( "MissionGroup/redHercs/h3" );

   %rules = "<tIDMULT_WAR_GAMETYPE>"      @        
            "<tIDMULT_WAR_MAPNAME>"       @ 
            $missionName                  @  
            "<tIDMULT_WAR_OBJECTIVES_1>"  @
            timeDifference($respawnDelay, 0)       @
            "<tIDMULT_WAR_OBJECTIVES_2>"  @
            timeDifference($respawnDelayNoHq, 0)   @
            "<tIDMULT_WAR_OBJECTIVES_3>"  @
            "<tIDMULT_WAR_OBJECTIVES_4>"  @
            $BUILDINGS_TO_DESTROY         @
            "<tIDMULT_WAR_OBJECTIVES_5>"  @
            "<tIDMULT_WAR_SCORING_1>"     @
            "<tIDMULT_WAR_SCORING_2>"     @
            "<tIDMULT_STD_ITEMS>"         @
            "<tIDMULT_WAR_HQ>"            @
            "<tIDMULT_WAR_GENERATORS>"    @
            "<tIDMULT_WAR_TURRETS>"       @
            "<tIDMULT_WAR_FLAGS>"         @
            "<tIDMULT_WAR_GLOW>"          @
            "<tIDMULT_STD_HEAL>"          @
            "<tIDMULT_STD_RELOAD_1>"      @
            $PadWaitTime                  @
            "<tIDMULT_STD_RELOAD_2>";
   
   setGameInfo(%rules);
}


//-------------------------------------------------------------
function player::onAdd(%this)
{
   say(%this, 0, *IDMULT_WAR_WELCOME);
}

function vehicle::onAdd(%this)
{
   // see if it is a player
   %player = playerManager::vehicleIdToPlayerNum(%this);
   if(%player == 0) 
      return;

   schedule( "setEnemyNavPoint(" @ %this @ ");", 1 );
}

function setEnemyNavPoint( %this )
{
   if( getTeam(%this) == *IDSTR_TEAM_YELLOW )
   {
      setNavMarker( "MissionGroup/RedBase/n1", true, %this );
   }
   else
   {
      setNavMarker( "MissionGroup/YellowBase/n1", true, %this );
   }
}


function vehicle::onDestroyed( %this, %destroyer )
{
   if( getTeam( %this ) == *IDSTR_TEAM_RED )
   {
      $yellowKills++;
   }
   else
   {
      $redKills++;
   }

   // award the player a kill ( if the enemy is a different color )
   if( getTeam( %destroyer ) != getTeam( %this ) )
   {
      %player = playerManager::vehicleIdToPlayerNum( %destroyer );
      if(%player != 0)
      {
         %player.numKills++;      
      }
   }
   
   //----------------------------------------------------------------
   // If any of our AI hercs die, give a message and re-clone/drop them
   //----------------------------------------------------------------
   if( %this == $yellowHerc1 )
   {
      $yellowHercsDestroyed++;
      
      if( $yellowHqDestroyed == false )
      {
         schedule( "trevorsCloneVehicle(\"$yellowHerc1\", " @ %this @ ", -320, -385, 1312);", $respawnDelay);
      }
      else
      {
         schedule( "trevorsCloneVehicle(\"$yellowHerc1\", " @ %this @ ", -320, -385, 1312);", $respawnDelayNoHq);
      }
   }
   if( %this == $yellowHerc2 )
   {
      $yellowHercsDestroyed++;

      if( $yellowHqDestroyed == false )
      {
         schedule( "trevorsCloneVehicle(\"$yellowHerc2\", " @ %this @ ", -120, -15, 1328);", $respawnDelay);
      }
      else
      {
         schedule( "trevorsCloneVehicle(\"$yellowHerc2\", " @ %this @ ", -120, -15, 1328);", $respawnDelayNoHq);
      }
   }
   if( %this == $yellowHerc3 )
   {
      $yellowHercsDestroyed++;
      
      if( $yellowHqDestroyed == false )
      {
         schedule( "trevorsCloneVehicle(\"$yellowHerc3\", " @ %this @ ", -63, -612, 1312);", $respawnDelay);
      }
      else
      {
         schedule( "trevorsCloneVehicle(\"$yellowHerc3\", " @ %this @ ", -63, -612, 1312);", $respawnDelayNoHq);
      }
   }

   if( %this == $redHerc1 )
   {
      $redHercsDestroyed++;
      
      if( $redHqDestroyed == false )
      {
         schedule( "trevorsCloneVehicle(\"$redHerc1\", " @ %this @ ", -2318, -38, 1478);", $respawnDelay);
      }
      else
      {
         schedule( "trevorsCloneVehicle(\"$redHerc1\", " @ %this @ ", -2318, -38, 1478);", $respawnDelayNoHq);
      }
   }
   if( %this == $redHerc2 )
   {
      $redHercsDestroyed++;
      
      if( $redHqDestroyed == false )
      {
         schedule( "trevorsCloneVehicle(\"$redHerc2\", " @ %this @ ", -2654, -249, 1431);", $respawnDelay);
      }
      else
      {
         schedule( "trevorsCloneVehicle(\"$redHerc2\", " @ %this @ ", -2654, -249, 1431);", $respawnDelayNoHq);
      }
   }
   if( %this == $redHerc3 )
   {
      $redHercsDestroyed++;
      
      if( $redHqDestroyed == false )
      {
         schedule( "trevorsCloneVehicle(\"$redHerc3\", " @ %this @ ", -2514, -253, 1438);", $respawnDelay);
      }
      else
      {
         schedule( "trevorsCloneVehicle(\"$redHerc3\", " @ %this @ ", -2514, -253, 1438);", $respawnDelayNoHq);
      }
   }

   // left over from missionStdLib.cs
   vehicle::onDestroyedLog(%this, %destroyer);
   
   // give the death messages...
   %message = getFancyDeathMessage(getHUDName(%this), getHUDName(%destroyer));
   if(%message != "")
   {
      say( 0, 0, %message);
   }

   // rules enforcement
   if(
      (getTeam(%this) == getTeam(%destroyer)) &&
      (%this != %destroyer)
   )
   {
      antiTeamKill(%destroyer);
   }
}

function initScoreBoard()
{
   deleteVariables("$ScoreBoard::PlayerColumn*");
   deleteVariables("$ScoreBoard::TeamColumn*");

	$ScoreBoard::PlayerColumnHeader1 = "Team";
	$ScoreBoard::PlayerColumnHeader2 = "Kills";
	$ScoreBoard::PlayerColumnHeader3 = "Buildings Destroyed";

	$ScoreBoard::PlayerColumnFunction1 = "getTeam2";
	$ScoreBoard::PlayerColumnFunction2 = "getKills";
	$ScoreBoard::PlayerColumnFunction3 = "getBuildingsDestroyed";

   $ScoreBoard::TeamColumnHeader1 = "Structures Remaining";
   $ScoreBoard::TeamColumnHeader2 = "Total Kills";
   $ScoreBoard::TeamColumnFunction1 = "getRemainingStructures";
   $ScoreBoard::TeamColumnFunction2 = "getTeamKills";
 
   serverInitScoreBoard();
}

function getTeam2(%player)
{
   if( getTeam( %player ) == *IDSTR_TEAM_YELLOW )
   {
      return *IDMULT_YELLOW;
   }
   else
   {
      return *IDMULT_RED;
   }
}

function getKills(%player)
{
   if( %player.numKills != "")
      return %player.numKills;
      
   return "0";
}

function getTeamKills(%team)
{
   if( getTeamNameFromTeamId(%team) == *IDSTR_TEAM_YELLOW )
   {
      return $yellowKills;
   }
   else
   {
      return $redKills;
   }   
}

function getRemainingStructures(%team)
{
   if( getTeamNameFromTeamId(%team) == *IDSTR_TEAM_YELLOW )
   {
      return $BUILDINGS_TO_DESTROY - $yellowBuildingsDestroyed;
   }
   else
   {
      return $BUILDINGS_TO_DESTROY - $redBuildingsDestroyed;
   }   
}

function getBuildingsDestroyed(%player)
{
   if( %player.buildingsDestroyed != "")
      return %player.buildingsDestroyed;
   
   return "0";
}

function redGen1::structure::onDestroyed( %this, %who )
{
  $redBuildingsDestroyed++;
  checkForWin();

  order( "MissionGroup/redBase/t1", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;
  }

  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_RED_BR_GEN_DESTROYED;
  say(IDSTR_TEAM_YELLOW, 1234, %txt, "ene_struct_dest.wav");    


  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_DESTROYED_RED_STRUCTURE;
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
}

function redGen2::structure::onDestroyed( %this, %who )
{
  $redBuildingsDestroyed++;
  checkForWin();

  order( "MissionGroup/redBase/t2", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_RED_FR_GEN_DESTROYED;
  say(IDSTR_TEAM_YELLOW, 1234, %txt, "ene_struct_dest.wav");    


  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_DESTROYED_RED_STRUCTURE;
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
}

function redGen3::structure::onDestroyed( %this, %who )
{
  $redBuildingsDestroyed++;
  checkForWin();
 
  order( "MissionGroup/redBase/t3", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_RED_CEN_GEN_DESTROYED;
  say(IDSTR_TEAM_YELLOW, 1234, %txt, "ene_struct_dest.wav");    


  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_DESTROYED_RED_STRUCTURE;
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
}

function redGen4::structure::onDestroyed( %this, %who )
{
  $redBuildingsDestroyed++;
  checkForWin();

  order( "MissionGroup/redBase/t4", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_RED_BL_GEN_DESTROYED;
  say(IDSTR_TEAM_YELLOW, 1234, %txt, "ene_struct_dest.wav");    


  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_DESTROYED_RED_STRUCTURE;
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
}

function redGen5::structure::onDestroyed( %this, %who )
{
  $redBuildingsDestroyed++;
  checkForWin();

  order( "MissionGroup/redBase/t5", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_RED_FL_GEN_DESTROYED;
  say(IDSTR_TEAM_YELLOW, 1234, %txt, "ene_struct_dest.wav");    


  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_DESTROYED_RED_STRUCTURE;
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
}

function yellowGen1::structure::onDestroyed( %this, %who )
{
  $yellowBuildingsDestroyed++;
  checkForWin();

  order( "MissionGroup/yellowBase/t1", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_YELLOW_FR_GEN_DESTROYED;
  say(IDSTR_TEAM_RED, 1234, %txt, "ene_struct_dest.wav");    


  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_DESTROYED_YELLOW_STRUCTURE;
  say(IDSTR_TEAM_YELLOW, 1234, %txt, "friend_struct_des.wav");    
}

function yellowGen2::structure::onDestroyed( %this, %who )
{
  $yellowBuildingsDestroyed++;
  checkForWin();

  order( "MissionGroup/yellowBase/t2", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_YELLOW_BR_GEN_DESTROYED;
  say(IDSTR_TEAM_RED, 1234, %txt, "ene_struct_dest.wav");    


  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_DESTROYED_YELLOW_STRUCTURE;
  say(IDSTR_TEAM_YELLOW, 1234, %txt, "friend_struct_des.wav");    
}

function yellowGen3::structure::onDestroyed( %this, %who )
{
  $yellowBuildingsDestroyed++;
  checkForWin();

  order( "MissionGroup/yellowBase/t3", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_YELLOW_CEN_GEN_DESTROYED;
  say(IDSTR_TEAM_RED, 1234, %txt, "ene_struct_dest.wav");    


  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_DESTROYED_YELLOW_STRUCTURE;
  say(IDSTR_TEAM_YELLOW, 1234, %txt, "friend_struct_des.wav");    
}

function yellowGen4::structure::onDestroyed( %this, %who )
{
  $yellowBuildingsDestroyed++;
  checkForWin();

  order( "MissionGroup/yellowBase/t4", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_YELLOW_BL_GEN_DESTROYED;
  say(IDSTR_TEAM_RED, 1234, %txt, "ene_struct_dest.wav");    


  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_DESTROYED_YELLOW_STRUCTURE;
  say(IDSTR_TEAM_YELLOW, 1234, %txt, "friend_struct_des.wav");    
}

function yellowGen5::structure::onDestroyed( %this, %who )
{
  $yellowBuildingsDestroyed++;
  checkForWin();

  order( "MissionGroup/yellowBase/t5", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_YELLOW_BL_GEN_DESTROYED;
  say(IDSTR_TEAM_RED, 1234, %txt, "ene_struct_dest.wav");    


  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_DESTROYED_YELLOW_STRUCTURE;
  say(IDSTR_TEAM_YELLOW, 1234, %txt, "friend_struct_des.wav");    
}

function redHQ::structure::onDestroyed( %this, %who )
{
  $redBuildingsDestroyed++;
  checkForWin();
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  $redHqDestroyed = true;

  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_DESTROYED_RED_STRUCTURE;
  say(IDSTR_TEAM_YELLOW, 1234, %txt, "ene_struct_dest.wav");    
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
  say(0, 1234, "<F5>" @ *IDMULT_WAR_RED_HQ_DESTROYED @ timeDifference($respawnDelayNoHq, 0) @ "." );
}


function yellowHQ::structure::onDestroyed( %this, %who )
{
  $yellowBuildingsDestroyed++;
  checkForWin();
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }
  
  $yellowHqDestroyed = true;

  %txt = "<F5>" @ getHUDName(%who) @ *IDMULT_WAR_DESTROYED_YELLOW_STRUCTURE;
  say(IDSTR_TEAM_YELLOW, 1234, %txt, "friend_struct_des.wav");    

  say(IDSTR_TEAM_RED, 1234, %txt, "ene_struct_dest.wav");    
  say(0, 1234, "<F5>" @ *IDMULT_WAR_YELLOW_HQ_DESTROYED @ timeDifference($respawnDelayNoHq, 0) @ "." );
}


function onMissionStart()
{
	$healRate = 100;   
	$ammoRate = 3;
 	$padWaitTime = 45;

   windSounds();
	marsSounds();	

   initGlobalVars();
   
   order( $yellowHerc1, guard, $yellowHerc1Path );
   order( $yellowHerc2, guard, $yellowHerc2Path );
   order( $yellowHerc3, guard, $yellowHerc3Path );
   order( $redHerc1, guard, $redHerc1Path );
   order( $redHerc2, guard, $redHerc2Path );
   order( $redHerc3, guard, $redHerc3Path );
}

function checkForWin()
{
   if( $yellowBuildingsDestroyed >= $BUILDINGS_TO_DESTROY )
   {
      fadeEvent( 0, out, 1.5, 1.0, 0, 0 ); 
      schedule( "missionEndConditionMet();", 1.6 );
      
      %txt = "Red team wins!";
      messageBox(0, %txt);
   }
   else if( $redBuildingsDestroyed >= $BUILDINGS_TO_DESTROY )
   {
      fadeEvent( 0, out, 1.5, 1.0, 1.0, 0 ); 
      schedule( "missionEndConditionMet();", 1.6 );
      
      %txt = "Yellow team wins!";
      messageBox(0, %txt);
   }   
}


// Healing Pad Functionality
//------------------------------------------------------------------------------
function ZenHeal::trigger::onEnter(%this, %object)
{
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_HEALPAD, true, true);  
}
function ZenHeal::trigger::onContact(%this, %object)
{
   	Zen::work(%this, %object, $healRate, 0, $padWaitTime, true); 
}

// Ammo Pad Functionality
//------------------------------------------------------------------------------
function ZenAmmo::trigger::onEnter(%this, %object)
{
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_AMMOPAD, true, true);  
}

function ZenAmmo::trigger::onContact(%this, %object)
{
   	Zen::work(%this, %object, 0, $ammoRate, $padWaitTime, true); 
}
 
function trevorsCloneVehicle(%globalVarName, %old, %x, %y, %z)
{
   %clone = cloneVehicle(%old);
   setPosition(%clone, %x, %y, %z);

   schedule( "deleteObject(" @ %old @ ");", $respawnDealNoHq + 10 );

   schedule( %globalVarName @ " = " @ %clone @ ";", 0);

   addToSet( "MissionGroup", %clone );

   %path = %globalVarName @ "Path";
   schedule( "order( " @ %clone @ ", guard, " @ %path @ " );", 1 );
}

