// FILENAME: Server_CTF_ALL.cs
//
//Capture the flag Server with no limitations


exec(serverLocation);
//-------------------------------------------------
//  RC file to start a server automatically
//-------------------------------------------------

//-------------------------------------------------
// Edit the parameters of this file to configure
// your server options. Use notepad, not a word
// processor. File must be saved as text only.
//-------------------------------------------------

//-------------------------------------------------
// Type the name for your server inside the quotes
// Must be less then 22 characters
//-------------------------------------------------
$server::Hostname =                                strcat($Location, ": CTF All");

//-------------------------------------------------
// Password Protection. Leave blank for none.
// Quotes optional.
//-------------------------------------------------
$server::Password =                                 "";

//-------------------------------------------------
// Maximum player limit. 16 is highest recommended.
//-------------------------------------------------
$server::MaxPlayers =                              16;

//-------------------------------------------------
// Number of kills before server cycles.
// If zero, kills are never reset.
//-------------------------------------------------
$server::FragLimit =                               0;

//-------------------------------------------------
// Time limit in minutes before server cycles.
// Must be a positive integer.
//-------------------------------------------------
$server::TimeLimit =                               20;

//-------------------------------------------------
// Mass limit per player
// Must be a positive integer or zero for no limit.
//-------------------------------------------------
$server::MassLimit =                                0;

//-------------------------------------------------
// Combat Value limit per player
// Must be a positive integer or zero for no limit.
//-------------------------------------------------
$server::CombatValueLimit =                         0;

//-------------------------------------------------
// Team play options. TeamPlay false =  deathmatch.
// TeamPlay true = teams
//-------------------------------------------------
$server::TeamPlay =                                true;

//-------------------------------------------------
// Team Mass limit - valid only when teamPlay is true
// Must be a positive integer or zero for no limit.
//-------------------------------------------------
$server::TeamMassLimit =                            0;

//-------------------------------------------------
// Team Combat Value Limit - valid only when teamPlay is true
// Must be a positive integer or zero for no limit.
//-------------------------------------------------
$server::TeamCombatValueLimit =                     0;

//-------------------------------------------------
// Tech Level Limit
// Must be a positive integer less then or
// equal to 128 or zero for no limit.
//-------------------------------------------------
$server::TechLevelLimit =                          0;

//-------------------------------------------------
// Drop In Progress
// Does server allow drop in progress??
//-------------------------------------------------
$server::DropInProgress =                            TRUE  ;

$server::AllowMixedTech =                            TRUE  ;
$server::FactoryVehOnly =                           FALSE  ;
$server::AllowTeamRed =                             FALSE  ;
$server::AllowTeamYellow =                          FALSE  ;
$server::AllowTeamBlue =                             TRUE  ;
$server::AllowTeamPurple =                           TRUE  ;

//-------------------------------------------------
// Mission rotation list. This is the order the
// worlds will cycle in a game with either the frag
// or time limits set.
//-------------------------------------------------
$MissionCycling::Stage0 =                          "CTF_Winter_Wasteland";
$MissionCycling::Stage1 =                          "CTF_Circular_Logic";
$MissionCycling::Stage2 =                          "CTF_Balance_of_Power";
$MissionCycling::Stage3 =                          "CTF_City_on_the_Edge";

//-------------------------------------------------
// Start mission. Defines which mission from the
// rotation list the server starts on.
//-------------------------------------------------
$server::Mission =                                  $MissionCycling::Stage0;

// These items will be allowed by default -- your mission script can change these
// by calling allowVehicle, allowComponent, or allowWeapon
function setAllowedItems()
{
	//Vehicles
	exec("defaultVehicles.cs");
	allowComponent(                                    all,      TRUE  );
	allowWeapon(                                       all,      TRUE  );
}

