using System;

namespace Reversi
{
	public static class PlayerExtensions
	{
		public static char Short(this Player player)
		{
			return player.ToString()[0];
		}

		public static Player Opposite(this Player player)
		{
			switch (player)
			{
				case Player.BLACK:
					return Player.WHITE;
				case Player.WHITE:
					return Player.BLACK;
				default:
					return Player.NONE;
			}
		}
	}

	public enum Player
	{
		NONE, WHITE, BLACK
	}
}