# modified 11/14/98

###############################################################################
#	EarthSiege 2 Controls 
#	>note: we don't have all the same commands as EarthSiege 2
#	       so there are some substitutes here.
###############################################################################

#------------------------------------------------------------------------------
#	include generic camera controls
#------------------------------------------------------------------------------
exec( "_defCamera.cs" );

#------------------------------------------------------------------------------
# include generic keyboard controls
#------------------------------------------------------------------------------
exec( "_defKeyboard.cs" );

#------------------------------------------------------------------------------
#	Herc Controls
#------------------------------------------------------------------------------
editActionMap( Herc );

#--------------------------------------------------------------------
#	Joystick controls
#--------------------------------------------------------------------
bindAction( joystick, xaxis, TO, IDACTION_YAW, deadzone, 0.1, center, square );
bindAction( joystick, yaxis, TO, IDACTION_SPEED, deadzone, 0.1, center, square );
bindAction( joystick, xpov, TO, IDACTION_LOOK_X, center );
bindAction( joystick, ypov, TO, IDACTION_LOOK_Y, center );

bindAction( joystick, make, button0, TO, IDACTION_FIRE, 1.0 );
bindAction( joystick, break,button0, TO, IDACTION_FIRE, 0.0 );
bindAction( joystick, make, button1, TO, IDACTION_TARGET_SELECTED );
bindAction( joystick, make, button2, TO, IDACTION_WEAPON_MODE_SELECT );
bindAction( joystick, make, button3, TO, IDACTION_SHIELD_TRACK );
bindAction( joystick, make, button4, TO, IDACTION_WEAPON_GROUP_ADJ, 1.0 );
bindAction( joystick, make, button5, TO, IDACTION_WEAPON_ADJ, "+1.0" );
bindAction( joystick, make, button6, TO, IDACTION_SHIELD_FOCUS_ADJ, "+1.0" );
bindAction( joystick, break, button6, TO, IDACTION_SHIELD_FOCUS_ADJ, 0.0 );
bindAction( joystick, make, button7, TO, IDACTION_SHIELD_FOCUS_ADJ, -1.0 );
bindAction( joystick, break, button7, TO, IDACTION_SHIELD_FOCUS_ADJ, 0.0 );

#--------------------------------------------------------------------
#	Mouse controls
#--------------------------------------------------------------------
bindAction( mouse0, xaxis, TO, IDACTION_LOOK_X, scale, 0.6 );
bindAction( mouse0, yaxis, TO, IDACTION_LOOK_Y, scale, 0.6 );
bindAction( mouse0, make, button0, TO, IDACTION_FIRE, 1.0 );
bindAction( mouse0, break, button0, TO, IDACTION_FIRE, 0.0 );
bindAction( mouse0, make, button1, TO, IDACTION_TARGET_SELECTED );

#--------------------------------------------------------------------
#	Function Keys
#--------------------------------------------------------------------
##### F1 : the status is already given in your HUD
bindaction( keyboard, make,  F2, TO, IDACTION_ORDER_ALL_SQUADMATES, 0 );
bindaction( keyboard, make,  F3, TO, IDACTION_MAP_VIEW, 0 ); 
##### You cannot turn off your radar in Starsiege so F4 controls the range...
bindaction( keyboard, make, F4, TO, IDACTION_SENSOR_RANGE_TOGGLE );
bindaction( keyboard, make, F5, TO, IDACTION_SCAN_TARGET );
bindaction( keyboard, make,  F7, TO, IDACTION_GENERAL_COMMAND, 0 );
#bind( keyboard, make, F9, to, "orbitPlayer(20,  90, 20);");
#bind( keyboard, make, F10, to, "orbitPlayer(20, 270, 20);");

#--------------------------------------------------------------------
#	Original Starsiege Function keys  alt 1-4
#--------------------------------------------------------------------
##### all of the original function keys for Starsiege are now under control-f 
##### the only exception is general command which is alt-f1
bindaction( keyboard, make,  control, F1, TO, IDACTION_ORDER_SQUADMATE_1, 0 );   
bindaction( keyboard, make,  control, F2, TO, IDACTION_ORDER_SQUADMATE_2, 0 );   
bindaction( keyboard, make,  control, F3, TO, IDACTION_ORDER_SQUADMATE_3, 0 );   
bindaction( keyboard, make,  control, F4, TO, IDACTION_ORDER_ALL_SQUADMATES, 0 );   
bindaction( keyboard, make,  alt, F1, TO, IDACTION_GENERAL_COMMAND, 0 );  

#--------------------------------------------------------------------
#	use i,j,k,m for targeting cursor
#--------------------------------------------------------------------
bindAction( keyboard, make, i, TO, IDACTION_LOOK_Y, "+1.0" );
bindAction( keyboard, make, m, TO, IDACTION_LOOK_Y, -1.0 );
bindAction( keyboard, break, i, TO, IDACTION_LOOK_Y, 0.0 );
bindAction( keyboard, break, m, TO, IDACTION_LOOK_Y, 0.0 );
bindAction( keyboard, make, j, TO, IDACTION_LOOK_X, "+1.0" );
bindAction( keyboard, make, k, TO, IDACTION_LOOK_X, -1.0 );
bindAction( keyboard, break, j, TO, IDACTION_LOOK_X, 0.0 );
bindAction( keyboard, break, k, TO, IDACTION_LOOK_X, 0.0 );


#--------------------------------------------------------------------
#	Numpad controls and other movement controls
#--------------------------------------------------------------------
bindAction( keyboard, make, numpad8, TO, IDACTION_SPEED, "+1.0" );
bindAction( keyboard, break, numpad8, TO, IDACTION_SPEED, 0.0 );
bindAction( keyboard, make, numpad7, TO, IDACTION_SHIELD_ROTATION_ADJ, "+0.125" );
bindAction( keyboard, make, numpad9, TO, IDACTION_SHIELD_ROTATION_ADJ, -0.125 );
bindAction( keyboard, make, numpad4, TO, IDACTION_YAW, "+1.0" );
bindAction( keyboard, break, numpad4, TO, IDACTION_YAW, 0.0 );
bindAction( keyboard, make, numpad2, TO, IDACTION_SPEED, -1.0 );
bindAction( keyboard, break, numpad2, TO, IDACTION_SPEED, 0.0 );
bindAction( keyboard, make, numpad6, TO, IDACTION_YAW, -1.0 );
bindAction( keyboard, break, numpad6, TO, IDACTION_YAW, 0.0 );
bindAction( keyboard, make, numpad0, TO, IDACTION_SHIELD );
bindAction( keyboard, make, numpad1, TO, IDACTION_SHIELD_FOCUS_ADJ, -0.125 );
bindAction( keyboard, make, numpad3, TO, IDACTION_SHIELD_FOCUS_ADJ, 0.125 );
bindAction( keyboard, make, numpad5, TO, IDACTION_STOP );
bindAction( keyboard, make, "numpad-", TO, IDACTION_SPEED, -1.0 );
bindAction( keyboard, break,"numpad-", TO, IDACTION_SPEED, 0.0 );
bindAction( keyboard, make, "numpad+", TO, IDACTION_SPEED, 1.0 );

bindAction( keyboard, make, up, TO, IDACTION_SPEED, "+1.0" );
bindAction( keyboard, break, up, TO, IDACTION_SPEED, 0.0 );
bindAction( keyboard, make, down, TO, IDACTION_SPEED, -1.0 );
bindAction( keyboard, break, down, TO, IDACTION_SPEED, 0.0 );
bindAction( keyboard, make, left, TO, IDACTION_YAW, "+1.0" );
bindAction( keyboard, break, left, TO, IDACTION_YAW, 0.0 );
bindAction( keyboard, make, right, TO, IDACTION_YAW, -1.0 );
bindAction( keyboard, break, right, TO, IDACTION_YAW, 0.0 );

#--------------------------------------------------------------------
#	Weapon Controls
#--------------------------------------------------------------------
bindAction( keyboard, make, n, TO, IDACTION_TARGET_ADJ_ENEMY, "+1.0" );
bindAction( keyboard, make, y, TO, IDACTION_TARGET_ADJ_ENEMY, -1.0 );
bindAction( keyboard, make, quote, TO, IDACTION_TARGET_CLOSEST_ENEMY );
bindAction( keyboard, make, enter, TO, IDACTION_TARGET_SELECTED );

bindAction( keyboard, make, 1, TO, IDACTION_WEAPON_SELECT, 0.0 );
bindAction( keyboard, make, 2, TO, IDACTION_WEAPON_SELECT, 1.0 );
bindAction( keyboard, make, 3, TO, IDACTION_WEAPON_SELECT, 2.0 );
bindAction( keyboard, make, 4, TO, IDACTION_WEAPON_SELECT, 3.0 );
bindAction( keyboard, make, 5, TO, IDACTION_WEAPON_SELECT, 4.0 );
bindAction( keyboard, make, 6, TO, IDACTION_WEAPON_SELECT, 5.0 );

bindAction( keyboard, make, w, TO, IDACTION_WEAPON_ADJ, "+1.0" );
bindAction( keyboard, make, alt, w, TO, IDACTION_WEAPON_ADJ, -1.0 );
bindAction( keyboard, make, l, TO, IDACTION_WEAPON_MODE_SELECT );

bindAction( keyboard, make, space, TO, IDACTION_FIRE, 1.0 );
bindAction( keyboard, break, space, TO, IDACTION_FIRE, 0.0 );

bindaction( keyboard, make,  alt, 1, TO, IDACTION_WEAPON_GROUP_SELECT, 0 );   
bindaction( keyboard, make,  alt, 2, TO, IDACTION_WEAPON_GROUP_SELECT, 1 );   
bindaction( keyboard, make,  alt, 3, TO, IDACTION_WEAPON_GROUP_SELECT, 2 );   

#--------------------------------------------------------------------
#	Misc. Controls
#--------------------------------------------------------------------

bindAction( keyboard, make, c, TO, IDACTION_CAMOUFLAGE );
bindAction( keyboard, make, p, TO, IDACTION_REACTOR );
bindAction( keyboard, make, s, TO, IDACTION_CROUCH, 1.0 );
bindAction( keyboard, break, s, TO, IDACTION_CROUCH, 0.0 );

bindAction( keyboard, make, numpad0, TO, IDACTION_SHIELD, 0.0 );
bindAction( keyboard, make, t, TO, IDACTION_SHIELD_TRACK );

bindaction( keyboard, make,  backspace, TO, IDACTION_CENTER_TURRET );    
bindaction( keyboard, make,  "\\", TO, IDACTION_CENTER_BODY ); 

bindaction( keyboard, make,  "[", TO, IDACTION_SHIELD_FOCUS_ADJ, "0.2" );   
bindaction( keyboard, make,  "]", TO, IDACTION_SHIELD_FOCUS_ADJ, "-0.2" ); 

bindAction( keyboard, make, shift, r, TO, IDACTION_SENSOR_RANGE_TOGGLE );
bindAction( keyboard, make, r, TO, IDACTION_SENSOR_MODE_TOGGLE );
bindAction( keyboard, make, alt, r, TO, IDACTION_SENSOR_RANGE_SET, 1.0 );
bindAction( keyboard, break, alt, r, TO, IDACTION_SENSOR_RANGE_SET, 0.0 );


bind( keyboard, make, v, to, "orbitPlayer(20,   0, 10);");


#------------------------------------------------------------------------------
#	game actions
#	Instead of using the generic game actions I used this to
#	avoid having multiply keys tied to the same function.
#------------------------------------------------------------------------------
newActionMap( gameActions );

#--------------------------------------------------------------------
#	Chats for keymaps that use the non default function keys
#--------------------------------------------------------------------
bindAction(keyboard0, make, control, numpad1, TO, IDACTION_QUICKCHAT, 1.000000);
bindAction(keyboard0, make, control, numpad2, TO, IDACTION_QUICKCHAT, 2.000000);
bindAction(keyboard0, make, control, numpad3, TO, IDACTION_QUICKCHAT, 3.000000);
bindAction(keyboard0, make, control, numpad4, TO, IDACTION_QUICKCHAT, 4.000000);
bindAction(keyboard0, make, control, numpad5, TO, IDACTION_QUICKCHAT, 5.000000);
bindAction(keyboard0, make, control, numpad6, TO, IDACTION_QUICKCHAT, 6.000000);
bindAction(keyboard0, make, control, numpad7, TO, IDACTION_QUICKCHAT, 7.000000);
bindAction(keyboard0, make, control, numpad8, TO, IDACTION_QUICKCHAT, 8.000000);
bindAction(keyboard0, make, control, numpad9, TO, IDACTION_QUICKCHAT, 9.000000);
bindAction(keyboard0, make, control, numpad0, TO, IDACTION_QUICKCHAT, 10.000000);
bindAction(keyboard0, make, control, "numpad-", TO, IDACTION_QUICKCHAT, 11.000000);
bindAction(keyboard0, make, control, "numpad+", TO, IDACTION_QUICKCHAT, 12.000000);
										   

#--------------------------------------------------------------------
#	Original Starsiege Function keys  alt 5-8
#--------------------------------------------------------------------
##### all of the original function keys(from 1 to 8) for Starsiege are now under control-f 
##### the only exception is general command which is alt-f1  
bindaction( keyboard, make,  control, F5, TO, IDACTION_TOGGLE_CHAT_DISPLAY  );    
bindaction( keyboard, make,  control, F6, TO, IDACTION_TELL_ALL  );    
bindaction( keyboard, make,  control, F7, TO, IDACTION_TELL_TEAM  );    
bindaction( keyboard, make,  control, F8, TO, IDACTION_TELL_TARGET  );


bindaction( keyboard, make,  F1, TO, IDACTION_TOGGLE_HUD_CONFIG  );

#------------------------------------------------------------------------------
#	Respawn key valid after player's vehicle is destroyed
#------------------------------------------------------------------------------
bindAction(keyboard0, make, "space", TO, IDACTION_RESPAWN);
bindAction(keyboard0, make, alt, r, TO, IDACTION_RESPAWN);

#------------------------------------------------------------------------------
#	Camera controls
#------------------------------------------------------------------------------
bindAction(keyboard0, make, control, "c", TO, IDACTION_PILOT_CAMERA);
bindAction(keyboard0, make, control, "o", TO, IDACTION_ORBIT_CAMERA);
bindAction(keyboard0, make, control, "v", TO, IDACTION_ORBIT_CAMERA);
bindAction( keyboard, make, "b", TO, IDACTION_PILOT_CAMERA ); 


#------------------------------------------------------------------------------
#	Single Player only Controls
#------------------------------------------------------------------------------
bindAction(keyboard0, make, "numlock", TO, IDACTION_PAUSE);

#------------------------------------------------------------------------------
#	Player prefs and scoreboard
#------------------------------------------------------------------------------
bindAction(keyboard0, make, "f9", TO, IDACTION_TOGGLE_HUD_CONFIG);
bindAction(keyboard0, make, "f11", TO, IDACTION_TOGGLE_PREF_CONFIG);
bindAction(keyboard0, make, "f12", TO, IDACTION_TOGGLE_SCOREBOARD);
