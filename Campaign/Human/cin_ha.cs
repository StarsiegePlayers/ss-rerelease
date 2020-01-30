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
   preLoadFile("cin_ha.ted#2.dtb");
   preLoadFile("cin_ha.ted#5.dtb");
   preLoadFile("cin_ha.ted#6.dtb");
   preLoadFile("cin_ha.ted#7.dtb");
   preLoadFile("cin_ha.ted#9.dtb");
   preLoadFile("cin_ha.ted#10.dtb");
   preLoadFile("cin_ha.ted#11.dtb");
   preLoadFile("cin_ha.ted#13.dtb");
   preLoadFile("cin_ha.ted#14.dtb");
   preLoadFile("cin_ha.ted#15.dtb");
}

function onCinematicStart()
{
	//cdAudioCycle(6);
   setWidescreen(true);
	say( 0, 1, "scene1.wav" );
	newFormation( Delta, 0,0,0,  0,-30,0,  0,-60,0,  0,-90,0 );

	order( "MissionGroup\\rebelGroup1\\rebel1", MakeLeader, True );
        order( "MissionGroup\\rebelGroup1\\rebel1", Speed, High );
        order( "MissionGroup\\rebelGroup1\\rebel1", Formation, Delta );

	//order( "MissionGroup\\rebelGroup2\\rebel1", MakeLeader, True );
        //order( "MissionGroup\\rebelGroup2\\rebel1", Speed, Low );
        //order( "MissionGroup\\rebelGroup2\\rebel1", Formation, Delta );

	//order( "MissionGroup\\rebelGroup3\\rebel1", MakeLeader, True );
        order( "MissionGroup\\rebelGroup3", Speed, High );
        //order( "MissionGroup\\rebelGroup3\\rebel1", Formation, Delta );

	order( "MissionGroup\\rebelGroup4\\rebel1", MakeLeader, True );
        order( "MissionGroup\\rebelGroup4\\rebel1", Speed, High );
        order( "MissionGroup\\rebelGroup4\\rebel1", Formation, Delta );

	if( $AllowHercsToMove == True )
	{
		order( "MissionGroup\\rebelGroup1\\rebel1", Guard, "MissionGroup\\rebelPath1" );	
	}

	focusCamera( splineCamera, path1 );
}

function path1::camera::waypoint2()
{
        order( "MissionGroup\\rebelGroup1\\rebel1", Speed, Medium );
}
function path1::camera::waypoint3()
{
	
}
function path1::camera::waypoint4()
{
	say( 0, 2, "cin_ha_01.wav" );
	if( $AllowHercsToMove == True )
	{
		order( "MissionGroup\\rebelGroup2", Guard, "MissionGroup\\rebelPath2" );	
	}
	
}
function path1::camera::waypoint6()
{
	if( $AllowHercsToMove == True )
	{
		order( "MissionGroup\\rebelGroup4", Guard, "MissionGroup\\rebelPath4" );
		order( "MissionGroup\\rebelGroup3", Guard, "MissionGroup\\rebelPath3" );
	}
}
function path1::camera::waypoint9()
{	
	say( 0, 2,"cin_ha_02.wav");
}
function path1::camera::waypoint10()
{
	schedule( "fadeEvent( 0, out, 1.0, 0, 0, 0 );", 0.35 );
	schedule( "focusCamera( splineCamera, path2 );", 1.75 );
}
function path2::camera::waypoint1()
{
	fadeEvent( 0, in, 1.0, 0, 0, 0 );
}
function path2::camera::waypoint2()
{
	fadeEvent( 0, out, 1, 0, 1.0, 0 );
	schedule( "focusCamera( splineCamera, path3 );", 1.5 );

	say( 0, 2, "cin_ha_03.wav");
}
function path3::camera::waypoint1()
{
	fadeEvent( 0, in, 1, 0, 1.0, 0 );
}
function path3::camera::waypoint2()
{
	$REB1 =  "MissionGroup\\rebelGroup5\\rebel1";
	$REB2 =  "MissionGroup\\rebelGroup5\\rebel2";
	$REB3 =  "MissionGroup\\rebelGroup5\\Tank1";
	$RPATH5 = "MissionGroup\\rebelPath5";
	$RPATH6 = "MissionGroup\\rebelPath6";
	$RPATH7 = "MissionGroup\\rebelPath7";
	order( "MissionGroup\\rebelGroup5", Speed, Low);
	schedule( "order($REB1, Guard,$RPATH6);", 2.0 );
	schedule( "order($REB3, Guard,$RPATH7);", 1.0 );
	schedule( "order($REB2, Guard,$RPATH5);", 3.0 );
	schedule( "fadeEvent( 0, out, 2.0, 0, 0, 0 );", 8.0 );
	schedule( "cdAudioFadeStop();", 10.0 );
}