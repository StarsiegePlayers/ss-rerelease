
//---------------------------------------------------------
Starsiege Patch to Version 1.004 


Notes on MODs and Hacking:
There has been some confusion about MODs and Hacking.
Starsiege is a very open-ended game system there are
hundreds of thousands of changes and customizations that
you can make and set up your own MODed servers.  Dynamix
strongly encourages such activity. Hacking is when someone
circumvents the settings on a server generally giving
themselves an unfair advantage on the battle field.  This
activity is STRONGLY frowned upon by Dynamix.



This patch addresses the following:

- Integrated terrain and interior changes to work around
  some OpenGL driver architectures.  These changes
  considerably improve performance on TNT and TNT2 based
  boards.  Due to the nature of these changes there are
  some operations in the mission editor that behave
  strangely, namely pinning.  I recommend pinning be done
  using Glide or Software mode.

- VTlist supports volumes with more that 1024 files.

- Fixed a hack that allowed vehicles to turn faster than
  normal.

- Fixed firing arc blind spot (introduced in v1.003).

- Fixed a pinning bug that would cause the mission editor
   to crash.

- Fixed drop-down combo boxes in the mission editor.

- Fixed the inability to name simobjects.

- Fixed a crash that occurred when saving a mission several
   times in a single session.

- Fixed bad HTML link in mission_editor_readme.html

- Fixed bug where you could not name some objects in the
   mission editor.

- By default these vehicles are now enabled:
      Platinum Adjudicator
      Platinum Executioner
      Harabec's Predator

- Fixed IRC based attack which would cause everyone in a
  given channel to crash.

- Chat wav files with no associated text now show up as
     "player: (no text)"

- Chat wave files longer than 15 seconds are automatically
   ignored.

- Added ability to Ignore the chat from specific players.
   To activate this feature assign a key/button to 'Ignore
   target chat on/off' in the keymap editor under options.
   To ignore a player select them as your target, pressing
   the key once will ignore your target, pressing the key
   again will turn ignore off for that target.

- Added ability to stop/flush all chrrent chat sounds.  To
   activate this feature assign a key/button to
   'Flush Chat Sound Files' in the keymap editor under
   options.
     
- New console command ignorePlayer(id, true/false); has
   been added.  This can be used to block the chat of any
   player. To get the ID of a player use playerList();

- The network protocol has been enhanced in v1.004.  All
   vehicle data is now downloaded from the server.  This
   addresses all the security issues that arose after
   releasing the mission editor and script source.

- Added checks to ensure that players cannot use hacked
   numbers for combat value, vehicle weight, etc to lower
   team values in value based matches.

- Changed hotchatting to work correctly. If you do not have
   a target, chats that go to 'target team' or 'target
   player' do not work. This means that only 'Vape em!',
   which is 'your team' targetted is the only hotchat to
   play without a target in the default hotchats.

- By default the servers will only allow version 1.004 or
   greater clients to attach and play. You can override
   this by setting the console variable
   $allowOldClients=true; By doing so you negate the
   enhancements of the 1.004 server network protocol and
   are open to some client side hacks. You can reverse the
   process by setting the variable to false.

- Team damage now defaults to off.

- Radiation guns no longer can be used to team kill for
   'free'.

- Removed the ability of many cheat commands in
   multiplayer(AllowWeapon, etc). The Server can access
   these for the purposes of setting up mods with 
   FocusServer();


Known Issue:

- Vehicle parameters are not reset between multiplayer
   sessions.  So if you play a modded 1.004 server then
   join a 1.003 server your vehicle may retain some of the
   1.004 server vehicle attributes.  The solution is to
   upgrade the server to v1.004.
 
