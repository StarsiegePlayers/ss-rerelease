$alphaComplete       = false;
$bravoComplete       = false;
$charlieComplete     = false;
$deltaComplete       = false;
$warning             = 0;
$playerNum           = 0;
$playerId            = 0;
$lastDistance        = 0;
$stray               = 0.0;
$missionFailed       = false;
$missionCompleted    = false;
$runNearingFunction  = false;
$missionFailed       = false;
$alphaId             = 0;
$bravoId             = 0;
$charlieId           = 0;
$deltaId             = 0;
$customId            = 0;

function onForceToDebrief(%string)
{
   say(0, 1234, *IDSTR_TRG_OUTR01, "TR_OUTR01.WAV");
   TRForceToDebrief(%string);
}

function onMissionFailed(%pause, %order_disregarded)
{
	$missionFailed = true;
	$Client::TrainingMissionStatus = "ONE_OR_MORE_FAILED";

	schedule("say(0, 1234, *IDSTR_TRG_HNTRFL, \"TR_HNTRFL.WAV\");", %pause);

   if (%order_disregarded)
   {
   	schedule("onForceToDebrief(*IDSTR_TRG_FOLLOWORDERS);", 5 + %pause);
   }
   else
   {
	   schedule("onForceToDebrief(*IDSTR_MISSION_FAILED);", 5 + %pause);
   }
}

function onMissionCompleted(%string, %speech)
{
   $missionCompleted = true;
	$Client::TrainingMissionStatus = "ALL_COMPLETED";

	say(0, 1234, %string, %speech);
	schedule("onForceToDebrief(*IDSTR_MISSION_SUCCESSFUL);", 5);
}

function onMissionStart()
{
   cdAudioCycle(SS4, Purge);
   exec("keymaps\\Key_Mouse_DigiJoy.cs");
	$Client::TrainingMissionStatus = "IN_PROGRESS";
}

function player::onAdd(%this)
{
   $playerNum = %this;
}

function ensureNavPointSet(%nav_point)
{
   if (getNavMarkerStatus(%nav_point) == true && $missionFailed == false && $missionCompleted == false)
   {
      setNavMarker(%nav_point, true, -1);
      schedule("ensureNavPointSet("@%nav_point@");", 1);
   }
}

function disableAllNavPoints()
{
   disableNavMarker("MissionGroup/Nav Points/Nav Alpha");
   disableNavMarker("MissionGroup/Nav Points/Nav Bravo");
   disableNavMarker("MissionGroup/Nav Points/Nav Charlie");
   disableNavMarker("MissionGroup/Nav Points/Nav Delta");
}

function vehicle::onAdd(%this)
{
   %num = playerManager::vehicleIdToPlayerNum(%this);
      
   if(%num == $playerNum)
   {
      $playerId = %this;
   }
}

function onSPClientInit()
{
   $alphaId   = getObjectId("MissionGroup/Nav Points/Nav Alpha");
   $bravoId   = getObjectId("MissionGroup/Nav Points/Nav Bravo");
   $charlieId = getObjectId("MissionGroup/Nav Points/Nav Charlie");
   $deltaId   = getObjectId("MissionGroup/Nav Points/Nav Delta");
   $customId  = getVehicleNavMarkerId($playerId);

   disableAllNavPoints();
   setHudChatDisplayType(1);

   initTrainer();
}

function vehicle::onDestroyed(%this, %who)
{
   if (%this == $playerId)
   {
      onMissionFailed(0, false);
   }
}

function onStrayNavPoint(%arg)
{
   if (%arg == $alphaId)
   {
		say(0, 1234, *IDSTR_TRG_HNTRAW, "TR_HNTRAW.WAV");
   }
   else if (%arg == $bravoId)
   {
		say(0, 1234, *IDSTR_TRG_HNTRBW, "TR_HNTRBW.WAV");
   }
   else if (%arg == $charlieId)
   {
		say(0, 1234, *IDSTR_TRG_HNTRCW, "TR_HNTRCW.WAV");
   }
   else if (%arg == $deltaId)
   {
		say(0, 1234, *IDSTR_TRG_HNTRDW, "TR_HNTRDW.WAV");
   }
   else if (%arg == $customId)
   {
      say(0, 1234, *IDSTR_TRG_HNTRYW, "TR_HNTRYW.WAV");
   }
}

function onLeaveTrainingArea()
{
   say(0, 1234, *IDSTR_TRG_HNTRFW, "TR_HNTRFW.WAV");
   onMissionFailed(5, true);
}

function msay(%channel, %client, %string, %wave)
{
   if ($missionFailed == false && $missionCompleted == false)
   {
      say(%channel, %client, %string, %wave);
   }
}

function monitorProgress(%arg, %nearing_distance, %arrival_distance, %stray_distance)
{
   $stray              = 0.0;
   $warning            = 0;
   $runNearingFunction = false;
   $lastDistance       = getDistance($playerId, %arg);

	playerDistanceCheck(%arg, %nearing_distance, %arrival_distance, %stray_distance);
}

function playerDistanceCheck(%arg, %nearing_distance, %arrival_distance, %stray_distance)
{
   %distance = getDistance($playerId, %arg);
	%pause    = 0;
   %continue = getNavMarkerStatus(%arg);

   if (%continue && $missionFailed == false && $missionCompleted == false)
   {
	   %diff = %distance - $lastDistance;

	   if (%diff > 0.0)
	   {
		   $stray += %diff;

		   if ($stray > %stray_distance)
		   {
            $warning ++;
            $stray = 0.0;

            if ($warning < 3)
            {
               %pause = 6;
               onStrayNavPoint(%arg);
            }
            else
            {
               %continue = false;
               onLeaveTrainingArea();
            }
		   }
	   }
	   
	   else if (%diff < 0.0)
	   {
		   $warning = 0;
		   $stray   = 0.0;

		   if (%nearing_distance  && (%distance <= 
             %nearing_distance) && ($runNearingFunction == false))
		   {
			   $runNearingFunction = true;
            onNearingNavPoint(%arg);
		   }

		   if (%arrival_distance && (%distance <= 
             %arrival_distance))
		   {
            %continue = false;
            onArriveNavPoint(%arg);
		   }
      }

	   if (%continue)
	   {
		   $lastDistance = %distance;
         schedule("playerDistanceCheck("
            @%arg@", "@%nearing_distance@", "@%arrival_distance@", "@%stray_distance@");", 1 + %pause);
	   }
   }
}
