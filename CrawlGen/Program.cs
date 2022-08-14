// See https://aka.ms/new-console-template for more information
using CrawlGen.Gen;

var world = WorldGen.MakeWorld();
CrawlGen.Out.Utils.HTMLPage page = new();
CrawlGen.Out.OverworldWriter.WriteWrold(world, page);