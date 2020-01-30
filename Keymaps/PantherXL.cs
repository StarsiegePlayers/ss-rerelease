#------------------------------------------------------------------------------
# include generic game actions
exec( "_gameActions.cs" );

#------------------------------------------------------------------------------
# include generic camera controls
exec( "_defCamera.cs" );


#------------------------------------------------------------------------------
# include generic keyboard controls
exec( "_defKeyboard.cs" );

#------------------------------------------------------------------------------
#
# Add support for MadCatz PantherXL joystick
#
editActionMap( Herc );

#--Weapon Controls
bindAction( keyboard, make, 1, TO, IDACTION_WEAPON_SELECT, 0.0 );
bindAction( keyboard, make, 2, TO, IDACTION_WEAPON_SELECT, 1.0 );
bindAction( keyboard, make, 3, TO, IDACTION_WEAPON_SELECT, 2.0 );
bindAction( keyboard, make, 4, TO, IDACTION_WEAPON_SELECT, 3.0 );
bindAction( keyboard, make, 5, TO, IDACTION_WEAPON_SELECT, 4.0 );
bindAction( keyboard, make, 6, TO, IDACTION_WEAPON_SELECT, 5.0 );
bindAction( keyboard, make, shift, 1, TO, IDACTION_WEAPON_GROUP_TOGGLE, 0.0 );
bindAction( keyboard, make, shift, 2, TO, IDACTION_WEAPON_GROUP_TOGGLE, 1.0 );
bindAction( keyboard, make, shift, 3, TO, IDACTION_WEAPON_GROUP_TOGGLE, 2.0 );
bindAction( keyboard, make, shift, 4, TO, IDACTION_WEAPON_GROUP_TOGGLE, 3.0 );
bindAction( keyboard, make, shift, 5, TO, IDACTION_WEAPON_GROUP_TOGGLE, 4.0 );
bindAction( keyboard, make, shift, 6, TO, IDACTION_WEAPON_GROUP_TOGGLE, 5.0 );

bindAction( keyboard, make, w, TO, IDACTION_WEAPON_ADJ, "+1.0" );
bindAction( keyboard, make, alt, w, TO, IDACTION_WEAPON_ADJ, -1.0 );
bindAction( keyboard, make, l, TO, IDACTION_WEAPON_MODE_SELECT, 0 );
bindAction( keyboard, make, z, TO, IDACTION_WEAPON_GROUP_SELECT, 0 );
bindAction( keyboard, make, x, TO, IDACTION_WEAPON_GROUP_SELECT, 1 );
bindAction( keyboard, make, c, TO, IDACTION_WEAPON_GROUP_SELECT, 2 );

bindAction( joystick, make, button10, TO, IDACTION_WEAPON_GROUP_SELECT, 0);
bindAction( joystick, make, button11, TO, IDACTION_WEAPON_GROUP_SELECT, 1);
bindAction( joystick, make, button12, TO, IDACTION_WEAPON_GROUP_SELECT, 2);

bindAction( joystick, make,  button0, TO, IDACTION_FIRE, 1.0);
bindAction( joystick, break, button0, TO, IDACTION_FIRE, 0.0);
bindAction( joystick, make,  button8, TO, IDACTION_WEAPON_MODE_SELECT, 0.0);
								  
# --Movement controls
bindAction( joystick, xaxis, TO, IDACTION_YAW, deadzone, 0.1, center, square );
bindAction( joystick, yaxis, TO, IDACTION_SPEED, deadzone, 0.1, center, square );

# --View and Sensor controls
bindaction( joystick, make,  rpov, TO, IDACTION_ZOOM_ADJ, 1 );   
bindaction( joystick, make,  lpov, TO, IDACTION_ZOOM_ADJ, "-1.0" );  
bindAction( joystick, make, button5, TO, IDACTION_SENSOR_RANGE_TOGGLE);
bindAction( joystick, make, button7, TO, IDACTION_SENSOR_MODE_TOGGLE);
bindAction( joystick, make, button4, TO, IDACTION_TARGET_ADJ_ENEMY, 1.0);
bindAction( joystick, make, button6, TO, IDACTION_TARGET_ADJ_ENEMY, -1.0);
bindAction( joystick, slider0, TO, IDACTION_LOOK_X, deadzone, 0.01, scale, 0.6, center );
bindAction( joystick, slider1, TO, IDACTION_LOOK_Y, deadzone, 0.01, scale, 0.6, center, flip );
bindaction( joystick, make,  upov, TO, IDACTION_SHIELD_FOCUS_ADJ, "0.2" );   
bindaction( joystick, make,  dpov, TO, IDACTION_SHIELD_FOCUS_ADJ, "-0.2" );   

#-- System and Targeting Controls
bindAction( joystick, make,  button3, TO, IDACTION_CAMOUFLAGE);
bindAction( joystick, make,  button9, TO, IDACTION_REACTOR);
bindAction( joystick, make,  button2, TO, IDACTION_TARGET_SELECTED);
bindAction( joystick, make,  button1, TO, IDACTION_TARGET_CLOSEST_ENEMY);
