using System;

namespace ReversiBot
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			Cell[] cells = new Cell[8*8];

			for(int i = 0; i < cells.Length; i++)
				cells[i] = new Cell();

			cells[11].Current = Player.WHITE;
			cells[19].Current = Player.WHITE;
			cells[26].Current = Player.BLACK;
			cells[27].Current = Player.BLACK;
			cells[28].Current = Player.BLACK;
			cells[33].Current = Player.WHITE;
			cells[34].Current = Player.WHITE;
			cells[35].Current = Player.BLACK;
			cells[36].Current = Player.BLACK;
			cells[41].Current = Player.BLACK;
			cells[44].Current = Player.BLACK;

			Board board = new Board(cells);

			while(true)
			{
				Utils.PrintBoard(board);
				Console.Write("\n" + board.Turn + " Move: ");
				string input = Console.ReadLine();
				board.Place(Utils.PosToIndex(input));

				Console.WriteLine("     -----");
				board.GetPossibleMoves();
				Console.WriteLine("     -----");
			}
		}
	}
}
