using System;
using System.Collections.Generic;

namespace ApolloRoboto.Reversi.Bot
{
	/// <summary>
	/// Easy bot, only plays randomly across all possible moves.
	/// </summary>
	public class EasyBot : IPlayer
	{
		private Random random = new Random();

		public PositionInformation NextMove(Board board, Player player)
		{
			List<PositionInformation> possibleMoves = board.GetPossiblePositionInformation(player);
			int choose = random.Next(0, possibleMoves.Count);

			return possibleMoves[choose];
		}
	}
}