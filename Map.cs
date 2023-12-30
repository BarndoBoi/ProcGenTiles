namespace ProcGenTiles
{
    public class Map
    {
        public Tile[,] Tiles { get; set; }
        public int width, height;

        public Map()
        {
            Tiles = new Tile[width, height];
            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    Tiles[x,y] = new Tile();
                }
            }
        }
    }
}