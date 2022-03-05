using System;
using System.Collections.Generic;

namespace ReversiBot
{
	public class Solver
	{
		// public static PositionScore Solve(Board board, Player player, int level, int depth)
		// {
		// 	if(level >= depth)
		// 		return new PositionScore();

		// 	List<PositionScore> positions = new List<PositionScore>();

		// 	foreach(PositionScore positionScore in board.GetPossiblePositionScore(player))
		// 	{
		// 		float score = 0;
		// 		Board newBoard = board.Clone();
		// 		newBoard.Place(positionScore.Pos, player);
		// 		PositionScore asd = Solve(newBoard, player, level+1, depth);

		// 		positions.Add(asd);
		// 	}

		// 	PositionScore top = null;
			
		// 	return new PositionScore();
		// }

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
	}
}