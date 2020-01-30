#------------------------------------------------------------------------------
#
# Generic camera control script file
#


#------------------------------------------------------------------------------
newActionMap( CameraOrbit );
#------- Joystick Control
bindAction( joystick0, yaxis, TO, IDACTION_PITCH, deadzone, 0.1, center, square );
bindAction( joystick0, xaxis, TO, IDACTION_YAW, deadzone, 0.1, center, square, flip );
bindAction( joystick0, make, button0, TO, IDACTION_NEXT );
bindAction( joystick0, make, button1, TO, IDACTION_TURBO );
bindAction( joystick0, make, button2, TO, IDACTION_LOOK_X );
bindAction( joystick0, make, button3, TO, IDACTION_LOOK_Y );

# rotate clockwise/counter-clockwise
bindAction( keyboard, make, left, TO, IDACTION_YAW, -0.75 );
bindAction( keyboard, make, right, TO, IDACTION_YAW, "+0.75" );
bindAction( keyboard, break, left, TO, IDACTION_YAW, 0.0 );
bindAction( keyboard, break, right, TO, IDACTION_YAW, 0.0 );

# rotate up/down
bindAction( keyboard, make, up, TO, IDACTION_PITCH, 0.5 );
bindAction( keyboard, break, up, TO, IDACTION_PITCH, 0.0 );
bindAction( keyboard, make, down, TO, IDACTION_PITCH, -0.5 );
bindAction( keyboard, break, down, TO, IDACTION_PITCH, 0.0 );

# zoom in/out
bindAction( keyboard, make, shift, up, TO, IDACTION_MOVE_Y, -0.5 );
bindAction( keyboard, break, shift, up, TO, IDACTION_MOVE_Y, 0.0 );
bindAction( keyboard, make, shift, down, TO, IDACTION_MOVE_Y, 0.5 );
bindAction( keyboard, break, shift, down, TO, IDACTION_MOVE_Y, 0.0 );
bindAction( joystick0, make, button4, TO, IDACTION_MOVE_Y, -1.0 );
bindAction( joystick0, break, button4, TO, IDACTION_MOVE_Y, 0.0 );
bindAction( joystick0, make, button1, TO, IDACTION_MOVE_Y, 1.0 );
bindAction( joystick0, break, button1, TO, IDACTION_MOVE_Y, 0.0 );
bindAction( joystick0, make,  button0, TO, IDACTION_ZOOM_MODE, 1.0 );
bindAction( joystick0, break, button0, TO, IDACTION_ZOOM_MODE, 0.0 );

