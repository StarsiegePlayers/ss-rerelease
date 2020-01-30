// FILENAME:	DM_Lunacy.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_Lunacy";

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
	$healRate = 100;   
	$ammoRate = 3;
   	$padWaitTime = 45;
  	$zenWaitTime = 90;
   	$instantPadWaitTime = 120;
	
	moonSounds();

   	$specialHealRate = 1500;   // special pads
   	$specialAmmoRate = 25;
   
   	$blastRadiusSmall = 50;   // for ammo boxes and special pads
   	$blastDamageSmall = 4000;

   	$blastRadiusLarge = 100;  // for fuel tanks
   	$blastDamageLarge = 8000;
   
	cdAudioCycle("Watching", "Terror", "Mechsoul"); 
}

function onMissionLoad(){
   cdAudioCycle("Watching", "Terror", "Mechsoul"); 
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

// Instant Pad stuff
//------------------------------------------------------------------------------
function ZenInstant::trigger::onEnter(%this, %object)
{
   Zen::onEnter(%this, %object, *IDMULT_CHAT_ALLPAD, true, true);  
}
function ZenInstant::trigger::onContact(%this, %object)
{
   // completely heal and reload instantly -- no reset time
   // shutdown necessary
   Zen::work(%this, %object, 10000, 100, $instantPadWaitTime, true); 
}
function ZenInstant::trigger::onLeave(%this, %object)
{
   Zen::onLeave(%this, %object);
}

// Special pads
// no message, no shutdown, no reset
function ZenAll1::trigger::onEnter(%this, %object)
{
   // instant work, no shutdown, no reset
    Zen::onEnter(%this, %object, "", false, false);  
	if(%this.wait != true){
		Zen::work(%this, %object, $specialHealRate, $specialAmmoRate, 20 , false);
	   // damage everyone else within %blastRadiusSmall meters
   		blast(%this, $blastRadiusSmall, $blastDamageSmall, %object);
		explosion1();
	}
}

function ZenAll2::trigger::onEnter(%this, %object)
{
   // instant work, no shutdown, no reset
    Zen::onEnter(%this, %object, "", false, false);  
	if(%this.wait != true){
		Zen::work(%this, %object, $specialHealRate, $specialAmmoRate, 20 , false);
	   // damage everyone else within %blastRadiusSmall meters
   		blast(%this, $blastRadiusSmall, $blastDamageSmall, %object);
		explosion2();
	}
}

function ZenAll3::trigger::onEnter(%this, %object)
{
   // instant work, no shutdown, no reset
    Zen::onEnter(%this, %object, "", false, false);  
	if(%this.wait != true){
		Zen::work(%this, %object, $specialHealRate, $specialAmmoRate, 20 , false);
	   // damage everyone else within %blastRadiusSmall meters
   		blast(%this, $blastRadiusSmall, $blastDamageSmall, %object);
		explosion3();
	}
}

function ZenAll4::trigger::onEnter(%this, %object)
{
   // instant work, no shutdown, no reset
    Zen::onEnter(%this, %object, "", false, false);  
	if(%this.wait != true){
		Zen::work(%this, %object, $specialHealRate, $specialAmmoRate, 20 , false);
	   // damage everyone else within %blastRadiusSmall meters
   		blast(%this, $blastRadiusSmall, $blastDamageSmall, %object);
		explosion4();
	}
} 
function ZenAll1::trigger::onLeave(%this, %object)
{
   Zen::onLeave(%this, %object);
}
function ZenAll2::trigger::onLeave(%this, %object)
{
   Zen::onLeave(%this, %object);
}
function ZenAll3::trigger::onLeave(%this, %object)
{
   Zen::onLeave(%this, %object);
}
function ZenAll4::trigger::onLeave(%this, %object)
{
   Zen::onLeave(%this, %object);
}

function explosion1()
{
	dropPod(-686, 688, 117, -759, 688, 117);
	dropPod(-759, 688, 117, -686, 688, 117);
}
function explosion2()
{
	dropPod(-686, 720, 117, -759, 720, 117);
	dropPod(-759, 720, 117, -686, 720, 117);
}
function explosion3()
{
	dropPod(-686, 752, 117, -759, 752, 117);
	dropPod(-759, 752, 117, -686, 752, 117);
}
function explosion4()
{
	dropPod(-686, 784, 117, -759, 784, 117);
	dropPod(-759, 784, 117, -686, 784, 117);
}


// Ammo boxes and special pads
//--------------------------------------------------------------------------------
function BlastDamageSmall::structure::onDestroyed(%this, %object)
{
   // blast everyone around
   // exclude nobody
   blast(%this, $blastRadiusSmall, $blastDamageSmall, 0);
}

// fuel tanks
//--------------------------------------------------------------------------------
function BlastDamageLarge::structure::onDestroyed(%this, %object)
{
   // blast everyone around
   // exclude nobody
   blast(%this, $blastRadiusLarge, $blastDamageLarge, 0);
}


// Easter Egg
//--------------------------------------------------------------------------------
function EasterEgg::trigger::onEnter(%this, %object)
{
	Say(playerManager::vehicleIdToPlayerNum(%object), %object, *IDMULT_MOON_LANDER_FOUND);
}
function EasterEgg::trigger::onContact(%this, %object)
{
	damageObject(%object, 250);
}



