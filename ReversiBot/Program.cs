using System;

namespace ReversiBot
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			Board board = BoardPreset.Startup();

			while (!board.IsGameOver())
			{
				Console.Clear();
				Utils.PrintBoard(board);
				Console.Write("\n" + board.Turn + " Move: ");
				string input = Console.ReadLine();
				board.Place(Utils.PosToCoord(input), board.Turn);
				board.SwitchTurn();
			}

			Console.WriteLine("=== GAME OVER ===");
			Console.WriteLine("Winner is " + board.GetWinner());
		}
	}
}
