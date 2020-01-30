newActionMap( herc );
bindAction(keyboard0, make, "down", TO, IDACTION_SPEED, -1.000000);
bindAction(mouse0, make, button0, TO, IDACTION_FIRE, 1.000000);
bindAction(keyboard0, make, "up", TO, IDACTION_SPEED, 1.000000);
bindAction(mouse0, xaxis0, TO, IDACTION_LOOK_X, Flip, Scale, 0.500000);
bindAction(mouse0, yaxis0, TO, IDACTION_LOOK_Y, Flip, Scale, 0.500000);
bindAction(keyboard0, make, "backspace", TO, IDACTION_STOP);
bindAction(mouse0, break, button1, TO, IDACTION_TARGET_SELECTED);
bindAction(keyboard0, make, "left", TO, IDACTION_YAW, 1.000000);
bindAction(keyboard0, make, "right", TO, IDACTION_YAW, -1.000000);
bindAction(keyboard0, break, "right", TO, IDACTION_YAW, 0.000000);
bindAction(keyboard0, break, "up", TO, IDACTION_SPEED, 0.000000);
bindAction(keyboard0, break, "left", TO, IDACTION_YAW, 0.000000);
bindAction(keyboard0, break, "down", TO, IDACTION_SPEED, 0.000000);
bindAction(mouse0, break, button0, TO, IDACTION_FIRE, 0.000000);


newActionMap( cameraOrbit );
bindAction(keyboard0, make, "n", TO, IDACTION_NEXT);
bindAction(keyboard0, make, "p", TO, IDACTION_PREV);
bindAction(keyboard0, make, "up", TO, IDACTION_SPEED, 0.500000);
bindAction(keyboard0, make, "down", TO, IDACTION_SPEED, -0.500000);
bindAction(keyboard0, make, "left", TO, IDACTION_YAW, -0.750000);
bindAction(keyboard0, make, "right", TO, IDACTION_YAW, 0.750000);
bindAction(keyboard0, break, "up", TO, IDACTION_SPEED, 0.000000);
bindAction(keyboard0, break, "right", TO, IDACTION_YAW, 0.000000);
bindAction(keyboard0, break, "left", TO, IDACTION_YAW, 0.000000);
bindAction(keyboard0, break, "down", TO, IDACTION_SPEED, 0.000000);
bindAction( joystick0, make,  button0, TO, IDACTION_ZOOM_MODE, 1.0 );
bindAction( joystick0, break, button0, TO, IDACTION_ZOOM_MODE, 0.0 );


#------------------------------------------------------------------------------
# include generic game actions
#------------------------------------------------------------------------------
exec( "_gameActions.cs" );



