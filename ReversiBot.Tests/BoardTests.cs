using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace ReversiBot.Tests
{
	public class BoardTests
	{
		[Fact]
		public void Place()
		{
			Board board = BoardPreset.Startup();
			Vector2 pos = new Vector2(2, 3);
			Assert.Equal(Player.NONE, board.GetPlayer(pos));
			board.Place(pos, Player.BLACK);
			Assert.Equal(Player.BLACK, board.GetPlayer(pos));
		}

		[Fact]
		public void PlaceMakesTheDiskFlip()
		{
			Board board = BoardPreset.Startup();
			board.Place(new Vector2(2, 3), Player.BLACK);
			Assert.Equal(Player.BLACK, board.GetPlayer(new Vector2(3, 3)));
		}

		[Fact]
		public void GetPossibleMoves()
		{
			Board board = BoardPreset.Startup();
			board.Place(new Vector2(2, 3), Player.BLACK);
			List<Vector2> expected = new List<Vector2>{
				new Vector2(2,2),
				new Vector2(2,4),
				new Vector2(4,2)
			};

			List<Vector2> indexes = board.GetPossibleMoves(Player.WHITE);
			Assert.Equal(expected, indexes);
		}

		[Fact]
		public void IsPlayable()
		{
			Board board = BoardPreset.Startup();
			Assert.True(board.IsPlayable(new Vector2(5, 4), Player.BLACK));
		}

		[Fact]
		public void IsNotPlayable()
		{
			Board board = BoardPreset.Startup();
			Assert.False(board.IsPlayable(new Vector2(5, 4), Player.WHITE));
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
			Assert.Equal(Player.WHITE, board.GetWinner());
		}

		[Fact]
		public void GetWinnerTie()
		{
			Board board = BoardPreset.Tie();
			Assert.Equal(Player.TIE, board.GetWinner());
		}
	}
}
