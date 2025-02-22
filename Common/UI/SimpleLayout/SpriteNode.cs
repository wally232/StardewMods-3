using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardewValley;

namespace Leclair.Stardew.Common.UI.SimpleLayout {
	public class SpriteNode : ISimpleNode {

		public SpriteInfo Sprite { get; }
		public float Scale { get; }
		public int Quantity { get; }
		public string Label { get; }

		public Alignment Alignment { get; }

		public bool DeferSize => false;

		public SpriteNode(SpriteInfo sprite, float scale = 4f, string label = null, int quantity = 0, Alignment alignment = Alignment.None) {
			Sprite = sprite;
			Scale = scale;
			Label = label;
			Quantity = quantity;
			Alignment = alignment;
		}

		public Vector2 GetSize(SpriteFont defaultFont, Vector2 containerSize) {
			float height = 16 * Scale;
			float width = height;

			if (!string.IsNullOrEmpty(Label)) {
				Vector2 size = Game1.smallFont.MeasureString(Label);
				width += (4 * Scale) + size.X;
				height = Math.Max(height, size.Y);
			}

			return new Vector2(width, height);
		}

		public void Draw(SpriteBatch batch, Vector2 position, Vector2 size, Vector2 containerSize, float alpha, SpriteFont defaultFont, Color? defaultColor, Color? defaultShadowColor) {

			float itemSize = 16 * Scale;
			float offsetY = (size.Y - itemSize) / 2;

			// Draw Object
			Sprite?.Draw(
				batch,
				offsetY != 0 ?
					new Vector2(position.X, position.Y + offsetY)
					: position,
				Scale
			);

			// Draw Quantity
			if (Quantity > 0) {
				float qScale = (float) Math.Round(Scale * 0.75f);
				float qX = position.X + itemSize - Utility.getWidthOfTinyDigitString(Quantity, qScale) + qScale;
				float qY = position.Y + itemSize - 6f * qScale + 2f;

				Utility.drawTinyDigits(Quantity, batch, new Vector2(qX, qY), qScale, 1f, Color.White * alpha);
			}

			// Draw Label
			if (!string.IsNullOrEmpty(Label)) {
				Vector2 labelSize = Game1.smallFont.MeasureString(Label);
				if (defaultShadowColor.HasValue)
					Utility.drawTextWithColoredShadow(
						b: batch,
						text: Label,
						font: Game1.smallFont,
						position: new Vector2(
							position.X + itemSize + (4 * Scale),
							position.Y + ((size.Y - labelSize.Y) / 2)
							),
						color: (defaultColor ?? Game1.textColor) * alpha,
						shadowColor: (defaultShadowColor.Value * alpha)
					);
				else
					Utility.drawTextWithShadow(
						b: batch,
						text: Label,
						font: Game1.smallFont,
						position: new Vector2(
							position.X + itemSize + (4 * Scale),
							position.Y + ((size.Y - labelSize.Y) / 2)
							),
						color: (defaultColor ?? Game1.textColor) * alpha
					);
			}
		}
	}
}
