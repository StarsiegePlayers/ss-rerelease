// FILENAME:	DM_Moonstrike.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_Moonstrike";

exec("multiplayerStdLib.cs");
exec("DMstdLib.cs");

$server::HudMapViewOffsetX = -1400;
$server::HudMapViewOffsetY = 2400;

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
	$healRate = 100;   
	$ammoRate = 3;
  	$zenWaitTime = 90;
	
	moonSounds();
}

function onMissionLoad(){
   cdAudioCycle("Gnash", "Cloudburst", "Cyberntx"); 
}

// ZenAll Pad Functionality
//------------------------------------------------------------------------------
function ZenAll::trigger::onEnter(%this, %object)
{
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_ALLPAD, true, true);  
}
function ZenAll::trigger::onContact(%this, %object)
{
   	Zen::work(%this, %object, $healRate, $ammoRate, $zenWaitTime, true); 
}
