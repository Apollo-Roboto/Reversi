using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace ApolloRoboto.Reversi.Tests
{
	public class BoardTests
	{
		[Fact]
		public void Place()
		{
			Board board = BoardPreset.Startup();
			Position pos = new Position(2, 3);
			Assert.Equal(Player.NONE, board.GetPlayer(pos));
			board.Place(pos, Player.BLACK);
			Assert.Equal(Player.BLACK, board.GetPlayer(pos));
		}

		[Fact]
		public void PlaceMakesTheDiskFlip()
		{
			Board board = BoardPreset.Startup();
			board.Place(new Position(2, 3), Player.BLACK);
			Assert.Equal(Player.BLACK, board.GetPlayer(new Position(3, 3)));
		}

		[Fact]
		public void GetPossibleMoves()
		{
			Board board = BoardPreset.Startup();
			board.Place(new Position(2, 3), Player.BLACK);
			List<Position> expected = new List<Position>{
				new Position(2,2),
				new Position(2,4),
				new Position(4,2)
			};

			List<Position> indexes = board.GetPossibleMoves(Player.WHITE);
			Assert.Equal(expected, indexes);
		}

		[Fact]
		public void IsPlayable()
		{
			Board board = BoardPreset.Startup();
			Assert.True(board.IsPlayable(new Position(5, 4), Player.BLACK));
		}

		[Fact]
		public void IsNotPlayable()
		{
			Board board = BoardPreset.Startup();
			Assert.False(board.IsPlayable(new Position(5, 4), Player.WHITE));
		}

		[Fact]
		public void CanPlayerPlay()
		{
			Board board = BoardPreset.Startup();
			Assert.True(board.CanPlayerPlay(Player.BLACK));
			Assert.True(board.CanPlayerPlay(Player.WHITE));
		}

		[Fact]
		public void CanPlayerNotPlay()
		{
			Board board = BoardPreset.WhiteCantPlay();
			Assert.False(board.CanPlayerPlay(Player.WHITE));
		}

		[Fact]
		public void CanPlayerNoMoreMoves()
		{
			Board board = BoardPreset.GameOver();
			Assert.False(board.CanPlayerPlay(Player.WHITE));
			Assert.False(board.CanPlayerPlay(Player.BLACK));
		}

		[Fact]
		public void IsGameOver()
		{
			Board board = BoardPreset.GameOver();
			Assert.True(board.IsGameOver());
		}

		[Fact]
		public void IsNotGameOver()
		{
			Board board = BoardPreset.Startup();
			Assert.False(board.IsGameOver());
		}

		[Fact]
		public void GetWinner()
		{
			Board board = BoardPreset.GameOver();
			Assert.Equal(WinningPlayer.WHITE, board.GetWinner());
		}

		[Fact]
		public void GetWinnerTie()
		{
			Board board = BoardPreset.Tie();
			Assert.Equal(WinningPlayer.TIE, board.GetWinner());
		}

		[Fact]
		public void GetPossiblePositionInformation()
		{
			Board board = BoardPreset.Startup();
			List<PositionInformation> positionInformations = board.GetPossiblePositionInformation(Player.BLACK);

			List<PositionInformation> expected = new List<PositionInformation>{
				new PositionInformation(new Position(2, 3), float.MinValue, new List<Position>{new Position(3, 3)}),
				new PositionInformation(new Position(3, 2), float.MinValue, new List<Position>{new Position(3, 3)}),
				new PositionInformation(new Position(4, 5), float.MinValue, new List<Position>{new Position(4, 4)}),
				new PositionInformation(new Position(5, 4), float.MinValue, new List<Position>{new Position(4, 4)}),
			};

			positionInformations = positionInformations.OrderBy(x => x.Pos).ToList();
			expected = expected.OrderBy(x => x.Pos).ToList();

			Assert.Equal(expected, positionInformations);
		}
	}
}
