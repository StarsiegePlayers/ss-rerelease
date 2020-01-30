//------------------------------------------------------------------------------
//
// FILENAME:   TDM_AI_2_DIE_4
//
// Author:     Louie McCrady (Hercasaurus Rex/Tyranny)
// Layout:     Shamelessly stolen from Chupie Doll & Youth in Asia
//
// Description:
//    The purpose of this mission is to provide an opportunity to 
//    sample what the AI is capable of when all of the AI pilots 
//    have what I consider to be very good attributes.  The vehicles
//    are randomly selected.  There are different numbers of AI's
//    at each base for each player that drops.  AI's leave when 
//    the player that created them leaves.
//    
//       Yellow   1
//       Blue     2
//       Red      3
//    
// Objective:
//    
//------------------------------------------------------------------------------

$missionName = "TDM_AI_2_Die_4";

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
   $server::AllowTeamBlue   = true;
   $server::AllowTeamRed    = true;
   $server::AllowTeamPurple = false;

   $server::disableTeamYellow = false;
   $server::disableTeamBlue   = false;
   $server::disableTeamRed    = false;
   $server::disableTeamPurple = true;
}

//-----------------------------------------------------------------------------

function onMissionStart()
{
   desertSounds();
   $AIKills = 0;
   $HumanKills = 0;
}

//-----------------------------------------------------------------------------

function onMissionLoad()
{
   cdAudioCycle("SS1", "Newtech", "Yougot"); 
}

//-----------------------------------------------------------------------------

function player::onAdd(%this)
{
   for ( %i=0; %i<3; %i++ )
      %this.AI[%i] = 0;

   %this.numKills = 0;
}

//-----------------------------------------------------------------------------
// When humans drop in, assign an AI to attack them.
// Remove extra AI's if they had AI's but dropped in at a lower base.

function vehicle::onAdd(%vehicle)
{
   %player = playerManager::vehicleIdToPlayerNum(%vehicle);

   if( %player ) // which means it's a human
   {
      echo ("--------- players AI[0] ", %player.AI[0] );
      echo ("--------- players AI[1] ", %player.AI[1] );
      echo ("--------- players AI[2] ", %player.AI[2] );

      if ( %player.AI[0] ) 
         schedule( "order(" @ %player.AI[0] @ ",attack," @ %vehicle @ ");", 5 );
      else
         schedule( "dropNewVehicle(" @ %player @ ");", 5 );

      if ( getTeam(%vehicle) != *IDSTR_TEAM_YELLOW )
      {
         if ( %player.AI[1] ) 
            schedule( "order(" @ %player.AI[1] @ ",attack," @ %vehicle @ ");", 5 );
         else
            schedule( "dropNewVehicle(" @ %player @ ");", 5 );
      }
      else if ( %player.AI[1] )
      {
         deleteObject( %player.AI[1] );
         %player.AI[1] = 0;
      }

      if ( getTeam(%vehicle) == *IDSTR_TEAM_RED )
      {
         if ( %player.AI[2] ) 
            schedule( "order(" @ %player.AI[2] @ ",attack," @ %vehicle @ ");", 5 );
         else
            schedule( "dropNewVehicle(" @ %player @ ");", 5 );
      }
      else if ( %player.AI[2] )
      {
         deleteObject( %player.AI[2] );
         %player.AI[2] = 0;
      }
   }
}

//-----------------------------------------------------------------------------
// If player goes, get rid of his associated AI's too

function player::onRemove( %player )
{
   for ( %i=0; %i<3; %i++ )
   {
      if ( %player.AI[%i] )
         deleteObject( %player.AI[%i] );
   }
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
      for ( %i=0; %i<4; %i++ )
      {
         %player = %victimVeh.owner;
         if ( %player.AI[%i] == %victimVeh )
         {
            %player.AI[%i] = 0;
            break;
         }
      }

// if you use resapawnVehicle instead of dropNewVehicle, you'll get the
// same AI opponent back after you kill him.
//      schedule( "respawnVehicle(" @ %victimVeh @ "," @ %destroyerVeh @ ");", 5 );

      schedule( "dropNewVehicle(" @ %player @ ");", 5 );

   }
   else
      $AIKills++;
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

//-----------------------------------------------------------------------------
// this respawns the AI Herc that just died

function respawnVehicle( %oldVeh, %destroyer )
{
   %player = playerManager::vehicleIdToPlayerNum( %destroyer );

   %x = getPosition(%destroyer,x)+200;
   %y = getPosition(%destroyer,y)+200;
   %z = getPosition(%destroyer,z)+150;

   %herc = hercVehicle(%oldVeh);
   setPosition(%herc, %x, %y, %z);
   schedule( "deleteObject(" @ %oldVeh @ ");", $respawnDealNoHq + 10 );
   addToSet( "MissionGroup", %herc );

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
function ZenHeal::trigger::onEnter(%this, %object)
{
   Zen::onEnter(%this, %object, *IDMULT_CHAT_HEALPAD, true, true);  
}
function ZenHeal::trigger::onContact(%this, %object)
{
   Zen::work(%this, %object, $healRate, 0, $padWaitTime, true); 
}

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
