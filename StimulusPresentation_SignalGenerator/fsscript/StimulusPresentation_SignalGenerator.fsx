#!/bin/dotnet fsi
#r "../../BCI2000RemoteNET.dll"
open BCI2000RemoteNET
open System.Runtime.InteropServices;
open System.Threading;
let bci = new BCI2000Remote(new BCI2000Connection()) in
let isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) in
let operatorPath = "../../../bci2000/prog/Operator" + if isWindows then ".exe" else "" in
bci.connection.StartOperator(operatorPath);
bci.connection.Connect ();
bci.LoadParameters "../parms/examples/StimulusPresentation_SignalGenerator.prm";
bci.StartupModules <| Map [
  ("SignalGenerator", ["LogKeyboard=1"; "SpinningWheel=1"; "ShowDisplayStatistics=1"]);
  ("P3SignalProcessing", null);
  ("StimulusPresentation", null);
  ]
