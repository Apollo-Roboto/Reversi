using System;
using System.Linq;

namespace ReversiBot
{
	public static class Utils
	{
		public static int PosToIndex(string pos)
		{
			if(pos.Length != 2)
				return -1;

			int horizontal = ((int)pos[0])-64;
			int vertical = (int.Parse(pos.Substring(1)));

			if(horizontal > 8 || vertical > 8)
				return -1;

			// return (horizontal-1) * (vertical-1);
			return (8*(vertical-1)) + (horizontal-1);
		}

		public static string IndexToPos(int index)
		{
			throw new NotImplementedException();
		}

		public static void PrintBoard(Board board)
		{
			string blankColor = "\x1b[38;2;100;100;100m";
			string resetColor = "\x1b[0m";
			string blank = blankColor + "•" + resetColor;
			
			Console.WriteLine();
			Console.WriteLine("       B {0,2} • {1,-2} W       ", board.GetScore(Player.BLACK), board.GetScore(Player.WHITE));
			Console.WriteLine("   ╭─────────────────╮ ");

			for(int i = 0; i < 64; i+=8)
			{
				string[] row = (
					from cell in board.cells.Skip(i).Take(8)
					select cell.Current.Short() == 'N' ? blank :  cell.Current.Short().ToString()
				).ToArray();

				Console.WriteLine(string.Format(i/8+1 + "  │ {0} {1} {2} {3} {4} {5} {6} {7} │",
					row
				));
			}

			Console.WriteLine("   ╰─────────────────╯ ");
			Console.WriteLine("     A B C D E F G H   ");
		}
	}
}