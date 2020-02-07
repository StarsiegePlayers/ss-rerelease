//---------------------------------
// master.cs
// Last Updated 12/09/18
//---------------------------------

$Inet::Master1 = "IP:master1.starsiege.io:29000";    // Jenetrix's Master
$Inet::Master2 = "IP:master2.starsiege.pw:29000";    // Wilzuun's Master
$Inet::Master3 = "IP:master3.starsiege.io:29000"; 

// Legacy Master Servers
// $Inet::Master1 = "IP:ss1m1.masters.dynamix.com:29000";
// $Inet::Master2 = "IP:ss1m2.masters.dynamix.com:29000";
// $Inet::Master3 = "IP:ss1m3.masters.dynamix.com:29000";

$inet::IPBroadcast1 = "IP:broadcast:29001";
$inet::IPXBroadcast1 = "IPX:broadcast:29001";

# times here are in ms
$pref::maxConcurrentPings = 20;
$pref::pingTimeoutTime = 1500;
$pref::pingRetryCount = 2;
$pref::maxConcurrentRequests = 10;
$pref::requestTimeoutTime = 3000;
$pref::requestRetryCount = 2;
 