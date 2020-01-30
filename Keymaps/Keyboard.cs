#modified 10/23/98
#------------------------------------------------------------------------------
#	include generic game actions
#------------------------------------------------------------------------------
exec( "_gameActions.cs" );

#------------------------------------------------------------------------------
#	include generic camera controls
#------------------------------------------------------------------------------
exec( "_defCamera.cs" );

#------------------------------------------------------------------------------
#	include generic keyboard controls
#------------------------------------------------------------------------------
exec( "_defKeyboard.cs" );

#------------------------------------------------------------------------------
#	Keys for control of the targeting cursor
#------------------------------------------------------------------------------
editActionMap( Herc );

#------- use w,a,d,s for targeting cursor
bindAction( keyboard, make, w, TO, IDACTION_LOOK_Y, "+1.0" );
bindAction( keyboard, make, s, TO, IDACTION_LOOK_Y, -1.0 );
bindAction( keyboard, break, w, TO, IDACTION_LOOK_Y, 0.0 );
bindAction( keyboard, break, s, TO, IDACTION_LOOK_Y, 0.0 );
bindAction( keyboard, make, a, TO, IDACTION_LOOK_X, "+1.0" );
bindAction( keyboard, make, d, TO, IDACTION_LOOK_X, -1.0 );
bindAction( keyboard, break, a, TO, IDACTION_LOOK_X, 0.0 );
bindAction( keyboard, break, d, TO, IDACTION_LOOK_X, 0.0 );

bindaction( keyboard, make, shift, s, TO, IDACTION_SPOT, 1 );   
bindaction( keyboard, break, shift, s, TO, IDACTION_SPOT, "-1" ); 

bindAction( keyboard, make, e, TO, IDACTION_TARGET_SELECTED ); 

#------- use arrow keys for targeting cursor
bindAction( keyboard, make, up, TO, IDACTION_LOOK_Y, "+1.0" );
bindAction( keyboard, make, down, TO, IDACTION_LOOK_Y, -1.0 );
bindAction( keyboard, break, up, TO, IDACTION_LOOK_Y, 0.0 );
bindAction( keyboard, break, down, TO, IDACTION_LOOK_Y, 0.0 );
bindAction( keyboard, make, left, TO, IDACTION_LOOK_X, "+1.0" );
bindAction( keyboard, make, right, TO, IDACTION_LOOK_X, -1.0 );
bindAction( keyboard, break, left, TO, IDACTION_LOOK_X, 0.0 );
bindAction( keyboard, break, right, TO, IDACTION_LOOK_X, 0.0 );

