//------------------------------------------------------------------------------
//
// DeathMatch standard library
//

//--------------------------------------
// Rules

// internal globals
// $killPoints: how many points scored for a kill
// $deathPoints: how many points LOST for dying

$killPoints = 3;
$deathPoints = 2;

function setRules()
{
   if($server::TeamPlay == true)
	{
      %rules = "<tIDMULT_TDM_GAMETYPE>"   @        
               "<tIDMULT_TDM_MAPNAME>"    @ 
               $missionName               @
               "<tIDMULT_TDM_OBJECTIVES>" @
               "<tIDMULT_TDM_SCORING_1>"  @
               "<tIDMULT_TDM_SCORING_2>"  @
               $killPoints                @
               "<tIDMULT_TDM_SCORING_3>"  @
               "<tIDMULT_TDM_SCORING_4>"  @
               $deathPoints               @
               "<tIDMULT_TDM_SCORING_5>"  @
               "<tIDMULT_TDM_SCORING_6>"  @
               "<tIDMULT_STD_ITEMS>"      @
               "<tIDMULT_STD_HEAL>"       @
               "<tIDMULT_STD_RELOAD_1>"   @
               $PadWaitTime               @
               "<tIDMULT_STD_RELOAD_2>"   @
               "<tIDMULT_STD_ZEN_1>"      @
               $ZenWaitTime               @
               "<tIDMULT_STD_ZEN_2>";
   }
	else
	{
      %rules = "<tIDMULT_DM_GAMETYPE>"    @        
               "<tIDMULT_DM_MAPNAME>"     @ 
               $missionName               @
               "<tIDMULT_DM_OBJECTIVES>"  @
               "<tIDMULT_DM_SCORING_1>"   @
               "<tIDMULT_DM_SCORING_2>"   @
               $killPoints                @
               "<tIDMULT_DM_SCORING_3>"   @
               "<tIDMULT_DM_SCORING_4>"   @
               $deathPoints               @
               "<tIDMULT_DM_SCORING_5>"   @
               "<tIDMULT_STD_ITEMS>"      @
               "<tIDMULT_STD_HEAL>"       @
               "<tIDMULT_STD_RELOAD_1>"   @
               $PadWaitTime               @
               "<tIDMULT_STD_RELOAD_2>"   @
               "<tIDMULT_STD_ZEN_1>"      @
               $ZenWaitTime               @
               "<tIDMULT_STD_ZEN_2>";
                  
   }
   setGameInfo(%rules);      
}

// setup the rules
// this has to be called after the definition of setRules
setRules();
                
function player::onAdd(%this)
{
   player::onAddLog(%this);
	
	if($server::TeamPlay == true)
	{
      say(%this,0, *IDMULT_TDM_WELCOME);	      
   }
	else
	{
      say(%this,0, *IDMULT_DM_WELCOME);
   }
}   

function vehicle::onAdd(%this)
{
   if($server::TeamPlay != true)
   {
   	// see if it is a player
      %player = playerManager::vehicleIdToPlayerNum(%this);
      if(%player == 0)  // that's the server
         return;
         
      // so it's a player
      
      // shouldn't have to do this, but oh well
      setTeam(%this, *IDSTR_TEAM_RED);
   }
}

//--------------------------------------
// Death Messages

function vehicle::onDestroyed(%destroyed, %destroyer)
{
   // left over from missionStdLib.cs
   vehicle::onDestroyedLog(%destroyed, %destroyer);
   
   // this is weird but %destroyer isn't necessarily a vehicle
   %message = getFancyDeathMessage(getHUDName(%destroyed), getHUDName(%destroyer));
   if(%message != "")
   {
      say( 0, 0, %message);
   }
   
   // enforce the rules
   if($server::TeamPlay == true)
   {
      if(
         (getTeam(%destroyed) == getTeam(%destroyer)) &&
         (%destroyed != %destroyer)
      )
      {
         antiTeamKill(%destroyer);
      }
   }   
}

//------------------------------------------------------------------------------
// scoreboard 

function getPlayerScore(%a)
{
   return((getKills(%a) * $killPoints) - (getDeaths(%a) * $deathPoints));
}

function getTeamScore(%a)
{
   return((getTeamKills(%a) * $killPoints) - (getTeamDeaths(%a) * $deathPoints));
}

function initScoreBoard()
{
   deleteVariables("$ScoreBoard::PlayerColumn*");
   deleteVariables("$ScoreBoard::TeamColumn*");

   if($server::TeamPlay == "True")	
   {
	   // Player ScoreBoard column headings
	   $ScoreBoard::PlayerColumnHeader1 = *IDMULT_SCORE_TEAM;
	   $ScoreBoard::PlayerColumnHeader2 = *IDMULT_SCORE_SQUAD;
	   $ScoreBoard::PlayerColumnHeader3 = *IDMULT_SCORE_SCORE;
	   $ScoreBoard::PlayerColumnHeader4 = *IDMULT_SCORE_KILLS;
	   $ScoreBoard::PlayerColumnHeader5 = *IDMULT_SCORE_DEATHS;

	   // Player ScoreBoard column functions
	   $ScoreBoard::PlayerColumnFunction1 = "getTeam";
	   $ScoreBoard::PlayerColumnFunction2 = "getSquad";
	   $ScoreBoard::PlayerColumnFunction3 = "getPlayerScore";
	   $ScoreBoard::PlayerColumnFunction4 = "getKills";
	   $ScoreBoard::PlayerColumnFunction5 = "getDeaths";
   }
   else
   {
       // Player ScoreBoard column headings
	   $ScoreBoard::PlayerColumnHeader1 = *IDMULT_SCORE_SQUAD;
	   $ScoreBoard::PlayerColumnHeader2 = *IDMULT_SCORE_SCORE;
	   $ScoreBoard::PlayerColumnHeader3 = *IDMULT_SCORE_KILLS;
	   $ScoreBoard::PlayerColumnHeader4 = *IDMULT_SCORE_DEATHS;

	   // Player ScoreBoard column functions
	   $ScoreBoard::PlayerColumnFunction1 = "getSquad";
	   $ScoreBoard::PlayerColumnFunction2 = "getPlayerScore";
	   $ScoreBoard::PlayerColumnFunction3 = "getKills";
	   $ScoreBoard::PlayerColumnFunction4 = "getDeaths";
   }

   // Team ScoreBoard column headings
   $ScoreBoard::TeamColumnHeader1 = *IDMULT_SCORE_SCORE;
   $ScoreBoard::TeamColumnHeader2 = *IDMULT_SCORE_PLAYERS;
   $ScoreBoard::TeamColumnHeader3 = *IDMULT_SCORE_KILLS;
   $ScoreBoard::TeamColumnHeader4 = *IDMULT_SCORE_DEATHS;

   // Team ScoreBoard column functions
   $ScoreBoard::TeamColumnFunction1 = "getTeamScore";
   $ScoreBoard::TeamColumnFunction2 = "getNumberOfPlayersOnTeam";
   $ScoreBoard::TeamColumnFunction3 = "getTeamKills";
   $ScoreBoard::TeamColumnFunction4 = "getTeamDeaths";

   // tell server to process all the scoreboard definitions defined above
   serverInitScoreBoard();
}
