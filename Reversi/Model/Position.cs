using System;

namespace ApolloRoboto.Reversi
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
		/// Algebraic Notation of this position
		/// </sumary>
		public string AN()
		{
			char letter = (char)((int)'A' + Y);
			return letter + "" + (X + 1);
		}

		/// <summary>
		/// Create a position from an algebraic notation
		/// </summary>
		public static Position FromAN(string an)
		{
			int horizontal = ((int)an[0]) - 64;
			int vertical = (int.Parse(an.Substring(1)));

			return new Position(vertical - 1, horizontal - 1);
		}


		public override string ToString() => "( " + X + "," + Y + " )";

		public override bool Equals(object obj) => obj is Position other && this.Equals(other);

		public bool Equals(Position p) => X == p.X && Y == p.Y;

		public override int GetHashCode() => (X, Y).GetHashCode();

		public int CompareTo(object obj)
		{
			Position other = (Position)obj;

			int x = X.CompareTo(other.X);
			if (x == 0)
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
