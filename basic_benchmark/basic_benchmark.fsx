#!/bin/dotnet fsi
#r "../BCI2000RemoteNET.dll"

open BCI2000RemoteNET;
open System.Threading;
open System.Diagnostics;
open System;


let bci = new BCI2000Remote(new BCI2000Connection()) in

bci.connection.StartOperator("../../bci2000/prog/Operator");
bci.connection.Connect ();

bci.AddEvent("test_event", 1);

bci.StartupModules <| Map [
  ("SignalGenerator", null);
  ("DummySignalProcessing", null);
  ("DummyApplication", null);
  ];

bci.Visualize("test_event");

let sw = new Stopwatch(); 

bci.WaitForSystemState(BCI2000Remote.SystemState.Running);

let rec repeat() =
  match bci.GetSystemState() with 
  | BCI2000Remote.SystemState.Running ->
    sw.Start();
    bci.SetEvent("test_event", 1u);
    sw.Stop();
    printfn "Roundtrip time: %dms %dmcs" (sw.Elapsed.Milliseconds) (sw.Elapsed.Microseconds);
    sw.Reset();
    Thread.Sleep(100);
    bci.SetEvent("test_event", 0u);
    Thread.Sleep(400);
    repeat ();
  | _ -> ()

repeat();
