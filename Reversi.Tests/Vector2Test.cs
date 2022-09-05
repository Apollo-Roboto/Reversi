using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace ReversiBot.Tests
{
	public class Vector2Tests
	{
		public static IEnumerable<object[]> AlgebraicNotationData()
		{
			yield return new object[] { "A1", new Vector2(0, 0) };
			yield return new object[] { "B1", new Vector2(0, 1) };
			yield return new object[] { "H1", new Vector2(0, 7) };
			yield return new object[] { "A2", new Vector2(1, 0) };
			yield return new object[] { "A8", new Vector2(7, 0) };
			yield return new object[] { "H8", new Vector2(7, 7) };
			yield return new object[] { "D4", new Vector2(3, 3) };
		}

		[Theory]
		[MemberData(nameof(AlgebraicNotationData))]
		public void AlgebraicNotation(string expected, Vector2 pos)
		{
			Assert.Equal(expected, pos.AN());
		}
	}
}
