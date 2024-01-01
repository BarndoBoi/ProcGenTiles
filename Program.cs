namespace ProcGenTiles
{
    public class Program
    {


        public static void Main()
        {
            int width = 10;
            int height = 10;
            float noiseScale = 3.78f;
            Dictionary<float, char> elevationCharacters = new Dictionary<float, char>();
            elevationCharacters.Add(-0.5f, '~'); //If under 0.5 use this symbol
            elevationCharacters.Add(0,'`');
            elevationCharacters.Add(0.3f, '.');
            elevationCharacters.Add(0.6f, '_');
            elevationCharacters.Add(0.75f, '-');
            elevationCharacters.Add(1, '^');


            //Init map and prep noise for terrain layer
            Map map = new Map(width, height);
            FastNoiseLite noise = new FastNoiseLite();
            noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);

            noise.SetFrequency(70.8f);

            //Loop through width and height and generate noise for terrain elevation
            //And print to the console while we're here.
            string layerName = "Elevation";
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    float value = noise.GetNoise(i * noiseScale, j * noiseScale);
                    map.Tiles[i,j].ValuesHere.Add(layerName, value);
                    char elevationIcon = elevationCharacters.Values.First();
                    foreach (float f in elevationCharacters.Keys)
                    {
                        if (f <= value)
                        {
                            elevationIcon = elevationCharacters[f];
                            break; //We found the character, exit the loop
                        }
                    }
                    Console.Write(elevationIcon);
                }
                Console.WriteLine("");
            }

        }

    }
}