using System.Linq;

using Leclair.Stardew.BetterCrafting.Models;
using Leclair.Stardew.Common;
using Leclair.Stardew.Common.Crafting;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardewValley;

namespace Leclair.Stardew.BetterCrafting.Integrations.RaisedGardenBeds {
	public class GardenPotRecipe : IRecipe {

		private readonly RGBIntegration RGB;

		private readonly CraftingRecipe Recipe;
		private readonly string Variant;
		private readonly object Info;
		private readonly string SpriteKey;

		public GardenPotRecipe(RGBIntegration rgb, CraftingRecipe recipe) {
			RGB = rgb;
			Recipe = recipe;

			Variant = RGB.GetVariantKeyFromName(Name);
			DisplayName = RGB.GetDisplayNameFromVariantKey(Variant);
			Description = RGB.GetRawDescription();
			Info = RGB.GetItemDefinition(Variant);

			SpriteKey = RGB.GetSpriteKey(Info);

			Ingredients = recipe.recipeList
				.Select(val => new BaseIngredient(val.Key, val.Value))
				.ToArray();
		}

		// Identity

		public int SortValue => Recipe.itemToProduce[0];

		// Display

		public string Name => Recipe.name;
		public string DisplayName { get; }
		public string Description { get; }

		public virtual int GetTimesCrafted(Farmer who) {
			if (who.craftingRecipes.ContainsKey(Name))
				return who.craftingRecipes[Name];

			return 0;
		}

		public CraftingRecipe CraftingRecipe => Recipe;

		// Display

		//public SpriteInfo Sprite => new(Texture, SourceRectangle);

		public Texture2D Texture => RGB.GetSprite(SpriteKey);

		public Rectangle SourceRectangle => RGB.GetSpriteSourceRectangle(spriteIndex: RGB.GetSpriteIndex(Info));

		public int GridHeight => Recipe.bigCraftable ? 2 : 1;

		public int GridWidth => 1;

		// Cost

		public int QuantityPerCraft => Recipe.numberProducedPerCraft;

		public IIngredient[] Ingredients { get; }

		public bool Stackable => true;

		public Item CreateItem() {
			return RGB.MakeOutdoorPot(Variant);
		}
	}
}
