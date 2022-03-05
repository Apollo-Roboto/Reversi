using System;

namespace ReversiBot
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			Game();
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
						Console.Write("\n" + Player.BLACK + " Move: ");
						string input = Console.ReadLine();
						board.Place(Utils.PosToCoord(input), Player.BLACK);
						break;

					case Player.WHITE:
						PositionScore nextMove = Solver.OneLevel(board, Player.WHITE);
						board.Place(nextMove.Pos, Player.WHITE);
						Console.WriteLine("nextMove: " + nextMove);
						Console.ReadLine();
						break;
				}
				board.SwitchTurn();
			}

			Console.WriteLine("=== GAME OVER ===");
			Utils.PrintBoard(board);
			Console.WriteLine("Winner is " + board.GetWinner());
		}
	}
}
