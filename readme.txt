Starsiege v1.003r + 1.004r
7/1/21 Update - Installer

------------------------------------------------------------------------------------------------------------------------

Changes since the 10/10/20 Build includes:
* Replaced executable with 1.003 version for better compatibility and bug fixes
  - SS.IO and SS.PW servers have been set to allow for legacy clients
  - You can enable legacy clients on your own servers with "$allowOldClients=true;"
  - Patched 1.004 executable still included as an alternative.
* Replaced mem.dll with a version-independent build for better compatibility
* Glide Wrapper moved from Extras installer to the Main installer
* Replaced OpenGLide with nGlide for better compatibility
* Change the default install directory to %systemdrive%\Dynamix\Starsiege
* Update DxWnd with better Borderless-Fullscreen, and correction for non-standard install paths
* Fixed regression in HE1-HE3 Terrain Warping
* Moved the multiplayer standard libraries to /scripts/scripts.vol (from /multiplayer)
* Updated master.cs and addrbook.cs
* Added recent user-created Skins to Extras

Changes since the 3/14/20 Build includes:
* Updated compatibility with Windows 7, 8.1, 10
* winmm.dll has been replaced with DxWnd for a better experience
* addrbook.cs has been added for game servers not broadcasting to the new master servers

------------------------------------------------------------------------------------------------------------------------

Special Notes for Glide:
* The game now includes the Glide wrapper nGlide by default, and will automatically launch in this mode on startup. 
* The game runs in borderless fullscreen by default in Glide mode, and will scale itself to fit the current monitor resolution automatically.
* Please note that Starsiege only supports the original aspect ratio and resolutions supported by the original 3Dfx hardware in Glide mode. (4:3, up to 1024x768)
* A 4:3 Aspect Ratio is maintained within menus. There are black borders around the window to fill the remaining monitor space, this is by design. 
  - The Black Borders will show up as a second application ("hider") running underneath Starsiege. 
  - When Alt-Tabbing to and from Starsiege, You will need to switch to "hider" first, then to the game itself.
* Ensure that the "3D Hardware Type" option in "Settings" set to "3Dfx Chipset"

Special Notes for OpenGL:
* The game runs in borderless fullscreen by default in OpenGL mode and will scale itself to fit the current resolution automatically.
* 4:3 Aspect Ratio is maintained within menus, and there are black borders around the window to fill the remaining monitor space. 
  - The Black Borders will show up as a second application ("hider") running underneath Starsiege. 
  - When Alt-Tabbing to and from Starsiege, You will need to switch to "hider" first, then to the game itself.
* If you are receiving font display issues, simply change your full-screen resolution 1-2 settings down from your monitor’s native resolution. This has the added benefit of reducing tearing.
  - 720p is ideal for a 1440p monitor, 1080p for a 2160p monitor, some experimentation may be needed here.
* Ensure that the "3D Hardware Type" option in "Settings" set to "Other"

Special Notes for Legacy OSs:
* This build should work on Windows 9X, Windows 2000, Windows XP, and Wine (Linux) however certain patches may need to be removed to run on these platforms.
  - dinput.dll, dxwnd.dll, dxwplay.dll, glide2x.dll, libogg.dll, libvorbis.dll, libvorbisfile.dll, mem.dll
* You will lose several features (Including CD Audio, Glide Support, FullScreen-Windowed Support) by removing these files.
* This is unsupported and your mileage may vary. You're better off installing straight off of disc at this point.

------------------------------------------------------------------------------------------------------------------------

Thanks to the following individuals and groups for their patch contributions:
Cin_HA Crash Fix                PlagueDog
Updated smackw32.dll            PlagueDog
Interior Terrain Crash Fix      PlagueDog
5-Min Crash Fix                 PlagueDog
Various Linux Fixes             PlagueDog, Jenetrix
Specials Skinning Fix           Jenetrix
Directory Repathing             Jenetrix
Updated master.cs               Jenetrix, Wilzuun, Eye
CD Audio Patch                  Jenetrix, S-110, Surprising_Steve, DxWnd Project
Updated 3dhardwarecard.cs       S-110
OpenGL Terrain Warp Fix         S-110
DDoS Crash Fix                  NoFiX, Floodberry, Neo
Vista Mouse Fix                 NoFiX, FLoodberry, Neo, Surprising_Steve, DxWnd Project
TGA Texture Patch               Floodberry, Neo
No CD Patch                     Drake, Fulgore
OpenGL Hooking Fix              Surprising_Steve
Server Crash Immunity Patch     Sentinal, Orogorous
Glide Wrapper                   Zeus Software

And special thanks to:
Cpt.Raynor-Macrage, Izzy, Stormtrooper, Nightfall, Angel-A, ericathesnark, AlsaValderaan
The rest of the Starsiege Discord for their continued support.
------------------------------------------------------------------------------------------------------------------------
