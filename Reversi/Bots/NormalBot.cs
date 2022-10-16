using System;

namespace Reversi.Bot
{
	/// <summary>
	/// NormalBot, calculates the next moves by choosing a position that limits the opponent's possible move
	/// </summary>
	public class NormalBot : IPlayer
	{
		private Random random = new Random();

		public PositionInformation NextMove(Board board, Player player)
		{
			return Recurse(board, player, 1);
		}

		private PositionInformation Recurse(Board board, Player player, int depth = 0, int level = 0)
		{
			PositionInformation bestPos = PositionInformation.Empty();
			foreach (PositionInformation pos in board.GetPossiblePositionInformation(player))
			{
				float score = pos.Flipped.Count;

				float enemyMoves = 0;
				Board newboard = board.Clone();
				newboard.Place(pos.Pos, player);

				if (newboard.CanPlayerPlay(player.Opposite()))
				{
					enemyMoves = newboard.GetPossibleMoves(player.Opposite()).Count;
					score -= enemyMoves;

					if (level < depth)
					{
						PositionInformation enemyPos = Recurse(newboard, player.Opposite(), depth, level + 1);
						score -= enemyPos.Score;
					}
				}
				else
				{
					// if enemy cannot move or is game over, grant higher score
					if (newboard.IsGameOver())
						score += 100000;
					else
						score += 1000;
				}

				score += (float)random.NextDouble();

				if (score > bestPos.Score)
				{
					bestPos = new PositionInformation(pos.Pos, score, pos.Flipped);
				}
			}
			return bestPos;
		}
	}
}