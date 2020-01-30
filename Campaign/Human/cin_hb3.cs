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
   preLoadFile("cin_hb3.ted#9.dtb");
   preLoadFile("cin_hb3.ted#13.dtb");
   preLoadFile("cin_hb3.ted#14.dtb");
   preLoadFile("cin_hb3.ted#15.dtb");
}

function onCinematicStart()
{
   setWidescreen(true);
	//fadeEvent( 0, out, 0.0, 0, 0, 0 );
	//schedule("fadeEvent( 0, in, 1.0, 0, 0, 0 );", 2.0);	

	setAnimSequence( "MissionGroup\\dropship\\dropship1", 0.0, 0.4);

	focusCamera( splineCamera, path1 );
	
	say( 0, 1, "cin_hb3.wav" );
	//cdAudioCycle(10);
		
        newFormation( XDelta, 0,0,0,  -20,-20,0,  40,-0,0,  -40,-40,0 );
	newFormation( XZeta, 0,0,0, -40,0,0,  20,-20,0,  -60,-20,0 );

        // Caanon near landers
	order( "MissionGroup\\knightGroup1\\Caanon", MakeLeader, true );
        order( "MissionGroup\\knightGroup1\\Caanon", Speed, Low );
	order( "MissionGroup\\knightGroup1", Formation, Delta );
	
	// Knights
        order( "MissionGroup\\knightGroup2\\knight1", MakeLeader, true );
        order( "MissionGroup\\knightGroup2\\knight1", Speed, Medium );
	order( "MissionGroup\\knightGroup2", Formation, XDelta );

	order( "MissionGroup\\knightGroup3\\knight1", MakeLeader, true );
        order( "MissionGroup\\knightGroup3\\knight1", Speed, Medium );
	order( "MissionGroup\\knightGroup3", Formation, XDelta );
       	
	order( "MissionGroup\\RebelGroup1\\Harabec", MakeLeader, true );
        order( "MissionGroup\\RebelGroup1\\Harabec", Speed, Low );
	order( "MissionGroup\\RebelGroup1", Formation, XZeta );

	order( "MissionGroup\\RebelGroup2\\rebel1", MakeLeader, true );
        order( "MissionGroup\\RebelGroup2\\rebel1", Speed, Low );
	order( "MissionGroup\\RebelGroup2", Formation, XZeta );

	order( "MissionGroup\\knightGroup1\\Caanon", Guard, "MissionGroup\\knightPath1" );
	order( "MissionGroup\\knightGroup2\\knight1", Guard, "MissionGroup\\knightPath2" );
	order( "MissionGroup\\knightGroup3\\knight1", Guard, "MissionGroup\\knightPath4" );
	
	order( "MissionGroup\\RebelGroup1\\Harabec", Speed, Low);
	order( "MissionGroup\\RebelGroup1\\Harabec", Guard, "MissionGroup\\rebelPath1" );	
	order( "MissionGroup\\RebelGroup2\\rebel1", Guard, "MissionGroup\\rebelPath2" );

	order( "MissionGroup\\RebelGroup3", Attack, "MissionGroup\\Scenario\\PoliceBase");
	
	order( "MissionGroup\\RebelGroup2\\rebel1", Attack, "MissionGroup\\knightGroup3\\knight1" );
	order( "MissionGroup\\RebelGroup2\\Drea1", Attack, "MissionGroup\\knightGroup3\\knight1" );
	//order( "MissionGroup\\RebelGroup1\\Harabec", Attack, "MissionGroup\\knightGroup3\\knight1" );
	
	$S1 = "CIN_PILOT01.WAV";
	schedule( "say( 0, 2, $S1);", 0.5 );
	
	//setTeam( "MissionGroup\\knightGroup3\\knight1", *IDSTR_TEAM_RED );

	$THerc1 = "MissionGroup\\knightGroup3\\knight1";
	
	schedule( "setFlybyCamera($THerc1, cameraOffset -10, 40, -2);", 6.5 );
	
	schedule( "damageObject( $THerc1, 15000);", 8.25 );

	$CATALK = "MissionGroup\\CaTalk\\CaTalk";
	schedule("playAnimSequence( $CATALK, 0, 1 );", 9.0);

	$S2 = "HB2_Caanon_CN1.wav";
	schedule("say( 0, 3,$S2);", 9.0);

	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 9.5 );

	schedule( "focusCamera( splineCamera, path2 );", 10.6 );

	damageObject ( "MissionGroup\\Scenario\\PoliceBase\\hUplink1", 1000);

}
function path2::camera::waypoint1( %this )
{
	
	fadeEvent( 0, in, 0.5, 0, 0, 0 );
	
	schedule( "focusCamera( splineCamera, path3 );", 3.0 );
	
	order( "MissionGroup\\knightGroup2\\knight1", Speed, Medium );
	order( "MissionGroup\\knightGroup2\\knight1", Guard, "MissionGroup\\knightPath3" );
	order( "MissionGroup\\knightGroup1\\Caanon", Guard, "MissionGroup\\knightPath1" );			
	
}
function path3::camera::waypoint1( %this )
{
	//fadeEvent( 0, in, 0.25, 0, 0, 0 );

	say( 0, 2,"CIN_PILOT02.WAV");

	damageObject ( "MissionGroup\\Scenario\\PoliceBase\\hPolicefin1", 1000);

	schedule( "focusCamera( splineCamera, path4 );", 3.0 );
	
}
function path4::camera::waypoint1( %this )
{
	$S4 = "HB2_Caanon_CN2.wav";
	schedule( "say( 0, 3, $S4);", 0.0 );
	schedule( "focusCamera( splineCamera, path5 );", 5.0 );
	
	$S5 = "CIN_PILOT03.WAV";
	schedule( "say( 0, 2, $S5);", 4.0 );
		
}
function path5::camera::waypoint1( %this )
{
	
	playAnimSequence( "MissionGroup\\dropship\\dropship1", 0.0, 1.0 );
		
	order( "MissionGroup\\knightGroup2\\knight1", Speed, High );
	$knight1 = "MissionGroup\\knightGroup2\\knight1";
	$kpath2 = "MissionGroup\\knightPath2";

	schedule( "order( $knight1, Guard, $kpath2 );", 3.0 );
	order( "MissionGroup\\RebelGroup1\\Harabec", Guard, "MissionGroup\\rebelPath1" );	
	order( "MissionGroup\\RebelGroup2\\rebel1", Guard, "MissionGroup\\rebelPath2" );
	
	schedule( "focusCamera( splineCamera, path6 );", 5.0 );
		
}
function path6::camera::waypoint1( %this )
{
	
	say( 0, 2,"HB2_Harabec_CN01.wav");
	schedule( "focusCamera( splineCamera, path7 );", 3.0 );

}
function path7::camera::waypoint1( %this )
{
	
	$can = "MissionGroup\\knightGroup1\\Caanon";

	setAnimSequence( "MissionGroup\\dropship\\dropship1", 0.0, 1.0);
	
	schedule( "focusCamera( splineCamera, path8 );", 6.0 );

}
function path7::camera::waypoint2( %this )
{
	
	
	schedule( "fadeEvent( 0, out, 2.0, 0, 0, 0 );", 8.0 );	
	
}
function path8::camera::waypoint1( %this )
{
	
	say( 0, 3,"HB2_Caanon_CN4.wav");	

	$S9 = "HB2_Harabec_CN03.wav";	
	schedule( "say( 0, 2,$S9 );", 5.0 );
	
	$can = "MissionGroup\\knightGroup1\\Caanon";
	schedule( "order( $can, shutDown, true);", 0.0 );
		
	order( "MissionGroup\\knightGroup2\\knight1", shutDown, true);
	order( "MissionGroup\\knightGroup2\\knight2", shutDown, true);	
	order( "MissionGroup\\knightGroup2\\knight3", shutDown, true);

	schedule( "fadeEvent( 0, out, 2.0, 0, 0, 0 );", 10.0 );
	schedule( "cdAudioFadeStop();", 12.0 );
}