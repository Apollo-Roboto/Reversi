using System;

namespace ReversiBot
{
	public class Cell
	{
		public Player Current {get;set;}
		public int Importance {get;set;}

		public Cell(Player current=Player.NONE, int importance=0)
		{
			this.Current = current;
			this.Importance = importance;
		}
		
		public bool IsFree()
		{
			return Current == Player.NONE;
		}

		public void Flip()
		{
			switch(Current)
			{
				case Player.BLACK:
					Current = Player.WHITE;
					break;
				case Player.WHITE:
					Current = Player.BLACK;
					break;
			}
		}

		public override bool Equals(Object obj)
		{
			return Equals(obj as Cell);
		}

		public bool Equals(Cell other)
		{
			return other != null &&
				Current == other.Current;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Current);
		}

		public override string ToString()
		{
			return Current.ToString();
		}
	}
}