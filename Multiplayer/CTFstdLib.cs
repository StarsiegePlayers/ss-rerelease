//
// ctfStdLib.cs 
//
// Simple capture the flag routines
//

// variables set by mission scripts
// $flagValue: how many points gained for capturing a flag
// $carrierValue: how many points gained for killing flag carrier
// $killPoints:   how many points gained for killing non-flag carrier
// $deathPoints:  how many points lost for getting killed
// $maxFlagCount: how many flags must be captured to win
// $flagTime: how much time does carrier have to return flag


// internal globals
// $teamFlagPoints:  how many points does a team get for capturing a flag
// $scoringFreeze: if true, no more points can be scored
// $<color>flagCarried: if true, flag has been grabbed
// $<color>flagCount: if 1, flag is at home
// $<color>carriersKilled: scorekeeping
// $<color>flagsCollected: scorekeeping
// $<color>navPoint: object id of each base's nav marker (if any)


// stuff that has to be in the mission
// four simGroups, one for each color:
// MissionGroup\\<color>Base, containing:
//    a staticShape called <color>Flag
//    an ESNavmarker called NavPoint (optional)
  	
//--------------------------------------------------------------------------------

// default values
$teamFlagPoints = 1;

function setRules()
{
   // compose a big "rich text" string of the rules to be displayed in the 
   // game info panel

   %rules = "<tIDMULT_CTF_GAMETYPE>"      @        
            "<tIDMULT_CTF_MAPNAME>"       @ 
            $missionName                  @  
            "<tIDMULT_CTF_OBJECTIVES>"    @
            "<tIDMULT_CTF_OBJECTIVES_2>"  @
            timeDifference($flagTime,0)   @
            "<tIDMULT_CTF_OBJECTIVES_3>"  @
            "<tIDMULT_CTF_SCORING_1>"     @
            "<tIDMULT_CTF_SCORING_2>"     @
            $flagValue                    @
            "<tIDMULT_CTF_SCORING_3>"     @
            "<tIDMULT_CTF_SCORING_4>"     @
            $carrierValue                 @
            "<tIDMULT_CTF_SCORING_5>"     @
            "<tIDMULT_CTF_SCORING_6>"     @
            $deathPoints                  @
            "<tIDMULT_CTF_SCORING_7>"     @
            "<tIDMULT_CTF_SCORING_8>"     @
            $killPoints                   @
            "<tIDMULT_CTF_SCORING_9>"     @
            "<tIDMULT_CTF_SCORING_10>"    @ 
            $maxFlagCount                 @
            "<tIDMULT_CTF_SCORING_11>"    @
            "<tIDMULT_STD_ITEMS>"         @
            "<tIDMULT_CTF_FLAGS>"         @
            "<tIDMULT_CTF_GLOW>"          @
            "<tIDMULT_STD_HEAL>"          @
            "<tIDMULT_STD_RELOAD_1>"      @
            $PadWaitTime                  @
            "<tIDMULT_STD_RELOAD_2>"      @
            "<tIDMULT_STD_ZEN_1>"         @
            $ZenWaitTime                  @
            "<tIDMULT_STD_ZEN_2>";
   
   setGameInfo(%rules);
}

// anything that redefines the rules needs to do this
// get the rules panel up and running
// this has to be called after the definition of setRules
setRules();


//--------------------------------------------------------------------------------

function onMissionStart()
{
	initGlobalVars();
}

function player::onAdd(%this)
{
   say(%this, 0, *IDMULT_CTF_WELCOME);
}

//--------------------------------------------------------------------------------
function vehicle::onAdd(%this)
{
	%team = getTeam(playerManager::vehicleIdToPlayerNum(%this));
	%color = teamToColor(%team);
	%flagKey = strcat(%color, "FlagCount");
	
   	adjTeamCount(%team, 1);
	
	//if the flag isn't at the base, but no one has it, correct situation
	if(dataRetrieve(0, %flagKey) && !dataRetrieve(0, strcat(%color, "FlagCarried")))
	{
		setFlag(%team, true);
	}
}

//--------------------------------------------------------------------------------
function vehicle::onDestroyed(%destroyed,%destroyer)
{
   %team = getTeam(%destroyed);
   adjTeamCount(%team, -1);
   %teamCount = getTeamPlayerCount(%team);      

   if(%teamCount < 1)
   {
	   setFlag(%team, false);					
   }

   %color = dataRetrieve(%destroyed, "hasFlag");
   %flagTeam = colorToTeam(%color);
   
   // if the destroyed vehicle has a flag, it goes back to it's base
   if (%color != "")
   {
      playerDropsFlag(%destroyed);
      
      // let everyone know this guy drops the flag
      %str =
         *IDMULT_CHAT_SURRENDER_FLAG_1 @
         getName( %destroyed )         @
         *IDMULT_CHAT_SURRENDER_FLAG_2 @ 
         %flagTeam                     @
         *IDMULT_CHAT_SURRENDER_FLAG_3;
      
      %soundFile = "";   
      if(%flagTeam == *IDSTR_TEAM_RED)
      {
         %soundFile = "red_flag_sur.wav";
      }
      else if(%flagTeam == *IDSTR_TEAM_BLUE)
      {
         %soundFile = "blue_flag_sur.wav";
      }
      else if(%flagTeam == *IDSTR_TEAM_YELLOW)
      {
         %soundFile = "yel_flag_sur.wav";
      }
      else
      {
         %soundFile = "purp_flag_sur.wav";
      }   
      
      say( 0, 0, %str, %soundFile );

            
      if($scoringFreeze == false)
      {     
         // destroyer's team gets credit for killing the carrier
         %destroyerTeam = getTeam(%destroyer);  
      
         // only give points if destroyed recovered his/her own flag
         if(%destroyerTeam == colorToTeam(%color))
         {
            %key = strcat(teamToColor(getTeam(%destroyer)), "CarriersKilled");
            dataStore(0, %key, 1 + dataRetrieve(0, %key));
            
            // player gets credit, too
            %player = playerManager::vehicleIdToPlayerNum(%destroyer);
            if(%player != 0)
            {
               %player.carriersKilled = %player.carriersKilled + 1;
            }
         }

         // echo the surrender to the console for logging
         echo(*IDSTR_CONSOLE_CTF_SURRENDER @ " " @ playerManager::vehicleIdToPlayerNum(%destroyed));                        
      }	  
   }
   else
   {
      // not a flag carrier ... is it an enemy?
      if($scoringFreeze == false)
      {
         if(getTeam(%destroyed) != getTeam(%destroyer))
         {
            %player = playerManager::vehicleIdToPlayerNum(%destroyer);
            if(%player != 0)
            {
               %player.genericKills = %player.genericKills + 1;
            }
         }
      }
   }
   
   // enforce the rules
   if(
      (getTeam(%destroyed) == getTeam(%destroyer)) &&
      (%destroyed != %destroyer)
   )
   {
      antiTeamKill(%destroyer);
   }

   vehicle::onDestroyedLog(%destroyed, %destroyer);
   
   // give the death messages...
   %message = getFancyDeathMessage(getHUDName(%destroyed), getHUDName(%destroyer));
   if(%message != "")
   {
      say( 0, 0, %message);
   }
}

//--------------------------------------------------------------------------------
function setFlag(%team, %bool)
{
   %color = teamToColor(%team);

	%flagCountKey = strcat(%color, "FlagCount");
	
	// set flag to visible
	if(%bool)
	{
      dataStore(0, %flagCountKey, 0);	
   	setShapeVisibility(getObjectId(strcat("MissionGroup\\", %color, "Base\\", %color, "Flag")), true);   		 	
	}
	
	// set flag to non-visible
	else
	{
		dataStore(0, %flagCountKey, 1);
		setShapeVisibility(getObjectId(strcat("MissionGroup\\", %color, "Base\\", %color, "Flag")),false);	
	}
}

function playerDropsFlag(%vehicle)
{
	// figure out which color of flag the guy is carrying
   %color = dataRetrieve(%vehicle, "hasFlag");
   %team = colorToTeam(%color);
   // the player is no longer carrying this or any flag
   dataRelease(%vehicle, "hasFlag");
   setVehicleSpecialIdentity(%vehicle, false);
   dataStore(0, strcat(%color, "FlagCarried"), 0);

   %teamPlayerCount = getTeamPlayerCount(%team);
   
   if(%teamPlayerCount < 1)
   {
      setFlag(%team, false);	
   }	
   else
   {	
   	setFlag(%team, true);   
   }
   
   // reset the timer for this person
   %vehicle.timer = 0;
   
   // turn off the hudtimer
   %player = playerManager::vehicleIdToPlayerNum(%vehicle);
   setHudTimer(0, 0, "", 1, %player);                  
}


//--------------------------------------------------------------------------------

function checkFlagRetrieved(%team, %vehicle)
{
   %color = teamToColor(%team);

   // you made it back to your own base, do you have any other teams' flags?
   if (dataRetrieve(%vehicle, "hasFlag") == "") 
      { return; }

   // is the player's team flag at his base when he brings back the enemy's flag?
   if (dataRetrieve(0, strcat(%color, "FlagCount")) != 0) 
      { return; }

   // player is no longer carrying the flag
   %hasColor = dataRetrieve(%vehicle, "hasFlag");
   %flagTeam = colorToTeam(%hasColor);
   playerDropsFlag(%vehicle);

   if($scoringFreeze == false)
   {
      // player's team gets credit for stealing the flag
      %collectedKey = strcat(teamToColor(getTeam(%vehicle)), "FlagsCollected");
      %collectedCount = dataRetrieve(0, %collectedKey);
      %collectedCount = %collectedCount + 1;
      dataStore(0, %collectedKey, %collectedCount);
      
      // player gets credit also
      %player = playerManager::vehicleIdToPlayerNum(%vehicle);
      %player.flagsCollected = %player.flagsCollected + 1;

      // echo the capture to the console for logging
      echo(*IDSTR_CONSOLE_CTF_CAPTURE @ " " @ playerManager::vehicleIdToPlayerNum(%vehicle));      	               

      %needMore = $maxFlagCount - %collectedCount;
      if (%needMore <= 0)
      {
         winEvent(getTeam(%vehicle));
      }
   }
  
   // let everyone know what happened
   %str =
      *IDMULT_CHAT_CAPTURE_FLAG_1 @
      getName( %vehicle ) @
      *IDMULT_CHAT_CAPTURE_FLAG_2 @ 
      colorToTeam(%hasColor) @
      *IDMULT_CHAT_CAPTURE_FLAG_3;
   
   %soundFile = "";
   if(%flagTeam == *IDSTR_TEAM_RED)
   {
      %soundFile = "red_flag_cap.wav";
   }
   else if(%flagTeam == *IDSTR_TEAM_BLUE)
   {
      %soundFile = "blue_flag_cap.wav";
   }
   else if(%flagTeam == *IDSTR_TEAM_YELLOW)
   {
      %soundFile = "yel_flag_cap.wav";
   }
   else
   {
      %soundFile = "purp_flag_cap.wav";
   }
   
   say( 0, 0, %str, %soundFile );      
}

//--------------------------------------------------------------------------------
function initGlobalVars()
{
   $scoringFreeze = false;
   
   %playerCount = playerManager::getPlayerCount();
	// clear all points for the players
   for (%p = 0; %p < %playerCount; %p++)
	{
		%player = playerManager::getPlayerNum(%p);
		%player.carriersKilled = 0;
		%player.flagsCollected = 0;
      %player.genericKills = 0;
   }
   

	dataStore(0, "blueFlagsCollected", 0);
	dataStore(0, "redFlagsCollected", 0);
	dataStore(0, "yellowFlagsCollected", 0);
	dataStore(0, "purpleFlagsCollected", 0);

	dataStore(0, "blueCarriersKilled", 0);
	dataStore(0, "redCarriersKilled", 0);
	dataStore(0, "yellowCarriersKilled", 0);
	dataStore(0, "purpleCarriersKilled", 0);

	dataStore(0, "blueFlagCount", 1);
	dataStore(0, "redFlagCount", 1);
	dataStore(0, "yellowFlagCount", 1);
	dataStore(0, "purpleFlagCount", 1);

	dataStore(0, "yellowFlagCarried", 0);
	dataStore(0, "blueFlagCarried", 0);
	dataStore(0, "redFlagCarried", 0);
	dataStore(0, "purpleFlagCarried", 0);

  	setShapeVisibility(getObjectId("MissionGroup\\yellowBase\\yellowFlag"), false);
  	setShapeVisibility(getObjectId("MissionGroup\\blueBase\\blueFlag"), false);
  	setShapeVisibility(getObjectId("MissionGroup\\redBase\\redFlag"), false);
  	setShapeVisibility(getObjectId("MissionGroup\\purpleBase\\purpleFlag"), false);
   
   $yellowNavPoint = getObjectId("MissionGroup\\yellowBase\\NavPoint");
   $blueNavPoint = getObjectId("MissionGroup\\blueBase\\NavPoint");
   $redNavPoint = getObjectId("MissionGroup\\redBase\\NavPoint");
   $purpleNavPoint = getObjectId("MissionGroup\\purpleBase\\NavPoint");
}

function winEvent(%team)
{
	%r = %g = %b = 0;

	if(%team == *IDSTR_TEAM_YELLOW)
	{
		%r = %g = 0.5;
	}
	
	if(%team == *IDSTR_TEAM_BLUE)
	{
		%b = 0.5;
	}
	
	if(%team == *IDSTR_TEAM_RED)
	{
		%r = 0.5;
	}
	
	if(%team == *IDSTR_TEAM_PURPLE)
	{
		%r = 0.345;
		%g = 0.1254;
		%b = 0.254;
	}

	// won decisively
   %winMessage =
      *IDMULT_CHAT_WON_GAME_1 @
      %team @
      *IDMULT_CHAT_WON_GAME_2;

   // tell everyone   
   messageBox(0, %winMessage);
   
   // schedule the end of the game
   %orderFade = "fadeEvent(0, \"out\", 5, " @ %r @ "," @ %g @ "," @ %b @ " );";
	schedule(%orderFade, 5);
	
	schedule("missionEndConditionMet();", 10);
   
   // freeze scoring
   $scoringFreeze = true;
}


function checkFlagStolen(%team, %vehicle)
{
   %color = teamToColor(%team);
   	
   // you just hit the trigger at an enemy base, is there a flag here?
   %flagCountKey = strcat(%color, "FlagCount");
   %flagCount = dataRetrieve(0, %flagCountKey);
   
   if (%flagCount != 0 || dataRetrieve(%vehicle, "hasFlag") != "") 
   {
      return;
   }

   if (playerManager::vehicleIdToPlayerNum(%vehicle) == 0)
   { 
      return;
   }

   // stop the flag shape from rendering
   setFlag(%team, false);

   // setup the vehicle to carry the flag
   setVehicleSpecialIdentity(%vehicle, true, %color);
   dataStore(%vehicle, "hasFlag", %color);

   // increment flag count so nobody else can pickup this flag
   dataStore(0, %flagCountKey, 1);

   // set global bool for flag of that color
   dataStore(0, strcat(%color, "FlagCarried"), 1); 

   // let everyone know what happened
   %str =
         *IDMULT_CHAT_STEAL_FLAG_1 @
         getName( %vehicle ) @
         *IDMULT_CHAT_STEAL_FLAG_2 @ 
         colorToTeam(%color) @
         *IDMULT_CHAT_STEAL_FLAG_3;
   
   %soundFile = "";
   if(%team == *IDSTR_TEAM_RED)
   {
      %soundFile = "red_flag_stol.wav";
   }
   else if(%team == *IDSTR_TEAM_BLUE)
   {
      %soundFile = "blue_flag_stol.wav";
   }
   else if(%team == *IDSTR_TEAM_YELLOW)
   {
      %soundFile = "yel_flag_stol.wav";
   }
   else
   {
      %soundFile = "purp_flag_stol.wav";
   }

   say( 0, 0, %str, %soundFile );

   // echo the steal to the console for logging
   echo(*IDSTR_CONSOLE_CTF_STEAL @ " " @ playerManager::vehicleIdToPlayerNum(%vehicle));      	                     

   if($flagTime != 0)
   {      
      // start a hud timer on the flag carrier
      %player = playerManager::vehicleIdToPlayerNum(%vehicle);
      
      %vehicle.timer = schedule("carrierDeath(" @ %vehicle @ ");", $flagTime);
      
      // here we should set the hudtimer
      if(%team == *IDSTR_TEAM_YELLOW)
      {
         %title = *IDMULT_CHAT_YELLOW_FLAG;
      }
      else if(%team == *IDSTR_TEAM_BLUE)
      {
         %title = *IDMULT_CHAT_BLUE_FLAG;
      }
      else if(%team == *IDSTR_TEAM_RED)
      {
         %title = *IDMULT_CHAT_RED_FLAG;
      }
      else if(%team == *IDSTR_TEAM_PURPLE)
      {
         %title = *IDMULT_CHAT_PURPLE_FLAG;
      } 
      
      setHudTimer($flagTime, -1, %title, 1, %player);      
   }
   
   // tell the vehicle where his or her base is
   %playerTeam = getTeam(%vehicle);
   
   if(%playerTeam == *IDSTR_TEAM_YELLOW)
   {
      if($yellowNavPoint != 0)
         setNavMarker($yellowNavPoint, true, %vehicle);
   } else if(%playerTeam == *IDSTR_TEAM_BLUE)
   {
      if($blueNavPoint != 0)
         setNavMarker($blueNavPoint, true, %vehicle);
   } else if(%playerTeam == *IDSTR_TEAM_RED)
   {
      if($redNavPoint != 0)
         setNavMarker($redNavPoint, true, %vehicle);
   } else if(%playerTeam == *IDSTR_TEAM_PURPLE)
   {
      if($purpleNavPoint != 0)
         setNavMarker($purpleNavPoint, true, %vehicle);
   }
}

//-------------------------------------------------------------------------------
function carrierDeath(%vehicle)
{
   // see if we really should kill this poor person
   
   // see if timer has been reset
   if(%vehicle.timer == 0)
      return;

   // see if this is the timer we're looking for
   if(%vehicle.timer <= getCurrentTime())
   {
      // that whole killing thing was just a joke
      // player drops the flag they are carrying
      
      %color = dataRetrieve(%vehicle, "hasFlag");
      if(%color != "")
      {      
         playerDropsFlag(%vehicle);
         %flagTeam = colorToTeam(%color);
      
         // let everyone know this guy drops the flag
         %str =
            *IDMULT_CHAT_SURRENDER_FLAG_1 @
            getName( %vehicle )           @
            *IDMULT_CHAT_SURRENDER_FLAG_2 @ 
            %flagTeam                     @
            *IDMULT_CHAT_SURRENDER_FLAG_3;
         
         %soundFile = "";   
         if(%flagTeam == *IDSTR_TEAM_RED)
         {
            %soundFile = "red_flag_sur.wav";
         }
         else if(%flagTeam == *IDSTR_TEAM_BLUE)
         {
            %soundFile = "blue_flag_sur.wav";
         }
         else if(%flagTeam == *IDSTR_TEAM_YELLOW)
         {
            %soundFile = "yel_flag_sur.wav";
         }
         else
         {
            %soundFile = "purp_flag_sur.wav";
         }   
         
         say( 0, 0, %str, %soundFile );
         
         
         if($scoringFreeze == false)
         {     
            // echo the surrender to the console for logging
            echo(*IDSTR_CONSOLE_CTF_SURRENDER @ " " @ playerManager::vehicleIdToPlayerNum(%vehicle));                        
         }
      }
   }   
}


//-------------------------------------------------------------------------------
function colorToTeam(%color)
{
	if(%color == "yellow")
	{return *IDSTR_TEAM_YELLOW;}
	if(%color == "blue")
	{return *IDSTR_TEAM_BLUE;}
	if(%color == "red")
	{return *IDSTR_TEAM_RED;}
	if(%color == "purple")
	{return *IDSTR_TEAM_PURPLE;}

	return 0;
}
function teamToColor(%team)
{
	if(%team == *IDSTR_TEAM_YELLOW)
	{return "yellow";}
	if(%team == *IDSTR_TEAM_BLUE)
	{return "blue";}
	if(%team == *IDSTR_TEAM_RED)
	{return "red";}
	if(%team == *IDSTR_TEAM_PURPLE)
	{return "purple";}

	return 0;
}   

//--------------------------------------------------------------------------------
function getTeamPlayerCount(%team)
{
	%key = strcat(%team, "Count");
	return dataRetrieve(0, %key);	
}

function adjTeamCount(%team, %amount)
{
	%key = strcat(%team, "Count");
	dataStore(0, %key, dataRetrieve(0, %key) + %amount);	
}

//--------------------------------------------------------------------------------
function initFlagTrigger(%team)
{
   %color = teamToColor(%team);

   %flagCountKey = strcat(%color, "FlagCount");
   dataStore(0, %flagCountKey, 1);
   %flagsCollectedKey = strcat(%color, "FlagsCollected");
   dataStore(0, %flagsCollected, 0);
   %carriersKilledKey = strcat(%color, "CarriersKilled");
   dataStore(0, %carriersKilledKey, 0);
}

//--------------------------------------------------------------------------------
function flagTriggerOnEnter(%team, %object)
{
   if (getTeam(%object) != %team) {
      checkFlagStolen(%team, %object);
   }
   else {
      checkFlagRetrieved(%team, %object);          
   }
}

//--------------------------------------------------------------------------------

// Team color specific code

// Yellow
function yellowFlagTrigger::trigger::onAdd(%this)
{
   initFlagTrigger(*IDSTR_TEAM_YELLOW);
}
function yellowFlagTrigger::trigger::onEnter(%this, %object)
{
   flagTriggerOnEnter(*IDSTR_TEAM_YELLOW, %object);
}

// Blue
function blueFlagTrigger::trigger::onAdd(%this)
{
   initFlagTrigger(*IDSTR_TEAM_BLUE);
}
function blueFlagTrigger::trigger::onEnter(%this, %object)
{
   flagTriggerOnEnter(*IDSTR_TEAM_BLUE, %object);
}

// Red
function redFlagTrigger::trigger::onAdd(%this)
{
   initFlagTrigger(*IDSTR_TEAM_RED);
}
function redFlagTrigger::trigger::onEnter(%this, %object)
{
   flagTriggerOnEnter(*IDSTR_TEAM_RED, %object);
}

// Purple
function purpleFlagTrigger::trigger::onAdd(%this)
{
   initFlagTrigger(*IDSTR_TEAM_PURPLE);
}
function purpleFlagTrigger::trigger::onEnter(%this, %object)
{
   flagTriggerOnEnter(*IDSTR_TEAM_PURPLE, %object);
}

//--------------------------------------------------------------------------------

//--------------------------------------------------------------------------------

// Scoreboard code

function getTeamCarriersKilled(%a)
{
   %key = strcat(teamToColor(getTeamNameFromTeamId(%a)), "CarriersKilled");
   return dataRetrieve(0, %key);
}

function getPlayerCarriersKilled(%a)
{
   %killed = %a.carriersKilled;
   if (%killed == "") {
      return 0;
   }
   else {
      return %killed;
   }
}

function getPlayerGenericKills(%a)
{
   %killed = %a.genericKills;
   if (%killed == "") {
      return 0;
   }
   else {
      return %killed;
   }
}

function getTeamFlags(%a)
{
   %collectedKey = strcat(teamToColor(getTeamNameFromTeamId(%a)), "FlagsCollected");
   %flags = dataRetrieve(0, %collectedKey);
   if (%flags == "") {
      return 0;
   }
   else {
      return %flags;
   }
}

function getPlayerFlags(%a)
{
   %flags = %a.flagsCollected;
   if (%flags == "") {
      return 0;
   }
   else {
      return %flags;
   }
}   

function getPlayerScore(%a)
{
   return(
      (getPlayerFlags(%a) * $flagValue) + 
      (getPlayerCarriersKilled(%a) * $carrierValue) +
      (getPlayerGenericKills(%a) * $killPoints) -
      (getDeaths(%a) * $deathPoints)
   );
}

function getTeamScore(%a)
{
   return(getTeamFlags(%a) * $teamFlagPoints);
}

function initScoreBoard()
{
   deleteVariables("$ScoreBoard::PlayerColumn*");
   deleteVariables("$ScoreBoard::TeamColumn*");

   // Player ScoreBoard column headings
   $ScoreBoard::PlayerColumnHeader1 = *IDMULT_SCORE_TEAM;
   $ScoreBoard::PlayerColumnHeader2 = *IDMULT_SCORE_SCORE;
   $ScoreBoard::PlayerColumnHeader3 = *IDMULT_SCORE_FLAGS;
   $ScoreBoard::PlayerColumnHeader4 = *IDMULT_SCORE_CARRIER_KILLS;
   $ScoreBoard::PlayerColumnHeader5 = *IDMULT_SCORE_KILLS;
   $ScoreBoard::PlayerColumnHeader6 = *IDMULT_SCORE_DEATHS;   

   // Player ScoreBoard column functions
   $ScoreBoard::PlayerColumnFunction1 = "getTeam";
   $ScoreBoard::PlayerColumnFunction2 = "getPlayerScore";
   $ScoreBoard::PlayerColumnFunction3 = "getPlayerFlags";
   $ScoreBoard::PlayerColumnFunction4 = "getPlayerCarriersKilled";
   $ScoreBoard::PlayerColumnFunction5 = "getPlayerGenericKills";
   $ScoreBoard::PlayerColumnFunction6 = "getDeaths";


   //Team ScoreBoard column headings
   $ScoreBoard::TeamColumnHeader1 = *IDMULT_SCORE_SCORE;
   $ScoreBoard::TeamColumnHeader2 = *IDMULT_SCORE_PLAYERS;
   $ScoreBoard::TeamColumnHeader3 = *IDMULT_SCORE_FLAGS;
    
   // Team ScoreBoard column functions
   $ScoreBoard::TeamColumnFunction1 = "getTeamScore";
   $ScoreBoard::TeamColumnFunction2 = "getNumberOfPlayersOnTeam";
   $ScoreBoard::TeamColumnFunction3 = "getTeamFlags";

   
   // tell server to process all the scoreboard definitions defined above
   serverInitScoreBoard();   
}

//--------------------------------------------------------------------------------



