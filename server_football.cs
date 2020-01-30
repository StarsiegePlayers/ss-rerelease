// FILENAME: Server_football.cs
//
//Starsiege Football server. No drop in progress


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
$server::Hostname =                                strcat($Location, ": SS Football");

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
$server::TimeLimit =                               30;

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
$server::AllowTeamRed =                              TRUE  ;
$server::AllowTeamYellow =                          FALSE  ;
$server::AllowTeamBlue =                             TRUE  ;
$server::AllowTeamPurple =                          FALSE  ;

//-------------------------------------------------
// Mission rotation list. This is the order the
// worlds will cycle in a game with either the frag
// or time limits set.
//-------------------------------------------------
$MissionCycling::Stage0 =                          "Starsiege_football";

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

	//weapons
	allowWeapon(                                       all,     FALSE  );
	allowWeapon(                                           101,  TRUE  );      //Laser
	allowWeapon(                                           102,  TRUE  );      //Heavy Laser
	allowWeapon(                                           103,  TRUE  );      //Comp Laser
	allowWeapon(                                           104,  TRUE  );      //Twin Laser
	allowWeapon(                                           105,  TRUE  );      //Emp
	allowWeapon(                                           106,  TRUE  );      //ELF
	allowWeapon(                                           107,  TRUE  );      //Blaster
	allowWeapon(                                           108, FALSE  );      //Heavy Blaster
	allowWeapon(                                           109, FALSE  );      //PBW
	allowWeapon(                                           110, FALSE  );      //Plasma
	allowWeapon(                                           111,  TRUE  );      //Blink Gun
	allowWeapon(                                           112, FALSE  );      //Qgun
	allowWeapon(                                           113, FALSE  );      //MFAC
	allowWeapon(                                           114,  TRUE  );      //Nano Infuser
	allowWeapon(                                           115,  TRUE  );      //Nanite Cannon
	allowWeapon(                                           116,  TRUE  );      //Autocannon
	allowWeapon(                                           117,  TRUE  );      //Hvy Autocannon
	allowWeapon(                                           118,  TRUE  );      //EMC Autocannon
	allowWeapon(                                           119,  TRUE  );      //Blast Cannon
	allowWeapon(                                           120, FALSE  );      //Hvy Blast Can
	allowWeapon(                                           121, FALSE  );      //Rail Gun
	allowWeapon(                                           124, FALSE  );      //Pit Viper 8
	allowWeapon(                                           125, FALSE  );      //Pit Viper 12
	allowWeapon(                                           126, FALSE  );      //Sparrow 6
	allowWeapon(                                           127, FALSE  );      //Sparrow 10
	allowWeapon(                                           128, FALSE  );      //SWARM 6
	allowWeapon(                                           129, FALSE  );      //Minion
	allowWeapon(                                           130, FALSE  );      //Shrike 8
	allowWeapon(                                           147, FALSE  );      //Aphid
	allowWeapon(                                           131,  TRUE  );      //Arachnitron 4
	allowWeapon(                                           132, FALSE  );      //Arachnitron 8
	allowWeapon(                                           133, FALSE  );      //Arachnitron 12
	allowWeapon(                                           134,  TRUE  );      //Proximity 6
	allowWeapon(                                           135,  TRUE  );      //Proximity 10
	allowWeapon(                                           136,  TRUE  );      //Proximity 15
	allowWeapon(                                           142, FALSE  );      //Radiation Gun
	allowWeapon(                                             3,  TRUE  );      //Disrupter
	allowWeapon(                                           150, FALSE  );      //SMART Gun

	//Components
	//Reactors
	allowComponent(                                        200,  TRUE  );      //Human Micro Reactor
	allowComponent(                                        201,  TRUE  );      //Small Human Reactor 2 -- small
	allowComponent(                                        202,  TRUE  );      //Medium Human Reactor 1 Standard
	allowComponent(                                        203,  TRUE  );      //Medium Human Reactor 2 medium
	allowComponent(                                        204,  TRUE  );      //Large Human Reactor 1 -- large
	allowComponent(                                        205,  TRUE  );      //Large Human Reactor 2-- Maxim
	allowComponent(                                        225,  TRUE  );      //Small Cybrid Reactor 1 -- Alpha
	allowComponent(                                        226,  TRUE  );      //Small Cybrid Reactor 2-- Beta
	allowComponent(                                        227,  TRUE  );      //Medium Cybrid Reactor 1 -- Gamma
	allowComponent(                                        228,  TRUE  );      //Medium Cybrid Reactor 2--delta
	allowComponent(                                        229,  TRUE  );      //Large Cybrid Reactor 1--epsilon
	allowComponent(                                        230,  TRUE  );      //Large Cybrid Reactor 2--theta
	//Shields
	allowComponent(                                        300, FALSE  );      //Human Standard Shield
	allowComponent(                                        301, FALSE  );      //Human Protector Shield
	allowComponent(                                        302, FALSE  );      //Human Guardian Shield
	allowComponent(                                        303, FALSE  );      //Human FastCharge Shield
	allowComponent(                                        304, FALSE  );      //Human Centurian Shield
	allowComponent(                                        305, FALSE  );      //Human Repulsor Shield
	allowComponent(                                        306, FALSE  );      //Human Titan Shield
	allowComponent(                                        307, FALSE  );      //Human Medusa Shield
	allowComponent(                                        326, FALSE  );      //Cybrid Alpha Shield
	allowComponent(                                        327, FALSE  );      //Cybrid Beta Shield
	allowComponent(                                        328, FALSE  );      //Cybrid Gamma Shield
	allowComponent(                                        329, FALSE  );      //Cybrid Delta Shield
	allowComponent(                                        330, FALSE  );      //Cybrid Epsilon Shield
	allowComponent(                                        331, FALSE  );      //Cybrid Zeta Shield
	allowComponent(                                        332, FALSE  );      //Cybrid Eta Shield
	allowComponent(                                        333, FALSE  );      //Cybrid Theta Shield

	//Sensors
	allowComponent(                                        400,  TRUE  );      //Basic Human Sensor
	allowComponent(                                        401,  TRUE  );      //Long Range Sensor -- Ranger
	allowComponent(                                        408,  TRUE  );      //Standard Human Sensor
	allowComponent(                                        409,  TRUE  );      //Human Longbow Sensor
	allowComponent(                                        410,  TRUE  );      //Human Infiltrator Sensor
	allowComponent(                                        411,  TRUE  );      //Human Crossbow Sensor
	allowComponent(                                        412,  TRUE  );      //Human Ultralight Sensor
	allowComponent(                                        413,  TRUE  );      //Human Hound Dog Sensor
	allowComponent(                                        414,  TRUE  );      //Thermal Sensor
	allowComponent(                                        426,  TRUE  );      //Basic Cybrid Sensor (Alpha)
	allowComponent(                                        427,  TRUE  );      //Long Range Cybrid Sensor (Beta)
	allowComponent(                                        428,  TRUE  );      //Standard Cybrid Sensor (Gamma)
	allowComponent(                                        429,  TRUE  );      //Cybrid Longbow Sensor (Delta)
	allowComponent(                                        430,  TRUE  );      //Cybrid Infiltrator Sensor (Epsilon)
	allowComponent(                                        431,  TRUE  );      //Cybrid Crossbow Sensor (Zeta)
	allowComponent(                                        432,  TRUE  );      //Cybrid Ultralight Sensor (Eta)
	allowComponent(                                        433,  TRUE  );      //Cybrid Hound Dog Sensor (Theta)
	allowComponent(                                        434,  TRUE  );      //Motion Detector (Iota)

	//Engines
	allowComponent(                                        100,  TRUE  );      //Human Light Vehicle Engine
	allowComponent(                                        101,  TRUE  );      //Human High Output Light Engine
	allowComponent(                                        102,  TRUE  );      //Human Agile Light Engine
	allowComponent(                                        103,  TRUE  );      //Human Standard Medium Engine
	allowComponent(                                        104,  TRUE  );      //Human High Output Medium Engine
	allowComponent(                                        105,  TRUE  );      //Human Medium Agility Engine
	allowComponent(                                        106,  TRUE  );      //Human Standard Heavy Engine
	allowComponent(                                        107,  TRUE  );      //Human Improved Heavy Engine
	allowComponent(                                        108,  TRUE  );      //Human Heavy Cruise Engine
	allowComponent(                                        109,  TRUE  );      //Human High Output Heavy Engine
	allowComponent(                                        110,  TRUE  );      //Human Agile Heavy Engine
	allowComponent(                                        111,  TRUE  );      //Human Standard Assault Engine
	allowComponent(                                        112,  TRUE  );      //Human Improved Assault Engine
	allowComponent(                                        113,  TRUE  );      //Human heavy turbine engine
	allowComponent(                                        114,  TRUE  );      //High Output Turbine (HOT)
	allowComponent(                                        115,  TRUE  );      //Human super heavy engine
	allowComponent(                                        128,  TRUE  );      //Cybrid Alpha Light Vehicle Engine
	allowComponent(                                        129,  TRUE  );      //Cybrid Beta Light Agility Engine
	allowComponent(                                        130,  TRUE  );      //Cybrid Gamma Standard Medium Engine
	allowComponent(                                        131,  TRUE  );      //Cybrid Delta Medium Cruise Engine
	allowComponent(                                        132,  TRUE  );      //Cybrid Epsilon Improved Medium Engine
	allowComponent(                                        133,  TRUE  );      //Cybrid Zeta Medium High Output Engine
	allowComponent(                                        134,  TRUE  );      //Cybrid Eta Medium Agility Engine
	allowComponent(                                        135,  TRUE  );      //Cybrid Theta Standard Heavy Engine
	allowComponent(                                        136,  TRUE  );      //Cybrid Iota Heavy High Output Engine
	allowComponent(                                        137,  TRUE  );      //Cybrid Kappa Heavy Agility Engine
	allowComponent(                                        138,  TRUE  );      //Cybrid Lamda Standard Assault Engine
	allowComponent(                                        139,  TRUE  );      //Cybrid Mu Improved Assault Engine
	allowComponent(                                        140,  TRUE  );      //Cybrid Nu High Output Assault Engine
	allowComponent(                                        141,  TRUE  );      //Cybrid Xi Heavy Assault Engine
	allowComponent(                                        142,  TRUE  );      //Cybrid Omicron Heavy Assault Turbine
	allowComponent(                                        143,  TRUE  );      //Cybrid Pi Super Heavy Turbine

	//Armor
	allowComponent(                                        926,  TRUE  );      //Carbon Fiber (CARLAM)
	allowComponent(                                        927,  TRUE  );      //Quad Bonded Metaplas (QBM)
	allowComponent(                                        928,  TRUE  );      //DURAC (Depleteted Uranium)
	allowComponent(                                        929,  TRUE  );      //Ceramic
	allowComponent(                                        930,  TRUE  );      //Crystaluminum
	allowComponent(                                        931, FALSE  );      //Quicksilver

	//Internal Components
	allowComponent(                                        800,  TRUE  );      //Human Basic Computer
	allowComponent(                                        801,  TRUE  );      //Human Improved Computer
	allowComponent(                                        802,  TRUE  );      //Human Advanced Computer
	allowComponent(                                        805,  TRUE  );      //Cybrid Basic Systems Control
	allowComponent(                                        806,  TRUE  );      //Cybrid Enhanced Systems Control
	allowComponent(                                        807,  TRUE  );      //Cybrid Advanced Systems Control
	allowComponent(                                        810,  TRUE  );      //Guardian ECM
	allowComponent(                                        811, FALSE  );      //Doppleganger ECM
	allowComponent(                                        812,  TRUE  );      //Cybrid Alpha ECM
	allowComponent(                                        813, FALSE  );      //Cybrid Beta ECM
	allowComponent(                                        820, FALSE  );      //Thermal Diffuser
	allowComponent(                                        830,  TRUE  );      //Chameleon
	allowComponent(                                        831,  TRUE  );      //Cuttlefish cloak
	allowComponent(                                        840, FALSE  );      //Shield Modulator
	allowComponent(                                        845, FALSE  );      //Shield Capacitor
	allowComponent(                                        850, FALSE  );      //Shield Amplifier (increases shield constant)
	allowComponent(                                        860, FALSE  );      //Laser Targeting Module
	allowComponent(                                        865,  TRUE  );      //Extra Battery
	allowComponent(                                        870,  TRUE  );      //Reactor Capacitor
	allowComponent(                                        875,  TRUE  );      //Field Stabilizer
	allowComponent(                                        880, FALSE  );      //Rocket Booster
	allowComponent(                                        885, FALSE  );      //Turbine Boost
	allowComponent(                                        890,  TRUE  );      //NanoRepair
	allowComponent(                                        900,  TRUE  );      //Angel Life Support
	allowComponent(                                        910, FALSE  );      //Agrav Generator
	allowComponent(                                        912,  TRUE  );      //ElectroHull
	allowComponent(                                        914,  TRUE  );      //UAP
}

