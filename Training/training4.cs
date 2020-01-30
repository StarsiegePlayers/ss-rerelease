$toldAboutTurrets         = false;
$toldToWatchLead          = false;
$currentWayPoint          = 0;
$bigKludge                = false;
$squadMate1Id             = 0;
$squadMate2Id             = 0;
$squadMate1Attacked       = 0;
$squadMate2Attacked       = 0;
$playedTip                = false;
$iCheck                   = 0;
$bunkerScanned            = false;
$squadHalted              = false;
$squadOrderedToAttack     = false;
$alphaComplete            = false;
$playedCharlieMessage     = false;
$playedDeltaMessage       = false;
$turret1Destroyed         = false;
$turret2Destroyed         = false;
$herc1Destroyed           = false;
$herc2Destroyed           = false;
$squadMateAttackingTurret = false;
$Client::Camera           = "player";
$Client::Weapon0InGroup0  = true;
$Client::Weapon1InGroup0  = true;
$Client::Weapon2InGroup0  = true;
$Client::Weapon3InGroup0  = true;
$Client::Weapon0InGroup1  = true;
$Client::Weapon1InGroup1  = true;

exec("training_fns.cs");

function checkForMissilesLinked()
{
   %pause = 0;

   if ($Client::WeaponGroup1Mode == 1)
   {
      $iCheck = 0;
      msay(0, 1234, *IDSTR_TR4_HNTR24, "TR04_HNTR24.WAV");
      schedule("setNavBravo();", 20);
   }
   else
   {
      $iCheck ++;

      if ($iCheck == 10)
      {
         %pause  = 3;
         $iCheck = 0;
         msay(0, 1234, *IDSTR_TR4_HNTR23, "TR04_HNTR23.WAV");
      }
      schedule("checkForMissilesLinked();", 1 + %pause);
   }
}

function explainTacticalStrategy()
{
   schedule("msay(0, 1234, *IDSTR_TR4_HNTR32, \"TR04_HNTR32.WAV\");",  2);
   schedule("msay(0, 1234, *IDSTR_TR4_HNTR33, \"TR04_HNTR33.WAV\");", 12);
   schedule("msay(0, 1234, *IDSTR_TR4_HNTR34, \"TR04_HNTR34.WAV\");", 27);
   schedule("setNavCharlie();", 34);
}

function onTurretsDestroyed()
{
   onArriveNavPoint($bravoId);

   msay(0, 1234, *IDSTR_TRG_HNTRGS, "TR_HNTRGS.WAV");
   explainTacticalStrategy();
}

function checkForTurretsDestroyed()
{
   if ($turret1Destroyed == true && $turret2Destroyed == true)
   {
      onTurretsDestroyed();
   }
   else
   {
      schedule("checkForTurretsDestroyed();", 1);
   }
}

function checkForLasersRemoved()
{
   %pause = 0;

   if ($Client::Weapon0InGroup1 == false && $Client::Weapon1InGroup1 == false)
   {
      $iCheck = 0;

      msay(0, 1234, *IDSTR_TR4_HNTR23, "TR04_HNTR23.WAV");
      schedule("checkForMissilesLinked();", 3);
   }
   else
   {
      $iCheck ++;

      if ($iCheck == 10)
      {
         %pause  = 8;
         $iCheck = 0;
         msay(0, 1234, *IDSTR_TR4_HNTR20, "TR04_HNTR20.WAV");
      }

      schedule("checkForLasersRemoved();", 1 + %pause);
   }
}

function checkForSecondMissileRemoved()
{
   %pause = 0;

   if ($Client::Weapon3InGroup0 == false)
   {
      $iCheck = 0;
      msay(0, 1234, *IDSTR_TR4_HNTR20, "TR04_HNTR20.WAV");
      schedule("checkForLasersRemoved();", 8);
   }
   else
   {
      $iCheck ++;

      if ($iCheck == 10)
      {
         %pause  = 6;
         $iCheck = 0;
         msay(0, 1234, *IDSTR_TR4_HNTR19, "TR04_HNTR19.WAV");
      }
      schedule("checkForSecondMissileRemoved();", 1 + %pause);
   }
}

function checkForFirstMissileRemoved()
{
   %pause = 0;

   if ($Client::Weapon2InGroup0 == false)
   {
      $iCheck = 0;
      msay(0, 1234, *IDSTR_TR4_HNTR19, "TR04_HNTR19.WAV");
      schedule("checkForSecondMissileRemoved();", 6);
   }
   else
   {
      $iCheck ++;

      if ($iCheck == 10)
      {
         %pause  = 10;
         $iCheck =  0;
         msay(0, 1234, *IDSTR_TR4_HNTR16, "TR04_HNTR16.WAV");
      }
      schedule("checkForFirstMissileRemoved();", 1 + %pause);
   }
}

function explainFiringChains()
{
   msay(0, 1234, *IDSTR_TR4_HNTR14, "TR04_HNTR14.WAV");
   schedule("msay(0, 1234, *IDSTR_TR4_HNTR15, \"TR04_HNTR15.WAV\");", 18);
   schedule("msay(0, 1234, *IDSTR_TR4_HNTR16, \"TR04_HNTR16.WAV\");", 29);
   schedule("checkForFirstMissileRemoved();", 39);
}

function checkForShieldsMovedForward()
{
   %pause = 0;

   if (getShieldDirStr($playerId) == 1.0)
   {
      setNavDelta();
   }
   else
   {
      $iShieldCheck ++;

      if ($iShieldCheck == 10)
      {
         %pause        = 7;
         $iShieldCheck = 0;
         msay(0, 1234, *IDSTR_TR4_HNTR43, "TR04_HNTR43.WAV");
      }

      schedule("checkForShieldsMovedForward();", 1 + %pause);
   }
}

function checkForLocalEnemiesDestroyed()
{
   %pause = 0;

   if ($herc1Destroyed == true && $herc2Destroyed == true)
   {
      msay(0, 1234, *IDSTR_TR4_HNTR40, "TR04_HNTR40.WAV");
      schedule("msay(0, 1234, *IDSTR_TR4_HNTR41, \"TR04_HNTR41.WAV\");", 6);
      schedule("msay(0, 1234, *IDSTR_TR4_HNTR42, \"TR04_HNTR42.WAV\");", 20);
      schedule("msay(0, 1234, *IDSTR_TR4_HNTR43, \"TR04_HNTR43.WAV\");", 35);
      schedule("checkForShieldsMovedForward();", 42);
   }
   else
   {
      if ($playedCharlieMessage == false)
      {
         %pause                = 3;
         $playedCharlieMessage = true;
         msay(0, 1234, *IDSTR_TR4_HNTR39, "TR04_HNTR39.WAV");
      }
      schedule("checkForLocalEnemiesDestroyed();", 1 + %pause);
   }
}

function checkForScan()
{
   %pause = 0;

   if ($bunkerScanned == true)
   {
      $iCheck = 0;

      onArriveNavPoint($charlieId);
      msay(0, 1234, *IDSTR_TR4_HNTR38, "TR04_HNTR38.WAV");
      schedule("checkForLocalEnemiesDestroyed();", 8);
   }
   else
   {
      $iCheck ++;

      if ($iCheck == 5)
      {
         %pause  = 4;
         $iCheck = 0;
         msay(0, 1234, *IDSTR_TR4_HNTR37, "TR04_HNTR37.WAV");
      }

      schedule("checkForScan();", 1 + %pause);
   }
}

function orderPlayerCamera()
{
   if ($Client::Camera == "player")
   {
      if (isCloaked($playerId))
      {
         msay(0, 1234, *IDSTR_TR4_HNTR12, "TR04_HNTR12.WAV");
         schedule("checkForCloak(false);", 5);
      }
      else
      {
         %pause = 0;

         if ($alphaComplete == false)
         {
            %pause = 2;
            msay(0, 1234, *IDSTR_TR4_HNTR13, "TR04_HNTR13.WAV");
         }
         schedule("waitForAlphaCompletion();", %pause);
      }
   }
   else
   {
      msay(0, 1234, *IDSTR_TR4_HNTR11, "TR04_HNTR11.WAV");
      schedule("checkForView(\"player\");", 6);
   }
}

function onObserverCamera()
{
   if (isCloaked($playerId))
   {
      msay(0, 1234, *IDSTR_TR4_HNTR10, "TR04_HNTR10.WAV");
      schedule("orderPlayerCamera();", 14);
   }
   else
   {
      $bigKludge = true;
      msay(0, 1234, *IDSTR_TR4_HNTR47, "TR04_HNTR47.WAV");
      schedule("checkForCloak(true);", 6);
   }
}

function checkForView(%arg)
{
   %pause = 0;

   if ($Client::Camera == %arg)
   {
      $iCheck = 0;

      if (%arg == "orbitCamera")
      {
         onObserverCamera();
      }
      else
      {
         if (isCloaked($playerId))
         {
            msay(0, 1234, *IDSTR_TR4_HNTR12, "TR04_HNTR12.WAV");
            schedule("checkForCloak(false);", 5);
         }
         else
         {
            %pause = 0;
            if ($alphaComplete == false)
            {
               %pause = 2;
               msay(0, 1234, *IDSTR_TR4_HNTR13, "TR04_HNTR13.WAV");
            }
            schedule("waitForAlphaCompletion();", %pause);
         }
      }
   }
   else
   {
      $iCheck ++;

      if ($iCheck == 5)
      {
         $iCheck = 0;

         if (%arg == "orbitCamera")
         {
            %pause = 5;
            msay(0, 1234, *IDSTR_TR4_HNTR09, "TR04_HNTR09.WAV");
         }
         else
         {
            %pause = 6;
            msay(0, 1234, *IDSTR_TR4_HNTR11, "TR04_HNTR11.WAV");
         }
      }

      schedule("checkForView(\""@%arg@"\");", 1 + %pause);
   }
}

function waitForAlphaCompletion()
{
   if ($alphaComplete == true)
   {
      explainFiringChains();
   }
   else
   {
      schedule("waitForAlphaCompletion();", 1);
   }
}

function onCloaked()
{
   if ($Client::Camera == "orbitCamera")
   {
      onObserverCamera();
   }
   else
   {
      if (isCloaked($playerId))
      {
         msay(0, 1234, *IDSTR_TR4_HNTR09, "TR04_HNTR09.WAV");
         schedule("checkForView(\"orbitCamera\");", 5);
      }
      else
      {
         msay(0, 1234, *IDSTR_TR4_HNTR08, "TR04_HNTR08.WAV");
         schedule("checkForCloak("@%arg@");", 9);
      }
   }
}

function checkForCloak(%arg)
{
   %pause = 0;

   if (isCloaked($playerId) == %arg)
   {
      $iCheck = 0;

      if (%arg == true)
      {
         if ($bigKludge == false || $Client::Camera == "player")
         {
            $bigKludge = false;
            onCloaked();
         }
         else
         {
            msay(0, 1234, *IDSTR_TR4_HNTR10, "TR04_HNTR10.WAV");
            schedule("orderPlayerCamera();", 14);
         }
      }
      else
      {
         waitForAlphaCompletion();
      }
   }
   else
   {
      $iCheck ++;

      if ($iCheck == 5)
      {
         $iCheck = 0;

         if (%arg == true)
         {
            if ($bigKludge == false)
            {
               %pause = 10;
               msay(0, 1234, *IDSTR_TR4_HNTR08, "TR04_HNTR08.WAV");
            }
            else
            {
               %pause = 6;
               msay(0, 1234, *IDSTR_TR4_HNTR47, "TR04_HNTR47.WAV");
            }
         }
         else
         {
            %pause = 5;
            msay(0, 1234, *IDSTR_TR4_HNTR12, "TR04_HNTR12.WAV");
         }
      }

      schedule("checkForCloak("@%arg@");", 1 + %pause);
   }
}

function checkForSquadHalted(%arg)
{
   %pause = 0;

   if ($squadHalted == %arg)
   {
      $iCheck = 0;

      if (%arg == true)
      {
         msay(0, 1234, *IDSTR_TR4_HNTR06, "TR04_HNTR06.WAV");
         schedule("msay(0, 1234, *IDSTR_TR4_HNTR07, \"TR04_HNTR07.WAV\");", 3);
         schedule("checkForSquadHalted(false);", 10);
      }
      else
      {
         if (isCloaked($playerId))
         {
            onCloaked();
         }
         else
         {
            msay(0, 1234, *IDSTR_TR4_HNTR08, "TR04_HNTR08.WAV");
            schedule("checkForCloak(true);", 10);
         }
      }
   }
   else
   {
      $iCheck ++;

      if ($iCheck == 5)
      {
         $iCheck = 0;

         if (%arg == true)
         {
            %pause = 4;
            msay(0, 1234, *IDSTR_TR4_HNTR05, "TR04_HNTR05.WAV");
         }
         else
         {
            %pause = 5;
            msay(0, 1234, *IDSTR_TR4_HNTR07, "TR04_HNTR07.WAV");
         }
      }

      schedule("checkForSquadHalted("@%arg@");", 1 + %pause);
   }
}

function checkForCommandMenu(%arg)
{
   %pause = 0;

   if ($Client::InCommandMenu == %arg)
   {
      $iCheck = 0;
      msay(0, 1234, *IDSTR_TR4_HNTR05, "TR04_HNTR05.WAV");
      schedule("checkForSquadHalted(true);", 4);
   }
   else
   {
      $iCheck ++;

      if ($iCheck == 5)
      {
         %pause  = 4;
         $iCheck = 0;
         msay(0, 1234, *IDSTR_TR4_HNTR04, "TR04_HNTR04.WAV");
      }

      schedule("checkForCommandMenu("@%arg@");", 1 + %pause);
   }
}

function orderAttackTurrets()
{
   %pause = 0;

   // If either turret is un-destroyed ...
   if ($turret1Destroyed == false || $turret2Destroyed == false)
   {
      %pause = 5;

      // Tell them to engage the turrets
      msay(0, 1234, *IDSTR_TR4_HNTR31, "TR04_HNTR31.WAV");
   }

   // Otherwise, just wait for them to kill them
   schedule("checkForTurretsDestroyed();", 1 + %pause);
}

function checkForSquadOrderedToAttack()
{
   %pause = 0;

   if ($squadOrderedToAttack)
   {
      $iCheck = 0;

      if ($turret1Destroyed && $turret2Destroyed)
      {
         onTurretsDestroyed();
      }
      else
      {
         orderAttackTurrets();
      }
   }  
   else
   {
      if ($turret1Destroyed && $turret2Destroyed)
      {
         onMissionFailed(0, true);
      }
      else
      {
         $iCheck ++;

         if ($iCheck == 10)
         {
            %pause  = 8;
            $iCheck = 0;
            msay(0, 1234, *IDSTR_TR4_HNTR29, "TR04_HNTR29.WAV");
         }

         schedule("checkForSquadOrderedToAttack();", 1 + %pause);
      }
   }
}

function orderSquadMatesAttackTurret()
{
   $toldAboutTurrets = true;
   if ($squadOrderedToAttack == false)
   {
      // Tell them to sic their squadmates on the turrets ...
      msay(0, 1234, *IDSTR_TR4_HNTR29, "TR04_HNTR29.WAV");
      schedule("checkForSquadOrderedToAttack();", 8);
   }
   else
   {
      orderAttackTurrets();
   }
}

function onTurretTargeted()
{
   %pause = 0;

   if ($toldToWatchLead == false)
   {
      %pause           = 15;
      $toldToWatchLead = true;

      // Tell them to check range marker / lead indicator ...
      msay(0, 1234, *IDSTR_TR4_HNTR28, "TR04_HNTR28.WAV");
   }

   // Schedule check for squadmates attacking turret
   schedule("orderSquadMatesAttackTurret();", 1 + %pause);
}

function checkForTargetAcquired(%arg)
{
   %pause       = 0;
   %continue    = true;
   %curTargetID = getCurrentTargetID($playerId);

   if (%arg == $bravoId)
   {
      if (%curTargetID == getObjectId("MissionGroup/Targets/Nav Bravo/Turret1") ||
          %curTargetID == getObjectId("MissionGroup/Targets/Nav Bravo/Turret2"))
      {
         %continue     = false;
         $iTargetCheck = 0;

         onTurretTargeted();
      }
   }
   else if (%arg == $charlieId)
   {
      if (%curTargetId == getObjectId("MissionGroup/Targets/Nav Charlie/Bunker"))
      {
         %continue     = false;
         $iTargetCheck = 0;

         msay(0, 1234, *IDSTR_TR4_HNTR37, "TR04_HNTR37.WAV");
         schedule("checkForScan();", 4);
      }
   }

   if (%continue)
   {
      $iTargetCheck ++;

      if ($iTargetCheck == 5)
      {
         $iTargetCheck = 0;

         if (%arg == $bravoId)
         {
            %pause = 2;
            msay(0, 1234, *IDSTR_TR4_HNTR27, "TR04_HNTR27.WAV");
         }
         else if(%arg == $charlieId)
         {
            %pause = 5;
            msay(0, 1234, *IDSTR_TR4_HNTR36, "TR04_HNTR36.WAV");
         }
      }

      schedule("checkForTargetAcquired("@%arg@");", 1 + %pause);
   }
}

function startCharlieHercPatrol()
{
   order("MissionGroup/Targets/Nav Charlie", Formation, Wedge);
   order("MissionGroup/Targets/Nav Charlie", Guard, "MissionGroup/Paths/Nav Charlie");
}

function startDeltaHercPatrol()
{
   order("MissionGroup/Targets/Nav Delta", Guard, "MissionGroup/Paths/Nav Delta");
}

function evaluateTurretStatus()
{
   %target = getCurrentTargetID($playerId);

   // If they have a turret targeted, tell them about the lead indicator
   if (%target == getObjectId("MissionGroup/Targets/Nav Bravo/Turret1") ||
       %target == getObjectId("MissionGroup/Targets/Nav Bravo/Turret2"))
   {
      onTurretTargeted();
   }
   // Otherwise, tell them to target the turret
   else
   {
      msay(0, 1234, *IDSTR_TR4_HNTR27, "TR04_HNTR27.WAV");
      schedule("checkForTargetAcquired("@$bravoId@");", 2);
   }
}

function onNearingNavPoint(%arg)
{
   if (%arg == $alphaId)
   {
      msay(0, 1234, *IDSTR_TR4_HNTR04, "TR04_HNTR04.WAV");
      schedule("checkForCommandMenu(true);", 4);
   }
   else if (%arg == $bravoId)
   {
      // Tell them they can see the turrets
      msay(0, 1234, *IDSTR_TR4_HNTR26, "TR04_HNTR26.WAV");
      schedule("evaluateTurretStatus();", 8);
   }
   else if (%arg == $charlieId)
   {
      msay(0, 1234, *IDSTR_TR4_HNTR36, "TR04_HNTR36.WAV");
      schedule("checkForTargetAcquired("@$charlieId@");", 5);
   }
   else if (%arg == $deltaId)
   {
      if ($playedDeltaMessage == false)
      {
         $playedDeltaMessage = true;

         if (getCurrentTargetId($playerId) == getObjectId("MissionGroup/Targets/Nav Delta/BadAssHerc1"))
         {
            msay(0, 1234, *IDSTR_TR4_HNTR46, "TR04_HNTR46.WAV");
         }
         else
         {
            msay(0, 1234, *IDSTR_TR4_HNTR45, "TR04_HNTR45.WAV");
         }
      }
   }
}

function onArriveNavPoint(%arg)
{
   if (%arg == $alphaId)
   {
      $alphaComplete = true;

      //squawkEnabled(false);
      //onHalt();
      //squawkEnabled(true);
   }
   setNavMarker(%arg, false, -1);
}

function setNavDelta()
{
   $currentWayPoint = $deltaId;
   setNavMarker("MissionGroup/Nav Points/Nav Delta", true, -1);
   startDeltaHercPatrol();
   msay(0, 1234, *IDSTR_TR4_HNTR44, "TR04_HNTR44.WAV");
   schedule("monitorProgress("@$deltaId@", 0, 600, 50);", 2);
}

function setNavCharlie()
{
   $currentWayPoint = $charlieId;
   setNavMarker("MissionGroup/Nav Points/Nav Charlie", true, -1);
   startCharlieHercPatrol();
   msay(0, 1234, *IDSTR_TR4_HNTR35, "TR04_HNTR35.WAV");
   schedule("monitorProgress("@$charlieId@", 200, 0, 100);", 4);
}

function setNavBravo()
{
   $currentWayPoint = $bravoId;

   squawkEnabled(false);
   orderSquadMate($squadMate1Id, HoldFire, true);
   orderSquadMate($squadMate2Id, HoldFire, true);
   squawkEnabled(true);

   setNavMarker("MissionGroup/Nav Points/Nav Bravo", true, -1);
   msay(0, 1234, *IDSTR_TR4_HNTR25, "TR04_HNTR25.WAV");
   schedule("monitorProgress("@$bravoId@", 1400, 100, 50);", 3);
}

function setNavAlpha()
{
   $currentWayPoint = $alphaId;
   setNavMarker("MissionGroup/Nav Points/Nav Alpha", true, -1);
   msay(0, 1234, *IDSTR_TR4_HNTR03, "TR04_HNTR03.WAV");
   schedule("monitorProgress("@$alphaId@", 2000, 100, 50);", 3);
}

function initTrainer()
{
   squawkChatEnabled(false);

   $squadMate1Id = getObjectId("PlayerSquad/SquadMate1");
   $squadMate2Id = getObjectId("PlayerSquad/SquadMate2");

   msay(0, 1234, "", "TR_INTR01.WAV");
   schedule("msay(0, 1234, *IDSTR_TR4_HNTR01, \"TR04_HNTR01.WAV\");",  3);
   schedule("msay(0, 1234, *IDSTR_TR4_HNTR02, \"TR04_HNTR02.WAV\");", 15);

   schedule("setNavAlpha();", 40);
}

function vehicle::onAttacked(%this, %who)
{
   if (%this == $squadMate1Id && %who == $playerId)
   {
      if ($squadMate1Attacked >= 6)
      {
         if ($playedTip == false)
         {
            $playedTip = true;
            msay(0, 1234, *IDSTR_TRG_HNTRDP, "TR_HNTRDP.WAV");
         }

         $missionFailed = true;
         order($squadMate1Id, Attack, $playerId);
         order($squadMate2Id, Attack, $playerId);
      }
      else
      {
         $squadMate1Attacked ++;
      }
   }

   else if (%this == $squadMate2Id && %who == $playerId)
   {
      if ($squadMate2Attacked >= 6)
      {
         if ($playedTip == false)
         {
            $playedTip = true;
            msay(0, 1234, *IDSTR_TRG_HNTRDP, "TR_HNTRDP.WAV");
         }

         $missionFailed = true;
         order($squadMate1Id, Attack, $playerId);
         order($squadMate2Id, Attack, $playerId);
      }
      else
      {
         $squadMate2Attacked ++;
      }
   }
}

function attackPlayer()
{
   if ($playedDeltaMessage == false)
   {
      $playedDeltaMessage = true;
      onArriveNavPoint($deltaId);

      if (getCurrentTargetId($playerId) == getObjectId("MissionGroup/Targets/Nav Delta/BadAssHerc1"))
      {
         msay(0, 1234, *IDSTR_TR4_HNTR48, "TR04_HNTR48.WAV");
      }
      else
      {
         msay(0, 1234, *IDSTR_TR4_HNTR45, "TR04_HNTR45.WAV");
      }
      order("MissionGroup/Targets/Nav Delta/BadAssHerc1", Attack, $playerId);
   }
}

function BadAssHerc1::vehicle::onDestroyed(%this, %who)
{
   if ($currentWayPoint == $deltaId)
   {
      onMissionCompleted(*IDSTR_TR4_HNTR46, "TR04_HNTR46.WAV");
   }
   else
   {
      onMissionFailed(0, true);
   }
}

function BadAssHerc1::vehicle::onTargeted(%this, %who)
{
   attackPlayer();
}

function BadAssHerc1::vehicle::onAttacked(%this, %who)
{
   attackPlayer();
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

function turret1::turret::onDestroyed(%this, %who)
{
   $turret1Destroyed = true;

   if ($currentWayPoint != $bravoId || $toldAboutTurrets == false)
   {
      onMissionFailed(0, true);
   }
}

function turret2::turret::onDestroyed(%this, %who)
{
   $turret2Destroyed = true;

   if ($currentWayPoint != $bravoId || $toldAboutTurrets == false)
   {
      onMissionFailed(0, true);
   }
}

function turret::onAttacked(%this, %who)
{
   // Kind of a loophole - the squadmate may start to attack by his/her self,
   // and it is a resonable response to not order them
   if (%who != $playerId)
   {
      $squadOrderedToAttack = true;
   }
}

function bunker::structure::onScan(%scanned, %scanner, %string)
{
   $bunkerScanned = true;

   if ($currentWayPoint != $charlieId)
   {
      onMissionFailed(0, true);
   }
}

function onHalt()
{
   $squadHalted = true;
   orderSquadMate($Order::Recipient, Clear, True );
   orderSquadMate($Order::Recipient, HoldPosition, True );
   orderSquadMate($Order::Recipient, Acknowledge, True );
}

function onJoinOnMe()
{
   $squadHalted = false;
   orderSquadMate($Order::Recipient, Formation);
   orderSquadMate($Order::Recipient, HoldPosition, True);
}

function onAttackMyTarget()
{
   if ($currentWayPoint == $bravoId)
   {
      $squadOrderedToAttack = true;

      //%curTargetID = getCurrentTargetID($playerId);

      //if (%curTargetID == getObjectId("MissionGroup/Targets/Nav Bravo/Turret1"))
      //{
      //   $squadOrderedToAttack1 = true;
      //}
      //if (%curTargetID == getObjectId("MissionGroup/Targets/Nav Bravo/Turret2"))
      //{
      //   $squadOrderedToAttack2 = true;
      //}
   }
   orderSquadMate($Order::Recipient, Attack, $Order::Target);
   orderSquadMate($Order::Recipient, HoldPosition, True);
}

function vehicle::onDestroyed(%this, %who)
{
   // If player killed or player killed a squadmate, terminate
   if(%this == $playerId || 
     (%who  == $playerId && (%this == $squadMate1Id || %this == $squadMate2Id)))
   {
	   $missionFailed = true;
	   $Client::TrainingMissionStatus = "ONE_OR_MORE_FAILED";

      if (%this == $playerId)
      {
	      onForceToDebrief(*IDSTR_MISSION_FAILED);
      }
      else
      {
         onForceToDebrief(*IDSTR_TRG_FOLLOWORDERS);
      }
   }
}
