#------------------------------------------------------------------------------
# include generic game actions
exec( "_gameActions.cs" );

#------------------------------------------------------------------------------
#
# NOTE: This key binding script may not work with analog joysticks
# if their readings fluctuate too much.
#


#------------------------------------------------------------------------------
# include generic camera controls
exec( "_defCamera.cs" );


#------------------------------------------------------------------------------
# include generic keyboard controls
exec( "_defKeyboard.cs" );


#------------------------------------------------------------------------------
#
# Add mouse to contol of the targeting cursor
#
editActionMap( Herc );

bindAction( mouse0, make, button0, TO, IDACTION_FIRE, 1.0 );
bindAction( mouse0, break, button0, TO, IDACTION_FIRE, 0.0 );
bindAction( mouse0, break, button1, TO, IDACTION_TARGET_SELECTED );
bindAction( mouse0, xaxis, TO, IDACTION_LOOK_X, scale, 0.9, flip );
bindAction( mouse0, yaxis, TO, IDACTION_LOOK_Y, scale, 0.9, flip );

bindAction( joystick, make, button0, TO, IDACTION_CAMOUFLAGE );
bindAction( joystick, make, button1, TO, IDACTION_TURBO );
bindAction( joystick, make, button2, TO, IDACTION_TARGET_CLOSEST_ENEMY );
bindAction( joystick, make, button3, TO, IDACTION_WEAPON_MODE_SELECT);
bindAction( joystick, make, button4, TO, IDACTION_REACTOR );
bindAction( joystick, make, upov, TO, IDACTION_ZOOM_ADJ, 1.0 );
bindAction( joystick, break, upov, TO, IDACTION_ZOOM_ADJ, 0.0);
bindAction( joystick, make, dpov, TO, IDACTION_ZOOM_ADJ, -1.0);
bindAction( joystick, break, dpov, TO, IDACTION_ZOOM_ADJ, 0.0);
bindAction( joystick, make, rpov, TO, IDACTION_SENSOR_RANGE_TOGGLE);
bindAction( joystick, make, lpov, TO, IDACTION_SENSOR_MODE_TOGGLE);

bindAction( keyboard, make, p, TO, IDACTION_REACTOR );

bindAction( joystick, xaxis, TO, IDACTION_YAW, deadzone, 0.1, center, square );
bindAction( joystick, yaxis, TO, IDACTION_SPEED, deadzone, 0.1, center, square );
bindAction( joystick, rzaxis, TO, IDACTION_SHIELD_FOCUS_ADJ, deadzone, 0.1, center, scale, 0.2 );
//bindAction( joystick, zaxis, TO, IDACTION_SHIELD_FOCUS_ADJ,deadzone, 0.1, center );

