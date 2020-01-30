#modified 10/25/98
#------------------------------------------------------------------------------
# include generic game actions
#------------------------------------------------------------------------------
exec( "_gameActions.cs" );

#------------------------------------------------------------------------------
# include generic camera controls
#------------------------------------------------------------------------------
exec( "_defCamera.cs" );


#------------------------------------------------------------------------------
# include generic keyboard controls
#------------------------------------------------------------------------------
exec( "_defKeyboard.cs" );


#------------------------------------------------------------------------------
# include generic joystick controls
#------------------------------------------------------------------------------
exec( "_defJoystick.cs" );
     

#------------------------------------------------------------------------------
#
editActionMap( Herc );

# Add mouse to control of the targeting cursor           
#           
bindAction( mouse0, make,  button0, TO, IDACTION_FIRE, 1 );   
bindAction( mouse0, break,  button0, TO, IDACTION_FIRE, 0 );   
bindAction( mouse0, break,  button1, TO, IDACTION_TARGET_SELECTED );    
bindAction( mouse0, break,  button2, TO, IDACTION_TARGET_CLOSEST_ENEMY );    
bindAction( mouse0, xaxis,   TO, IDACTION_LOOK_X, scale, 0.5, flip ); 
bindAction( mouse0, yaxis,   TO, IDACTION_LOOK_Y, scale, 0.5, flip ); 

