﻿using System.Linq;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using xTile.Tiles;
using AllowBeachSprinklers.Helpers;


namespace AllowBeachSprinklers
{
	public class ModEntry : Mod
	{
		private const int BeachFarm = 6;
		private const string NoSprinklersKey = "NoSprinklers";
		private const string SandLayerName = "Back";

		public override void Entry(IModHelper helper)
		{
			I18n.Init(helper.Translation);

			helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
		}

		private void OnSaveLoaded(object? sender, SaveLoadedEventArgs e)
		{
			if (Game1.whichFarm != BeachFarm) return;

			var farm = Game1.getFarm();
			var tileList = farm.Map.GetLayer(SandLayerName).Tiles.Array.Cast<Tile>().ToList();

			var noSprinklerTiles = tileList
				.Where(tile => tile?.TileIndexProperties != null && tile.TileIndexProperties.ContainsKey(NoSprinklersKey))
				.Distinct(new TileEqualityComparer())
				.OrderBy(tile => tile.TileIndex);

			foreach (var tile in noSprinklerTiles)
			{
				tile.TileIndexProperties[NoSprinklersKey] = false;
			}

			Monitor.Log(I18n.Log_SprinklersAllowed(), LogLevel.Info);
		}
	}
}
