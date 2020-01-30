//
// user configuration file
//

if (isFile("defaultPrefs.cs") != "")
{
   exec( "defaultPrefs.cs" );
}

// if the mission editor is on, give access to the console
if ($me::enableMissionEditor) 
   Console::enable(true);

// uncomment this line to enable mission results logging:
// $missionLogFile = "log.txt";

// without this access key, the movie recorder playback screen
// will be automatically loaded after the splash screen (instead of the main menu)
$AccessKey = "2829";

// the player limit for non dedicated servers
$server::NoneDedicatedPlayerLimit = 16;

// Default console stuff
$Console::Prompt        = "% ";
$Console::History       = 25;
$Console::CursorType    = "VERTICAL";
$Console::CursorLines   = 1;
$Console::RepeatsPerSec = 5;

//------------------------------------------------------------------------------
//	Alt camera views
//------------------------------------------------------------------------------
bind( keyboard, make, alt, 1, to, "flybyCamera(5, -6, 40, 2);");
bind( keyboard, make, alt, 2, to, "targetPrimaryCamera(-25, 12);");
bind( keyboard, make, alt, 3, to, "targetSecondaryCamera(-25, 12);");
bind( keyboard, make, alt, 4, to, "combatCamera(L, 50);");
bind( keyboard, make, alt, 5, to, "orbitPlayer(20, 180, 20);");
bind( keyboard, make, alt, 6, to, "orbitPlayer(20,   0, 20);");
bind( keyboard, make, alt, 7, to, "orbitPlayer(20,  90, 20);");
bind( keyboard, make, alt, 8, to, "orbitPlayer(20, 270, 20);");
bind( keyboard, make, alt, 9, to, "focusCamera(player);");

bind( keyboard, make, alt, f5, TO, "meModetoggle();");

