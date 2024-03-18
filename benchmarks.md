```

BenchmarkDotNet v0.13.12, macOS Sonoma 14.2.1 (23C71) [Darwin 23.2.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD


```
| Method              | Mean     | Error   | StdDev  |
|-------------------- |---------:|--------:|--------:|
| BenchmarkDirectCall | 520.5 ns | 5.76 ns | 5.39 ns |
| BenchmarkMediatr    | 766.4 ns | 8.45 ns | 7.06 ns |
