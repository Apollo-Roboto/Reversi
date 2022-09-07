using System;
using Xunit;

namespace Reversi.Tests
{
	public class CellTests
	{
		[Fact]
		public void IsFreeOnInit()
		{
			Cell cell = new Cell();
			Assert.True(cell.IsFree());
		}

		[Theory]
		[InlineData(Player.WHITE)]
		[InlineData(Player.BLACK)]
		public void IsNotFree(Player player)
		{
			Cell cell = new Cell();
			cell.Current = player;
			Assert.False(cell.IsFree());
		}

		[Theory]
		[InlineData(Player.WHITE, Player.BLACK)]
		[InlineData(Player.BLACK, Player.WHITE)]
		public void Flip(Player expected, Player c)
		{
			Cell cell = new Cell();
			cell.Current = c;
			cell.Flip();
			Assert.Equal(expected, cell.Current);
		}

		[Theory, InlineData(Player.NONE), InlineData(Player.WHITE), InlineData(Player.BLACK)]
		public void Equal(Player c)
		{
			Cell cell1 = new Cell(c);
			Cell cell2 = new Cell(c);
			Assert.True(cell1.Equals(cell2));
		}

		[Fact]
		public void NotEqual()
		{
			Cell cell1 = new Cell(Player.WHITE);
			Cell cell2 = new Cell(Player.BLACK);
			Assert.False(cell1.Equals(cell2));
		}
	}
}
