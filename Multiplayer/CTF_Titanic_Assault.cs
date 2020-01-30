// FILENAME:	CTF_Titanic_Assault.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//
// NOTE: This script overrides some of the CTF standard functionality.
//       Cybrids are red, Humans are yellow; no tech mixing. --TSL
//
//------------------------------------------------------------------------------

///////////////////////////////////////////////////////////////////////////////////////////////////
// Cybrids vs Humans      
//
// Any vehicle on the wrong side will get a message and be automatically set to the
// correct team. -- TSL
///////////////////////////////////////////////////////////////////////////////////////////////////

$missionName = "CTF_Titanic_Assault";

$maxFlagCount  = 8;        
$flagValue     = 5;        
$carrierValue  = 2;        
$killPoints    = 1;
$deathPoints   = 1;
$flagTime = 300;

exec("multiplayerStdLib.cs");
exec("CTFstdLib.cs");

$server::HudMapViewOffsetX = -6300;
$server::HudMapViewOffsetY = 2400;

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

	titanSounds();
}   

function onMissionLoad(){
   cdAudioCycle("Yougot", "Purge", "SS1"); 

   %rules = "<tIDMULT_CTF_GAMETYPE>"      @        
            "<tIDMULT_STD_CYBRID_VS_HUMAN_1>"  @
            "<tIDMULT_CTF_MAPNAME>"       @ 
            $missionName                  @  
            "<tIDMULT_CTF_OBJECTIVES>"    @
            "<tIDMULT_CTF_OBJECTIVES_2>"  @
            timeDifference($flagTime,0)   @
            "<tIDMULT_CTF_OBJECTIVES_3>"  @
            "<tIDMULT_CTF_SCORING_1>"     @
            "<tIDMULT_CTF_SCORING_2>"     @
            $flagValue                    @
            "<tIDMULT_CTF_SCORING_3>"     @
            "<tIDMULT_CTF_SCORING_4>"     @
            $carrierValue                 @
            "<tIDMULT_CTF_SCORING_5>"     @
            "<tIDMULT_CTF_SCORING_6>"     @
            $deathPoints                  @
            "<tIDMULT_CTF_SCORING_7>"     @
            "<tIDMULT_CTF_SCORING_8>"     @
            $killPoints                   @
            "<tIDMULT_CTF_SCORING_9>"     @
            "<tIDMULT_CTF_SCORING_10>"    @ 
            $maxFlagCount                 @
            "<tIDMULT_CTF_SCORING_11>"    @
            "<tIDMULT_STD_ITEMS>"         @
            "<tIDMULT_CTF_FLAGS>"         @
            "<tIDMULT_CTF_GLOW>"          @
            "<tIDMULT_STD_HEAL>"          @
            "<tIDMULT_STD_RELOAD_1>"      @
            $PadWaitTime                  @
            "<tIDMULT_STD_RELOAD_2>"      @
            "<tIDMULT_STD_ZEN_1>"         @
            $ZenWaitTime                  @
            "<tIDMULT_STD_ZEN_2>";

   setGameInfo(%rules);
}

function vehicle::onAdd(%this)
{
   // Cybrid vs Humans ... Cybrids must be red, Humans must be yellow
  
   if(getVehicleTechBase(%this) == "H" && getTeam(%this) != *IDSTR_TEAM_BLUE)
   {
      setTeam(%this, *IDSTR_TEAM_BLUE);
      say(PlayerManager::vehicleIdToPlayerNum(%this), 1234, *IDMULT_CHANGING_TEAM_BLUE);
      redrop(%this);
   }
   else if(getVehicleTechBase(%this) == "C" && getTeam(%this) != *IDSTR_TEAM_PURPLE)
   {
      setTeam(%this, *IDSTR_TEAM_PURPLE);
      say(PlayerManager::vehicleIdToPlayerNum(%this), 1234, *IDMULT_CHANGING_TEAM_PURPLE);
      redrop(%this);
   }
   
   %team = getTeam(playerManager::vehicleIdToPlayerNum(%this));
   %color = teamToColor(%team);
   %flagKey = strcat(%color, "FlagCount");

	adjTeamCount(%team, 1);

   //if the flag isn't at the base, but no one has it, correct situation
   if(dataRetrieve(0, %flagKey) && !dataRetrieve(0, strcat(%color, "FlagCarried")))
   {
	   setFlag(%team, true);
   }
}

//--------------------------------------------------------------------------------
//--easter code

function vanishTrigger1::trigger::onAdd(%this)
{
	dataStore(%this, "isActive", true); // bool for trigger active
}
function vanishTrigger1::trigger::onEnter(%this, %object)
{
	triggerOnEnter(1, %object);	
}
function vanishTrigger2::trigger::onAdd(%this)
{
	dataStore(%this, "isActive", false); // bool for trigger active
}

function vanishTrigger2::trigger::onEnter(%this, %object)
{
	triggerOnEnter(2, %object);	
}
function vanishTrigger3::trigger::onAdd(%this, %object)
{
	dataStore(%this, "isActive", false); // bool for trigger active
}
function vanishTrigger3::trigger::onEnter(%this, %object)
{
	triggerOnEnter(3, %object);	
}
function teleportTrigger::trigger::onAdd(%this)
{
	dataStore(%this, "isActive", false); // bool for trigger active
}
function teleportTrigger::trigger::onEnter(%this, %object)
{
	triggerOnEnter(teleport, %object);	
}
function triggerOnEnter(%index, %object)
{
	%thisTrigger = "";
	%nextTrigger = "";
	%chatMsg = "";
	
	if(%index == 1)
	{
		%thisTrigger = getObjectId("MissionGroup\\extra\\vanishTrigger1");
		%nextTrigger = getObjectId("MissionGroup\\extra\\vanishTrigger2");
		%thisFlag =	   getObjectId("MissionGroup\\extra\\flag1");
		%nextFlag =    getObjectId("MissionGroup\\extra\\flag2");
		%chatMsg  = "Trigger 2 activated.";		 	 	
	}

	if(%index == 2)
	{
		%thisTrigger = getObjectId("MissionGroup\\extra\\vanishTrigger2");
		%nextTrigger = getObjectId("MissionGroup\\extra\\vanishTrigger3");
		%thisFlag =	   getObjectId("MissionGroup\\extra\\flag2");
		%nextFlag =    getObjectId("MissionGroup\\extra\\flag3");
		%chatMsg = "Trigger 3 activated.";	
	}

	if(%index == 3)
	{
		%thisTrigger = getObjectId("MissionGroup\\extra\\vanishTrigger3");
		%nextTrigger = getObjectId("MissionGroup\\extra\\teleportTrigger");
		%thisFlag =	   getObjectId("MissionGroup\\extra\\flag3");
		%nextFlag =    getObjectId("MissionGroup\\extra\\teleport");
		%chatMsg = "Teleportation device online."; 
		setShapeVisibility(getObjectId("MissionGroup\\Extra\\fx_tele_t1"), true);	
	}

	if(%index == teleport)
	{
		%thisTrigger = getObjectId("MissionGroup\\extra\\teleportTrigger");
		%nextTrigger = getObjectId("MissionGroup\\extra\\vanishTrigger1");
		%thisFlag =	   getObjectId("MissionGroup\\extra\\teleport");
		%nextFlag =    getObjectId("MissionGroup\\extra\\flag1");
		%chatMsg = "Teleportation Device is inoperable.";	
	}
	
	if(%thisTrigger == "" || %nextTrigger == "")
	{
		echo("bad triggerIds!");
		return;
	}
	
	%isActive = dataRetrieve(%thisTrigger, "isActive");

	if(%isActive != false)
	{
		dataStore(%thisTrigger, "isActive", false);
		dataStore(%nextTrigger, "isActive", true);
		
		if(%index != 3)
		{
			setShapeVisibility(%nextFlag, true);
		}
				
		if(%index == teleport)
		{
			%malfunctionChance = randomFloat(0.0,0.1);
			if(%malfunctionChance > randomFloat(0.0,1.0))
			{
				malfunction(%object);
			}
			else
			{
				teleport(%object);
				setShapeVisibility(getObjectId("MissionGroup\\Extra\\fx_tele_t1"), false);
			}
		}
		else
		{
			setShapeVisibility(%thisFlag, false);
			// chat(%object, 0, %chatMsg);	
		   Say( playerManager::vehicleIdToPlayerNum(%object), %object, %chatMsg );
      }					
	}
	else if(%index == teleport)
	{
		// chat(%object, 0, %chatMsg);
	   Say( playerManager::vehicleIdToPlayerNum(%object), %object, %chatMsg );
   }	
}
function teleport(%object)
{
	%flagNum = randomInt(1,2);
	%x = %y = 0;

	if(%flagNum == 1)
	{
		%x = -5099;
		%y = 4310;
	}

	if(%flagNum == 2)
	{
		%x = -4863;
		%y = 893;
	}
	
	%playerNum = playerManager::vehicleIdToPlayerNum(%object);
	
	Say( %object, %object, "Teleportation initiated." );
   
   	healObject(%object, 600.0);
	reloadObject(%object, 60.0);

	randomTransport(%object, %x, %y, %x, %y);
   	%str = "Say( %object, %object, \"Teleportation complete.\" );";
   	schedule( %str, 2.5 );
}
function malfunction(%object)
{
	Say( %object, %object, "Teleportation initiated." );
   
  	%playerNum = playerManager::vehicleIdToPlayerNum(%object);
	fadeEvent(%playerNum, out, 2.5, 0.5,0.0,0.0);
  	%str = "Say( %object, %object, \"Teleportation malfunction!\" );";
  	schedule( %str, 1.8 );
	
 	schedule(strcat("fadeEvent(", %playerNum, ", in, 1.5, 0.5,0.0,0.0);"), 2.5);
	schedule(strcat("damageObject(", %object, ", 10000);"), 2.5);		
}




