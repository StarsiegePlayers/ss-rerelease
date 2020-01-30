#modified 11/9/98

###############################################################################           
#
# 		Generic keyboard Herc control script file
#
###############################################################################     
       
newActionMap( Herc );     
#------------------------------------------------------------------------------
#	Movement Controls           
#------------------------------------------------------------------------------           
bindaction( keyboard, make,  numpad4, TO, IDACTION_YAW, "+1.0" );   
bindaction( keyboard, break,  numpad4, TO, IDACTION_YAW, 0 );   
bindaction( keyboard, make,  numpad6, TO, IDACTION_YAW, "-1.0" );   
bindaction( keyboard, break,  numpad6, TO, IDACTION_YAW, 0 );   
bindaction( keyboard, make,  numpad8, TO, IDACTION_SPEED, "+1.0" );   
bindaction( keyboard, break,  numpad8, TO, IDACTION_SPEED, 0 );   
bindaction( keyboard, make,  numpad2, TO, IDACTION_SPEED, "-1.0" );   
bindaction( keyboard, break,  numpad2, TO, IDACTION_SPEED, 0 );   
bindaction( keyboard, make,  numpad0, TO, IDACTION_REVERSE_THROTTLE );    
bindaction( keyboard, make,  numpad5, TO, IDACTION_STOP );    
           
bindaction( keyboard, make,  left, TO, IDACTION_YAW, "+1.0" );   
bindaction( keyboard, break,  left, TO, IDACTION_YAW, 0 );   
bindaction( keyboard, make,  right, TO, IDACTION_YAW, "-1.0" );   
bindaction( keyboard, break,  right, TO, IDACTION_YAW, 0 );   
bindaction( keyboard, make,  up, TO, IDACTION_SPEED, "+1.0" );   
bindaction( keyboard, break,  up, TO, IDACTION_SPEED, 0 );   
bindaction( keyboard, make,  down, TO, IDACTION_SPEED, "-1.0" );   
bindaction( keyboard, break,  down, TO, IDACTION_SPEED, 0 );      
bindaction( keyboard, make,  backspace, TO, IDACTION_STOP );    
           
bindaction( keyboard, make,  "[", TO, IDACTION_CENTER_TURRET );    
bindaction( keyboard, make,  "]", TO, IDACTION_CENTER_BODY );    
bindaction( keyboard, make,  c, TO, IDACTION_CROUCH );    
bindaction( keyboard, make,  numpadEnter, TO, IDACTION_REACTOR ); 
   
#------------------------------------------------------------------------------           
#	Weapon Controls           
#------------------------------------------------------------------------------
bindaction( keyboard, make, control, 1, TO, IDACTION_WEAPON_SELECT, 0 );   
bindaction( keyboard, make, control, 2, TO, IDACTION_WEAPON_SELECT, 1 );   
bindaction( keyboard, make, control, 3, TO, IDACTION_WEAPON_SELECT, 2 );   
bindaction( keyboard, make, control, 4, TO, IDACTION_WEAPON_SELECT, 3 );   
bindaction( keyboard, make, control, 5, TO, IDACTION_WEAPON_SELECT, 4 );   
bindaction( keyboard, make, control, 6, TO, IDACTION_WEAPON_SELECT, 5 );   
bindaction( keyboard, make, shift, 1, TO, IDACTION_WEAPON_GROUP_TOGGLE, 0 );   
bindaction( keyboard, make, shift, 2, TO, IDACTION_WEAPON_GROUP_TOGGLE, 1 );   
bindaction( keyboard, make, shift, 3, TO, IDACTION_WEAPON_GROUP_TOGGLE, 2 );   
bindaction( keyboard, make, shift, 4, TO, IDACTION_WEAPON_GROUP_TOGGLE, 3 );   
bindaction( keyboard, make, shift, 5, TO, IDACTION_WEAPON_GROUP_TOGGLE, 4 );   
bindaction( keyboard, make, shift, 6, TO, IDACTION_WEAPON_GROUP_TOGGLE, 5 );   
bindaction( keyboard, make,  tab, TO, IDACTION_WEAPON_MODE_SELECT );    
bindaction( keyboard, make,  l, TO, IDACTION_WEAPON_MODE_SELECT );
bindaction( keyboard, make,  "numpad+", TO, IDACTION_WEAPON_MODE_SELECT );    
bindaction( keyboard, make,  "=", TO, IDACTION_WEAPON_GROUP_ADJ, 1 );   
bindaction( keyboard, make,  "-", TO, IDACTION_WEAPON_GROUP_ADJ, "-1.0" );   
bindaction( keyboard, make,  1, TO, IDACTION_WEAPON_GROUP_SELECT, 0 );   
bindaction( keyboard, make,  2, TO, IDACTION_WEAPON_GROUP_SELECT, 1 );   
bindaction( keyboard, make,  3, TO, IDACTION_WEAPON_GROUP_SELECT, 2 );   

bindaction( keyboard, make,  space, TO, IDACTION_FIRE, 1 );   
bindaction( keyboard, break,  space, TO, IDACTION_FIRE, 0 );   

#------------------------------------------------------------------------------
#	Targeting & Navigation Controls           
#------------------------------------------------------------------------------
bindaction( keyboard, make,  t, TO, IDACTION_TARGET_CLOSEST_ENEMY );    
bindaction( keyboard, make,  y, TO, IDACTION_TARGET_ADJ_ENEMY, "+1.0" );   
bindaction( keyboard, make, shift, y, TO, IDACTION_TARGET_ADJ_ENEMY, "-1.0" );   
           
bindaction( keyboard, make,  f, TO, IDACTION_TARGET_CLOSEST_FRIENDLY );    
bindaction( keyboard, make,  g, TO, IDACTION_TARGET_ADJ_FRIENDLY, "+1.0" );   
bindaction( keyboard, make, shift, g, TO, IDACTION_TARGET_ADJ_FRIENDLY, "-1.0" );   
           
bindaction( keyboard, make,  i, TO, IDACTION_SCAN_TARGET );    
bindaction( keyboard, make,  s, TO, IDACTION_SPOT, 1 );   
bindaction( keyboard, break,  s, TO, IDACTION_SPOT, "-1" );   
           
bindaction( keyboard, make,  n, TO, IDACTION_NAVPOINT_NEXT );    
bindaction( keyboard, make, shift, n, TO, IDACTION_NAVPOINT_PREV );    
bindaction( keyboard, make,  comma, TO, IDACTION_NAVPOINT_SET, "-1" );   
bindaction( keyboard, make,  m, TO, IDACTION_DROP_MARKER );    

#------------------------------------------------------------------------------           
# 	Shield Controls           
#------------------------------------------------------------------------------
bindaction( keyboard, make,  home, TO, IDACTION_SHIELD );    
bindaction( keyboard, make,  insert, TO, IDACTION_SHIELD_TRACK );    
bindaction( keyboard, make,  prior, TO, IDACTION_SHIELD_FOCUS_ADJ, "0.2" );   
bindaction( keyboard, make,  next, TO, IDACTION_SHIELD_FOCUS_ADJ, "-0.2" );   
bindaction( keyboard, make,  delete, TO, IDACTION_SHIELD_ROTATION_ADJ, "0.125" );   
bindaction( keyboard, make,  end, TO, IDACTION_SHIELD_ROTATION_ADJ, "-0.125" );   

#------------------------------------------------------------------------------           
#	Squadmate Controls           
#------------------------------------------------------------------------------           
bindaction( keyboard, make,  F1, TO, IDACTION_ORDER_SQUADMATE_1, 0 );   
bindaction( keyboard, make,  F2, TO, IDACTION_ORDER_SQUADMATE_2, 0 );   
bindaction( keyboard, make,  F3, TO, IDACTION_ORDER_SQUADMATE_3, 0 );   
bindaction( keyboard, make,  F4, TO, IDACTION_ORDER_ALL_SQUADMATES, 0 );   
bindaction( keyboard, make,  shift, F1, TO, IDACTION_GENERAL_COMMAND, 0 );   

#------------------------------------------------------------------------------
#	Miscellaneous Controls           
#------------------------------------------------------------------------------           
bindaction( keyboard, make, shift, r, TO, IDACTION_SENSOR_RANGE_TOGGLE );    
bindaction( keyboard, make,  r, TO, IDACTION_SENSOR_MODE_TOGGLE );    
bindaction( keyboard, make,  z, TO, IDACTION_ZOOM_ADJ, 1 );   
bindaction( keyboard, break,  z, TO, IDACTION_ZOOM_ADJ, "-1.0" );   
bindaction( keyboard, make,  "numpad/", TO, IDACTION_ZOOM_ADJ, 1 );   
bindaction( keyboard, break,  "numpad/", TO, IDACTION_ZOOM_ADJ, "-1.0" );    
     
bindaction( keyboard, make,  Enter, TO, IDACTION_MAP_VIEW, 0 );   
           
bindaction( keyboard, make,  ";", TO, IDACTION_TOGGLE_NAME_TAG, 0 );   
bindaction( keyboard, make,  "'", TO, IDACTION_SET_NAME_TAG, 1 );   
bindaction( keyboard, break,  "'", TO, IDACTION_SET_NAME_TAG, 0 );   

#------------------------------------------------------------------------------
#	Special Component Controls           
#------------------------------------------------------------------------------
bindaction( keyboard, make, control, x, TO, IDACTION_CAMOUFLAGE );    
bindaction( keyboard, make, control, b, TO, IDACTION_TURBO );    
bindaction( keyboard, make, control, s, TO, IDACTION_USE_SHIELD_CAPACITOR );    
bindaction( keyboard, make, control, r, TO, IDACTION_USE_REACTOR_CAPACITOR );    
bindaction( keyboard, make, control, j, TO, IDACTION_ECM_TOGGLE );    
bindaction( keyboard, make, control, t, TO, IDACTION_THERMAL_DIFFUSER_TOGGLE );    
 
bindaction( keyboard, make, 7, TO, IDACTION_USE_SPECIAL , 0 );
bindaction( keyboard, make, 8, TO, IDACTION_USE_SPECIAL , 1 );
bindaction( keyboard, make, 9, TO, IDACTION_USE_SPECIAL , 2 );
bindaction( keyboard, make, 0, TO, IDACTION_USE_SPECIAL , 3 );


     
  