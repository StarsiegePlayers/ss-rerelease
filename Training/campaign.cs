//------------------------------------------------------------------------------
// Campaign Data
//
// Campaign campaignInfo
// {
//    name;                // string: name of the campaign
//    usePlanetInv;        // bool:   otherwise, infinite amounts of all components
//    scanX;               // string: scanX file to use
//    encyclopedia;        // string: encyclopedia to use
//    curMission;          // string: mission to start on the campaign
//    playerTeam;          // string: team of the player
//                         //         *IDSTR_TEAM_YELLOW 
//                         //         *IDSTR_TEAM_BLUE   
//                         //         *IDSTR_TEAM_RED    
//                         //         *IDSTR_TEAM_PURPLE 
//    cinematicRec;        // string: in game <*.rec> cinematic to be played
//    cinematicSmk;        // string: in game <*.smk> cinematic to be played
//    campaignEndSmk;      // string: in game <*.smk> cinematic to be played at campaign end
// }
//------------------------------------------------------------------------------
Campaign campaignInfo
{ 
   name = "";
   usePlanetInv = false;
   scanX = "";
   encyclopedia = ""; 
   curMission = "";
   curDate = 9999.0;
   playerTeam = *IDSTR_TEAM_YELLOW;
   cinematicRec = "";
   cinematicSmk = "";
   campaignEndSmk = "";
};

exec("DatPilot_hu.cs");

//------------------------------------------------------------------------------
// TrainingMission Data
//
// TrainingMission trainingMission<num>
// {
//    desc;                // string: description displayed in shell
//    name;                // string: name of the mission
//    bmp;                 // string: bmp displayed in shell
//    playerVeh;           // string: vehicle to be used by the player
//    squadMate1;          // string: name of squadmate 1
//    squadMateVeh1;       // string: name of squadmate 1's vehicle 
//    squadMate2;          // string: name of squadmate 2
//    squadMateVeh2;       // string: name of squadmate 2's vehicle
//    squadMate3;          // string: name of squadmate 3
//    squadMateVeh3;       // string: name of squadmate 3's vehicle
// }
//------------------------------------------------------------------------------

TrainingMission trainingMission1 {
   desc           = *IDSTR_TRAINING_1;
   name           = "training1";
   bmp            = "herc_train.BMP";
   playerVeh      = "TR_Emancipator.fvh";
   squadMate1     = "";
   squadMateVeh1  = "";
   squadMate2     = "";
   squadMateVeh2  = "";
   squadMate3     = "";
   squadMateVeh3  = "";
};

TrainingMission trainingMission2 {
   desc           = *IDSTR_TRAINING_2;
   name           = "training2";
   bmp            = "target_train.BMP";
   playerVeh      = "TR_Emancipator.fvh";
   squadMate1     = "";
   squadMateVeh1  = "";
   squadMate2     = "";
   squadMateVeh2  = "";
   squadMate3     = "";
   squadMateVeh3  = "";
};

TrainingMission trainingMission3 {
   desc           = *IDSTR_TRAINING_3;
   name           = "training3";
   bmp            = "weapons_train.BMP";
   playerVeh      = "TR_Emancipator.fvh";
   squadMate1     = "";
   squadMateVeh1  = "";
   squadMate2     = "";
   squadMateVeh2  = "";
   squadMate3     = "";
   squadMateVeh3  = "";
};

TrainingMission trainingMission4 {
   desc           = *IDSTR_TRAINING_4;
   name           = "training4";
   bmp            = "squad_train.BMP";
   playerVeh      = "TR_Basilisk.fvh";
   squadMate1     = *IDPLT_CALL_BIO_DERM;
   squadMateVeh1  = "TR_SquadmateEmancipator.fvh";
   squadMate2     = *IDPLT_CALL_VERITY;
   squadMateVeh2  = "TR_SquadmateEmancipator.fvh";
   squadMate3     = "";
   squadMateVeh3  = "";
};
