// FILENAME:	CTF_Circular_Logic.cs
//
// AUTHORS:  	Cupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "CTF_Circular_Logic";

$maxFlagCount  = 8;           // no of flags required by a team to end the game
$flagValue     = 5;          // points your team gets for capturing
$carrierValue  = 2;          //  "      "    "    "    " killing carrier
$killPoints    = 1;
$deathPoints   = 1;
$flagTime = 300;

exec("multiplayerStdLib.cs");
exec("CTFstdLib.cs");

function setDefaultMissionOptions()
{
	$server::TeamPlay = true;
	$server::AllowDeathmatch = false;
	$server::AllowTeamPlay = true;	

	$server::AllowTeamRed = false;
	$server::AllowTeamBlue = true;
	$server::AllowTeamYellow = false;
	$server::AllowTeamPurple = true;

   // what can the server admin choose for available teams
   $server::disableTeamRed = true;
   $server::disableTeamBlue = false;
   $server::disableTeamYellow = true;
   $server::disableTeamPurple = false;
}

function onMissionStart()
{
	initGlobalVars();
  
	
	$lastVehicleOnZen = "";
	$lastVehicleOnRedZen = "";
	$lastVehicleOnBlueZen = "";
	$secondsToGain = 0;
	$secondsToGainRedZen = 0;
	$secondsToGainBlueZen = 0;
	
	titanSounds();
}

function onMissionLoad(){
   cdAudioCycle("Purge", "Cyberntx", "Yougot"); 
}

// ZenAll Pad Functionality
//------------------------------------------------------------------------------
function ZenAll::trigger::onEnter(%this, %object)
{
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_ALLPAD, true, true);  
}
function ZenAll::trigger::onContact(%this, %object)
{
   	Zen::work(%this, %object, $yellowZenHealRate, $yellowZenAmmoRate, $zenWaitTime, true);
	if(isShutDown(%object)){
		if(%object != $lastVehicleOnZen){
			$lastVehicleOnZen = %object;
			$secondsToGain = 1;
		}
		else{
			$secondsToGain = $secondsToGain + 1;
		}
		if($secondsToGain >= 4)
			giveTurretsToThisGuy(%object);
	}
}
// ZenAll Pad Functionality
//------------------------------------------------------------------------------
function RedZen::trigger::onEnter(%this, %object)
{
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_ALLPAD, true, true);  
}
function RedZen::trigger::onContact(%this, %object)
{
   	Zen::work(%this, %object, $redZenHealRate, $redZenAmmoRate, $zenWaitTime, true);
	if(isShutDown(%object)){
		if(%object != $lastVehicleOnRedZen){
			$lastVehicleOnRedZen = %object;
			$secondsToGainRedZen = 1;
		}
		else{
			$secondsToGainRedZen = $secondsToGainRedZen + 1;
		}
		if($secondsToGainRedZen >= 4)
			giveRedTurretsToThisGuy(%object);
	}
}
// ZenAll Pad Functionality
//------------------------------------------------------------------------------
function BlueZen::trigger::onEnter(%this, %object)
{
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_ALLPAD, true, true);  
}
function BlueZen::trigger::onContact(%this, %object)
{
   	Zen::work(%this, %object, $blueZenHealRate, $blueZenAmmoRate, $zenWaitTime, true);
	if(isShutDown(%object)){
		if(%object != $lastVehicleOnBlueZen){
			$lastVehicleOnBlueZen = %object;
			$secondsToGainBlueZen = 1;
		}
		else{
			$secondsToGainBlueZen = $secondsToGainBlueZen + 1;
		}
		if($secondsToGainBlueZen >= 4)
			giveBlueTurretsToThisGuy(%object);
	}
}

// Turret Code
// -----------------------------------------------------------------------------------

function giveTurretsToThisGuy(%vehicleId){
	%team = getTeam(%vehicleId);
	setTeam("MissionGroup\\CenterStructures\\T1", %team);
	setTeam("MissionGroup\\CenterStructures\\T2", %team);
	setTeam("MissionGroup\\CenterStructures\\T3", %team);
	setTeam("MissionGroup\\CenterStructures\\T4", %team);
	setTeam("MissionGroup\\CenterStructures\\NavZen", %team);
	order("MissionGroup\\CenterStructures", Shutdown, false);	
}
function giveRedTurretsToThisGuy(%vehicleId){
	%team = getTeam(%vehicleId);
	setTeam("MissionGroup\\CenterStructures\\T5", %team);
	setTeam("MissionGroup\\CenterStructures\\T6", %team);
	setTeam("MissionGroup\\CenterStructures\\NavZenRed", %team);
	order("MissionGroup\\CenterStructures", Shutdown, false);	
}
function giveBlueTurretsToThisGuy(%vehicleId){
	%team = getTeam(%vehicleId);
	setTeam("MissionGroup\\CenterStructures\\T7", %team);
	setTeam("MissionGroup\\CenterStructures\\T8", %team);
	setTeam("MissionGroup\\CenterStructures\\NavZenBlue", %team);
	order("MissionGroup\\CenterStructures", Shutdown, false);	
}


