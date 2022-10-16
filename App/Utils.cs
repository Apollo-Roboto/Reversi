using System;
using System.Text.RegularExpressions;
using Reversi;

namespace App
{
	public static class Utils
	{
		public static void WaitForEnter()
		{
			Console.WriteLine();
			Console.Write("Press enter to continue");
			while (Console.ReadKey().Key != ConsoleKey.Enter) { }
			Console.Write(new String(' ', Console.BufferWidth));
			Console.WriteLine();
		}

		public static bool ValidateInput(string input)
		{
			return Regex.Matches(input, @"^[A-H][1-8]$").Count != 0;
		}

		public static Position PosToCoord(string pos)
		{
			int horizontal = ((int)pos[0]) - 64;
			int vertical = (int.Parse(pos.Substring(1)));

			return new Position(vertical - 1, horizontal - 1);
		}

		public static Player StringToPlayer(string player)
		{
			switch (player.ToLower())
			{
				case "black":
					return Player.BLACK;
				case "white":
					return Player.WHITE;
				default:
					return Player.NONE;
			}
		}

		public static void PrintBoard(Board board)
		{
			string blankColor = "\x1b[38;2;100;100;100m";
			string resetColor = "\x1b[0m";
			string blank = blankColor + "•" + resetColor;

			Console.Write("Possible moves: ");
			board.GetPossibleMoves(board.Turn).ForEach(x => Console.Write(x.AN() + " "));
			Console.WriteLine("\n");
			Console.WriteLine("       B {0,2} • {1,-2} W       ", board.GetScore(Player.BLACK), board.GetScore(Player.WHITE));
			Console.WriteLine("   ╭─────────────────╮ ");

			for (int i = 0; i < 8; i++)
			{
				Console.Write(i + 1 + "  │ ");
				for (int j = 0; j < 8; j++)
				{
					Cell cell = board.GetCell(i, j);
					string c = cell.Current.Short() == 'N' ? blank : cell.Current.Short().ToString();
					Console.Write(c + " ");
				}
				Console.WriteLine("│");
			}

			Console.WriteLine("   ╰─────────────────╯ ");
			Console.WriteLine("     A B C D E F G H   ");
		}
	}
}