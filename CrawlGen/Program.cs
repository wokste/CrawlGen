// See https://aka.ms/new-console-template for more information
using CrawlGen.Gen;

var world = WorldGen.MakeWorld();
CrawlGen.Writers.Utils.HTMLPage page = new();
CrawlGen.Writers.OverworldWriter.WriteWrold(world, page);