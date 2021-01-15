using System.Collections.Generic;
using xTile.Tiles;

namespace AllowBeachSprinklers.Helpers
{
	internal class TileEqualityComparer : IEqualityComparer<Tile>
	{
		public bool Equals(Tile x, Tile y)
		{
			if (ReferenceEquals(x, y)) return true;
			if (ReferenceEquals(x, null)) return false;
			if (ReferenceEquals(y, null)) return false;
			if (x.GetType() != y.GetType()) return false;

			return Equals(x.TileSheet, y.TileSheet) && x.TileIndex == y.TileIndex;
		}

		public int GetHashCode(Tile obj)
		{
			unchecked
			{
				var hashCode = (obj.Layer != null ? obj.Layer.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (int) obj.BlendMode;
				hashCode = (hashCode * 397) ^ (obj.TileSheet != null ? obj.TileSheet.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ obj.TileIndex;
				hashCode = (hashCode * 397) ^ (obj.TileIndexProperties != null ? obj.TileIndexProperties.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}
