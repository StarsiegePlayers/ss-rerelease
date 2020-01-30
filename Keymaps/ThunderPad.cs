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
# Add support for Logitech ThunderPad Digital
#
editActionMap( Herc );

bindAction( joystick, xaxis, TO, IDACTION_YAW, center, square );
bindAction( joystick, yaxis, TO, IDACTION_SPEED, center, square );
bindAction( joystick, make, button0, TO, IDACTION_FIRE, 1.0 );
bindAction( joystick, break, button0, TO, IDACTION_FIRE, 0.0 );
bindAction( joystick, make, button1, TO, IDACTION_TARGET_SELECTED );
bindAction( joystick, make, button2, TO, IDACTION_WEAPON_MODE_SELECT);
bindAction( joystick, make, button3, TO, IDACTION_CAMOUFLAGE );
bindAction( joystick, make, button4, TO, IDACTION_CROUCH, 1.0 );
bindAction( joystick, break, button4, TO, IDACTION_CROUCH, 0.0 );
bindAction( joystick, make, button5, TO, IDACTION_SHIELD_TRACK );
bindAction( joystick, make, button6, TO, IDACTION_WEAPON_ADJ, -1.0 );
bindAction( joystick, make, button7, TO, IDACTION_WEAPON_ADJ, 1.0 );

