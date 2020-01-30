// FILENAME:	DM_Sacrifice_to_Bast.cs
//
// AUTHORS:		Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_Sacrifice_To_Bast";

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
	desertSounds();
}

function onMissionLoad(){
   cdAudioCycle("SS1", "Newtech", "Yougot"); 
}









