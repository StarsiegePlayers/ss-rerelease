// FILENAME:	DM_Twin_Siege.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_Twin_Siege";

exec("multiplayerStdLib.cs");
exec("DMstdLib.cs");

function setDefaultMissionOptions()
{
	$server::TeamPlay = false;
	$server::AllowDeathmatch = true;
	$server::AllowTeamPlay = true;	

	$server::AllowTeamRed = true;
	$server::AllowTeamBlue = true;
	$server::AllowTeamYellow = true;
	$server::AllowTeamPurple = true;
}

function onMissionLoad(){
   cdAudioCycle("Watching", "Mechsoul", "Gnash"); 
}

// move the map
$server::HudMapViewOffsetX = 7500;
$server::HudMapViewOffsetY = -6000;

function onMissionStart()
{
	titanSounds();
}

