using System;
using System.Linq;

namespace Reversi
{
	public static class BoardPreset
	{
		public static Board Startup()                         // - - - - - - - -
		{                                                     // - - - - - - - -
			Cell[,] cells = new Cell[8, 8];                   // - - - - - - - -
			for (int i = 0; i < 8; i++)                       // - - - W B - - -
				for (int j = 0; j < 8; j++)                   // - - - B W - - -
					cells[i, j] = new Cell();                 // - - - - - - - -
															  // - - - - - - - -
			cells[3, 3].Current = Player.WHITE;               // - - - - - - - -
			cells[3, 4].Current = Player.BLACK;
			cells[4, 3].Current = Player.BLACK;
			cells[4, 4].Current = Player.WHITE;
			return new Board(cells);
		}

		public static Board WhiteCantPlay()                   // - - - - - - - -
		{                                                     // - - - - - - - B
			Cell[,] cells = new Cell[8, 8];                   // - - - - W W W B
			for (int i = 0; i < 8; i++)                       // - - - - - - - -
				for (int j = 0; j < 8; j++)                   // - - - - - - - -
					cells[i, j] = new Cell();                 // - - - - - - - -
															  // - - - - - - - -
			cells[2, 4].Current = Player.WHITE;               // - - - - - - - -
			cells[2, 5].Current = Player.WHITE;
			cells[2, 6].Current = Player.WHITE;
			cells[1, 7].Current = Player.BLACK;
			cells[2, 7].Current = Player.BLACK;
			return new Board(cells);
		}

		public static Board Tie()                             // W W W W B B B B
		{                                                     // W W W W B B B B
			Cell[,] cells = new Cell[8, 8];                   // W W W W B B B B
			for (int i = 0; i < 4; i++)                       // W W W W B B B B
				for (int j = 0; j < 8; j++)                   // W W W W B B B B
					cells[i, j] = new Cell(Player.WHITE);     // W W W W B B B B
															  // W W W W B B B B
			for (int i = 4; i < 8; i++)                       // W W W W B B B B
				for (int j = 0; j < 8; j++)
					cells[i, j] = new Cell(Player.BLACK);

			return new Board(cells);
		}

		public static Board GameOver()                        // - - - - W - - -
		{                                                     // - - - - W W - -
			Cell[,] cells = new Cell[8, 8];                   // W W W W W W W B
			for (int i = 0; i < 8; i++)                       // - - W W W W - B
				for (int j = 0; j < 8; j++)                   // - - W W W - - B
					cells[i, j] = new Cell();                 // - - - - - - - -
															  // - - - - - - - -
			cells[0, 4].Current = Player.WHITE;               // - - - - - - - -
			cells[1, 4].Current = Player.WHITE;
			cells[1, 5].Current = Player.WHITE;
			cells[2, 0].Current = Player.WHITE;
			cells[2, 1].Current = Player.WHITE;
			cells[2, 2].Current = Player.WHITE;
			cells[2, 3].Current = Player.WHITE;
			cells[2, 4].Current = Player.WHITE;
			cells[2, 5].Current = Player.WHITE;
			cells[2, 6].Current = Player.WHITE;
			cells[2, 7].Current = Player.BLACK;
			cells[3, 2].Current = Player.WHITE;
			cells[3, 3].Current = Player.WHITE;
			cells[3, 4].Current = Player.WHITE;
			cells[3, 5].Current = Player.WHITE;
			cells[3, 7].Current = Player.BLACK;
			cells[4, 2].Current = Player.WHITE;
			cells[4, 3].Current = Player.WHITE;
			cells[4, 4].Current = Player.WHITE;
			cells[4, 7].Current = Player.BLACK;
			return new Board(cells, Player.WHITE);
		}

		public static Board TwoCombine()                      // - - - - - - - -
		{                                                     // - - - - - B - -
			Cell[,] cells = new Cell[8, 8];                   // - - W W W B - -
			for (int i = 0; i < 8; i++)                       // - - B B B B - -
				for (int j = 0; j < 8; j++)                   // - - - B W - - -
					cells[i, j] = new Cell();                 // - - - - - - - -
															  // - - - - - - - -
			cells[1, 5].Current = Player.BLACK;               // - - - - - - - -
			cells[2, 2].Current = Player.WHITE;
			cells[2, 3].Current = Player.WHITE;
			cells[2, 4].Current = Player.WHITE;
			cells[2, 5].Current = Player.BLACK;
			cells[3, 2].Current = Player.BLACK;
			cells[3, 3].Current = Player.BLACK;
			cells[3, 4].Current = Player.BLACK;
			cells[3, 5].Current = Player.BLACK;
			cells[4, 3].Current = Player.BLACK;
			cells[4, 4].Current = Player.WHITE;
			return new Board(cells);
		}
	}
}