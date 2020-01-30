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
   preLoadFile("cin_hd.ted#15.dtb");
}
function onCinematicStart()
{
   setWidescreen(true);
	focusCamera( splineCamera, path1 );
	
	//cdAudioCycle(6);
	say( 0, 1, "scene4.wav" );

	$WAV1 = "cin_hd_01.wav";
	schedule( "Say( 0, 2, $WAV1 );", 4.5 );

        newFormation( XDelta, 0,0,0,  30,-30,0,  -30,-30,0,  30,-90,0 );
	newFormation( Zeta, 0,0,0, -40,0,0,  40,-20,0,  -20,-20,0 );

       	// Alliance 1
        order( "MissionGroup\\AllianceGroup1\\Herc1", MakeLeader, true );
        order( "MissionGroup\\AllianceGroup1\\Herc1", Speed, Low );
	order( "MissionGroup\\AllianceGroup1", Formation, XDelta );

	// Alliance 2
	order( "MissionGroup\\AllianceGroup2\\Herc1", MakeLeader, true );
        order( "MissionGroup\\AllianceGroup2\\Herc1", Speed, Low );
	//order( "MissionGroup\\AllianceGroup2", Formation, XDelta );
	//order( "MissionGroup\\Escape\\Flyer1", Speed, High);
	order( "MissionGroup\\escape\\Flyer1", Height, 5000, 7500);
	order( "MissionGroup\\escape\\Flyer1", Guard, "MissionGroup\\EscapePath1" );
	       	
}
function path1::camera::waypoint3( %this )

{
	playAnimSequence( "MissionGroup\\DiasIrae\\hTitanBeam1", 0, 1 );
	order( "MissionGroup\\AllianceGroup1\\Herc1", Guard, "MissionGroup\\AllyPath1" );
}

function path1::camera::waypoint5( %this )
{
	fadeEvent( 0, out, 1.0, 0, 0.0, 0.0 );

	schedule( "focusCamera( splineCamera, path2 );", 1.5 );
}

function path2::camera::waypoint1( %this )
{

	playAnimSequence( "MissionGroup\\cryo\\doors1", 0.5, 1 );
	
	$DOORS2 = "MissionGroup\\cryo\\doors2";
	schedule( "playAnimSequence( $DOORS2, 0, 1 );", 2.0);
	fadeEvent( 0, in, 1.0, 0, 0.0, 0.0 );
	$WAV2 = "cin_hd_02.wav";
	schedule( "Say( 0, 2, $WAV2 );", 2.0 );

}
function path2::camera::waypoint2( %this )
{

	$DOORS3 = "MissionGroup\\cryo\\doors3";
	schedule( "playAnimSequence( $DOORS3, 0, 1 );", 2.0);

}

function path2::camera::waypoint4( %this )
{
	schedule( "fadeEvent( 0, out, 1.0, 0, 0.0, 0.0 );", 2.5 );
	schedule( "focusCamera( splineCamera, path3 );", 4.0 );
}
function path3::camera::waypoint1( %this )
{
	
	fadeEvent( 0, in, 1.0, 0, 0.0, 0.0 );
	order( "MissionGroup\\AllianceGroup2", Guard, "MissionGroup\\AllyPath2" );	
	$WAV3 = "cin_hd_03.wav";
	schedule( "Say( 0, 2, $WAV3 );", 1.0 );

}
function path3::camera::waypoint3( %this )
{

	schedule( "fadeEvent( 0, out, 0.5, 0, 0, 0 );", 0.5 );

	schedule( "focusCamera( splineCamera, path4 );", 1.1 );

}
function path4::camera::waypoint1( %this )
{
	
	fadeEvent( 0, in, 0.5, 0, 0, 0 );

	$FLYER1 = "MissionGroup\\flyers\\flyer1";
	$FLYER2 = "MissionGroup\\flyers\\flyer2";
	schedule( "playAnimSequence( $FLYER1, 0, 1 );", 2.5 );
	schedule( "playAnimSequence( $FLYER2, 0, 1 );", 3.0 );

}
function path4::camera::waypoint2( %this )
{

	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 0.0 );
	schedule( "cdAudioFadeStop();", 2.0 );

}