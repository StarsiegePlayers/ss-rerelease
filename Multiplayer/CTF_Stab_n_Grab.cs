// FILENAME:	CTF_Stab_n_Grab.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

///////////////////////////////////////////////////////////////////////////////////////////////////
// Cybrids vs Humans      
//
// Any vehicle on the wrong side will get a message and be automatically set to the
// correct team. -- TSL
///////////////////////////////////////////////////////////////////////////////////////////////////

$missionName = "CTF_Stab_n_Grab";

$maxFlagCount  = 8;       
$flagValue     = 5;       
$carrierValue  = 2;       
$killPoints    = 1;
$deathPoints   = 1;
$flagTime = 300;

exec("multiplayerStdLib.cs");
exec("CTFstdLib.cs");

function setDefaultMissionOptions()
{
	$server::TeamPlay = true;
	$server::AllowDeathmatch = false;
	$server::AllowTeamPlay = true;	

	$server::AllowTeamRed = false;
	$server::AllowTeamBlue = true;
	$server::AllowTeamYellow = false;
	$server::AllowTeamPurple = true;

   // what can the server admin choose for available teams
   $server::disableTeamRed = true;
   $server::disableTeamBlue = false;
   $server::disableTeamYellow = true;
   $server::disableTeamPurple = false;
}

function onMissionLoad()
{
	initGlobalVars();
 	cdAudioCycle("Cloudburst", "Mechsoul", "SS3"); 

   %rules = "<tIDMULT_CTF_GAMETYPE>"      @        
            "<tIDMULT_STD_CYBRID_VS_HUMAN_1>"  @
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

function onMissionStart()
{
	temperateSounds();
	say(0, 0, "", IDSFX_AMBIENT_RAIN);
}

function vehicle::onAdd(%this)
{
   // Cybrid vs Humans ... Cybrids must be red, Humans must be yellow
  
   if(getVehicleTechBase(%this) == "H" && getTeam(%this) != *IDSTR_TEAM_BLUE)
   {
      setTeam(%this, *IDSTR_TEAM_BLUE);
      say(PlayerManager::vehicleIdToPlayerNum(%this), 1234, *IDMULT_CHANGING_TEAM_BLUE);
      redrop(%this);
   }
   else if(getVehicleTechBase(%this) == "C" && getTeam(%this) != *IDSTR_TEAM_PURPLE)
   {
      setTeam(%this, *IDSTR_TEAM_PURPLE);
      say(PlayerManager::vehicleIdToPlayerNum(%this), 1234, *IDMULT_CHANGING_TEAM_PURPLE);
      redrop(%this);
   }

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
