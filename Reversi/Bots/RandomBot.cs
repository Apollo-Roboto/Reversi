using System;
using System.Collections.Generic;

namespace ReversiBot
{
	public class RandomBot : IPlayer
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