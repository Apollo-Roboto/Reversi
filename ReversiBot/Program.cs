using System;
using System.Threading;

namespace ReversiBot
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			Game();
			// BotVsBot();
		}

		static void Game()
		{
			Board board = BoardPreset.Startup();
			Console.WriteLine("");

			while (!board.IsGameOver())
			{
				// Console.Clear();
				Utils.PrintBoard(board);

				switch(board.Turn)
				{
					case Player.BLACK:
						bool valid = false;
						Vector2 pos = new Vector2(-1,-1);
						while(!valid)
						{
							Console.Write("\n" + Player.BLACK + " Move: ");
							string input = Console.ReadLine().Trim();
							if(!Utils.ValidateInput(input))
								continue;
							pos = Utils.PosToCoord(input);
							if(board.IsPlayable(pos, Player.BLACK))
								valid = true;
						}
						board.Place(pos, Player.BLACK);
						break;

					case Player.WHITE:
						PositionScore nextMove = Solver.Recursion1(board, Player.WHITE, 3);
						board.Place(nextMove.Pos, Player.WHITE);
						Console.WriteLine("\nNext move: " + nextMove);
						Console.ReadLine();
						break;
				}
				board.SwitchTurn();
			}

			Console.WriteLine("=== GAME OVER ===");
			Utils.PrintBoard(board);
			Console.WriteLine("Winner is " + board.GetWinner());
		}

		static void BotVsBot()
		{
			Board board = BoardPreset.Startup();
			// Console.WriteLine("");

			while (!board.IsGameOver())
			{
				Console.Clear();
				Utils.PrintBoard(board);
				PositionScore nextMove;

				switch(board.Turn)
				{
					case Player.BLACK:
						nextMove = Solver.Recursion1(board, Player.BLACK, 3);
						board.Place(nextMove.Pos, Player.BLACK);
						Console.WriteLine("\nBLACK Next move: " + nextMove);
						break;

					case Player.WHITE:
						nextMove = Solver.Recursion1(board, Player.WHITE, 1);
						board.Place(nextMove.Pos, Player.WHITE);
						Console.WriteLine("\nWHITE Next move: " + nextMove);
						break;
				}
				Thread.Sleep(75);
				// Console.ReadLine();
				board.SwitchTurn();
			}

			Console.WriteLine("=== GAME OVER ===");
			Utils.PrintBoard(board);
			Console.WriteLine("Winner is " + board.GetWinner());
		}
	}
}
