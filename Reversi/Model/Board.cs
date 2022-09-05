using System;
using System.Linq;
using System.Collections.Generic;

namespace ReversiBot
{
	public class Board
	{
		public Cell[,] cells = new Cell[8, 8];
		public Player Turn { get; private set; }
		private Vector2[] directions = {
			new Vector2(0,1), new Vector2(1,1), new Vector2(0,-1), new Vector2(-1, 1),
			new Vector2(1,0), new Vector2(-1,-1), new Vector2(-1,0), new Vector2(1, -1)
		};

		public Board(Cell[,] cells = null, Player turn = Player.BLACK)
		{
			this.Turn = turn;
			this.cells = cells;
		}

		public Board Clone()
		{
			Cell[,] clonedCells = new Cell[8,8];
			for(int i = 0; i < 8; i++)
				for(int j = 0; j < 8; j++)
					clonedCells[i,j] = (Cell)cells[i,j].Clone();

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

		public void Place(Vector2 pos, Player player)
		{
			cells[pos.X, pos.Y].Current = player;

			foreach (Vector2 dir in directions)
			{
				FlipDirection(pos, dir, player);
			}
		}

		private void FlipDirection(Vector2 pos, Vector2 dir, Player player)
		{
			List<Cell> toFlip = new List<Cell>(24);
			bool flippable = false;

			for (int i = 1; i < 8; i++)
			{
				Vector2 cpos = new Vector2(pos.X + i * dir.X, pos.Y + i * dir.Y);
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
			if(CanPlayerPlay(Turn.Opposite()))
				Turn = Turn.Opposite();
		}

		public Player GetPlayer(Vector2 pos)
		{
			return GetCell(pos).Current;
		}

		private bool IsOutside(Vector2 pos)
		{
			return pos.X < 0 || pos.X >= 8 || pos.Y < 0 || pos.Y >= 8;
		}

		public Cell GetCell(Vector2 pos)
		{
			return cells[pos.X, pos.Y];
		}

		public Cell GetCell(int x, int y)
		{
			return cells[x, y];
		}

		public bool IsGameOver()
		{
			return !CanPlayerPlay(Player.WHITE) && !CanPlayerPlay(Player.BLACK);
		}

		public Player GetWinner()
		{
			if (!IsGameOver())
				return Player.NONE;

			int blackScore = GetScore(Player.BLACK);
			int whiteScore = GetScore(Player.WHITE);

			if (blackScore > whiteScore)
				return Player.BLACK;
			if (blackScore < whiteScore)
				return Player.WHITE;
			if (blackScore == whiteScore)
				return Player.TIE;

			return Player.NONE;
		}

		public bool CanPlayerPlay(Player player)
		{
			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
					if (IsPlayable(new Vector2(i, j), player))
						return true;

			return false;
		}

		public bool IsPlayable(Vector2 pos, Player player)
		{
			if (!GetCell(pos).IsFree())
				return false;

			foreach (Vector2 dir in directions)
			{
				for (int i = 1; i < 8; i++)
				{
					Vector2 cpos = new Vector2(pos.X + i * dir.X, pos.Y + i * dir.Y);
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

		public List<Vector2> GetPossibleMoves(Player player)
		{
			List<Vector2> possibleMoves = new List<Vector2>(32);
			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
				{
					Vector2 pos = new Vector2(i, j);
					if (IsPlayable(pos, player))
						possibleMoves.Add(pos);
				}
			return possibleMoves;
		}

		public List<PositionInformation> GetPossiblePositionInformation(Player player)
		{
			List<PositionInformation> possibleMoves = new List<PositionInformation>();

			foreach(Vector2 pos in GetPossibleMoves(player))
			{
				Cell cell = GetCell(pos);
				// if(cell.Current != Player.NONE)
				// 	continue;
				
				List<Vector2> flipped = new List<Vector2>(24);
				foreach (Vector2 dir in directions)
				{
					flipped.AddRange(FlippedDirection(pos, dir, player));
				}
				if(flipped.Count > 0)
					possibleMoves.Add(new PositionInformation(pos, float.MinValue, flipped));
			}
			return possibleMoves;
		}

		private List<Vector2> FlippedDirection(Vector2 pos, Vector2 dir, Player player)
		{
			List<Vector2> flipped = new List<Vector2>(6);
			for(int i = 1; i < 8; i++)
			{
				Vector2 cpos = new Vector2(pos.X + i * dir.X, pos.Y + i * dir.Y);
				if (IsOutside(cpos))
					return new List<Vector2>();

				Cell cell = GetCell(cpos);

				// if nothing on next cell, no flip
				if (cell.Current == Player.NONE)
					return new List<Vector2>();

				// if neighbor is our player, no flip
				if(i == 1 && cell.Current == player)
					return new List<Vector2>();

				// if opposite color, add to the flips
				if(cell.Current == player.Opposite())
					flipped.Add(cpos);

				// if our color and not neighbor, done with the loop
				if(i != 1 && cell.Current == player)
					break;
			}
			return flipped;
		}
	}
}