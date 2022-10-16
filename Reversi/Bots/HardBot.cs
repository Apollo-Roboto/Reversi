using System;

namespace Reversi.Bot
{
	/// <summary>
	/// NormalBot, Recursively calculates the next moves by choosing a position that limits the opponent's possible move and prioritizes corners.
	/// </summary>
	public class HardBot : IPlayer
	{
		private Random random = new Random();

		public PositionInformation NextMove(Board board, Player player)
		{
			return Recurse(board, player, 3);
		}

		private PositionInformation Recurse(Board board, Player player, int depth = 0, int level = 0)
		{
			PositionInformation bestPos = PositionInformation.Empty();
			foreach (PositionInformation pos in board.GetPossiblePositionInformation(player))
			{
				float score = pos.Flipped.Count;

				// if position is a corner
				if (pos.Pos == new Position(0, 0) ||
					pos.Pos == new Position(8, 0) ||
					pos.Pos == new Position(0, 8) ||
					pos.Pos == new Position(8, 8))
				{
					score += 2000;
				}

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