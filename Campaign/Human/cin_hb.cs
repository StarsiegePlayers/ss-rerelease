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
   preLoadMaterialList(*IDDML_SKY_MARS_DAY);
   preLoadFile("cin_hb.ted#9.dtb");
   preLoadFile("cin_hb.ted#10.dtb");
   preLoadFile("cin_hb.ted#11.dtb");
}

function onCinematicStart()
{
   setWidescreen(true);
	setStarsVisibility(true);
	setSkyMaterialListTag(0);	

	focusCamera( splineCamera, path1 );
	
	//cdAudioCycle(4);
	say( 0, 1, "scene2.wav" );
	
	$CIN01 = "cin_hb_01.wav";
	schedule( "say( 0, 2, $CIN01 );",0.0 );

        newFormation( Delta, 0,0,0,  -30,-30,0,  30,-30,0,  -60,-60,0 );
	newFormation( Zeta, 0,0,0, -40,0,0,  40,-20,0,  -20,-20,0 );

        // Caanon near landers
	order( "MissionGroup\\knightGroup1\\knight1", MakeLeader, true );
        order( "MissionGroup\\knightGroup1\\knight1", Speed, Low );
	order( "MissionGroup\\knightGroup1", Formation, Delta );
	
	// Caanon attacking rebels
        order( "MissionGroup\\knightGroup2\\Herc1", MakeLeader, true );
        order( "MissionGroup\\knightGroup2\\Herc1", Speed, Low );
	order( "MissionGroup\\knightGroup2", Formation, Delta );
       	
	order( "MissionGroup\\rebelGroup1\\rebel1", MakeLeader, true );
        order( "MissionGroup\\rebelGroup1\\rebel1", Speed, Medium);
	order( "MissionGroup\\rebelGroup1", Formation, Delta );

	order( "MissionGroup\\rebelGroup2\\rebel1", MakeLeader, true );
        order( "MissionGroup\\rebelGroup2\\rebel1", Speed, High );
	order( "MissionGroup\\rebelGroup2", Formation, Zeta );
}
function path1::camera::waypoint2( %this )
{
	schedule( "fadeEvent( 0, out, 2.0, 0, 0, 0 );",10.5 );

	schedule( "focusCamera( splineCamera, path2 );", 13.5 );

	schedule( "playAnimSequence( 8283, 0.0, 1.0 );", 9.0 );
	schedule( "playAnimSequence( 8284, 0.0, 1.0 );", 13.0 );
	schedule( "playAnimSequence( 8285, 0.0, 1.0 );", 18.0 );
	schedule( "playAnimSequence( 8286, 0.0, 1.0 );", 16.0 );
	schedule( "playAnimSequence( 8407, 0.0, 1.0 );", 15.0 );
		
}
function path2::camera::waypoint1( %this )
{
	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	setStarsVisibility(false);
	setSkyMaterialListTag(IDDML_SKY_MARS_DAY);			
}
function path2::camera::waypoint2( %this )
{
	order( "MissionGroup\\knightGroup1\\knight1", Guard, "MissionGroup\\knightPath1" );
	
	say( 0, 2, "cin_hb_02.wav" );
	
	fadeEvent( 0, out, 0.5, 0, 0, 0 );
	schedule( "focusCamera( splineCamera, path2b );", 1.1 );
}
function path2b::camera::waypoint1( %this )
{
        fadeEvent( 0, in, 0.5, 0, 0, 0 );
	
	schedule( "fadeEvent( 0, out, 0.5, 0, 0, 0 );", 3.5 );
	//schedule( "focusCamera( splineCamera, path3 );", 4.1 );
	
	$knight1 = "MissionGroup\\knightGroup1\\knight1";
	//schedule( "setFlybyCamera($knight1, cameraOffset 6, 60, 0);", 4.25 );
	schedule( "setTowerCamera($knight1, cameraPosition -285, 1640, 57.5);", 4.25 );
	schedule( "fadeEvent( 0, in, 0.5, 0, 0, 0 );", 4.5 );

	$CIN03 = "cin_hb_03.wav";
	schedule( "say( 0, 2, $CIN03 );",5.0 );

	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 15.0 );
	schedule( "focusCamera( splineCamera, path4 );", 16.1 );
}
function path3::camera::waypoint1( %this )
{
        fadeEvent( 0, in, 1.0, 0, 0, 0 );
}
function path3::camera::waypoint2( %this )
{
	fadeEvent( 0, out, 1.0, 0, 0, 0 );
	schedule( "focusCamera( splineCamera, path4 );", 1.1 );
}
function path4::camera::waypoint1( %this )
{
	fadeEvent( 0, in, 1.0, 0, 0, 0 );

	$CINS1 = "CIN_REBEL01.WAV";
	schedule( "say( 0, 2, $CINS1 );",0.0 );
	$CINS2 = "CIN_REBEL02.WAV";
	schedule( "say( 0, 2, $CINS2 );",2.0 );

	order( "MissionGroup\\rebelGroup2", Guard, "MissionGroup\\rebelPath2" );
	order( "MissionGroup\\knightGroup2\\Herc1", Guard, "MissionGroup\\knightPath2" );

	order( "MissionGroup\\rebelGroup1\\rebel1", Guard, "MissionGroup\\rebelPath1" );
	
	//schedule( "setTeam( \"MissionGroup/rebelGroup1/rebel1\", *IDSTR_TEAM_RED );", 0.0 );
	//setTeam( "MissionGroup\\rebelGroup1\\rebel1", *IDSTR_TEAM_RED );

	$IMP2 = "MissionGroup\\knightGroup2\\Herc1";
	$REBEL1 = "MissionGroup\\rebelGroup1\\rebel1";
	schedule( "damageObject ( $REBEL1, 4500);",2.0 );
	schedule( "order( $IMP2, Attack, $REBEL1 );",0.0 );
	
	//$Knight2 = "MissionGroup\\knightGroup2\\Herc1";
	//$Rebel1 = "MissionGroup\\rebelGroup1\\rebel1";
	//schedule( "order($Knight, ATTACK, $Rebel1);", 1.0 );

}
function rebelLeader::vehicle::onDestroyed( %this, %attacker )
{
	order( "MissionGroup\\knightGroup2\\Herc1", Guard, "MissionGroup\\knightPath2" );
       	order( "MissionGroup\\knightGroup2\\Herc1", MakeLeader, true );
        order( "MissionGroup\\knightGroup2\\Herc1", Speed, Low );
	order( "MissionGroup\\knightGroup2", Formation, Delta );
	order( "MissionGroup\\rebelGroup2", Speed, High );
	damageObject ( "MissionGroup\\rebelGroup2\\oly", 5000);
	damageObject ( "MissionGroup\\rebelGroup2\\rebe1", 4500);
	damageObject ( "MissionGroup\\rebelGroup2\\eman", 4500);
}	
function path4::camera::waypoint3( %this )
{
	schedule( "fadeEvent( 0, out, 0.5, 0, 0, 0 );", 2.0 );
	schedule( "focusCamera( splineCamera, path4c );", 2.6 );

}
function path4c::camera::waypoint1( %this )
{
	fadeEvent( 0, in, 0.5, 0, 0, 0 );

	say( 0, 2, "CAA_IMP_RET01.WAV" );
	playAnimSequence( "MissionGroup\\CaTalk\\CaTalk", 0, 1 );

	schedule( "fadeEvent( 0, out, 0.5, 0, 0, 0 );", 2.5 );
	schedule( "focusCamera( splineCamera, path5 );", 3.1 );
	

}
function path5::camera::waypoint1( %this )
{
	
	fadeEvent( 0, in, 0.5, 0, 0, 0 );

	$CIN04 = "cin_hb_04.wav";
	schedule( "say( 0, 2, $CIN04 );",3.0 );

	$HERC1 = "MissionGroup\\knightGroup2\\Herc1";
	$HERC2 = "MissionGroup\\knightGroup2\\Herc2";
	$HERC3 = "MissionGroup\\knightGroup2\\Herc3";

	$REBEL1 = "MissionGroup\\rebelGroup2";
	$REBEL2 = "MissionGroup\\rebelGroup2\\eman";
	$REBEL3 = "MissionGroup\\rebelGroup2\\oly";

	schedule( "order( $HERC1, Attack, $REBEL1 );",3.0 );
	schedule( "order( $HERC2, Attack, $REBEL2 );",4.0 );
	schedule( "order( $HERC3, Attack, $REBEL3 );",0.0 );
	//order( "MissionGroup\\knightGroup2\\Herc2", Attack, "MissionGroup\\rebelGroup2\\eman" );
	//order( "MissionGroup\\knightGroup2\\Herc3", Attack, "MissionGroup\\rebelGroup2\\oly" );
	
	//order( "MissionGroup\\knightGroup2\\Herc1", Attack, "MissionGroup\\rebelGroup2" );
	//order( "MissionGroup\\knightGroup2\\Herc2", Attack, "MissionGroup\\rebelGroup2\\eman" );
	//order( "MissionGroup\\knightGroup2\\Herc3", Attack, "MissionGroup\\rebelGroup2\\oly" );
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 12.5 );	
	schedule( "focusCamera( splineCamera, path6 );", 14.0 );

}
function path6::camera::waypoint1( %this )
{
	setStarsVisibility(true);
	setSkyMaterialListTag(0);

	$CIN05 = "cin_hb_05.wav";
	schedule( "say( 0, 2, $CIN05 );",3.0 );

	fadeEvent( 0, in, 1.5, 0, 0, 0 );
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 8.0 );
	schedule( "cdAudioFadeStop();", 9.0 );
}