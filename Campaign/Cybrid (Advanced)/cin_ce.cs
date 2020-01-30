$Gui::PrepCinematics = True;
$Gui::widescreen = True;
$AllowHercsToMove = True;
function setDefaultMissionOptions()
{
	$server::TeamPlay = True;		// for TDM_ games
}

function player::onAdd( %this )
{
	$ThePlayer = %this;
	
}

function onCinematicStart()
{
   setWidescreen(true);

	//fadeEvent( 0, out, 0.0, 0, 0, 0 );
	//schedule("fadeEvent( 0, in, 1.0, 0, 0, 0 );", 2.0);
	focusCamera( splineCamera, path4 );

	say( 0, 1, "CE.wav" );

	//cdAudioCycle(13);
	
	setStarsVisibility(true);
	setSkyMaterialListTag(0);

	$CE01 = "cin_ce_01.wav";

	schedule( "say( 0, 2,$CE01 );", 0.0 );

        newFormation( Delta, 0,0,0,  20,-20,0,  -20,-20,0,  -40,-40,0 );
	newFormation( Zeta, 0,0,0, -40,0,0,  40,-20,0,  -20,-20,0 );

	// Trooper 1
        //order( "MissionGroup\\trooperGroup1\\Herc1", MakeLeader, true );
        //order( "MissionGroup\\trooperGroup1\\Herc1", Speed, Low );
	//order( "MissionGroup\\trooperGroup1", Formation, Delta );

	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 17.5 );

	schedule( "focusCamera( splineCamera, path2 );", 19.0 );

}
function path2::camera::waypoint1( %this )
{
	
	setStarsVisibility(false);
	setSkyMaterialListTag(IDDML_SKY_DESERT_DAY);	

	order( "MissionGroup\cybridGroup1\\Herc1", MakeLeader, true );
	order( "MissionGroup\\cybridGroup1\\Herc1", Speed, High);
	order( "MissionGroup\\cybridGroup1", Guard, "MissionGroup\\cybridPath1" );
	
	fadeEvent( 0, in, 1.0, 0, 0, 0 );

	$CE02 = "cin_ce_02.wav";
	schedule( "say( 0, 2, $CE02);", 3.0 );	
}
function path2::camera::waypoint3( %this )
{
		
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 2.0 );

	schedule( "focusCamera( splineCamera, path3 );", 3.5 );

}
function path3::camera::waypoint1( %this )
{
	
	$CE03 = "cin_ce_03.wav";
	schedule( "say( 0, 2, $CE03);", 3.0 );	

	order( "MissionGroup\cybridGroup2\\Herc1", MakeLeader, true );
	order( "MissionGroup\\cybridGroup2\\Herc1", Speed, High);
	order( "MissionGroup\\cybridGroup2", Guard, "MissionGroup\\HarabecPath1" );

	fadeEvent( 0, in, 1.0, 0, 0, 0 );
}	
function path3::camera::waypoint3( %this )
{
	order( "MissionGroup\\knightGroup1\\Herc1", Speed, Low);
	order( "MissionGroup\\knightGroup2\\Herc1", Speed, Low);
	order( "MissionGroup\\knightGroup1\\Herc1", Guard, "MissionGroup\\knightPath1" );
	order( "MissionGroup\\knightGroup2\\Herc1", Guard, "MissionGroup\\knightPath2" );

}	
function path3::camera::waypoint4( %this )
{	
	
	order( "MissionGroup\\Harabec\\Herc1", Speed, Low);
	order( "MissionGroup\\Harabec\\Herc1", Guard, "MissionGroup\\HarabecPath1" );
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 5.0 );
	
	//schedule( "focusCamera( splineCamera, path4 );", 6.5 );

	
	$CE04 = "cin_ce_04.wav";
	schedule( "say( 0, 2, $CE04);", 1.0 );	

	schedule( "fadeEvent( 0, out, 2.0, 0, 0, 0 );", 25.5 );
}
//function path4::camera::waypoint1( %this )
{
	
	setStarsVisibility(true);
	setSkyMaterialListTag(0);
	
	fadeEvent( 0, in, 1.0, 0, 0, 0 );

	$CE04 = "cin_ce_04.wav";
	schedule( "say( 0, 3, $CE04);", 1.0 );	

	schedule( "fadeEvent( 0, out, 2.0, 0, 0, 0 );", 10.0 );
	schedule( "cdAudioFadeStop();", 11.0 );

}