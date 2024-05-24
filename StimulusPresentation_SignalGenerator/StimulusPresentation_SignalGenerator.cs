using BCI2000RemoteNET;
using System.Runtime.InteropServices;
using System.Threading;
using System;
int port = int.Parse(Environment.GetCommandLineArgs()[1]);
BCI2000Remote bci = new(new BCI2000Connection());
bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
bci.connection.StartOperator("../../bci2000/prog/Operator" + (isWindows ? ".exe" : ""), port: port); //Add file extension if on windows 
Thread.Sleep(1000);
bci.connection.Connect(port: port);
bci.LoadParameters("../parms/examples/StimulusPresentation_SignalGenerator.prm");
bci.StartupModules(new Dictionary<string, IEnumerable<string>?>() {
	{"SignalGenerator", new List<string>() {"LogKeyboard=1", "SpinningWheel=1", "ShowDisplayStatistics=1"}},
	{"P3SignalProcessing", null},
	{"StimulusPresentation", null}
	});
