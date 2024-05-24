default: all


clockres: 
	csc /out:bin/clockres clockres.cs


basic_benchmark:
	csc /r:"BCI2000RemoteNET.dll" /out:bin/basic_benchmark basic_benchmark.cs
