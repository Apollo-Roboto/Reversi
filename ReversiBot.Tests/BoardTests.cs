using System;
using Xunit;
using System.Linq;

namespace ReversiBot.Tests
{
	public class BoardTests
	{
		[Fact]
		public void Place()
		{
			Board board = new Board();
			Assert.Equal(board.Get(19), Player.NONE);
			board.Place(19);
			Assert.Equal(board.Get(19), Player.BLACK);
		}

		[Theory]
		[InlineData(27, 19)]
		[InlineData(27, 26)]
		[InlineData(36, 44)]
		[InlineData(36, 37)]
		public void PlaceMakesTheDiskFlip(int flipped, int place)
		{
			Board board = new Board();
			board.Place(place);
			Assert.Equal(Player.BLACK, board.Get(flipped));
		}

		[Fact]
		public void TurnChangesAfterPlace()
		{
			Board board = new Board();
			Assert.Equal(Player.BLACK, board.Turn);
			board.Place(19);
			Assert.Equal(Player.WHITE, board.Turn);
		}

		[Fact]
		public void GetPossibleMoves()
		{
			Board board = new Board();
			board.Place(19);
			int[] expected = {18, 20, 34};

			int[] indexes = board.GetPossibleMoves()
				.Select(x => x.Index)
				.OrderBy(x => x)
				.ToArray<int>();

			Assert.Equal(expected, indexes);
		}

		[Theory]
		[InlineData(18)]
		[InlineData(20)]
		[InlineData(34)]
		public void IsPlayable(int pos)
		{
			Board board = new Board();
			board.Place(19);
			Assert.True(board.IsPlayable(pos));
		}

		[Theory]
		[InlineData(27)]
		[InlineData(28)]
		[InlineData(35)]
		[InlineData(36)]
		[InlineData(19)]
		[InlineData(56)]
		[InlineData(63)]
		[InlineData(45)]
		public void IsNotPlayable(int pos)
		{
			Board board = new Board();
			board.Place(19);
			Assert.False(board.IsPlayable(pos));
		}


	}
}
