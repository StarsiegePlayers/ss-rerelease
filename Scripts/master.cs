//---------------------------------
// master.cs
// Last Updated 7/1/2021
//---------------------------------

// Primary
$Inet::Master1 = "IP:master1.starsiegeplayers.com:29000";    // Jenetrix's Master (SS.IO)
$Inet::Master2 = "IP:master2.starsiegeplayers.com:29000";    // Wilzuun's Master (SS.PW)
$Inet::Master3 = "starsiege1.no-ip.org:29000";               // Eye's Master (No-IP)

// Secondary
$Inet::Master4 = "IP:master3.starsiegeplayers.com:29000";    // Jenetrix's Master (SS.IO Address 2)
$Inet::Master5 = "starsiege.noip.us:29000";                  // Eye's Master (No-IP Address 2)

// Legacy
// $Inet::Master1 = "IP:ss1m1.masters.dynamix.com:29000";
// $Inet::Master2 = "IP:ss1m2.masters.dynamix.com:29000";
// $Inet::Master3 = "IP:ss1m3.masters.dynamix.com:29000";

// Broadcast
$inet::IPBroadcast1 = "IP:broadcast:29001";
$inet::IPXBroadcast1 = "IPX:broadcast:29001";

// Times here are in ms
$pref::maxConcurrentPings = 15;
$pref::pingTimeoutTime = 3000;
$pref::pingRetryCount = 3;
$pref::maxConcurrentRequests = 15;
$pref::requestTimeoutTime = 3000;
$pref::requestRetryCount = 3;
