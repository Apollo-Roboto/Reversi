using System;
using System.Collections.Generic;

namespace ReversiBot
{
	public class Solver
	{

		public static PositionScore Random(Board board, Player player)
		{
			Random rand = new Random();
			List<PositionScore> possibleMoves = board.GetPossiblePositionScore(player);
			int choose = rand.Next(0, possibleMoves.Count);

			return possibleMoves[choose];
		}

		public static PositionScore OneLevel(Board board, Player player)
		{
			PositionScore bestPos = new PositionScore(new Vector2(-1,-1), int.MinValue);
			foreach(PositionScore pos in board.GetPossiblePositionScore(player))
			{
				if(pos.Score > bestPos.Score)
				{
					bestPos = pos;
				}
			}
			return bestPos;
		}

		public static PositionScore TwoLevel(Board board, Player player)
		{
			PositionScore bestPos = new PositionScore(new Vector2(-1,-1), float.MinValue);
			foreach(PositionScore pos in board.GetPossiblePositionScore(player))
			{
				float score = pos.Score;

				float enemyMoves = 0;
				Board newboard = board.Clone();
				newboard.Place(pos.Pos, player);
				if(newboard.CanPlayerPlay(player.Opposite()))
					// find how many moves they can make
					enemyMoves = newboard.GetPossibleMoves(player.Opposite()).Count;
				
				score -= enemyMoves;

				if(score > bestPos.Score)
				{
					bestPos = new PositionScore(pos.Pos, score);
				}
			}
			return bestPos;
		}
	}
}