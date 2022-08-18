﻿using CrawlGen.Gen;
using CrawlGen.Grid;

namespace CrawlGen.Model.Dungeon
{
    public class Room
    {
        public int ID = -1;
        public Anchor Anchor = new Anchor();

        internal List<Room> Connections = new();
        public string? Name;
        public List<string> Treasure = new();

        public override string ToString() => $"{ID}: {Name ?? "???"}";

        public void ChooseName()
        {
            string[] names = new[] { "Storage Room", "Hallway", "Library", "Prison", "Dining Hall", "Courtyard", "Guard post", "Kitchen", "Labratory", "Stables", "Study/Office", "Throne Room", "Workspace", "Thophy Room" };
            string[] prefixes = new[] { "Dusty ", "Moldy ", "Slimy ", "Foggy ", "Creepy ", "Well Maintained " };

            Name = Rng.TakeOne(names);
            if (Rng.P(0.6))
                Name = Rng.TakeOne(prefixes) + Name;
        }

        public void AddEncounter() {
            //TODO:
        }
    }
}
