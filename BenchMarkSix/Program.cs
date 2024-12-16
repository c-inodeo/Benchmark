using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchMarkSix;

var config = ManualConfig.Create(DefaultConfig.Instance)
    .AddExporter(new BenchmarkDotNet.Exporters.HtmlExporter());

BenchmarkRunner.Run<BenchmarkTests>(config);