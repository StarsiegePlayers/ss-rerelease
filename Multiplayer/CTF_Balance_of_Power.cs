// FILENAME:	CTF_Balance_of_Power.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "CTF_Balance_Of_Power";

$maxFlagCount  = 8;           // no of flags required by a team to end the game
$flagValue     = 5;          // points your team gets for capturing
$carrierValue  = 2;          //  "      "    "    "    " killing carrier
$killPoints    = 1;
$deathPoints   = 1;
$flagTime = 240;

exec("multiplayerStdLib.cs");
exec("CTFstdLib.cs");

function setDefaultMissionOptions()
{
	$server::TeamPlay = true;
	$server::AllowDeathmatch = false;
	$server::AllowTeamPlay = true;	

   // what can the client choose for a team
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
   logStandardMissionStart();

   cdAudioCycle("Cyberntx", "Cloudburst", "Terror"); 
}

function onMissionStart()
{
	initGlobalVars();
   
	temperateSounds();
}

