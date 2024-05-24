#r "../BCI2000RemoteNET.dll"
using BCI2000RemoteNET;
using System.RuntimeInformation.InteropServices;
BCI2000Remote bci = new(new BCI2000Connection());
bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
bci.connection.StartOperator("../prog/Operator" + isWindows ? ".exe" : ""); //Add file extension if on windows 
bci.connection.Connect();
bci.LoadParameters("../parms/examples/StimulusPresentation_SignalGenerator.prm");
bci.StartupModules(new() {
	{"SignalGenerator", new() {"LogKeyboard=1", "SpinningWheel=1", "ShowDisplayStatistics=1"}},
	{"P3SignalProcessing", null},
	{"StimulusPresentation", null}
	});
