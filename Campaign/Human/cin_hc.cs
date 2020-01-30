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
   // mission specific preload
   preLoadFile("cin_HC.ted#5.dtb");
   preLoadFile("cin_HC.ted#9.dtb");
   preLoadFile("cin_HC.ted#13.dtb");
   preLoadFile("cin_HC.ted#14.dtb");
   preLoadFile("cin_HC.ted#11.dtb");
   preLoadFile("cin_HC.ted#10.dtb");
}

function onCinematicStart()
{
   setWidescreen(true);

	focusCamera( splineCamera, path1 );
	
	//cdAudioCycle(5);
	say( 0, 1, "scene3.wav" );
	
	$WAV1 = "cin_hc_01.wav";
	schedule( "Say( 0, 2, $WAV1 );", 1.0 );
	
        newFormation( xDelta, 0,0,0,  25,-20,0,  -10,-40,0,  0,-90,0 );
	newFormation( xZeta, 0,0,0, -40,0,0,  40,-20,0,  -20,-20,0 );

       	// Alliance 1
        order( "MissionGroup\\AllianceGroup1\\Herc1", MakeLeader, true );
        order( "MissionGroup\\AllianceGroup1\\Herc1", Speed, Medium );
	order( "MissionGroup\\AllianceGroup1", Formation, xDelta );
       	
	order( "MissionGroup\\Harabec\\Herc1", MakeLeader, true );
        order( "MissionGroup\\Harabec\\Herc1", Speed, Medium );
	order( "MissionGroup\\Harabec", Formation, xZeta );

	order( "MissionGroup\\Cannon\\Herc1", MakeLeader, true );
        order( "MissionGroup\\Cannon\\Herc1", Speed, Low );
	order( "MissionGroup\\Cannon", Formation, xZeta );
}

function path1::camera::waypoint2( %this )
{
	fadeEvent( 0, out, 2.0, 0, 0, 0 );

	schedule( "focusCamera( splineCamera, path2 );", 2.5 );
}
function path2::camera::waypoint1( %this )
{
	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
	$WAV2 = "cin_hc_02.wav";
	schedule( "Say( 0, 2, $WAV2 );", 3.0 );

	order( "MissionGroup\\AllianceGroup1\\Herc1", Guard, "MissionGroup\\AlliancePath1" );

	schedule( "fadeEvent( 0, out, 0.5, 0, 0, 0 );", 5.5 );

	schedule( "focusCamera( splineCamera, path3 );", 6.25 );
}
function path3::camera::waypoint1( %this )
{
	
	fadeEvent( 0, in, 0.5, 0, 0, 0 );
	


	schedule( "playAnimSequence( 8335, 0.0, 1.0 );", 0.25 );
	schedule( "playAnimSequence( 8336, 0.0, 1.0 );", 1.0 );
	schedule( "playAnimSequence( 8337, 0.0, 1.0 );", 2.25 );
	schedule( "playAnimSequence( 8338, 0.0, 1.0 );", 3.0 );
		
	$har = "MissionGroup\\Harabec\\Herc1";
	$can = "MissionGroup\\Cannon\\Herc1";
	$Path = "MissionGroup\\AlliancePath1";
	
	schedule( "order( $har, Guard, $Path );", 2.5 );
	schedule( "order( $can, Guard, $Path );", 5.0 );
//	playAnimSequence( 3223, 0, 1 );	

}
function path3::camera::waypoint3( %this )
{

	$WAV3 = "cin_hc_03.wav";
	schedule( "Say( 0, 2, $WAV3 );", 2.0 );

	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 5.0 );

	schedule( "focusCamera( splineCamera, path4 );", 7.0 );

}
function path4::camera::waypoint1( %this )
{
	
	fadeEvent( 0, in, 2.0, 0, 0, 0 );
	
}
function path4::camera::waypoint2( %this )
{

	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 1.5 );

	schedule( "focusCamera( splineCamera, path5 );", 2.6 );

}
function path5::camera::waypoint1( %this )
{
	
	fadeEvent( 0, in, 1.0, 0, 0, 0 );

	$WAV4 = "cin_hc_04.wav";
	schedule( "Say( 0, 2, $WAV4 );", 0.0 );
	
}
function path5::camera::waypoint3( %this )
{
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 3.0 );
	schedule( "cdAudioFadeStop();", 4.0 );

}