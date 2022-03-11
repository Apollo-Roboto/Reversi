using System;
using System.Collections.Generic;

namespace ReversiBot
{
	public class Solver
	{
		private static Random random = new Random();

		public static PositionScore Random(Board board, Player player)
		{
			Random rand = new Random();
			List<PositionScore> possibleMoves = board.GetPossiblePositionScore(player);
			int choose = rand.Next(0, possibleMoves.Count);

			return possibleMoves[choose];
		}

		public static PositionScore OneLevel(Board board, Player player)
		{
			PositionScore bestPos = new PositionScore(new Vector2(-1, -1), int.MinValue);
			foreach (PositionScore pos in board.GetPossiblePositionScore(player))
			{
				if (pos.Score > bestPos.Score)
				{
					bestPos = pos;
				}
			}
			return bestPos;
		}

		public static PositionScore TwoLevel(Board board, Player player)
		{
			PositionScore bestPos = new PositionScore(new Vector2(-1, -1), float.MinValue);
			foreach (PositionScore pos in board.GetPossiblePositionScore(player))
			{
				float score = pos.Score;

				float enemyMoves = 0;
				Board newboard = board.Clone();
				newboard.Place(pos.Pos, player);
				if (newboard.CanPlayerPlay(player.Opposite()))
					// find how many moves they can make
					enemyMoves = newboard.GetPossibleMoves(player.Opposite()).Count;

				score -= enemyMoves;

				if (score > bestPos.Score)
				{
					bestPos = new PositionScore(pos.Pos, score);
				}
			}
			return bestPos;
		}

		public static PositionScore Recursion1(Board board, Player player, int depth = 0, int level = 0)
		{
			PositionScore bestPos = new PositionScore(new Vector2(-1, -1), float.MinValue);
			foreach (PositionScore pos in board.GetPossiblePositionScore(player))
			{
				float score = pos.Score;

				float enemyMoves = 0;
				Board newboard = board.Clone();
				newboard.Place(pos.Pos, player);

				if (newboard.CanPlayerPlay(player.Opposite()))
				{
					enemyMoves = newboard.GetPossibleMoves(player.Opposite()).Count;
					score -= enemyMoves;

					if (level < depth)
					{
						PositionScore enemyPos = Recursion1(newboard, player.Opposite(), depth, level + 1);
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
					bestPos = new PositionScore(pos.Pos, score);
				}
			}
			return bestPos;
		}

		static private float ScoreResolver(Vector2 pos, int diskFlipped, int enemyMoves)
		{
			/// todo use configuration file values
			return 0;
		}
	}
}