using System.Drawing;
using Console = Colorful.Console;

namespace ProcGenTiles
{
	public class Program
	{


		public static void Main()
		{
			int Height = 10;
			int Width = 25;
			float noiseScale = 0.5f;
			float noiseFrequency = 0.25f;
			int seed = 10;
			Map map = new Map(Height, Width);
			
			Pathfinding path = new Pathfinding(map);
			
			
			//Terrain definitions and such
			Dictionary<Range, TerrainInfo> elevationCharacters = new Dictionary<Range, TerrainInfo>();
			TerrainInfo deepWater = new TerrainInfo(Color.DarkBlue, '~', "Deep Water");
			TerrainInfo shallowWater = new TerrainInfo(Color.LightBlue, '`', "Shallow Water");
			TerrainInfo lowGround = new TerrainInfo(Color.LightGreen, '.', "Low Ground");
			TerrainInfo mediumGround = new TerrainInfo(Color.Green, '_', "Medium Ground");
			TerrainInfo highlands = new TerrainInfo(Color.DarkGray, '-', "Highland");
			TerrainInfo mountains = new TerrainInfo(Color.Gray, '^', "Mountain");
			
			//Add the definitions to the ranges
			elevationCharacters.Add(new Range(-1f, -0.5f), deepWater); //If under 0.5 use this symbol
			elevationCharacters.Add(new Range(-0.5f, 0),shallowWater);
			elevationCharacters.Add(new Range(0, 0.3f), lowGround);
			elevationCharacters.Add(new Range(0.3f, 0.6f), mediumGround);
			elevationCharacters.Add(new Range(0.6f, 0.9f), highlands);
			elevationCharacters.Add(new Range(0.9f, 1.1f), mountains); //Push the range up slightly higher so we don't miss tiles


			//Init map and prep noise for terrain layer
			
			FastNoiseLite noise = new FastNoiseLite();
			noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
			noise.SetFrequency(noiseFrequency);
			noise.SetSeed(seed);

			//Loop through width and height and generate noise for terrain elevation
			//And print to the console while we're here.
			string layerName = "Elevation";
			for (int i = 0; i < Height; i++)
			{
				for (int j = 0; j < Width; j++)
				{
					float value = noise.GetNoise(i * noiseScale, j * noiseScale);
					map.Tiles[i,j].ValuesHere.Add(layerName, value);
					TerrainInfo found = null;
					foreach (Range r in elevationCharacters.Keys)
					{
						if (r.InRange(value))
						{
							found = elevationCharacters[r];
							break; //We found the character, exit the loop
						}
					}
					//Console.Write(elevationIcon + "Noise: " + value);
					Console.Write(found.DisplayCharacter, found.GlyphColor);
				}
				Console.WriteLine("");
			}

			Console.WriteLine("");
			Console.WriteLine("----------");
			Console.WriteLine("");
			path.LandWaterFloodfill((0,0)); //Just floodfill from the first tile and mark everything 0 or 1
			PrintLayerValues(map, "Land");
			Console.WriteLine("");
			Console.WriteLine("----------");
			Console.WriteLine("");
			path.MarkAllRegions();
			PrintLayerValues(map, "Region");
		}
		
		private static void PrintLayerValues(Map map, string layer)
		{
			for (int x = 0; x < map.Width; x++)
			{
				for (int y = 0; y < map.Height; y++)
				{
					Tile t = map.GetTile((x, y));
					float value = t.ValuesHere[layer];
					Color color;
					if (value == 1)
						color = Color.Green;
					else
						color = Color.Blue;
					Console.Write(value, color);
				}
				Console.WriteLine("");
			}
		}

	}
}