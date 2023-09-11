using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using xTile.Tiles;

using AllowBeachSprinklers.Helpers;
using System.Linq;

namespace AllowBeachSprinklers
{
	public class ModEntry : Mod
	{
		private const int BEACH_FARM = 6;
		private const string NO_SPRINKLERS_KEY = "NoSprinklers";
		private const string SAND_LAYER_NAME = "Back";

		public override void Entry(IModHelper helper)
		{
			helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
		}

		private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
		{
			if (Game1.whichFarm != BEACH_FARM) return;

			var farm = Game1.getFarm();
			var tileList = farm.Map.GetLayer(SAND_LAYER_NAME).Tiles.Array.Cast<Tile>().ToList();

			var noSprinklerTiles = tileList
				.Where(tile => tile?.TileIndexProperties != null && tile.TileIndexProperties.ContainsKey(NO_SPRINKLERS_KEY))
				.Distinct(new TileEqualityComparer())
				.OrderBy(tile => tile.TileIndex);

			foreach (var tile in noSprinklerTiles)
			{
				tile.Properties[NO_SPRINKLERS_KEY] = "false";
			}

			Monitor.Log("Sprinklers can now be placed in sand on your farm.", LogLevel.Info);
		}
	}
}
