exec("training_fns.cs");

DropPoint drop1
{
   name = "Drop Point 1";
   desc = "Drop Point 1";
};

function onArriveNavPoint(%arg)
{
   if (%arg == $alphaId)
   {
      setNavBravo();
   }
   else
   {
		onMissionCompleted(*IDSTR_TR1_HNTR15, "TR01_HNTR15.WAV");
   }

   setNavMarker(%arg, false, -1);
}

function onNearingNavPoint(%arg)
{
   if (%arg == $alphaId)
   {
		msay(0, 1234, *IDSTR_TR1_HNTR13, "TR01_HNTR13.WAV");
   }
}

function setNavBravo()
{
	setNavMarker($bravoId, true, -1);
	msay(0, 1234, *IDSTR_TR1_HNTR14, "TR01_HNTR14.WAV");

	schedule("monitorProgress("@$bravoId@", 0, 50, 50);", 13);
   ensureNavPointSet($bravoId);
}

function setNavAlpha()
{
   setNavMarker($alphaId, true, -1);
   msay(0, 1234, *IDSTR_TR1_HNTR10, "TR01_HNTR10.WAV");

   schedule("msay(0, 1234, *IDSTR_TR1_HNTR11, \"TR01_HNTR11.WAV\");", 20);
	schedule("msay(0, 1234, *IDSTR_TR1_HNTR12, \"TR01_HNTR12.WAV\");", 35);

	schedule("monitorProgress("@$alphaId@", 600, 50, 50);", 45);
   ensureNavPointSet($alphaId);
}

function initTrainer()
{
   msay(0, 1234, "", "TR01_INTR01.WAV");

	schedule("msay(0, 1234, *IDSTR_TR1_HNTR01, \"TR01_HNTR01.WAV\");",   8);
   schedule("msay(0, 1234, *IDSTR_TR1_HNTR02, \"TR01_HNTR02.WAV\");",  28);
   schedule("msay(0, 1234, *IDSTR_TR1_HNTR03, \"TR01_HNTR03.WAV\");",  38);
   schedule("msay(0, 1234, *IDSTR_TR1_HNTR04, \"TR01_HNTR04.WAV\");",  46);
   schedule("msay(0, 1234, *IDSTR_TR1_HNTR06, \"TR01_HNTR06.WAV\");",  61);
   schedule("msay(0, 1234, *IDSTR_TR1_HNTR07, \"TR01_HNTR07.WAV\");",  74);
   schedule("msay(0, 1234, *IDSTR_TR1_HNTR08, \"TR01_HNTR08.WAV\");",  80);
   schedule("msay(0, 1234, *IDSTR_TR1_HNTR09, \"TR01_HNTR09.WAV\");", 106);

   schedule("setNavAlpha();", 115);
}
