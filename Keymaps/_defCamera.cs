###############################################################################
#
# 			Generic camera control script file
#
###############################################################################


#------------------------------------------------------------------------------
#	Orbit Camera controls
#------------------------------------------------------------------------------
newActionMap( CameraOrbit );

#------- Joystick Controls
bindAction( joystick0, yaxis,  TO, IDACTION_PITCH, deadzone, 0.1, center, square );
bindAction( joystick0, xaxis,  TO, IDACTION_YAW,   deadzone, 0.1, center, square, flip );
bindAction( joystick0, rzaxis, TO, IDACTION_ROLL,  deadzone, 0.1, center, square );
bindAction( joystick0, make,  button0, TO, IDACTION_ZOOM_MODE, 1.0 );
bindAction( joystick0, break, button0, TO, IDACTION_ZOOM_MODE, 0.0 );
bindAction( joystick0, make,  button1, TO, IDACTION_NEXT );
bindAction( joystick0, make,  button2, TO, IDACTION_LOOK_X );
bindAction( joystick0, make,  button3, TO, IDACTION_LOOK_Y );

#------- Keyboard Controls

# rotate camera left/right
bindAction( keyboard, make,  left,  TO, IDACTION_YAW, -0.75 );
bindAction( keyboard, break, left,  TO, IDACTION_YAW,  0.0 );
bindAction( keyboard, make,  right, TO, IDACTION_YAW,  0.75 );
bindAction( keyboard, break, right, TO, IDACTION_YAW,  0.0 );

bindAction( keyboard, make,  shift, left,  TO, IDACTION_YAW, -0.75 );
bindAction( keyboard, break, shift, left,  TO, IDACTION_YAW,  0.0 );
bindAction( keyboard, make,  shift, right, TO, IDACTION_YAW,  0.75 );
bindAction( keyboard, break, shift, right, TO, IDACTION_YAW,  0.0 );

# rotate camera up/down
bindAction( keyboard, make,  up,   TO, IDACTION_PITCH,  0.5 );
bindAction( keyboard, break, up,   TO, IDACTION_PITCH,  0.0 );
bindAction( keyboard, make,  down, TO, IDACTION_PITCH, -0.5 );
bindAction( keyboard, break, down, TO, IDACTION_PITCH,  0.0 );

# zoom in/out
bindAction( keyboard, make,  prior, TO, IDACTION_MOVE_Y, -0.5 );
bindAction( keyboard, break, prior, TO, IDACTION_MOVE_Y,  0.0 );
bindAction( keyboard, make,  next,  TO, IDACTION_MOVE_Y,  0.5 );
bindAction( keyboard, break, next,  TO, IDACTION_MOVE_Y,  0.0 );

bindAction( keyboard, make,  shift, up,   TO, IDACTION_MOVE_Y, -0.5 );
bindAction( keyboard, break, shift, up,   TO, IDACTION_MOVE_Y,  0.0 );
bindAction( keyboard, make,  shift, down, TO, IDACTION_MOVE_Y,  0.5 );
bindAction( keyboard, break, shift, down, TO, IDACTION_MOVE_Y,  0.0 );

# next/prev object
bindAction( keyboard, make, home, TO, IDACTION_NEXT );
bindAction( keyboard, make, end,  TO, IDACTION_PREV );
