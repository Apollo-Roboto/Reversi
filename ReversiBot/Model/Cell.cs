using System;

namespace ReversiBot
{
	public class Cell : ICloneable
	{
		public Player Current { get; set; }
		public int Importance { get; set; }

		public Cell(Player current = Player.NONE, int importance = 0)
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
			switch (Current)
			{
				case Player.BLACK:
					Current = Player.WHITE;
					break;
				case Player.WHITE:
					Current = Player.BLACK;
					break;
			}
		}

		public override bool Equals(object obj) => obj is Cell other && this.Equals(other);

		public bool Equals(Cell p) => Current == p.Current && Importance == p.Importance;

		public override int GetHashCode()
		{
			return HashCode.Combine(Current);
		}

		public override string ToString()
		{
			return Current.ToString();
		}

		public object Clone()
		{
			return new Cell(Current, Importance);
		}
	}
}