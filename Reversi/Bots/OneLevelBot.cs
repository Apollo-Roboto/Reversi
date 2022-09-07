namespace Reversi
{
	public class OneLevelBot : IPlayer
	{
		public PositionInformation NextMove(Board board, Player player)
		{
			PositionInformation bestPos = PositionInformation.Empty();
			foreach (PositionInformation pos in board.GetPossiblePositionInformation(player))
			{
				if (pos.Flipped.Count > bestPos.Flipped.Count)
					bestPos = pos;
			}
			return bestPos;
		}
	}
}