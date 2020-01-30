// FILENAME:	CTF_City_on_the_Edge.cs
//
// AUTHORS:		Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "CTF_City_on_the_Edge";

$maxFlagCount  = 8;           // no of flags required by a team to end the game
$flagValue     = 5;          // points your team gets for capturing
$carrierValue  = 2;          //  "      "    "    "    " killing carrier
$killPoints    = 1;
$deathPoints   = 1;
$flagTime = 180;

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
   cdAudioCycle("Purge", "Terror", "Watching");
}

function onMissionStart()
{
	initGlobalVars();
   
	marsSounds();
}

// Water splashing functionality
//------------------------------------------------------------------------------
function structure::onDestroyed(%this, %attackerId){
	// Which water tower was just destroyed?
	if(%this == getObjectId("MissionGroup\\BlueBase\\WaterTower1")){
		setShapeVisibility(getObjectId("MissionGroup\\BlueBase\\Splash1"), true);
		playAnimSequence(getObjectId("MissionGroup\\BlueBase\\Splash1"), 0, true);
	}
	else if(%this == getObjectId("MissionGroup\\BlueBase\\WaterTower2")){
		setShapeVisibility(getObjectId("MissionGroup\\BlueBase\\Splash2"), true);
		playAnimSequence(getObjectId("MissionGroup\\BlueBase\\Splash2"), 0, true);
	}
	else if(%this == getObjectId("MissionGroup\\BlueBase\\WaterTower3")){
		setShapeVisibility(getObjectId("MissionGroup\\BlueBase\\Splash3"), true);
		playAnimSequence(getObjectId("MissionGroup\\BlueBase\\Splash3"), 0, true);
	}
}
