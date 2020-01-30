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
# Add support for Logitech WingMan Warrior
#
editActionMap( Herc );

bindAction( joystick, zaxis, TO, IDACTION_SPEED, deadzone, 0.1, center, square );
bindAction( joystick, yaxis, TO, IDACTION_SPEED, deadzone, 0.1, center, square );
bindAction( joystick, xaxis, TO, IDACTION_YAW, deadzone, 0.1, center, square );
bindAction( joystick, rzaxis, TO, IDACTION_LOOK_X, deadzone, 0.1, center, square );
bindAction( joystick, xpov, TO, IDACTION_SHIELD_FOCUS_ADJ );
bindAction( joystick, ypov, TO, IDACTION_LOOK_Y );

bindAction( joystick, make, button0, TO, IDACTION_FIRE, 1.0 );
bindAction( joystick, break, button0, TO, IDACTION_FIRE, 0.0 );
bindAction( joystick, make, button1, TO, IDACTION_CAMOUFLAGE );
bindAction( joystick, make, button2, TO, IDACTION_TARGET_SELECTED );
bindAction( joystick, make, button3, TO, IDACTION_MODE_SELECT, 1.0 );




