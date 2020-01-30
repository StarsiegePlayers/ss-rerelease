// FILENAME:	DM_City_on_the_Edge.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_City_On_The_Edge";

exec("multiplayerStdLib.cs");
exec("DMstdLib.cs");

function setDefaultMissionOptions()
{
	$server::TeamPlay = false;
	$server::AllowDeathmatch = true;
	$server::AllowTeamPlay = true;	

   // what can the client choose for a team
	$server::AllowTeamRed = true;
	$server::AllowTeamBlue = true;
	$server::AllowTeamYellow = false;
	$server::AllowTeamPurple = false;

   // what can the server admin choose for available teams
   $server::disableTeamRed = false;
   $server::disableTeamBlue = false;
   $server::disableTeamYellow = true;
   $server::disableTeamPurple = true;
}

function onMissionStart()
{
	marsSounds();
}

function onMissionLoad(){
   cdAudioCycle("Purge", "Terror", "Watching"); 
}

// Water Tower splash functionality
function structure::onDestroyed(%this, %attackerId){
	// Which water tower was destroyed?
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
