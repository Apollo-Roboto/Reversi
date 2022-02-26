using System;

namespace ReversiBot
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			Cell[,] cells = new Cell[8,8];

			for(int i = 0; i < 8; i++)
				for(int j = 0; j < 8; j++)
					cells[i,j] = new Cell();

			cells[3,1].Current = Player.WHITE;
			cells[3,2].Current = Player.BLACK;
			cells[3,3].Current = Player.BLACK;
			cells[4,4].Current = Player.BLACK;
			cells[5,4].Current = Player.BLACK;
			cells[6,4].Current = Player.WHITE;


			Board board = new Board(cells);

			while(true)
			{
				Utils.PrintBoard(board);
				Console.Write("\n" + board.Turn + " Move: ");
				string input = Console.ReadLine();
				board.Place(Utils.PosToCoord(input));

				Console.WriteLine("     -----");
				board.GetPossibleMoves();
				Console.WriteLine("     -----");
			}
		}
	}
}
