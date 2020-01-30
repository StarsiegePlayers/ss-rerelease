//------------------------------------------------------------------------------
//
// Multiplayer standard library
//

//-----------------------------------------------
// Utility functions


// If this variable is set to true, when a teammate hits another teammate repeatedly, 
// he will be penalized. The offender's Herc will be shut down, his reactor and shields
// drained, and all of his ammo (energy too) drained. Once a player has offended, he
// will be on probation temporarily. The probation time depends on how bad (or many times)
// he has attacked his teammates. 
//
// When enabled, the offending player is notified with a text message that he is being
// penalized for team attacking.
$teamHittingPenalty = False;


function timeDifference(%t1, %t2)
{
   // calculates the time difference t1-t2 and returns it as a string
   // such as "1 minute, 32 seconds"
   
   %diff = %t1 - %t2;
   
   %minutes = 0;
   
   // can't do rounding in console so hack it
   %temp = %diff;
   %temp = %temp - 60;
   
   while(%temp >= 0)
   {
      %minutes = %minutes + 1;
      %temp = %temp - 60;
   }
      
   %seconds = %temp + 60;
   
   
   // prepare output
   %output = "";
   
   if(%minutes > 0)
   {
      %output = %output @ %minutes @ " ";
      if(%minutes == 1)
      {
         %output = %output @ *IDMULT_MINUTE;      
      }
      else
      {
         %output = %output @ *IDMULT_MINUTES;
      }
      
      if(%seconds > 0)
      {
         // make space for seconds output
         %output = %output @ ", ";
      }
   }
   
   if(%seconds > 0)
   {
      %output = %output @ %seconds @ " ";
      if(%seconds == 1)
      {
         %output = %output @ *IDMULT_SECOND;      
      }
      else
      {
         %output = %output @ *IDMULT_SECONDS;
      }
   }
   
   return %output;
}



function withinDistance( %obj1, %obj2, %distance)
{
   %obj1 = getObjectId( %obj1 );
   %obj2 = getObjectId( %obj2 );

   %var = getDistance( %obj1, %obj2 );
   
   if( %var > %distance )
   {
      return false;
   }
   else
   {
      return true;
   }
}


function warp( %obj1, %obj2 )
{
   // set the position of obj1 to the position of obj2
   %obj2X = getPosition( %obj2, "x" );
   %obj2Y = getPosition( %obj2, "y" );
   %obj2Z = getPosition( %obj2, "z" );
   
   setPosition( %obj1, %obj2X, %obj2Y, %obj2Z );
}


function randomTransport( %object, %x1, %y1, %x2, %y2)
{
   // set the position of %object to a randow spot within the space
   // bounded by (x1, y1) and (x2, y2).  The Z coordinate is going to
   // be set to 40 meters above the terrain at the new point.
   
   %x = randomInt(%x1, %x2);
   %y = randomInt(%y1, %y2);
   %z = getTerrainHeight(%x, %y) + 60;

	%playerNum = playerManager::vehicleIdToPlayerNum(%object);
 
   // play a sound at both locations
   playSound(0, IDSFX_TELEPORTER, IDPRF_NEAR, %object);
   playSound(0, IDSFX_TELEPORTER, IDPRF_NEAR, %x, %y, %z);
   
   if(%playerNum != 0)
   {
   	fadeEvent(%playerNum, out, 1.0, 1.0, 1.0, 1.0);
   	schedule(strcat("fadeEvent(", %playerNum, ",in, 1.0, 1.0, 1.0, 1.0);"), 1.2);
   }
   schedule(strcat("setPosition(", %object, ",", %x,",", %y,",", %z,");"), 1.0);   
}

function displayPercentage(%this, %percentage)
{
	%player = playerManager::vehicleIdToPlayerNum( %this );
   if(%player != 0)
   {
	   say(%player, 0, *IDMULT_CHAT_SHOW_PERCENTAGE_1 @ %percentage @ *IDMULT_CHAT_SHOW_PERCENTAGE_2);
   }
}


function getFullPath(%object)
{
   // recursive function to get the path of this thing
   if(%object == 0)
   {
      return "";
   }
   
   %name = getObjectName(%object);
   
   if(%name == "MissionGroup")
   {
      return %name;
   }
   else
   {
      return (getFullPath(getGroup(%object)) @ "\\" @ %name);
   }
}

//-----------------------------------------------
// Rules enforcement


function vehicle::onDestroyed(%destroyed, %destroyer)
{
   // a generic enforcement of rules
 
   if($server::TeamPlay == true)
   {
      if(
         (getTeam(%destroyed) == getTeam(%destroyer)) &&
         (%destroyed != %destroyer)
      )
      {
         antiTeamKill(%destroyer);
      }
   }
}

function antiTeamKill(%offender)
{
   // offender is a vehicle
   %player = playerManager::vehicleIdToPlayerNum(%offender);   
   
   if(%player == 0)
      return;  // it's the server
   
   say(%player, %player, "", "rules_violated.wav");
   
   // since whatever we do will kill the offender, avoid recursion   
   resetLastHitBy(%offender);
   
   if(
      (%player.teamKillWarn == true) &&   // already been warned
      (%player != getConnectedClient())   // check to see if it is the hosting player
   )
   {  
      // kick 'em
      
      // reset the warn flag first
      %player.teamKillWarn = false;
      
      kick(%player, *IDMULT_CHAT_TEAM_KILL_KICK);
   }
   else
   {
      // set the warn flag first
      %player.teamKillWarn = true;
      
      // kill the offender
      damageObject(%offender, 1000000);
      
      // tell the player
      messageBox(%player, *IDMULT_CHAT_TEAM_KILL_WARN);
   }
}


function getNumberOfPlayersOnTeam(%bits)
{
   %name = getTeamNameFromTeamId(%bits);
   
   %count = 0;

   %playerCount = playerManager::getPlayerCount();
	for (%p = 0; %p < %playerCount; %p++)
	{
		%player = playerManager::getPlayerNum(%p);
		%team = getTeam(%player);
      
      if(%team == %name)
      {
         %count++;
      }      
   }
   
   return %count;
}

//-----------------------------------------------
// Zen Stuff
// these are triggers that heal, reload, or both
// only one player can use it at a time, and each time it is used, it
// will "re-charge" for a certain number of seconds (optional).  The player
// who landed on it is "special" and can stay as long as he/she wants.
// As soon as this person leaves it, he/she is no longer "special".


// player::onRemove must be run for triggers to handle "special" correctly
// any script that overrides this function should specifically include this code
function player::onRemove(%this)
{
   %vehicle = playerManager::playerNumToVehicleId(%this);
   if(%vehicle != 0)
   {
      if(%vehicle.pad != "")
      {
         // reset the special for the pad this guy was on
         %vehicle.pad.special = "";
      }
   }
}

function Zen::onEnter(%trigger, %object, %message, %isReady, %countdown)
{
   // %trigger has been entered
   // %object is the one who hit it
   // %message is a string to display (could be empty).
   // %isReady is a boolean -- if we report, do we tell if trigger is ready?
   // %countdown is a boolean -- if we report, and we tell player if trigger
   //   is ready, and trigger is not ready, do we tell player when trigger
   //   will be ready?
   
   // in all cases, this function will only send one message to the player,
   // since all of the above information will be concatenated.
   
   if(%message != "")
   {
      // the basic message
      %output = %message;
      
      // check for which soundfile we should play
      %soundFile = "";
      if(%message == *IDMULT_CHAT_HEALPAD)
      {
         %soundFile = "repair_ent.wav";
      }
      else if(%message == *IDMULT_CHAT_AMMOPAD)
      {
         %soundFile = "reload_ent.wav";
      }
      
      if(%isReady == true)
      {
         // make space for the next part
         %output = %output @ " ";
      
         // see whether or not it is ready
         if(
            %trigger.wait == false ||
            %trigger.wait == "" ||
            (%trigger.reset <= getCurrentTime())    
         )
         {
            // it's ready
            %output = %output @ *IDMULT_READY; 
         }
         else
         {
            %output = %output @ *IDMULT_NOT_READY;
            
            // trigger isn't ready, but do we display the countdown
            if(
               %countdown == true &&   // display countdown?
               %trigger.reset != ""    // is there one to display?
            )
            {
               %output = %output @ " " @ *IDMULT_READY_IN_1;
            
               // show the countdown
               %remaining = timeDifference(%trigger.reset, getCurrentTime());
               
               %output = %output @ %remaining @ *IDMULT_READY_IN_2;            
            }         
         }
      }
      
      // finally display the message
      %player = playerManager::vehicleIdToPlayerNum(%object);
      if(%player != 0)
      {
         say(%player, %trigger, %output, %soundFile);
      }
   }
}

function Zen::onLeave(%trigger, %object)
{
   // %trigger has been left
   // %object is the one who left it
   
   // if object is the trigger.special, then trigger no onger has a special
   if(%object == %trigger.special)
   {
      %trigger.special.pad = "";
      %trigger.special = "";
   }
}

function Zen::work(%trigger, %object, %healRate, %ammoRate, %reset, %shutdown)
{
   // %trigger has been activated
   // %object receives %healRate amount of healing
   // %object receives %ammoRate percent of ammo to each weapon
   // %reset is how many seconds must pass before trigger is recharged
   // %shutdown is a boolean -- whether or not trigger requires shutdown
      
   if(
      (
         isShutDown(%object) == true ||      // is vehicle shut down?
         %shutdown == false                  // does it even matter?
      ) &&
      (
         %trigger.wait == false ||     // is trigger ready?
         %trigger.wait == "" ||
         %trigger.special == %object   // is object special?   
      ) &&
      (
         %trigger.depleted == "" ||    // is pad not depleted
         %trigger.depleted == false
      )
   )   
   {
      if(%healRate > 0)
      {
         // do the healing bit
         healObject(%object, %healRate);
      }
      
      if(%ammoRate > 0)
      {   
         // and the reloading bit
         reloadObject(%object, %ammoRate);
      }

      if(
         %reset > 0 &&           // if we require a reset   
         %trigger.reset == ""    // and it's not already scheduled
      )
      {      
         // make this object special
         %trigger.special = %object;
         %object.pad = %trigger; // cross-link it

         // schedule the depletion time (one third of reset time)
         schedule("Zen::depleted(" @ %trigger @ ");", (%reset / 3));

         // schedule the recharge time and store it for later
         %trigger.reset = schedule("Zen::recharged(" @ %trigger @ ");", %reset);
        
         // trigger is no longer ready
         %trigger.wait = true;
      }
   }
   else
   {
      // check to see if player has shut down and started up again
   
      if(
         %object == %trigger.special &&   // if object is special
         %shutdown == true &&             // and is on a pad that requires shutdown
         isShutDown(%object) == false     // and is no longer shut down
      )
      {
         // object is no longer special
         %trigger.special.pad = "";
         %trigger.special = "";      
      }
   }
}

function Zen::depleted(%trigger)
{
   // scheduled function
   // pad becomes depleted after 1/3 of padWaitTime expires
   
   %trigger.depleted = true;
   
   if(%trigger.special != "")
   {
      // tell the person that they get no more free goodies

      %player = playerManager::vehicleIdToPlayerNum(%trigger.special);
      if(%player != 0)
      {
         say(%player, %trigger, *IDMULT_DEPLETED);
      }
   }
}

function Zen::recharged(%trigger)
{
   // scheduled to occur when padwaitTime has expired

   // see if our special is still hanging around -- loser
   if(%trigger.special != "")
   {
      // tell the person that the pad is back on again

      %player = playerManager::vehicleIdToPlayerNum(%trigger.special);
      if(%player != 0)
      {
         say(%player, %trigger, *IDMULT_RECHARGED);
      }
   }
   
   %trigger.wait = false;
   %trigger.depleted = false;
   %trigger.reset = "";
}

//-----------------------------------------------
// Default pads

//	ZenHeal should behave as follows:
//	- Announces it has been entered
//	- Heals object at normal rate; shut-down is necessary
//	- no reset time
//---------------------------------------------------------------------------
function ZenHeal::trigger::onEnter(%this, %object)
{
   // tell user pad has been entered
   Zen::onEnter(%this, %object, *IDMULT_CHAT_HEALPAD, true, true);  
}

function ZenHeal::trigger::onContact(%this, %object)
{
   // one hundred points healing
   Zen::work(%this, %object, 100, 0, 0, true); 
}

function ZenHeal::trigger::onLeave(%this, %object)
{
   Zen::onLeave(%this, %object); 
}

//	ZenAmmo should behave as follows:
//	- Announce it has been entered
//	- Reloads object at normal rate; shut-down is necessary
//	- no reset time
//---------------------------------------------------------------------------
function ZenAmmo::trigger::onEnter(%this, %object)
{
   // tell user pad has been entered
   Zen::onEnter(%this, %object, *IDMULT_CHAT_AMMOPAD, true, true);  
}

function ZenAmmo::trigger::onContact(%this, %object)
{
   // five percent reloading
   Zen::work(%this, %object, 0, 5, 0, true); 
}

function ZenAmmo::trigger::onLeave(%this, %object)
{
   Zen::onLeave(%this, %object); 
}


//	ZenAll should behave as follows:
//	- Announce it has been entered
//	- Reloads object at normal rate; shut-down is necessary
//	- no reset time
//---------------------------------------------------------------------------
function ZenAll::trigger::onEnter(%this, %object)
{
   // tell user pad has been entered
   Zen::onEnter(%this, %object, *IDMULT_CHAT_ALLPAD, true, true);  
}

function ZenAll::trigger::onContact(%this, %object)
{
   // one hundred points of loading
   // five percent reloading
   Zen::work(%this, %object, 100, 5, 0, true); 
}

function ZenAll::trigger::onLeave(%this, %object)
{
   Zen::onLeave(%this, %object); 
}




//---------------------------------------------------------------------------
// Damage 

function blast(%exploder, %range, %damage, %exclude)
{
   // exploder does some damage
   // find all players within %range and do linear-dropoff %damage
   // note that this only damages players
   
   %count = playerManager::getPlayerCount();
	for(%i = 0; %i < %count; %i = %i + 1)
	{
		%curPlayerNum = playerManager::getPlayerNum(%i);
		%curVehicle = playerManager::playerNumToVehicleId(%curPlayerNum);
      
      if(
         %exclude == 0 ||
         %exclude != %curVehicle
      )
      {      
         %curDist = getDistance(%exploder, %curVehicle);
         
         // see if it is close enough
         if(%curDist < %range)
         {
            %curDamage = (%damage * ((%range - %curDist) / %range));
         
            resetLastHitBy(%curVehicle);
            damageObject(%curVehicle, %curDamage); 
         }
      }
	}
}

//	DESCRIPTION:  Creates a meteor shower within a specified area
//
// Types:
//		focalStart:  Drops for a fixed points
//			args("focalStart", %rate, %count, %xMinEnd, %yMinEnd, %xMaxEnd, %yMaxEnd, %xStart, %yStart)
//		focalEnd:	 Hits a fixed point
//			args("focalend", %rate, %count, %xMinStart, %yMinStart, %xMaxStart, %yMaxStart, %xEnd, %yEnd)
//		vertical:	 Meteor drops vertically
//			args("verticle", %rate, %count, %xMin, %yMin, %xMax, %yMax)
//		default:     Meteor uses coordinates and randomly drops
//			args("verticle", %rate, %count, %xMin, %yMin, %xMax, %yMax)
//			args("verticle", %rate, %count, %xMinStart, %yMinStart, %xMaxStart, %yMaxStart, %xMinEnd, %yMinEnd, %xMaxEnd, %yMaxEnd) 
//
// %rate = how many seconds before next meteorite falls
//		if negative, the next metorite falls in 0 to abs(%rate) seconds
//
// %count = number of meteorites to drop
//
// The coordinates define the area a meteorite drops from and the area it can land in (limited by X, Y)
// ---------------------------------------------------------------------------------------------------
function dropMeteor(%type, %rate, %count, %xHigh1, %yHigh1, %xHigh2, %yHigh2, %xLow1, %yLow1, %xLow2, %yLow2){

	%ceiling = 1800;

	if(%xHigh2 == "") %xHigh2 = 0;
	if(%yHigh2 == "") %yHigh2 = 0;
	if(%xLow1 == "") %xLow1 = 0;
	if(%yLow1 == "") %yLow1 = 0;
	if(%xLow2 == "") %xLow2 = 0;
	if(%yLow2 == "") %yLow2 = 0;

	if(%xLow1 == 0 && %xLow2 == 0 && %yLow1 == 0 && %yLow2 == 0){
		%xLow1 = %xHigh1;
		%xLow2 = %xHigh2;
		%yLow1 = %yHigh1;
		%yLow2 = %yHigh2;
	}

	%startX = randomInt(%xHigh1, %xHigh2);
	%startY = randomInt(%yHigh1, %yHigh2);

	%endX = randomInt(%xLow1, %xLow2);
	%endY = randomInt(%yLow1, %yLow2);

	if(%type == "focus"){
		%startX = %endX = %xHigh1;
		%startY = %endY = %yHigh1;		
	}
	else if(%type == "focalStart"){
		%startX = %xLow1;
		%startY = %yLow1;
	}
	else if(%type == "focalEnd"){
		%endX = %xLow1;
		%endY = %yLow1;
	}
	else if(%type == "vertical"){
		%startX = %endX;
		%startY = %endY;
	}

	%endZ = getTerrainHeight(%endX, %endY);

	// Determine when the next meteorite will fall
	%when = %rate;
	if(%when < 0){
		%when = %when * -1;
		%when = randomFloat(0, %when);
	}

	if(count != -1)
		%count = %count - 1;

	%nextMeteorite = "dropMeteor(";
	%nextMeteorite = strcat(%nextMeteorite, %type, ", ");
	%nextMeteorite = strcat(%nextMeteorite, %rate, ", ");
	%nextMeteorite = strcat(%nextMeteorite, %count, ", ");
	%nextMeteorite = strcat(%nextMeteorite, %xHigh1, ", ");
	%nextMeteorite = strcat(%nextMeteorite, %yHigh1, ", ");
	%nextMeteorite = strcat(%nextMeteorite, %xHigh2, ", ");
	%nextMeteorite = strcat(%nextMeteorite, %yHigh2, ", ");
	%nextMeteorite = strcat(%nextMeteorite, %xLow1, ", ");
	%nextMeteorite = strcat(%nextMeteorite, %yLow1, ", ");
	%nextMeteorite = strcat(%nextMeteorite, %xLow2, ", ");
	%nextMeteorite = strcat(%nextMeteorite, %yLow2, ");");

	dropPod(%startX, %startY, %ceiling, %endX, %endY, %endZ, "MissionGroup\\meteorite");
	%doDamage = "blast(";
	%doDamage = strcat(%doDamage, "\"MissionGroup\\\\meteorite\", 120, 7000, \"MissionGroup\\\\meteorite\");");

	// This next bit calculates when the meteor hits the ground and damages the herc when it does...
	%timeDrop = ((%endX - %startX) * (%endX - %startX));
	%timeDrop = %timeDrop + ((%endY - %startY) * (%endY - %startY));
	%timeDrop = %timeDrop + ((%ceiling - %endZ) * (%ceiling - %endZ));
	%timeDrop = sqrt(%timeDrop);
	%timeDrop = %timeDrop / 400;
	schedule(%doDamage, %timeDrop);

	if(%count != 0)
		schedule(%nextMeteorite, %when);
}

// called by the create multiplayer shell
// to initialize the vehicle, component, and weapon
// list
function setDefaultMissionItems() 
{
}

// Destructable Pad Functionality
// ----------------------------------------------------------------------------------
// These functions are for yellow, blue, red, and purple bases
// It is assumed that only one pad for each type is on any base
// The following globals are used for these functions
// Their responsibilities are self explanatory
// You can reset their values to whatever you feel is appropriate                
// ----------------------------------------------------------------------------------
// Amount a pad's current rate is divided by after power structure has been destroyed
	$yellowHealRate = $blueHealRate = $redHealRate = $purpleHealRate = 200;
	$yellowAmmoRate = $blueAmmoRate = $redAmmoRate = $purpleAmmoRate = 25;
	$yellowZenHealRate = $blueZenHealRate = $redZenHealRate = $purpleZenHealRate = 1000;
	$yellowZenAmmoRate = $blueZenAmmoRate = $redZenAmmoRate = $purpleZenAmmoRate = 40;
	$damagedHealDenominator = 2;
	$damagedAmmoDenominator = 5;

   	$padWaitTime = 40;
  	$zenWaitTime = 70;
// ----------------------------------------------------------------------------------
// Yellow Healing Pad Functionality
function YellowHeal::trigger::onEnter(%this, %object){
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_HEALPAD, true, true);
}
function YellowHeal::trigger::onContact(%this, %object){
   	Zen::work(%this, %object, $yellowHealRate, 0, 0, true); 
}
function YellowHeal::structure::onDestroyed(%this, %vehicleId){
	bigExplosion(%this);
	$yellowHealRate = $yellowHealRate / $damagedHealDenominator;
}
// Yellow Ammo Pad Functionality
function YellowAmmo::trigger::onEnter(%this, %object){
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_AMMOPAD, true, true);  
}
function YellowAmmo::trigger::onContact(%this, %object){
   	Zen::work(%this, %object, 0, $yellowAmmoRate, $padWaitTime, true); 
}
function YellowAmmo::structure::onDestroyed(%this, %vehicleId){
	bigExplosion(%this);
	$yellowAmmoRate = $yellowAmmoRate / $damagedAmmoDenominator;
}
function YellowAmmo::trigger::onLeave(%this, %object){
	Zen::onLeave(%this, %object);
}
// Yellow ZenAll Pad Functionality
function YellowZen::trigger::onEnter(%this, %object){
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_ALLPAD, true, true);  
}
function YellowZen::trigger::onContact(%this, %object){
   	Zen::work(%this, %object, $yellowZenHealRate, $yellowZenAmmoRate, $zenWaitTime, true); 
}
function YellowZen::structure::onDestroyed(%this, %vehicleId){
	bigExplosion(%this);
	$yellowZenAmmoRate = $yellowZenAmmoRate / $damagedAmmoDenominator;
	$yellowZenHealRate = $yellowZenHealRate / $damagedHealDenominator;
}

// ----------------------------------------------------------------------------------
// Blue Healing Pad Functionality
function BlueHeal::trigger::onEnter(%this, %object){
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_HEALPAD, true, true);  
}
function BlueHeal::trigger::onContact(%this, %object){
   	Zen::work(%this, %object, $blueHealRate, 0, 0, true); 
}
function BlueHeal::structure::onDestroyed(%this, %vehicleId){
	bigExplosion(%this);
	$blueHealRate = $blueHealRate / $damagedHealDenominator;
}
// Blue Ammo Pad Functionality
function BlueAmmo::trigger::onEnter(%this, %object){
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_AMMOPAD, true, true);  
}
function BlueAmmo::trigger::onContact(%this, %object){
   	Zen::work(%this, %object, 0, $blueAmmoRate, $padWaitTime, true); 
}
function BlueAmmo::structure::onDestroyed(%this, %vehicleId){
	bigExplosion(%this);
	$blueAmmoRate = $blueAmmoRate / $damagedAmmoDenominator;
}
// Blue ZenAll Pad Functionality
function BlueZen::trigger::onEnter(%this, %object){
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_ALLPAD, true, true);  
}
function BlueZen::trigger::onContact(%this, %object){
   	Zen::work(%this, %object, $BlueZenHealRate, $BlueZenAmmoRate, $zenWaitTime, true); 
}
function BlueZen::structure::onDestroyed(%this, %vehicleId){
	bigExplosion(%this);
	$blueZenAmmoRate = $blueZenAmmoRate / $damagedAmmoDenominator;
	$blueZenHealRate = $blueZenHealRate / $damagedHealDenominator;
}

// ----------------------------------------------------------------------------------
// Red Healing Pad Functionality
function RedHeal::trigger::onEnter(%this, %object){
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_HEALPAD, true, true);  
}
function RedHeal::trigger::onContact(%this, %object){
   	Zen::work(%this, %object, $redHealRate, 0, 0, true); 
}
function RedHeal::structure::onDestroyed(%this, %vehicleId){
	bigExplosion(%this);
	$redHealRate = $redHealRate / $damagedHealDenominator;
}
// Red Ammo Pad Functionality
function RedAmmo::trigger::onEnter(%this, %object){
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_AMMOPAD, true, true);  
}
function RedAmmo::trigger::onContact(%this, %object){
   	Zen::work(%this, %object, 0, $redAmmoRate, $padWaitTime, true); 
}
function RedAmmo::structure::onDestroyed(%this, %vehicleId){
	bigExplosion(%this);
	$redAmmoRate = $redAmmoRate / $damagedAmmoDenominator;
}
// Red ZenAll Pad Functionality
function RedZen::trigger::onEnter(%this, %object){
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_ALLPAD, true, true);  
}
function RedZen::trigger::onContact(%this, %object){
   	Zen::work(%this, %object, $redZenHealRate, $redZenAmmoRate, $zenWaitTime, true); 
}
function RedZen::structure::onDestroyed(%this, %vehicleId){
	bigExplosion(%this);
	$redZenAmmoRate = $redZenAmmoRate / $damagedAmmoDenominator;
	$redZenHealRate = $redZenHealRate / $damagedHealDenominator;
}

// ----------------------------------------------------------------------------------
// Purple Healing Pad Functionality
function PurpleHeal::trigger::onEnter(%this, %object){
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_HEALPAD, true, true);  
}
function PurpleHeal::trigger::onContact(%this, %object){
   	Zen::work(%this, %object, $purpleHealRate, 0, 0, true); 
}
function PurpleHeal::structure::onDestroyed(%this, %vehicleId){
	bigExplosion(%this);
	$purpleHealRate = $purpleHealRate / $damagedHealDenominator;
}
// Purple Ammo Pad Functionality
function PurpleAmmo::trigger::onEnter(%this, %object){
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_AMMOPAD, true, true);  
}
function PurpleAmmo::trigger::onContact(%this, %object){
   	Zen::work(%this, %object, 0, $purpleAmmoRate, $padWaitTime, true); 
}
function PurpleAmmo::structure::onDestroyed(%this, %vehicleId){
	bigExplosion(%this);
	$purpleAmmoRate = $purpleAmmoRate / $damagedAmmoDenominator;
}
// Purple ZenAll Pad Functionality
function PurpleZen::trigger::onEnter(%this, %object){
   	Zen::onEnter(%this, %object, *IDMULT_CHAT_ALLPAD, true, true);  
}
function PurpleZen::trigger::onContact(%this, %object){
   	Zen::work(%this, %object, $purpleZenHealRate, $purpleZenAmmoRate, $zenWaitTime, true); 
}
function PurpleZen::structure::onDestroyed(%this, %vehicleId){
	bigExplosion(%this);
	$purpleZenAmmoRate = $purpleZenAmmoRate / $damagedAmmoDenominator;
	$purpleZenHealRate = $purpleZenHealRate / $damagedHealDenominator;
}

function bigExplosion(%this){
    blast(%this, 200, 10000, 0);      
}

///////////////////////////////////////////////////////////////////////////////////////////////////
// Handle annoying team hitters (would-be team killers)
///////////////////////////////////////////////////////////////////////////////////////////////////
$countdownFrequency = 1;
$probeFrequency = 1.5;
$teamHitPenalty = 7;
$allowableOffenses = 10;

function vehicle::onAttacked(%this, %attacker)
{
   if( $enableTeamHittingPenalty == True )
   {
      if($server::TeamPlay == true)
      {
         if( %this != %attacker )
         {
            if(getTeam(%this) == getTeam(%attacker))
            {
               if(dataRetrieve(%attacker, "onProbation") != True)
               {
                  dataStore(%attacker, "onProbation", True);
                  schedule("dataStore(" @ %attacker @ ", \"onProbation\", False);", $probeFrequency);         
                  dataStore(%attacker, "totalOffenses", (dataRetrieve(%attacker, "totalOffenses") + $teamHitPenalty));
                  
                  if( dataRetrieve(%attacker, "hasCountdownSpawned") != True)
                  {
                     countdownOffenses(%attacker);
                     dataStore(%attacker, "hasCountdownSpawned", True);
                  }

                  if( dataRetrieve(%attacker, "totalOffenses") >= $allowableOffenses )
                  {
                     violate(%attacker);
                     %player = playerManager::vehicleIdToPlayerNum(%attacker);
                     if(%player != 0)
                     {
                        say(%player, 1234, *IDMULT_SPANKED, "rules_violated.wav");
                     }
                  }
               }   
            }
         }
      }
   }
}

function countdownOffenses(%offender)
{
   if(dataRetrieve(%offender, "totalOffenses"))
   {
      dataStore(%offender, "totalOffenses", dataRetrieve(%offender, "totalOffenses") - 1);
      schedule("countdownOffenses(" @ %offender @ ");", $probeFrequency);
   }
   else
   {
      dataStore(%offender, "hasCountdownSpawned", False);
   }
} 