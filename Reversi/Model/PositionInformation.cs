using System;
using System.Collections.Generic;

namespace Reversi
{
	public class PositionInformation
	{
		public Position Pos;
		public float Score;
		public List<Position> Flipped;

		public PositionInformation(Position pos, float score, List<Position> flipped)
		{
			Pos = pos;
			Score = score;
			Flipped = flipped;
		}

		public override string ToString()
		{
			return $"Pos: {Pos.AN()}, Flipped: {Flipped.Count}, Score: {Score}";
		}

		public static PositionInformation Empty()
		{
			return new PositionInformation(new Position(-1, -1), float.MinValue, new List<Position>());
		}

		public override bool Equals(object obj)
		{
			return obj is PositionInformation && this.Equals(obj as PositionInformation);
		}

		public bool Equals(PositionInformation obj) => this.Pos == obj.Pos;

		public override int GetHashCode()
		{
			return HashCode.Combine(Pos, Score, Flipped);
		}
	}
}