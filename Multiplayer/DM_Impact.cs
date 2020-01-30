// FILENAME:	DM_Impact.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_Impact";

exec("multiplayerStdLib.cs");
exec("DMstdLib.cs");

function setDefaultMissionOptions()
{
	$server::TeamPlay = false;
	$server::AllowDeathmatch = true;
	$server::AllowTeamPlay = true;	
}

function onMissionStart()
{
	// Keep tracks of hercs near a door
	$HercsAtDoor1 = 0;
	$HercsAtDoor2 = 0;   
   
	// Start the meteor showers
	dropMeteor("default", -40, -1,-100, -4000, 100, -4100, -854, -620, 844, 1935);
	
	// Schedule a meteor storm
	%randomTime = randomInt(60, 200);
	schedule("randomDeathStorm();", %randomTime);

	$healRate = 100;   
	$ammoRate = 3;
   	$padWaitTime = 45;
  	$zenWaitTime = 90;
	
	europaSounds();
}

function onMissionLoad(){
   cdAudioCycle("Gnash", "Cloudburst", "Cyberntx"); 
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

function randomDeathStorm(){
	%nextStorm = randomInt(120, 400);

	Say(0, 0, "WARNING:  Meteor shower detected!");
	dropMeteor("default", 1, 3, -100, -4000, 100, -4100, -854, -620, 844, 1935);
    
    %dropMeteors = "dropMeteor(\"default\", 1, 3, -100, -4000, 100, -4100, -854, -620, 844, 1935);";

	schedule(%dropMeteors, 4);
	schedule(%dropMeteors, 8);
	schedule(%dropMeteors, 12);
	schedule(%dropMeteors, 16);
	schedule(%dropMeteors, 20);
	schedule(%dropMeteors, 24);
	schedule(%dropMeteors, 28);
	schedule(%dropMeteors, 32);
	schedule(%dropMeteors, 36);

	schedule("randomDeathStorm();", %nextStorm);
}

function door1::trigger::onEnter(%this, %vehicleId){
	if($hercsAtDoor1 == 0){
		playAnimSequence(getObjectId("MissionGroup\\Stuff\\Door1"), 0, true);
	}
	$hercsAtDoor1 = $hercsAtDoor1 + 1;
}
function door1::trigger::onLeave(%this, %vehicleId){
	$hercsAtDoor1 = $hercsAtDoor1 - 1;
	if($hercsAtDoor1 == 0){
		playAnimSequence(getObjectId("MissionGroup\\Stuff\\Door1"), 0, false);
	}
}
function door2::trigger::onEnter(%this, %vehicleId){
	if($hercsAtDoor2 == 0){
		playAnimSequence(getObjectId("MissionGroup\\Stuff\\Door2"), 0, true);
	}
	$hercsAtDoor2 = $hercsAtDoor2 + 1;
}
function door2::trigger::onLeave(%this, %vehicleId){
	$hercsAtDoor2 = $hercsAtDoor2 - 1;
	if($hercsAtDoor2 == 0){
		playAnimSequence(getObjectId("MissionGroup\\Stuff\\Door2"), 0, false);
	}
}

