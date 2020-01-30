// FILENAME:	DM_ColourMePluto.cs
//
// AUTHORS:  	Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_Fear_in_Isolation";

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
	plutoSounds();
}

function onMissionLoad(){
   cdAudioCycle("Purge", "Newtech", "SS4", "SS3"); 
}

