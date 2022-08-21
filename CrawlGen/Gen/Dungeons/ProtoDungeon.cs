using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Gen.Dungeons
{
    /// <summary> A protoDungeon uses a grid to define the dungeon layout. It doesn't . </summary>
    internal class ProtoDungeon
    {
        int Width, Height;
        bool[,] PassHor;
        bool[,] PassVert;
		ushort[,] IDs;

        public ProtoDungeon(int width, int height)
        {
            Width = width;
            Height = height;

            PassHor = new bool[Width, Height + 1];
            PassVert = new bool[Width + 1, Height];
			IDs = new ushort[Width, Height];
		}

		/// <summary>
		/// Generates a proto-dungeon with some default options
		/// </summary>
		/// <returns></returns>
		public static ProtoDungeon Generate() {
			var maze = new ProtoDungeon(4, 4);
			maze.BSPMaze();
			maze.KickDownWalls(0.1, 0.3);
			maze.SolidifyLeaves(3, 0.7);
			return maze;
		}


		internal void BSPMaze() => BSPMaze(0, 0, Width, Height);
		void BSPMaze(int x0, int y0, int x1, int y1)
		{
			if (x1 - x0 == 1)
			{
				for (int y = y0 + 1; y < y1; ++y)
					PassHor[x0, y] = true;
				return;
			}
			if (y1 - y0 == 1)
			{
				for (int x = x0 + 1; x < x1; ++x)
					PassVert[x, y0] = true;
				return;
			}

			if (Rng.P(0.5))
			{
				// Split on the X axis
				var x = Rng.UniformInt(x0 + 1, x1);
				var y = Rng.UniformInt(y0, y1);

				PassVert[x, y] = true;

				BSPMaze(x0, y0, x, y1);
				BSPMaze(x, y0, x1, y1);
			}
			else
			{
				// Split on the Y axis
				var x = Rng.UniformInt(x0, x1);
				var y = Rng.UniformInt(y0 + 1, y1);

				PassHor[x, y] = true;

				BSPMaze(x0, y0, x1, y);
				BSPMaze(x0, y, x1, y1);
			}
		}

		/// <summary>
		/// Kicks down some walls to add cycles to the dungeon.
		/// </summary>
		/// <param name="normalProb">The probability that any given wall will be kicked down.</param>
		/// <param name="borderProb">The probability if the wall is on a "border". This should be higher than the normal probability to account for the fewer walls that a cell has.</param>
		public void KickDownWalls(double normalProb, double borderProb)
		{
			for (int y = 1; y < Height; ++y)
				for (int x = 0; x < Width; ++x)
				{
					var prob = (x == 0 || x == Width - 1) ? borderProb : normalProb;
					if (!PassHor[x, y] && Rng.P(prob))
						PassHor[x, y] = true;
				}

			for (int y = 0; y < Height; ++y)
				for (int x = 1; x < Width; ++x)
				{
					var prob = (y == 0 || y == Height - 1) ? borderProb : normalProb;
					if (!PassVert[x, y] && Rng.P(prob))
						PassVert[x, y] = true;
				}
		}

#if DEBUG
		/// <summary>
		/// Debug function to show how the map looks like.
		/// Feel free to remove when we have a proper dungeon renderer.
		/// </summary>
		public void Print() {
			for (int y = 0; y <= Height; ++y)
			{
				for (int x = 0; x < Width; ++x)
				{
					Console.Write('+');
                    Console.Write(PassHor[x,y] ? "   " : "---");
				}
				Console.WriteLine("+");

				if (y != Height)
				{
					for (int x = 0; x <= Width; ++x)
					{
						Console.Write(PassVert[x, y] ? ' ' : '|');
						if (x != Width)
                            Console.Write(IDs[x, y] == ushort.MaxValue ? "###" : IDs[x, y].ToString().PadLeft(3, ' '));
					}
					Console.WriteLine();
				}
			}		
		}
#endif
		/// <summary> Counts the number of passages for a single square. </summary>
		int CountEdges(int x, int y) => (PassHor[x, y] ? 1 : 0) + (PassHor[x, y + 1] ? 1 : 0) + (PassVert[x, y] ? 1 : 0) + (PassVert[x + 1, y] ? 1 : 0);

		/// <summary> Makes the tile solid, meaning it has no content in the dungeon. </summary>
		void MakeSolid(int x, int y)
		{
			PassHor[x, y] = PassHor[x, y + 1] = PassVert[x, y] = PassVert[x + 1, y] = false;
			IDs[x, y] = ushort.MaxValue;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="iterations"></param>
		/// <param name="prob"></param>
		public void SolidifyLeaves(int iterations, double prob)
		{
			IEnumerable<(int,int)> GetLeaves()
			{
				for (int y = 0; y < Height; ++y)
					for (int x = 0; x < Width; ++x)
						if (CountEdges(x,y) == 1 && Rng.P(prob))
							yield return (x,y);
			}

			for (int i = 0; i < iterations; ++i)
			{
				var leaves = GetLeaves().ToArray();
				foreach (var (x,y) in leaves)
				{
					MakeSolid(x, y);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>The x and y coordinate of each square</returns>
		internal IEnumerable<(int, int)> GetSquaresAndAssignIDs()
		{
			ushort idAutoInc = 0;
			for (int y = 0; y < Height; ++y)
			{
				for (int x = 0; x < Width; ++x)
				{
					if (IDs[x, y] == ushort.MaxValue)
						continue;

					IDs[x, y] = idAutoInc++;
					yield return (x, y);
				}
			}
		}

		/// <returns>The passages between squares that don't contain a wall</returns>
		internal IEnumerable<(ushort, ushort)> GetPassages()
		{
			for (int y = 1; y < Height; ++y)
				for (int x = 0; x < Width; ++x)
					if (PassHor[x, y])
						yield return (IDs[x, y - 1], IDs[x, y]);

			for (int y = 0; y < Height; ++y)
				for (int x = 1; x < Width; ++x)
					if (PassVert[x, y])
						yield return (IDs[x - 1, y], IDs[x, y]);
		}
	}
}
