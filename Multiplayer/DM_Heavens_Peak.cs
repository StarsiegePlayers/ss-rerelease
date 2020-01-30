// FILENAME:	DM_Heavens_Peak.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_Heavens_Peak";

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
   cdAudioCycle("Purge", "Newtech", "Cyberntx"); 
}

// move the map
$server::HudMapViewOffsetX = -1500;
$server::HudMapViewOffsetY = 2500;

function onMissionStart()
{
	$healRate = 100;   
	$ammoRate = 3;
   	$padWaitTime = 45;
  	$zenWaitTime = 90;
	
	temperateSounds();
}

// Healing Pad Functionality
//------------------------------------------------------------------------------
function ZenHeal::trigger::onEnter(%this, %object)
{
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_HEALPAD, true, true);  
}
function ZenHeal::trigger::onContact(%this, %object)
{
   	Zen::work(%this, %object, $healRate, 0, $padWaitTime, true); 
}

// Ammo Pad Functionality
//------------------------------------------------------------------------------
function ZenAmmo::trigger::onEnter(%this, %object)
{
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_AMMOPAD, true, true);  
}

function ZenAmmo::trigger::onContact(%this, %object)
{
   	Zen::work(%this, %object, 0, $ammoRate, $padWaitTime, true); 
}

// ZenAll Pad Functionality
//------------------------------------------------------------------------------
function ZenAll::trigger::onEnter(%this, %object)
{
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_ALLPAD, true, true);  
}
function ZenAll::trigger::onContact(%this, %object)
{
   	Zen::work(%this, %object, $healRate, $ammoRate, $padWaitTime, true); 
}
