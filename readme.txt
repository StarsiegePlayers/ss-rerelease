STARSIEGE
README FILE
V. 1.02 4/15/99

About This Document:
Thank you for Purchasing Starsiege, the ultimate in giant mechanized combat! 
This document contains last-minute information about Starsiege and other 
troubleshooting information not found in the manual. This README file 
includes information pertinent to general problems and questions you may have 
concerning this software or your computer. Should you experience any problems 
with Starsiege, please refer to this file for additional help in answering 
questions about the game and solving technical difficulties.

CONTENTS:
I:	SYSTEM COMPATIBILITY 
II:	OPENGL
III:	INTERNET SERVICE PROVIDER 
IV:	UPDATES AND PATCHES 
V:	HOSTING A GAME 
VI:	NETWORK PERFORMANCE TUNING 
VII:	KNOWN PROBLEMS 
VIII:	TROUBLESHOOTING 
IX:	CONTACTING SIERRA 


I: SYSTEM COMPATIBILITY 

These are the minimum machine requirements needed to run Starsiege: 
Pentium 166, 32 MB RAM with 3D Graphics Accelerator 
Pentium 200, 32 MB RAM without a Graphics Accelerator 
324MB Hard Disk space (Full Install)
264MB Hard Disk space (Buddy Install)

Platforms: 
Windows 95/98 w/DirectX6
Windows NT 4.0 w/Service Pack 4

Hardware Requirements: 
LAN Card or minimum 28.8 kps modem, 4x CD-ROM 
2D Graphics Card: 
DirectDraw compatible card (minimum SVGA 640x480 @ 256 colors) 
3D Graphics Card: 
3Dfx Glide compatible 3D accelerator (recommended), or OpenGL 
compatible card (see below for more information on OpenGL)
Sound Card Support: 
DirectSound and Directsound3D compatible sound cards 

Peripheral Support: 
Mouse, Keyboard, Joystick, or any compatible DirectInput device 

Network Support: 
Internet, TCP/IP, IPX, minimum 28.8 Kbps modem

Starsiege requires DirectX to run properly on Windows 95/98. If you already 
have DirectX, you may elect not to re-install it. Should you decide to 
install it at a later time, run the dxsetup program located in the DirectX 
folder on the Starsiege Disk #1. If you are running Windows NT 4.0, you must 
have Service Pack 3 installed. If you do not have Service Pack 3 (or greater) 
installed, you will need to download it from WWW.MICROSOFT.COM.

We recommend that you configure Windows with at least 200mb of virtual memory 
before running Starsiege.


II: OPENGL

Starsiege supports OpenGL on video cards that use the nVidia Riva TNT 
chipset, the Intel i740 chipset, and the S3 Savage3D shipset. By the time you 
read this, other cards should be supported. Please see 
www.starsiegeplayers.com for details.

To use OpenGL on a Riva TNT based card, you must have the latest drivers. 
Currently these are the Detonator drivers located in the "\Extras\TNT 
Detonator Drivers" folder on Starsiege Disk #1. To install the Detonator 
drivers, double-click on the "tnt_detonator.exe" application (in the 
aforementioned folder), and follow the on-screen instructions very carefully.

To use OpenGL on any other card, be absolutely sure you have the latest 
drivers from the manufacturer.

To run Starsiege in OpenGL mode, go to the Options screen from the Main Menu. 
In the "Full Screen Mode" drop-down box, select "OpenGL". From the "3D 
Hardware Type" drop-down box, select the chipset used on your video card. 

If you are experiencing low frame rates or "jerkyness" in Starsiege, try 
reducing some of the graphics detail levels (available from the "Graphics" 
tab in the Options menu, or from by pressing F11 during actual game play).  
Minor adjustments in detail levels, especially the "Scenery Detail" can 
significantly impact game performance.

If you experience problems running Starsiege in OpenGL, try reducing the 
color depth of your desktop from 32 bit (True Color) to 16 bit (65536 
colors), using the "Settings" tab in the "Display Properties" control panel.

If you are using OpenGL under Windows 95 and have your taskbar set to "Always 
On Top" (which is the default), you will experience problems IF your taskbar 
has been placed at the top of the desktop or at the left of the desktop.  The 
solution is to drag your taskbar to the bottom of your desktop, or uncheck 
the "Always On Top" box that is located in the "Taskbar Properties" dialog 
box, which is available from the Settings menu in the Start menu.

III: INTERNET SERVER PROVIDER 

To enjoy Starsiege fully, you must be connected to the Internet via a 
reliable Internet Service Provider (ISP). If you do not currently have an 
ISP, install the AT&T WorldNet(R) Service provided on the CD-ROM by running 
the Starsiege setup program. 

IV: UPDATES AND PATCHES 

Running the Auto-Update program from the Starsiege Start menu will check 
automatically for any patches or updates to the game. You must be connected 
to the Internet for this program to work correctly. We also recommend you 
check the Starsiege web site at www.starsiegeplayers.com for the latest 
information. 

V: HOSTING A GAME 

Hosting a game with more than a few players takes a fair amount of bandwidth. 
Hosting over a 28.8 modem connection is not recommended. By default, all 
hosted games are visible only to a network running the TCP/IP protocol such 
as the Internet or a LAN. If you are connected to a LAN running only the IPX 
protocol, you must select the IPX protocol from the connection type box in 
the Create Game menu. Those of you with faster Internet connections may wish 
to run dedicated servers. Information on how to configure and run a dedicated 
server is included in the game manual.

There are two ways you can host a server: (1) by creating awhile running the 
game normally, or (2) by creating a dedicated console server from the Windows 
Start Menu or the MS-DOS command prompt. Because it doesn't have to draw 
graphics, a dedicated console server requires far less computing resources 
than a non-dedicated server. For this reason, dedicated servers may handle 
many more players than non-dedicated servers. A single fast machine can 
usually host 3-4 dedicated servers simultaneously.

When you are hosting a non-dedicated server (one you create from the user 
interface), players who join your server may experience lag whenever you 
perform operations (e.g. go into the vehicle lab) that cause your computer to 
pause the game in order to load resources. For this reason, it is a good idea 
to set up all the vehicle configurations you will use before creating a non-
dedicated server.

Starsiege multiplayer servers can run a single mission for up to 24 hours 
(1440 minutes). If you set up a server to run a mission for more than 1440 
minutes, it will automatically time out after 1440 minutes.
In a multiplayer game with a mass limit set, the maximum tonnage displayed in 
the vehicle lab will be the lesser of the vehicle's maximum tonnage OR the 
server mass limit.

VI: NETWORK PERFORMANCE TUNING 

If you have a reliable connection to your ISP, but are still experiencing 
packet loss and lag in the game, you can use a Packet Rate option to help 
tune the game. To adjust the packet rate, go to the Options-General screen. 
We recommend that you decrease the packet rate if you are experiencing 
problems. See Section 4: Modem Connection Problems, below, for more 
information.

VII: KNOWN PROBLEMS 

Here is a list of some of the known problems and interactions with other 
programs. 
You cannot run Starsiege with the Windows Display Properties dialog box 
present on your desktop. Please close the Display Properties dialog box 
before running Starsiege.

Problems related to CD-ROM drivers can occur in Starsiege. If you experience 
music not playing, jerky movies, or distorted sound, please make sure you 
have the most up-to-date CD-ROM drivers from your manufacturer.

If Starsiege freezes for no apparent reason, try ejecting and reinserting the 
CD-ROM. If this action succeeds in fixing the problem, you should make sure 
you have the latest CD-ROM drivers.

Some machines experience significant game pauses when the CD-ROM drive 
switches audio tracks. Note that you may turn off CD audio music in the 
Options screen (available from the Main Menu).

Some machines experience crackling sound during gameplay. There may be 
several possible causes. Make sure you have the most recent drivers for your 
sound card AND video card (especially if you have a 3DFX Voodoo1 card). If 
you use A3D, get the latest drivers form www.aureal.com. If you have all the 
latest drivers and still experience problems, try changing your sound system 
type in the Options screen to DirectSound.

During some single player missions, an important event may occur at a great 
distance, and your viewpoint may automatically shift for a few seconds to 
allow you to observe the event. The missions are designed so that these 
events should for the most part not occur while your vehicle is vulnerable to 
attack. If this shift does occur while you are in battle, you may quickly 
return the viewpoint to your vehicle cockpit by pressing Ctrl-C.

Ejecting the CD during the introduction movie causes problems under Windows 
NT. The solution is to press the spacebar to skip the introduction movie, and 
then eject the CD.

Some video cards have problems running in 720x480 resolution. The solution is 
to use a more standard resolution, like 640x480 or 800x600.

When using keyboard controls, some users have been able to reproduce the 
"Circle of Death", where their vehicle uncontrollably turns to the left or to 
the right. This problem usually occurs when you hold down some keys and 
repeatedly task-switch to and from Starsiege. The solution to this problem is 
to press the backspace key to stop turning.

Gamma correction with Voodoo2 and Banshee 3Dfx based cards: Changing the 
gamma correction slider in the game has no immediate effect on these cards. 
You must exit and restart the game for your gamma value to take effect. 

Canopus 3D cards: At the time of this release, a number of people have 
experienced problems running Starsiege on these cards using this 
manufacturer's video drivers. The Glide reference drivers seem to work 
better. More information on 3Dfx/Glide drivers can be found in the 
Troubleshooting section. 

The key combination Ctrl-F9 using the left-hand control key is intercepted by 
a driver on some machines, especially under Windows 98. Starsiege uses this 
key combination for quick chat. The solution to this problem is either to use 
the right-hand control key, or to change the Ctrl-F9 quick chat function to a 
different key combination.

Some 3DFX-based cards have problems taking screenshots when the scoreboard is 
up in multiplayer games. This problem is intermittent, and no solution to 
this problem currently exists.

The "Force Triple Color Buffering" feature of 3DFX Voodoo2 based video cards 
is incompatible with the screen shot mechanism in Starsiege. If you have 
trouble taking screen shots and you have a Voodoo2 based card, make sure 
"Force Triple Color Buffering" is turned off in the Display Properties.

Joystick Hat Problems. "When I move the hat on my joystick, my Herc fires its 
weapons and squats, or carries out some other strange and unexpected 
behavior." Solution: Some joysticks have a CH Flightstick emulation mode in 
which hat movements are translated into combinations of button presses. Most 
joysticks have a switch on the joystick itself that changes the behavior of 
the joystick to that of a normal joystick. For some joysticks, however, this 
change may be a setting in the calibration program found in the Control 
Panel. Change the position of this switch and recalibrate your joystick.

Problem: Game runs very slowly under Windows NT. Solution: If you do not 
physically have a joystick installed, check that your joystick control panel 
shows no joystick installed.

Joystick does not respond properly. Joystick is plugged in and calibrated but 
it doesn't seem to work at all. Possible Solution: If the joystick was 
plugged in after the machine was booted, the operating system may not 
recognize it. This problem may also occur if the joystick is not plugged in 
correctly. To be sure, check your joystick connection and reboot your machine 
by powering off. Go into the Control Panel and recalibrate your joystick.

Problem: Sometimes during the game, Starsiege crashes to the desktop after 
playing a little while. It does not seem to follow a particular pattern. 
Solution: If you have previously installed a service pack for your operating 
system, it is possible that you may need to upgrade the drivers for 
peripheral devices such as the joystick, mouse, sound, or video card. These 
upgrades can usually be found at the peripheral manufacturer's website.

Problem: Sometimes during play, the game locks up and everything appears to 
freeze. Solution: We have received a few reports that the game may be 
crashing as a result of certain virus scanning software. The solution is 
either to turn off the software while the game is running or to upgrade the 
scanning software.

Skin/Face/Logo won't remap. When I run the RemapArt.exe program, my bitmap 
does not get remapped. Solution: It is possible the bitmap is not saved in 
the correct format. Make sure it is saved in 24-bit color depth. PaintShop 
Pro writes out a 24-bit bitmap that is incompatible with our bitmap loading 
routine. If you use PaintShop Pro, you should load the bitmap with another 
program and save it back out before you attempt to remap the art. Skins 
stored in 24-bit color should have a file size of either 196,662, or 98,358, 
depending on the size of the original artwork. If your bitmap is not one of 
these sizes, it probably will not work.

We have had problems with some Microsoft USB joysticks. Please make sure you 
have the latest drivers.

Problems with the Thrustmaster Fusion Gamepad have been reported. These 
problems occur because the gamepad incorrectly reports to Starsiege that it 
has a Z axis, making it difficult to use Starsiege's input configuration 
customization feature to autodetect input. If you experience problems with 
this gamepad, please visit www.starsiegeplayers.com for information on 
manually creating a custom input configuration.

Buttons five through eight on Microsoft Sidewinder 3D joysticks do not work 
on Windows NT. This is because the Winodws NT joystick drivers only support 
buttons 1-4.

When playing multiplayer, it is possible for other players to send you 
"taunts" using the quick chat mechanism. Taunts consist of two parts, a text 
message that appears on your chat window, and a sound that plays. If you wish 
to suppress the quick chat sounds, rename the "taunts.vol" file that exists 
in your Starsiege folder. Rename it to something other than "taunts.vol" 
(e.g. "No Taunts.vol") to turn off the quick chat sounds. Simply rename it 
back to "taunts.vol" to turn the quick chat sounds back on.

If your connection speed is slow or varies a lot during multiplayer play, the 
target lead indicator on your HUD may be slightly inaccurate. You may have to 
compensate for this manually by firing a little ahead of where the lead 
indicator directs. The lead indicator works best on a fast, stable 
connection.

Under no circumstances should you copy existing keymaps, vehicle 
configurations, preferences, or any other files from the Alpha Technology 
Release Demo that was released in the summer of 98. These files are not 
compatible with the final release of Starsiege. 

VII: TROUBLESHOOTING 

We hope you enjoy playing Starsiege. If you experience any difficulties in 
getting the game to operate to your satisfaction, please read further. If the 
symptoms of the problem obviously point to sound or video issues, concentrate 
on those sections. Otherwise, please spend a couple of minutes reading the 
entire section. The time you spend here may well help you get Starsiege 
running more quickly and will provide you with information that will be 
helpful in case you need to contact Technical Support.

Section 1: Notes on Connection 

If you cannot find a game to join, first check that you are connected to your 
ISP. If you are connected but still cannot find a game, try Refresh and/or 
Find New Servers. If you still can't get connected, try re-running Starsiege.

Section 2: Notes on Sound Problems 

Starsiege uses DirectSound, which is a part of Microsoft's DirectX 
programming interface, for sound generation. If you have problems with 
distorted sounds or no sound at all, check to make sure that your sound card 
drivers are DirectSound-compliant. To do so, run DXDIAG; it is located in 
your C:\Program Files\DirectX directory on your hard drive. When running 
DXDIAG, choose the Sound tab. In the upper right corner of the dialog box, 
look for the line that reads "Certified." If this line says "No", then you 
should check with the manufacturer of your system or your sound card to 
determine if DirectX certified drivers are available. If you contact these 
companies via the Internet, you can usually obtain updated drivers free of 
charge. 

Section 3: Notes on Video Problems 

If you experience display problems while in full screen hardware mode (using 
Glide or OpenGL), you'll want to see if they persist when you switch to 
software mode. Simply hold down the ALT key on the keyboard and press ENTER 
to switch to windows (software accelerated) mode. If the problems go away 
when running in a window, the problem is probably related to the device 
drivers you are using with your 3D card. Contact the manufacturer of the card 
to verify that you have the most recent driver with the most current version 
of Glide or OpenGL. The latest Glide reference drivers can be obtained from 
3Dfx at www.3dfx.com/download/download.html. See the OpenGL section (above) 
for more information on OpenGL issues.

The latest information on 3Dfx and Glide can be found at www.3dfx.com. 
Copyright © 1997 3Dfx Interactive, Inc. The 3Dfx Interactive logo, Voodoo 
Graphics and Voodoo Rush are trademarks of 3Dfx Interactive. 

If problems occur while running in windowed mode, changing the color depth 
may help. To change to 16-bit color, right-click on your Windows Desktop and 
choose Properties from the pop-up menu that appears. Choose the Settings tab 
in the dialog box; it should be the one furthest to the right. Select the 
Color pull-down menu and choose 16-bit color; you may have to reduce your 
screen resolution if you are raising the color setting. 

Section 4: Modem connection problems

"I've noticed that my Herc seems to be sliding around on the screen a lot 
when I play a multiplayer game." What you are seeing is a result of your 
machine not getting enough information from the game server on the other side 
of the internet connection. Possible Explanations:

Too much traffic on the internet. Periodically, the internet experiences 
severe slowdowns when a lot of people are transmitting data. When this 
"traffic jam" occurs, the internet may lack the bandwidth to transmit all 
the data in a timely manner. Try logging on at a different time of day.

Busy ISP. Some very large ISP's such as AOL and Compuserve can have 
problems with performance-related applications such as Starsiege.

Your ISP does not have enough bandwidth. In order to handle the ever-
growing demand for internet connections, some Internet Service Providers 
simply increase the number of connections to the internet without spending 
money to increase their ability to transmit more data. This practice is 
called "Overselling Bandwidth", and is quite common. Shop around for an 
ISP that guarantees good data throughput and low ping times. It may cost a 
bit more, but it may be worth it.

Your line may be noisy. Your phone line may have too much static to 
provide a clear connection. An indicator might be a hissing sound or 
crackling when you talk on the phone. High-speed modems such as the 56k 
modems require an exceptionally clear line in order to operate at a high 
transmission rate or they will lose data during transmission. Try using a 
single new cable between the wall and your computer’s modem. Remove 
sources of possible interference such as stereo speakers, TV's, radios, 
etc.

The game server is too slow or on a poor connection. The game you connect 
to in the Join Game screen may be running on a machine that cannot handle 
the job of being a host computer. There may be too many players for the 
machine’s communications capability. It is also possible that the host is 
playing on that computer at the same time he or she is hosting the game. 
To see if this situation is the problem, look for games run on dedicated 
servers. A symbol on the Join Game screen looks something like this: 
((T)). It indicates that the host computer is dedicated to the purpose of 
hosting a game and that nobody else is playing on this machine. Also note 
the machine’s CPU speed and Ping times. Usually a high CPU speed and a low 
Ping time on a dedicated server can handle the most players.

Section 5: Other Troubleshooting 

The following points are steps that can be taken to help correct non-game 
specific issues, such as random game crashes or performance problems. 

1. Verify you have sufficient hard drive space to install the program. Go to 
My Computer and right-click on the drive where you plan to install the game. 
Select Properties from the pop-up menu that appears. You should see a Free 
Space listing; make sure it shows that you have enough free space to install 
the game properly. The System Requirements for Starsiege are listed at the 
top of this document.

2. Make sure all non-vital programs are closed when you run Starsiege. To 
check which programs are active, hold down the CTRL and ALT keys on your 
keyboard and press the DEL key. This will bring up a dialog box called Close 
Programs. Generally, any program listed here besides Explorer and Systray is 
non-vital and should be closed before running Starsiege. To close a program, 
highlight it and click on the End Task button. You will need to repeat this 
process for each listed program. If a program will not shut down via this 
method, you may have to consult that program’s documentation to find 
instructions for shutting it down. (Note: This is not a permanent change to 
your computer. Simply rebooting will re-activate all of the programs you shut 
down.)

3. Run a thorough ScanDisk on your hard drive. You can run ScanDisk by 
clicking on the Start button and selecting Programs. Inside the Accessories 
there will be a System Tools group containing ScanDisk. Once you have clicked 
on ScanDisk, select the drive to scan and put the dot in the Thorough option. 
Then click on the Start button. This will probably take at least half an hour 
and as long as several hours. ScanDisk will locate errors on the hard drive 
and attempt to fix these errors. (Note: Always back up any critical 
information on your system before running Scandisk. If you have errors in the 
data on your hard drive, Scandisk will fix them by deleting the corrupted 
data. After this deletion occurs, some programs on your computer may quit 
functioning. In this event, you will want to remove and reinstall those 
affected programs. If you need assistance with that process, you should 
contact the manufacturer of the particular program.)

4. Try using a boot disk to prevent real mode device drivers from loading. 
Put a blank, high-density diskette in your A: drive. Then, open the My 
Computer icon from the desktop and highlight the icon for Drive A:. Right-
click on the icon and choose Format. In the resulting dialog box, make sure 
there are checks in the boxes for "Full" and "Copy System Files". Click on OK 
to start the process. Once the disk is formatted, double-click on the icon 
for the C: drive in My Computer. Look for the file called MSDOS.SYS in the 
list of files. If you cannot find it, click on the View menu, choose Options 
and then the View tab. Make sure "Show all files" is checked and "Hide MS DOS 
file extensions" is not checked. Once you've found the MSDOS.SYS file, right-
click on it and choose Send To 3½" Floppy (A). You will be prompted to 
replace an existing file - click on OK. Once you’ve done this, reboot your 
system with the disk in the A: drive. 

5. If you are still having problems at this point, try doing a clean 
installation of the game. Run SETUP from the root directory of your Starsiege 
CD and choose to uninstall the game. Reboot your computer with the boot disk 
that you created in step 4. Close all programs as listed in step 2. Then run 
SETUP from your Starsiege CD again and reinstall the game. For further 
information, see the Starsiege web page at www.starsiegeplayers.com.

IX: CONTACTING SIERRA 

A) Customer Service, Support, and Sales
B) Technical Support
C) Legal Information


A) Customer Service, Support, and Sales
----------------------------------
United States 

U.S.A. Sales Phone: (800) 757-7707
International Sales: (425) 746-5771 
Hours: Monday-Saturday 7AM to 11 PM CST,
Sundays 8 AM to 9PM CST 
FAX: (402) 393-3224 

Sierra Direct 
7100 W. Center Rd 
STE 301 
Omaha, NE 68106 

United Kingdom

Havas Interactive 
Main: (0118) 920-9111
Monday-Friday, 9:00 a.m. - 5:00 p.m.
Fax: (0118) 987-5603
Disk/CD replacements in the U.K. are £6.00,
or £7.00 outside the UK. Add "ATTN.: Returns."

2 Beacontree Plaza, 
Gillette Way, 
Reading, Berkshire 
RG2 0BS United Kingdom 


France
Havas Interactive 
Téléphone: 01-46-01-46-50
Lundi au Jeudi de 10h à 19h
Vendredi de 10h à 18h
Fax: 01-46-30-00-65

Parc Tertiaire de Meudon 
Immeuble "Le Newton" 
25 rue Jeanne Braconnier 
92366 Meudon La Forêt Cedex
France


Germany

Havas Interactive 
Tel: (0) 6103-99-40-40
Montag bis Freitag von 9h - 19Uhr
Fax: (0) 6103-99-40-35

Robert-Bosh-Str. 32 
D-63303 Dreieich 
Germany


On-Line Sales
CompuServe United Kingdom:GO UKSIERRA
CompuServe France: GO FRSIERRA
CompuServe Germany: GO DESIERRA
Internet USA: http://www.sierra.com
Internet United Kingdom: http://www.sierra-online.co.uk
Internet France: http://www.sierra.fr
Internet Germany: http://www.sierra.de


B) TECHNICAL SUPPORT
-------------------------
North America

Sierra On-Line offers a 24-hour automated technical support line with 
recorded answers to the most frequently asked technical questions. To access
this service, call (425) 644-4343, and follow the recorded instructions to 
find your specific topic and resolve the issue. If this fails to solve your
problem, you may still write, or fax us with your questions, or contact us 
via our Web site.

Sierra On-Line 
Technical Support
P.O. Box 85006 
Bellevue, WA 98015-8506 

Main: (425) 644-4343
Monday-Friday, 8:00 a.m.- 4:45 p.m. PST 
Fax: (425) 644-7697

http://www.sierra.com
support@sierra.com


United Kingdom

Havas Interactive offers a 24-hour Automated Technical Support line with 
recorded answers to the most frequently asked technical questions. To
access this service, call (0118) 920-9111, and follow the recorded 
instructions to find your specific topic and resolve the issue. If this fails 
to solve your problem, you may still write, or fax us with your questions, or 
contact us via our Internet or CompuServe sites.

Havas Interactive 
2 Beacontree Plaza, 
Gillette Way, 
Reading, Berkshire 
RG2 0BS United Kingdom 

Main: (0118) 920-9111
Monday-Friday, 9:00 a.m. - 5:00 p.m.
Fax: (0118) 987-5603

http://www.sierra-online.co.uk


France

Havas Interactive 
Parc Tertiaire de Meudon 
Immeuble "Le Newton" 
25 rue Jeanne Braconnier 
92366 Meudon La Forêt Cedex 
France

Téléphone: 01-46-01-46-50
Lundi au Jeudi de 10h à 19h
Vendredi de 10h à 18h
Fax: 01-46-30-00-65

http://www.sierra.fr


Germany

Havas Interactive 
Robert-Bosh-Str. 32 
D-63303 Dreieich 
Deutschland 

Tel: (0) 6103-99-40-40
Montag bis Freitag von 9 - 19Uhr
Fax: (0) 6103-99-40-35
Mailbox: (0) 6103-99-40-35

http://www.sierra.de


Spain

Havas Interactive 
Avenida de Burgos 9 
1º-OF2 
28036 Madrid
Spain

Teléfono: (01) 383-2623
Lunes a Viernes de 9h30 a 14h y de 15h a 18h30
Fax: (01) 381-2437


Italy

Contattare il vostro distribotore. 

-----------------------------------
Sierra's end user license agreement, limited warranty and return policy is 
set forth in the EULA.txt, found on the CD, and is also available during the 
install of the product.

All trademarks are properties of their respective owners.

