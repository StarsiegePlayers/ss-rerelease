// FILENAME:	Starsiege_Football.cs				  
//
// AUTHORS:  	Brother Youth in Asia of the clan NHMK
//
// READER'S NOTE: Someone was in an awful good mood when this script was made...
//                -- Boss Hogg
//
//------------------------------------------------------------------------------

$missionName = "Starsiege_Football";

exec("multiplayerStdLib.cs");

$BlueScore = 0;
$RedScore = 0;
$PointInterval = 7;
$WinScore = $PointInterval * 6;
$BallCarrierKillValue = 7;
$FumbleTakeUpsValue = 5;
$FumblesValue = 4;
$TouchDownValue = 7;
$MaxFouls = 3;
$PenaltyTime = 60;
$GameOver = "false";
$GoalMade = "false";

//--------------------------------------------------------------------------------

	
function setRules(){
	echo("function setRules()");

   // compose a big "rich text" string of the rules to be displayed in the 
   // game info panel
   
   %rules = "<tIDMULT_FOOTBALL_GAMETYPE>"       @        
            "<tIDMULT_FOOTBALL_MAPNAME>"        @ 
            $missionName                        @  
            "<tIDMULT_FOOTBALL_OBJECTIVES>"     @
            "<tIDMULT_FOOTBALL_SCORING_1>"      @
            "<tIDMULT_FOOTBALL_SCORING_2>"      @
            $TouchDownValue                     @
            "<tIDMULT_FOOTBALL_SCORING_3>"      @
            "<tIDMULT_FOOTBALL_SCORING_4>"      @
            $WinScore                           @
            "<tIDMULT_FOOTBALL_SCORING_5>"      @
            "<tIDMULT_STD_ITEMS>"               @
            "<tIDMULT_FOOTBALL_ENDZONE>"        @
            "<tIDMULT_FOOTBALL_GLOW>"           @
            "<tIDMULT_FOOTBALL_PENALTY_1>"      @
            $MaxFouls                           @
            "<tIDMULT_FOOTBALL_PENALTY_2>"      @
            timeDifference($PenaltyTime, 0)     @
            "<tIDMULT_FOOTBALL_PENALTY_3>"      @
            "<tIDMULT_FOOTBALL_MARKERS>";
  
   // display this string in the panel
   setGameInfo(%rules);
}

// anything that redefines the rules needs to do this
// get the rules panel up and running
// this has to be called after the definition of setRules
setRules();

//----------------------------------------------

function setDefaultMissionOptions()
{
	echo("function setDefaultMissionOptions()");

   $server::AllowTeamRed      = true;
   $server::AllowTeamBlue     = true;
   $server::AllowTeamYellow   = false;
   $server::AllowTeamPurple   = false;

   $server::TeamPlay          = true;
   $server::AllowDeathmatch   = false;
   $server::AllowTeamPlay     = true;	

   $server::disableTeamRed    = false;
   $server::disableTeamBlue   = false;
   $server::disableTeamYellow = true;
   $server::disableTeamPurple = true;
}

function onMissionStart(%playerNum)
{
	echo("function onMissionStart(%playerNum)");
	cls();

	//reinitialize scores of players who were in previous game
	%count = playerManager::getPlayerCount();
	for(%i = 0; %i < %count; %i = %i + 1)	{
		%curPlayerNum = playerManager::getPlayerNum(%i);
		initStats(%curPlayerNum);
	}

}		

function onMissionLoad()
{
	echo("function onMissionLoad()");

   cdAudioCycle("Yougot", "Terror", "Cloudburst", "Mechsoul"); 
}

function initStats(%playerNum){
	echo("function initStats(%playerNum)");
	
	dataStore(%playerNum, "ballCarrierKills", 0);
	dataStore(%playerNum, "fumbleTakeUps", 0);
	dataStore(%playerNum, "fumbles", 0);
	dataStore(%playerNum, "touchDowns", 0);
	dataStore(%playerNum, "fouls", 0);
}

//-------------------------------------------------------------
function player::onAdd(%playerNum){
	echo("function player::onAdd(%playerNum)");

	dataStore(%playerNum, "onField", 0);

   initStats(%playerNum);

   say(%playerNum, 0, *IDMULT_FOOTBALL_WELCOME);
}

//-------------------------------------------------------------
function vehicle::onAdd(%vehicleId){
	echo("function vehicle::onAdd(%vehicleId)");

	%playerNum = playerManager::vehicleIdToPlayerNum(%vehicleId);

	// Check to make sure this person isn't cheating to get out of the foul box
	%playersTeam = getTeam(%playerNum);
	if(%playersTeam == *IDSTR_TEAM_YELLOW || %playersTeam == *IDSTR_TEAM_PURPLE){
		sendToPenaltyBox(%vehicleId);
		return;
	}

	// Drop them at a point on their ten yard line
	dropAtTenYardLine(%vehicleId);

	// Remember that this player is currently on the field
	dataStore(%playerNum, "onField", 1);

	%ballCarrier = getItNum();
	
	// If the other team made a touchdown &&
	// no one has the ball &&
	// this person isn't going to the foul box &&
	// this person teams is to get the ball, give them the ball
	if(($GoalMade == *IDSTR_TEAM_BLUE || $GoalMade == *IDSTR_TEAM_RED) &&  getTeam(%playerNum) != $GoalMade){
		schedule("$GoalMade = \"false\";", 7);			
		giveBall(%playerNum, "true");
	}
	// Else this herc is the first in the game, give them the ball...
	else if($GoalMade == "false" && %ballCarrier == "false"){
		schedule("$GoalMade = \"false\";", 7);			
		giveBall(%playerNum, "true");
	}
	// Else no one has the ball and someone should at least get it!!!
}

// AREN"T SURE IF WE WANT THIS SINCE THE FLAG IS GIVEN TO HERCS, NOT PLAYERS????
// --------------------------------------------------------------------------
function player::onRemove(%playerNum){
	echo("function player::onRemove(%playerNum)");
	// Are they carrying the ball?  Give it to someone on the opposing team
	// and if no one else is on their team, give it to the other team...
	if(getItNum() == %playerNum){
		giveBallToOtherTeam("true", "true");
	}
}

// A vehicle is destroyed
// Is it a team kill?
// Is it a flag carrier?
// --------------------------------------------------------------------------
function vehicle::onDestroyed(%destroyed, %destroyer){
	echo("function vehicle::onDestroyed(%destroyed, %destroyer)");

	// Who is this player?
	%playerNum = playerManager::vehicleIdToPlayerNum(%destroyed);

	// Who destroyed them?
	%playerDestroyerNum = playerManager::vehicleIdToPlayerNum(%destroyer);  	

	// K.  They're off the field!
	dataStore(%playerNum, "onField", 0);

	// If the destroyer is in the penalty box then give ball to the other team...
	if((getTeam(%playerDestroyerNum) == *IDSTR_TEAM_YELLOW) || (getTeam(%playerDestroyerNum) == *IDSTR_TEAM_PURPLE)){
		giveBallToOtherTeam("true", "true");
		return;
	}

	// A touchdown was made.  But does this person have the ball?  Tell them to drop it
	if($GoalMade != "false"){
		if(%playerNum == getItNum())
			dropBall(%playerNum);
	}

	// If this player didn't kill themselves...
	if(%destroyed != %destroyer){

		// Throw this team killer in the penalty box
		if(getTeam(%playerNum) == getTeam(%playerDestroyerNum)){
//			destroyThisPlayer(%playerDestroyerNum);
			dataStore(%playerDestroyerNum, "fouls", 2);
			Say(0, 0, getName(%playerDestroyerNum) @ *IDMULT_FOOTY_ENTER_PENALTY_BOX);
			addFoul(%playerDestroyerNum);
		}

		// Return if the player killed wasn't it!!!!!!!!!!!!!!!!!!!!!!!!
		if(%playerNum != getItNum())
			return;
		
		// The player killed WAS IT; now check the following conditions!!!!!!!!!!!!!!!1

		// Give the destroyer points for stopping the ball carrier IF they are on opposing teams
		if(getTeam(%playerNum) != getTeam(%playerDestroyerNum)){
			dataStore(%playerDestroyerNum, "ballCarrierKills", getBallCarrierKills(%playerNum) + 1);
		}
				
		// Return if the game's over
		if($GameOver == "true")
			return;

		// Give ball to the destroyer if this person had the ball and destroyer on opposite team & destroyer isOnField
		else if(getTeam(%playerDestroyerNum) != getTeam(%playerNum) && isOnField(%playerDestroyerNum) != 0){
			// Give the ball to the person who killed the ball carrier
			giveBall(%playerDestroyerNum, "true");
		}
		// Destroyer isn't on the field!  Give ball to other team!!!  Doesn't count if the destroyer is a team killer
		else if(getTeam(%playerDestroyerNum) != getTeam(%playerNum)){
			giveBallToOtherTeam("true", "true");
		}
		else{
			giveBallToOtherTeam("false", "true");
		}
	}
	// What if this person did kill themselves by running into the sideline (or whatever) &&
	// They were it
	else if(%playerNum == getItNum()){
		giveBallToOtherTeam("true", "true");
	}
}

// Drops a player on the ten yard line of his/her side
// --------------------------------------------------------------------------
function dropAtTenYardLine(%vehicleId){
	echo("function dropAtTenYardLine(%vehicleId)");
	%playerNum = playerManager::vehicleIdToPlayerNum(%vehicleId);
	if(getTeam(%playerNum) == *IDSTR_TEAM_BLUE){
		%x = randomInt(-750, 682);
		randomTransport(%vehicleId, %x, -591, %x, -591);
		dropPod(-55, 589, 500, %x, -591, 100);
	}else if(getTeam(%playerNum) == *IDSTR_TEAM_RED){
		%x = randomInt(-750, 682);
		randomTransport(%vehicleId, %x, 1776, %x, 1776);
		dropPod(-55, 589, 500, %x, 1776, 100);
	}
}

// A vehicle has been attacked
// If this player is it, do they fumble the ball?
// --------------------------------------------------------------------------
function vehicle::onAttacked(%vehicleId, %attackerVehicleId){
	echo("function vehicle::onAttacked(%vehicleId, %attackerVehicleId)");

	%playerNum = playerManager::vehicleIdToPlayerNum(%vehicleId);
	%attackerNum = playerManager::vehicleIdToPlayerNum(%attackerVehicleId);

	if(getItNum() == %playerNum && %vehicleId != %attackerVehicleId && fumble(140) == "true"){

		// Play a sound to let people know there's been a fumble
		playSound(0, "sfx_reactor_xplo.wav", IDPRF_FAR, %vehicleId);

		// Now let them know over chat
		Say(0, 0, getName(%playerNum) @ *IDMULT_FOOTY_FUMBLES_THE_BALL);
		
		// Remember this player made a fumble
		dataStore(%playerNum, "fumbles", getFumbles(%playerNum) + 1);

		// Remember that the attacker took up the fumbled ball
		dataStore(%attackerNum, "fumbleTakeUps", getFumbleTakeUps(%attackerNum) + 1);

		// Give the attacker the ball now		
		giveBall(%attackerNum, "true");		
	}
}

// Return the playerNum of the current person holding the ball
// If no one is, return "false"
// --------------------------------------------------------------------------
function getItNum(){
	echo("function getItNum()");

	// Go through everyone and return who is IT, if exists
	%count = playerManager::getPlayerCount();
	for(%i = 0; %i < %count; %i = %i + 1)	{
		%temp = playerManager::getPlayerNum(%i);
		if(dataRetrieve(%temp, "isIt") == "Yes"){
			return %temp;
		}	
	}
	// Else return false if no one is IT
	return "false";
}

// Vehicle has gone out of bounds
// Destroy them
// Are they a flag carrier???  Don't worry, on destroyed checks for this case!
// --------------------------------------------------------------------------
function OutOfBounds::trigger::onEnter(%this, %vehicleId){
	echo("function OutOfBounds::trigger::onEnter(%this, %vehicleId)");

	%player = playerManager::vehicleIdToPlayerNum(%vehicleId);

	// Destroy this player and inform them of their sin
	destroyThisPlayer(%player);
	
	// Let them know they've strayed
	Say(%player, %player, *IDMULT_FOOTY_OUT_OF_BOUNDS);
	
	// And give them a foul
	addFoul(%player);
}

// The Red Goal has been entered?
// Is this a touchdown or a foul?
// If a touchdown, onAdd gives ball to the other team
// If player has a ball but is in the wrong goal, then ball to other team
// --------------------------------------------------------------------------
function RedGoal::trigger::onEnter(%this, %vehicleId){
	echo("function RedGoal::trigger::onEnter(%this, %vehicleId)");

	%playerNum = playerManager::vehicleIdToPlayerNum(%vehicleId);

	// If this person has the ball and has reached the goal
	if(getTeam(%playerNum) == *IDSTR_TEAM_BLUE && %playerNum == getItNum()){
		$BlueScore = $BlueScore + $PointInterval;
		$GoalMade = *IDSTR_TEAM_BLUE;

		// Remember this player made the touchdown
		dataStore(%playerNum, "touchDowns", getTouchDowns(%playerNum) + 1);

		// Do the touchdown thing!
		touchDown(%vehicleId);
		return;
	}
		// Player doesn't have the ball anymore (even if they once did...)
	else{
		// Let them know they've strayed
		Say(%playerNum, %playerNum, *IDMULT_FOOTY_OUT_OF_BOUNDS);

		// Destroy this player and inform them of their sin
		destroyThisPlayer(%playerNum);

		// Give them a foul
		addFoul(%playerNum);
	}
}

// 
// --------------------------------------------------------------------------
function BlueGoal::trigger::onEnter(%this, %vehicleId){
	echo("function BlueGoal::trigger::onEnter(%this, %vehicleId)");

	%playerNum = playerManager::vehicleIdToPlayerNum(%vehicleId);
	
	// If this person has the ball and has reached the goal
	if(getTeam(%playerNum) == *IDSTR_TEAM_RED && %playerNum == getItNum()){
		$RedScore = $RedScore + $PointInterval;
		$GoalMade = *IDSTR_TEAM_RED;

		// Remember this player made the touchdown
		dataStore(%playerNum, "touchDowns", getTouchDowns(%playerNum) + 1);

		// Do the touchdown thing
		touchDown(%vehicleId);
	}
		// Player doesn't have the ball anymore (even if they once did...)
	else{
		// Let them know they've strayed
		Say(%playerNum, %playerNum, *IDMULT_FOOTY_OUT_OF_BOUNDS);
	
		// Destroy this player and inform them of their sin
		destroyThisPlayer(%playerNum);

		// Give them a foul
		addFoul(%playerNum);
	}
}

// This player has committed a foul
// Increment their foul count 
// If they now have 3 fouls, throw them in the penalty box
// If that is true and they have the ball, give the ball to the other team
// --------------------------------------------------------------------------
function addFoul(%playerNum){
	echo("function addFoul(%playerNum)");

	// Remember this as a 'foul'
	dataStore(%playerNum, "fouls", getFouls(%playerNum) + 1);

	%numberOfFouls = getFouls(%playerNum);

	// Display the number of this foul if they're not in the foul box
	if(%numberOfFouls <= $MaxFouls)
		Say(%playerNum, 0, *IDMULT_FOOTY_PERSONAL_FOUL @ %numberOfFouls @ ".");
	// They've reached exactly the maximum allowed fouls, 
	// -> throw them in the penalty box
	if(%numberOfFouls == $MaxFouls){

		// Change their colour so they don't accidently get assigned the ball!!!
		%teamColour = getTeam(%playerNum);
		%vehicleId = playerManager::playerNumToVehicleId(%playerNum);
		if(%teamColour == *IDSTR_TEAM_BLUE){
			setTeam(%vehicleId, *IDSTR_TEAM_YELLOW);
		}
		else{
			setTeam(%vehicleId, *IDSTR_TEAM_PURPLE);
		}

		// Kill them because its fun to do so
	    destroyThisPlayer(%playerNum);
	}
}

// Take this player out of the penalty box and throw them on the right team
// --------------------------------------------------------------------------
function leavePenaltyBox(%playerNum){
	echo("function leavePenaltyBox(%playerNum)");

	// Reset their foul count to nothing
	dataStore(%playerNum, "fouls", 0);

	// Let them know they've been freed
	Say(%playerNum, 0, *IDMULT_FOOTY_LEAVING_PENALTY_BOX);

	%vehicleId = playerManager::playerNumToVehicleId(%playerNum);

	// Revert them to their original team colour
	%color = getTeam(%playerNum);
	if(%color == *IDSTR_TEAM_YELLOW){
		setTeam(%vehicleId, *IDSTR_TEAM_BLUE);
	}
	else{
		setTeam(%vehicleId, *IDSTR_TEAM_RED);
	}

	// Now we kill the idiot again cause it's fun to watch them topple!!!  AHAHAHAHAHAH
	destroyThisPlayer(%playerNum);
}

// This is called if we know a touchdown has been made by %vehicleId
// Do a flyby of this herc, then check if the game has been won.
// If game is won, call the appropriate function otherwise destroy everyone
// --------------------------------------------------------------------------
function touchDown(%vehicleId){
	echo("function touchDown(%vehicleId)");

	setFlyByCamera(%vehicleId, 30, 100, 20);

	// If no one has one the game yet,
	// Display the message for this touchdown and destroy everyone
	if(checkGameWon() == "false"){
		schedule("destroyEveryone();", 2);		// Kill dem all!
		schedule("messageScoreBox(false);", 3);
	}	
}

// Create message windows for touchdown scores at regular touchdowns and game end
// --------------------------------------------------------------------------
function messageScoreBox(%isGameOver, %winningTeam){
	echo("function messageScoreBox(%isGameOver, %winningTeam)");

	if(%isGameOver == "false"){
		// Let everyone know a touchdown was made
		%message = $GoalMade @ *IDMULT_FOOTY_MADE_TOUCHDOWN @ $RedScore @ "\n" @
				*IDMULT_FOOTY_BLUE_SCORE @ $BlueScore @ "\n";
	}
	else{
		// Let everyone know a touchdown has been made and it's the end of the game
		%message = $GoalMade @ *IDMULT_FOOTY_END1 @ $RedScore @ "\n" @
				*IDMULT_FOOTY_BLUE_SCORE @ $BlueScore @ "\n\n" @ $winningTeam @ *IDMULT_FOOTY_END2 @
				*IDMULT_FOOTY_END3 @ getMostValuablePlayer() @ *IDMULT_FOOTY_END4;
	}

	// Tell em what the heck's going on
	say(0, 0, %message);
	// And reiterate it with a message window!
	messageBox(0, %message);	
}

// Destroy this player
// --------------------------------------------------------------------------
function destroyThisPlayer(%playerNum){
	echo("function destroyThisPlayer(%playerNum)");
	// Convert the player number into a vehicle ID
	%vehicleId = playerManager::playerNumToVehicleId(%playerNum);

	// Kill the bloody git!
	damageObject(%vehicleId, 10000);	// Destroy!
	damageObject(%vehicleId, 10000);	// Destroy!!
	damageObject(%vehicleId, 10000);	// DESTROY!!!
	damageObject(%vehicleId, 10000);	// Lets get it across to them!!!
	damageObject(%vehicleId, 10000);	// Just in case they're deaf!!!!!
	damageObject(%vehicleId, 10000);	// Hehe
}

// Destroy every herc on the field
// We do this because we are Gods, of course :)
// --------------------------------------------------------------------------
function destroyEveryone(){
	echo("function destroyEveryone()");

	// Spot each player and destroy them!
	%count = playerManager::getPlayerCount();
	for(%i = 0; %i < %count; %i = %i + 1)	{
		%temp = playerManager::getPlayerNum(%i);
		destroyThisPlayer(%temp);
	}
}

// Has the game been won?
// --------------------------------------------------------------------------
function checkGameWon(){
	echo("function checkGameWon()");

	// Games isn't over, return false
	if($BlueScore != $WinScore && $RedScore != $WinScore)
		return "false";

	// Let the rest of the script know this game is over
	$GameOver = "true";

	// Which team won?
	if($BlueScore == $WinScore){
		$winningTeam = *IDSTR_TEAM_BLUE;
	}
	else{
		$winningTeam = *IDSTR_TEAM_RED;
	}

	// On HUD:  Display game over & scores
	// We display the touchdown here too since touchdown() doesn't
	// when the final touchtown is made
	Say( 0, %playerNum, *IDMULT_FOOTY_GAME_OVER );
	Say( 0, %playerNum, $winningTeam @  *IDMULT_FOOTY_WINS_THE_GAME);
	Say( 0, %playerNum, *IDMULT_FOOTY_FINAL_SCORES );

	// Destroy everyone for the heck of it
	schedule("destroyEveryone();", 2);		// Kill dem all!

	// Let them know with a message box
	schedule("messageScoreBox(true);", 3);
		
	// Fadeout and end the game
	if($winningTeam == *IDSTR_TEAM_BLUE){
		// Fade to Blue
		%orderFade = "fadeEvent(0, out, 5, 0,0,1 );";
	}
	else{
		// Fade to Red
		%orderFade = "fadeEvent(0, out, 5, 1,0,0 );";
	}
	// Now do that fade, homeboy!
	schedule(%orderFade, 5.0);

	// Then kick everyone to the wait room!!!
	schedule("missionEndConditionMet();", 10.0);

	// Remember to tell 'm the game was won!
	return "true";
} 

// Initialize the scoreboard, funky boy
// --------------------------------------------------------------------------
function initScoreboard(){
	echo("function initScoreboard()");

   deleteVariables("$ScoreBoard::PlayerColumn*");
   deleteVariables("$ScoreBoard::TeamColumn*");

     $ScoreBoard::TeamColumnHeader1 = "Team Score";

   
     $ScoreBoard::TeamColumnFunction1 = "getTeamScore";

   // Player ScoreBoard column headings
	 $ScoreBoard::PlayerColumnHeader1 = *IDMULT_SCORE_TEAM;
	 $ScoreBoard::PlayerColumnHeader2 = *IDMULT_FOOTY_HAS_BALL;
	 $ScoreBoard::PlayerColumnHeader3 = *IDMULT_SCORE_SCORE;
   	 $ScoreBoard::PlayerColumnHeader4 = *IDMULT_SCORE_CARRIER_KILLS;
	 $ScoreBoard::PlayerColumnHeader5 = *IDMULT_FOOTY_TAKE_UPS;
	 $ScoreBoard::PlayerColumnHeader6 = *IDMULT_FOOTY_TOUCHDOWNS;
	 $ScoreBoard::PlayerColumnHeader7 = *IDMULT_FOOTY_NUM_FUMBLES;

   //Player ScoreBoard column functions
     $ScoreBoard::PlayerColumnFunction1 = "getTeam";
	 $ScoreBoard::PlayerColumnFunction2 = "getBall";
     $ScoreBoard::PlayerColumnFunction3 = "getPlayerTotalPoints";
     $ScoreBoard::PlayerColumnFunction4 = "getBallCarrierKills";
     $ScoreBoard::PlayerColumnFunction5 = "getFumbleTakeUps";
     $ScoreBoard::PlayerColumnFunction6 = "getTouchDowns";
     $ScoreBoard::PlayerColumnFunction7 = "getFumbles";

   // tell server to process all the scoreboard definitions defined above
   serverInitScoreBoard();
}

// Return this team's current score; used for scoreboard
// --------------------------------------------------------------------------
function getTeamScore(%team){
	%team = getTeamNameFromTeamId(%team);
	if(%team == *IDSTR_TEAM_BLUE)
		return $BlueScore;
    if(%team == *IDSTR_TEAM_RED)
		return $RedScore;
	return 0;
}

function getFouls(%playerNum){
	return dataRetrieve(%playerNum, "fouls");
}

function getBallCarrierKills(%playerNum){
	return dataRetrieve(%playerNum, "ballCarrierKills");
}
function getBallCarrierKillPoints(%playerNum){
	return getBallCarrierKills(%playerNum) * $BallCarrierKillValue;
}
function getFumbleTakeUps(%playerNum){
	return dataRetrieve(%playerNum, "fumbleTakeUps");
}
function getFumbleTakeUpPoints(%playerNum){
	return getFumbleTakeUps(%playerNum) * $FumbleTakeUpsValue;
}
function getFumbles(%playerNum){
	return dataRetrieve(%playerNum, "fumbles");
}
function getTouchDowns(%playerNum){
	return dataRetrieve(%playerNum, "touchDowns");
}
function getTouchDownPoints(%playerNum){
	return getTouchDowns(%playerNum) * $TouchDownValue;
}
function getPlayerTotalPoints(%playerNum){
	%total = getBallCarrierKillPoints(%playerNum);
	%total = %total + getFumbleTakeUpPoints(%playerNum);
	%total = %total + getTouchDownPoints(%playerNum);
	return %total;
}
function getBall(%playerNum){
	if(%playerNum == getItNum()){
		return "I have it!!!";
	}
	return "-";
}

function getMostValuablePlayer(){
	%hiScore = 0;
	%count = playerManager::getPlayerCount();
	for(%i = 0; %i < %count; %i = %i + 1)	{
		%temp = playerManager::getPlayerNum(%i);
		if(getPlayerTotalPoints(%temp) >= %hiScore){
			%MVP = %temp;
			%hiScore = getPlayerTotalPoints(%MVP);
		}
	}
	return getName(%MVP);
}

// Give ball to this player
// If %message then let everyone know they have it
// --------------------------------------------------------------------------
function giveBall(%playerNum, %message){
	echo("function giveBall(%playerNum, %message)");

	// Make sure no one else has the ball
	if(getItNum() != "false"){
		dropBall(getItNum());
	}

	%vehicleId = playerManager::playerNumToVehicleId(%playerNum);

	// Make this player glow
	setVehicleSpecialIdentity(%vehicleId, true, *IDSTR_TEAM_PURPLE);
	
	// Remember that they're it!
	dataStore(%playerNum, "isIt", "Yes");

	// Display a message if we're told to
	if(%message == "true")
		Say( 0, 0, getName( %playerNum ) @ " [" @ getTeam(%playerNum) @ "]" @ *IDMULT_FOOTY_HAS_THE_BALL );
}

// Take ball from this player
// --------------------------------------------------------------------------
function dropBall(%playerNum){
	echo("function dropBall(%playerNum)");

	%vehicleId = playerManager::playerNumToVehicleId(%playerNum);

	// Make sure they aren't glowing any more
	setVehicleSpecialIdentity(%vehicleId, false);

	// Then take the ball from the twit
	dataStore(%playerNum, "isIt", "No");
}

// Give the ball to someone on this team or
// someone on the other team, depending on %toOtherTeam true/false ???
// %message = "true" means print the accompanying message
// --------------------------------------------------------------------------
function giveBallToOtherTeam(%toOtherTeam, %message){
	echo("function giveBallToOtherTeam(%toOtherTeam, %message)");

	// What team currently has the ball?  What team doesn't?
	%currentTeam = getTeam(getItNum());
	if(%currentTeam == *IDSTR_TEAM_BLUE || %currentTeam == *IDSTR_TEAM_YELLOW){
		%otherTeam =  *IDSTR_TEAM_RED;
	}
	else{
		%otherTeam = *IDSTR_TEAM_BLUE;
	}

	// Just in case no one has the ball
	// Return and the next player added should get it...  
	if(%currentTeam == "false")
		return;

	// Which team is going to get the ball
	if(%toOtherTeam == "true"){
		%teamToGetBall = %otherTeam;
	}
	else{
		%teamToGetBall = %currentTeam;
	}

	dropBall(getItNum());

	%newCarrier = getRandomPlayerOnField(%teamToGetBall);
	if(%newCarrier == "false")
		%newCarrier = getRandomPlayerOnField(%otherTeam);
		
	// Now give that person the ball if they exist
	if(%newCarrier != "false")
		giveBall(%newCarrier, "true");		
}

function getRandomPlayerOnField(%team){
	echo("function getRandomPlayerOnField(%team)");

	// How many people currently in the game?
	%count = playerManager::getPlayerCount();

	%playersOnField = 0;
	// Count the number of people on this team that are currently on the field
	for(%i = 0; %i < %count; %i = %i + 1){
		%temp = playerManager::getPlayerNum(%i);
		if(getTeam(%temp) == %team && dataRetrieve(%temp, "onField") == 1){
			%playersOnField = %playersOnField + 1;
		}	
	}

	%checker = 0;
	%newCarrier = "false";
	%playerToReturn = randomInt(1, %playersOnField);
	// Go through and find the %playerToGetBall person who's on the appropriate team
	for(%i = 0; %i < %count; %i = %i + 1){
		%temp = playerManager::getPlayerNum(%i);
		if(getTeam(%temp) == %team && dataRetrieve(%temp, "onField") == 1){
			%checker = %checker + 1;
			if(%checker == %playerToReturn){
				%newCarrier = %temp;
				break;
			}
		}	
	}
	return %newCarrier;
}

// Check for a fumble; chances are 1 out of %top that a player fumbles the ball
// --------------------------------------------------------------------------
function fumble(%top){
	echo("function fumble(%top)");

	%temp = randomInt(1, %top);
	if(%temp == 2){
		return "true";	// Yes, a fumble has occurred
	}
	return "false";	// Nope, no fumble here duder!
}

// Teleport a player to the penalty box
// And give them the appropriate messages!
// --------------------------------------------------------------------------
function sendToPenaltyBox(%vehicleId){
	echo("function sendToPenaltyBox(%vehicleId)");
	%playerNum = playerManager::vehicleIdToPlayerNum(%vehicleId);

	%do1 = "";
	%do1 = strcat(%do1, "MessageBox(", %playerNum);
	%do1 = strcat(%do1, ", *IDMULT_FOOTY_TOO_MANY_FOULS);");
	schedule(%do1, 3);

	%teamColour = getTeam(%playerNum);

	if(getFouls(%playerNum) == $MaxFouls){

		%do2 = "";
		%do2 = strcat(%do2, "Say(0, 0, \"", getName(%playerNum));
		%do2 = strcat(%do2, "\" @ *IDMULT_FOOTY_PENALTY_BOX);");
		schedule(%do2, 3);

		dataStore(%playerNum, "fouls", getFouls(%playerNum) + 1);
		
		setHudTimer($PenaltyTime, -1, *IDMULT_FOOTY_PENALTY_OVER_IN, 1, %playerNum);

		schedule("leavePenaltyBox(" @ %playerNum @ ");", 60);
	}

	// The player should already be their penalty colour as
	// Changed by addFouls()
	if(%teamColour == *IDSTR_TEAM_YELLOW){
		randomTransport(%vehicleId, -1416, 529, -1301, 646);
		dropPod(-55, 589, 1000, -1361, 590, 175);
	}
	else{
		randomTransport(%vehicleId, 1270, 531, 1378, 646);
		dropPod(-55, 589, 1000, 1329, 595, 190);
	}
}

function EasterEgg::trigger::onEnter(%this, %vehicleId){
	randomTransport(%vehicleId, 31.9, -831.8, 31.9, -831.8);
}

function isOnField(%playerNum){
	return dataRetrieve(%playerNum, "onField");
}