// FILENAME: Server_DM_ALL.cs
//
//Regular Deathmatch Server with no limitations


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
$server::Hostname =                                strcat($Location, ": DM All");

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
$server::TeamPlay =                                FALSE;

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

//-------------------------------------------------
// Mission rotation list. This is the order the
// worlds will cycle in a game with either the frag
// or time limits set.
//-------------------------------------------------
$MissionCycling::Stage0 =                          "DM_Avalanche";
$MissionCycling::Stage1 =                          "DM_Terran_conquest";
$MissionCycling::Stage2 =                          "DM_Bloody_Brunch";
$MissionCycling::Stage3 =                          "DM_City_on_the_Edge";
$MissionCycling::Stage4 =                          "DM_Cold_Titan_Night";
$MissionCycling::Stage5 =                          "DM_Fear_in_Isolation";
$MissionCycling::Stage6 =                          "DM_Heavens_Peak";
$MissionCycling::Stage7 =                          "DM_Impact";
$MissionCycling::Stage8 =                          "DM_Lunacy";
$MissionCycling::Stage9 =                          "DM_Mercury_Rising";
$MissionCycling::Stage10 =                         "DM_Moonstrike";
$MissionCycling::Stage11 =                         "DM_Requiem_for_Gen_Lanz";
$MissionCycling::Stage12 =                         "DM_Sacrifice_to_Bast";
$MissionCycling::Stage13 =                         "DM_State_of_Confusion";
$MissionCycling::Stage14 =                         "DM_The_Guardian";
$MissionCycling::Stage15 =                         "DM_Twin_Siege";

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

