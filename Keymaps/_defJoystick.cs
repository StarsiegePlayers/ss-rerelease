#Modified 2/2/99

###############################################################################           
#
# 		Generic Joystick Herc control script file
#
###############################################################################     

editActionMap( Herc );

# Joystick Controls           
bindAction( joystick, make,  button0, TO, IDACTION_FIRE, 1 );   
bindAction( joystick, break, button0, TO, IDACTION_FIRE, 0 );   
bindAction( joystick, make,  button1, TO, IDACTION_TARGET_SELECTED );    

# multi-button joystick
bindAction( joystick, make,  button2, TO, IDACTION_WEAPON_MODE_SELECT );    
bindAction( joystick, make,  button3, TO, IDACTION_WEAPON_GROUP_ADJ, 1 );    
bindAction( joystick, make,  button4, TO, IDACTION_REVERSE_THROTTLE );   
bindAction( joystick, make,  button5, TO, IDACTION_SENSOR_MODE_TOGGLE );    
bindAction( joystick, make,  button6, TO, IDACTION_REACTOR );    
bindAction( joystick, make,  button7, TO, IDACTION_SHIELD );    
           
bindAction( joystick,   xaxis, TO, IDACTION_YAW, deadzone, 0.1, center, square );
bindAction( joystick,   yaxis, TO, IDACTION_SPEED, deadzone, 0.1, center, square );        
#bindAction( joystick,   zaxis, TO, IDACTION_ZOOM_ADJ, deadzone, 0.1, center ); 
           
# comment these 2 lines out if you have problem with your analog joystick           
bindAction( joystick,   xpov, TO, IDACTION_LOOK_X, center );   
bindAction( joystick,   ypov, TO, IDACTION_LOOK_Y, center );   

