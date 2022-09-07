using System;
using System.Collections.Generic;

namespace Reversi
{
	public class TwoLevelBot : IPlayer
	{
		private Random random = new Random();
		
		public PositionInformation NextMove(Board board, Player player)
		{
			PositionInformation bestPos = PositionInformation.Empty();
			foreach (PositionInformation pos in board.GetPossiblePositionInformation(player))
			{
				float score = pos.Flipped.Count;

				float enemyMoves = 0;
				Board newboard = board.Clone();
				newboard.Place(pos.Pos, player);
				if (newboard.CanPlayerPlay(player.Opposite()))
					// find how many moves they can make
					enemyMoves = newboard.GetPossibleMoves(player.Opposite()).Count;

				score -= enemyMoves;

				score += (float)random.NextDouble();

				if (score > bestPos.Score)
					bestPos = new PositionInformation(pos.Pos, score, pos.Flipped);
			}
			return bestPos;
		}
	}
}