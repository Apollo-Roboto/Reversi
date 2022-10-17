using System;

namespace ApolloRoboto.Reversi
{
	public static class WinningPlayerExtensions
	{
		public static char Short(this WinningPlayer player)
		{
			return player.ToString()[0];
		}
	}

	public enum WinningPlayer
	{
		NONE, WHITE, BLACK, TIE
	}
}