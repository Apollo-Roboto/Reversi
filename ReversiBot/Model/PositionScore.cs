using System;

namespace ReversiBot
{
	public class PositionScore
	{
		public int Index {get; set;}
		public float Score {get; set;}

		public PositionScore(int index, float score)
		{
			Index = index;
			Score = score;
		}

		public override string ToString()
		{
			return "Pos: " + Index + " Score: " + Score;
		}
	}
}