// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Dappa.Api.Benchmarks;

BenchmarkRunner.Run<UsersBenchmarks>(new ManualConfig().WithOptions(ConfigOptions.DisableOptimizationsValidator));
