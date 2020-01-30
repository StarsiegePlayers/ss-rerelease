// FILENAME:	DM_Avalanche.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

// this var 
$missionName = "DM_Avalanche";

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
	iceSounds();
}

function onMissionLoad(){
   cdAudioCycle("Gnash", "Cloudburst", "Cyberntx"); 
}