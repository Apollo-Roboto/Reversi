using System;
using System.Linq;
using System.Collections.Generic;

namespace ReversiBot
{
	public class Board
	{
		public Cell[,] cells = new Cell[8,8];
		public Player Turn {get; private set;} = Player.BLACK;
		public Player Winner {get; private set;} = Player.NONE;
		private Vector2[] directions = {
			new Vector2(0,1), new Vector2(1,1), new Vector2(0,-1), new Vector2(-1, 1),
			new Vector2(1,0), new Vector2(-1,-1), new Vector2(-1,0), new Vector2(1, -1)
		};

		public Board(Cell[,] cells = null)
		{
			if(cells == null)
				InitBoard();
			else
				this.cells = cells;
		}

		private void InitBoard()
		{
			for(int i = 0; i < 8; i++)
				for(int j = 0; j < 8; j++)
					cells[i,j] = new Cell();
				
			cells[3,3].Current = Player.WHITE;
			cells[3,4].Current = Player.BLACK;
			cells[4,3].Current = Player.BLACK;
			cells[4,4].Current = Player.WHITE;
		}

		public int GetScore(Player player)
		{
			int sum = 0;
			
			for(int i = 0; i < 8; i++)
				for(int j = 0; j < 8; j++)
					if(GetCell(i,j).Current == player)
						sum++;

			return sum;
		}
		
		public void Place(Vector2 pos)
		{
			cells[pos.X,pos.Y].Current = Turn;

			foreach(Vector2 dir in directions)
			{
				FlipDirection(pos, dir);
			}

			SwitchTurn();
		}
		private void FlipDirection(Vector2 pos, Vector2 dir)
		{
			List<Cell> toFlip = new List<Cell>();
			bool flippable = false;

			for(int i = 1; i < 8; i++)
			{
				Vector2 cpos = new Vector2(pos.X + i*dir.X, pos.Y + i*dir.Y);
				if(IsOutside(cpos))
					return;

				Cell cell = GetCell(cpos);

				// if opposite color, Flip
				if(cell.Current == Turn.Opposite())
					toFlip.Add(cell);

				// if this player, stop looking and flip
				else if(cell.Current == Turn)
				{
					flippable = true;
					break;
				}

				else
					break;
			}

			// flip all that needs to flip
			if(flippable)
				toFlip.ForEach(cell => cell.Flip());
		}

		private void SwitchTurn()
		{
			Turn = Turn.Opposite();
		}

		public Player GetPlayer(Vector2 pos)
		{
			return GetCell(pos).Current;
		}

		public bool IsPlayable(Vector2 pos)
		{
			foreach(PositionScore positionScore in GetPossibleMoves())
			{
				if(positionScore.Pos.X == pos.X && positionScore.Pos.Y == pos.Y)
					return true;
			}
			return false;
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

		private PositionScore ScoreDirection(Vector2 pos, Vector2 dir)
		{
			float score = 0;

			for(int i = 1; i < 8; i++)
			{
				Vector2 cpos = new Vector2(pos.X + i*dir.X, pos.Y + i*dir.Y);
				if(IsOutside(cpos))
					return null;

				Cell cell = GetCell(cpos);

				// if nothing on next cell, skip
				if(i == 1 && cell.Current == Player.NONE)
					return null;
				
				// if same color, no possible move here
				if(cell.Current == Turn)
					return null;

				// if opposite color, add score, continue
				else if(cell.Current == Turn.Opposite())
					score += 1;
				
				// if spot is free, take it
				else if(cell.Current == Player.NONE)
					return new PositionScore(cpos, score);
			}
			return null;
		}

		public PositionScore[] GetPossibleMoves()
		{
			List<PositionScore> possibleMoves = new List<PositionScore>();

			for(int i = 0; i < 8; i++)
			{
				for(int j = 0; j < 8; j++)
				{
					Vector2 pos = new Vector2(i, j);
					Cell cell = GetCell(pos);

					// Only calculate those about this turn
					if(cell.Current != Turn)
						continue;
					
					foreach(Vector2 dir in directions)
					{
						PositionScore posScore = ScoreDirection(pos, dir);
						if(posScore != null)
						{
							possibleMoves.Add(posScore);
						}
					}
				}
			}

			// combine scores at same positions
			for(int i = 0; i < possibleMoves.Count; i++)
			{
				for(int j = 0; j < possibleMoves.Count; j++)
				{
					if(j == i) continue;
					if(possibleMoves[i].Pos == possibleMoves[j].Pos)
					{
						float score = possibleMoves[i].Score + possibleMoves[j].Score;
						PositionScore newPos = new PositionScore(possibleMoves[i].Pos, score);

						possibleMoves[i] = newPos;
						possibleMoves.RemoveAt(j);
					}
				}
			}

			return possibleMoves.ToArray();
		}
	}
}