using System;
using System.Collections.Generic;

namespace Reversi
{
	public class HumanPlayer : IPlayer
	{
		public PositionInformation NextMove(Board board, Player player)
		{
			bool valid = false;
			Position pos = new Position(-1,-1);

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

			return new PositionInformation(pos, 0, new List<Position>());
		}
	}
}