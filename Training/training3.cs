$currentWayPoint      = 0;
$playedHercMessage    = false;
$playedTankMessage    = false;
$iTargetCheck         = 0;
$droneDestroyed       = false;
$tank1Destroyed       = false;
$tank2Destroyed       = false;
$herc1Destroyed       = false;
$herc2Destroyed       = false;
$orderedAttackTanks   = false;
$orderedTargetTank    = false;
$orderedTargetDrone   = false;
$playerTargeted       = false;

exec("training_fns.cs");

DropPoint drop1
{
   name = "Drop Point 1";
   desc = "Drop Point 1";
};

function setupTargets()
{
   order("MissionGroup/Targets/Nav Alpha", HoldFire,     true);
   order("MissionGroup/Targets/Nav Alpha", HoldPosition, true);

   setHercOwner("MissionGroup\\Targets\\Nav Alpha", $playerId);
   setHercOwner("MissionGroup\\Targets\\Nav Bravo", $playerId);
}

function checkForPlayerTargeted()
{
   if ($playerTargeted)
   {
      onArriveNavPoint($charlieId);
      checkForHercsDestroyed();
   }
   else
   {
      schedule("checkForPlayerTargeted();", 1);
   }
}

function checkForHercsDestroyed()
{
   %pause = 0;

   if ($herc1Destroyed && $herc2Destroyed)
   {
      onMissionCompleted(*IDSTR_TR3_HNTR32, "TR03_HNTR32.WAV");
   }
   else
   {
      if (($herc1Destroyed || $herc2Destroyed) && $playedHercMessage == false)
      {
         %pause             = 4;
         $playedHercMessage = true;
         msay(0, 1234, *IDSTR_TR3_HNTR31, "TR03_HNTR31.WAV");
      }
      schedule("checkForHercsDestroyed();", 1 + %pause);
   }
}

function checkForDroneDestroyed()
{
   if ($droneDestroyed == true)
   {
      msay(0, 1234, *IDSTR_TR3_HNTR30, "TR03_HNTR30.WAV");
      schedule("setNavCharlie();", 11);
   }
   else
   {
      schedule("checkForDroneDestroyed();", 1);
   }
}

function checkForTanksDestroyed()
{
   %pause = 0;

   if ($tank1Destroyed && $tank2Destroyed)
   {
      onArriveNavPoint($alphaId);

      if ($orderedAttackTanks)
      {
         msay(0, 1234, *IDSTR_TR3_HNTR22, "TR03_HNTR22.WAV");
      }
      else
      {
         msay(0, 1234, *IDSTR_TRG_HNTRGS, "TR_HNTRGS.WAV");
      }
      schedule("explainSensor();", 2);
   }
   else
   {
      if (($tank1Destroyed || $tank2Destroyed) && $playedTankMessage == false)
      {
         %pause             = 3;
         $playedTankMessage = true;
         msay(0, 1234, *IDSTR_TR3_HNTR21, "TR03_HNTR21.WAV");
      }
      schedule("checkForTanksDestroyed();", 1 + %pause);
   }
}

function orderDestroyTanks()
{
   %pause = 0;

   if ($tank1Destroyed == false || $tank2Destroyed == false) 
   {
      $orderedAttackTanks = true; 

      if ($tank1Destroyed == false && $tank2Destroyed == false)
      {
         %pause = 9;
         msay(0, 1234, *IDSTR_TR3_HNTR20, "TR03_HNTR20.WAV");
      }
      else
      { 
         %pause             = 3;
         $playedTankMessage = true;
         msay(0, 1234, *IDSTR_TR3_HNTR21, "TR03_HNTR21.WAV");
      }
   }

   schedule("checkForTanksDestroyed();", 1 + %pause);
}

function checkForTargetAcquired(%arg)
{
   %pause       = 0;
   %curTargetID = getCurrentTargetID($playerId);

   if (%arg == navAlpha)
   {
      if (%curTargetID == getObjectId("MissionGroup/Targets/Nav Alpha/Tank1") ||
          %curTargetID == getObjectid("MissionGroup/Targets/Nav Alpha/Tank2"))
      {
         $iTargetCheck = 0;

         msay(0, 1234, *IDSTR_TR3_HNTR17, "TR03_HNTR17.WAV");
         schedule("msay(0, 1234, *IDSTR_TR3_HNTR18, \"TR03_HNTR18.WAV\");",  8);
         schedule("msay(0, 1234, *IDSTR_TR3_HNTR19, \"TR03_HNTR19.WAV\");", 26);

         schedule("orderDestroyTanks();", 31);
      }
      else
      {
         if ($tank1Destroyed && $tank2Destroyed)
         {
            onMissionFailed(0, true);
         }
         else
         {
            $iTargetCheck ++;

            if ($iTargetCheck == 10)
            {
               %pause        = 5;
               $iTargetCheck = 0;
               msay(0, 1234, *IDSTR_TR3_HNTR16, "TR03_HNTR16.WAV");
            }

            schedule("checkForTargetAcquired(navAlpha);", 1 + %pause);
         }
      }
   }
   else if (%arg == navBravo)
   {
      if (%curTargetId == getObjectId("MissionGroup/Targets/Nav Bravo/Drone1"))
      {
         order(%curTargetId, Retreat, "MissionGroup/Paths/Drone/Retreat");
         $iTargetCheck = 0;

         onArriveNavPoint($bravoId);
         msay(0, 1234, *IDSTR_TR3_HNTR28, "TR03_HNTR28.WAV");

         schedule("msay(0, 1234, *IDSTR_TR3_HNTR29, \"TR03_HNTR29.WAV\");", 12);
         schedule("checkForDroneDestroyed();", 23);
      }
      else
      {
         if ($orderedTargetDrone)
         {
            $iTargetCheck ++;

            if ($iTargetCheck == 10)
            {
               %pause        = 5;
               $iTargetCheck = 0;
               msay(0, 1234, *IDSTR_TR3_HNTR27, "TR03_HNTR27.WAV");
            }
         }
         schedule("checkForTargetAcquired(navBravo);", 1 + %pause);
      }
   }
}

function explainSensor()
{
   msay(0, 1234, *IDSTR_TR3_HNTR23, "TR03_HNTR23.WAV");
   schedule("msay(0, 1234, *IDSTR_TR3_HNTR24, \"TR03_HNTR24.WAV\");", 16);
   schedule("setNavBravo();", 28);
}

function startHercPatrol()
{
   order("MissionGroup\\Targets\\Nav Charlie", Formation, Wedge);
   order("MissionGroup\\Targets\\Nav Charlie", Guard, "MissionGroup/Paths/Herc");
}

function onNearingNavPoint(%arg)
{
   if (%arg == $alphaId)
   {
      %pause       = 0;
      %curTargetID = getCurrentTargetID($playerId);
      %distance    = getDistance($playerId, $alphaId);

      $orderedTargetTank = true;

      if (%curTargetID == getObjectId("MissionGroup/Targets/Nav Alpha/Tank1") ||
          %curTargetID == getObjectid("MissionGroup/Targets/Nav Alpha/Tank2"))
      {
         %pause = 8;
         msay(0, 1234, *IDSTR_TR3_HNTR15, "TR03_HNTR15.WAV");
      }
      else
      {
         if (%distance > 100)
         {
            %pause = 13;
            msay(0, 1234, *IDSTR_TR3_HNTR13, "TR03_HNTR13.WAV");
         }
         else
         {
            %pause = 11;
            msay(0, 1234, *IDSTR_TR3_HNTR14, "TR03_HNTR14.WAV");
         }
      }
      schedule("checkForTargetAcquired(navAlpha);", %pause);
   }
   else if (%arg == $bravoId)
   {
      if (getCurrentTargetID($playerId) != 
          getObjectId("MissionGroup/Targets/Nav Bravo/Drone1"))
      {
         $orderedTargetDrone = true;
         msay(0, 1234, *IDSTR_TR3_HNTR27, "TR03_HNTR27.WAV");
      }
   }
}

function onArriveNavPoint(%arg)
{
   setNavMarker(%arg, false, -1);
}

function setNavCharlie()
{
   $currentWayPoint = $charlieId;
   startHercPatrol();
   setNavMarker("MissionGroup/Nav Points/Nav Charlie", true,  -1);
   schedule("monitorProgress("@$charlieId@", 0, 100, 50);", 12);
   schedule("checkForPlayerTargeted();", 1);
   ensureNavPointSet($charlieId);
}

function setNavBravo()
{
   $currentWayPoint = $bravoId;
   order("MissionGroup\\Targets\\Nav Bravo", Guard, "MissionGroup/Paths/Drone/Patrol");
   setNavMarker("MissionGroup/Nav Points/Nav Bravo", true,  -1);
   msay(0, 1234, *IDSTR_TR3_HNTR25, "TR03_HNTR25.WAV");
   schedule("monitorProgress("@$bravoId@", 900, 100, 50);", 3);
   schedule("checkForTargetAcquired(navBravo);", 1);
   ensureNavPointSet($bravoId);
}

function setNavAlpha()
{
   $currentWayPoint = $alphaId;
   setNavMarker("MissionGroup/Nav Points/Nav Alpha", true, -1);
   msay(0, 1234, *IDSTR_TR3_HNTR12, "TR03_HNTR12.WAV");
   schedule("monitorProgress("@$alphaId@", 900, 100, 50);", 5);
   ensureNavPointSet($alphaId);
}

function initTrainer()
{
   setupTargets();
   say(0, 1234, "", "TR_INTR01.WAV");
   schedule("msay(0, 1234, *IDSTR_TR3_HNTR01, \"TR03_HNTR01.WAV\");",   4);
   schedule("msay(0, 1234, *IDSTR_TR3_HNTR02, \"TR03_HNTR02.WAV\");",  14);
   schedule("msay(0, 1234, *IDSTR_TR3_HNTR03, \"TR03_HNTR03.WAV\");",  31);
   schedule("msay(0, 1234, *IDSTR_TR3_HNTR04, \"TR03_HNTR04.WAV\");",  43);
   schedule("msay(0, 1234, *IDSTR_TR3_HNTR05, \"TR03_HNTR05.WAV\");",  54);
   schedule("msay(0, 1234, *IDSTR_TR3_HNTR06, \"TR03_HNTR06.WAV\");",  70);
   schedule("msay(0, 1234, *IDSTR_TR3_HNTR07, \"TR03_HNTR07.WAV\");",  88);
   schedule("msay(0, 1234, *IDSTR_TR3_HNTR08, \"TR03_HNTR08.WAV\");",  100);
   schedule("msay(0, 1234, *IDSTR_TR3_HNTR09, \"TR03_HNTR09.WAV\");", 111);
   schedule("msay(0, 1234, *IDSTR_TR3_HNTR10, \"TR03_HNTR10.WAV\");", 122);
   schedule("msay(0, 1234, *IDSTR_TR3_HNTR11, \"TR03_HNTR11.WAV\");", 136);
   schedule("setNavAlpha();", 152);
}

function tank1::vehicle::onDestroyed(%this, %who)
{
   $tank1Destroyed = true;

   if ($currentWayPoint != $alphaId || $orderedTargetTank == false)
   {
      onMissionFailed(0, true);
   }
}

function tank2::vehicle::onDestroyed(%this, %who)
{
   $tank2Destroyed = true;

   if ($currentWayPoint != $alphaId || $orderedTargetTank == false)
   {
      onMissionFailed(0, true);
   }
}

function drone1::vehicle::onDestroyed(%this, %who)
{
   $droneDestroyed = true;

   if ($currentWayPoint != $bravoId)
   {
      onMissionFailed(0, true);
   }
}

function herc1::vehicle::onDestroyed(%this, %who)
{
   $herc1Destroyed = true;

   if ($currentWayPoint != $charlieId)
   {
      onMissionFailed(0, true);
   }
}

function herc2::vehicle::onDestroyed(%this, %who)
{
   $herc2Destroyed = true;

   if ($currentWayPoint != $charlieId)
   {
      onMissionFailed(0, true);
   }
}

function vehicle::onTargeted(%this, %who)
{
   if (%this == $playerId && $currentWayPoint == $charlieId && $playerTargeted == false)
   {
      if (%who == getObjectId("MissionGroup/Targets/Nav Charlie/Herc1") ||
          %who == getObjectId("MissionGroup/Targets/Nav Charlie/Herc2"))
      {
         $playerTargeted = true;
      }
   }
}
