namespace ReversiBot
{
	public interface IBot
	{
		public PositionInformation NextMove(Board board, Player player);
	}
}