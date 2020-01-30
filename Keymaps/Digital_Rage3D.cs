#------------------------------------------------------------------------------
# include generic game actions
exec( "_gameActions.cs" );

#------------------------------------------------------------------------------
# include generic camera controls
exec( "_rage3DCamera.cs" );


#------------------------------------------------------------------------------
# include generic keyboard controls
exec( "_defKeyboard.cs" );


#------------------------------------------------------------------------------
#
#  Add support from Digital Rage3D Thrustmaster
#

editActionMap( Herc );
# Weapon controls
bindAction( joystick, make, button9, TO, IDACTION_FIRE, 1.0 );
bindAction( joystick, break, button9, TO, IDACTION_FIRE, 0.0 );
bindAction( joystick, make, button8, TO, IDACTION_TARGET_SELECTED );
bindAction( joystick, make, button5, TO, IDACTION_TARGET_CLOSEST_ENEMY );

# Movement and view controls
bindAction( joystick, xaxis, TO, IDACTION_LOOK_X, deadzone, 0.1, center, scale, 0.4 );
bindAction( joystick, yaxis, TO, IDACTION_LOOK_Y, deadzone, 0.1, center, scale, 0.4 );
bindAction( joystick, make, button4, TO, IDACTION_SPEED, "+1.0" );
bindAction( joystick, break, button4, TO, IDACTION_SPEED, 0.0 );
bindAction( joystick, make, button1, TO, IDACTION_SPEED, -1.0 );
bindAction( joystick, break, button1, TO, IDACTION_SPEED, 0.0 );
bindAction( joystick, make, button0, TO, IDACTION_YAW, "+1.0" );
bindAction( joystick, break, button0, TO, IDACTION_YAW, 0.0 );
bindAction( joystick, make, button2, TO, IDACTION_YAW, -1.0 );
bindAction( joystick, break, button2, TO, IDACTION_YAW, 0.0 );
bindAction( joystick, make, button3, TO, IDACTION_ZOOM_SET, 1.0 );
bindAction( joystick, break, button3, TO, IDACTION_ZOOM_SET, 0.0 );

# System controls
bindAction( joystick, make, button6, TO, IDACTION_CAMOUFLAGE );
bindAction( joystick, make, button7, TO, IDACTION_REACTOR );

