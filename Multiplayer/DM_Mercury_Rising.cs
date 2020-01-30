// FILENAME:	DM_Mercury_Rising.cs
//
// AUTHORS:  	GMAN
//------------------------------------------------------------------------------

$missionName = "DM_Mercury_Rising";

$server::HudMapViewOffsetX = 500; 
$server::HudMapViewOffsetY = 4000; 

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

function onMissionStart()
{
	mercurySounds();
}

function onMissionLoad(){
   cdAudioCycle("Gnash", "Cloudburst", "Cyberntx"); 
}

