// ASCOM FilterWheel Driver Test Script for autoFilterWheel
var X = new ActiveXObject("ASCOM.autoFilterWheel.FilterWheel");
WScript.Echo("Driver: " + X.Name);
WScript.Echo("Description: " + X.Description);
// You may want to uncomment this to test connection...
// X.Connected = true;
X.SetupDialog();
