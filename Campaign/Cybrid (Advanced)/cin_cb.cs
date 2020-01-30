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
   preLoadFile("mercury#6.dtb");
   preLoadFile("mercury#7.dtb");
   preLoadFile("mercury#9.dtb");
   preLoadFile("mercury#13.dtb");
   preLoadFile("mercury#14.dtb");
   preLoadFile("mercury#5.dtb");
   preLoadFile("mercury#15.dtb");
   preLoadFile("mercury#11.dtb");
}

function onCinematicStart()
{
   setWidescreen(true);

	focusCamera( splineCamera, path1 );
	
	say( 0, 1, "CB.wav" );
	//cdAudioCycle(14);
	
	$CB01 = "cin_cb_01.wav";

	schedule( "say( 0, 2, $CB01);", 5.0 );
       	
	$TComm = "MissionGroup\\impBase1\\hTroopcomm1";
	$TLis = "MissionGroup\\impBase1\\hTrooplisten1";

	schedule( "damageObject ( $TComm, 4000);", 0.0 );
	schedule( "damageObject ( $TLis, 4000);", 7.0 );
	
	damageObject ( "MissionGroup\\impBase1\\hTroopgarage1", 4000);
	damageObject ( "MissionGroup\\impBase1\\hTroopbigdish1", 4000);
	damageObject ( "MissionGroup\\impBase1\\hTroopbarracks1", 4000);
	
	$THerc1 =  "MissionGroup\\impGroup1\\Herc1";
	$THerc2 =  "MissionGroup\\impGroup1\\Herc2";
	$CY1 = "MissionGroup\\cyGroup1";

	schedule( "damageObject ( $THerc1, 15000);", 15.0 );
	schedule( "damageObject ( $THerc2, 25000);", 9.0 );
	schedule( "order(  $CY1, Attack, $THerc2);", 3.0 );

	$THerc1 =  "MissionGroup\\impGroup1\\Herc1";
	//schedule( "setTeam(  $THerc1, *IDSTR_TEAM_RED);", 10.0 );
	schedule( "order(  $CY1, Attack, $THerc1);", 12.0 );

	order(  "MissionGroup\\impGroup1\\Herc2", Speed, Medium );
	order(  "MissionGroup\\cyGroup1\\Herc1", Speed, Medium );
	order(  "MissionGroup\\cyGroup1\\Herc3", Speed, Medium );
	order(  "MissionGroup\\impGroup1\\Herc2", Guard, "MissionGroup\\impPath2" );
	order(  "MissionGroup\\cyGroup1\\Herc1", Guard, "MissionGroup\\impPath2" );
	order(  "MissionGroup\\cyGroup1\\Herc3", Guard, "MissionGroup\\impPath2" );
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 11.0 );
	//schedule( "focusCamera( splineCamera, path2 );", 12.0 );


	$tpath1 = "MissionGroup\\impPath1";
	schedule( "setFlybyCamera($THerc1, cameraOffset -16, 40, 0);", 11.75 );
	schedule( "fadeEvent( 0, in, 0.5, 0, 0, 0 );", 12.5 );
			
	schedule( "order( $THerc1, Guard, $tpath1 );", 11.0 );
	schedule( "fadeEvent( 0, out, 0.5, 0, 0, 0 );", 17.0 );
	schedule( "focusCamera( splineCamera, path3 );", 17.6 );

}
function path2::camera::waypoint1( %this )
{
	
	fadeEvent( 0, in, 0.5, 0, 0, 0 );
	damageObject ( "MissionGroup\\impBase1\\Herc1", 7600);
		
	order(  "MissionGroup\\impGroup1\\Herc1", Guard, "MissionGroup\\impPath1" );
	schedule( "fadeEvent( 0, out, 0.5, 0, 0, 0 );", 4.0 );

	schedule( "focusCamera( splineCamera, path3 );", 4.6 );

}
function path3::camera::waypoint1( %this )
{
		
	say( 0, 3,"cin_cb_02.wav");

	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
	$EXP2 = "MissionGroup\\rocks\\exp2";
	schedule( "damageObject ( $EXP2, 4000);", 2.5 );

}	
function path3::camera::waypoint3( %this )
{
	
	say( 0, 2,"cin_cb_03.wav");

}
function path3::camera::waypoint4( %this )
{
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 4.0 );

	schedule( "focusCamera( splineCamera, path4 );", 5.1 );

}
function path4::camera::waypoint1( %this )
{
	
	
	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 6.0 );
	schedule( "focusCamera( splineCamera, path5 );", 7.1 );

}
function path5::camera::waypoint1( %this )
{
	
	$CB04 = "cin_cb_04.wav";

	schedule( "say( 0, 2, $CB04);", 1.0 );

	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 10.0 );
	schedule( "focusCamera( splineCamera, path6 );", 11.1 );

}
function path6::camera::waypoint1( %this )
{
		
	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 16.0 );
	schedule( "cdAudioFadeStop();", 16.0 );
	
}