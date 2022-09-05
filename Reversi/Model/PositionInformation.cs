using System;
using System.Collections.Generic;

namespace ReversiBot
{
	public class PositionInformation
	{
		public Vector2 Pos;
		public float Score;
		public List<Vector2> Flipped;

		public PositionInformation(Vector2 pos, float score, List<Vector2> flipped)
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
			return new PositionInformation(new Vector2(-1,-1), float.MinValue, new List<Vector2>());
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