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
#  Add support for Logitech WingMan Light
#
editActionMap( Herc );

bindAction( joystick, make, button0, TO, IDACTION_FIRE, 1.0 );
bindAction( joystick, break, button0, TO, IDACTION_FIRE, 0.0 );
bindAction( joystick, make, button1, TO, IDACTION_TARGET_SELECTED );
bindAction( joystick, yaxis, TO, IDACTION_SPEED, deadzone, 0.1, center, square );
bindAction( joystick, xaxis, TO, IDACTION_YAW, deadzone, 0.1, center, square );

bindAction( mouse0, make, button0, TO, IDACTION_FIRE, 1.0 );
bindAction( mouse0, break, button0, TO, IDACTION_FIRE, 0.0 );
bindAction( mouse0, make, shift, button0, TO, IDACTION_FIRE, -1.0 );
bindAction( mouse0, break, shift, button0, TO, IDACTION_FIRE, 0.0 );
bindAction( mouse0, break, button1, TO, IDACTION_TARGET_SELECTED );
bindAction( mouse0, break, button2, TO, IDACTION_TARGET_CLOSEST_ENEMY );
bindAction( mouse0, xaxis, TO, IDACTION_LOOK_X, scale, 0.5, flip );
bindAction( mouse0, yaxis, TO, IDACTION_LOOK_Y, scale, 0.5, flip );

