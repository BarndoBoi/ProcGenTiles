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
            int seed = 1;
            Dictionary<Range, char> elevationCharacters = new Dictionary<Range, char>();
            elevationCharacters.Add(new Range(-1f, -0.5f), '~'); //If under 0.5 use this symbol
            elevationCharacters.Add(new Range(-0.5f, 0),'`');
            elevationCharacters.Add(new Range(0, 0.3f), '.');
            elevationCharacters.Add(new Range(0.3f, 0.6f), '_');
            elevationCharacters.Add(new Range(0.6f, 0.9f), '-');
            elevationCharacters.Add(new Range(0.9f, 1.1f), '^'); //Push the range up slightly higher so we don't miss tiles


            //Init map and prep noise for terrain layer
            Map map = new Map(Height, Width);
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
                    char elevationIcon = elevationCharacters.Values.First();
                    foreach (Range r in elevationCharacters.Keys)
                    {
                        if (r.InRange(value))
                        {
                            elevationIcon = elevationCharacters[r];
                            break; //We found the character, exit the loop
                        }
                    }
                    //Console.Write(elevationIcon + "Noise: " + value);
                    Console.Write(elevationIcon);
                }
                Console.WriteLine("");
            }

        }

    }
}