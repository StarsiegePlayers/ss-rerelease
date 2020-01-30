$LEVEL = 1;
$muteComputer = true;
function win()
{  
	missionObjective1.status = *IDSTR_OBJ_IGNORE;
	missionObjective2.status = *IDSTR_OBJ_IGNORE;
	missionObjective3.status = *IDSTR_OBJ_IGNORE;
	missionObjective4.status = *IDSTR_OBJ_IGNORE;
	missionObjective5.status = *IDSTR_OBJ_IGNORE;
	missionObjective6.status = *IDSTR_OBJ_IGNORE;
	missionObjective7.status = *IDSTR_OBJ_COMPLETED;
	missionObjective8.status = *IDSTR_OBJ_COMPLETED;
	missionObjective9.status = *IDSTR_OBJ_COMPLETED;
	updatePlanetInventory(he1);
   schedule("forceToDebrief();", 3.0);
}
///////////////////////////////////////////////////////////////////////////////
// HE2                                                                       //
//                                                                           //
// Primary Objectives                                                        //
// 7. Destroy all Cybrid Resistance                                          //
// 8. Protect Harabec                                                        //
///////////////////////////////////////////////////////////////////////////////

MissionBriefInfo missionBriefInfo
{
   campaign             = *IDSTR_HE1_CAMPAIGN;             
   title                = *IDSTR_HE1_TITLE;      
   planet               = *IDSTR_PLANET_PLUTO;     
   location             = *IDSTR_HE1_LOCATION; 
   dateOnMissionEnd     = *IDSTR_HE1_DATE;               
   shortDesc            = *IDSTR_HE1_SHORTBRIEF;
   longDescRichText     = *IDSTR_HE1_LONGBRIEF;
   media                = *IDSTR_HE1_MEDIA;
   successDescRichText  = *IDSTR_HE1_DEBRIEF_SUCC;
   failDescRichText     = *IDSTR_HE1_DEBRIEF_FAIL;
   nextMission          = *IDSTR_HE1_NEXTMISSION;
   scrambleNextMission  = true;
   soundVol             = "HE1.vol";
   successWavFile       = "HE1_Debriefing.wav";
};

MissionBriefObjective missionObjective1         
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HE1_OBJ1_SHORT;
   longTxt              = *IDSTR_HE1_OBJ1_LONG;
   bmpName              = *IDSTR_HE1_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective2
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HE1_OBJ2_SHORT;
   longTxt              = *IDSTR_HE1_OBJ2_LONG;
   bmpName              = *IDSTR_HE1_OBJ2_BMPNAME;
};

MissionBriefObjective missionObjective3
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HE1_OBJ3_SHORT;
   longTxt              = *IDSTR_HE1_OBJ3_LONG;
   bmpName              = *IDSTR_HE1_OBJ3_BMPNAME;
};

MissionBriefObjective missionObjective4
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HE1_OBJ4_SHORT;
   longTxt              = *IDSTR_HE1_OBJ4_LONG;
   bmpName              = *IDSTR_HE1_OBJ4_BMPNAME;
};

MissionBriefObjective missionObjective5
{
   isPrimary            = false;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HE1_OBJ5_SHORT;
   longTxt              = *IDSTR_HE1_OBJ5_LONG;
   bmpName              = *IDSTR_HE1_OBJ5_BMPNAME;
};


MissionBriefObjective missionObjective6
{
   isPrimary            = false;
   status               = *IDSTR_OBJ_ACTIVE;
   shortTxt             = *IDSTR_HE1_OBJ6_SHORT;
   longTxt              = *IDSTR_HE1_OBJ6_LONG;
   bmpName              = *IDSTR_HE1_OBJ6_BMPNAME;
};

MissionBriefObjective missionObjective7
{
   isPrimary            = false;
   status               = *IDSTR_OBJ_IGNORE;
   shortTxt             = *IDSTR_HE1_OBJ7_SHORT;
   longTxt              = *IDSTR_HE1_OBJ7_LONG;
   bmpName              = *IDSTR_HE1_OBJ7_BMPNAME;
};

MissionBriefObjective missionObjective8
{
   isPrimary            = true;
   status               = *IDSTR_OBJ_IGNORE;
   shortTxt             = *IDSTR_HE1_OBJ8_SHORT;
   longTxt              = *IDSTR_HE1_OBJ8_LONG;
   bmpName              = *IDSTR_HE1_OBJ8_BMPNAME;
};

MissionBriefObjective missionObjective9
{
   isPrimary            = false;
   status               = *IDSTR_OBJ_IGNORE;
   shortTxt             = *IDSTR_HE1_OBJ9_SHORT;
   longTxt              = *IDSTR_HE1_OBJ9_LONG;
   bmpName              = *IDSTR_HE1_OBJ9_BMPNAME;
};

//-----------------------------------------------------------------------------
function player::onAdd(%playerNum)
{
   $playerNum = %playerNum;
}
//-----------------------------------------------------------------------------
function vehicle::onAdd(%this)
{
   if( %this == playerManager::playerNumToVehicleId($playerNum) )
      schedule("fadeEvent( 0, in, 100.0, 0, 0, 0 );", 0.2);
}
//-----------------------------------------------------------------------------
function onMissionStart(%this)
{
   dbecho($LEVEL, "onMissionStart(" @ %this @ ")");
   
   setHostile(*IDSTR_TEAM_RED, *IDSTR_TEAM_YELLOW);
   setHostile(*IDSTR_TEAM_YELLOW, *IDSTR_TEAM_RED);
   setHostile(*IDSTR_TEAM_PURPLE);

	cdAudioCycle(12, 13);

   $missionSuccess   = false;
   $missionFailed    = false;
   $missionStart     = false;
   $BOOL             = false;
   $PI               = 3.14;
   $boundFail        = false;
      
}
//-----------------------------------------------------------------------------

function onSPClientInit()
{
   initPlayer();
   initCinematic();
}
//-----------------------------------------------------------------------------
function init()
{
   dbecho($LEVEL, "init()");

   initFormations();
   initNavPoints();
   initCybrids();
   initCaanon();
   initMission();
}

///////////////////////////////////////////////////////////////////////////////
// INIT Functions                                                            //
///////////////////////////////////////////////////////////////////////////////
//-----------------------------------------------------------------------------
function initFormations()
{
   dbecho ($LEVEL, "initFormations()");
 
   newFormation(Wedge,     0,  0,  0,
                         -40,-40,  0,
                          40,-40,  0);
   
                           
   newFormation(Peace,     0,  0,  0,
                           0, 80,  0,  
                         -80,-80,  0,
                          80,-80,  0);
}
///////////////////////////////////////////////////////////////////////////////
// Player Functions                                                          //
///////////////////////////////////////////////////////////////////////////////
function initPlayer()
{
   dbecho($LEVEL, "initPlayer()");
   $playerId         = playerManager::playerNumToVehicleId($playerNum);
   
   $playerSquad      = myGetObjectId("playerSquad");
   $playerSquad.start   = 0;
   $playerSquad.num     = 0;

   $mate1            = myGetObjectId("playerSquad\\squadMate1");
   $mate2            = myGetObjectId("playerSquad\\squadMate2");
   $mate3            = myGetObjectId("playerSquad\\squadMate3");

   if( $mate1 != "" && $mate1 != 0 )
   {
      $playerSquad.start++;   
      $mate1.drop       = myGetObjectId("MissionGroup\\DropPoints\\Squad\\1");
      $mate1.destroyed  = false;
      placeHerc($mate1, $mate1.drop);
      haltHerc($mate1);
   }
   if( $mate2 != "" && $mate2 != 0 )
   {
      $playerSquad.start++;   
      $mate2.drop       = myGetObjectId("MissionGroup\\DropPoints\\Squad\\2");
      $mate2.destroyed  = false;
      placeHerc($mate2, $mate2.drop);
      haltHerc($mate2);
   }
   if( $mate3 != "" && $mate3 != 0 )
   {
      $playerSquad.start++;   
      $mate3.drop       = myGetObjectId("MissionGroup\\DropPoints\\Squad\\3");
      $mate3.destroyed  = false;
      placeHerc($mate3, $mate3.drop);
      haltHerc($mate3);
   }

   $playerSquad.num = $playerSquad.start;
   
}
//-----------------------------------------------------------------------------
function vehicle::onDestroyed(%this, %who)
{
   if( getGroup(%this) != $playerSquad )
      return;

   dbecho($LEVEL, "vehicle::onDestroyed(" @ %this @ ", " @ %who @ ")");
   
   $playerSquad.num--;
   %this.destroyed = true;
}
//-----------------------------------------------------------------------------
function placeHerc(%herc, %point)
{
   dbecho($LEVEL, "placeHerc(" @ %herc @ ", " @ %point @ ")");
   
   %x = getPosition(%point, x);
   %y = getPosition(%point, y);
   %z = getTerrainHeight(%x, %y);
   
   setPosition(%herc, %x, %y, %z);
}

//-----------------------------------------------------------------------------
function haltHerc(%herc)
{
   order(%herc, Clear, true);
   order(%herc, HoldPosition, true);
   order(%herc, HoldFire, true);
}

//-----------------------------------------------------------------------------
function startHerc(%herc)
{
   order(%herc, HoldPosition, false);
   order(%herc, HoldFire,     false);
}
   
   
   
//-----------------------------------------------------------------------------
function isPlayerSafe()
{
   dbecho($LEVEL, "isPlayerSafe()");
   
   if( IsSafe(*IDSTR_TEAM_YELLOW, $playerId, 1000) && 
       IsSafe(*IDSTR_TEAM_YELLOW, $caanon,   1000)   )
   {
      schedule("objectiveCompleted(8);", 10.0);
   }
   else
   {
      schedule("isPlayerSafe();", 1.0);
   }
}
///////////////////////////////////////////////////////////////////////////////
// Cybrid Funtions                                                           //
///////////////////////////////////////////////////////////////////////////////

function initCybrids()
{
   dbecho($LEVEL, "initCybrids()");
   
   $cybridNum  = 12;

   $alpha   = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Alpha");
   $bravo   = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Bravo");
   $charlie = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Charlie");
   $delta   = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Delta");
   $goad    = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Goad");
   $cin     = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Cin");
   
   $exec    = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Delta\\EXECUTIONER");

   $cin1    = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Cin\\Seeker1");
   $cin2    = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Cin\\Seeker2");
   $cin3    = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Cin\\Seeker3");
   
   $cin1.point = myGetObjectId("MissionGroup\\DropPoints\\Cin\\1");
   $cin2.point = myGetObjectId("MissionGroup\\DropPoints\\Cin\\2");
   $cin3.point = myGetObjectId("MissionGroup\\DropPoints\\Cin\\3");
   
   $charlie.drop  = myGetObjectId("MissionGroup\\DropPoints\\Charlie");
   $delta.drop    = myGetObjectId("MissionGroup\\DropPoints\\Delta");
   $delta.num     = 3;
   $exec.drop     = myGetObjectId("MissionGroup\\KillCaanon");

   $goad.lead     = myGetObjectId("MissionGroup\\vehicles\\CYBRID\\Goad\\GOAD");
   order($goad.lead   , MakeLeader, true);
   order($goad,   Formation,  Wedge);
  
   order($goad,      HoldPosition, $BOOL);
   order($cin,       HoldPosition, $BOOL);

   order($goad,    HoldFire, $BOOL );
   order($cin,     HoldFire, $BOOL );
   
   order($goad,    ShutDown, true);
   order($cin,     ShutDown, true);
   
}

//-----------------------------------------------------------------------------

function dropCybrid(%group, %point )
{
   dbecho($LEVEL, "dropCybrid(" @ %group @ ", " @ %point @ ")");
   
   if(%point == 0 || %point == "" )
   {
      %cybrid     = GetNextObject(%group, 0);
      %delay      = 0;
   
      while( %cybrid != 0 )
      {
         echo("will place " @ getObjectName(%cybrid) @ " in " @ (%delay * 15) @ " sec.");
         
         schedule("calcAndDrop(" @ %cybrid @ ");", (%delay * 15) );
         schedule("order(" @ %cybrid @ ",    ShutDown,   false);",   (%delay * 15) );
         schedule("order(" @ %cybrid @ ",    Cloak,      true);",    (%delay * 15) );
         
         %cybrid     = GetNextObject(%group, %cybrid);
         %delay++;
      }  
   }
   else
   {
      %cybrid     = GetNextObject(%group,    0);
      %xpoint     = GetNextObject(%point,   0);
      %delay      = 0;
      
      while( %xpoint != 0 && %cybrid != 0 )
      {

         echo("will place " @ getObjectName(%cybrid) @ " in " @ (%delay * 15) @ " sec.");

         %x = getPosition(%xpoint, x);
         %y = getPosition(%xpoint, y);
         %z = getTerrainHeight(%x, %y);
         
         schedule("setPosition(" @ %cybrid @ ", " @ %x @ ", " @ %y @ ", " @ %z @ ");", (%delay * 15) ); 
         schedule("order(" @ %cybrid @ ",    ShutDown,   false);",   (%delay * 15) );
         schedule("order(" @ %cybrid @ ",    Cloak,      true);",    (%delay * 15) );
         
         %cybrid  = GetNextObject(%group, %cybrid);
         %xpoint  = GetNextObject(%point, %xpoint);
         %delay++;
      }
   }
}

function calcAndDrop(%cybrid)
{
   dbecho($LEVEL, "calcAndDrop(" @ %cybrid @ ")");
   
   %offset = randomInt(-350, 350);
   
   if(%cybrid != 0)
   {
      %x      = getPosition($playerId, x);
      %y      = getPosition($playerId, y);
      %z      = getPosition($playerId, z);
      %rot    = getPosition($playerId, r);
      
      if(     %rot >=   -($PI/4)    &&      %rot <    ($PI/4)   )
      {
          %y = %y + 500;
          %x = %x + %offset;
      }
      else
      if(     %rot >=    ($PI/4)    &&      %rot <  3*($PI/4)   )
      {
          %x = %x - 500;
          %y = %y + %offset;
      }
      else
      if(     %rot >=  3*($PI/4)    ||      %rot < -3*($PI/4)   )
      {
          %y = %y - 500;
          %x = %x + %offset;
      }
      else
      if(     %rot >= -3*($PI/4)    &&      %rot <   -($PI/4)   )
      {
          %x = %x + 500;
          %y = %y + %offset;
      }
      
      setPosition(%cybrid, %x, %y, getTerrainHeight(%x, %y) );
      
   }
}

//-----------------------------------------------------------------------------
function attackPlayer(%group)
{
   dbecho($LEVEL, "attackPlayer(" @ %group @ ")");
  
   %cybrid     = GetNextObject(%group, 0);
   %delay      = 0;

   while( %cybrid != 0 )
   {
      echo("will order " @ getObjectName(%cybrid) @ " to attack " @ (%delay * 15) @ " sec.");
      
      order(%cybrid, Attack, pick(playerSquad) );  
      %cybrid = GetNextObject(%group, %cybrid);
      %delay++;
   }  
}
//-----------------------------------------------------------------------------

function attackCaanon(%group)
{
   dbecho($LEVEL, "attackCaanon(" @ %group @ ")");
  
   %cybrid     = GetNextObject(%group, 0);
   %delay      = 0;

   while( %cybrid != 0 )
   {
      order(%cybrid, Attack, $caanon );
      updateOrder(%cybrid);
        
      %cybrid = GetNextObject(%group, %cybrid);
      %delay++;
   }  
}

function updateOrder(%cybrid)
{
   %target = "";
   
   if( getDistance(%cybrid, $playerId) < 400 &&
       getDistance(%cybrid, $playerId) < getDistance(%cybrid, $caanon) )
         %target = $playerId;
   else if( $mate1 != "" && $mate1 != 0  && !($mate1.destroyed)      && 
            getDistance(%cybrid, $mate1) < 400                       &&
            getDistance(%cybrid, $mate1) < getDistance(%cybrid, $caanon) )
               %target = $mate1;
   else if( $mate2 != "" && $mate2 != 0  && !($mate2.destroyed)      &&
            getDistance(%cybrid, $mate2) < 400                       &&
            getDistance(%cybrid, $mate2) < getDistance(%cybrid, $caanon) )
               %target = $mate2;
   else if( $mate3 != "" && $mate3 != 0  && !($mate3.destroyed)      &&
            getDistance(%cybrid, $mate3) < 400                       &&
            getDistance(%cybrid, $mate3) < getDistance(%cybrid, $caanon) )
               %target = $mate3;
      
   if(%target != "")
      order(%cybrid, Attack, %target);
   else
      schedule("updateOrder(" @ %cybrid @ ");", 1.0);
}
      
//-----------------------------------------------------------------------------

function cybrid::vehicle::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "cybrid::vehicle::onDestroyed(" @ %this @ ", " @ %who @ ")");
                                           
   if (getGroup(%this) == $delta)
      $delta.num--;
  
   if ( $delta.num == 0 )
   {
      $delta.num--;
      isPlayerSafe();
   }
   
   $cybridNum--;
}

//////////////////////////////////////////////////////////////////////////////////
// Caanon Functions                                                             //
//////////////////////////////////////////////////////////////////////////////////

function initCaanon()
{
   dbecho($LEVEL, "initCaanon()");
   
   $caanon  = myGetObjectId ("MissionGroup\\Caanon\\Caanon");

   $XXLongEntered    = false;
   $XLongEntered     = false;
   $LongEntered      = false;
   $MediumEntered    = false;
   $ShortEntered     = false;
   
   
   checkDistance($playerId, $caanon, 2700, onEnterCaanonExtraExtraLong, 5.0); 
   checkDistance($playerId, $caanon, 2430, onEnterCaanonExtraLong, 5.0); 
   checkDistance($playerId, $caanon, 1840, onEnterCaanonLong, 5.0); 
   checkDistance($playerId, $caanon, 1100, onEnterCaanonMedium, 5.0); 
   checkDistance($playerId, $caanon,  400, onEnterCaanonShort, 5.0);
   
   checkBound($playerId, $caanon, 6000, "missionBoundFail", 5.0); 
    
}

//-----------------------------------------------------------------------------

function caanon::structure::onAttacked(%this, %who)
{
   if($boundFail)
      damageObject(%this, 3000);
}

//-----------------------------------------------------------------------------
function caanon::structure::onDestroyed(%this, %who)
{
   dbecho($LEVEL, "caanon::structure::onDestroyed(" @ %this @ ", " @ %who @ ")");

   if( !($boundFail) )
      setDominantCamera(%who, %this);
   
   killChannel($caanon);
   missionFailed(caanonDied);
}

//-----------------------------------------------------------------------------
function onEnterCaanonExtraExtraLong()
{
   if($XXLongEntered)
      return;
   
   dbecho($LEVEL, "onEnterCaanonExtraExtraLong()");
   
   dropCybrid($alpha);
   attackPlayer($alpha);
   
   $XXLongEntered = true;   
   schedule("onEnterCaanonExtraLong();", 600.0);
}

//-----------------------------------------------------------------------------
function onEnterCaanonExtraLong()
{
   if($XLongEntered)
      return;
 
   dbecho($LEVEL, "onEnterCaanonExtraLong()");
 
   $XLongEntered = true;   
   schedule("onEnterCaanonLong();", 600.0);
}

//-----------------------------------------------------------------------------
function onEnterCaanonLong()
{
   if($LongEntered)
      return;

   dbecho($LEVEL, "onEnterCaanonLong()");

   $LongEntered = true;   
 
   say(0, $caanon, *IDSTR_HE1_CAA02, "HE1_CAA02.wav");
   schedule("onEnterCaanonMedium();", 600.0);    
}

//-----------------------------------------------------------------------------
function onEnterCaanonMedium()
{
   if($MediumEntered)
      return;

   dbecho($LEVEL, "onEnterCaanonMedium()");

   $MediumEntered = true;   
   
   dropCybrid($bravo);
   attackPlayer($bravo);
   
   schedule("onEnterCaanonShort();", 600.0);    
}
//-----------------------------------------------------------------------------
function onEnterCaanonShort()
{
   if($ShortEntered)
      return;

   dbecho($LEVEL, "onEnterCaanonShort()");

   $ShortEntered = true;
   
   dropCybrid($charlie, $charlie.drop);
   attackCaanon($charlie);   
   schedule("dropCybrid(" @ $delta @ ", " @ $delta @ ".drop);", 30); 
   schedule("attackCaanon(" @ $delta @ ");", 30);
 
   say(0, $caanon, *IDSTR_HE1_CAA03, "HE1_CAA03.wav");
   setNavMarker($navBogus, false, -1);

}

////////////////////////////////////////////////////////////////////////////////
// Nav Point Functions                                                        //
////////////////////////////////////////////////////////////////////////////////
function initNavPoints()
{
   dbecho($LEVEL, "initNavPoints()");
   
   $navCaanon  = myGetObjectId("MissionGroup\\NavPoints\\NavCaanon");
   $navBogus   = myGetObjectId("MissionGroup\\NavPoints\\Bogus");

   setNavMarker($navCaanon, false);
   setNavMarker($navBogus, false);
}

////////////////////////////////////////////////////////////////////////////////
// Mission Functions                                                          //
////////////////////////////////////////////////////////////////////////////////

function initMission()
{
   dbecho($LEVEL, "initMission()");
   
   $missionStart = true;
                                    
   plutoSounds();   
   moveCinematicShapes();
                     
   order($goad,      Shutdown,      false);
   order($goad,      HoldPosition,  false);
   order($goad,      HoldFire,      false);
   order($goad.lead, Guard,         $playerId);
   
   schedule("startSquadMates();", 12);
   
   schedule("heresCaanon(1);", 10);   
   schedule("heresCaanon(2);", 50);

   schedule("onEnterCaanonExtraExtraLong();", 600.0);
}

function  moveCinematicShapes()
{
   dbecho($LEVEL, "moveCinematicShapes()");

   %shapes  = myGetObjectId("MissionGroup\\CinSpace");
   %loc     = myGetObjectId("MissionGroup\\CinMarker");

   setPosition(   %shapes, getPosition(%loc, x),
                           getPosition(%loc, y),
                           getPosition(%loc, z) );
}

//-----------------------------------------------------------------------------
function updateMissionObjectives()
{
   missionObjective1.status = *IDSTR_OBJ_IGNORE;
   missionObjective2.status = *IDSTR_OBJ_IGNORE;
   missionObjective3.status = *IDSTR_OBJ_IGNORE;
   missionObjective4.status = *IDSTR_OBJ_IGNORE; 
   missionObjective5.status = *IDSTR_OBJ_IGNORE;
   missionObjective6.status = *IDSTR_OBJ_IGNORE;
   
   missionObjective7.status = *IDSTR_OBJ_ACTIVE;
   missionObjective8.status = *IDSTR_OBJ_ACTIVE;
   
   if ( $playerSquad.start > 0 )
      missionObjective9.status = *IDSTR_OBJ_ACTIVE;

   say(0, $caanon, "Mission_obj_new.wav");
}
//-----------------------------------------------------------------------------
function heresCaanon(%num)
{
   dbecho($LEVEL, "heresCaanon(" @ %num @ ")");
   
   if(%num == 1)
   {
      say(0, $caanon, *IDSTR_GEN_STATIC01, "GEN_STATIC01.WAV");
   }
   if(%num == 2)
   {
      say(0, $caanon, *IDSTR_HE1_CAA01, "HE1_CAA01.wav");
      setNavMarker($navCaanon, true, -1);
      updateMissionObjectives();
   }
}

//-----------------------------------------------------------------------------
function startSquadMates()
{
   if( $mate1 != "" && $mate1 != 0 )
      startHerc($mate1);   
   if( $mate2 != "" && $mate2 != 0 )
      startHerc($mate2);   
   if( $mate3 != "" && $mate3 != 0 )
      startHerc($mate3);   

   order($cin, ShutDown,   false);

   placeHerc($cin1, $cin1.point);
   placeHerc($cin2, $cin2.point);
   placeHerc($cin3, $cin3.point);
}
   
///////////////////////////////////////////////////////////////////////////////
// Mission Completion Functions                                              //
///////////////////////////////////////////////////////////////////////////////

//-----------------------------------------------------------------------------
function objectiveCompleted(%objective)
{  
   dbecho($LEVEL, "objectiveCompleted(" @ %objective @ ")");
   
   if($missionFailed )
      return;
   if(%objective == 8)
   {
      missionObjective8.status = *IDSTR_OBJ_COMPLETED;
      
      if( $cybridNum <=0 )
         missionObjective7.status = *IDSTR_OBJ_COMPLETED;
      
      if( cardinalSpearUnited() && $playerSquad.start > 0 )
         missionObjective9.status = *IDSTR_OBJ_COMPLETED;
      else if ( $playerSquad.start > 0 )
         missionObjective9.status = *IDSTR_OBJ_FAILED;

      say(0, $playerId, "GEN_OC01.wav");
      schedule("say(0," @ $caanon @ ", *IDSTR_HE1_CAA04, \"HE1_CAA04.wav\");", 3.0);
      schedule("missionSuccess();", 14.0);                                   
   }
}


//-----------------------------------------------------------------------------
function missionSuccess()
{
   if($missionFailed)
     return;

   $missionSuccess = true;    

   dbehco($LEVEL, "missionSuccess()");
   
   updatePlanetInventory(he1); 
   
   forceToDebrief();
}
//-----------------------------------------------------------------------------
function missionFailed(%reason)
{
   dbecho($LEVEL, "missionFailed(" @ %reason @ ")");
   
   if($missionSuccess)
      return;

   $missionFailed = true;      
   
   if(%reason == "caanonDied")
   {
       %msg = "Caanon Died";
       missionObjective8.status = *IDSTR_OBJ_FAILED;
   }
           
   forceToDebrief();
}
//-----------------------------------------------------------------------------

function missionBoundFail()
{
   $boundFail = true;
   
   setPosition(   $exec,   getPosition($exec.drop, x),
                           getPosition($exec.drop, y),
                           getPosition($exec.drop, z) );

   order( $exec, Attack, $caanon );

	schedule("fadeEvent( 0, out, 1.0, 1, 1, 1 );",   0.0 );
	schedule("setWidescreen(true);", 1.5);
   schedule("squawkEnabled( false );", 1.5);  
   schedule("setFlybyCamera(" @ $exec @ ");", 1.5);
   schedule("cameraLockFocus(true);", 1.5);
	schedule("fadeEvent( 0, in,  1.0, 1, 1, 1 );",   2.5 );
}   
//-----------------------------------------------------------------------------

function cardinalSpearUnited()
{
   %united = true;
   if ( $playerSquad.start == 1 )
      %target = 1;
   else
      %target = $playerSquad.start - 1;

   squawkEnabled( false );  
   
   if( $mate1 != ""  && $mate1 != 0  && !($mate1.destroyed) &&  getDistance($playerId, $mate1) > 1000 )
      damageObject($mate1, 100000);
   if( $mate2 != ""  && $mate2 != 0  && !($mate2.destroyed) &&  getDistance($playerId, $mate2) > 1000 )
      damageObject($mate2, 100000); 
   if( $mate3 != ""  && $mate3 != 0  && !($mate3.destroyed) &&  getDistance($playerId, $mate3) > 1000 )
      damageObject($mate3, 100000);
      
   if( $playerSquad.num < %target )
      %united = false;
   
   return %united;
}    

///////////////////////////////////////////////////////////////////////////////
// Movie                                                                     //
///////////////////////////////////////////////////////////////////////////////
//-----------------------------------------------------------------------------
function initCinematic()
{
	setWidescreen(true);
   lockUserInput(true);
   squawkEnabled( false );  
   
   $dropPod = myGetObjectId("MissionGroup\\hDropPod1");

	setposition ( $dropPod, 0, 0, 1000);
	setposition ( $playerId, 0, 0, getTerrainHeight(0, 0) + 5 );
   
   schedule( "focusCamera( splineCamera, path4 );", 0.5 );
   schedule( "cameraLockFocus(true);", 0.25);
	schedule( "fadeEvent( 0, in, 1.0, 0, 0, 0 );",   0.6 );
		
	say( 0, 2,"HE1_Harabec_CN01.WAV");
	say( 0, 2,"HE1_Harabec_CN02.WAV");
}
//-----------------------------------------------------------------------------
function path4::camera::waypoint1()
{
	schedule( "focusCamera( splineCamera, path5 );", 6.75 );
}
//-----------------------------------------------------------------------------
function path5::camera::waypoint1()
{
	say( 0, 2,"CIN_PLUTO02.WAV");
	schedule( "focusCamera( splineCamera, path1 );", 5.5 );
	schedule( "fadeEvent( 0, out, 0.5, 0, 0, 0 );",   4.5 );	
}
//-----------------------------------------------------------------------------
function path1::camera::waypoint1()
{
	setSkyMaterialListTag(IDDML_SKY_PLUTO_DAY);

	fadeEvent( 0, in, 1.0, 0, 0, 0 );
	
	say( 0, 2,"HE1_Caanon_CN1.WAV");

	schedule( "dropPod(-1112, 250, 2950, -500, -1500, 300);",  0.0);
	schedule( "dropPod(-1112, 250, 2950,-1500, -1000, 300);", 0.2);
	schedule( "dropPod(-1112, 250, 2950,  500, -1550, 300);", 1.4);
	schedule( "dropPod(-1112, 250, 2950, -750, -2500, 300);", 2.3);
	schedule( "dropPod(-1112, 250, 2950,-1112,  -350, 300);", 2.5);
	schedule( "dropPod(-1112, 250, 295," @ $dropPod @ ");", 5.0);

	schedule( "focusCamera( splineCamera, path2 );", 8.0 );

	say( 0, 2,"HE1_Harabec_CN03.WAV");	

	
}
//-----------------------------------------------------------------------------
function path2::camera::waypoint1()
{
	
	schedule( "fadeEvent( 0, out, 1.0, 1, 1, 1 );", 3.5 );
	schedule( "focusCamera( splineCamera, path3 );", 7.5 );

}
//-----------------------------------------------------------------------------
function path3::camera::waypoint1()
{	
	fadeEvent( 0, in, 1.0, 1, 1, 1 );

	setposition ( $playerId, -1112.644531, 281.721679, getTerrainHeight(-1112.644531, 281.721679) );

	schedule("init();", 0.0);
  	schedule( "fadeEvent( 0, out, 0.5, 0, 0, 0 );", 5.0 );

   schedule( "cameraLockFocus(false);", 5.20);
	schedule( "setPlayerCamera();", 5.25 );

   schedule( "lockUserInput(false);", 5.5);
	schedule( "fadeEvent( 0, in, 1.0, 0, 0, 0 );", 5.75 );
   schedule( "squawkEnabled(true);", 6.5);  
	schedule( "setWidescreen(False);", 6.5 );
}




///////////////////////////////////////////////////////////////////////////////
// Generic Functions                                                         //
///////////////////////////////////////////////////////////////////////////////

function checkDistance( %obj1, %obj2, %distance, %callback, %time )
{
  
   dbecho( 5, "DEBUG: checkDistance ( ", %obj1, " ", %obj2, " ) ", %distance, "m CALLBACK: ", %callback );

   %var = getDistance( %obj1, %obj2 );
   dbecho( 5, "DEBUG: Distance difference: ", %var, "m" );

   if( getDistance( %obj1, %obj2 ) <= %distance )
   {
       %func = %callback @ "();";
       schedule( %func, 0 );
   }
   else
   {    schedule( "checkDistance( "
                @ %obj1 @ ", "
                @ %obj2 @ ", "
                @ %distance @ ", "
                @ %callback @ ", "
                @ %time @ ");", %time  );
   }        
}
//-----------------------------------------------------------------------------

function checkBound( %obj1, %obj2, %distance, %callback, %time )
{
   dbecho( 5, "DEBUG: checkBound ( ", %obj1, " ", %obj2, " ) ", %distance, "m CALLBACK: ", %callback );

   %var = getDistance( %obj1, %obj2 );
   dbecho( 5, "DEBUG: Distance difference: ", %var, "m" );

   if( getDistance( %obj1, %obj2 ) > %distance )
   {
       %func = %callback @ "();";
       schedule( %func, 0 );
   }
   else
   {    schedule( "checkBound( "
                @ %obj1 @ ", "
                @ %obj2 @ ", "
                @ %distance @ ", "
                @ %callback @ ", "
                @ %time @ ");", %time  );
   } 
}       


//-----------------------------------------------------------------------------
function myGetObjectId(%name)
{
    %id = getObjectId(%name);
    if( %id == "")
    {
        dbecho($LEVEL, "Object " @ %name @ " not found.");
        return false;
    }
    else
    {
        return %id;
    } 
}

//-----------------------------------------------------------------------------
function vehicle::onNewLeader(%this)
{
   if(!$missionStart)
      return;
   
   %group = getGroup(%this);
   
   if( %group != $goad )
      return;
   
   %group.lead = %this;
   order(%group.lead, Attack, $playerId);
}
//-----------------------------------------------------------------------------




   
   