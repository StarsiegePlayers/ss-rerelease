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
//    playerRace;          // string: race of player
//                         //         *IDSTR_RACE_HUMAN_IMPERIAL
//                         //         *IDSTR_RACE_HUMAN_REBEL
//                         //         *IDSTR_RACE_CYBRID
//    techLevel;           // int:    current tech level access for the player
//    cinematicRec;        // string: in game <*.rec> cinematic to be played
//    cinematicSmk;        // string: in game <*.smk> cinematic to be played
//    campaignEndSmk;      // string: in game <*.smk> cinematic to be played at campaign end
// }
//------------------------------------------------------------------------------
Campaign campaignInfo
{ 
   name = *IDCMP_NAME_CYBRID;
   usePlanetInv = true;
   scanX = "scanX.ddb";
   encyclopedia = "encyclopedia.ddb"; 
   curMission = "CA0";
   curDate = 3583860;
   playerTeam = *IDSTR_TEAM_YELLOW;
   playerRace = *IDSTR_RACE_CYBRID;
   techLevel = 3;
   cinematicRec = "cinCA.rec";
   campaignEndSmk = "cybrid_finale.smk";
};


exec("DatPilot_cy.cs");


function campaignInitInventories()
{
exec("cy_planet_init.cs");
}
