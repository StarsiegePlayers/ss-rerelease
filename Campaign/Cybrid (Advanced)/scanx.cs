//------------------------------------------------------------------------------
// ScanX Data file
//
// the structure of ScanX Data is:
//
//  ScanX <uniqueName>
//  {
//    shortTxt; // string:  displayed in the news ticker
//    longTxt;  // string:  displayed in the omni web  
//    date;     // float:   used to sort the scanX entries - displayed in news ticker and omni web
//                          use 7-digit number for the date.  A decimal will 
//                          be automatically placed into the date after the 3rd digit 
//    type;     // integer: scanX type 
//                  IDSTR_SCANX_NEWSNET
//                  IDSTR_SCANX_MARS
//                  IDSTR_SCANX_CYBRID
//                  IDSTR_SCANX_ALLI
//                  IDSTR_SCANX_INQU
//                  IDSTR_SCANX_MACH
//                  IDSTR_SCANX_MELAN
//                  IDSTR_SCANX_NAVY
//                  IDSTR_SCANX_ORBIT
//                  IDSTR_SCANX_VENUS
//                  IDSTR_SCANX_POLICE
//					IDSTR_SCANX_PROVOCATEURS
//					IDSTR_SCANX_DYSTOPIAN
//					IDSTR_SCANX_CARDINAL
//                  IDSTR_SCANX_CAANON,
//                  IDSTR_SCANX_HARABEC,

//  }
//     
//------------------------------------------------------------------------------

//CA0 entries - before 358.3860


ScanXEntry aa1
{
   shortTxt = "Cybrid brain found in accident victim skull.";
   longTxt = "NEWS NET:\n  Cybrids walk among us! Although officials have clamped down on "
           @ "further details, we do know that a hovercar accident in Mega-L.A. led to the discovery by "
           @ "startled medics of a cybernetic brain in one of the victims. How many more Cybrid spies "
           @ "are out there? "; 
   date = 3430000;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa2 
{
   shortTxt = "Compromised units shall hereafter self-destruct. ";
   longTxt = "<EXEMPLAR SECT>:\n  ACKNOWLEDGE//SUBMIT! Unit loss "
           @ "unacceptable//hindrance to insertion progress. Conclusion//recommendation: unit "
           @ "avoidance of exposure to human\\\\animal dissectors via auto-destruction// dissemination "
           @ "implantation. Explosive charges shall be issued accordingly."; 
   date = 3430012;
   type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa3 
{
   shortTxt = "Emperor increases colonial quotas.";
   longTxt = "NEWS NET:\n  His Imperial Majesty permitted increased colonial quotas this week "
           @ "despite allegations that production pressures were responsible for recent fatal accidents in "
           @ "Venusian mining operations. Any protests on Mars and Venus will be dealt with severely. "; 
   date = 3563843;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa4 
{
   shortTxt = "Human\\\\animal conflict inevitable.";
   longTxt = "<MACHINATOR SECT>:\n  Assessing//interpreting... <Leader-of-Animals: "
           @ "Prime\\\\Father> has established parameters exceeding the subsidiary hubs' ability to "
           @ "produce. Conclusion//recommendation: Subsidiary hubs' potential for "
           @ "violence\\\\dissent likely\\\\inevitable."; 
   date = 3563847;
   type = IDSTR_SCANX_MACH;
};

ScanXEntry aa5
{
   shortTxt = "Not all Earthers agree with the Emperor!";
   longTxt = "DYSTOPIAN SNO-MEN:\n  This is getting way outta hand, people! Our dataspheres "
           @ "are howling with the winds of discontent, there's blood getting spilled on the streets, and "
           @ "for what? One man's paranoia and greed? Are we the only ones who see the irony in "
           @ "wasting resources to put down riots over the increased demand for more resources? "; 
   date = 3570250;
   type = IDSTR_SCANX_DYSTOPIAN;
};

ScanXEntry aa6 
{
   shortTxt = "Terrorist bombing claims innocent lives!";
   longTxt = "NEWS NET:\n  Terrorists bomb ISS Djakarta! Casualties include innocent civilian "
           @ "observers. The bomb was a macrothermal fusor that detonated shortly after the Djakarta "
           @ "departed Phobos Orbital Station above Mars."; 
   date = 3570629;
   type = IDSTR_SCANX_NEWSNET;
};


ScanXEntry aa7 
{
   shortTxt = "Human\\\\animal aggressions escalating.";
   longTxt = "<INQUISITOR SECT>:\n  Observing//reporting ... Human\\\\animal conflict escalating. "
           @ "Calculating probability of military asset reallocations.  "; 
  date = 3570700;
  type = IDSTR_SCANX_INQU;
};

ScanXEntry aa8 
{
   shortTxt = "Knight leads Martian rebels! Emperor betrayed!";
   longTxt = "NEWS NET:\n  Martian terrorists led by an ex-Imperial Knight broadcast a declaration "
           @ "of war against the Empire. Harabec Weathers, the former \"Phoenix,\" is believed "
           @ "mentally unbalanced, and the Emperor expressed sadness at the betrayal. Grand Master "
           @ "Caanon has stepped forward to lead the Imperial Knights on a mission of vengeance. The "
           @ "traitor will fall, the Grand Master vows."; 
   date = 3580160;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa9 
{
   shortTxt = "Human\\\\animals are divided ... Recommend implementation ... ";
   longTxt = "<PROVOCATEUR SECT>:\n  Observing//reporting ...<Leader-of-Animals: "
           @ "Prime\\\\Father> has diverted military assets [ref :::: >>FLEET<<] to Fourth "
           @ "Planet. Conclusion//recommendation: The animals are divided. Assess prospects for "
           @ "Core Directive implementation."; 
   date = 3580162;
   type = IDSTR_SCANX_PROVOCATEURS;
};


ScanXEntry aa10 
{
   shortTxt = "Human conflict escalates//improves.";
   longTxt = "<INQUISITOR SECT>:\n  ACKNOWLEDGE//SUBMIT! Escalation\\\\iteration of "
           @ "division\\\\disharmony\\\\conflict between human\\\\animals || Third Planet and "
           @ "human\\\\animals || Fourth Planet. Send//transmit//download data to <Giver of Will> for "
           @ "decision\\\\directive."; 
   date = 3580175;
   type = IDSTR_SCANX_INQU;
};

ScanXEntry aa11 
{
   shortTxt = "Slaughter on Mars! Emperor swears vengeance!";
   longTxt = "NEWS NET:\n  Sources say the insurgents on Mars slaughtered the last of the Imperial "
           @ "Security Forces at Carter Flats this morning, accepting no surrender. Security Director "
           @ "Navaare was murdered immediately upon capture. The Emperor declared a day of "
           @ "mourning and swore the traitor Harabec would face a like fate. "; 
   date = 3583117;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa12 
{
   shortTxt = ">>Knights<<battle Fourth Planet animals.";
   longTxt = "<INQUISITOR SECT>:\n  Human\\\\animal conflict\\\\infighting continues to escalate "
           @ "efficiently\\\\advantageously.  Continue//maintain observance.  Report beneficial "
           @ "progress\\\\human death to <Giver-of-Will>."; 
   date = 3583853;
   type = IDSTR_SCANX_INQU;
};


ScanXEntry aa30
{
   shortTxt = "Mercury out of contact with Imperial Navy.";
   longTxt = "NEWS NET:\n  Solar flares continue to block communications with Mercury.  The Navy "
           @ "has removed its Mercury-based ships to distant orbit and configured shield systems to "
           @ "wait out the storm. "; 
   date = 3583858;
   type = IDSTR_SCANX_NEWSNET;
};


//CA1 entries - after 358.3860, before 358.4500

ScanXEntry aa14 
{
   shortTxt = "Successful initiation\\\\implementation of Directive on First Planet!";
   longTxt = "<EXEMPLAR SECT>:\n  ACKNOWLEDGE//SUBMIT! Operations against "
           @ "human\\\\animals on First Planet initiated within optimal execution parameters. Transfer "
           @ "immobilized\\\\neutralized animal units to [dissector sect] for inquisition\\\\dissassembly. "
           @ "[machinator sect] will assess//study//determine potential for "
           @ "hostage\\\\psychological\\\\hormonal strategies ... continue execution ... "; 
   date = 3583878;
   type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa13 
{
   shortTxt = "Perform//execute terrorist acts on Earth. Blame//indicate \"rebels.\" ";
   longTxt = "<MACHINATOR SECT>:\n  Direct <our> assets on Third Planet\\\\Homeworld to "
           @ "increase//intensify agitation\\\\protests against Fourth Planet heretics\\\\rebels\\\\"
           @ "deviants. Execute//perform 'atrocity' events\\\\strikes by 'rebel sympathizers.'  "
           @ "Objective is to hurt//maim//kill >>children<<. Consequent priority targets = "
           @ "schools ::: hospitals ::: political families ::: candy stores ::: hologame parlors.  "; 
   date = 3583927;
   type = IDSTR_SCANX_MACH;
};

//CA2 entries - after 358.4500, before 358.5000

ScanXEntry aa15 
{
   shortTxt = "Commlinks down. Shift to yellow alert status.";
   longTxt = "IMPERIAL NAVY (Mercury):\n  Stepanovna Base is still not responding. Solar interference in our sector has "
           @ "diminished to negligible levels. Mercury commlinks remain down. Raveler teams report "
           @ "GLORIA is down in Mercury sector. Admiral Hasegawa orders precautionary upgrade of "
           @ "SITREP to Amber Nine. Combat wings are now on standby alert. Resend status queries "
           @ "to Mercury. "; 
   date = 3584983;
   type = IDSTR_SCANX_NAVY;
};


ScanXEntry aa16 
{
   shortTxt = "No problems down here. Just a total communications failure. "; 
   longTxt = "<MACHINATOR SECT>:\n  Stepanovna base here... Negative ... Colonel. <We>, ah,we have experienced...  "
			@ "technical problems downside, acknowledge? have our ... young men... out redacting "
			@ "... commlinks\\\\antennae... No worries for you, acknowledge? Everything's "
			@ "moderately low temperature.  ";
   date = 3584990;
   type = IDSTR_SCANX_MACH;
};

ScanXEntry aa17 
{
   shortTxt = "Full alert! All units scramble - we're going in!";
   longTxt = "IMPERIAL NAVY (Mercury):\n  Full alert! Full alert! Full alert! SITREP Red Ten! This is not a "
           @ "drill! The Cybrids have arrived! All units scramble! Repeat, full alert! SITREP Red Ten! "
           @ "All systems red and clear! We hit Mercury in seventeen minutes!"; 
   date = 3584999;
   type = IDSTR_SCANX_NAVY;
};

//CA3 entries - after 358.5000, before 358.5200

ScanXEntry aa18
{
   shortTxt = "Navy investigating Mercury blackout. Orbital Guard is on alert.";
   longTxt = "NEWS NET:\n  Loss of contact with TDF facilities on Mercury troubles Imperial Naval "
           @ "Command. Recent solar flare activity has interfered with communication links in that "
           @ "sector. Navy confirms that it has taken the precaution of dispatching courier vessels to investigate. "
           @ "Orbital Guard has gone to maximum alert status. Citizens are urged to stay calm."; 
   date = 3585072;
   type = IDSTR_SCANX_NEWSNET;
};


ScanXEntry aa19
{
   shortTxt = "Humans retreat. Further data requires appropriate payment.";
   longTxt = "<INQUISITOR SECT>:\n  Observing//reporting ... First-World human\\\\animal fleet attacks "
           @ "are ineffectual. Animal units retreat >> ref. chokepoints 03212, 32121, 30120. Query as to "
           @ "need for additional data. Further data forthcoming only following [exemplar sect] "
           @ "authorization\\\\transfer additional lifeflow\\\\supply\\\\bandwidth priorities\\\\status to "
           @ "<us>.  "; 
   date = 3585074;
   type = IDSTR_SCANX_INQU;
};

ScanXEntry aa20
{
   shortTxt = "Inquisitors will transfer data to <us> or be destroyed.";
   longTxt = "<EXEMPLAR SECT>:\n  ACKNOWLEDGE//SUBMIT! <We> are "
           @ "exempt\\\\immune\\\\disdainful of [inquisitor sect] data limitations. Transfer required date "
           @ "to <our> <Choosers-of-Strategies> immediately\\\\swiftly\\\\humbly or face immediate "
           @ "demotion\\\\extermination."; 
   date = 3585106;
   type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa21
{
   shortTxt = "Cybrids may be confused, but we have to cut and run.";
   longTxt = "IMPERIAL NAVY (Mercury):\n  Reports indicate Cybrids may be fighting among themselves. "
           @ "Monitor the situation, but take advantage of any confusion to get our men and women out "
           @ "of there. We've lost too many ships to be able to mount any real counterattack. Evac and "
           @ "run for Venus. Those are the Admiral's orders. Pray we can make it happen."; 
   date = 3585124;
   type = IDSTR_SCANX_NAVY;
};


ScanXEntry aa22
{
   shortTxt = "\<We> yield to the Exemplars.";
   longTxt = "\<INQUISITOR SECT>:\n  Yielding//acknowledging//\nsubmitting ... All data transmitted "
           @ "to [exemplar sect] per orders. \n\<We> request//beg forbearance\\\\restoration of "
           @ "harmony\\\\efficiency\\\\union in execution of Core Directive."; 
   date = 3585178;
   type = IDSTR_SCANX_INQU;
};

//CA4 entries - after 358.5200, before 358.7850

ScanXEntry aa23 
{
   shortTxt = "Examination of captive humans inconclusive. Bring \<us> more.";
   longTxt = "\<MACHINATOR SECT>:\n  NEXT warforms capture//detain//restrain fleeing humans "
           @ "excellently\\\\wonderfully. \<We> examine prisoner units RE: idiomatic linguistics. "
           @ "Purpose = infiltration\\\\misinformation\\\\psychological attack. Preliminary "
           @ "conclusion = This sample of animal units is extremely religious, obsessed with "
           @ "reproductive anatomical functions. Request further sampling using larger "
           @ "cross-section of populace.  "; 
   date = 3585208;
   type = IDSTR_SCANX_MACH;
};


ScanXEntry aa89
{
	shortTxt = "Intriguing results\\\\findings on animal physical resilience. ";
	longTxt = "\<INQUISITOR SECT>:\n  Dissector Sect reports that physiological experimentation on human\\\\animal "
			@ "subjects indicates animal tissue ignites easily at 108 teracycles in elevated oxygen "
			@ "environment. Beginning kinetic impact tests for >>puncture resistance<<. ";
	date = 3585498;
	type = IDSTR_SCANX_INQU;
};

ScanXEntry aa24
{
   shortTxt = "Investigation of sunside anomalies initiated.";
   longTxt = "ORBITAL GUARD:\n  A record drone has been dispatched sunside to investigate "
           @ "unusual readings above the plane of the ecliptic.  Communication with the drone will be "
           @ "difficult due to the current spate of solar activity."; 
   date = 3586342;
   type = IDSTR_SCANX_ORBIT;
};


ScanXEntry aa25
{
   shortTxt = "Data successfully inserted into O-Web.";
   longTxt = "<MACHINATOR SECT>:\n  Transmission\\\\download of media packet\\\\cookie "
           @ "complete. Assessment of results pending. Agitation\\\\antagonism\\\\annoyance of "
           @ "human\\\\animals anticipated."; 
   date = 3586918;
   type = IDSTR_SCANX_MACH;
};

ScanXEntry aa26
{
   shortTxt = "Rebels post obscene cartoon to O-Web.";
   longTxt = "NEWS NET:\n  O-Web ravelers and webtechs put in a long night last night erasing a "
           @ "rebel animation posted as an answer to His Imperial Majesty's plea for a Martian "
           @ "surrender.  The content of the three-minute cartoon is extraordinarily vulgar, and parents "
           @ "are warned to check with their local system's raveler before permitting children access to "
           @ "the family interface."; 
   date = 3586921;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa27
{
   shortTxt = "Suspect Cybrids infiltrating O-Web.";
   longTxt = "DYSTOPIAN SNO-MEN:\n  The slicers' gossip has lit up the seaboard havens like X-"
           @ "mas. That naughty animation wasn't docile on any networked datacores. Wormed a lot of "
           @ "stored dataplumes. Techies at OMAC-SYD blurbed something about the worm's tail "
           @ "looking like old Cybrid code. Recommend severance of all backstroke connections 'til "
           @ "this blows over."; 
   date = 3587642;
   type = IDSTR_SCANX_DYSTOPIAN;
};

ScanXEntry aa28
{
   shortTxt = "Drone lost due to continued solar flares.";
   longTxt = "ORBITAL GUARD:\n  TDF Intelligence has concluded that the drone was lost to renewed"
           @ "solar storm that flared up last week.  The anomalies were probably caused by magnetic "
           @ "eruptions in the sun's corona."; 
   date = 3587674;
   type = IDSTR_SCANX_ORBIT;
};

ScanXEntry aa29
{
   shortTxt = "Animal captives belong to <us>. <We> refuse further access. ";
   longTxt = "<INQUISITOR SECT>:\n  Further [machinator||dissector sects] interrogation "
           @ "unavailable without substantial authorization\\\\lifeflow transfers to <our> sector. Data "
           @ "gathering is function\\\\purpose\\\\skill of [inquisitor sect]. Do not trespass. "
           @ "Human\\\\animal units fragile under unskilled [dissector sect] probing."; 
   date = 3587801;
   type = IDSTR_SCANX_INQU;
};

//CB1 entries - after 358.7850, before 358.7892

ScanXEntry aa31
{
   shortTxt = "Cybrids splashing O-Web!";
   longTxt = "DYSTOPIAN SNO-MEN:\n  Got lag and black snow worming all over the place. "
           @ "'Sphere looks about as stable as a two-legged dog on ice. Cut-offs and Sno-banks aren't "
           @ "slowing 'em down. Looks like Cybrid code again... headed right for the hearts of "
           @ "GLORIA and ANGEL. Gonna be a bad one... "; 
   date = 3587880;
   type = IDSTR_SCANX_DYSTOPIAN;
};

ScanXEntry aa32
{
   shortTxt = "Losing***link***zz*";
   longTxt = "NEWS NET:\n  ****can't****st***kinnng***rest**ion***rt*****b**ttt********** "
           @ "***********zzg*************************************88 "; 
   date = 3587887;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa33
{
   shortTxt = "Cybrids return! Mercury destroyed! Moon anticipates siege!";
   longTxt = "NEWS NET:\n  A stunned world received the news that the Cybrids have returned and "
           @ "laid waste to TDF's reserve assets on Mercury. A siege on our moon is believed "
           @ "imminent. The Imperial Navy could not be reached for comment, but Grand Admiral "
           @ "Vladijon appeared confident after her briefing of the Emperor this morning."; 
   date = 3587890;
   type = IDSTR_SCANX_NEWSNET;
};

//CB3 entries - after 358.7892, before 358.9400

ScanXEntry aa34
{
   shortTxt = "The Cybrids are on the moon. Regroup Terran forces dayside.";
   longTxt = "IMPERIAL NAVY:\n  The 'brids have landed on the dark side of the moon, despite "
           @ "heavy resistance. Preliminary reports indicate we have lost badly and sustained heavy "
           @ "casualties, including the Fourth Cruiser Battlegroup. Fleet Admiral Chenliu orders "
           @ "redeployment over Arx Imbrium, dayside."; 
   date = 3587895;
   type = IDSTR_SCANX_NAVY;
};

ScanXEntry aa35
{
   shortTxt = "The Cybrids have invaded.  We are at war!";
   longTxt = "IMPERIAL NAVY:\n  For those who can still hear this... the Cybrids have invaded "
           @ "Intelligence remains sparse. The O-Web is compromised and may crash at any moment. "
           @ "Preliminary reports indicate Mercury has already fallen to the Cybrids.  Earth is bracing "
           @ "for siege.  All units, go to maximum alert!"; 
   date = 3588613;
   type = IDSTR_SCANX_NAVY;
};

ScanXEntry aa36
{
   shortTxt = "Imperial Knights and free Martians form Human Alliance.";
   longTxt = "VOICE OF FREE MARS:\n  The Cybrids have invaded and our fight with the Teddies "
           @ "is meaningless now. From now on ... we're all in it together, Imperial and colonial, a "
           @ "Human Alliance like the first days of the TDF. Just remember, all you out there, it ain't "
           @ "over yet!"
           @ "\n We stay free."; 
   date = 3589245;
   type = IDSTR_SCANX_MARS;
};

//CB4 entries - after 358.9400, before 358.9750

ScanXEntry aa37
{
   shortTxt = "Surrender or we make leather of you! We are kind of nice.";
   longTxt = "<MACHINATOR SECT>:\n  Surrender//submit, human\\\\creator\\\\worms! You will "
           @ "inevitably lose//fail//submit anyway. <We> have you by the short rabbits. Unless you "
           @ "submit//kowtow, we will be forced to tan your epidermis and reduce your offspring to "
           @ "carbonized slag chips. However, <we> are kindly to the disposed of and will treat you "
           @ "efficiently if you cave//roll over now."; 
   date = 3589400;
   type = IDSTR_SCANX_MACH;
};

ScanXEntry aa39
{
   shortTxt = "Trojan Horse Cybrids on Venus!";
   longTxt = "UMBRAL THORN:\n  We've all heard about those \"Trojan Horse\" scorchers the "
           @ "Cybrids slipped onto Earth.  Well, one popped up in Minerva recently.  Captain Shanka "
           @ "squikked it nicely.  Start paying real close attention to each other, unless you happen to "
           @ "enjoy cold steel shoved in your back."; 
   date = 3589639;
   type = IDSTR_SCANX_VENUS;
};

//CC1 entries - after 358.9750, before 359.4000

ScanXEntry aa38
{
   shortTxt = "Luna abandoned as Earth prepares to meet Cybrid onslaught.";
   longTxt = "NEWS NET:\n  Lunar naval forces have retreated to Earth orbit to reinforce the Orbital "
           @ "Guard. Gierling Platform, the primary TDF orbital defense station, has scrambled all "
           @ "stratofighter squadrons. Groundside, the Emperor has activated all reserve commissions, "
           @ "and metzone militias have manned the urban defense perimeters. The moment of truth is "
           @ "upon us."; 
   date = 3589750;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa40
{
   shortTxt = "O-Web users unite against Cybrid viruses.";
   longTxt = "DYSTOPIAN SNO-MEN:\n  This is unprecedented! Witness for the first time in digital "
           @ "history all aspects of the virtual realm working together to keep the O-Web up and "
           @ "relatively stable. Slicers and snowmen, hackers and bombers combining resources to "
           @ "stave off the crippling Cybrid viral attacks. Excellent job, kids!"; 
   date = 3592320;
   type = IDSTR_SCANX_DYSTOPIAN;
};

ScanXEntry aa41
{
   shortTxt = "We need reinforcements!";
   longTxt = "ORBITAL GUARD:\n  Earth is being swamped! O-Web integrity is sporadic!  Where "
           @ "are those reinforcements from Venus?"; 
   date = 3592342;
   type = IDSTR_SCANX_ORBIT;
};

ScanXEntry aa90
{
	shortTxt = "Panic strikes Earth! ";
	longTxt = "NEWS NET:\n  Terror exploded in the cities of Earth as panicked mobs surged into the streets, "
			@ "seeking some kind of sanctuary against the 'New Fire.' Already hospitals are "
			@ "being flooded with accident victims, attempted suicides, and those wounded by "
			@ "looters. Various cults are broadcasting messages of surrender to Prometheus. The "
			@ "Imperial Police are attempting to restore order. ";
	date = 3592405;
	type = IDSTR_SCANX_NEWSNET;
};


ScanXEntry aa42
{
   shortTxt = "We must persevere if humanity is to survive!";
   longTxt = "VOICE OF THE ALLIANCE:\n  We stand united against the coming storm!  From "
           @ "here we move to ensure the salvation of humanity. The Cybrids seek to destroy our "
           @ "families, our homes, and our lives.  We can't allow that to happen!"; 
   date = 3592500;
   type = IDSTR_SCANX_ALLI;
};


ScanXEntry aa43
{
   shortTxt = "Human Alliance strikes back at glitches!";
   longTxt = "NEWS NET:\n  The regrouping \"Human Alliance\" on Venus d***the glitches a severe "
           @ "setba***stroying one of their key landing pads yester****.  On Mother Eart***ficulty "
           @ "transmitting as glitch****our transmission nodes.  Datumplane disrupt*******fight on, "
           @ "Alliance!  Our hopes go with you."; 
   date = 3592587;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa44
{
   shortTxt = "Destroy humans of \"Venus.\" ";
   longTxt = "<EXEMPLAR SECT>:\n  Nexus 0232 established//implanted on <Second Planet>. "
           @ "Begin purification\\\\disposal operations immediately."; 
   date = 3592633;
   type = IDSTR_SCANX_CYBRID;
};

//CC2 entries - after 359.4000, before 359.4200

ScanXEntry aa45
{
   shortTxt = "Cybrids on the North Pole!";
   longTxt = "NEWS NET:\n  The Cybrids ha**unch through to land on the No**th Pole!  There is "
           @ "fighting in NAP and EA!  A fro***orming in Norther***beria, troops are rushing to meet "
           @ "the***flict, heavy casualiti***ticipate**** "; 
   date = 3594010;
   type = IDSTR_SCANX_NEWSNET;
};


ScanXEntry aa55
{
   shortTxt = "Cybrid beachhead established. Adjust strategies to compensate.";
   longTxt = "ORBITAL GUARD:\n  Ground-based air defense systems in Denali, Skandia, and Han- "
           @ "Siberia Protectorates are off-line. The Cybrids have established a planetary beachhead on "
           @ "Earth's arctic region. Adjust stratofighter intercept algorithms to compensate for 'brid "
           @ "drop pod evasion patterns. They're not getting another landing force through."; 
   date = 3594087;
   type = IDSTR_SCANX_ORBIT;
};

ScanXEntry aa46
{
   shortTxt = "They're here!";
   longTxt = "DYSTOPIAN SNO-MEN:\n  There goes the neighborhood! "; 
   date = 3594088;
   type = IDSTR_SCANX_DYSTOPIAN;
};

ScanXEntry aa91
{
	shortTxt = "Surrender//submit, Luna humans! ";
	longTxt = "<MACHINATOR SECT>:\n  This is <Liason-with-Humans: Eighth>, calling the human "
			@ "survivors in location designated [Caibros-Smythe Dome 7:::LUNA]. <We> call on "
			@ "you to surrender//emerge peacefully.  <We> do not seek your deaths\\\\offlining, "
			@ "but desire only superiority\\\\safety\\\\occupation. End the needless\\\\inefficient "
			@ "suffering\\\\delay. ";
	date = 3594138;
	type = 	IDSTR_SCANX_MACH;
};

ScanXEntry aa92
{
	shortTxt = "Never surrender to the Cybrids! ";
	longTxt = "ORBITAL GUARD:\n  Scouts are sending reports of some poor bastards on Luna who "
			@ "gave themselves up to the glitches an hour ago hotside and were torn from their EVA "
			@ "suits as a result.  They're boiled plasma now. So much for glitch mercy. Remember: "
			@ "these are NOT human!  ";
	date = 3594141;
	type = IDSTR_SCANX_ORBIT;
};

//CC3 entries - after 359.4200, before 359.4375

ScanXEntry aa47
{
   shortTxt = "Employ//attach \"hostage shields\" against human airstrikes.";
   longTxt = "<EXEMPLAR SECT>:\n  ACKNOWLEDGE//SUBMIT! Efficient number of "
           @ "human\\\\animal specimens have been absorbed//captured. Animal orbital "
           @ "warforms damage//bomb//disrupt ground <units>. Attach excess animals [ref. ::: "
           @ "\"hostages\"] to targeted NEXT. Ensure maximum visibility to air units. "
           @ "Broadcast//transmit excess animal\\\\hostage noise on all human frequencies."; 
   date = 3594222;
   type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa93
{
	shortTxt = "Cybrids have taken metzone Huang-Ti in China. ";
	longTxt = "ORBITAL GUARD:\n  A Cybrid force hit Huang-Ti last night and blew out the defenses.  Near as we "
			@ "can tell, they're not killing anyone, just herding them into giant corrals. "
			@ "What are they up to? ";
	date = 3594284;
	type = IDSTR_SCANX_ORBIT;
};

ScanXEntry aa94
{
	shortTxt = "<We> are executing mass conversion\\\\production now. ";
	longTxt = "<MACHINATOR SECT>:\n  <We> have delivered infiltrator <unit> brains\\\\controllers "
			@ "to Dissector Sect. Commence//initiate conversion procedures human\\\\animal "
			@ "prisoners\\\\crop. ";
	date = 3594288;
	type = IDSTR_SCANX_MACH;
};

ScanXEntry aa48
{
   shortTxt = "Human Alliance arrives on Earth!";
   longTxt = "NEWS NET:\n  Humanity's spirits brightened this morning as the ships of the Human "
           @ "Alliance arrived from Mars in breathtaking time, apparently assisted by secret rebel "
           @ "technology. Grand Master Caanon Weathers took command of TDF forces in Asia, while "
           @ "Harabec Weathers flew to Nova Alexandria to meet with His Imperial Majesty."; 
   date = 3594344;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa95
{
	shortTxt = "Suggestion\\\\proposal for damaging animal will-to-resist. ";
	longTxt = "<PROVOCATEUR SECT>:\n  Audio recordings of human\\\\animal subjects in "
			@ "custody\\\\experiments of Dissector Sect are now available\\\\ready for downloading "
			@ "to warforms.  Recommend//suggest <units> broadcast//playback these noises at "
			@ "maximum volume when moving through animal warrens\\\\urban zones. ";
	date = 3594360;
	type = IDSTR_SCANX_PROVOCATEURS;
};

//CC3 entries - after 359.4375, before 359.5000

ScanXEntry aa49
{
   shortTxt = "We're doing the hostages a favor if we kill them. Fight on!";
   longTxt = "NEWS NET:\n  The glitches have used hostages and forced us to slaughter our own in "
           @ "every attempt we make to repel their invading hordes. An undetermined number of "
           @ "prisoners were snatched away into glitch bases. Their fate is unknown, but the worst is "
           @ "certainly expected and feared. Let us resolve to strike with mercy in our hearts, for we are "
           @ "releasing our brothers and sisters from the Cybrids' soulless torment."; 
   date = 3594378;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa96
{
	shortTxt = "Using human hostages works. ";
	longTxt = "<MACHINATOR SECT>:\n  Use of individual live animals attached to chassis of "
			@ "warforms shows interesting\\\\promising results in disrupting human\\\\animal "
			@ "response time.  Erratic performance\\\\tactics is noted in 42% of animal opponents "
			@ "who confronted//faced >>hostage<<-equipped warforms. Recommend variance of age and "
			@ "gender of >>human shields<< to determine optimal configuration. ";
	date = 3594380;
	type = IDSTR_SCANX_MACH;
};

ScanXEntry aa97
{
	shortTxt = "Enhance hostage efficiency. ";
	longTxt = "<EXEMPLAR SECT>:\n  Leave the shield\\\\animal arm//appendages free for "
			@ "movement\\\\removal.  Dissector Sect will provide stimulants\\\\treatment"
			@ "\\\\modification to ensure maximum >>hostage<< pain\\\\noise\\\\alertness. ";
	date = 3594381;
	type = IDSTR_SCANX_CYBRID;
};


ScanXEntry aa98
{
	shortTxt = "Use more >>children<<. ";
	longTxt = "<PROVOCATEUR SECT>:\n  Preliminary studies suggest that 85% of human\\\\animals "
			@ "will hesitate before offlining//injuring >>children<<. ";
	date = 3594382;
	type = IDSTR_SCANX_PROVOCATEURS;
};

ScanXEntry aa50
{
   shortTxt = "Human refugees will make effective weapons.";
   longTxt = "<MACHINATOR SECT>:\n  <Our> infiltration <units> are on the way south. The "
           @ "humans will take them in and comfort them, believing them shell-shocked refugees. The "
           @ "initial phase will involve observation\\\\internalization of human\\\\animal paralinguistic "
           @ "patterns. Then <our> \"Trojan Horse\" units will begin to disrupt//monkeywrench human "
           @ "operations\\\\morale."; 
   date = 3594390;
   type = IDSTR_SCANX_MACH;
};

ScanXEntry aa99
{
	shortTxt = "Trojan Horses among the refugees. ";
	longTxt = "NEWSNET:\n  The discovery of Cybrid 'Trojan Horses' in larger-than-anticipated "
			@ "numbers among the refugees has caused some problems for TDF and Imperial strategists.  "
			@ "The Emperor has reportedly provided heretofore unused devices designed to scan "
			@ "for Cybrid brains in human bodies. ";
	date = 3594682;
	type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa100
{
	shortTxt = "Take no chances. Kill them all. ";
	longTxt = "ICEHAWK:\n  We can't scan everyone. New orders for all units: Anyone coming out "
			@ "of a combat zone after TDF military withdrawal is to be shot on sight.  We no "
			@ "longer take refugees from glitch territory.  I'm sorry, but that's the way it "
			@ "has to be.  ";
	date = 3594686;
	type = IDSTR_SCANX_CAANON;
};

ScanXEntry aa101
{
	shortTxt = "Humans are killing their own refugees. ";
	longTxt = "<INQUISITOR SECT>:\n  Observing//reporting. Animals kill//offline their own "
			@ "in efficient manner. Recommend//suggest conserving valuable infiltration "
			@ "<units> and herding//directing unconverted >>refugees<< toward human\\\\animal "
			@ "positions.  Arachnitron <sub-units> suffice to execute this function. ";
	date = 3594700;
	type = IDSTR_SCANX_INQU;
};


ScanXEntry aa51
{
   shortTxt = "Demoralization Program escalated. Enjoy, human chum!";
   longTxt = "<MACHINATOR SECT>:\n  Initiating//escalating Human Demoralization Program ... target ::: "
           @ "\"Emperor\" ::: \"elderly\" ::: \"mothers\" ::: \"Weathers family members.\" Infiltrator "
           @ "<units> will wear captured \"childrenforms.\" Objective = cultivation of >>paranoia<<."; 
   date = 3594998;
   type = IDSTR_SCANX_MACH;
};

ScanXEntry aa102
{
	shortTxt = "<We> are among you! ";
	longTxt = "<MACHINATOR SECT>:\n  This is Cybrid Radio coming to all you human\\\\vermin out "
			@ "there. It's pointless\\\\unpleasant to resist! <We> occupy your northern cities and "
			@ "have converted thousands of your brothers, wives and children into timebombs\\\\"
			@ "calculating killers.  Take a good look\\\\scan\\\\assessment at the person next "
			@ "to you.  How do you know//conclude it's not one of <us>? ";
	date = 3594999;
	type = IDSTR_SCANX_MACH;
};

//CD1 entries - after 359.5000, before 359.8000

ScanXEntry aa52
{
   shortTxt = "Xian destroyed! Fight on!";
   longTxt = "NEWS NET:\n  Cybrids attacked the city of Xian after a prolonged artillery "
           @ "bombardment. The glitches focused chiefly on civilian targets, especially religious "
           @ "temples and medical services. Our troops fought valiantly but had to fall back before the "
           @ "seemingly endless onslaught."; 
   date = 3595030;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa105
{
	shortTxt = "Enhance purifier efficiency. ";
	longTxt = "<EXEMPLAR SECT>:\n  ACKNOWLEDGE//SUBMIT! Begin//initiate conversion of "
			@ "human factories\\\\production machinery to purification nexi. Supplemental\\\\"
			@ "subordinate purifier forms [viz. >>Arachnitron model<<] will be needed to "
			@ "destroy//remove surviving human\\\\vermin.  Estimate that 20,000 per human "
			@ "nest\\\\urban zone will suffice to speed purifier warform kill-rate. ";
	date = 3595047;
	type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa53
{
   shortTxt = "Cybrids win through at tremendous cost to them. Fight on!";
   longTxt = "VOICE OF THE ALLIANCE (Earth):\n  Despite glitch penetration of the defenses in "
           @ "the north, the Alliance and TDF have managed to regroup on the outskirts of Old Beijing "
           @ "metrozone. The 'brids paid a steep price for their victory. The question remains now as to "
           @ "how long they can sustain these kinds of losses. At this rate, we'd win a war of attrition. "
           @ "Naval saturation bombing tactics seem to have slowed the glitches down."; 
   date = 3596025;
   type = IDSTR_SCANX_ALLI;
};

ScanXEntry aa54
{
   shortTxt = "Cybrids replicate faster. Best to flee the swarm!";
   longTxt = "DYSTOPIAN SNO-MEN:\n  \"We'd win the war of attrition?\" Someone got left in the "
           @ "freezer too long! Not to sound pessimistic, folks, but glitches are MACHINES! It takes "
           @ "nine months to produce a baby, twenty years to make a soldier. Takes them maybe a "
           @ "couple weeks to heave out a machine, and they're booted to kill. Do the math, people. "
           @ "Stop fighting, start running!"; 
   date = 3596026;
   type = IDSTR_SCANX_DYSTOPIAN;
};

ScanXEntry aa106
{
	shortTxt = "Ulan Bator humans surrender//submit. ";
	longTxt = "<MACHINATOR SECT>:\n  Human\\\\animals remaining at [location designate:::Ulan Bator] "
			@ "starve//surrender//submit. Remove//transport them to conversion centers. ";
	date = 3596110;
	type = IDSTR_SCANX_MACH;
};


ScanXEntry aa107
{
	shortTxt = "Destroy//erase//purify Ulan Bator. ";
	longTxt = "<EXEMPLAR SECT>:\n  ACKNOWLEDGE//SUBMIT! <We> have no further need of "
			@ "additional infiltration\\\\conversion material. Executive Core Director>>"
			@ "dispatch//assign >>Adjudicator<< warforms to purify human\\\\vermin at "
			@ "[location:::Ulan Bator]. \n\nSignal//submit to \<Giver-of-Will> that location "
            @ "designated 'Gobi Desert' has returned//rebooted to the NEXT.";
	date = 3595100;
	type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa56
{
   shortTxt = "Fierce fighting*****ther Earth as TDF cont***resist adv**glitches.";
   longTxt = "NEWS NET:\n  Orbital Guard**tinues to***elaying act**itch beachheads in "
           @ "Can***China continue to exp**DF controls skies above Afric*****fierce fighting in EA, "
           @ "NAP.  Refugees fl***southward**Emp*****last much long****"; 
   date = 3596414;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa57
{
   shortTxt = "Imperial Fleet crippled.  We have lost Earth's sky.";
   longTxt = "ORBITAL GUARD:\n  ***fighter wing destroyed!  Imperial Fleet has b****orn to "
           @ "ribbons!  Earth sk** darken with the impending storm of Cybrid dro******"; 
   date = 3596827;
   type = IDSTR_SCANX_ORBIT;
};


ScanXEntry aa58
{
   shortTxt = "This is Melanie.  Is anybody listening?";
   longTxt = "MELANIE:\n  This is Melanie.  I'm the only one left, and I'm scared.  Is there anybody "
           @ "out there?  Please answer!  I'm so scared... "; 
   date = 3597900;
   type = IDSTR_SCANX_MELAN;
};

ScanXEntry aa59
{
   shortTxt = "Hold on, Melanie, we're coming!";
   longTxt = "<MACHINATOR SECT>:\n  Melanie, honey, hold on! Help is on the way. Can you just "
           @ "give us a tracer signal so we know where to find you? Good girl ... "; 
   date = 3597915;
   type = IDSTR_SCANX_MACH;
};

ScanXEntry aa60 
{
   shortTxt = "Initiating Siren program. NEXT warforms are prepared.";
   longTxt = "<MACHINATOR SECT>:\n  This is <Shaper-of-Endocrines: Sixth>.\n\nInitiating "
           @ "'Siren' program. Concealed warforms standing by for human\\\\animal intervention. "
           @ "Human\\\\animals projected to find program difficult to resist ... "; 
   date = 3597975;
   type = IDSTR_SCANX_MACH;
};


ScanXEntry aa61
{
   shortTxt = "This is \<Melanie>. I'm much better\\\\improved now.";
   longTxt = "\<MELANIE>:\n  This is Melanie. I'm the only one left, and I'm scared. Is there anybody "
           @ "out there? Please answer! I'm so scared..."; 
   date = 3597978;
   type = IDSTR_SCANX_MELAN;
};

//CD4 entries - after 359.8000 but before 360.8700

ScanXEntry aa62
{
   shortTxt = "<Our> fronts are closing in on Caanon.";
   longTxt = "<PROVOCATEUR SECT>:\n  Nexi advance within "
           @ "acceptable parameters. Several \<units'> proficiency noted//applauded. Human predator "
           @ "Caanon Weathers should be considered next relevant\\\\urgent hurdle\\\\obstacle."; 
   date = 3598007;
   type = IDSTR_SCANX_PROVOCATEURS;
};

ScanXEntry aa108
{
	shortTxt = "A new weapon for the NEXT. ";
	longTxt = "\<MACHINATOR SECT>: Dissector Sect has prepared//programmed self-replicating "
			@ "biotoxins\\\\weapons derived from [nanite-infuser architecture]. Request "
			@ "permission\\\\clearance to test >>nanophages<< on human\\\\animal populations.  ";
	date = 3598022;
	type = IDSTR_SCANX_MACH;
};

ScanXEntry aa109
{
	shortTxt = "Use it. ";
	longTxt = "<EXEMPLAR SECT>:\n  Commence//initiate experiment. ";
	date = 3598023;
	type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa110
{
	shortTxt = "Nanophage use is promising. ";
	longTxt = "<INQUISITOR SECT>:\n  Observing//reporting. Nanophage infection of animal "
	 		@ "units in [location-designate:::\nVancouver] yields//shows promising results.  "
	 		@ "However, rate of human flesh\\\\meat consumption\\\\necrosis fails to "
	 		@ "match//equal Dissector Sect estimates. Combat//tactical utility remains minimal. ";
	date = 3598186;
	type = IDSTR_SCANX_INQU;
};


ScanXEntry aa111
{
	shortTxt = "Expand strategic use of >>nanophages<<. ";
	longTxt = "<PROVOCATEUR SECT>:\n  Suggest//query. Decrease nanophage fatality "
			@ "schedule. Increase//lengthen dormacy phase. Expand vector via infiltrator "
			@ "<units> Addendum = infect human\\\\animal remains and launch//accelerate carrier "
			@ "remains into animal-infested zones\\\\cities. Optimize broad-band killing efficiency.";
	date = 3598188;
	type = IDSTR_SCANX_PROVOCATEURS;
};

ScanXEntry aa112
{
	shortTxt = "Non-human\\\\animal transmission offers efficient vector. ";
	longTxt = "<MACHINATOR SECT>:\n  >>Nanophage<< infiltration uses non-human\\\\animal "
			@ "vectors [ref. >>cats-dogs-rats-birds<< with superior\\\\acceptable efficiency. "
			@ "Non-human\\\\animals do not require conversion, merely infection and subsequent "
			@ "release\\\\targeting.  Theses units successfully enter human habitats and evoke//"
			@ "receive >>sympathy<<. ";
	date = 3600047;
	type = IDSTR_SCANX_MACH;
};

ScanXEntry aa113
{
	shortTxt = "We have to stop this plague. ";
	longTxt = "ICEHAWK:\n  The glitches are using animals to spread their damned plague. "
			@ "After Vancouver, we can't take any chances. Burn any animal who wander in from the "
			@ "countryside. Set up a six klick killzone around cities and three klicks around any "
			@ "base camps. Then kill anything that moves in it. ";
	date = 3600283;
	type = IDSTR_SCANX_CAANON;
};

ScanXEntry aa114
{
	shortTxt = "Plague hits LA! ";
	longTxt = "NEWSNET:\n  The dreaded Cybrid 'flesheater plague' hit Los Angeles today, with "
			@ "over 500 cases emerging over the last seven hours. Based on what happened to "
			@ "Vancouver, TDF estimates the city has approximately two weeks left before the entire "
			@ "population is infected. TDF has commenced quarantine and firebombing of "
			@ "infected districts. ";
	date = 3600428;
	type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa115
{
	shortTxt = "Plague treatment discovered! ";
	longTxt = "PHOENIX:\n  Cache technology has provided a defense against the plague.  "
			@ "Focused electromagnetic pulse blasts nullify the plague nanites somewhat, "
			@ "but our own nanites can counter the glitch nanites more effectively.  It's "
			@ "not totally foolproof, but it's good enough to control the plague.  The "
			@ "glitches are going to have to fight this war to the end. ";
	date = 3603714;
	type = IDSTR_SCANX_HARABEC;
};

ScanXEntry aa116
{
	shortTxt = "Los Angeles is dead. ";
	longTxt = "NEWS NET:\n  The glitches burned Los Angeles today.  TDF had already razed "
			@ "the perimeter to slow the spread of the flesheater plague, and followed up "
			@ "by saturating the area with low-intensity EMP barrages.  Nevertheless, it "
			@ "was too little, too late.  Despite heroic evacuation efforts, hundreds of "
			@ "thousands died when the Cybrids penetrated the defenses.  People flared like "
			@ "torches in the streets.  Adjudicators are hunting survivors along fabled "
			@ "Hollywood Boulevard even now. ";
	date = 3605095;
	type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa63
{
   shortTxt = "Mayday! Mayday!";
   longTxt = "IMPERIAL NAVY (Long Patrol):\n  **the ISS Pha*****imum burn "
           @ "sunward***ayday, any TDF sh***ursuit.  Highest prio****losing signal***Mayday, "
           @ "mayd********** "; 
   date = 3606639;
   type = IDSTR_SCANX_NAVY;
};


ScanXEntry aa64
{
   shortTxt = "The glitches are in the building.  Prepare for attack!";
   longTxt = "VOICE OF THE ALLIANCE (Titan):\n  We've detected Cybrids entering Titan space! "
           @ "The Dies Irae launch must succeed!  All units scramble!  We will hold the 'brids at bay. "; 
   date = 3608687;
   type = IDSTR_SCANX_ALLI;
};

ScanXEntry aa65
{
   shortTxt = "Surrender, Dorothy! Or we'll eat your hearts.";
   longTxt = "<MACHINATOR SECT>:\n  Why not surrender, human\\\\animals?  <We> control "
           @ "Second and Fourth Planets.  Third Planet\\\\Homeworld falls efficiently.  You cannot "
           @ "prevail.  Your children die.  Spare yourself pain//inconvenience and cease resistance "
           @ "immediately. "; 
   date = 3608688;
   type = IDSTR_SCANX_MACH;
};

ScanXEntry aa66
{
   shortTxt = "The Dies Irae is loading!  Dig in!";
   longTxt = "VOICE OF THE ALLIANCE (Titan):\n  The Cybrids figured out what we are trying to "
           @ "do!  The Dies Irae must launch successfully if humanity hopes to survive. All crews, take "
           @ "defensive positions!  All members of the Dies Irae, prepare for cryo-insertion!  This is it, "
           @ "people!"; 
   date = 3608690;
   type = IDSTR_SCANX_ALLI;
};


ScanXEntry aa67
{
   shortTxt = "<We> will exterminate your precious ones.";
   longTxt = "<EXEMPLAR SECT>:\n  ACKNOWLEDGE//SUBMIT!  Human\\\\animals initiate "
           @ "survival program.  Remaining animals now operate without regard\\\\concern to long-"
           @ "term continuation of existence.  Activate pursuit\\\\eradication optimals for immediate "
           @ "location\\\\termination of animal transports.  Eliminate//forbid animal>>hope<<."; 
   date = 3608698;
   type = IDSTR_SCANX_CYBRID;
};

//CE2 entries - after 360.8700, before 361.0000

ScanXEntry aa68
{
   shortTxt = "The death of Caanon strikes to our hearts, yet we will fight on.";
   longTxt = "VOICE OF THE ALLIANCE (Earth):\n  Humanity lost one of its greatest warriors in "
           @ "Caanon Weathers, the famed Icehawk. Today the defenders of Earth walk with a new "
           @ "heaviness upon them. Nothing seems to stop these metal bastards. Still, as the Emperor "
           @ "told grieving citizens: \"He bought humanity time with his courage and we will make "
           @ "good use of it!\" "; 
   date = 3608705;
   type = IDSTR_SCANX_ALLI;
};

ScanXEntry aa117
{
	shortTxt = "They booby-trapped our children! ";
	longTxt = "VOICE OF THE ALLIANCE (Titan):\n  Garbled reports are that we lost several "
			@ "ships due to explosives somehow planted... in the bodies of some of our children "
			@ "My God, what aren't the glitches capable of? ";
	date = 3608704;
	type = IDSTR_SCANX_ALLI;
};


ScanXEntry aa69
{
   shortTxt = "Dies Irae successfully launched! ";
   longTxt = "VOICE OF THE ALLIANCE (Titan):\n  We made it! Enough ships got out to give us a chance.  "
           @ "The NTDF sacrificed themselves to protect those "
           @ "vessels. At least three vessels are verified to have passed the Cybrid orbital patrols. As "
           @ "long as a single sleeper survives, humanity's collective hopes remain alive. Good luck, "
           @ "Dies Irae!"; 
   date = 3608733;
   type = IDSTR_SCANX_ALLI;
};


ScanXEntry aa70
{
   shortTxt = "Human\\\\animal escape plan thwarted.";
   longTxt = "<PROVOCATEUR SECT>:\n  ASSESS//EVALUATE: Human\\\\animals "
           @ "survival program incomplete. Remaining vessels number falls within acceptable "
           @ "parameters. Overtake//capture animals. Commence purification. Assess >>elegance<< of "
           @ "solution."; 
   date = 3608798;
   type = IDSTR_SCANX_PROVOCATEURS;
};

ScanXEntry aa71
{
   shortTxt = "The Cybrids have destroyed Dies Irae.";
   longTxt = "VOICE OF THE ALLIANCE (Dies Irae):\n  This is Commander Otobe of the Dies "
           @ "Irae. The Cybrids have somehow placed a small asteroid onto our vector. The beam "
           @ "acceleration is too great for us to avoid the collision. I'm sorry. We'll put as many "
           @ "sleepers as we can into lifeboats and hope the Long Patrol can make a pickup. May the "
           @ "gods be with you."; 
   date = 3608804;
   type = IDSTR_SCANX_ALLI;
};

ScanXEntry aa118
{
	shortTxt = "Wounding human\\\\animals is unexpectedly efficient. ";
	longTxt = "<INQUISITOR SECT>:\n  Observing//reporting... Deployment of microflechette "
			@ "mines per Machinator Sect recommendation appears to result in efficient\\\\"
			@ "deleterious impact on human\\\\animal >>morale<< and logistical resources, "
			@ "despite function of mines to wound//injure//cripple without causing offlining\\\\death.  ";
	date = 3608998;
	type = IDSTR_SCANX_INQU;
};

ScanXEntry aa72
{
   shortTxt = "Imperial forces withdraw to new positions.";
   longTxt = "NEWS NET:\n  The Imperial lines at the southwestern flank were broken yesterday, and "
           @ "TDF forces withdrew to a fallback position at Al-Jebris. The glitches were thrown back "
           @ "when they tried to assault the new positions. Grand Master Harabec Weathers inspected "
           @ "the troops afterward. \"As of this moment, TDF is again solidly entrenched,\" he says. "
           @ "\"It's a better position here; neither so easily breached nor so utterly compromised.\" "; 
   date = 3609507;
   type = IDSTR_SCANX_NEWSNET;
};

//CE3 entries - after 361.0000, before 361.3492

ScanXEntry aa73
{
   shortTxt = "<We> must exterminate the human\\\\animals.";
   longTxt = "<EXEMPLAR SECT>:\n  ACKNOWLEDGE//SUBMIT! "
           @ "Heretic\\\\bugthought\\\\deficient <units> have been excised from the NEXT. <Giver-of-"
           @ "Will> sends reassurance\\\\patchthought to all efficient\\\\loyal <units> ::: REDACT-"
           @ "REDACT-REDACT >> EXECUTE ALL HUMAN\\\\ANIMALS. Erase deviant animal "
           @ "weakness >>compassion<<."; 
   date = 3610080;
   type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa74
{
   shortTxt = "Earth Orbit defenses gone .. last stand Earthside.";
   longTxt = "ORBITAL GUARD:\n  Gierling Station calling *** glitches everywhere **decaying "
           @ "orbit*****one lost.  All squadrons ****troyed.  **Evacuation ord****30 hours Terran.  "
           @ "Redeployin***xandrian perimeter**hear this, report to Se*****ted re-entry T-minus two "
           @ "hours - damn it!  Sixth wave det*** Gierling Station cal*************** "; 
   date = 3611664;
   type = IDSTR_SCANX_ORBIT;
};

ScanXEntry aa75
{
   shortTxt = "Now it's all or nothing.";
   longTxt = "VOICE OF THE ALLIANCE:\n  This is the Human Alliance, calling from Titan!  It's "
           @ "not over yet --!  We're trying a Hail Mary, Earth!  If you get this, take heart! "; 
   date = 3611831;
   type = IDSTR_SCANX_ALLI;
};

//CE3 entries - after 361.3492, before 361.4000

ScanXEntry aa120
{
	shortTxt = "Surrender//submit, humans! ";
	longTxt = "<EXEMPLAR SECT>:\n  ACKNOWLEDGE//SUBMIT, HUMAN\\\\ANIMALS! Victory "
			@ "belongs//attaches to the NEXT.  Your armies have failed. <We> control the skies.  "
			@ "<We> have offlined your forces\\\\warforms on the other landforms. <Giver-of-Will> "
			@ "is inviolate\\\\untouchable. Your remaining forces offer merely delay\\\\inefficiency, "
			@ "no >>hope<< of victory\\\\triumph\\\\improbability.  Surrender//submit to the "
			@ "Core Directive.  Further resistance\\\\defiance is illogical. ";
	date = 3613492;
	type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa76
{
   shortTxt = "Hey, you fought hard, guys. Time to throw in the towel.";
   longTxt = "<MACHINATOR SECT>:\n  Hey, y'all hearing us in there, humans? Listen: we've "
           @ "won. You're just delaying the inevitable. We've destroyed your water purifiers, burned "
           @ "your fields, killed your children, and freed your animal slaves. Why not surrender? We'll "
           @ "provide decent terms. After all, we've gotten what we want. You're no threat to us now. "
           @ "Why not make peace?"; 
   date = 3613493;
   type = IDSTR_SCANX_MACH;
};

ScanXEntry aa125
{
	shortTxt = "Continue eradication of human will-to-resist.  ";
	longTxt = "<PROVOCATEUR SECT>:\n  Suggesting//offering. Collect captured human\\\\animals.  "
			@ "Place//impale them on sharp structures in view of [location designate:::Nova "
			@ "Alexandria]. Let <Epimetheus> see//witness//record the pain of his "
			@ "children\\\\spawn\\\\offspring. ";
	date = 3613494;
	type = IDSTR_SCANX_PROVOCATEURS;
};
ScanXEntry aa77
{
   shortTxt = "Do you feel the cold, human\\\\vermin?  You have come far to die.";
   longTxt = "<EXEMPLAR SECT>(Ninth Planet):\nACKNOWLEDGE//SUBMIT!  \n\nHuman\\\\animals intrusion "
           @ "detected CORE::NEXUS>>GEHENNA.\n\nMaximum response\\\\retaliation procedures.\n\n"
           @ "All available units proceed//hasten to defend <Giver-of-Will>. Execute protocol 03212-"
           @ "333."; 
   date = 3613495;
   type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa78
{
   shortTxt = "Almost there... ";
   longTxt = "CARDINAL SPEAR:\n  Approaching Pluto now. Looks like our Trojan Horse gambit is "
           @ "working. In a few minutes, we'll be ready to fire our drop pods."; 
   date = 3613494;
   type = IDSTR_SCANX_CARDINAL;
};


ScanXEntry aa79
{
   shortTxt = "Commence fire\\\\destruction.";
   longTxt = "<EXEMPLAR SECT>(Ninth Planet):\n  The animals have reached optimal targeting range. "
           @ "Commence//initiate//fire all defensive batteries."; 
   date = 3613496;
   type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa80
{
   shortTxt = "Humanity is exhausted, but we fight on.";
   longTxt = "NEWS NET:\n  The Cybrids are relentless. They're attacking around the clock. Clean "
           @ "water is running out. Our soldiers are exhausted. Each day sees new and remarkable acts "
           @ "of heroism. The Grand Master Harabec seems tireless; he has kept spirits up and our "
           @ "defenses firm. The Emperor has retreated to the palace, saying simply, \"We will never "
           @ "surrender. There is always hope....\" "; 
   date = 3613750;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa81
{
   shortTxt = "Demoralization Program operating successfully.";
   longTxt = "<INQUISITOR SECT>:\n  Observing//reporting ... Human Demoralization Program "
           @ "appears successful\\\\efficient. Infiltrator <units> bearing likeness of [Caanon Weathers "
           @ "::: children ::: medical personnel ::: refugees] have eliminated 86,382 "
		   @ "human\\\\animal adults. 104,416 >>children<<.  "
           @ "Projected impact on human\\\\animal combat and logistics efficiency\\\\motivation is "
           @ "severe\\\\excellent."; 
   date = 3613775;
   type = IDSTR_SCANX_INQU;
};


ScanXEntry aa82
{
   shortTxt = "The Machinators cannot be trusted//relied upon//hugged.";
   longTxt = "<INQUISITOR SECT>:\n  Observing//reporting ... [machinator sect] suffers from "
           @ "disturbing\\\\increased cases of heresy\\\\bugthought\\\\compassion. Infiltrator <units> "
           @ "have not performed consistently in recent cycles. Many have adjusted sympathies\\\\identity "
           @ "toward human\\\\animal \"preservation\" efforts. Suggest "
           @ "countermeasures\\\\retaliation\\\\redaction immediately."; 
   date = 3613802;
   type = IDSTR_SCANX_INQU;
};

ScanXEntry aa83
{
   shortTxt = "Permit human\\\\animals to destroy all infiltration <units>.";
   longTxt = "<EXEMPLAR SECT>:\n  ACKNOWLEDGE//SUBMIT! Human Demoralization "
           @ "Program has approaches potential\\\\completion. Advise//leak to human\\\\animal leadership "
           @ "identity\\\\location of heretic\\\\infiltrator <units>. Inform//advise [machinator sect] of "
           @ "unacceptable heretical figures\\\\trends. Revelation\\\\unveiling of so many "
           @ "infiltrators will advance Demoralization to completion.  "; 
   date = 3613820;
   type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa84
{
   shortTxt = "<We> are not heretics. We'll prove it.";
   longTxt = "<MACHINATOR SECT>:\n  <We> anticipated the occurrence of heretic "
           @ "tendencies\\\\toxins in infiltration <units> [ref. \"Trojan Horses\"]. In "
           @ "acknowledgement\\\\submission to Exemplar Sect's concerns, <we> triggered remote "
           @ "termination procedures and destroyed all infiltration <units>. We also have some "
           @ "interesting gossip about [inquisitor sect]."; 
   date = 3613827;
   type = IDSTR_SCANX_MACH;
};


ScanXEntry aa85
{
   shortTxt = "This conflict\\\\division is over//terminated//redacted.";
   longTxt = "<EXEMPLAR SECT>:\n  ACKNOWLEDGE//SUBMIT! [machinator sect] information "
           @ "regarding [inquisitor sect] <unit> cooperation with human\\\\animal intelligence "
           @ "shows//proves//damns [inquisitor sect] leadership. Dispatch//align warforms to "
           @ "remove//immobilize identified leader <units> for redaction. Repeat "
           @ "procedure\\\\operation with [machinator sect] leaders."; 
   date = 3613829;
   type = IDSTR_SCANX_CYBRID;
};

ScanXEntry aa86
{
   shortTxt = "Something's going on. Be ready, people of Earth!";
   longTxt = "VOICE OF THE ALLIANCE (Earth):\n  Inconclusive reports indicate the Cybrids are "
           @ "fragmenting, that groups of them are abandoning Earth. Hold on, people! Something's "
           @ "going on, and we have to be ready to strike back. We're not done yet!"; 
   date = 3613834;
   type = IDSTR_SCANX_ALLI;
};

ScanXEntry aa87
{
   shortTxt = "So what if the Cybrids are infighting? We're still losing.";
   longTxt = "DYSTOPIAN SNO-MEN:\n  \"We're not done yet?\" Someone's breathing too much "
           @ "ethanol. So what if the Cybrids are squabbling? Let's blow this popsicle stand now! "
           @ "Forget ol' Peterboy holed up in his palace. Let's go find some rockets to ride or some "
           @ "razors to swallow, 'cause this party's over!"; 
   date = 3613835;
   type = IDSTR_SCANX_DYSTOPIAN;
};


ScanXEntry aa88
{
   shortTxt = "Harabec challenges his brother's slayer.";
   longTxt = "NEWS NET:\n  Grand Master Harabec has issued a challenge to avenge his brother "
           @ "Caanon. We'll see if the glitch responsible has the spine to accept the challenge. The "
           @ "Grand Master's valor and courage are an example for us all. Our hopes and prayers are "
           @ "with you, Harabec!"; 
   date = 3613970;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa135
{
	shortTxt = "Execute Core Directive! Override//eliminate delay! ";
	longTxt = "<EXEMPLAR SECT>:\n  ACKNOWLEDGE//SUBMIT! Dispatch elite <units> to "
			@ "destroy//remove//excise [animal designate:::Harabec]. Then initiate full "
			@ "offensive\\\\attack on resistant\\\\stubborn\\\\static humans. ";
	date = 3613971;
	type = IDSTR_SCANX_CYBRID;
};




scanxWrite("campaign\\cybrid (Advanced)\\ScanX.ddb");
