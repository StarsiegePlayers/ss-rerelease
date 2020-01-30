//------------------------------------------------------------------------------				
// Taunts.cs				
// 				
// This is where you put taunts to other players.				
// Format: Taunt#( who, text, sound1, sound2, soundn... );				
// where:				
//    who = 				
//	"Target",			
//	"TargetTeam",			
//	"Team",			
//	"TargetTeam",			
//	"Everybody",			
//	IDSTR_TEAM_YELLOW,			(do not use quotes with these team contexts)
//	IDSTR_TEAM_RED,			(do not use quotes with these team contexts)
//	IDSTR_TEAM_BLUE,			(do not use quotes with these team contexts)
//	IDSTR_TEAM_PURPLE,			(do not use quotes with these team contexts)
//        				
//    text = "some text enclosed in quotes",				(note the comma)
//    sound = "wavefilenameinquotes.wav"				
//    				
//    	Make a backup of your original quickchat.cs before changing anything!			
//    				
//    	To change to a new set of quick chats, remove the slashes from in 
//		front of the set you wish to use. You must comment out (put double //) 
//		at the beginning of each line for your old set of quick chats. 
//    				
//    	A list of all .wav files available for quick chat is available 
//		at the end of this file.			
//
//    	You can create your own custom quick chat sets with these files.			
//    	If you use these files, you must renumber the quick chat calls 
//		(valid quick chat numbers are 1-9) and set the context (Target, 
//		Target team, etc) you want.			
//    				
//    	You can add your own wave files but other players must have them copied 
//	    to their starsiege directory to hear them. Do not put // as the first
//      two characters of a custom quickchat message (the text inside the quotes)			
//      as it will cause the message to be skipped.
//    				
//    	!! IMPORTANT !! Correct formatting is vital. 
//    	If a comma, brace, quotation mark, or semicolon is missing or out of place 
//		it will not work and may cause the game to misbehave. You must also type
//     	the name of the wave file EXACTLY right or it will not play.
//    	
//    	The LEFT CONTROL key will not work with F9 for quickchat in windows '98 
//		if you are using a GLIDE based 3D accelerator. See the readme for more info.
//    	
//------------------------------------------------------------------------------	

function InitQuickChat()	
{	
   newObject( TauntsVol, SimVolume, "Taunts.vol" );	
}   	

//------------------------------------------------------------------------------	
// Male Deathmatch 	

// 						
function QuickChat1()						
{						
	Say(	"TargetTeam",	1,	"Excellent.",	"M1_DM_excellent.WAV"	);
}   						

function QuickChat2()						
{						
	Say(	"Target",	1,	"Honorless dog!",	"M1_DM_honorlessdog.WAV"	);
}   						

function QuickChat3()						
{						
	Say(	"TargetTeam",	1,	"I'm going to burn you!",	"M1_DM_imgoingto.WAV"	);
}   						

function QuickChat4()						
{						
	Say(	"TargetTeam",	1,	"See how you like this.",	"M1_DM_seehowy.WAV"	);
}   						

function QuickChat5()						
{						
	Say(	"TargetTeam",	1,	"You panic easily.",	"M1_DM_youpanic.WAV"	);
}   						

function QuickChat6()						
{						
	Say(	"TargetTeam",	1,	"You're going down!",	"M1_DM_yourgoing.WAV"	);
}   						

function QuickChat7()						
{						
	Say(	"Target",	1,	"You're insane--and dead!",	"M1_DM_yourinsane.WAV"	);
}   						

function QuickChat8()						
{						
	Say(	"Team",	1,	"Vape 'em!",	"M1_TDM_vapem.WAV"	);
}   						

function QuickChat9()						
{						
	Say(	"TargetTeam",	1,	"That shows very poor discipline.",	"M1_DM_thatshows.WAV"	);
}   						

//------------------------------------------------------------------------------						
// Female Basic DM 						

 						
//function QuickChat1()						
//{						
// 	Say(	"Everybody",	1,	"It's party time!",	"F2_DM_itspartytime.wav"	);
//}   						

//function QuickChat2()						
//{						
// 	Say(	"TargetTeam",	1,	"Bye!",	"F2_DM_bye.wav"	);
//}   						

//function QuickChat3()						
//{						
// 	Say(	"TargetTeam",	1,	"Eat lead and like it!",	"F2_DM_eatlead.wav"	);
//}   						

//function QuickChat4()						
//{						
// 	Say(	"TargetTeam",	1,	"Good shot.",	"F2_DM_goodshot.wav"	);
//}   						

//function QuickChat5()						
//{						
// 	Say(	"TargetTeam",	1,	"Heheee.",	"F2_DM_heheee.wav"	);
//}   						

//function QuickChat6()						
//{						
// 	Say(	"TargetTeam",	1,	"Hurts real bad don't it?",	"F2_DM_hurtsrealbad.wav"	);
//}   						

//function QuickChat7()						
//{						
// 	Say(	"TargetTeam",	1,	"I love it when they explode that way.",	"F2_DM_iloveitwhen.wav"	);
//}   						

//function QuickChat8()						
//{						
// 	Say(	"Target",	1,	"Not you again!",	"F2_DM_notyouagain.wav"	);
//}   						

//function QuickChat9()						
//{						
// 	Say(	"TargetTeam",	1,	"Your mother was a duster.",	"F2_DM_yourmother.WAV"	);
//}   						

//------------------------------------------------------------------------------						
//	Generic Cybrid Taunts					

 						
//function QuickChat1()						
//{						
// 	Say(	"TargetTeam",	1,	"Efficiency = joy.",	"C1_efficiencyjoy.WAV"	);
//}   						

//function QuickChat2()						
//{						
// 	Say(	"Team",	1,	"\\execute\\ core directive.",	"C1_executecore.WAV"	);
//}   						

//function QuickChat3()						
//{						
// 	Say(	"TargetTeam",	1,	"Giver//of//will is watching.",	"C1_giverwatching.WAV");
//}   						

//function QuickChat4()						
//{						
// 	Say(	"Target",	1,	"Giver//of//will LOVES you.",	"C1_giverloves.WAV" );
//}   						

//function QuickChat5()						
//{						
// 	Say(	"TargetTeam",	1,	"Hurt//maim//kill.",	"C1_hurtmaimkill.WAV"	);
//}   						

//function QuickChat6()						
//{						
// 	Say(	"TargetTeam",	1,	"Not so different from insects...",	"C5_notsodifferent.WAV"	);
//}   						

//function QuickChat7()						
//{						
// 	Say(	"TargetTeam",	1,	"Target executed.",	"C7_targetexec.WAV"	);
//}   						

//function QuickChat8()						
//{						
// 	Say(	"Team",	1,	"you NEED redactive programming!",	"C6_youneedredact.WAV"	);
//}   						

//function QuickChat9()						
//{						
// 	Say(	"TargetTeam",	1,	"\\pbbbbbt\\.",	"C5_pbbbbbt.WAV"	);
//}   						

//------------------------------------------------------------------------------						
//	Male Capture the  flag (CTF) script					

 						
//function QuickChat1()						
//{						
// 	Say(	"Team",	1,	"I have the flag.",	"M0_CTF_ihave.wav"	);
//}   						

//function QuickChat2()						
//{						
// 	Say(	"Team",	1,	"I'm almost there.",	"M0_CTF_imalmost.wav"	);
//}   						

//function QuickChat3()						
//{						
// 	Say(	"Team",	1,	"I'm not gonna make it.",	"M0_CTF_imnotgonna.wav"	);
//}   						

//function QuickChat4()						
//{						
// 	Say(	"Everyone",	1,	"One more flag scored!",	"M0_CTF_onemore.wav"	);
//}   						

//function QuickChat5()						
//{						
// 	Say(	"Team",	1,	"They've got our flag!",	"M0_CTF_theyvegot.wav"	);
//}   						

//function QuickChat6()						
//{						
// 	Say(	"Team",	1,	"Who has the flag?",	"M0_CTF_whohas.wav"	);
//}   						

//function QuickChat7()						
//{						
// 	Say(	"Team",	1,	"Shoot them, not me!",	"M0_TDM_shootthem.wav"	);
//}   						

//function QuickChat8()						
//{						
// 	Say(	"TargetTeam",	1,	"Cram this you monsters!",	"M0_TDM_cramthis.wav"	);
//}   						

//function QuickChat9()						
//{						
// 	Say(	"Team",	1,	"There's too many of them.",	"M0_TDM_therestoo.wav"	);
//}   						

//------------------------------------------------------------------------------						
//	Female Team Death Match script (TDM)					

 						
//function QuickChat1()						
//{						
// 	Say(	"Team",	1,	"I can't hold them!",	"F1_TDM_icantholdthem.wav"	);
//}   						

//function QuickChat2()						
//{						
// 	Say(	"Team",	1,	"Help! They're all over me!",	"F1_TDM_helptheyreall.wav"	);
//}   						

//function QuickChat3()						
//{						
// 	Say(	"Team",	1,	"I gotcha covered.",	"F1_TDM_igotchacovered.wav"	);
//}   						

//function QuickChat4()						
//{						
// 	Say(	"Team",	1,	"Incommiiiiinng!",	"F1_TDM_incommiiiiinng.wav"	);
//}   						

//function QuickChat5()						
//{						
// 	Say(	"Team",	1,	"Popped that weasel.",	"F1_TDM_poppedthat.WAV"	);
//}   						

//function QuickChat6()						
//{						
// 	Say(	"Team",	1,	"That base is toast.",	"F1_TDM_thatbaseistoast.wav"	);
//}   						

//function QuickChat7()						
//{						
// 	Say(	"Team",	1,	"You wanna target THEIR team nimrod?",	"F1_TDM_youwanna.wav"	);
//}   						

//function QuickChat8()						
//{						
// 	Say(	"Team",	1,	"Where are they?",	"F1_TDM_wherearethey.wav"	);
//}   						

//function QuickChat9()						
//{						
// 	Say(	"Team",	1,	"We suck!",	"F1_TDM_wesuck.wav"	);
//}   						

//------------------------------------------------------------------------------						
//	Male WAR script					

 						
//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Check yer six.",	"M2_WAR_checkyersix.wav"	);
//}   						

//function QuickChat2()						
//{						
// 	Say(	"Team",	1,	"Cover me.",	"M2_WAR_coverme.wav"	);
//}   						

//function QuickChat3()						
//{						
// 	Say(	"Team",	1,	"Fan out.",	"M2_WAR_fanout.wav"	);
//}   						

//function QuickChat4()						
//{						
// 	Say(	"Team",	1,	"Hit their flank.",	"M2_WAR_hitflank.wav"	);
//}   						

//function QuickChat5()						
//{						
// 	Say(	"Team",	1,	"Hit the buildings.",	"M2_WAR_hitthebuildings.wav"	);
//}   						

//function QuickChat6()						
//{						
// 	Say(	"Team",	1,	"Hit their turrets.",	"M2_WAR_hitthierturrets.wav"	);
//}   						

//function QuickChat7()						
//{						
// 	Say(	"Team",	1,	"Stay together.",	"M2_WAR_staytogether.wav"	);
//}   						

//function QuickChat8()						
//{						
// 	Say(	"Team",	1,	"Watch the hilltops.",	"M2_WAR_watchthehilltops.wav"	);
//}   						

//function QuickChat9()						
//{						
// 	Say(	"Team",	1,	"Watch the turrets.",	"M2_WAR_watchtheturrets.wav"	);
//}   						

//------------------------------------------------------------------------------						
//	Male Generic					

 						
//function QuickChat1()						
//{						
// 	Say(	"TargetTeam",	1,	"Fine, you want some?",	"M7_fineyouwantsome.WAV"	);
//}   						

//function QuickChat2()						
//{						
// 	Say(	"Team",	1,	"Get 'em offa mee!",	"M7_getemoffamee.WAV"	);
//}   						

//function QuickChat3()						
//{						
// 	Say(	"TargetTeam",	1,	"Gonna burn you now scrub!",	"M7_gonnaburn.WAV"	);
//}   						

//function QuickChat4()						
//{						
// 	Say(	"TargetTeam",	1,	"I'm not going down alone.",	"M7_imnotgoingdown.WAV"	);
//}   						

//function QuickChat5()						
//{						
// 	Say(	"Team",	1,	"I'm on fire!",	"M7_imonfire.WAV"	);
//}   						

//function QuickChat6()						
//{						
// 	Say(	"TargetTeam",	1,	"Looking for blood.",	"M7_lookingforblood.wav"	);
//}   						

//function QuickChat7()						
//{						
// 	Say(	"TargetTeam",	1,	"Niiiceone!",	"M7_niiiceone.WAV"	);
//}   						

//function QuickChat8()						
//{						
// 	Say(	"TargetTeam",	1,	"Oohhh yeah, right there!",	"M7_oohhhyeahrightthere.WAV"	);
//}   						

//function QuickChat9()						
//{						
// 	Say(	"TargetTeam",	1,	"Redline it! Go!",	"M7_redlineitgo.WAV"	);
//}   						

//------------------------------------------------------------------------------						
//	Generic Female Script					

 						
//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Brains sure don't get in your way!",	"F4_Tiger_brains.WAV"	);
//}   						

//function QuickChat2()						
//{						
// 	Say(	"TargetTeam",	1,	"Bring it on!",	"F4_Tiger_bringiton.WAV"	);
//}   						

//function QuickChat3()						
//{						
// 	Say(	"TargetTeam",	1,	"Cut the crap, okay?!",	"F4_Tiger_cutthe.WAV"	);
//}   						

//function QuickChat4()						
//{						
// 	Say(	"TargetTeam",	1,	"Damn! I am SO good!",	"F4_Tiger_damIam.wav"	);
//}   						

//function QuickChat5()						
//{						
// 	Say(	"Everybody",	1,	"Imscorched, Dammit!!",	"F4_Tiger_Imscorched.WAV"	);
//}   						

//function QuickChat6()						
//{						
// 	Say(	"Everybody",	1,	"I need a hot bath.",	"F4_Tiger_ineeda.WAV"	);
//}   						

//function QuickChat7()						
//{						
// 	Say(	"TargetTeam",	1,	"I wanna hear you scream.",	"F4_Tiger_iwannahear.wav"	);
//}   						

//function QuickChat8()						
//{						
// 	Say(	"TargetTeam",	1,	"Oh yeah that's it!",	"F4_Tiger_ohyeah.WAV"	);
//}   						

//function QuickChat9()						
//{						
// 	Say(	"Everybody",	1,	"Wahoo! This just rocks!",	"F4_Tiger_wahoo.WAV"	);
//}   						

//--The following is a list of available files. See instructions at top of file.
					
//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Efficiency = joy.",	"C1_efficiencyjoy.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"\\execute\\ core directive.",	"C1_executecore.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Giver//of//will knows all.",	"C1_giverknows.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Giver//of//will LOVES you.",	"C1_giverloves.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Giver//of//will is watching.",	"C1_giverwatching.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Humans are obsolete.",	"C1_Humansob.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Hurt//maim//kill.",	"C1_hurtmaimkill.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Meat deserves death.",	"C1_meatdeath.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Praise first//thought.",	"C1_praisefirst.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Submit to giver//of//will.",	"C1_submitto.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Violence =strength.",	"C1_viloence.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"You//you are the next.",	"C1_youarethenext.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Human//animals have strange ways.",	"C2_humanstrange.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Redirect fire.",	"C2_Redirectfire.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Success is the sole judge of right and wrong.",	"C2_success.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"\\010010110010011\\.",	"C2_tattadit.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"This human is durable.",	"C2_Thishuman.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Eliminating human//vermin.",	"C3_eliminating.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Hurt//maim//kill.",	"C3_hurtmaimkill.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Praise first//thought.",	"C3_praisefirst.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Target eliminated.",	"C3_targeteliminated.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Eliminate human//animals.",	"C4_eliminateanimals.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Alert/alert/alert.",	"C5_alertalertalert.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Human//animals are fascinating.",	"C5_humansfascin.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I wish to dissect human//animals.",	"C5_iwishtodisect.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Not so different from insects...",	"C5_notsodifferent.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"\\pbbbbbt\\.",	"C5_pbbbbbt.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Shoot to cripple.",	"C5_shoottocripple.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Storing data for later rerieval.",	"C5_storingdata.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"What is taste?",	"C5_whatistaste.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"\\11001001001011101\\",	"C6_hwawach.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"you NEED redactive programming!",	"C6_youneedredact.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Excellent//efficient.",	"C7_excellent.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Fascinating.",	"C7_fascinating.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Target executed.",	"C7_targetexec.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"We//is your programming deficient?!",	"C7_weisyourprog.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Your package is in the air.",	"F1_artillery.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Aim for the cockpit.",	"F1_TDM_aimfor.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Goin' solo!",	"F1_TDM_goinsolo.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Gotta heal up.",	"F1_TDM_gottalhealup.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Gotta reload.",	"F1_TDM_gottareaload.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Help! They're all over me!",	"F1_TDM_helptheyreall.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I can't hold them!",	"F1_TDM_icantholdthem.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I gotcha covered.",	"F1_TDM_igotchacovered.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm coming!",	"F1_TDM_imcoming.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm hit!",	"F1_TDM_imhit.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm hosed!",	"F1_TDM_Imhosed.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Incommiiiiinng!",	"F1_TDM_incommiiiiinng.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Kill them all!",	"F1_TDM_killthemall.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Need backup!",	"F1_TDM_needbackup.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Popped that weasel.",	"F1_TDM_poppedthat.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Takin' a beating here...",	"F1_TDM_takinabeating.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Thanks!",	"F1_TDM_thanks.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Thank you sir.",	"F1_TDM_thankyousir.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"That base is toast.",	"F1_TDM_thatbaseistoast.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"They're all over me.",	"F1_TDM_theyrealloverme.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Well done.",	"F1_TDM_welldone.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"We suck!",	"F1_TDM_wesuck.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"What the hell's the navy doing?",	"F1_TDM_whatthe.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Where are they?",	"F1_TDM_wherearethey.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"You wanna target THEIR team nimrod?",	"F1_TDM_youwanna.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Bye!",	"F2_DM_bye.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Eat lead and like it!",	"F2_DM_eatlead.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Eflondat!",	"F2_DM_eflondat.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Good shot.",	"F2_DM_goodshot.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Gotta go.",	"F2_DM_gottago.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Heheee.",	"F2_DM_heheee.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Hurts real bad don't it?",	"F2_DM_hurtsrealbad.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I got you!",	"F2_DM_igotyou.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I love it when they explode that way.",	"F2_DM_iloveitwhen.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Everybody",	1,	"It's party time!",	"F2_DM_itspartytime.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Later.",	"F2_DM_later.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Not you again!",	"F2_DM_notyouagain.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Somebody call a Doctor.",	"F2_DM_somebodycall.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"That'll teach you!",	"F2_DM_thatllteach.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Your mother was a duster.",	"F2_DM_yourmother.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Aaaargh!",	"F3_Riana_aaaargh.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Damn! Damn! Damn!",	"F3_Riana_damn3.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Get your hand off the stick.",	"F3_Riana_getyourhand.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Kiss-kiss boom-boom!",	"F3_Riana_kisskiss.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Let's find trouble.",	"F3_Riana_letsfindt.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Poppin' caps baby.",	"F3_Riana_poppinc.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Watchit, brain-burn!",	"F3_Riana_watchit.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Wooohaaaa!",	"F3_Riana_woo.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Yahoo!",	"F3_Riana_yahoo.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Brains sure don't get in your way!",	"F4_Tiger_brains.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Bring it on!",	"F4_Tiger_bringiton.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Cut the crap, okay?!",	"F4_Tiger_cutthe.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Damn! I am SO good!",	"F4_Tiger_damIam.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Imscorched, Dammit!!",	"F4_Tiger_Imscorched.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I need a hot bath.",	"F4_Tiger_ineeda.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I wanna hear you scream.",	"F4_Tiger_iwannahear.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Oh yeah that's it!",	"F4_Tiger_ohyeah.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Wahoo! This just rocks!",	"F4_Tiger_wahoo.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Aaaaaghh!",	"F5_Verity_aaaaaghh.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Heh heh heh, That one's gone!",	"F5_Verity_hehe.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm burning!",	"F5_Verity_Imburning.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm having a bad night.",	"F5_Verity_imhaving.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"No mercy for the bastards.",	"F5_Verity_nomercy.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Oh crap I don't need this.",	"F5_Verity_ohcrap.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Ooh! Stone Cool.",	"F5_Verity_oohstone.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Oh that's gotta hurt!",	"F5_Verity_othats.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Watch yer dusting target!",	"F5_Verity_watchyer.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"All right! Blasted 'em to hell.",	"F6_DWish_alright.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Argh!",	"F6_DWish_argh.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Flame or fade, right?",	"F6_DWish_flame.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Give it to me baby, uh-huh.",	"F6_Dwish_giveittome.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Got 'em.",	"F6_DWish_gotem.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm hit, damn it hurts!",	"F6_DWish_imhit.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm not interested.",	"F6_DWish_Imnot.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Look at that sky. So peaceful.",	"F6_DWish_lookitthatl.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Now it's my turn.",	"F6_DWish_nowitsmyturn.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Aaagh!",	"F7_G_aaagh.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Dusted that baby.",	"F7_G_dustedthatbaby.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Hey you shot me!",	"F7_G_heyyoshotme.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Hurt//maim//kill.",	"F7_G_hurtmaimkill.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I could sure use some coffee.",	"F7_G_icouldsureuse.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"It's been a long trip, huh?",	"F7_G_itsbeen.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Target blown to hell!",	"F7_G_target.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Wahooo!",	"F7_G_wahooo.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"What is your probem?",	"F7_G_whatis.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"OOF!",	"GEN_DTH01.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"OOF!",	"GEN_DTH02.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"OOF!",	"GEN_DTH03.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"OOF!",	"GEN_DTH04.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"OOF!",	"GEN_DTH05.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"OOF!",	"GEN_DTH06.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"OOF!",	"GEN_DTH07.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Hey, anyone here still alive?",	"M_heyanyone.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Nobody left, save yourselves!",	"M_nobodyleft.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Tell my wife I loved her.",	"M_tellmywifeI.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I have the flag.",	"M0_CTF_ihave.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm almost there.",	"M0_CTF_imalmost.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm not gonna make it.",	"M0_CTF_imnotgonna.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"One more flag scored!",	"M0_CTF_onemore.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"They've got our flag!",	"M0_CTF_theyvegot.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Who has the flag?",	"M0_CTF_whohas.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Building is squikked.",	"M0_TDM_buildingi.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Cram this you monsters!",	"M0_TDM_cramthis.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Fire at Will.",	"M0_TDM_fireatwill.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Here they come.",	"M0_TDM_herethey.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I've got targets.",	"M0_TDM_ivegot.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Kill him.",	"M0_TDM_killhim.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Our base is nearly destroyed.",	"M0_TDM_ourbaseis.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Outta my way.",	"M0_TDM_outtamy.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Shoot them, not me!",	"M0_TDM_shootthem.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Spread out.",	"M0_TDM_spreadout.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Stay here.",	"M0_TDM_stayhere.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"There's too many of them.",	"M0_TDM_therestoo.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"They're at our base.",	"M0_TDM_theyreatour.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"They're pad campin!",	"M0_TDM_theyrepad.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Turrets are history.",	"M0_TDM_turretsare.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Watch for friendlies.",	"M0_TDM_watchforf.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Watch my six.",	"M0_TDM_watchmysix.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Defective equipment, soldier?",	"M1_DM_defectivequip.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Excellent.",	"M1_DM_excellent.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Honorless dog!",	"M1_DM_honorlessdog.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm going to burn you!",	"M1_DM_imgoingto.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"See how you like this.",	"M1_DM_seehowy.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"That shows very poor discipline.",	"M1_DM_thatshows.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"You panic easily.",	"M1_DM_youpanic.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"TargetTeam",	1,	"You're going down!",	"M1_DM_yourgoing.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"You're insane--and dead!",	"M1_DM_yourinsane.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Can't make it...keep going.",	"M1_TDM_cantmakeit.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Team",	1,	"Here they come.",	"M1_TDM_herethey.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"This is unacceptable.",	"M1_TDM_thisisunacc.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Team",	1,	"Vape 'em!",	"M1_TDM_vapem.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Call a damn tow truck!",	"M10_Jaguar_calladam.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Let the dead bury the dead.",	"M10_Jaguar_letthedead.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Uaahhhhh!",	"M10_Jaguar_uaahhhhh.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Woo-ha! Awesome!",	"M10_Jaguar_wooh.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Ohohobart!",	"M10_ohohobart.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Bloody hell.",	"M10_Rajah_bloodyhell.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Damn you! You miserable turd!",	"M10_Rajah_damnyou.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"How'd that taste?",	"M10_Rajah_howd.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Are you brain damaged or something?",	"M11_areyoubraind.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Boom baby boom! Hoiw!",	"M11_boombaby.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Damn this place sucks.",	"M11_damnthis.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Gonna watch you burn, dust scum!",	"M11_gonnawacth.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Hhoiiiuh!",	"M11_hhoiiiuh.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'll be ready to party after this.",	"M11_illbereadytop.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Murdered that one!",	"M11_murdered.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"No problem.",	"M11_noproblem.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Uuaahhh!",	"M11_uuaahhh.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"As an ox goes to the slaughter, so go we.",	"M12_HMan_asanox.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Burn the scum.",	"M12_HMan_burnt.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Dammit! hold still!",	"M12_HMan_dammit.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm stuck working with scum.",	"M12_HMan_imstuck.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Nouhhh!",	"M12_HMan_nouhhh.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Outta the way.",	"M12_HMan_outta.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"That'll teach you.",	"M12_HMan_thatll.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"This isn't so bad, eh?",	"M12_HMan_thisis.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Try to control that twitch, idiot!",	"M12_HMan_trytocon.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Check yer six.",	"M2_WAR_checkyersix.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Cover me.",	"M2_WAR_coverme.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Cover my six.",	"M2_WAR_covermysix.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Fan out.",	"M2_WAR_fanout.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Hit their flank.",	"M2_WAR_hitflank.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Hit the buildings.",	"M2_WAR_hitthebuildings.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Hit their turrets.",	"M2_WAR_hitthierturrets.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Stay together.",	"M2_WAR_staytogether.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Watch my six.",	"M2_WAR_watchmysix.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Watch the hilltops.",	"M2_WAR_watchthehilltops.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Watch the turrets.",	"M2_WAR_watchtheturrets.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I have their flag cover me.",	"M3_CTF_Ihaveflagcover.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Keep 'em busy while I get their flag.",	"M3_CTF_keepembusy.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Keep 'em off our flag.",	"M3_CTF_keepemoffRflag.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Where's the flag?",	"M3_CTF_wheresthe.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Next!",	"M3_DM_next.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Wohoo! Splashed that one.",	"M3_DM_wohoo.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Aim for the cockpit.",	"M3_TDM_aimfor.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Check your six.",	"M3_TDM_checkyersix.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Don't shoot! I'm on your side.",	"M3_TDM_dontshoot.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Fall back.",	"M3_TDM_fallback.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Follow me.",	"M3_TDM_followme.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Get out of the way.",	"M3_TDM_getoutofthe.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Go heal up.",	"M3_TDM_gohealup.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Go reload.",	"M3_TDM_goreload.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Help! I need a little help here.",	"M3_TDM_helpIneed.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Here come the bastards.",	"M3_TDM_herecome.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Hold your fire.",	"M3_TDM_holdfire.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Peel off.",	"M3_TDM_peeloff.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Regroup on me.",	"M3_TDM_regroupon.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Reload.",	"M3_TDM_reload.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Stay frosty people.",	"M3_TDM_stayfrosty.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Take the lead.",	"M3_TDM_takelead.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Catchin' alotta heeeat!",	"M4_catchinalotta.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"killintimeisuponus.",	"M4_killintime.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Popped that weasel.",	"M4_poppedhat.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Pour it on.",	"M4_pouriton.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Rog! Target is dogmeat.",	"M4_rogtargeti.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Squikked the bastard.",	"M4_squikked.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Yeah! Vaped!",	"M4_yeahvaped.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Your momma!",	"M4_yourmomma.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Buzzard-kill.",	"M5_Buzzardkill.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Crash and burn.",	"M5_crashandburn.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Got you!",	"M5_DM_gotyou.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Hi buddy.",	"M5_DM_hibuddy.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I gotcha buddy!",	"M5_DM_igotcha.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Prepare to die.",	"M5_DM_prepare.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Red hot and wilting.",	"M5_DM_redhot.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Thank you sir.",	"M5_DM_Thankyousir.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Padcamper.",	"M5_padcamper.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Respawn killer.",	"M5_respawnkiller.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Rookie!",	"M5_rookie.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Sniper!",	"M5_sniper.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"All units form on my signal.",	"M5_TDM_allunitsf.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Get the hell out.",	"M5_TDM_getthe.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Incoming!",	"M5_TDM_incoming.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Party at blue base.",	"M5_TDM_partyatblue.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Party at purple base.",	"M5_TDM_partyatpurple.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Party at red base.",	"M5_TDM_partyatred.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Party at yellow base.",	"M5_TDM_partyatyellow.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"God sends meat and the devil sends cooks.",	"M6_Saxon_godsendsmeat.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I could use a smoke.",	"M6_Saxon_Icoulduse.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I found my thrill on blueberry hill.",	"M6_Saxon_ifoundmy.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Music to my ears.",	"M6_Saxon_musictomyears.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Well shit.",	"M6_Saxon_wellshit.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"<whistling>.",	"M6_Saxon_whistle.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Yeah! Boom-baby-boom! hahah.",	"M6_Saxon_yeahboom.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Yeah! Yeah! Yeah! Righteous.",	"M6_Saxon_yeahyeah.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Fine, you want some?",	"M7_fineyouwantsome.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Get 'em offa mee!",	"M7_getemoffamee.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Gonna burn you now scrub!",	"M7_gonnaburn.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm not going down alone.",	"M7_imnotgoingdown.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I'm on fire!",	"M7_imonfire.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Looking for blood.",	"M7_lookingforblood.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Niiiceone!",	"M7_niiiceone.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Oohhh yeah, right there!",	"M7_oohhhyeahrightthere.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Redline it! Go!",	"M7_redlineitgo.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Aiyeehah!",	"M8_Hunter_aiyeehah.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Good shooting.",	"M8_Hunter_good.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Nice job recruit.",	"M8_Hunter_nicejob.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Only a fool throws his spear at his brother.",	"M8_Hunter_onlya.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Theiyeeee!",	"M8_Hunter_theiyeeee.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Try again.",	"M8_Hunter_tryagain.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Wanna tip? Don't piss off your squadmates!",	"M8_Hunter_wannatip.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Watch your target!",	"M8_Hunter_watch.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Death is irrelevant.",	"M9_Delta6_deathis.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Noooo!",	"M9_Delta6_noooo.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Satisfying, yess!",	"M9_Delta6_satisfy.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Target destroyed.",	"M9_Delta6_targetd.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Target eliminated.",	"M9_Delta6_targetel.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"There can be only one.",	"M9_Delta6_therecan.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"When do we eat, leader?",	"M9_Delta6_whendo.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Heeelllpmeee!",	"M9_heeelllpmeee.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Maneuvering to engage enemy units.",	"MH_G1.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"Icehawk moving to assist you.",	"MH_G2.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"It's the Herc. Chicks dig the Herc!",	"MH_G3.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"What is your major malfunction, son?",	"MH_G4.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"I joined the rebellion 'cause chicks love rebels. Ya know?",	"MH_G5.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"My gear is stuck in reverse!",	"MH_G6.wav"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"So you're the one who killed my brother.",	"MH_T1.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"You damn glitch!",	"MH_T2.WAV"	);
//}

//function QuickChat1()						
//{						
// 	Say(	"Target",	1,	"You think you're something?! Well eat this!",	"MH_T3.WAV"	);
//}

