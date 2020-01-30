$iWaitTick         = 0;
$iCheck            = 0;
$customLocation    = "0";
$fInputRecorded    = false;
$Client::InMapView = false;

exec("training_fns.cs");

DropPoint drop1
{
   name = "Drop Point 1";
   desc = "Drop Point 1";
};

function dummy()
{
}

function lockCustom(%arg)
{
   if (%arg == true)
   {
      bind(keyboard, make, m, TO, "dummy();");
   }
   else
   {
      unbind(keyboard, make, m);
   }
}

function lockMap(%arg)
{
   if (%arg == true)
   {
      //bind(keyboard, make, ESCAPE);
   }
   else
   {
      //bind(keyboard, make, ESCAPE, to, "dummy();");
   }
}

function awaitInputOrTimeout()
{
   if ($fInputRecorded == true || $iWaitTick == 60)
   {
   	msay(0, 1234, "", "TR02_HNTR10.WAV");
   	schedule("createCustomNavPoint("@customId@");", 10);
   }
   else
   {
      $iWaitTick ++;
      schedule("awaitInputOrTimeout();", 1);
   }
}

function skipWait()
{
   $fInputRecorded = true;
   unbind(keyboard, make, space);
}

function playWithMap()
{
   bind(keyboard, make, space, TO, "skipWait();");
	msay(0, 1234, *IDSTR_TR2_HNTR09, "TR02_HNTR09.WAV");
	awaitInputOrTimeout();
}

function explainMap()
{
   stopPlayerVehicle($playerId);
	msay(0, 1234, "", "TR02_HNTR08.WAV");
   schedule("playWithMap();", 48);
}

function checkForMapUp(%arg)
{
	if ($Client::InMapView == %arg)
	{
		if (%arg)
      {
         lockMap(true);
			explainMap();
      }
		else
      {
         setNavCustom();
      }
	}
	else
	{
		$iCheck ++;

		if ($iCheck == 10)
		{
			$iCheck = 0;

			if (%arg)
				enterMap();
			else
				exitMap();
		}
		else
		{
         schedule("checkForMapUp("@%arg@");", 1);
		}
	}
}

function enterMap()
{
   if ($Client::InMapView)
   {
      explainMap();
   }
   else
   {
   	msay(0, 1234, *IDSTR_TR2_HNTR07, "TR02_HNTR07.WAV");
	   schedule("checkForMapUp(true);", 4);
   }
}

function exitMap()
{
   if ($Client::InMapView)
   {
	   msay(0, 1234, "", "TR02_HNTR13.WAV");
	   schedule("checkForMapUp(false);", 3);
   }
   else
   {
      setNavCustom();
   }
}

function onArriveNavPoint(%arg)
{
   if (%arg == $alphaId)
   {
      stopPlayerVehicle($playerId);
      setNavMarker($alphaId, false, -1);

	   msay(0, 1234, *IDSTR_TR2_HNTR06, "TR02_HNTR06.WAV");
	   schedule("enterMap();", 3);
   }
   else if (%arg == $bravoId)
   {
      setNavMarker($bravoId, false, -1);
	   onMissionCompleted(*IDSTR_TR2_HNTR17, "TR02_HNTR17.WAV");
   }
   else if (%arg == $customId)
   {
      lockCustom(false);
      setNavMarker($alphaId, true, 0);
      setNavMarker($bravoId, true, 0);

	   msay(0, 1234, *IDSTR_TR2_HNTR15, "TR02_HNTR15.WAV");
	   schedule("setNavBravo();", 14);
   }
}

function setNavCustom()
{
	msay(0, 1234, *IDSTR_TR2_HNTR14, "TR02_HNTR14.WAV");
   schedule("monitorProgress("@$customId@", 0, 50, 50);", 10);
}

function setNavBravo()
{
	msay(0, 1234, *IDSTR_TR2_HNTR16, "TR02_HNTR16.WAV"); 
	schedule("monitorProgress("@$bravoId@", 0, 50, 50);", 8);
}

function setNavAlpha()
{
   setNavMarker($alphaId, true, -1);

	msay(0, 1234, *IDSTR_TR2_HNTR05, "TR02_HNTR05.WAV");
	schedule("monitorProgress("@$alphaId@", 0, 50, 50);", 4);
   ensureNavPointSet($alphaId);
}

function checkForCustomNavPoint(%arg)
{
	if (getVehicleNavMarkerLocation($playerId) != $customLocation)
	{
		$iCheck         = 0;
		$customLocation = getVehicleNavMarkerLocation($playerId);

	   if (%arg == $alphaId)
	   {
         lockCustom(true);
		   setNavAlpha();
	   }
	   else
	   {
         %dist = dist($customLocation, 
                 getPosition(getObjectId("MissionGroup/Turrets/Northern Defensive Turret")));

         if (%dist >= 300.0)
         {
            msay(0, 1234, "", "TR02_HNTR18.WAV");
            schedule("checkForCustomNavPoint("@%arg@");", 4);
         }
         else
         {
            lockMap(false);
            msay(0, 1234, "", "TR02_HNTR12.WAV");
		      schedule("exitMap();", 7);
         }
	   }
	}
	else
	{
		$iCheck ++;

		if ($iCheck >= 10)
		{
			$iCheck = 0;
         createCustomNavPoint(%arg);
		}
		else
		{
         schedule("checkForCustomNavPoint("@%arg@");", 1);
		}
	}
}

function createCustomNavPoint(%arg)
{
   $iCheck = 0;

   if (%arg == $alphaId)
      msay(0, 1234, *IDSTR_TR2_HNTR04, "TR02_HNTR04.WAV");
   else
	   msay(0, 1234, "", "TR02_HNTR11.WAV");

   schedule("checkForCustomNavPoint("@%arg@");", 10);
}

function initTrainer()
{
	$customLocation = getVehicleNavMarkerLocation($playerId);

   msay(0, 1234, "", "TR_INTR01.WAV");

	schedule("msay(0, 1234, *IDSTR_TR2_HNTR01, \"TR02_HNTR01.WAV\");", 4);
	schedule("msay(0, 1234, *IDSTR_TR2_HNTR02, \"TR02_HNTR02.WAV\");", 15);
   schedule("msay(0, 1234, *IDSTR_TR2_HNTR03, \"TR02_HNTR03.WAV\");", 27);
	
   schedule("createCustomNavPoint("@$alphaId@");", 35);
}
