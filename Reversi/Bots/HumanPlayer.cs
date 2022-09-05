using System;
using System.Collections.Generic;

namespace ReversiBot
{
	public class HumanPlayer : IPlayer
	{
		public PositionInformation NextMove(Board board, Player player)
		{
			bool valid = false;
			Vector2 pos = new Vector2(-1,-1);

			while(!valid)
			{
				Console.Write("\n" + player + " Move: ");
				string input = Console.ReadLine().Trim();

				if(!Utils.ValidateInput(input))
					continue;

				pos = Utils.PosToCoord(input);

				if(board.IsPlayable(pos, player))
					valid = true;
			}

			return new PositionInformation(pos, 0, new List<Vector2>());
		}
	}
}