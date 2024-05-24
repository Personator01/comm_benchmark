using BCI2000RemoteNET;

BCI2000Connection conn = new();

conn.StartOperator("/home/tytbu/bcistuff/bci2000/prog/Operator", address: "127.0.0.1", port: 3999);
Thread.Sleep(1000);
conn.Connect(address: "127.0.0.1", port: 3999);

BCI2000Remote bci = new(conn);

bci.AddParameter("Application:Place", "Parmam", "0", "0", "10");
bci.AddEvent("AEvent", 32);

bci.StartupModules(new Dictionary<string, IEnumerable<string>?>() {
		{"SignalGenerator", null},
		{"DummySignalProcessing", null},
		{"DummyApplication", null},
		});

bci.Visualize("AEvent");

bci.WaitForSystemState(BCI2000Remote.SystemState.Running);

bci.SetEvent("AEvent", 400);

Console.ReadLine();

bci.connection.Quit();
