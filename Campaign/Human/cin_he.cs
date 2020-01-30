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
   preLoadFile("cin_he.ted#11.dtb");
   preLoadFile("cin_he.ted#14.dtb");
   preLoadFile("cin_he.ted#15.dtb");
}

function onCinematicStart()
{
   setWidescreen(true);

	focusCamera( splineCamera, path1 );
	//cdAudioCycle(12);
	say( 0, 1, "scene5.wav" );	

	setposition ( "MissionGroup\\space\\Pluto", 0, 0, 0);

	damageObject ( "MissionGroup\\DiasIrae\\hTitanrefinery1", 1000);
	damageObject ( "MissionGroup\\DiasIrae\\hTitanpower2", 1000);
	damageObject ( "MissionGroup\\DiasIrae\\hTitanbigdish1", 1000);
	damageObject ( "MissionGroup\\DiasIrae\\hTitantroopercom1", 1000);

	newFormation( XDelta, 0,0,0,  30,-30,0,  -30,-30,0,  30,-90,0 );
	newFormation( Zeta, 0,0,0, -40,0,0,  40,-20,0,  -20,-20,0 );

       	// Alliance 1
        order( "MissionGroup\\Caanon\\Herc1", MakeLeader, true );
        order( "MissionGroup\\Caanon\\Herc1", Speed, High );
	order( "MissionGroup\\Caanon", Formation, XDelta );

	// Alliance 2
        order( "MissionGroup\\AllyGroup2\\Herc1", MakeLeader, true );
        order( "MissionGroup\\AllyGroup2\\Herc1", Speed, Low );
	order( "MissionGroup\\AllyGroup2", Formation, XDelta );

	// Cybrid 1
        order( "MissionGroup\\cyGroup1\\Herc1", MakeLeader, true );
        order( "MissionGroup\\cyGroup1\\Herc1", Speed, High );
	order( "MissionGroup\\cyGroup1", Formation, XDelta );

	// Cybrid 2
        order( "MissionGroup\\cyGroup2\\Herc1", MakeLeader, true );
        order( "MissionGroup\\cyGroup2\\Herc1", Speed, High );
	order( "MissionGroup\\cyGroup2", Formation, XDelta );

	// Cybrid 3
        order( "MissionGroup\\cyGroup3\\Herc1", MakeLeader, true );
        order( "MissionGroup\\cyGroup3\\Herc1", Speed,High );
	order( "MissionGroup\\cyGroup3", Formation, XDelta );

	$S1 = "cin_he_01.wav";
	schedule( "Say( 0, 2, $S1);", 4.0 );

	order( "MissionGroup\\cyGroup1", Guard, "MissionGroup\\cyPath1" );
	order( "MissionGroup\\cyGroup2", Guard, "MissionGroup\\cyPath2" );
	order( "MissionGroup\\cyGroup3", Guard, "MissionGroup\\cyPath3" );
	order( "MissionGroup\\AllyGroup2", Guard, "MissionGroup\\AllyPath1" );
	
}
function path1::camera::waypoint2( %this )
{

	$S2 = "cin_he_02.wav";
	schedule( "Say( 0, 2, $S2);", 3.5 );

	damageObject ( "MissionGroup\\DiasIrae\\hTitanDiasIrae1", 2000);

	order( "MissionGroup\\cyGroup4", Speed, Medium);
	order( "MissionGroup\\cyGroup4", Guard, "MissionGroup\\cyPath4" );	
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 6.0 );
	schedule( "focusCamera( splineCamera, path5 );", 7.1 );
	
}
function path2::camera::waypoint1( %this )
{

	$S3 = "cin_he_03.wav";
	schedule( "Say( 0, 2, $S3);", 2.0 );

	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 7.0 );

	schedule( "focusCamera( splineCamera, path3 );", 8.1);
		
}
function path3::camera::waypoint1( %this )
{

	fadeEvent( 0, in, 1.0, 0, 0, 0 );

	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 4.5 );
	
	schedule( "focusCamera( splineCamera, path4 );", 6.0 );
		
}
function path4::camera::waypoint1( %this )
{

	Say( 0, 2, "cin_he_04.wav");

	fadeEvent( 0, in, 1.0, 0, 0, 0 );

}
function path4::camera::waypoint2( %this )
{

	fadeEvent( 0, out, 1.0, 0, 0, 0 );

	schedule( "focusCamera( splineCamera, path8 );",1.5 );
		
}	
function path5::camera::waypoint1( %this )
{
	
	//Say( 0, 2, "cin_he_05.wav");	
	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
	$CAN = "MissionGroup\\Caanon\\Herc1";
	$APATH1 = "MissionGroup\\AllyPath1";
	$CY1 = "MissionGroup\\cyGroup1\\Herc1";
	
	//schedule( "order( $CAN, Guard, $APATH1 );", 4.0 );
	schedule( "order( $CAN, Attack, $CY1 );", 2.0 );

	schedule( "focusCamera( splineCamera, path6 );", 3.0 );

}
function path6::camera::waypoint1( %this )
{

	$CY2 = "MissionGroup\\cyGroup1\\Herc1";
	$CY1 = "MissionGroup\\cyGroup2\\Herc1";
	$AL2 = "MissionGroup\\Caanon\\Herc1";

	//schedule( "setDominantCamera($CY2, $CAN, 0, -25, 20);", 5.0 );
	
	schedule( "setFlybyCamera($CY1, cameraOffset -6, 40, -2);", 2.5 );
	schedule( "setFlybyCamera($AL2, cameraOffset -6, 60, 0);", 5.0 );
	
	//schedule( "setFlybyCamera($CY2, cameraOffset -6, 40, -2);", 10.0 );
	//schedule( "setCombatCamera($CAN,$CY2, R, 60);", 12.0 );
	
	schedule( "focusCamera( splineCamera, path7 );", 9.0 );	

}
function path7::camera::waypoint1( %this )
{
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 4.0 );
	schedule( "focusCamera( splineCamera, path2 );", 5.1 );

	setposition ( "MissionGroup\\space\\Pluto", -4780.958984, -5241.195800, 2279.973632);
}
function path8::camera::waypoint1( %this )
{
	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 9.0 );
	schedule( "cdAudioFadeStop();", 10.0 );
}