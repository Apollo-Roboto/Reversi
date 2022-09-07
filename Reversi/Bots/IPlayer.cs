namespace Reversi
{
	public interface IPlayer
	{
		public PositionInformation NextMove(Board board, Player player);
	}
}