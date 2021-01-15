﻿using System.Linq;

using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using xTile.Tiles;

using AllowBeachSprinklers.Helpers;

namespace AllowBeachSprinklers
{
	/// <summary>
	/// The mod entry point.
	/// </summary>
	public class ModEntry : Mod
	{
		private const int BEACH_FARM = 6;
		private const string NO_SPRINKLERS = "NoSprinklers";
		private const string SAND_LAYER = "Back";

		/// <summary>
		/// The mod entry point, called after the mod is first loaded.
		/// </summary>
		/// <param name="helper">Provides simplified APIs for writing mods.</param>
		public override void Entry(IModHelper helper)
		{
			helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
		}

		/// <summary>
		/// Raised after the user has loaded a save and the world is initialized.
		/// </summary>
		/// <param name="sender">The event sender.</param>
		/// <param name="e">The event data. NOTE: Likely unused.</param>
		private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
		{
			if (Game1.whichFarm == BEACH_FARM)
			{
				var farm = Game1.getFarm();
				var tileList = farm.Map.GetLayer(SAND_LAYER).Tiles.Array.Cast<Tile>().ToList();

				var noSprinklerTiles = tileList
					.Where(tile => tile?.TileIndexProperties != null && tile.TileIndexProperties.ContainsKey(NO_SPRINKLERS))
					.Distinct(new TileEqualityComparer())
					.OrderBy(tile => tile.TileIndex);

				foreach (var tile in noSprinklerTiles)
				{
					tile.TileIndexProperties[NO_SPRINKLERS] = false;
				}

				Monitor.Log("Sprinklers can be placed in sand on your farm.", LogLevel.Info);
			}
		}
	}
}
