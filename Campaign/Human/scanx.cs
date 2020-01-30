//------------------------------------------------------------------------------
// ScanX Data file
//
// the structure of ScanX Data is:
//
//  ScanX <uniqueName>
//  {
//    shortTxt; // string:  displayed in the news ticker
//    longTxt;  // string:  displayed in the omni web  
//    date;     // int:     used to sort the scanX entries - displayed in news
//                          use 8-digt number for the date.  A decimal will 
//                          be automatically placed into the date after the 4th digit 
// ticker and omni web
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
//					     IDSTR_SCANX_PROVOCATEURS
//					     IDSTR_SCANX_DYSTOPIAN
//					     IDSTR_SCANX_CARDINAL
//                  IDSTR_SCANX_CAANON,
//                  IDSTR_SCANX_HARABEC,
//  }
//     
//------------------------------------------------------------------------------

//HA0 entries - before 2828.1002
ScanXEntry aa1 
{
   //HA0
   shortTxt = "Emperor claims rights to all colonial resources.";
   longTxt = "NEWS NET HISTORICAL ARCHIVES:\n August 1, 2770 will be remembered as a historic date in the history of the Empire. "
           @ "His Imperial Majesty presented the Fortress Earth Proclamations today before an "
           @ "enthusiastic crowd at the Good Fortune Pavilion in Nova Alexandria. Response from the "
           @ "colonies is less enthusiastic, as colonial representatives call "
           @ "the Proclamations \"heavy-handed exploitation.\" More clear-eyed citizens, however, harbor no "
           @ "illusions about the overriding need to defend Earth, the ultimate target of the Cybrids.";
   date = 27705823;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa2 
{
   //HA0
   shortTxt = "Cybrid brain found in accident victim skull.";
   longTxt = "NEWS NET HISTORICAL ARCHIVES:\nCybrids walk among us! Although officials have clamped down on further "
           @ "details, we do know that a hovercar accident in Mega-L.A. led to the discovery by startled "
           @ "medics of a cybernetic brain in one of the victims. How many more Cybrid spies are out "
           @ "there? ";
   date = 28140523;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa3
{
   //HA0
   shortTxt = "\"Phoenix\" resigns from Imperial Knights.";
   longTxt = "NEWS NET HISTORICAL ARCHIVES:\nHarabec Weathers, the infamous \"Phoenix,\" resigned his commission "
           @ "following a closed-door session with His Imperial Majesty. Observers speculate the "
           @ "decision arose from Weathers' role in the disastrous Turkhazakistan defeat earlier this "
           @ "year. Grand Master Caanon Weathers could not be reached for comment, but sources "
           @ "close to the Weathers say the news has saddened the family. ";
   date = 28197301;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa6
{
   //HA0
   shortTxt = "Straight talk from the digital resistance!";
   longTxt = "NEWS NET HISTORI**\<file break>**\n\n***\<file restored>\n\nDYSTOPIAN SNO-MEN:\nReading some "
           @ "history? News Net jingles out a lot of happy-joy crap about how great "
           @ "things are here on Earth.\n\nYay, Empire, and all that.\n\nWell, we're gonna pop "
           @ "some of those nice comfy bubbles, so all you citizens out there keep an eye on the "
           @ "O-Web. We're out there along with the truth, your friendly shadows in the snow. ";
   date = 28258376;
   type = IDSTR_SCANX_DYSTOPIAN;
};

ScanXEntry aa4
{
   //HA0
   shortTxt = "Emperor increases colonial quotas.";
   longTxt = "NEWS NET HISTORICAL ARCHIVES:\nHis Imperial Majesty permitted increased colonial quotas this week despite "
           @ "allegations that production pressures were responsible for recent fatal accidents in "
           @ "Venusian mining operations. Any protests on Mars and Venus will be dealt with severely. ";
   date = 28258710;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa5
{
  //HA0
   shortTxt = "Empire makes example of Venusian insurgents.";
   longTxt = "NEWS NET HISTORICAL ARCHIVES:\nHis Imperial Majesty announced today that Strikeforce Rocking Horse has "
           @ "been dispatched to Venus as a peacekeeping measure. The Emperor expressed deep regret "
           @ "over the escalation of the Palusteri crisis, but declared that such arrogant "
           @ "defiance would not be tolerated. \n\n"
           @ "\"No one who challenges the laws of the Empire will escape punishment. "
           @ "All insurgency members will be executed.\"\n\n*************************\n\<END REVIEW> "
           @ "\n\n\<RESET PRESENT YEAR>\n\n*************************\n";
   date = 28264598;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa7
{
   //HA0
   shortTxt = "Martian strike resolved peacefully.";
   longTxt = "IMPERIAL POLICE (Mars):\nImperial Police peacefully faced down striking miners at a "
           @ "Tharsis City facility on Mars yesterday. Several miners were cited and released. ";
   date = 28280170;
   type = IDSTR_SCANX_POLICE;
};

ScanXEntry aa8
{
   //HA0
   shortTxt = "Imp Lice brutally crushed a peaceful strike!";
   longTxt = "VOICE OF FREE MARS:\n\"Cited and released.\" Who are they kidding? The Imp Lice "
           @ "left 'em bleeding in the street after \"citing\" them with stunstaves! ";
   date = 28280174;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa11
{
   //HA0
   shortTxt = "Cybrid signals detected. Do not panic. Exercise caution.";
   longTxt = "IMPERIAL NAVY:\nCybrid transmissions have been detected in near-Venus space! All "
           @ "ships on Venusian approach vectors are warned to exercise extreme caution! ";
   date = 28280200;
   type = IDSTR_SCANX_NAVY;
};

ScanXEntry aa12
{
   //HA0
   shortTxt = "Cybrids. Riiight. How conveniently distracting...! ";
   longTxt = "VOICE OF FREE MARS:\nOoooo. Cybrids. Citizens, don't pay any attention to the "
           @ "tyrant behind the curtain. Pay attention to the big scary Cybrids. ";
   date = 28280220;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa13
{
   //HA0
   shortTxt = "The Navy is on full alert status. TDF is ready.";
   longTxt = "IMPERIAL NAVY:\nSeventh and Ninth Strike Fleets to hold maneuvers off Venus this "
           @ "week. TDF is confident in its ability to defend Imperial citizenry. ";
   date = 28280850;
   type = IDSTR_SCANX_NAVY;
};

ScanXEntry aa14
{
   //HA0
   shortTxt = "TDF reassures public as it confirms location of Cybrid signals.";
   longTxt = "NEWS NET: TDF tracking Cybrid transmissions. Citizens are urged to remain calm. ";
   date = 28281001;
   type = IDSTR_SCANX_NEWSNET;
};

//HA1 entries - after 2828.1002, before 2828.1131

ScanXEntry aa20
{
   //HA1
   shortTxt = "Imperial cargo ship goes down on Mars.";
   longTxt = "NEWS NET:\nAn Imperial cargo transport went down on Mars today. Equipment "
           @ "malfunction was to blame, and no one was hurt. \"Just another day on the job,\" says the "
           @ "Imperial Security Director for Mars, Ernesto Navarre. \"Nothing for the law-abiding "
           @ "citizens of Mars to worry about.\" ";
   date = 28281009;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa15
{
   //HA1
   shortTxt = "\"Cybrids\" were a rebel trick!";
   longTxt = "NEWS NET:\nRecent \"Cybrid transmissions\" discovered to be a rebel hoax to draw off "
           @ "TDF interceptors from smugglers. Imperial Navy will exercise extreme skepticism in "
           @ "future, Admiral Vladijon says. ";
   date = 28281010;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa21
{
   //HA1
   shortTxt = "Successful raid on terrorist base!";
   longTxt = "IMPERIAL POLICE (Mars):\nA terrorist base was located and eradicated today by "
           @ "members of the Sixth Tharsis Ranger Patrol. The terrorists were using a freejack mining "
           @ "station as a lair. The patrol took no prisoners. ";
   date = 28281011;
   type = IDSTR_SCANX_POLICE;
};
ScanXEntry aa22
{
   //HA1
   shortTxt = "Lice murdered an innocent miner!";
   longTxt = "VOICE OF FREE MARS:\nThe Sixth Tharsis \"raid\" happened when the Lice took a fancy "
           @ "to some miner's daughter! When the miner protested, the patrol took out the poor "
           @ "bastard's rigs and hanged him from the wreckage alongside his two crew members! We "
           @ "still don't know what happened to the girl. ";
   date = 28281013;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa9
{
   //HA2
   shortTxt = "Don't associate with subversive elements!";
   longTxt = "IMPERIAL POLICE (Mars):\nDue to the number of violent criminals filling our prison "
           @ "space, Director Navarre has authorized magistrates to impose longer sentences of "
           @ "chemical incarceration for non-violent convicts. All citizens are urged to avoid contact "
           @ "with subversives. ";
   date = 28281024;
   type = IDSTR_SCANX_POLICE;
};

ScanXEntry aa10
{
   //HA2
   shortTxt = "Let's all help the nice police, hey?";
   longTxt = "DYSTOPIAN SNO-MEN:\n\"Subversive\" is just a government-defined term for people with "
           @ "questions! ";
   date = 28281025;
   type = IDSTR_SCANX_DYSTOPIAN;
};

//HA2 entries - after 2828.1131, before 2828.2211

ScanXEntry aa23
{
   //HA2
   shortTxt = "Terrorists ambush an Imperial supply convoy on Mars.";
   longTxt = "NEWS NET:\nAn Imperial supply convoy in the Syrtis region on Mars was ambushed by "
           @ "terrorist raiders. Police casualties were minimal. A rise in terrorist activity has been "
           @ "reported since then. Imperial Security Director Navarre denies any escalation of the "
           @ "conflict. ";
   date = 28281151;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa24
{
   //HA2
   shortTxt = "Raiders are desperate, ill-equipped.";
   longTxt = "IMPERIAL POLICE (Mars):\nDirector Navarre reassures citizens. \"The raiders are "
           @ "hitting our supply lines because they're desperate. They're using makeshift weaponry and "
           @ "hit-and-run tactics. Any reports of advanced weapons in raider hands are simply media "
           @ "exaggeration.\" ";
   date = 28281174;
   type = IDSTR_SCANX_POLICE;
};

ScanXEntry aa33
{
   //HA2
   shortTxt = "Terrorist families in protective custody. ";
   longTxt = "IMPERIAL POLICE (Mars):\nChief Navarre has authorized Imperial Security units to "
           @ "take into protective custody family members of suspected terrorists. Outraged Martian "
           @ "citizens have threatened to lynch people with terrorist connections. Naturally, the Police "
           @ "want to prevent any unnecessary violence. ";
   date = 28281918;
   type = IDSTR_SCANX_POLICE;
};

ScanXEntry aa25
{
   //HA2
   shortTxt = "Curfew begins tomorrow!";
   longTxt = "IMPERIAL POLICE (Mars):\nDirector Navarre plans to deal conclusively with the "
           @ "current infestation of terrorists. The first step is to restore a sense of order. Starting "
           @ "tomorrow evening, the Imperial Security Forces will be enforcing a curfew. Any person "
           @ "out-of-doors after 1800 hours without proper authorization will be arrested. ";
   date = 28282015;
   type = IDSTR_SCANX_POLICE;
};

ScanXEntry aa55
{
   //HA2
   shortTxt = "Imperial Police interrogation methods remain humane. ";
   longTxt = "NEWS NET:\nImperial Security Director Navarre says vigorous interrogation of captured "
           @ "Martian rebels offers useful intelligence on rebel operations. \"We make minimal use of "
           @ "throe-clamps and psychrape,\" Navarre says. \"Nothing more than the situation demands.\" ";
   date = 28282038;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa56
{
   //HA2
   shortTxt = "Navarre uses the worst forms of torture! ";
   longTxt = "VOICE OF FREE MARS:\nNavarre has supervised interrogation using procedures that "
           @ "would shock a Cybrid. Some of our people have been cut apart with plasmatorches; others "
           @ "have been injected with psychedelic drugs and forced to watch their families tortured to "
           @ "death. We can't stomach the more disturbing stories. Suffice to say \"humane\" is not in "
           @ "\"the Chief's\" vocabulary. ";
   date = 28282045;
   type = IDSTR_SCANX_MARS;
};

//HA3 entries - after 2828.2211, before 2828.5633

ScanXEntry aa26
{
   //HA3
   shortTxt = "We got the goods to dust the Imp Lice!";
   longTxt = "VOICE OF FREE MARS:\nThis is the Voice of Free Mars, coming to you from "
           @ "somewhere out in the dust! Thanks for all the gear, Navarre! Word to the wise, boys 'n "
           @ "ghels of the Imp Lice: We're coming for you hardcore now! ";
   date = 28282228;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa27
{
   //HA3
   shortTxt = "Imperial Police strike at raider hideouts.";
   longTxt = "NEWS NET:\nImperial Police met little resistance as they overwhelmed several suspected "
           @ "raider hideouts. Although the missions are considered successes, none of the stolen "
           @ "armaments were recovered. Imperial Police Security Chief Navarre promises the missing "
           @ "weaponry and supplies will soon be recovered. ";
   date = 28283802;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa28
{
   //HA3
   shortTxt = "Navarre calls for public crusade against spies!";
   longTxt = "IMPERIAL POLICE (Mars):\nChief Navarre denies that the rebel insurgents have a "
           @ "broad base of popular support. \"But they have infiltrated our cities. Remember, all law-"
           @ "abiding citizens should keep a lookout for suspicious activity. If you see anything strange, "
           @ "it's your duty to report it. Citizens who fail to do so will be arrested for treason.\" ";
   date = 28283408;
   type = IDSTR_SCANX_POLICE;
};

ScanXEntry aa50
{
   //HA3
   shortTxt = "Imperial Police regroup. ";
   longTxt = "NEWS NET:\nImperial Police stand their ground bravely in the face of brutal rebel attacks "
           @ "\"It's not easy,\" one unnamed officer says. \"It's like fighting ghosts!\" "
           @ "Reports indicate the Imperial Police are turning the tide. "
           @ "The numbers of rebel prisoners grow by the day, and Chief Navarre's office has released "
           @ "a \"truly optimistic\" body count.";
   date = 28284600;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa29
{
   //HA3
   shortTxt = "Terrorist in Valles region. Pursue and apprehend.";
   longTxt = "IMPERIAL POLICE (Mars):\nAll units in North Valles Patrol Zone, scramble! A rebel "
           @ "force was caught in the open by our flyers. Our units destroyed two Hercs, but the third "
           @ "one got away. Our pilot debriefings indicate the reb is badly damaged. Orders are to try to "
           @ "take him alive. Alive enough for interrogation, that is. ";
   date = 28285617;
   type = IDSTR_SCANX_POLICE;
};

//HA4 entries - after 2828.5633, before 2828.8804

ScanXEntry aa30
{
   //HA4
   shortTxt = "Courageous rebel pilots evade Imperial goon squads!";
   longTxt = "VOICE OF FREE MARS:\nDiMarco's resting well after giving the slip to rabid Imp dog "
           @ "packs in Valles Province. She says she's ready to waste more Lice. Music to the ears of "
           @ "freedom lovers everywhere! You Lice, wipe the foam off your mouths and stop chasing "
           @ "your tails. Girl is long gone. ";
   date = 28285641;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa51
{
   //HA4
   shortTxt = "Imperial Police regroup. ";
   longTxt = "NEWS NET:\nImperial Security Director Navarre soothed critics of his campaign against "
           @ "Martian rebels. \"Stomach-turning rebel atrocities have shaken up many of our younger "
           @ "officers,\" he explained with a chuckle. \"Don't let a few panicky voices give the wrong "
           @ "impression. Everything is under control. The rebels are on the ropes.\" ";
   date = 28286180;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa52
{
   //HA4
   shortTxt = "We're tearing you Lice to pieces! ";
   longTxt = "VOICE OF FREE MARS:\nRight. If throwing dirtboys out of Victoria, Syrtis, and Pei-"
           @ "Shan means we're \"on the ropes,\" then Navarre is a sweet candidate for sainthood. Reality "
           @ "check, boyos: We're kickin' you ass over teakettle. ";
   date = 28286184;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa16
{
   //HA4
   shortTxt = "Terrorist bombing claims innocent lives!";
   longTxt = "NEWS NET:\nTerrorists bomb ISS Djakarta! Casualties include innocent civilian "
           @ "observers. The bomb was a macrothermal fusor that detonated shortly after the Djakarta "
           @ "departed Phobos Orbital Station above Mars. ";
   date = 28287029;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa17
{
   //HA4
   shortTxt = "Innocents die in war, people! Can't be helped, unfortunately.";
   longTxt = "VOICE OF FREE MARS:\nThe Free Martian Alliance accepts responsibility for the "
           @ "tragically necessary demise of the Djakarta. This is a war, people, and the Djakarta was a "
           @ "military vessel. Wake up! ";
   date = 28287032;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa18
{
   //HA4
   shortTxt = "The innocent blood is ultimately on the Emperor's hands!";
   longTxt = "VOICE OF FREE MARS:\nThe Martian Liberation Force regrets the death of civilians "
           @ "on the Djakarta. However, the blame lies with the Imperial Terran Defense Force and the "
           @ "heavy-handed policies of the Emperor. ";
   date = 28287737;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa19
{
   //HA4
   shortTxt = "So-called liberators are terrorists.";
   longTxt = "IMPERIAL POLICE (Mars):\nThese terrorists are murderers, pure and simple. They will "
           @ "be brought to justice, whatever the cost. These acts of violence will not be tolerated. "
           @ "Accordingly, we are executing one Martian prisoner for each casualty on the Djakarta. ";
   date = 28288441;
   type = IDSTR_SCANX_POLICE;
};

//HA5 entries - after 2828.8804, before 2829.3006

ScanXEntry aa31
{
   //HA5
   shortTxt = "Rebels slaughter peaceful scientists.";
   longTxt = "NEWS NET:\nRebel thugs on Mars raided an Imperial field research station yesterday and "
           @ "killed the scientists there when rebel demands for drugs and weapons were not met "
           @ "Imperial Police were grave after examining the victims' bodies. Security Director Navarre "
           @ "declares new strategies will be necessary to stop these kinds of brutal murders. ";
   date = 28288819;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa32
{
   //HA5
   shortTxt = "More Imperial lies!";
   longTxt = "VOICE OF FREE MARS:\n\"Drugs and weapons...?\" Riiiight. You can't come up with "
           @ "anything better, Navarre?  Get baked, brainburn! We're not dustin' tied up in "
           @ "that kind of stuff. You know why we hit that base... and we know \"field research stations\" "
           @ "don't usually warrant sophisticated military auto-defenses.";
   date = 28288887;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa34
{
   //HA5
   shortTxt = "Mars throws off the yoke of Empire. ";
   longTxt = "PHOENIX:\nThis is HARABEC WEATHERS, former Imperial Knight, now Provost-General of "
           @ "the Free Martian Republic. We bear no malice toward citizens of the Empire. Don't let "
           @ "your leaders drag you into a long war. Let it go. Leave us our freedom. As a Knight, I "
           @ "adopted the callsign \"Phoenix,\" a symbol of fire and rebirth. Today, Mars is reborn to the "
           @ "bright light of independence from the Emperor's tyranny. The flame of liberty burns in our "
           @ "hearts. Let it burn also in yours.... ";
   date = 28290158;
   type = IDSTR_SCANX_HARABEC;
};

ScanXEntry aa35
{
   //HA5
   shortTxt = "Right on, Phoenix! ";
   longTxt = "DYSTOPIAN SNO-MEN:\nGee, the Imperial ravelers seem rather interested in keeping "
           @ "this little Phoenix quiet. Let's see what a little datasphere path-clearing will do here. And "
           @ "maybe some extrrrra bandwidth into NewsNet's main pipe. ";
   date = 28290159;
   type = IDSTR_SCANX_DYSTOPIAN;
};

ScanXEntry aa36
{
   //HA5
   shortTxt = "Knight leads Martian rebels! Emperor betrayed! ";
   longTxt = "NEWS NET:\nMartian terrorists led by an ex-Imperial Knight broadcast a declaration of "
           @ "war against the Empire. Harabec Weathers, the former \"Phoenix,\" is believed mentally "
           @ "unbalanced, and the Emperor expressed sadness at the betrayal. Grand Master Caanon "
           @ "Weathers has stepped forward to lead the Imperial Knights on a mission of vengeance. "
           @ "The traitor will fall, the Grand Master vows. ";
   date = 28290160;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa37
{
   //HA5
   shortTxt = "Duke disowns traitor son! ";
   longTxt = "NEWS NET:\nThis morning, a saddened Duke Archibald Weathers confirmed the "
           @ "discovery that his youngest son Harabec leads the terrorist Martian Liberation Front. \"I "
           @ "disown this traitor,\" he said. \"He is not my son.\" ";
   date = 28290163;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa38
{
   //HA5
   shortTxt = "Harabec will be destroyed. ";
   longTxt = "ICEHAWK:\nIt is with deepest shame that I learned my brother Harabec leads those who "
           @ "rebel against the Empire and the man who led us to victory against the Cybrids. Let me "
           @ "make a solemn oath to the people of Earth: I will lay my brother's head at His Imperial "
           @ "Majesty's feet. The Weathers name will be cleansed. ";
   date = 28290164;
   type = IDSTR_SCANX_CAANON;
};

ScanXEntry aa39
{
   //HA5
   shortTxt = "People of Earth support war! ";
   longTxt = "NEWS NET:\nOutrage at Martian terrorism drove Imperial citizens onto the streets and "
           @ "O-Web yesterday in record numbers. O-Web polls strongly support teaching the Martians "
           @ "a lesson in respect. The cry is Vengeance! ";
   date = 28290165;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa40
{
   //HA5
   shortTxt = "People of Earth are sick of tyranny! ";
   longTxt = "DYSTOPIAN SNO-MEN: The cry is \"Freedom!\" The Web's on fire with demands for "
           @ "the Wings to loosen the chains here at home. R U listening, Peterboy? ";
   date = 28290166;
   type = IDSTR_SCANX_DYSTOPIAN;
};

ScanXEntry aa41
{
   //HA5
   shortTxt = "Rise up! Strike back! ";
   longTxt = "VOICE OF FREE MARS:\nThe Free Martian Republic calls upon those people who live "
           @ "in other places oppressed by the Empire to rise up and free yourselves! The Emperor "
           @ "doesn't own us! ";
   date = 28290169;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa42
{
   //HA5
   shortTxt = "Venus will also be FREE! ";
   longTxt = "UMBRAL THORN:\nToday, the Venusian resistance joins our Martian comrades in "
           @ "throwing off the chains of Empire. No longer shall we tolerate having the fruits of our "
           @ "labor sucked away by the parasitical Terran Defense Force.\n\n Free Venus! ";
   date = 28290172;
   type = IDSTR_SCANX_VENUS;
};

ScanXEntry aa43
{
   //HA5
   //(Use Cybrid INQUISITOR SECT LOGO here)
   shortTxt = "Human conflict escalates//improves. ";
   longTxt = "<unidentified transmission>:\nACKNOWLEDGE//SUBMIT! Escalation//iteration of "
           @ "division\\\\disharmony\\\\conflict between human\\\\animals || Third Planet and human\\\\animals "
           @ "|| Fourth Planet. Send//transmit//download data to <Giver of Will> for decision\\directive. ";
   date = 28290175;
   type = IDSTR_SCANX_INQU;
};

ScanXEntry aa44
{
   //HA5
   shortTxt = "Imp Lice, get the hell off our planet! ";
   longTxt = "VOICE OF FREE MARS:\nOkay, boys s'n ghels of the Imp Lice, now that we've shown "
           @ "our colors, it's time to see yours - specifically the color of your asses as you get the hell "
           @ "off our planet. We will no longer support your paranoid delusions or serve you like slaves! "
           @ "We will be free! ";
   date = 28290177;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa45
{
   //HA5
   shortTxt = "Phoenix assassinated! ";
   longTxt = "NEWS NET:\nThe rebel leader Harabec is dead. The patriot who fired the fatal shot has "
           @ "been identified as Stanley Michaelson. Rebel agents brutally executed him before he could "
           @ "escape. Although some people may disagree with assassination, haven't we all hoped "
           @ "someone would stop the Phoenix? Rest in peace, Michaelson. ";
   date = 28292168;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa46
{
   //HA5
   shortTxt = "Harabec will be missed! ";
   longTxt = "UMBRAL THORN:\n We grieve at the loss of the Phoenix. His spirit will live on in the "
           @ "deeds of those who strike for freedom. As an act of solidarity with our brothers and sisters "
           @ "on Mars, we're killing a mess of Imperial bootboys today. ";
   date = 28292169;
   type = IDSTR_SCANX_VENUS;
};

ScanXEntry aa47
{
   //HA5
   shortTxt = "Navarre calls on rebels to cease resistance. ";
   longTxt = "IMPERIAL POLICE (Mars):\nDirector Navarre believes the death of the Phoenix should "
           @ "make the rebels far easier for Imperial Police patrols to contain. \"The Phoenix had a "
           @ "certain dark charisma. Now he's out of the way. Perhaps the rebels should think twice "
           @ "before continuing their campaign of terror.\" ";
   date = 28292170;
   type = IDSTR_SCANX_POLICE;
};

ScanXEntry aa48
{
   //HA5
   shortTxt = "We're going to kill you, Navarre! ";
   longTxt = "VOICE OF FREE MARS:\nOkay, Navarre, it doesn't take an orbital plotter to see who "
           @ "Michaelson's handler was. Pray that you can breathe concrete, asshole. ";
   date = 28292171;
   type = IDSTR_SCANX_MARS;
};


ScanXEntry aa49
{
   //HA5
   shortTxt = "Sorry to disappoint you, \"Chief.\" ";
   longTxt = "PHOENIX:\nNice try, Navarre, but the reports of my demise were greatly exaggerated. "
           @ "I'm alive and well. Hope you like the boxes we sent Michaelson home in. ";
   date = 28292172;
   type = IDSTR_SCANX_HARABEC;
};

ScanXEntry aa53
{
   //HA5
   shortTxt = "\"Phoenix\" message is a rebel trick! ";
   longTxt = "IMPERIAL POLICE (Mars):\nDespite the recent Omni-Web message allegedly from the "
           @ "Phoenix, the traitor has not been active on the battlefield, and our pilots have made no "
           @ "sightings of the Phoenix's trademark colors. That message is a trick to deceive citizens of "
           @ "the Empire.";
   date = 28292485;
   type = IDSTR_SCANX_POLICE;
};

ScanXEntry aa54
{
   //HA5
   shortTxt = "Imperial Police regroup. ";
   longTxt = "NEWS NET:\nImperial Security Director Navarre confirmed reports that the rebels appear "
           @ "demoralized, and lack the innovation that marked the strategies used under the Phoenix. "
           @ "Imperial Police are gaining ground over the Martian insurgents, and \"the Chief\" believes "
           @ "the rebellion will be over by June. ";
   date = 28292564;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa57
{
   //HA5
   shortTxt = "Orderly withdrawal from Victoria. ";
   longTxt = "IMPERIAL POLICE (Mars):\nDispatch units to support our forces retreating from "
           @ "Victoria. The Chief is pulling out. Standard double cover formations. Don't let the reb "
           @ "ghosts slip inside the second perimeter. They should be busy enough with the thermal "
           @ "charges we seeded behind us, though. There won't be much Victoria for them to occupy. ";
   date = 28292643;
   type = IDSTR_SCANX_POLICE;
};

ScanXEntry aa58
{
   //HA5
   shortTxt = "Imperial Police regroup. ";
   longTxt = "NEWS NET:\nImperial Security Director Navarre has relocated his staff to the Imperial "
           @ "Police facility at Carter Flats. The structures there are heavily fortified and well-defended. "
           @ "Navarre could not be reached for comment, but his office says the Director wants to use "
           @ "the high-security communications uplink at the Carter Flats base during the final push "
           @ "against the insurgents. ";
   date = 28292704;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa59
{
   //HA5
   shortTxt = "New policy for checkpoints.";
   longTxt = "NAVARRE:\nArrest, search, confiscate. Loyal citizens will cooperate wholeheartedly. "
            @ "Interrogate and then shoot any detainees who protest. Leave their bodies in the dust.";
   date = 28292759;
   type = IDSTR_SCANX_POLICE;
};

ScanXEntry aa60
{
   //HA5
   shortTxt = "Imperial strikeforce burns for Mars! ";
   longTxt = "IMPERIAL NAVY:\nImperial Strikeforce \"Red Whirlwind,\" led by Grand Master Caanon "
           @ "Weathers, accelerated out of Luna's orbit a few moments ago, bound for Mars. The larger "
           @ "Imperial Fleet is still mustering, and will follow when transfer orbits are better aligned. "
           @ "Mars will become a convincing example of Imperial might. ";
   date = 28292762;
   type = IDSTR_SCANX_NAVY;
};

ScanXEntry aa61
{
   //HA5
   shortTxt = "This is wrong! ";
   longTxt = "DYSTOPIAN SNO-MEN:\nWhee! Wave flags. Cheer for our happy fascist deathsquad. "
           @ "WAKE UP, PEOPLE! ";
   date = 28292763;
   type = IDSTR_SCANX_DYSTOPIAN;
};

ScanXEntry aa62
{
   //HA5
   shortTxt = "Navarre says Imperial patience is over. ";
   longTxt = "NEWS NET:\nImperial Security Director Navarre announced new policies aimed at "
           @ "breaking rebel resistance. \"The rebels have a substantial network of sympathizers who "
           @ "undermine Imperial efforts and lend sustenance to the terrorists in the field. We've been "
           @ "lenient with these people until now, but my patience is at an end! Now we will crush them "
           @ "under our heels like the vermin they are....\" ";
   date = 28292764;
   type = IDSTR_SCANX_NEWSNET;
};


ScanXEntry aa65
{
   //HA5
   shortTxt = "New procedures following arrest. ";
   longTxt = "NAVARRE:\nI don't care if detainees are civilian noncombatants. If they're native "
           @ "Martians, kill them. ";
   date = 28292890;
   type = IDSTR_SCANX_POLICE;
};


//HA6 entries - after 2829.3006, before 2829.3103

ScanXEntry aa63
{
   //HA6
   shortTxt = "Valiant Police strike deadly blow at rebels! ";
   longTxt = "NEWS NET:\nImperial Police dealt a serious blow to rebel forces in Tharsis, Melas, and "
           @ "Ophir a short time ago, catching rebel raiders as they struck at civilian settlements. "
           @ "Citizens were evacuated safely, and the rebels sustained heavy losses. The rebels expected "
           @ "easy pickings, says a jubilant Security Director Navarre. \"They walked right into our little "
           @ "mouse trap!\" ";
   date = 28293023;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa64
{
   //HA6
   shortTxt = "No mercy, Navarre. By your rules. ";
   longTxt = "PHOENIX:\nOnly an utter scumbag would use innocent civilians as bait! No mercy, "
           @ "Navarre! You're gonna be the first one up against the wall when we win. Count on it, "
           @ "murderer. Your days are numbered. ";
   date = 28293024;
   type = IDSTR_SCANX_HARABEC;
};

ScanXEntry aa66
{
   //HA6
   shortTxt = "The Empire murdered innocent families in Tharsis! ";
   longTxt = "VOICE OF FREE MARS:\nSo this is how the high-and-mighty Empire plays, huh? "
           @ "Shooting suspects in the head? Murdering unarmed civilians? Putting children in prison "
           @ "camps? Guess what, stiffs? Freedom always wins in the end! ";
   date = 28293035;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa67
{
   //HA6
   shortTxt = "Navarre denies baseless rebel accusations. ";
   longTxt = "NEWS NET:\nImperial Security Director Navarre brushes aside rebel propaganda alleging "
           @ "Imperial atrocities. \"They're insane, obviously,\" he says gently. \"Imperial Police protect "
           @ "Martian citizens, especially innocent children. Only the guilty need be terrified.\" ";
   date = 28293039;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa68
{
   //HA6
   shortTxt = "We're shakin' in our boots, Lice! ";
   longTxt = "VOICE OF FREE MARS:\nOh, yeah, Mister Director, we're terrified all right. We're so "
           @ "terrified, we're on our way to Carter Flats to stick your head on a pike. We're so terrified, "
           @ "we're cutting cards for the chance to be the one to grab you by the ears and do the "
           @ "honors. ";
   date = 28293041;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa69
{
   //HA6
   shortTxt = "Imperial Police win great victory at Capri! ";
   longTxt = "NEWS NET:\nImperial Security forces on Mars pulled back after pummeling Martian "
           @ "insurgents in fierce combat at Capri Station. Police pilots attempted to minimize collateral "
           @ "damage to civilians who were exposed to the line of fire through rebel recklessness. ";
   date = 28293063;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa70
{
   //HA6
   shortTxt = "Lice slaughter civilian hostages! ";
   longTxt = "VOICE OF FREE MARS:\nIs Navarre insane? The Lice used truckloads of innocent "
           @ "prisoners as cover for their Hercs at Capri. Then they vaped the trucks on the way out! "
           @ "This is \"minimizing collateral damage\"? Bastards! ";
   date = 28293069;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa71
{
   //HA6
   shortTxt = "Updated policy for checkpoints. ";
   longTxt = "NAVARRE:\nYes, well... nobody's really innocent anymore, don't you see? ";
   date = 28293070;
   type = IDSTR_SCANX_POLICE;
};


ScanXEntry aa72
{
   //HA6
   shortTxt = "Mercury out of contact with Imperial Navy. ";
   longTxt = "NEWS NET:\nSolar flares continue to block communications with Mercury. The Navy has "
           @ "removed its Mercury-based ships to distant orbit and configured shield systems to wait out "
           @ "the storm. Signal interference is producing unusual sensor readings from sunside. ";
   date = 28293100;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa73
{
   //HA6
   shortTxt = "Imperial Police defeat Martian rebels! ";
   longTxt = "NAVARRE:\nThe situation is completely under control. Advise the Imperial Council that "
           @ "Martian pacification requires extermination of the majority of the Martian population, and "
           @ "tell them war is hell, but that character always counts in the end. ";
   date = 28293102;
   type = IDSTR_SCANX_POLICE;
};

//HB1 entries - after 2829.3103, before 2829.5068

ScanXEntry aa74
{
   //HB1
   shortTxt = "Slaughter on Mars! Emperor swears vengeance! ";
   longTxt = "NEWS NET:\nSources say the insurgents on Mars slaughtered the last of the Imperial "
           @ "Security Forces at Carter Flats this morning, accepting no surrender. Security Director "
           @ "Navarre was murdered immediately upon capture. The Emperor declared a day of "
           @ "mourning and swore the traitor Harabec would face a like fate. ";
   date = 28293117;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa75
{
   //HB1
   shortTxt = "Navarre got what he deserved! ";
   longTxt = "VOICE OF FREE MARS:\nHey, Navarre, how's your new candlegun facial? You got the "
           @ "same mercy you gave our families and friends, and like many of them, there's not enough "
           @ "left of you to bury. Hope you're burning in Hell, asshole. ";
   date = 28293118;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry bb91
{
   //HB1
   shortTxt = "We lost the chance for real justice! ";
   longTxt = "PHOENIX:\nDamn it, I thought I made it clear we needed Navarre alive to stand trial! We have "
           @ "to show the people of the Empire what was really going on here, and expose Navarre's insanity. "
           @ "Those MLF idiots squandered a priceless opportunity.... ";
   date = 28293120;
   type = IDSTR_SCANX_HARABEC;
};

ScanXEntry aa76
{
   //HB1
   shortTxt = "We surrender. ";
   longTxt = "IMPERIAL POLICE (Mars):\nWe surrender. We ask that when you rebels consider our "
           @ "fate, remember that not all of us carried out Navarre's orders... especially toward the end. ";
   date = 28293293;
   type = IDSTR_SCANX_POLICE;
};

ScanXEntry aa77
{
   //HB1
   shortTxt = "Grand Fleet leaves for Mars. ";
   longTxt = "IMPERIAL NAVY:\nThe Grand Fleet departed from Luna to quell the hostilities on "
           @ "Mars. ETA is three months standard. Expected mission will be to resupply the Knights and "
           @ "commence mop-up of surviving rebel forces. His Imperial Majesty has ordered that Mars "
           @ "be taught a lasting lesson, and we are it. ";
   date = 28293529;
   type = IDSTR_SCANX_NAVY;
};

ScanXEntry aa78
{
   //HB1
   shortTxt = "We won today and we'll win tomorrow! ";
   longTxt = "VOICE OF FREE MARS:\nOne tyrant's gone, and the Empire's sending others to put us "
           @ "back under the boot. Fine! We won't break, Teddies. We stay free. ";
   date = 28293540;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa79
{
   //HB1
   shortTxt = "Knights prepare for drop onto Mars. ";
   longTxt = "NEWS NET:\nStrikeforce Red Whirlwind approaches Mars. Excitement in the dropship "
           @ "bays is high as our valiant Knights prepare their Hercs. \"The dustrags are our meat now,\" "
           @ "says Knight-Captain 'Jaguar.' \"We'll hit the dust firing.\" We wish the Knights luck!";
   date = 28293844;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa80
{
   //HB1
   shortTxt = "Unconditional surrender. Now. ";
   longTxt = "ICEHAWK:\nBrother, you have our terms. Think hard. In less than one hour, we drop. "
           @ "Then it's Armageddon for your rebellion. ";
   date = 28293849;
   type = IDSTR_SCANX_CAANON;
};

ScanXEntry bb90 
{
   //HB1
   shortTxt = "Simple answer for you, Caanon. ";
   longTxt = "PHOENIX:\nSuck dust. We stay free.";
   date = 28293850;
   type = IDSTR_SCANX_HARABEC;

};

ScanXEntry aa81
{
   //HB1
   shortTxt = "The Knights have met the enemy. ";
   longTxt = "IMPERIAL NAVY (Mars):\nThe Knights have dropped onto Mars and are engaging "
           @ "enemy forces at Carter Flats. The last place to fall to the rebels will be the first liberated by "
           @ "Red Whirlwind. Planethead LZs will be secured per standard operating procedures. ";
   date = 28293851;
   type = IDSTR_SCANX_NAVY;
};

ScanXEntry aa82
{
   //HB1
   shortTxt = "Landing zones are secure. ";
   longTxt = "IMPERIAL NAVY (Mars):\nA staging area has been established at Carter Flats. The "
           @ "Knights are expanding the perimeter and preparing to move hunter-killer teams out. The "
           @ "flag of Empire flies again on Mars. ";
   date = 28293858;
   type = IDSTR_SCANX_NAVY;
};

ScanXEntry aa83
{
   //HB1
   shortTxt = "Your Knights are bloodied, bro. ";
   longTxt = "PHOENIX:\nNot bad, Caanon. But your crews took some heavy hits. Not the walk "
           @ "through the nursery you expected, is it? And we've only just begun. ";
   date = 28293860;
   type = IDSTR_SCANX_HARABEC;
};

ScanXEntry aa84
{
   //HB1
   shortTxt = "Spare me, Harabec. ";
   longTxt = "ICEHAWK:\nWe're not the Imperial Police, brother. You left more than a few dead "
           @ "pilots behind when you withdrew. ";
   date = 28293861;
   type = IDSTR_SCANX_CAANON;
};

ScanXEntry aa85
{
   //HB1
   shortTxt = "Victory at Carter Flats! ";
   longTxt = "NEWS NET:\nA first look at the Knights' LZ at Carter Flats shows determined and "
           @ "disciplined Imperial warriors battling windblown dust and a treacherous foe. Rebel dead "
           @ "sprawl everywhere, and the wreckage of Hercs and makeshift tanks litters the landscape. "
           @ "Despite the carnage, Knights carry out their tasks with cheer and steely efficiency. ";
   date = 28293862;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa86
{
   //HB1
   //(Use CYBRID INQUISITOR SECT LOGO here)
   shortTxt = ">>Knights<< battle Fourth Planet animals. ";
   longTxt = "<unidentified transmission>:\nHuman\\\\animal conflict\\\\infighting continues to escalate "
           @ "efficiently\\\\advantageously. Continue//maintain observance. Report beneficial "
           @ "progress\\\\human death to <Giver-of-Will>. ";
   date = 28293863;
   type = IDSTR_SCANX_INQU;
};

ScanXEntry bb1 
{
   //HB1
   shortTxt = "Knights reach Victoria! ";
   longTxt = "NEWS NET:\nGrand Master Weathers has established a base command in Victoria, the Imperial capital " 
           @ " of Mars.  \"We're closing in,\" Weathers says.  \"The rebels have pulled back toward "
           @ "Tharsis in the west and Haldane in the south. We'll resupply before moving on.\" ";
   date = 28294333;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry bb2 
{
   //HB1
   shortTxt = "The rebels are using mercenary assistance. ";
   longTxt = "NEWS NET:\nThe Knights are advancing on Rio de Luz. The Martians have "
           @ "supplemented their forces with mercenary bands such as the Black Death Union. Private "
           @ "units such as the Order of the Stormkeepers have declared neutrality in the conflict. ";
   date = 28294380;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry bb3 
{
   //HB1
   shortTxt = "Mercs offer Knights a challenge! ";
   longTxt = "NEWS NET:\nBlack Death Union (BDU) forces are heavily entrenched in the hills around "
           @ "Rio de Luz. Initial Knight forays have failed to gain much ground. Grand Master Weathers "
           @ "is reportedly bringing in reinforcements from Syrtis. Our prayers go with the Grand "
		   @ "Master and his brave men and women. ";
   date = 28294391;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb4 
{
   //HB1
   shortTxt = "NewsNet reporters need to exercise discretion. ";
   longTxt = "ICEHAWK:\nLieutenant, get these zipperheads out of my TOC. NewsNet can wait for "
           @ "official TDF press releases. If they bitch about their right to compromise our security "
           @ "again, shoot them. The BDU isn't playing games. Neither are we. ";
   date = 28294392;
   type = IDSTR_SCANX_CAANON;

};

ScanXEntry bb5 
{
   //HB1
   shortTxt = "Knights hit the mercs at Rio! ";
   longTxt = "NEWS NET:\nSources say mercenary opposition at Rio de Luz has committed all available "
           @ "assets against the Blood Eagle pennants sent to draw them out. The Knights say they "
		   @ "respect these mercs and would be willing to offer them the chance for an honorable "
           @ "withdrawal. ";
   date = 28294441;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb6 
{
   //HB1
   shortTxt = "Gonna be a furball at Rio, Teddies! ";
   longTxt = "VOICE OF FREE MARS:\nAll our dusters are out of Rio de Luz. If the city falls now, all "
           @ "we lose is mercs. ";
   date = 28294450;
   type = IDSTR_SCANX_MARS;

};

ScanXEntry bb7 
{
   //HB1
   shortTxt = "Knights take Rio de Luz! ";
   longTxt = "NEWS NET:\nThe Knights have occupied Rio de Luz. Many of the BDU accepted the "
           @ "withdrawal terms, but some of the mercs remained and fought to the bitter end. The "
		   @ "Knights turned the remains over to BDU envoys with full honors. However, Grand Master "
           @ "Weathers is reportedly not pleased at failing to reach rebel regulars. ";
   date = 28294665;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry aa87
{
   //HB1
   shortTxt = "Knights probe deep into rebel territory. ";
   longTxt = "NEWS NET:\nKnight recon forces encountered rebel artillery near Capri Station in the "
           @ "western equatorial region of Mars. The Knights retreated without casualties after inflicting " 
           @ "heavy damage on the cowardly \"dustrags.\" Grand Master Weathers prepares to lock down the "
           @ "Valles-Xanthe front. The rebels seem to be folding. ";
   date = 28295060;
   type = IDSTR_SCANX_NEWSNET;
};

ScanXEntry aa89
{
   //HB1
   shortTxt = "Knights get their ribbons scorched. ";
   longTxt = "VOICE OF FREE MARS:\nFine examples of Martian courage called the Red "
           @ "Armageddon blew holes through those Teddy scouts. Should have seen those Knights run "
           @ "once the big guns started opening up. Brought tears to our eyes, it was so beautiful... ";
   date = 28295061;
   type = IDSTR_SCANX_MARS;
};

ScanXEntry aa88
{
   //HB1
   shortTxt = "The Knights probe deep... ";
   longTxt = "DYSTOPIAN SNO-MEN:\nThe Knights probe deep\n\n...but will they find the ball Sparky swallowed? "
           @ "\n\nTune in next week, for \"Wallowing in Blood.\" ";
   date = 28295062;
   type = IDSTR_SCANX_DYSTOPIAN;
};

//HB3 entries - after 2829.5068, before 2829.7899

ScanXEntry bb8 
{
   //HB3
   shortTxt = "Victorious Knights pursue shattered rebel forces. ";
   longTxt = "NEWS NET:\nImperial Knights brought rebels to bay at Melas and Ophir Stations last "
           @ "night. After bitter fighting, the Knights broke the back of rebel resistance. Even now, "
           @ "Grand Master Weathers pursues the remnants of the rebel army. Hail to Imperial justice! ";
   date = 28295079;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb10 
{
   //HB3
   shortTxt = "Investigation of sunside anomalies initiated. ";
   longTxt = "ORBITAL GUARD:\nA recon drone has been dispatched sunside to investigate unusual "
           @ "readings above the plane of the ecliptic. Communication with the drone will be difficult "
           @ "due to the current spate of solar activity. ";
   date = 28296342;
   type = IDSTR_SCANX_ORBIT;

};

ScanXEntry bb9 
{
   //HB3
   shortTxt = "The rebels used the \"Cybrid\" ruse before. ";
   longTxt = "IMPERIAL NAVY:\nAdmiral Aidana Vladijon has ordered Fleet assets to remain alert for "
           @ "rebel signals attempting to indicate \"Cybrid presence.\" The Admiral referred to the "
           @ "incident in Venusian space last year. Remember the Djakarta! ";
   date = 28296343;
   type = IDSTR_SCANX_NAVY;

};

ScanXEntry bb11 
{
   //HB3
   shortTxt = "It isn't over yet! We stay free! ";
   longTxt = "VOICE OF FREE MARS:\nGuess what, Teddies, we have a few tricks left! Don't get all "
           @ "sore slapping yourselves on the back quite yet. We stay free! ";
   date = 28296700;
   type = IDSTR_SCANX_MARS;

};

ScanXEntry bb12 
{
   //HB3
   shortTxt = "The Knights are winning. Emperor recommends rebels surrender. ";
   longTxt = "NEWS NET:\nThe Knights continue to push back the rebel remnants. His Imperial "
           @ "Majesty expressed hope today that the conflict will be ended quickly. He also urges the "
           @ "rebels to concede defeat and surrender, before more lives are lost. ";
   date = 28296895;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb13 
{
   //HB3
   shortTxt = "Rebels post obscene cartoon to O-Web. ";
   longTxt = "NEWS NET:\nO-Web ravelers and webtechs put in a long night last night erasing a rebel "
           @ "animation posted as an answer to His Imperial Majesty's plea for a Martian surrender. The "
           @ "content of the three minute cartoon is extraordinarily vulgar, and parents are warned to "
           @ "check with their local system's raveler before permitting children access to the family "
           @ "interface. ";
   date = 28296921;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb14 
{
   //HB3
   shortTxt = "His Imperial Majesty certainly looks limber, doesn't he? ";
   longTxt = "DYSTOPIAN SNO-MEN:\nGee, we kinda liked the reb cartoon. Thought it deserved a "
           @ "wider audience. So we piggybacked it onto GLORIA's mail exchange and popped it into "
           @ "this morning's NN broadcasts. Enjoy! ";
   date = 28296922;
   type = IDSTR_SCANX_DYSTOPIAN;

};

ScanXEntry bb15 
{
   //HB3
   shortTxt = "Rebels strike overcome by courageous Knights. ";
   longTxt = "NEWS NET:\nUsing a stolen Herc, rebels ambushed a position held by Imperial Knights. "
           @ "Some supplies were lost, but Knight casualties were minimal. The rebels paid for their "
           @ "treacherous attack. ";
   date = 28296999;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb16 
{
   //HB3
   shortTxt = "The odds are even now, Teddies! Eat dust! ";
   longTxt = "VOICE OF FREE MARS:\nOK, Teddies, let's see whether the loss of your fancy gear "
           @ "evens the odds. Dust is relentless as time, y'know. So are Martians. We stay free! ";
   date = 28297005;
   type = IDSTR_SCANX_MARS;

};

ScanXEntry bb17 
{
   //HB3
   shortTxt = "Knights have rebels on the defensive ";
   longTxt = "IMPERIAL NAVY:\nETA to Mars is 12 days, 18 hours. Red Whirlwind reports victory "
           @ "on all fronts. The rebels are broken. Our mission is to resupply pursuing Knight forces and "
           @ "then bombard remaining rebel strongholds. Any rebel who does not surrender before Fleet "
           @ "arrival faces summary execution. ";
   date = 28297539;
   type = IDSTR_SCANX_NAVY;

};

ScanXEntry bb18 
{
   //HB3
   shortTxt = "TDF concerned about anomalies. ";
   longTxt = "NEWS NET:\nPuzzling disappearance of an Orbital Guard drone has TDF leaders "
           @ "concerned regarding the possibility of rebel raiders striking insystem. Admiral Vladijon "
           @ "confirms that a high level strategy session has been called at Gierling Station, NorthAm, at "
           @ "the Centagon. ";
   date = 28297601;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb19 
{
   //HB3
   shortTxt = "Drone lost due to solar flares. ";
   longTxt = "ORBITAL GUARD:\nTDF Intelligence has concluded that the drone was lost to the solar "
           @ "storm that began last week. The anomalies were probably caused by magnetic eruptions in "
           @ "the sun's corona. ";
   date = 28297642;
   type = IDSTR_SCANX_ORBIT;

};


ScanXEntry bb20 
{
   //HB3
   shortTxt = "Last chance, brother. ";
   longTxt = "ICEHAWK:\nThe Fleet's nearly here, Harabec. Surrender and make it easy on everyone.  "
           @ "I'll discuss an amnesty for your troops if you turn yourself in before the Fleet arrives. ";
   date = 28297884;
   type = IDSTR_SCANX_CAANON;

};


ScanXEntry bb21 
{
   //HB3
   shortTxt = "You know my answer ";
   longTxt = "PHOENIX:\nWe stay free, brother. ";
   date = 28297885;
   type = IDSTR_SCANX_HARABEC;

};


ScanXEntry bb22 
{
   //HB3
   shortTxt = "So be it. Time's up. ";
   longTxt = "ICEHAWK:\nThe Fleet's here. All Swords, orders are: Burn the dustrags. Accept no "
             @ "surrender. ";
   date = 28297886;
   type = IDSTR_SCANX_CAANON;

};


ScanXEntry bb23 
{
   //HB3
   shortTxt = "We stay free! ";
   longTxt = "VOICE OF FREE MARS:\nHere it comes. Dig in, dusters... ";
   date = 28297887;
   type = IDSTR_SCANX_MARS;

};

ScanXEntry bb24 
{
   //HB3
   shortTxt = "Colonies wait for outcome of Martian rebellion. ";
   longTxt = "NEWS NET:\nThe Grand Fleet has arrived at Mars. Violence on the other colonies has "
           @ "abated. The entire system seems to be waiting to see the outcome of the fight for Mars. ";
   date = 28297888;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb25 
{
   //HB3
   shortTxt = "Mercury out of contact with Imperial Navy. ";
   longTxt = "NEWS NET:\nSolar flares continue to block communications with Mercury. The Navy has "
           @ "removed its Mercury-based ships to distant orbit and configured shield systems to wait out "
           @ "the storm. ";
   date = 28297890;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb26 
{
   //HB3
   shortTxt = "Something's splashing O-Web! ";
   longTxt = "DYSTOPIAN SNO-MEN:\nGot lag and black snow worming all over the place. 'Sphere "
           @ "looks about as stable as a two-legged dog on ice. Cut-offs and Sno-banks aren't slowing "
           @ "'em down. Looks like that code anomaly again -- headed right for the hearts of GLORIA and "
           @ "ANGEL. Gonna be a bad one...!";
   date = 28297891;
   type = IDSTR_SCANX_DYSTOPIAN;

};

ScanXEntry bb27 
{
   //HB3
   shortTxt = "GLORIA compromised... ";
   longTxt = "ICEHAWK:\nWhat ***pened to GLO***? Has to be ***bec's work. ******to laser "
            @ "tran*****Phobos Orbital. ";
   date = 28297892;
   type = IDSTR_SCANX_CAANON;

};

ScanXEntry bb28 
{
   //HB3
   shortTxt = "Losing***link***zz* ";
   longTxt = "NEWS NET:\n****can't****st***kinnng***rest**ion***rt*****b**ttt********** "
           @ "***********zzg**************************************88 ";
   date = 28297893;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb29 
{
   //HB3
   shortTxt = "O-Web crashed! Local commlinks unreliable... ";
   longTxt = "NEWS NET (Mars):\nWe have lost datumsphere connection with Earth-Luna nodes. "
           @ "Commlinks around Mars are spotty due to rebel destruction of satellite systems, but local "
           @ "O-Web function has been partially restored. Earth's orbital position permits only delayed "
           @ "laser transmissions. As of now, we have no hard data as to the cause of the interruption. ";
   date = 28297894;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb30 
{
   //HB3
   shortTxt = "Recalling all units for immediate departure, Code Gold. ";
   longTxt = "IMPERIAL NAVY (Mars):\nAll units, cease operations and return to DZs for immediate "
           @ "retrieval. Repeat, cease operations. Do not attempt to recover supplies. All dropships will "
           @ "lift by 1930 hours Martian. You have five hours. ";
   date = 28297896;
   type = IDSTR_SCANX_NAVY;

};

ScanXEntry bb31 
{
   //HB3
   shortTxt = "They're running! Take 'em out! ";
   longTxt = "VOICE OF FREE MARS:\nWill you look at that? The Imps are leaving their big bad "
           @ "hero Caanon high and dry. Guess they forgot something back home on the stove, huh? "
           @ "We'll win this war yet! Bust some Imp heads, heroes! ";
   date = 28297897;
   type = IDSTR_SCANX_MARS;

};

//HB4 entries - after 2829.7899, before 2830.1411

ScanXEntry bb32 
{
   //HB4
   shortTxt = "Caanon surrenders! But what's happening on Earth? ";
   longTxt = "VOICE OF FREE MARS:\nCaanon surrendered! Mole Command's ordered a cease-fire! "
           @ "We win! The O-Web's down, though. Mixed reports coming in… something about Earth. "
           @ "We need more data. ";
   date = 28297910;
   type = IDSTR_SCANX_MARS;

};

ScanXEntry bb33 
{
   //HB4
   shortTxt = "The Cybrids have invaded. We are at war! ";
   longTxt = "IMPERIAL NAVY:\nFor those who can still hear this… the Cybrids have invaded. "
           @ "Intelligence remains sparse … The O-Web is compromised and may crash at any moment. "
           @ "Preliminary reports indicate Mercury has already fallen to the Cybrids. Earth is bracing for "
           @ "siege. All units, go to maximum alert! ";
   date = 28298613;
   type = IDSTR_SCANX_NAVY;

};

ScanXEntry bb34 
{
   //HB4
   shortTxt = "Cybrids are coming... ";
   longTxt = "VOICE OF FREE MARS:\nIt's bad. Real bad. The Cybrids crushed the Navy at Mercury "
           @ "and are heading out for us like hell with the lid off. Lace 'em up, dusters, and polish the "
           @ "candleguns - this'll be one goddamn nasty ride. ";
   date = 28299400;
   type = IDSTR_SCANX_MARS;

};

ScanXEntry bb35 
{
   //HB4
   shortTxt = "Imperial Knights and free Martians form Alliance. ";
   longTxt = "PHOENIX:\nThe Cybrids have invaded, and our fight with the Teddies is meaningless "
           @ "now. From now on ... we're all in it together, Imperial and colonial, a human Alliance just "
           @ "like the first days of the TDF. Just remember, all you out there, it ain't over yet!\n\nWe stay free. ";
   date = 28299496;
   type = IDSTR_SCANX_HARABEC;

};

ScanXEntry bb36 
{
   //HB4
   shortTxt = "We must persevere if humanity is to survive! ";
   longTxt = "VOICE OF THE ALLIANCE:\nWe stand united against the coming storm! From here "
           @ "we move to ensure the salvation of humanity. The Cybrids seek to destroy our families, "
           @ "our homes, and our lives. We can't allow that to happen! ";
   date = 28300007;
   type = IDSTR_SCANX_ALLI;

};

//HC1 entries - after 2830.1411, before 2830.2335

ScanXEntry bb37 
{
   //HC1
   shortTxt = "Bring it on, glitches! ";
   longTxt = "VOICE OF FREE MARS:\nWell, this is it, kiddies and clogs. We're gonna make damn "
           @ "sure the Cybrids choke on this dustball. This'll probably be our final broadcast, folks. The "
           @ "glitches are moving in. Voice of Free Mars, signing off... And remember:\n\nWe stay free. ";
   date = 28301518;
   type = IDSTR_SCANX_MARS;

};

ScanXEntry bb38 
{
   //HC1
   shortTxt = "Trojan Horse Cybrids on Venus! ";
   longTxt = "UMBRAL THORN:\nWe've all heard about those \"Trojan Horse\" scorchers the Cybrids "
           @ "slipped onto Earth. Well, one popped up in Minerva recently. Captain Shanka squikked it "
           @ "nicely. Start paying real close attention to each other, unless you happen to enjoy cold "
           @ "steel shoved in your back. ";
   date = 28302300;
   type = IDSTR_SCANX_VENUS;

};

ScanXEntry bb39 
{
   //HC1
   shortTxt = "Welcome, Alliance! ";
   longTxt = "UMBRAL THORN:\nThis is the Venusian rebel leadership. Cease ops, kerls and deerns! "
           @ "We're in the Alliance now. The scorchin' toasters are landing in force, and the Imps and "
           @ "Mars bars are here to help! Give them anything they ask for. "
		   @ "\n\nBy the way, offworld mates, welcome to our little corner of Hell. Check your seals.";
   date = 28302325;
   type = IDSTR_SCANX_VENUS;

};

ScanXEntry bb40 
{
   //HC1
   shortTxt = "Where's the TDF? ";
   longTxt = "ICEHAWK:\nThis is Icehawk of the Human Alliance. Get me the ranking TDF officer - "
           @ "now. ";
   date = 28302326;
   type = IDSTR_SCANX_CAANON;

};

ScanXEntry bb41 
{
   //HC1
   shortTxt = "TDF already left Venus. ";
   longTxt = "UMBRAL THORN:\nSince so many of our local bootboys ditched us for Luna once the "
           @ "word from Earth came through, we speak for Venus now, Icehawk. Live with it, heya?"
           @ "\n\nBy the way, you're cleared to land at Drachensatem. Watch that turbulence now. ";
   date = 28302327;
   type = IDSTR_SCANX_VENUS;

};

ScanXEntry bb43 
{
   //HC1
   shortTxt = "We need reinforcements! ";
   longTxt = "ORBITAL GUARD:\nEarth is being swamped! O-Web integrity is sporadic! Where are "
           @ "those reinforcements from Venus? ";
   date = 28302330;
   type = IDSTR_SCANX_ORBIT;

};

ScanXEntry bb44 
{			 
   //HC1
   shortTxt = "These guys are optimists! We're hosed! ";
   longTxt = "DYSTOPIAN SNO-MEN:\nWe need reinforcements from God, people! Have you looked "
           @ "out the window? It's raining parts of Orbital G! ";
   date = 28302331;
   type = IDSTR_SCANX_DYSTOPIAN;

};

ScanXEntry bb45 
{
   //HC1
   shortTxt = "Mercury survivors are almost ready to land near Drachensatem! ";
   longTxt = "IMPERIAL NAVY (Mercury):\nCalling Venus... You getting this? We're nearly in orbit now... "
           @ "Not much left, not many of us made it out -- Mercury's covered in glitches. They're on us "
           @ "like flies on dung. We're going to try to lose 'em in the Oberwind and drop in near "
           @ "Drachensatem. ";
   date = 28302333;
   type = IDSTR_SCANX_NAVY;

};

//HC2 entries - after 2830.2335, before 2830.2642

ScanXEntry bb46 
{
   //HC2
   shortTxt = "Alliance wins victory on Venus! ";
   longTxt = "VOICE OF THE ALLIANCE (Venus):\nStrikeforce \"Guardian\" has been strengthened "
           @ "by the arrival of TDF troops from Mercury. Fought the Cybrids off and bought ourselves "
           @ "some breathing room. ";
   date = 28302344;
   type = IDSTR_SCANX_ALLI;

};

ScanXEntry bb47 
{
   //HC2
   shortTxt = "Human Alliance strikes back at glitches! ";
   longTxt = "NEWS NET:\nThe regrouping \"Human Alliance\" on Venus d***the glitches a severe "
           @ "setba***stroying one of their key landing pads yester****. On Mother Eart***ficulty "
           @ "transmitting as glitch****our transmission nodes. Datumplane disrupt******fight on, "
           @ "Alliance! Our hopes go with you. ";
   date = 28302387;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb48 
{
   //HC2
   shortTxt = "Glitch infiltrators are slaughtering us! Stay alert! ";
   longTxt = "UMBRAL THORN:\nThis is a full alert! We found \"Trojan Horse\" units in Hammadi, "
           @ "Shelley and Dante. Those were destroyed before they could do any real damage. Nobody "
           @ "noticed the ones in Ishtar, though, and now most of Ishtar's citizens are dissolving in "
           @ "puddles of their own blood. Don't get sloppy, people! These bastards won't give you a "
           @ "second chance! ";
   date = 28302538;
   type = IDSTR_SCANX_VENUS;

};

ScanXEntry bb49 
{
   //HC2
   shortTxt = "Dig in, people! There's nowhere to run! ";
   longTxt = "UMBRAL THORN:\nThere's another wave of glitches on its way. Stay sharp, kerls and "
            @ "deerns! We'll hand out dance tickets soon! Of course, there's no other show in town... ";
   date = 28302575;
   type = IDSTR_SCANX_VENUS;

};

ScanXEntry bb50 
{
   //HC2
   shortTxt = "Toasters incoming! Pull back! ";
   longTxt = "VOICE OF THE ALLIANCE (Venus):\nCybrids are landing across Hera and Ishtar "
           @ "Provinces. They can't use their damned drop pods due to the Oberwind. At least that's "
           @ "something. Begin a withdrawal toward Sa Thauri. We'll hold Carson and Erste Landung "
           @ "and follow later. ";
   date = 28302610;
   type = IDSTR_SCANX_ALLI;

};

ScanXEntry bb51 
{
   //HC2
   //USE CYBRID EXEMPLAR LOGO
   shortTxt = "Destroy humans of \"Venus.\" ";
   longTxt = "<unidentified transmission>:\nNexus 0232 established//implanted on <Second Planet>. "
           @ "Begin purification\\\\disposal operations immediately. ";
   date = 28302633;
   type = IDSTR_SCANX_CYBRID;

};

//HC3 entries - after 2830.2642, before 2830.4189

ScanXEntry bb52 
{
   //HC3
   shortTxt = "Fall back to Sa Thauri...! ";
   longTxt = "ICEHAWK:\nOur techs gutted that Cybrid Nexus. We got all we can get from it, but the "
           @ "glitches are landing in greater numbers. All units, fall back to Sa Thauri. Be damn sure to "
           @ "mine the passes. Flyers aren't much use on this planet, so the Cybrids will have to come to "
           @ "us on the ground. ";
   date = 28302950;
   type = IDSTR_SCANX_CAANON;

};

ScanXEntry bb53 
{
   //HC3
   shortTxt = "Carson calling...! ";
   longTxt = "UMBRAL THORN:\nWe're cut off in Carson! The glitches are punching holes in the "
           @ "bulkheads. Hangar bays are scorched, and the 'brids are sending in their hunter-killers. "
           @ "They've got some kind of radiation guns! We're dead, boyos. Give our love to our "
           @ "families, eh? ";
   date = 28303577;
   type = IDSTR_SCANX_VENUS;

};

ScanXEntry bb54 
{
   //HC3
   shortTxt = "Alliance prepares to leave Venus. ";
   longTxt = "VOICE OF THE ALLIANCE (Venus):\nWe're getting as many refugees as we can, but "
           @ "we're running out of ships - and time. We won't be able to hold much longer. Too many "
           @ "glitches. The Alliance has begun withdrawal operations. ";
   date = 28303974;
   type = IDSTR_SCANX_ALLI;

};

ScanXEntry bb55 
{
   //HC3
   shortTxt = "Guardian crew will hold the rearguard. ";
   longTxt = "ICEHAWK:\nFalling back to Sa Thauri for the final time. We're not leaving any Alliance "
           @ "troops behind, damn it. The Guardian strikeforce crew will hold the glitches back while "
           @ "the main group of ships evacuate. We'll follow when the rest of the Alliance ships are "
           @ "clear. ";
   date = 28304180;
   type = IDSTR_SCANX_CAANON;

};

//HD1 entries - after 2830.4189, before 2831.8699

ScanXEntry bb56 
{
   //HD1
   shortTxt = "Good luck, Alliance! ";
   longTxt = "UMBRAL THORN:\nNot many of us rats left here, but there's enough to stay and keep "
           @ "the glitches busy. Thanks to the Alliance for taking so many of our families. We've been "
           @ "preparing for a long time. It's not going to be easy for the Cybrids to ferret us out. We'll "
           @ "be a thorn in their side for awhile. ";
   date = 28304286;
   type = IDSTR_SCANX_VENUS;

};

ScanXEntry bb57 
{
   //HD1
   shortTxt = "Cybrids on the North Pole! ";
   longTxt = "NEWS NET:\nThe Cybrids ha**unch through to land on the No**th Pole! There is "
           @ "fighting in NAP and EA! A fro***orming in Norther***beria, troops are rushing to meet "
           @ "the***flict, heavy casualti***ticipate**** ";
   date = 28305307;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb58 
{
   //HD1
   shortTxt = "Fierce fighting*****ther Earth as TDF cont***resist adv**glitches ";
   longTxt = "NEWS NET:\nOrbital Guard**tinues to***elaying act**itch beachheads in Can***China "
           @ "continue to exp**DF controls skies above Afric*****fierce fighting in EA, NAP. "
           @ "Refugees fl***southward**Emp*****last much long**** ";
   date = 28306000;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb59
{
   //HD1
   shortTxt = "Don't despair. We're not done yet. ";
   longTxt = "VOICE OF THE ALLIANCE:\nThe 'brids are monitoring the dataspheres, so we'll keep "
           @ "this short. The Alliance is strong. Let's be thankful for that. We have a plan. Don't give in "
           @ "to despair. We beat the Cybrids twice before when all seemed darkest. We'll do it again. ";
   date = 28306414;
   type = IDSTR_SCANX_ALLI;

};

ScanXEntry bb60
{
   //HD1
   shortTxt = "This is Melanie. Is anybody listening? ";
   longTxt = "MELANIE:\nThis is Melanie. I'm the only one left, and I'm scared. Is there anybody out "
             @ "there? Please answer! I'm so scared... ";
   date = 28306775;
   type = IDSTR_SCANX_MELAN;

};

ScanXEntry bb61
{
   //HD1
   shortTxt = "Imperial Fleet crippled. We have lost Earth's sky. ";
   longTxt = "ORBITAL GUARD:\n***fighter wing destroyed! Imperial Fleet has b****orn to ribbons! "
           @ "Earth's sk***lling up with ****load of Cybrid dro****** ";
   date = 28306827;
   type = IDSTR_SCANX_ORBIT;

};

ScanXEntry bb62
{
   //HD1
   shortTxt = "We can't find Melanie... ";
   longTxt = "PHOENIX:\nMelanie? We can't get a trace... Where are you?";
   date = 28307974;
   type = IDSTR_SCANX_HARABEC;

};

ScanXEntry bb63
{
   //HD1
   shortTxt = "Here I am! ";
   longTxt = "MELANIE:\nHere! I'm on Europa! Please help!";
   date = 28307975;
   type = IDSTR_SCANX_MELAN;

};

ScanXEntry bb64
{
   //HD1
   shortTxt = "OK, we're coming! ";
   longTxt = "PHOENIX:\nWe're on our way, sweetheart. Hang tight, 'K?";
   date = 28307975;
   type = IDSTR_SCANX_HARABEC;

};

ScanXEntry bb65
{
   //HD1
   shortTxt = "Drachensatem calling ... falling ...! ";
   longTxt = "UMBRAL THORN:\nDrachensatem calling ... Drachensatem cal***outh chamber "
           @ "breach**seals failing, you copy, Stoltz? Tak****to lower access relays***TDF kerls will "
           @ "have to hold th***mn it! Goddamn! Level Three cracked! This is Drachensa*****our "
           @ "seals blew***fall back, Norcross, roj? Tr***scorchin' toaste*********** ";
   date = 28308932;
   type = IDSTR_SCANX_VENUS;

};

ScanXEntry bb66
{
   //HD1
   shortTxt = "The Navy will assist with the Alliance plan. ";
   longTxt = "IMPERIAL NAVY (Titan):\nThe project will have to set up on the surface. One of the "
           @ "northern islands would be best. The ice is deep there, and the tectonics are stable. We'll "
           @ "ferry materials over from Eskandani Base. ";
   date = 28311224;
   type = IDSTR_SCANX_NAVY;

};

ScanXEntry bb67
{
   //HD1
   shortTxt = "Dies Irae will save humanity! ";
   longTxt = "VOICE OF THE ALLIANCE:\nThe courage you've all displayed has been nothing short "
           @ "of amazing. As you know, several of you will take on new roles. You will raise future "
           @ "generations that have a chance of being something other than soldiers or refugees. ";
   date = 28313336;
   type = IDSTR_SCANX_ALLI;

};

ScanXEntry bb68
{
   //HD1
   shortTxt = "Mayday! Mayday! ";
   longTxt = "IMPERIAL NAVY (Long Patrol):\n**the ISS Pha*****imum burn sunward***ayday, "
           @ "any TDF sh***ursuit. Highest prio****losing signal***Mayday, mayd********** ";
   date = 28315454;
   type = IDSTR_SCANX_NAVY;

};

ScanXEntry bb69
{
   //HD1
   shortTxt = "The glitches are in the building. Prepare for attack! ";
   longTxt = "VOICE OF THE ALLIANCE:\nWe've detected Cybrids entering Titan space! The Dies "
             @ "Irae launch must succeed! All units, scramble! We will hold the 'brids at bay. ";
   date = 28318687;
   type = IDSTR_SCANX_ALLI;

};

ScanXEntry bb70
{
   //HD1
   shortTxt = "Surrender, Dorothy! Or we'll eat your hearts. ";
   longTxt = "<cybrid transmission>:\nWhy not surrender, human\\\\animals? <We> control Second and "
           @ "Fourth Planets. Third Planet\\\\Homeworld falls efficiently. You cannot prevail. Your "
           @ "children die. Spare yourself pain\\\\inconvenience and cease resistance immediately. ";
   date = 28318689;
   type = IDSTR_SCANX_MACH;

};

ScanXEntry bb71
{
   //HD1
   shortTxt = "The Dies Irae is loading! Dig in! ";
   longTxt = "VOICE OF THE ALLIANCE:\nThe Cybrids figured out what we're trying to do! The "
           @ "Dies Irae must launch successfully if humanity hopes to survive. All crews, take defensive "
           @ "positions! All members of the Dies Irae, prepare for cryo-insertion! This is it, people! ";
   date = 28318690;
   type = IDSTR_SCANX_ALLI;

};

ScanXEntry bb72
{
   //HD1
   //use EXEMPLAR SECT GRAPHIC
   shortTxt = "<We> will exterminate your precious ones. ";
   longTxt = "<cybrid transmission>:\nACKNOWLEDGE//SUBMIT! Human\\\\animals initiate survival "
           @ "program. Remaining animals now operate without regard\\\\concern to long-term "
           @ "continuation of existence. Activate pursuit\\\\eradication optimals for immediate "
           @ "location\\\\termination of animal transports. Eliminate//forbid animal >>hope<<. ";
   date = 28318698;
   type = IDSTR_SCANX_CYBRID;

};

//HD2 entries - after 2831.8699, before 2832.1439

ScanXEntry bb73
{
   //HD2
   shortTxt = "Dies Irae successfully launched! ";
   longTxt = "VOICE OF THE ALLIANCE:\nIn a burst of light we cast our seed to the stars. In the "
           @ "blood of our sleepers waits a galaxy. Dream of new suns, new skies, new futures, friends. "
           @ "And our fate? We remember our ancestors and The Fire. We will not go gently into the "
           @ "night. Good luck, Dies Irae! ";
   date = 28318715;
   type = IDSTR_SCANX_ALLI;

};

ScanXEntry bb74
{
   //HD2
   shortTxt = "We kicked ass, people! ";
   longTxt = "PHOENIX:\nAll Alliance officers, return to base for debriefing. The NTDF is on recon "
           @ "duty and will alert us if our glitch buddies try again. However, I think the metal bastards "
           @ "are gonna be licking their burnt wires awhile after this furball. Awesome job, everyone! ";
   date = 28318717;
   type = IDSTR_SCANX_HARABEC;

};

ScanXEntry bb75
{
   //HD2
   shortTxt = "Long Patrol priority transmission! ";
   longTxt = "IMPERIAL NAVY (Long Patrol):\nPriority transmission from ISS Phaedrus, Long "
           @ "Patrol. Dedicated encryption code: Alpha Zebra Romeo India Echo Lima. Repeat: Priority "
           @ "transmission from... ";
   date = 28318748;
   type = IDSTR_SCANX_NAVY;

};

//HE1 entries - after 2832.1439, before 2832.4779

ScanXEntry bb76
{
   //HE1
   shortTxt = "Earth Orbit defenses gone ... last stand Earthside. ";
   longTxt = "ORBITAL GUARD:\nGierling Station calling *** glitches everywhere **decaying "
           @ "orbit*****one lost. All squadrons ****troyed. **Evacuation ord****30 hours Terran. "
           @ "Redeployin***xandrian perimeter**hear this, report to Se*****ted re-entry T-minus two "
           @ "hours - damn it! Sixth wave det***Gierling Station cal************* ";
   date = 28321664;
   type = IDSTR_SCANX_ORBIT;

};

ScanXEntry bb77
{
   //HE1
   shortTxt = "It's all or nothing! ";
   longTxt = "VOICE OF THE ALLIANCE:\nThis is the Human Alliance, calling from Titan! It's not "
           @ "over yet -! We're trying a Hail Mary, Earth! If you get this, take heart! ";
   date = 28321831;
   type = IDSTR_SCANX_ALLI;

};

ScanXEntry bb78
{
   //HE1
   shortTxt = "Hope is our greatest weapon. ";
   longTxt = "CARDINAL SPEAR:\nWe will thrust into the heart of the darkness. Never say die, "
           @ "people. ";
   date = 28321850;
   type = IDSTR_SCANX_CARDINAL;

};

//HE2 entries - after 2832.4779, before 2832.4784

ScanXEntry bb79
{
   //HE2
   //use EXEMPLAR SECT GRAPHIC
   shortTxt = "Do you feel the cold, human\\\\vermin? You have come far to die. ";
   longTxt = "<cybrid transmission>:\nACKNOWLEDGE//SUBMIT! Human\\\\animal intrusion "
           @ "detected CORE::NEXUS>>GEHENNA. Maximum response\\\\retaliation forthcoming. All "
           @ "available units proceed//hasten to defend <Giver-of-Will>. Execute protocol 03212-333. ";
   date = 28324780;
   type = IDSTR_SCANX_CYBRID;

};

ScanXEntry bb80
{
   //HE2
   shortTxt = "Pull it together! ";
   longTxt = "CARDINAL SPEAR:\nCalling all crews. Hope you're out there. Nippy here, isn't it? "
           @ "Objective remains \"The Big Toaster.\" Remember, we rode a one-way ticket out here. "
           @ "Make it count. ";
   date = 28324780;
   type = IDSTR_SCANX_CARDINAL;

};

ScanXEntry bb81
{
   //HE2
   //use EXEMPLAR SECT GRAPHIC
   shortTxt = "Error\\\\inefficiency. Alter//reconfigure defensive program. ";
   longTxt = "<cybrid transmission>:\nACKNOWLEDGE//SUBMIT! Alert >>Platinum Guard<<. "
           @ "Lapse\\\\error in defensive efficiency permitted animal intrusion to reach//attain surface. "
           @ "Error\\\\inefficiency suggests metagen\\\\heretic influence. Purge//cleanse//exsect <units> "
           @ "responsible for lapse. ";
   date = 28324781 ;
   type = IDSTR_SCANX_CYBRID;

};

ScanXEntry bb82
{
   //HE2
   shortTxt = "Lost contact with the Emperor! ";
   longTxt = "NEWS NET:\nNo****ch time left. Desp***istance***sacrifi*****the Cybrids "
           @ "ha*****red Nova Alexandr****Emperor's pala**attack.**Smoke b*****no word on his "
           @ "stat***Guard units**mustering*****all situation**peless******lieve the Emperor is "
           @ "dea************** ";
   date = 28324782;
   type = IDSTR_SCANX_NEWSNET;

};

ScanXEntry bb83
{
   //HE2
   shortTxt = "Cybrid invasion is here! ";
   longTxt = "HUMAN ALLIANCE (Titan):\nCybrid forces incoming... that many? Christ and Hunter, "
           @ "they're all dropping onto Sector Nine! That's the NTDF perimeter! Scramble "
           @ "reinforcement crews! Damn it! Those boys better hold out 'til we get there! Pray they "
           @ "do... ";
   date = 28324783;
   type = IDSTR_SCANX_ALLI;

};

//HE2 entries - after 2832.4784, before 2832.4791

ScanXEntry bb84
{
   //HE3
   shortTxt = "Last message. ";
   longTxt = "PHOENIX:\nIf this message hits your computers, it's because I'm dead. There's a lot to "
           @ "say, but not a lot of time. Caanon, I'm not your brother. I never was, though I tried to be. "
           @ "I lied to you and the family every moment of every day. Mars gave me the chance to be "
           @ "true again, to regain my sense of honor. The Empire stands on a foundation of lies. "
           @ "Change that, if you make it back, Cay. Make it something true again. ";
   date = 28324785;
   type = IDSTR_SCANX_HARABEC;

};

ScanXEntry bb85
{
   //HE3
   shortTxt = "Update on Titan! ";
   longTxt = "HUMAN ALLIANCE (Titan):\nHolding... just barely. Imperials, rebs, and merc crews "
           @ "are fighting side by side. New Terra Defense Force, Black Death Union, and the Imperial "
           @ "First Cav plugged the breaches. Sons of the Brotherhood and Red Armageddon tore up "
           @ "the glitch flanks. Stormkeepers claim they're going after the Choosers of Strategies or "
           @ "something. Keep it up, people! Make it expensive for the wireheads! ";
   date = 28324786;
   type = IDSTR_SCANX_ALLI;

};
									   
ScanXEntry bb86
{
   //HE3
   shortTxt = "Proceed to Nav Omega. ";
   longTxt = "ICEHAWK:\nCalling any remaining Cardinal Spear units. This is Icehawk. Harabec is "
           @ "gone. Cardinal Three and I are on a bearing for Nav Omega. There's heavy Cybrid resistance. "
           @ "\n\nWe're going in.";
   date = 28324788;
   type = IDSTR_SCANX_CAANON;

};

ScanXEntry bb87
{
   //HE3
   //use EXEMPLAR SECT GRAPHIC
   shortTxt = "Defend//shield Prometheus! ";
   longTxt = "<cybrid transmission>:\nOUTRAGE::DISSONANCE! Human\\\\animal intrusion "
           @ "continues//infects//defiles CORE::NEXUS>>GEHENNA. All units accelerate//hasten to "
           @ "defend//protect <Giver-of-Will>! Do not allow//submit animals to reach//disturb//offend "
           @ "<First-Thought>! ";
   date = 28324789;
   type = IDSTR_SCANX_CYBRID;

};

ScanXEntry bb88
{
   //HE3
   shortTxt = "It's time. ";
   longTxt = "CARDINAL THREE:\nThis is it, Icehawk. Let's get the job done.";
   date = 28324790;
   type = IDSTR_SCANX_CARDINAL;

};


 scanxWrite("campaign\\human\\ScanX.ddb");

 