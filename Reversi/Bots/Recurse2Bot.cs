using System;
using System.Collections.Generic;

namespace Reversi
{
	public class Recurse2Bot : IPlayer
	{
		private Random random = new Random();
		private Config config = Config.Load();

		public PositionInformation NextMove(Board board, Player player)
		{
			return Recurse(board, player, config.Depth);
		}
		
		private PositionInformation Recurse(Board board, Player player, int depth = 0, int level = 0)
		{
			PositionInformation bestPos = PositionInformation.Empty();

			foreach (PositionInformation pos in board.GetPossiblePositionInformation(player))
			{
				List<Position> diskFlipped = pos.Flipped;
				int enemyMoves = 0;
				float enemyScore = 0;
				bool enemyCannotPlay = false;
				bool isGameOver = false;

				Board newboard = board.Clone();
				newboard.Place(pos.Pos, player);

				if (newboard.CanPlayerPlay(player.Opposite()))
				{
					enemyMoves = newboard.GetPossibleMoves(player.Opposite()).Count;

					if (level < depth)
					{
						PositionInformation enemyPos = Recurse(newboard, player.Opposite(), depth, level + 1);
						enemyScore = enemyPos.Score;
					}
				}
				else
				{
					// if enemy cannot move or is game over, grant higher score
					enemyCannotPlay = true;

					if (newboard.IsGameOver())
						isGameOver = true;
				}

				float score = ScoreResolver(
					pos.Pos,
					diskFlipped,
					enemyMoves,
					enemyScore,
					enemyCannotPlay,
					isGameOver,
					board.GetScore(player),
					board.GetScore(player.Opposite()),
					level
				);

				if (score > bestPos.Score)
				{
					bestPos = new PositionInformation(pos.Pos, score, pos.Flipped);
				}
			}
			return bestPos;
		}

		/// <summary>
		/// Calculate the score from multiple factors from the configuration file
		/// </summary>
		private float ScoreResolver(
			Position pos,
			List<Position> diskFlipped,
			int enemyMoves,
			float enemyScore,
			bool enemyCannotPlay,
			bool isGameOver,
			int CurrentDisk,
			int EnemyDisk,
			int level)
		{
			float score = 0;

			foreach (Position diskToFlip in diskFlipped)
			{
				score += config.GetCellImportance(diskToFlip) * config.CellImportanceMultiplier;
			}

			score += config.GetCellImportance(pos) * config.CellImportanceMultiplier;
			score += enemyMoves * config.EnemyPossibleMoveScore;
			score += diskFlipped.Count * config.FlipAmountMultiplier;

			if (isGameOver)
				score += config.GameOverScore;
			else if (enemyCannotPlay)
				score += config.EnemyCannotPlayScore;

			score += enemyScore *= config.EnemyScoreMultiplier;

			score += CurrentDisk * config.CurrentPiecesScore;

			score += EnemyDisk * config.EnemyPiecesScore;

			score += (float)random.NextDouble();

			if (level == 0) Console.WriteLine($"Calculated score for position {pos.AN()} : {score}");

			return score;
		}
	}
}