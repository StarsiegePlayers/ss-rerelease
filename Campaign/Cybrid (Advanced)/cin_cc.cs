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
   preLoadFile("cin_cc.ted#14.dtb");
   preLoadFile("cin_cc.ted#15.dtb");
   preLoadFile("cin_cc.ted#5.dtb");
   preLoadFile("cin_cc.ted#6.dtb");
   preLoadFile("cin_cc.ted#9.dtb");
   preLoadFile("cin_cc.ted#10.dtb");
   preLoadFile("cin_cc.ted#11.dtb");
}

function onCinematicStart()
{
   setWidescreen(true);
	//fadeEvent( 0, out, 0.0, 0, 0, 0 );
	//schedule("fadeEvent( 0, in, 1.0, 0, 0, 0 );", 3.0);
	focusCamera( splineCamera, path1 );

	say( 0, 1, "CC.wav");

	//cdAudioCycle(6);
		
	$CC01 = "cin_cc_01.wav";
	schedule( "say( 0, 2, $CC01 );", 7.0);
	
	newFormation( xDelta, 0,0,0,  20,-20,0,  -20,-20,0,  -40,-40,0 );
	newFormation( xZeta, 0,0,0, -40,0,0,  40,-20,0,  -20,-20,0 );

	//damageObject ( "MissionGroup\\impBase1\\hMoonHQ1", 4000);
	//damageObject ( "MissionGroup\\impBase1\\hMoongather1", 4000);
	//damageObject ( "MissionGroup\\impBase1\\hMoonbarracks1", 4000);
	//damageObject ( "MissionGroup\\impBase1\\hMoonbigdish1", 4000);
	//damageObject ( "MissionGroup\\impBase1\\hMoonuplink1", 4000);

	// Trooper 1
        order( "MissionGroup\\trooperGroup1\\Herc1", MakeLeader, true );
        order( "MissionGroup\\trooperGroup1\\Herc1", Speed, High );
	order( "MissionGroup\\trooperGroup1", Formation, xDelta );


       	// Cybrid 1
        order( "MissionGroup\\cyGroup1\\Herc1", MakeLeader, true );
        order( "MissionGroup\\cyGroup1\\Herc1", Speed, Low );
	order( "MissionGroup\\cyGroup1", Formation, xDelta );

 	// Cybrid 2
        order( "MissionGroup\\cyGroup2\\Herc1", MakeLeader, true );
        order( "MissionGroup\\cyGroup2\\Herc1", Speed, Medium );
	order( "MissionGroup\\cyGroup2", Formation, xDelta );

	order( "MissionGroup\\trooperGroup1\\Herc1", Guard, "MissionGroup\\troopPath1" );
	order( "MissionGroup\\cyGroup1\\Herc1", Guard, "MissionGroup\\troopPath1" );
	order( "MissionGroup\\cyGroup2\\Herc1", Guard, "MissionGroup\\troopPath1" );
       	
	$CY1 = "MissionGroup\\cyGroup1\\Herc1";
	$CY2 = "MissionGroup\\cyGroup2\\Herc1";
	$TROOP1 = "MissionGroup\\trooperGroup1\\Herc3";
	$TROOP2 = "MissionGroup\\trooperGroup1\\Herc2";
	$TROOP3 = "MissionGroup\\trooperGroup1";

	schedule( "order( $CY1, Attack, $TROOP1 );", 2.0 );
	schedule( "order( $CY2, Attack, $TROOP2 );", 1.0 );

	schedule( "focusCamera( splineCamera, path2 );", 11.5 );

}
function path2::camera::waypoint1( %this )
{
	
	$CC02 = "cin_cc_02.wav";
	schedule( "say( 0, 3,$CC02);", 8.0);

	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 11.5 );	

	schedule( "focusCamera( splineCamera, path3 );", 12.75 );
	
}
function path3::camera::waypoint1( %this )
{

	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 6.5 );
	schedule( "focusCamera( splineCamera, path4 );", 8.0 );

}
function path4::camera::waypoint1( %this )
{

	say( 0, 2,"cin_cc_03.wav" );
	
	fadeEvent( 0, in, 0.5, 0, 0, 0 );
	
	schedule( "fadeEvent( 0, out, 1.5, 0, 0, 0 );", 8.0 );

	schedule( "focusCamera( splineCamera, path6 );", 10.0 );

}
function path6::camera::waypoint1( %this )
{

	say( 0, 3,"cin_cc_06.wav");
	
	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
}
function path6::camera::waypoint2( %this )
{
	
	schedule( "fadeEvent( 0, out, 2.0, 0.0, 0.0, 0.0 );", 23.0 );
	
	schedule( "cdAudioFadeStop();", 23.0 );

}