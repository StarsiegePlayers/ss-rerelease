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
   preLoadFile("cin_he2.ted#6.dtb");
   preLoadFile("cin_he2.ted#7.dtb");
   preLoadFile("cin_he2.ted#11.dtb");
}

function onCinematicStart()
{
   setWidescreen(true);
	//fadeEvent( 0, out, 0.0, 0, 0, 0 );
	//schedule("fadeEvent( 0, in, 1.0, 0, 0, 0 );", 3.0);

	focusCamera( splineCamera, path1 );
	
	say( 0, 1, "Cin_he2.wav" );
	//cdAudioCycle(12);
	
	$S1 = "HE2_Caanon_CN1.WAV";
	schedule( "say( 0, 2,$S1);", 4.0 );

	schedule( "fadeEvent( 0, out, 0.25, 0, 0, 0 );", 7.0 );

	schedule( "focusCamera( splineCamera, path2 );", 7.5 );

}
function path2::camera::waypoint1()
{
	
	fadeEvent( 0, in, 0.5, 0, 0, 0 );
	
	$HAR = "MissionGroup\\Harabec\\herc1";
	order( "MissionGroup\\Harabec\\herc1", Guard, "MissionGroup\\haPath1" );
		
	$S2 = "HE3_Harabec_CN01.WAV";
	schedule( "say( 0, 3, $S2);", 1.0 );
	
	schedule( "setFlybyCamera($HAR, cameraOffset -8, 60, 0);", 7.5 );
	schedule( "setTowerCamera($HAR, cameraPosition -800, -50, 1150);", 14.0 );
	
	$S3 = "HE2_Caanon_CN2.WAV";
	schedule( "say( 0, 2, $S3);", 5.0 );

	$S4 = "HE3_Harabec_CN02.WAV";
	schedule( "say( 0, 3, $S4);", 8.5 );

	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 18.5 );	
	schedule( "focusCamera( splineCamera, path3 );", 20.0 );

}
function path3::camera::waypoint1()
{
	
	fadeEvent( 0, in, 1.0, 0, 0, 0 );

	say( 0, 3,"HE3_Harabec_CN03.WAV");
	
	order( "MissionGroup\\cyGroup1", Guard, "MissionGroup\\cyPath1" );
	order( "MissionGroup\\cyGroup1", Attack, $HAR );

	schedule( "playAnimSequence( 8214, 0.0, 1.0 );", 0.25 );
	schedule( "playAnimSequence( 8215, 0.0, 1.0 );", 1.0 );
	schedule( "playAnimSequence( 8216, 0.0, 1.0 );", 2.25 );
	schedule( "playAnimSequence( 8217, 0.0, 1.0 );", 3.0 );
	schedule( "playAnimSequence( 8218, 0.0, 1.0 );", 4.0 );
	schedule( "playAnimSequence( 8219, 0.0, 1.0 );", 5.0 );

}
function path3::camera::waypoint3()
{

	$S6 = "HE2_Caanon_CN3.WAV";
	schedule( "say( 0, 2, $S6);", 0.0 );
	$CYHERC = "MissionGroup\\cyGroup1\\Herc8";
	schedule( "setTowerCamera($CYHERC, cameraPosition -2350, -820, 820);", 0.0 );
	schedule( "focusCamera( splineCamera, path4 );", 4.0 );

}
function path4::camera::waypoint1()
{

	say( 0, 2,"HE3_Harabec_CN04.WAV");
	schedule( "focusCamera( splineCamera, path5 );", 5.0 );

}
function path5::camera::waypoint1()
{
	
	$S8 ="HE3_Harabec_CN05.WAV";
	schedule( "say( 0, 2, $S8);", 1.0 );
	order( $HAR, Attack, "MissionGroup\\cyGroup1" );
	
	schedule( "setFlybyCamera($HAR,cameraOffset -10, 80, -6);", 3.0 );	
	$S9 = "HE2_Caanon_CN4.WAV";
	schedule( "say( 0, 2, $S9);", 3.0 );
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 14.0 );
	schedule( "cdAudioFadeStop();", 14.0 );

}