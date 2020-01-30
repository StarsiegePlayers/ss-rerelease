// FILENAME:	DM_The_Guardian.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_The_Guardian";

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
   cdAudioCycle("Yougot", "SS3", "SS1"); 
}

function onMissionStart(){
	titanSounds();
}

function turret::onAdd(%this)
{
	if($server::TeamPlay == false)
	{
		setTeam(%this, *IDSTR_TEAM_NEUTRAL);
	}
}

