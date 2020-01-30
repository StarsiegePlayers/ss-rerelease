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
# Add generic joystick support
#
editActionMap( Herc );

#-- Weapon controls
bindAction( joystick, make, button0, TO, IDACTION_FIRE, 1.0 );
bindAction( joystick, break, button0, TO, IDACTION_FIRE, 0.0 );
bindAction( joystick, make, button1, TO, IDACTION_TARGET_SELECTED );
bindAction( joystick, make, button2, TO, IDACTION_TARGET_CLOSEST_ENEMY );
bindAction( joystick, make, button4, TO, IDACTION_WEAPON_MODE_SELECT );

#-- System controls
bindAction( joystick, make, button5, TO, IDACTION_SHIELD_TRACK );
bindAction( joystick, make, button3, TO, IDACTION_CAMOUFLAGE );

#-- Movement controls
bindAction( joystick, xaxis, TO, IDACTION_YAW, deadzone, 0.1, center, square );
bindAction( joystick, yaxis, TO, IDACTION_SPEED, deadzone, 0.1, center, square );
bindAction( joystick, rzaxis, TO, IDACTION_SHIELD_FOCUS_ADJ, deadzone, 0.1, center, square );
bindAction( joystick, xpov, TO, IDACTION_LOOK_X, center );
bindAction( joystick, ypov, TO, IDACTION_LOOK_Y, center );

