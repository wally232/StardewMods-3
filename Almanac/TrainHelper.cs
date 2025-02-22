using System;

using Microsoft.Xna.Framework;

using StardewValley;

namespace Leclair.Stardew.Almanac {
	public static class TrainHelper {

		public static readonly Rectangle TRAIN = new(577, 512, 126, 62);

		public static bool IsTrainDay(WorldDate date) {
			return GetTrainTime(date) >= 0;
		}

		public static int GetTrainTime(WorldDate date) {
			int days = date.TotalDays + 1;
			if (days < 32U)
				return -1;

			Random rnd = new((int) Game1.uniqueIDForThisGame / 2 + days);
			if (rnd.NextDouble() >= 0.2)
				return -1;

			int time = rnd.Next(900, 1800);
			time -= time % 10;

			if (time % 100 > 50)
				return -1;

			return time;
		}

	}
}
