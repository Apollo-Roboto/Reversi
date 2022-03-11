using System;
using Xunit;
using Xunit.Abstractions;

namespace ReversiBot.Tests
{
	public class SolverTests
	{

		[Fact]
		public void OneLevelBetterThanRandom()
		{
			int blackWins = 0;
			int whiteWins = 0;

			for (int i = 0; i < 250; i++)
			{
				Board board = BoardPreset.Startup();

				while (!board.IsGameOver())
				{
					PositionScore nextMove;
					switch (board.Turn)
					{
						case Player.BLACK:
							nextMove = Solver.OneLevel(board, Player.BLACK);
							board.Place(nextMove.Pos, Player.BLACK);
							break;
						case Player.WHITE:
							nextMove = Solver.Random(board, Player.WHITE);
							board.Place(nextMove.Pos, Player.WHITE);
							break;
					}
					board.SwitchTurn();
				}
				Player winner = board.GetWinner();
				if (winner == Player.BLACK)
					blackWins++;
				else if (winner == Player.WHITE)
					whiteWins++;
			}
			
			Assert.True(blackWins > whiteWins, $"Black was expected to win. blackWins: {blackWins}, whiteWins: {whiteWins}.");
		}

		[Fact]
		public void TwoLevelBetterThanOneLevel()
		{
			int blackWins = 0;
			int whiteWins = 0;

			for (int i = 0; i < 250; i++)
			{
				Board board = BoardPreset.Startup();

				while (!board.IsGameOver())
				{
					PositionScore nextMove;
					switch (board.Turn)
					{
						case Player.BLACK:
							nextMove = Solver.TwoLevel(board, Player.BLACK);
							board.Place(nextMove.Pos, Player.BLACK);
							break;
						case Player.WHITE:
							nextMove = Solver.OneLevel(board, Player.WHITE);
							board.Place(nextMove.Pos, Player.WHITE);
							break;
					}
					board.SwitchTurn();
				}
				Player winner = board.GetWinner();
				if (winner == Player.BLACK)
					blackWins++;
				else if (winner == Player.WHITE)
					whiteWins++;
			}
			
			Assert.True(blackWins > whiteWins, $"Black was expected to win. blackWins: {blackWins}, whiteWins: {whiteWins}.");
		}

		[Fact]
		public void Recursion1BetterThanTwoLevel()
		{
			int blackWins = 0;
			int whiteWins = 0;

			for (int i = 0; i < 250; i++)
			{
				Board board = BoardPreset.Startup();

				while (!board.IsGameOver())
				{
					PositionScore nextMove;
					switch (board.Turn)
					{
						case Player.BLACK:
							nextMove = Solver.Recursion1(board, Player.BLACK, 1);
							board.Place(nextMove.Pos, Player.BLACK);
							break;
						case Player.WHITE:
							nextMove = Solver.TwoLevel(board, Player.WHITE);
							board.Place(nextMove.Pos, Player.WHITE);
							break;
					}
					board.SwitchTurn();
				}
				Player winner = board.GetWinner();
				if (winner == Player.BLACK)
					blackWins++;
				else if (winner == Player.WHITE)
					whiteWins++;
			}
			
			Assert.True(blackWins > whiteWins, $"Black was expected to win. blackWins: {blackWins}, whiteWins: {whiteWins}.");
		}
	}
}
