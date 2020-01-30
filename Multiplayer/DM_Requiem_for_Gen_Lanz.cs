// FILENAME:	DM_Requiem_for_General_Lanz.cs
//
// AUTHORS:  	Chupie Doll & Youth in Asia
//------------------------------------------------------------------------------

$missionName = "DM_Requiem_for_Gen_Lanz";

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
     
   	$blastRadius = 100;   // for ammo boxes
	$blastDamage = 4000;   

	iceSounds();	

}

function onMissionLoad(){
   cdAudioCycle("Purge", "Newtech", "SS4", "SS3"); 
   // custom rules for this mission ( teleporter )
   %rules = "<tIDMULT_TDM_GAMETYPE>"   @        
            "<tIDMULT_TDM_MAPNAME>"    @ 
            $missionName               @
            "<tIDMULT_TDM_OBJECTIVES>" @
            "<tIDMULT_TDM_SCORING_1>"  @
            "<tIDMULT_TDM_SCORING_2>"  @
            $killPoints                @
            "<tIDMULT_TDM_SCORING_3>"  @
            "<tIDMULT_TDM_SCORING_4>"  @
            $deathPoints               @
            "<tIDMULT_TDM_SCORING_5>"  @
            "<tIDMULT_TDM_SCORING_6>"  @
            "<tIDMULT_STD_ITEMS>"      @
            "<tIDMULT_STD_TELEPORTER>" @
            "<tIDMULT_STD_HEAL>"       @
            "<tIDMULT_STD_RELOAD>";

   setGameInfo(%rules);
}

function turret::onAdd(%this)
{
	if($server::TeamPlay == false)
	{
		setTeam(%this, *IDSTR_TEAM_NEUTRAL);
	}
}

// Transporter Functionality
// --------------------------------------------------------------------------
function deathmatchTransport(%this, %vehicleId){
	%teamName = getTeam(%vehicleId);
   
	%huh = randomInt(1, 5);
	if(%huh == 1){
		transportToYellowBase(%vehicleId);
	}
	else if(%huh == 2){
		transportToBlueBase(%vehicleId);
	}
	else if(%huh == 3){
		transportToRedBase(%vehicleId);
	}
	else if(%huh == 4){
		transportToPurpleBase(%vehicleId);
	}
	else{
		transportToMountain(%vehicleId);
	}
}
function YellowTransporter::trigger::onEnter(%this, %vehicleId){
	if($server::TeamPlay == false){
		deathmatchTransport(%this, %vehicleId);
		return;
	}

	%teamName = getTeam(%vehicleId);
   
   	if(%teamName == *IDSTR_TEAM_YELLOW){
		%huh = randomInt(1, 4);
		if(%huh == 1){
			transportToBlueBase(%vehicleId);
		}
		else if(%huh == 2){
			transportToRedBase(%vehicleId);
		}
		else if(%huh == 3){
			transportToPurpleBase(%vehicleId);
		}
		else{
			transportToMountain(%vehicleId);
		}
	}
	else{
		transportToHomeBase(%vehicleId);
	}
}
function BlueTransporter::trigger::onEnter(%this, %vehicleId){
	if($server::TeamPlay == false){
		deathmatchTransport(%this, %vehicleId);
		return;
	}

	%teamName = getTeam(%vehicleId);
   
   	if(%teamName == *IDSTR_TEAM_BLUE){
		%huh = randomInt(1, 4);
		if(%huh == 1){
			transportToYellowBase(%vehicleId);
		}
		else if(%huh == 2){
			transportToRedBase(%vehicleId);
		}
		else if(%huh == 3){
			transportToPurpleBase(%vehicleId);
		}
		else{
			transportToMountain(%vehicleId);
		}
	}
	else{
		transportToHomeBase(%vehicleId);
	}
}
function RedTransporter::trigger::onEnter(%this, %vehicleId){
	if($server::TeamPlay == false){
		deathmatchTransport(%this, %vehicleId);
		return;
	}
	%teamName = getTeam(%vehicleId);
   
   	if(%teamName == *IDSTR_TEAM_RED){
		%huh = randomInt(1, 4);
		if(%huh == 1){
			transportToBlueBase(%vehicleId);
		}
		else if(%huh == 2){
			transportToYellowBase(%vehicleId);
		}
		else if(%huh == 3){
			transportToPurpleBase(%vehicleId);
		}
		else{
			transportToMountain(%vehicleId);
		}
	}
	else{
		transportToHomeBase(%vehicleId);
	}
}
function PurpleTransporter::trigger::onEnter(%this, %vehicleId){
	if($server::TeamPlay == false){
		deathmatchTransport(%this, %vehicleId);
		return;
	}
	%teamName = getTeam(%vehicleId);

   	if(%teamName == *IDSTR_TEAM_PURPLE){
		%huh = randomInt(1, 4);
		if(%huh == 1){
			transportToBlueBase(%vehicleId);
		}
		else if(%huh == 2){
			transportToRedBase(%vehicleId);
		}
		else if(%huh == 3){
			transportToYellowBase(%vehicleId);
		}
		else{
			transportToMountain(%vehicleId);
		}
	}
	else{
		transportToHomeBase(%vehicleId);
	}
}

function transportToBlueBase(%vehicleId)
{
	echo("Transporting to Blue Base");
	randomTransport(%vehicleId, 2151, 1078, 2185, 802);
}
function transportToYellowBase(%vehicleId)
{
	echo("Transporting to Yellow Base");
	randomTransport(%vehicleId, 582, -2016, 715, -1968);
}
function transportToRedBase(%vehicleId)
{
	echo("Transporting to Red Base");
	randomTransport(%vehicleId, 669, 3201, 746, 3046);
}
function transportToPurpleBase(%vehicleId)
{
	echo("Transporting to Purple Base");
    randomTransport(%vehicleId, -2948, 66, -3041, -117);
}
function transportToMountain(%vehicleId)
{
      randomTransport(%vehicleId, 56, 667, 60, 675);
}

function transportToHomeBase(%vehicleId)
{
	// What colour is this person?
   %teamName = getTeam(%vehicleId);
   
	// Call the appropriate transport function (above) and get them home
   if(%teamName == *IDSTR_TEAM_YELLOW)
   {
      transportToYellowBase(%vehicleId);
   }
   else if(%teamName == *IDSTR_TEAM_RED)
   {
      transportToRedBase(%vehicleId);
   }
   else if(%teamName == *IDSTR_TEAM_BLUE)
   {
      transportToBlueBase(%vehicleId);
   }
   else
   {
      transportToPurpleBase(%vehicleId);
   }   
}

// The following code is necessary for everything on the mountain
// ------------------------------------------------------------------------------

function ZenTransporter::trigger::onContact(%this, %object)
{
	Zen::work(%this, %object, 100000, 100, 0, false);
	setPosition(%object, -183, 573, 1500);
}
function ball1::structure::onAttacked(%this, %attacker){
	transportToYellowBase(%attacker);
}
function ball2::structure::onAttacked(%this, %attacker){
	transportToBlueBase(%attacker);
}
function ball3::structure::onAttacked(%this, %attacker){
	transportToRedBase(%attacker);
}
function ball4::structure::onAttacked(%this, %attacker){
	transportToPurpleBase(%attacker);
}



