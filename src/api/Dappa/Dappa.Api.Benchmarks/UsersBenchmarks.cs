using System.Text.Json.Serialization;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Dappa.Core.Features.Auth.Login;
using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json;

namespace Dappa.Api.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class UsersBenchmarks
{
    [Benchmark]
    public async Task Benchmark()
    {
        var http = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5000"),
        };
        var content = new LoginCommand()
        {
            Username = "user",
            Password = "pass",
        };
        await http.PostAsync("/api/auth/login", new StringContent(JsonConvert.SerializeObject(content)));
    }
}
