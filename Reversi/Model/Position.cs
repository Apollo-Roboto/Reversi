using System;

namespace Reversi
{
	public struct Position : IComparable
	{
		public int X { get; set; }
		public int Y { get; set; }
		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}
		
		public static Position Zero()
		{
			return new Position(0, 0);
		}

		/// <sumary>
		/// Algebraic Notation
		/// </sumary>
		public string AN()
		{
			char letter = (char)((int)'A' + Y);
			return letter + "" + (X+1);
		}

		public override string ToString() => "( " + X + "," + Y + " )";

		public override bool Equals(object obj) => obj is Position other && this.Equals(other);

		public bool Equals(Position p) => X == p.X && Y == p.Y;

		public override int GetHashCode() => (X, Y).GetHashCode();

		public int CompareTo(object obj)
		{	
			Position other = (Position)obj;
			
			int x = X.CompareTo(other.X);
			if(x == 0)
			{
				int y = Y.CompareTo(other.Y);
				return y;
			}
			else
				return x;
		}

		public static bool operator ==(Position lhs, Position rhs) => lhs.Equals(rhs);
		public static bool operator !=(Position lhs, Position rhs) => !(lhs == rhs);
		public static bool operator <(Position lhs, Position rhs) => lhs.X < rhs.X || lhs.Y < rhs.Y;
		public static bool operator >(Position lhs, Position rhs) => lhs.X > rhs.X || lhs.Y > rhs.Y;

	}
}
