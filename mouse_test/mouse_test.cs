using BCI2000RemoteNET;
using System.Threading;

BCI2000Connection conn = new();

conn.StartOperator("/home/tytbu/bcistuff/bci2000/prog/Operator");
conn.Connect();

BCI2000Remote bci = new(conn);

bci.AddParameter("Application:Place", "Parmam", "0", "0", "10");
bci.AddEvent("AEvent", 32);

bci.StartupModules(new Dictionary<string, IEnumerable<string>?>() {
		{"SignalGenerator", new string[] {"LogKeyboard=1", "LogMouse=1"}},
		{"DummySignalProcessing", null},
		{"DummyApplication", null},
		});


bci.Visualize("MousePosX");
bci.Visualize("MousePosY");

bci.WaitForSystemState(BCI2000Remote.SystemState.Running);

while(bci.GetSystemState() == BCI2000Remote.SystemState.Running) {
	Console.WriteLine($"{bci.GetEvent("MousePosX")} {bci.GetEvent("MousePosY")}");
	Thread.Sleep(100);
}

bci.connection.Quit();
