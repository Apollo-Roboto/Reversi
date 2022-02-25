using System;
using System.Linq;
using System.Collections.Generic;

namespace ReversiBot
{
	public class Board
	{
		public Cell[] cells = new Cell[8*8];
		public Player Turn {get; private set;} = Player.BLACK;
		public Player Winner {get; private set;} = Player.NONE;

		public Board(Cell[] cells = null)
		{
			if(cells == null)
				InitBoard();
			else
				this.cells = cells;
		}

		private void InitBoard()
		{
			for(int i = 0; i < cells.Length; i++)
				cells[i] = new Cell();
				
			cells[27].Current = Player.WHITE;
			cells[28].Current = Player.BLACK;
			cells[35].Current = Player.BLACK;
			cells[36].Current = Player.WHITE;

		}

		public int GetScore(Player player)
		{
			int sum = 0;
			foreach(Cell cell in cells)
			{
				if(cell.Current == player)
					sum++;
			}
			return sum;
		}
		
		public void Place(int index)
		{
			cells[index].Current = Turn;

			// do the flip in all 8 directions
			int[][] directions = new int[][]{
				new int[]{1,0},new int[]{-1,0},new int[]{0,1},new int[]{0,-1},
				new int[]{1,1},new int[]{-1,1},new int[]{-1,-1},new int[]{1,-1}};
			
			foreach(int[] dir in directions)
			{
				FlipDirection(dir[0], dir[1], index);
			}

			SwitchTurn();
		}
		private void FlipDirection(int x, int y, int i)
		{
			int incrementation = x + -(y*8);
			int init = i + incrementation;
			List<int> toFlip = new List<int>();
			bool flippable = false;

			if(cells[init].Current == Player.NONE || cells[init].Current == Turn)
				return;

			for(int j = init; IsOutside(x, y, j); j+=incrementation)
			{
				// if opposite color, Flip
				if(cells[j].Current == Turn.Opposite())
					toFlip.Add(j);
					
				// if this player, stop looking and flip
				else if(cells[j].Current == Turn)
				{
					flippable = true;
					break;
				}

				else
					break;
			}

			// flip all that needs to flip
			if(flippable)
				toFlip.ForEach(x => cells[x].Flip());

		}

		private void SwitchTurn()
		{
			Turn = Turn == Player.BLACK ? Player.WHITE : Player.BLACK;
		}

		public Player Get(int index)
		{
			return cells[index].Current;
		}

		public bool IsPlayable(int index)
		{
			foreach(PositionScore positionScore in GetPossibleMoves())
			{
				if(positionScore.Index == index)
					return true;
			}
			return false;
		}

		private bool IsOutside(int x, int y, int j)
		{
			bool outsidex = false;
			bool outsidey = false;

			if(x == 1)
				outsidex = (j % 8 != 0);
			if(x == -1)
				outsidex = ((j+1) % 8 != 0);

			if(y == 1)
				outsidey = (j > 0);
			else if(y == -1)
				outsidey = (j < 64);

			return outsidex || outsidey;
		}


		private PositionScore ScoreDirection(int x, int y, int i)
		{
			float score = 0;
			int incrementation = x + -(y*8);
			int init = i + incrementation;
			
			if(cells[init].Current == Player.NONE || cells[init].Current == Turn)
				return null;

			for(int j = init; IsOutside(x, y, j); j+=incrementation)
			{
				// if same color, no possible move here
				if(cells[j].Current == Turn)
					return null;

				// if opposite color, add score, continue
				else if(cells[j].Current == Turn.Opposite())
					score += 1;

				// if spot is free, take it
				else if(cells[j].Current == Player.NONE)
					return new PositionScore(j, score);
			}

			return null;
		}
		public PositionScore[] GetPossibleMoves()
		{
			List<PositionScore> possibleMoves = new List<PositionScore>();
			int[][] directions = new int[][]{
				new int[]{1,0},new int[]{-1,0},new int[]{0,1},new int[]{0,-1},
				new int[]{1,1},new int[]{-1,1},new int[]{-1,-1},new int[]{1,-1}};

			for(int i = 0; i < cells.Length; i++)
			{
				// Only calculate those about this turn
				if(cells[i].Current != Turn)
					continue;
				
				Console.WriteLine("Checking cell " + i + " : " + cells[i].Current);
				
				foreach(int[] dir in directions)
				{
					PositionScore posScore = ScoreDirection(dir[0], dir[1], i);
					if(posScore != null)
					{
						possibleMoves.Add(posScore);
						Console.WriteLine(posScore);
					}
				}
			}
			return possibleMoves.ToArray();
		}
	}
}