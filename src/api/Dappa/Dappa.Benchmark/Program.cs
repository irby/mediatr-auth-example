// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Dappa.Benchmark;

BenchmarkRunner.Run<BenchmarkMediatorVsDirectCall>();
