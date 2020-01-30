#modified 12:23 PM 10/26/98

###############################################################################
#	 			Game Actions
###############################################################################

newActionMap( gameActions );

#------------------------------------------------------------------------------
#	Respawn key valid after player's vehicle is destroyed
#------------------------------------------------------------------------------
bindAction(keyboard0, make, "space", TO, IDACTION_RESPAWN);
bindAction(keyboard0, make, alt, r, TO, IDACTION_RESPAWN);

#------------------------------------------------------------------------------
#	Camera controls
#------------------------------------------------------------------------------
bindAction(keyboard0, make, control, "c", TO, IDACTION_PILOT_CAMERA);
bindAction(keyboard0, make, control, "o", TO, IDACTION_ORBIT_CAMERA);
bindAction(keyboard0, make, control, "v", TO, IDACTION_ORBIT_CAMERA);

#------------------------------------------------------------------------------
#	Chat controls          
#------------------------------------------------------------------------------    
bindAction(keyboard0, make, "f5", TO, IDACTION_TOGGLE_CHAT_DISPLAY);
bindAction(keyboard0, make, "f6", TO, IDACTION_TELL_ALL);
bindAction(keyboard0, make, "f7", TO, IDACTION_TELL_TEAM);
bindAction(keyboard0, make, "f8", TO, IDACTION_TELL_TARGET);
bindAction(keyboard0, make, control, "f1", TO, IDACTION_QUICKCHAT, 1.000000);
bindAction(keyboard0, make, control, "f2", TO, IDACTION_QUICKCHAT, 2.000000);
bindAction(keyboard0, make, control, "f3", TO, IDACTION_QUICKCHAT, 3.000000);
bindAction(keyboard0, make, control, "f4", TO, IDACTION_QUICKCHAT, 4.000000);
bindAction(keyboard0, make, control, "f5", TO, IDACTION_QUICKCHAT, 5.000000);
bindAction(keyboard0, make, control, "f6", TO, IDACTION_QUICKCHAT, 6.000000);
bindAction(keyboard0, make, control, "f7", TO, IDACTION_QUICKCHAT, 7.000000);
bindAction(keyboard0, make, control, "f8", TO, IDACTION_QUICKCHAT, 8.000000);
bindAction(keyboard0, make, control, "f9", TO, IDACTION_QUICKCHAT, 9.000000);
bindAction(keyboard0, make, control, "f10", TO, IDACTION_QUICKCHAT, 10.000000);
bindAction(keyboard0, make, control, "f11", TO, IDACTION_QUICKCHAT, 11.000000);
bindAction(keyboard0, make, control, "f12", TO, IDACTION_QUICKCHAT, 12.000000);

#------------------------------------------------------------------------------
#	Single Player only Controls
#------------------------------------------------------------------------------
bindAction(keyboard0, make, "numlock", TO, IDACTION_PAUSE);

#------------------------------------------------------------------------------
#	Debug only
#------------------------------------------------------------------------------
bindAction(keyboard0, make, shift, "f9", TO, IDACTION_COLLISION_DETAIL);
bindAction(keyboard0, make, shift, "f11", TO, IDACTION_OUTLINE);


#------------------------------------------------------------------------------
#	Player prefs and scoreboard
#------------------------------------------------------------------------------
bindAction(keyboard0, make, "f9", TO, IDACTION_TOGGLE_HUD_CONFIG);
bindAction(keyboard0, make, "f11", TO, IDACTION_TOGGLE_PREF_CONFIG);
bindAction(keyboard0, make, "f12", TO, IDACTION_TOGGLE_SCOREBOARD);
