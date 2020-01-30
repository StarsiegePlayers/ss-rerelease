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
   preLoadFile("cin_cd.ted#5.dtb");
   preLoadFile("cin_cd.ted#6.dtb");
   preLoadFile("cin_cd.ted#7.dtb");
   preLoadFile("cin_cd.ted#10.dtb");
   preLoadFile("cin_cd.ted#11.dtb");
   preLoadFile("cin_cd.ted#14.dtb");
   preLoadFile("cin_cd.ted#15.dtb");
   preLoadFile("cin_cd.ted#13.dtb");
   preLoadFile("cin_cd.ted#9.dtb");
}

function onCinematicStart()
{
   setWidescreen(true);
	//fadeEvent( 0, out, 0.0, 0, 0, 0 );
	//schedule("fadeEvent( 0, in, 1.0, 0, 0, 0 );", 2.0);
	focusCamera( splineCamera, path1 );

	setStarsVisibility(false);

	say( 0, 1, "CD.wav" );

	//cdAudioCycle(12);
	
	$CD01 = "cin_Cd_01.wav";

	schedule( "say( 0, 2, $CD01);", 2.0 );

        newFormation( xDelta, 0,0,0,  20,-20,0,  -20,-20,0,  -40,-40,0 );
	newFormation( xZeta, 0,0,0, -40,0,0,  40,-20,0,  -20,-20,0 );

	// Trooper 1
        order( "MissionGroup\\trooperGroup1\\Herc1", MakeLeader, true );
        order( "MissionGroup\\trooperGroup1\\Herc1", Speed, Low );
	order( "MissionGroup\\trooperGroup1", Formation, xDelta );


       	// Cybrid 1
        //order( "MissionGroup\\cyGroup1\\Herc1", MakeLeader, true );
        //order( "MissionGroup\\cyGroup1\\Herc1", Speed, Low );
	//order( "MissionGroup\\cyGroup1", Formation, xDelta );

 	order( "MissionGroup\\cyGroup1\\Herc1", Speed, Low );
	order( "MissionGroup\\cyGroup1\\Herc2", Speed, Low );
	order( "MissionGroup\\cyGroup1\\Herc3", Speed, Low );
	order( "MissionGroup\\cyGroup1\\Herc1", Guard, "MissionGroup\\cyPath1" );
	order( "MissionGroup\\cyGroup1\\Herc2", Guard, "MissionGroup\\cyPath2" );
	order( "MissionGroup\\cyGroup1\\Herc3", Guard, "MissionGroup\\cyPath3" );
	order( "MissionGroup\\cyGroup2\\Flyer1", ShutDown );	
	order( "MissionGroup\\cyGroup2\\Flyer2", ShutDown );	

       	
}
function path1::camera::waypoint2( %this )
{
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 1.0 );

	schedule( "focusCamera( splineCamera, path2 );", 2.1 );

}
function path2::camera::waypoint1( %this )
{
		
	fadeEvent( 0, in, 1.0, 0, 0, 0 );

}
function path2::camera::waypoint2( %this )
{
		
	$CD02 = "cin_Cd_02.wav";
	schedule( "say( 0, 3, $CD02 );", 1.0 );	
		
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 5.0 );

	schedule( "focusCamera( splineCamera, path3 );", 6.1 );

}
function path3::camera::waypoint1( %this )
{
	
	$CD03 = "cin_Cd_03.wav";
	schedule( "say( 0, 2, $CD03 );", 2.0 );	

	setStarsVisibility(true);
	setSkyMaterialListTag(0);

	schedule( "fadeEvent( 0, in, 1.0, 0, 0, 0 );", 1.0 );
}	
function path3::camera::waypoint3( %this )
{
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 1.0 );
	
	schedule( "focusCamera( splineCamera, path4 );", 2.5 );

}
function path4::camera::waypoint1( %this )
{

	fadeEvent( 0, in, 1.0, 0, 0, 0 );
		
	$CD03b = "cin_Cd_04.wav";
	schedule( "say( 0, 3,$CD03b );", 5.0 );	

}
function path4::camera::waypoint2( %this )
{

	order( "MissionGroup\\impGroup1\\Herc1", Attack, "MissionGroup\\cyGroup1\\Herc1" );

	order( "MissionGroup\\cyGroup1\\Herc1", MakeLeader, true );
        order( "MissionGroup\\cyGroup1\\Herc1", Speed, High );
	order( "MissionGroup\\cyGroup1", Formation, xDelta );

	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 0.0 );

	schedule( "focusCamera( splineCamera, path5 );", 1.1 );
	
	order( "MissionGroup\\cyGroup2", Guard, "MissionGroup\\cyPath4" );	
	order( "MissionGroup\\cyGroup2", Height, 50, 100);
	
}
function path5::camera::waypoint1( %this )
{
	setSkyMaterialListTag(IDDML_SKY_TEMPERATE_DAY);
	setStarsVisibility(false);
	order( "MissionGroup\\impFlyers1", Guard, "MissionGroup\\cyPath4" );

	schedule( "fadeEvent( 0, in, 1.0, 0, 0, 0 );", 1.0 );
	
	$CY1 = "MissionGroup\\cyGroup1\\Herc1";
	$TROOP1 = "MissionGroup\\impBase";

	setTowerCamera($CY1, cameraPosition -3240, 650, 690);

	say( 0, 2, "cin_Cd_05.wav" );	
		
	order( $CY1, Attack, $TROOP1 );
	
	schedule( "focusCamera( splineCamera, path6 );", 12.0 );

		

}
function path6::camera::waypoint1( %this )
{

	schedule( "fadeEvent( 0, out, 2.0, 0, 0, 0 );", 12.0 );
	schedule( "cdAudioFadeStop();", 14.0 );

}