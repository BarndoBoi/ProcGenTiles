using System.Drawing;

namespace ProcGenTiles{
	public class TerrainInfo
	{
		Color GlyphColor;
		Color BackgroundColor;
		char DisplayCharacter;
		
		public TerrainInfo(Color glyph, Color background, char symbol)
		{
			GlyphColor = glyph;
			BackgroundColor = background;
			DisplayCharacter = symbol;
		}
		
	}
}