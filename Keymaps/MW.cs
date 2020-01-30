#------------------------------------------------------------------------------
#	Mech Warrior Controls (aprox)
#------------------------------------------------------------------------------

# include generic game actions
exec( "_gameActions.cs" );
#------------------------------------------------------------------------------
#	include generic keyboard controls
#------------------------------------------------------------------------------
exec( "_defKeyboard.cs" );

#------------------------------------------------------------------------------
# include generic camera controls
#------------------------------------------------------------------------------
exec( "_defCamera.cs" );

#==============================================================================
#	OrbitCam controls
#==============================================================================
editActionMap( CameraOrbit );
#------- Joystick Controls
bindAction( joystick0, zaxis, TO, IDACTION_SPEED, deadzone, 0.1, center, square, flip );
bindAction( joystick0, ypov, TO, IDACTION_PITCH, center );
bindAction( joystick0, xpov, TO, IDACTION_YAW, center );

bindAction( keyboard, make, z, TO, IDACTION_SPEED, -1.0 );
bindAction( keyboard, break, z, TO, IDACTION_SPEED, 0.0 );
bindAction( keyboard, make, shift, z, TO, IDACTION_SPEED, "+1.0" );
bindAction( keyboard, break, shift, z, TO, IDACTION_SPEED, 0.0 );
bindAction( keyboard, make, control, left, TO, IDACTION_YAW, "+1.0" );
bindAction( keyboard, make, control, right, TO, IDACTION_YAW, -1.0 );
bindAction( keyboard, break, control, left, TO, IDACTION_YAW, 0.0 );
bindAction( keyboard, break, control, right, TO, IDACTION_YAW, 0.0 );
bindAction( joystick0, make,  button0, TO, IDACTION_ZOOM_MODE, 1.0 );
bindAction( joystick0, break, button0, TO, IDACTION_ZOOM_MODE, 0.0 );

#==============================================================================
#	Herc controls
#==============================================================================

editActionMap( Herc );

#------------------------------------------------------------------------------
#	Joystick controls
#------------------------------------------------------------------------------
bindAction( joystick, xaxis, TO, IDACTION_YAW, deadzone, 0.1, center, square, flip );
bindAction( joystick, xpov, TO, IDACTION_LOOK_X, flip, center );
bindAction( joystick, ypov, TO, IDACTION_LOOK_Y, center );
bindAction( joystick, yaxis, TO, IDACTION_SPEED, deadzone, 0.1, center, square, flip );
bindAction( joystick, make, button0, TO, IDACTION_FIRE, 1.0 );
bindAction( joystick, break, button0, TO, IDACTION_FIRE, 0.0 );
bindAction( joystick, make, button1, TO, IDACTION_TARGET_SELECTED );
bindAction( joystick, make, button2, TO, IDACTION_CAMOUFLAGE );
bindAction( joystick, make, button3, TO, IDACTION_WEAPON_MODE_SELECT );
bindAction( joystick, make, button4, TO, IDACTION_WEAPON_ADJ, -1.0 );
bindAction( joystick, make, button5, TO, IDACTION_WEAPON_ADJ, "+1.0" );
bindAction( joystick, make, button6, TO, IDACTION_SHIELD_TRACK );
bindAction( joystick, make, button7, TO, IDACTION_CROUCH, 1.0 );
bindAction( joystick, break, button7, TO, IDACTION_CROUCH, 0.0 );

#------------------------------------------------------------------------------
#	Mouse controls
#------------------------------------------------------------------------------
bindAction( mouse0, xaxis, TO, IDACTION_LOOK_X, scale, 0.5 );
bindAction( mouse0, yaxis, TO, IDACTION_LOOK_Y, scale, 0.5, flip );
bindAction( mouse0, make, button0, TO, IDACTION_FIRE, 1.0 );
bindAction( mouse0, break, button0, TO, IDACTION_FIRE, 0.0 );
bindAction( mouse0, make, button1, TO, IDACTION_TARGET_SELECTED );

#--------------------------------------------------------------------
#	Keyboard movement controls
#--------------------------------------------------------------------  
bindAction( keyboard, make, up, TO, IDACTION_LOOK_Y, -1.0 );
bindAction( keyboard, break, up, TO, IDACTION_LOOK_Y, 0.0 );
bindAction( keyboard, make, down, TO, IDACTION_LOOK_Y, "+1.0" );
bindAction( keyboard, break, down, TO, IDACTION_LOOK_Y, 0.0 );
bindAction( keyboard, make, left, TO, IDACTION_YAW, "+1.0" );
bindAction( keyboard, break, left, TO, IDACTION_YAW, 0.0 );
bindAction( keyboard, make, right, TO, IDACTION_YAW, -1.0 );
bindAction( keyboard, break, right, TO, IDACTION_YAW, 0.0 );

bindAction( keyboard, make, tab, TO, IDACTION_CROUCH, 1.0 );
bindAction( keyboard, break, tab, TO, IDACTION_CROUCH, 0.0 );

bindAction( keyboard, make, enter, TO, IDACTION_WEAPON_ADJ, 1.0 );
bindAction( keyboard, make, space, TO, IDACTION_FIRE, 1.0 );
bindAction( keyboard, break, space, TO, IDACTION_FIRE, 0.0 );
bindAction( keyboard, make, "\\", TO, IDACTION_WEAPON_MODE_SELECT);

bindAction( keyboard, make, 1, TO, IDACTION_STOP );
bindAction( keyboard, make, 2, TO, IDACTION_SPEED, 0.2 );
bindAction( keyboard, make, 3, TO, IDACTION_SPEED, 0.3 );
bindAction( keyboard, make, 4, TO, IDACTION_SPEED, 0.4 );
bindAction( keyboard, make, 5, TO, IDACTION_SPEED, 0.5 );
bindAction( keyboard, make, 6, TO, IDACTION_SPEED, 0.6 );
bindAction( keyboard, make, 7, TO, IDACTION_SPEED, 0.7 );
bindAction( keyboard, make, 8, TO, IDACTION_SPEED, 0.8 );
bindAction( keyboard, make, 9, TO, IDACTION_SPEED, 0.9 );
bindAction( keyboard, make, 0, TO, IDACTION_SPEED, 1.0 );
bindAction( keyboard, make, "=", TO, IDACTION_SPEED, "+1.0" );
bindAction( keyboard, break, "=", TO, IDACTION_SPEED, 0.0 );
bindAction( keyboard, make, "-", TO, IDACTION_SPEED, -1.0 );
bindAction( keyboard, break, "-", TO, IDACTION_SPEED, 0.0 );
bindAction( keyboard, make, s, TO, IDACTION_REACTOR );



#--------------------------------------------------------------------
#	Shield controls
#--------------------------------------------------------------------          
bindAction( keyboard, make, prior, TO, IDACTION_SHIELD_TRACK, 0.0 );
bindAction( keyboard, make, numpad5, TO, IDACTION_SHIELD_FOCUS_SET );
bindAction( keyboard, make, home, TO, IDACTION_SHIELD_FOCUS_ADJ, 0.125 );
bindAction( keyboard, make, insert, TO, IDACTION_SHIELD_FOCUS_ADJ, -0.125 );
bindAction( keyboard, make, end, TO, IDACTION_SHIELD_ROTATION_ADJ, 0.125 );
bindAction( keyboard, make, delete, TO, IDACTION_SHIELD_ROTATION_ADJ, -0.125 );
bindAction( keyboard, make, numpad0, TO, IDACTION_SHIELD );

#--------------------------------------------------------------------
#	Weapon controls
#--------------------------------------------------------------------
bindaction( keyboard, make,  alt, 1, TO, IDACTION_WEAPON_GROUP_SELECT, 0 );   
bindaction( keyboard, make,  alt, 2, TO, IDACTION_WEAPON_GROUP_SELECT, 1 );   
bindaction( keyboard, make,  alt, 3, TO, IDACTION_WEAPON_GROUP_SELECT, 2 );   

bindAction( keyboard, make, t, TO, IDACTION_TARGET_ADJ_ENEMY, 1.0 );
bindAction( keyboard, make, r, TO, IDACTION_TARGET_ADJ_ENEMY, -1.0 );
bindAction( keyboard, make, e, TO, IDACTION_TARGET_CLOSEST_ENEMY );
bindAction( keyboard, make, q, TO, IDACTION_TARGET_SELECTED );
bindAction( keyboard, make, shift, r, TO, IDACTION_SENSOR_RANGE_TOGGLE );

#--------------------------------------------------------------------
#	Misc.
#--------------------------------------------------------------------
bindAction( keyboard, make, n, TO, IDACTION_NAVPOINT_NEXT );
bindAction( keyboard, make, v, TO, IDACTION_NAVPOINT_PREV );
bindAction( keyboard, make, b, TO, IDACTION_NAVPOINT_SET, -1 );
bindaction( keyboard, make, alt, 7, TO, IDACTION_USE_SPECIAL , 0 );
bindaction( keyboard, make, alt, 8, TO, IDACTION_USE_SPECIAL , 1 );
bindaction( keyboard, make, alt, 9, TO, IDACTION_USE_SPECIAL , 2 );
bindaction( keyboard, make, alt, 0, TO, IDACTION_USE_SPECIAL , 3 );


