// FILENAME:	DM_Battlefield_Earth.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_Battlefield_Earth";

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
   cdAudioCycle("Purge", "Watching", "Mechsoul"); 
}

function onMissionStart()
{
	temperateSounds();	
}

function Buddha::structure::onAttacked(%destroyed, %destroyer){
	%sayTo = playerManager::vehicleIdToPlayerNum(%destroyer);
	Say(%sayTo, %sayTo, *IDMULT_YOU_KILLED_BUDDHA);
	healObject(%destroyer, -10000);
	healObject(%destroyer, -10000);
	healObject(%destroyer, -10000);
	healObject(%destroyer, -10000);
	healObject(%destroyer, -10000);
}

function Door1::trigger::onEnter(%this, %vehicleId){
	playAnimSequence(getObjectId("MissionGroup\\Triggers\\Door"), 0, true);
}
function Door1::trigger::onLeave(%this, %vehicleId){
	playAnimSequence(getObjectId("MissionGroup\\Triggers\\Door"), 0, false);
}
