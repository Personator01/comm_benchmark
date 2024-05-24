#!/bin/dotnet fsi
#r "../../bci2000/prog/BCI2000RemoteNET.dll"
open BCI2000RemoteNET
open System.Runtime.InteropServices;
open System.Threading;

let bci = new BCI2000Remote(new BCI2000Connection()) in

bci.connection.StartOperator "../../bci2000/prog/Operator";
bci.connection.Connect();

bci.StartupModules <| Map [
  ("SignalGenerator", ["LogKeyboard=1"; "SpinningWheel=1"; "ShowDisplayStatistics=1"]);
  ("DummySignalProcessing", null);
  ("StimulusPresentation", null);
  ]

bci.WaitForSystemState BCI2000Remote.SystemState.Running;

let mutable lv = 0 in
let rec repeat () = 
  match bci.GetSystemState() with
  | BCI2000Remote.SystemState.Running -> 
    bci.SetEvent("ev", (uint32) lv);
    lv <- ( match lv with 
            | 0 -> 1
            | _ -> 0
    )
    Thread.Sleep(1000);
    repeat ();
  | _ -> ();
