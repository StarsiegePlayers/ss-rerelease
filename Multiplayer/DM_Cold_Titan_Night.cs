// FILENAME:	DM_Cold_Titan_Night.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_Cold_Titan_Night";

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
   cdAudioCycle("SS3", "SS4", "Watching"); 
}

function onMissionStart()
{
	$healRate = 100;   
	$ammoRate = 3;
   	$padWaitTime = 45;
  	$zenWaitTime = 90;

	titanSounds();	
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

//If someone shoots frosty, his houses blow up revealing a nasty surprise
//------------------------------------------------
function tur1::turret::onAdd(%this)
{
	$turret1=%this;
	order($turret1,ShutDown,true);
}
function tur2::turret::onAdd(%this)
{
	$turret2=%this;
	order($turret2,ShutDown,true);
}
function Frosty::structure::onAttacked(%attacked,%attacker)
{
	%bang1=getObjectId("MissionGroup\\extra\\frosty's house\\house1");
	healObject(%bang1,-30000);
	order($turret1,ShutDown,false);
	order($turret1, Attack, %attacker);
	%bang2=getObjectId("MissionGroup\\extra\\frosty's house\\house2");
	healObject(%bang2,-30000);
	order($turret2,ShutDown,false);
	order($turret2, Attack, %attacker);
}
