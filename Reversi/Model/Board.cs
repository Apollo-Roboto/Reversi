using System;
using System.Collections.Generic;

namespace Reversi
{
	public class Board
	{
		public Cell[,] Cells { get; private set; } = new Cell[8, 8];
		public Player Turn { get; private set; }
		private Position[] directions = {
			new Position(0,1), new Position(1,1), new Position(0,-1), new Position(-1, 1),
			new Position(1,0), new Position(-1,-1), new Position(-1,0), new Position(1, -1)
		};

		public Board(Cell[,] cells = null, Player turn = Player.BLACK)
		{
			this.Turn = turn;
			this.Cells = cells;
		}

		public Board Clone()
		{
			Cell[,] clonedCells = new Cell[8, 8];
			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
					clonedCells[i, j] = (Cell)Cells[i, j].Clone();

			return new Board(
				clonedCells,
				Turn
			);
		}

		public int GetScore(Player player)
		{
			int sum = 0;

			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
					if (GetCell(i, j).Current == player)
						sum++;

			return sum;
		}

		public void Place(Position pos, Player player)
		{
			Cells[pos.X, pos.Y].Current = player;

			foreach (Position dir in directions)
			{
				FlipDirection(pos, dir, player);
			}
		}

		private void FlipDirection(Position pos, Position dir, Player player)
		{
			List<Cell> toFlip = new List<Cell>(24);
			bool flippable = false;

			for (int i = 1; i < 8; i++)
			{
				Position cpos = new Position(pos.X + i * dir.X, pos.Y + i * dir.Y);
				if (IsOutside(cpos))
					return;

				Cell cell = GetCell(cpos);

				// if opposite color, Flip
				if (cell.Current == player.Opposite())
					toFlip.Add(cell);

				// if this player, stop looking and flip
				else if (cell.Current == player)
				{
					flippable = true;
					break;
				}

				else
					break;
			}

			// flip all that needs to flip
			if (flippable)
				toFlip.ForEach(cell => cell.Flip());
		}

		public void SwitchTurn()
		{
			// don't switch turns if the other player cannot play
			if (CanPlayerPlay(Turn.Opposite()))
				Turn = Turn.Opposite();
		}

		public Player GetPlayer(Position pos)
		{
			return GetCell(pos).Current;
		}

		private bool IsOutside(Position pos)
		{
			return pos.X < 0 || pos.X >= 8 || pos.Y < 0 || pos.Y >= 8;
		}

		public Cell GetCell(Position pos)
		{
			return Cells[pos.X, pos.Y];
		}

		public Cell GetCell(int x, int y)
		{
			return Cells[x, y];
		}

		public bool IsGameOver()
		{
			return !CanPlayerPlay(Player.WHITE) && !CanPlayerPlay(Player.BLACK);
		}

		public WinningPlayer GetWinner()
		{
			if (!IsGameOver())
				return WinningPlayer.NONE;

			int blackScore = GetScore(Player.BLACK);
			int whiteScore = GetScore(Player.WHITE);

			if (blackScore > whiteScore)
				return WinningPlayer.BLACK;
			if (blackScore < whiteScore)
				return WinningPlayer.WHITE;
			if (blackScore == whiteScore)
				return WinningPlayer.TIE;

			return WinningPlayer.NONE;
		}

		public bool CanPlayerPlay(Player player)
		{
			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
					if (IsPlayable(new Position(i, j), player))
						return true;

			return false;
		}

		public bool IsPlayable(Position pos, Player player)
		{
			if (!GetCell(pos).IsFree())
				return false;

			foreach (Position dir in directions)
			{
				for (int i = 1; i < 8; i++)
				{
					Position cpos = new Position(pos.X + i * dir.X, pos.Y + i * dir.Y);
					if (IsOutside(cpos))
						break;

					Cell cell = GetCell(cpos);

					// if nothing, no moves
					if (cell.Current == Player.NONE)
						break;

					// if next cell is same color, no possible move here
					else if (i == 1 && cell.Current == player)
						break;

					// if Followed by our player, possible move
					else if (cell.Current == player)
						return true;
				}
			}
			return false;
		}

		public List<Position> GetPossibleMoves(Player player)
		{
			List<Position> possibleMoves = new List<Position>(32);
			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
				{
					Position pos = new Position(i, j);
					if (IsPlayable(pos, player))
						possibleMoves.Add(pos);
				}
			return possibleMoves;
		}

		public List<PositionInformation> GetPossiblePositionInformation(Player player)
		{
			List<PositionInformation> possibleMoves = new List<PositionInformation>();

			foreach (Position pos in GetPossibleMoves(player))
			{
				Cell cell = GetCell(pos);
				// if(cell.Current != Player.NONE)
				// 	continue;

				List<Position> flipped = new List<Position>(24);
				foreach (Position dir in directions)
				{
					flipped.AddRange(FlippedDirection(pos, dir, player));
				}
				if (flipped.Count > 0)
					possibleMoves.Add(new PositionInformation(pos, float.MinValue, flipped));
			}
			return possibleMoves;
		}

		private List<Position> FlippedDirection(Position pos, Position dir, Player player)
		{
			List<Position> flipped = new List<Position>(6);
			for (int i = 1; i < 8; i++)
			{
				Position cpos = new Position(pos.X + i * dir.X, pos.Y + i * dir.Y);
				if (IsOutside(cpos))
					return new List<Position>();

				Cell cell = GetCell(cpos);

				// if nothing on next cell, no flip
				if (cell.Current == Player.NONE)
					return new List<Position>();

				// if neighbor is our player, no flip
				if (i == 1 && cell.Current == player)
					return new List<Position>();

				// if opposite color, add to the flips
				if (cell.Current == player.Opposite())
					flipped.Add(cpos);

				// if our color and not neighbor, done with the loop
				if (i != 1 && cell.Current == player)
					break;
			}
			return flipped;
		}

		/// <summary>
		/// Creates a start board with 4 disks placed (2 black 2 white)
		/// <code>
		/// - - - - - - - - <br/>
		/// - - - - - - - - <br/>
		/// - - - - - - - - <br/>
		/// - - - W B - - - <br/>
		/// - - - B W - - - <br/>
		/// - - - - - - - - <br/>
		/// - - - - - - - - <br/>
		/// - - - - - - - - <br/>
		/// </code>
		/// </summary>

		public static Board Startup()
		{
			Cell[,] cells = new Cell[8, 8];
			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
					cells[i, j] = new Cell();

			cells[3, 3].Current = Player.WHITE;
			cells[3, 4].Current = Player.BLACK;
			cells[4, 3].Current = Player.BLACK;
			cells[4, 4].Current = Player.WHITE;
			return new Board(cells);
		}

	}
}