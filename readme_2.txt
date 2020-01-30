
Starsiege Patch to Version 1.003 

This patch addresses the following:

- The Mission Editor can now be used for more information
  on the Mission Editor, see the file Mission_Editor_Readme.html

- 3DFX Voodoo 3 based cards are now fully supported

- Team damage can be turned off on MP servers.  Team damage is ON by
  default (meaning two people on the same team can damage eachother).
  To turn OFF team damage, you must add the following line of code to
  the server mission script or your scripts/autoexec.cs file to take
  effect for all missions:

            $server::TeamDamage = false;

  For dedicated (console) servers, the server script is in your
  Starsiege executable directory, and is named "server_*.cs" (e.g.
  "server_DM_ALL.cs").  For non-dedicated servers you'll have to 
  modify the mission script which is located in the multiplayer
  directory.  In either case, insert the above line at the top of
  the file to turn off team damage.

- OpenGL is available for cards not specifically supported by
  Starsiege.  If any OpenGL driver is available, Starsiege will
  attempt to operate in OpenGL mode, with default (meaning non-card
  specific) OpenGL settings.

- The cheat that allows players to mount artillery guns on 
  conventional vehicles and bring them into multiplayer games has
  been patched.

- Several different cheats to allow hacked vehicles to enter a server
  have been patched.

- The "+connect" command line argument to Starsiege now operates
  correctly.  Starsiege can be instructed to automatically connect to
  a game server  by including the +connect command line argument
  followed by an IP address, and with a port for example the
  following will start Starsiege and immediately connect to the
  Oregon game server at 198.74.40.71 port 29001:

    Starsiege.exe +connect IP:198.74.40.71:29001

  This is especially convenient for those users who wish to attach 
  desktop short cuts to their favorite servers.

- The code to query the secondary and terciary master servers was not 
  functioning properly. If the primary Starsiege master server went
  offline, Starsiege failed to query the other master servers.  This
  bug has been  fixed.

- The allowVehicle(%id, T/F) script command has been modified slightly 
  to prevent/accomodate cheat vehicles.  Previously, allowVehicle()
  only acted on conventional, non-cheat vehicles, so the call:

    allowVehicle(all, false);

  would not restrict cheat vehicles.  Cheaters who gained access to 
  cheat vehicles were able to exploit this bug and join games in
  cheat vehicles.  This routine now acts on ALL vehicles, so the
  following code:

    allowVehicle(all, false);
    allowVehicle(30, true);

  will allow ONLY Emancipators in the game, whereas before it would 
  allow Emancipators and cheat vehicles.
  
- To aid in the restriction of cheat vehicles, the file 
  scripts/disallow.cs has been added.  disallow.cs is called on the
  server *after* the mission is setup and after any server scripts
  are called, so that it has the last word on what vehicles are
  allowed and not allowed.

  This makes it easy to globally allow and disallow vehicles over all 
  multiplayer missions, without modifying every multiplayer .cs file 
  individually.

  By default, disallow.cs disallows all cheat vehicles (and only cheat 
  vehicles).
  
- Several changes have been made for our localization team. One of
  these changes is the ability to specify the ascii returned by all
  key strokes.  Squads may find this useful to support the graphics
  characters many of them have in their names.

- Bug with mines not exploding has been patched.

- Added new scripting function isEqualIP() that allows you to compare
  an IP address to a IP mask.  Useful to ban users.  Script examples
  are available on the website.

- Optimized float to int floting point conversions.

- Did I say MISSION EDITOR !!!