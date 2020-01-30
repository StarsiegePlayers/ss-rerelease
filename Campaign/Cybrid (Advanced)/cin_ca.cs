$AllowHercsToMove = True;
function setDefaultMissionOptions()
{
	$server::TeamPlay = True;		// for TDM_ games
}

function player::onAdd( %this )
{
	$ThePlayer = %this;
}

function onMissionPreLoad()
{
   // mission specific preloads
   preLoadFile("cin_ca.ted#13.dtb");
   preLoadFile("cin_ca.ted#7.dtb");
   preLoadFile("cin_ca.ted#5.dtb");
   preLoadFile("cin_ca.ted#9.dtb");
   preLoadFile("cin_ca.ted#14.dtb");
   preLoadFile("cin_ca.ted#15.dtb");
   preLoadFile("cin_ca.ted#6.dtb");
   preLoadFile("cin_ca.ted#11.dtb");
}

function onCinematicStart()
{
   setWidescreen(true);

	focusCamera( splineCamera, path1 );

	say( 0, 1, "CA.wav" );
	//cdAudioCycle(12);
	
	$CA01 = "cin_ca_01.wav";

	schedule( "say( 0, 2, $CA01);", 1.0 );

        newFormation( xDelta, 0,0,0,  20,-20,0,  -20,-20,0,  -40,-40,0 );
	newFormation( xZeta, 0,0,0, -40,0,0,  40,-20,0,  -20,-20,0 );

	// Trooper 1
        order( "MissionGroup\\trooperGroup1\\Herc1", MakeLeader, true );
        order( "MissionGroup\\trooperGroup1\\Herc1", Speed, Low );
	order( "MissionGroup\\trooperGroup1", Formation, xDelta );


       	// Cybrid 1
        order( "MissionGroup\\cyGroup1\\Herc1", MakeLeader, true );
        order( "MissionGroup\\cyGroup1\\Herc1", Speed, Low );
	order( "MissionGroup\\cyGroup1", Formation, xDelta );

 	// Cybrid 2
        order( "MissionGroup\\cyGroup2\\Herc1", MakeLeader, true );
        order( "MissionGroup\\cyGroup2\\Herc1", Speed, Low );
	order( "MissionGroup\\cyGroup2", Formation, xDelta );
	
	$CA02 = "cin_ca_02.wav";

	schedule( "say( 0, 2, $CA02);", 12.0 );
       	
}
function path1::camera::waypoint3( %this )
{
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 5.5 );

	schedule( "focusCamera( splineCamera, path2 );", 7.0 );

}
function path2::camera::waypoint1( %this )
{
		
	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 6.4 );

	schedule( "focusCamera( splineCamera, path3 );", 7.5 );

}
function path3::camera::waypoint1( %this )
{
	
	say( 0, 2,"cin_ca_03.wav");

	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
	order( "MissionGroup\\trooperGroup1\\Herc1", Guard, "MissionGroup\\trooperPath1" );
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 13.5 );

	schedule( "focusCamera( splineCamera, path4 );", 14.6 );
	
	$CA04 = "cin_ca_04.wav";

	schedule( "say( 0, 2, $CA04);", 13.0 );
}
function path4::camera::waypoint1( %this )
{
	
	fadeEvent( 0, in, 0.5, 0, 0, 0 );
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 14.0 );

	schedule( "focusCamera( splineCamera, path5 );", 15.5 );

}
function path5::camera::waypoint1( %this )
{

	fadeEvent( 0, in, 1.0, 0, 0, 0 );	
	
	say( 0, 2,"cin_ca_05.wav");	

	order( "MissionGroup\\cyGroup1\\Herc1", Guard, "MissionGroup\\cyPath1" );
	order( "MissionGroup\\cyGroup2\\Herc1", Guard, "MissionGroup\\cyPath2" );

	$CY1 = "MissionGroup\\cyGroup1\\Herc1";
	$CY2 = "MissionGroup\\cyGroup2\\Herc1";
	$TROOP1 = "MissionGroup\\trooperBase";

	schedule( "order( $CY1, Attack, $TROOP1 );", 15.0 );
	schedule( "order( $CY2, Attack, $TROOP1 );", 15.0 );

	schedule( "order( $TROOP1, Attack, $CY1 );", 15.0 );
	schedule( "order( $TROOP1, Attack, $CY2 );", 15.0 );

	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 20.0 );
	schedule( "cdAudioFadeStop();", 20.0 );

}