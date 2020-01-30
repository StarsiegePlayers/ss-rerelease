// FILENAME:	DM_State_of_Confusion.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_State_of_Confusion";

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
	marsSounds();
}

function onMissionLoad(){
   cdAudioCycle("Gnash", "Cloudburst", "Cyberntx"); 
}

// Easter Egg time
function AirChina::trigger::onEnter(%this, %vehicleId){
	say(playerManager::vehicleIdToPlayerNum(%vehicleId), 0, *IDMULT_AIR_CHINA);
	%backToTheField = strcat("endTheTrip(", %vehicleId, ");");
	schedule(%backToTheField, 10);	
}

function endTheTrip(%vehicleId){
	randomTransport(%vehicleId, -1890, 1681, -1890, 1681);
}
