//------------------------------------------------------------------------------
//
// FILENAME:   TDM_TO_YOUR_KNEES
//
// Author:     Louie McCrady (Hercasaurus Rex/Tyranny)
// Layout:     Shamelessly stolen from Trevor Lanz (Sepsis)
//
// Description:
//    So, you think you have a hot computer?  You got that 450Mhz PII
//    with two VoodooII cards and you think your stylin'?  Good!  
//    Hear's a mission that will show you how good it can be.
//    
//    It's a team Death Match.  Cybrids VS Terran.  25-30 Hercs on a 
//    side.  When the dust settles, only one side will remain.
//    
//    If it's too much to handle, uncomment some of the lines that say
//    "deleteObject", to delete a few of the squads.
//    
//    
// Objective:
//    Survive.
//    
//------------------------------------------------------------------------------

$missionName = "TDM_TO_YOUR_KNEES";

exec("multiplayerStdLib.cs");
exec("DMstdLib.cs");

//-----------------------------------------------------------------------------

Pilot Reaper
{
   id = 28;
   
   skill = 1.0;
   accuracy = 1.0;
   aggressiveness = 0.5;
   activateDist = 2000.0;
   deactivateBuff = 3000.0;
   targetFreq = 4.0;
   trackFreq = 0.1;
   fireFreq = 0.1;
   LOSFreq = 0.4;
   name = "Reaper";
};

//-----------------------------------------------------------------------------

function setDefaultMissionOptions()
{
   $server::TeamPlay        = true;
   $server::AllowTeamPlay   = true;   
   $server::AllowDeathmatch = false;
   
   $server::AllowTeamYellow = true;
   $server::AllowTeamBlue   = false;
   $server::AllowTeamRed    = false;
   $server::AllowTeamPurple = true;

   $server::disableTeamYellow = false;
   $server::disableTeamBlue   = true;
   $server::disableTeamRed    = true;
   $server::disableTeamPurple = false;
}

//-----------------------------------------------------------------------------

function onMissionLoad()
{
   cdAudioCycle("SS1", "Newtech", "Yougot"); 

   $NTDF1 = getObjectId( "MissionGroup/Humans/NTDF1" );
   $NTDF2 = getObjectId( "MissionGroup/Humans/NTDF2" );
   $NTDF3 = getObjectId( "MissionGroup/Humans/NTDF3" );
   $NTDF4 = getObjectId( "MissionGroup/Humans/NTDF4" );
   $NTDF5 = getObjectId( "MissionGroup/Humans/NTDF5" );

   $CYBR1 = getObjectId( "MissionGroup/Cybrids/CYBR1" );
   $CYBR2 = getObjectId( "MissionGroup/Cybrids/CYBR2" );
   $CYBR3 = getObjectId( "MissionGroup/Cybrids/CYBR3" );
   $CYBR4 = getObjectId( "MissionGroup/Cybrids/CYBR4" );
   $CYBR5 = getObjectId( "MissionGroup/Cybrids/CYBR5" );
}

//-----------------------------------------------------------------------------

function onMissionStart()
{
//   deleteObject( $NTDF1 );
//   deleteObject( $NTDF2 );
//   deleteObject( $NTDF3 );
//   deleteObject( $NTDF4 );
//   deleteObject( $NTDF5 );
//   deleteObject( $CYBR1 );
//   deleteObject( $CYBR2 );
//   deleteObject( $CYBR3 );
//   deleteObject( $CYBR4 );
//   deleteObject( $CYBR5 );

   desertSounds();
   $AIKills = 0;
   $HumanKills = 0;

   // Make all pilots Reapers
   setPilotId("MissionGroup",28);

   echo( "onMissionStart -----------------------------------------------" );

   order( "MissionGroup/Humans/NTDF1/NTDF11", MakeLeader, true );
   order( "MissionGroup/Humans/NTDF2/NTDF21", MakeLeader, true );
   order( "MissionGroup/Humans/NTDF3/NTDF31", MakeLeader, true );
   order( "MissionGroup/Humans/NTDF4/NTDF41", MakeLeader, true );
   order( "MissionGroup/Humans/NTDF5/NTDF51", MakeLeader, true );

   order( "MissionGroup/Cybrids/CYBR1/CYBR11", MakeLeader, true );
   order( "MissionGroup/Cybrids/CYBR2/CYBR21", MakeLeader, true );
   order( "MissionGroup/Cybrids/CYBR3/CYBR31", MakeLeader, true );
   order( "MissionGroup/Cybrids/CYBR4/CYBR41", MakeLeader, true );
   order( "MissionGroup/Cybrids/CYBR5/CYBR51", MakeLeader, true );
}

//-----------------------------------------------------------------------------

function vehicle::onNewLeader(%this)
{
   //-----------------------------------------------
   // human

   echo( "new leader -----------------------------------------------" );

   %group = getGroup(%this);
   order( %group, formation, delta );

   echo ( %group, $NTDF1 );

   if ( %group == $NTDF1 )
      order( %this, guard, "MissionGroup/HuPath1" );
   else if ( %group == $NTDF2 )
      order( %this, guard, "MissionGroup/HuPath2" );
   else if ( %group == $NTDF3 )
      order( %this, guard, "MissionGroup/HuPath3" );
   else if ( %group == $NTDF4 )
      order( %this, guard, "MissionGroup/HuPath4" );
   else if ( %group == $NTDF5 )
      order( %this, guard, "MissionGroup/HuPath5" );

   //-----------------------------------------------
   // cybrid

   else if ( %group == $CYBR1 )
      order( %this, guard, "MissionGroup/CyPath1" );
   else if ( %group == $CYBR2 )
      order( %this, guard, "MissionGroup/CyPath2" );
   else if ( %group == $CYBR3 )
      order( %this, guard, "MissionGroup/CyPath3" );
   else if ( %group == $CYBR4 )
      order( %this, guard, "MissionGroup/CyPath4" );
   else if ( %group == $CYBR5 )
      order( %this, guard, "MissionGroup/CyPath5" );
}

//-----------------------------------------------------------------------------

function player::onAdd(%this)
{
   for ( %i=0; %i<3; %i++ )
      %this.AI[%i] = 0;

   %this.numKills = 0;
}

//-----------------------------------------------------------------------------

function vehicle::onAdd(%vehicle)
{
   %player = playerManager::vehicleIdToPlayerNum(%vehicle);

//   if( %player ) // which means it's a human
//   {
//   }
}

//-----------------------------------------------------------------------------

function player::onRemove( %player )
{
}

//-----------------------------------------------------------------------------

function vehicle::onDestroyed( %victimVeh, %destroyerVeh )
{
   if( getTeam( %destroyerVeh ) != getTeam( %victimVeh ) )
   {
      %player = playerManager::vehicleIdToPlayerNum( %destroyerVeh );
      %player.numKills++;      
      $HumanKills++;
   }

   //----------------------------------------------------------------
   // if it's an AI, drop a new one 
   if( getTeam( %victimVeh ) == *IDSTR_TEAM_PURPLE )
   {
   }
}

//-----------------------------------------------------------------------------
// this creates a new random AI Herc

function getRandomAI()
{
   // check for valid ranges
   %i = RandomInt(1,28);
   if ( %i>8 && %i<10 )
      %i = RandomInt(1,8);
   if ( %i>17 && %i<20 )
      %i = RandomInt(20,28);

   // get appropriate type Tank or Herc
   %type = Herc;
   if ( %i>5  && %i<= 8 )   %type = Tank;
   if ( %i>14 && %i<= 17 )  %type = Tank;
   if ( %i>24 && %i<= 26 )  %type = Tank;

   return NewObject( "AI" @ %i, %type, %i );
}

//-----------------------------------------------------------------------------
// this gives you a new random AI Herc

function dropNewVehicle( %player )
{
   %playerVeh = playerManager::playerNumToVehicleId(%player);                                                                                    
   %herc = getRandomAI();
   setTeam( %herc, *IDSTR_TEAM_PURPLE );
   setPilotId(%herc,28);
   %x = getPosition(%playerVeh,x)+RandomInt(-400,400);
   %y = getPosition(%playerVeh,y)+RandomInt(-400,400);
   %z = getPosition(%playerVeh,z)+150;
   setPosition( %herc, %x, %y, %z);
   schedule( "order(" @ %herc @ ",attack," @ %playerVeh @ ");", 5 );

   %herc.owner = %player;
   for ( %i=0; %i<4; %i++ )
   {
      if ( !%player.AI[%i] )
      {
         %player.AI[%i] = %herc;
         break;
      }
   }
}

//------------------------------------------------------------------------------
// Scoreboard stuff (doesn't work yet)
//------------------------------------------------------------------------------

function initScoreBoard()
{
   deleteVariables( "$ScoreBoard::PlayerColumn*" );
   deleteVariables( "$ScoreBoard::TeamColumn*" );

   $ScoreBoard::PlayerColumnHeader1    = "Team";
   $ScoreBoard::PlayerColumnHeader2    = "Kills";

   $ScoreBoard::PlayerColumnFunction1  = "getTeam2";
   $ScoreBoard::PlayerColumnFunction2  = "getKills";

   $ScoreBoard::TeamColumnHeader1      = "Total Kills";
   $ScoreBoard::TeamColumnFunction1    = "getTeamKills";
 
   serverInitScoreBoard();
}


function getTeam2(%player)
{
   if( getTeam( %player ) == *IDSTR_TEAM_PURPLE )
   {
      return "AI";
   }
   else
   {
      return "Human";
   }
}

function getKills(%player)
{
   return %player.numKills;
}

function getTeamKills(%team)
{
   if( getTeamNameFromTeamId(%team) == *IDSTR_TEAM_PURPLE )
   {
      return $aiKills;
   }
   else
   {
      return $playerKills;
   }   
}

//------------------------------------------------------------------------------
// Healing Pad Functionality
//------------------------------------------------------------------------------
// you wish

//------------------------------------------------------------------------------
// Ammo Pad Functionality
//------------------------------------------------------------------------------
function ZenAmmo::trigger::onEnter(%this, %object)
{
   Zen::onEnter(%this, %object, *IDMULT_CHAT_AMMOPAD, true, true);  
}

function ZenAmmo::trigger::onContact(%this, %object)
{
   Zen::work(%this, %object, 0, $ammoRate, $padWaitTime, true); 
}
