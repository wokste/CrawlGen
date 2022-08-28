// See https://aka.ms/new-console-template for more information
using CrawlGen.Gen;
using System.Diagnostics;

using YamlDotNet.Serialization;

var serializer = new SerializerBuilder()
    .Build();

var skeletons = new CrawlGen.Model.Encounters.Monster[] { EncounterGen.Make().Monster };

var yaml = serializer.Serialize(skeletons);
System.Console.WriteLine(yaml);


var world = WorldGen.MakeWorld();

{
    using CrawlGen.Writers.Utils.HTMLPage page = new(@"output/test.html");
    using var htmlDom = page.MakeDom("html");
    using var bodyDom = page.MakeDom("body");
    CrawlGen.Writers.OverworldWriter.WriteWrold(world, page);
}

Process.Start(new ProcessStartInfo(Path.GetFullPath(@"output/test.html")) {
    UseShellExecute = true
});
