using System;
using System.Collections.Generic;

using Reversi;
using Reversi.Bot;

namespace App
{
	public class HumanPlayer : IPlayer
	{
		public PositionInformation NextMove(Board board, Player player)
		{
			bool valid = false;
			Position pos = new Position(-1, -1);

			while (!valid)
			{
				Console.Write("\n" + player + " Move: ");
				string input = Console.ReadLine().Trim();

				if (!Utils.ValidateInput(input))
					continue;

				pos = Utils.PosToCoord(input);

				if (board.IsPlayable(pos, player))
					valid = true;
			}

			return new PositionInformation(pos, 0, new List<Position>());
		}
	}

	class Program
	{
		public static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			Play(new HardBot(), new HumanPlayer());
		}

		static void Play(IPlayer p1, IPlayer p2)
		{
			// randomly set black or white
			if (new Random().NextDouble() > 0.5)
			{
				IPlayer tmp = p1;
				p1 = p2;
				p2 = tmp;
			}

			Board board = Board.Startup();
			while (!board.IsGameOver())
			{
				Console.Clear();
				Utils.PrintBoard(board);

				PositionInformation nextMove;

				switch (board.Turn)
				{
					case Player.BLACK:
						nextMove = p1.NextMove(board, Player.BLACK);
						break;
					case Player.WHITE:
						nextMove = p2.NextMove(board, Player.WHITE);
						break;
					default:
						throw new Exception("Invalid player");
				}

				board.Place(nextMove.Pos, board.Turn);
				board.SwitchTurn();
			}

			Console.WriteLine("=== GAME OVER ===");
			Utils.PrintBoard(board);
			Console.WriteLine("Winner is " + board.GetWinner());
		}
	}
}
