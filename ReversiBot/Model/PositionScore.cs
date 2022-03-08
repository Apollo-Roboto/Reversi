using System;

namespace ReversiBot
{
	public class PositionScore
	{
		public Vector2 Pos { get; set; }
		public float Score { get; set; }

		public PositionScore(Vector2 pos, float score)
		{
			Pos = pos;
			Score = score;
		}

		public override string ToString()
		{
			return "Pos: " + Pos.AN() + " Score: " + Score;
		}
	}
}