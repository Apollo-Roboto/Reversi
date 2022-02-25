using System;
using Xunit;

namespace ReversiBot.Tests
{
	public class UtilsTests
	{
		[Theory]
		[InlineData(0, "A1")]
		[InlineData(7, "H1")]
		[InlineData(8, "A2")]
		[InlineData(56, "A8")]
		[InlineData(63, "H8")]
		public void PosToIndex(int expected, string s)
		{
			Assert.Equal(expected, Utils.PosToIndex(s));
		}

		[Theory]
		[InlineData("A1", 0)]
		[InlineData("H1", 7)]
		[InlineData("A8", 56)]
		[InlineData("H8", 63)]
		public void IndexToPos(string expected, int index)
		{
			Assert.Equal(expected, Utils.IndexToPos(index));
		}
	}
}
