using System;
using Xunit;
using Reversi;
using System.Collections.Generic;

namespace Reversi.Tests
{
	public class PositionTests
	{
		public static IEnumerable<object[]> AlgebraicNotationData()
		{
			yield return new object[] { "A1", new Position(0, 0) };
			yield return new object[] { "B1", new Position(0, 1) };
			yield return new object[] { "H1", new Position(0, 7) };
			yield return new object[] { "A2", new Position(1, 0) };
			yield return new object[] { "A8", new Position(7, 0) };
			yield return new object[] { "H8", new Position(7, 7) };
			yield return new object[] { "D4", new Position(3, 3) };
		}

		[Theory]
		[MemberData(nameof(AlgebraicNotationData))]
		public void PositionToAlgebraicNotation(string expected, Position pos)
		{
			Assert.Equal(expected, pos.AN());
		}

		[Theory]
		[MemberData(nameof(AlgebraicNotationData))]
		public void AlgebraicNotationToPosition(string an, Position expected)
		{
			Assert.Equal(expected, Position.FromAN(an));
		}
	}
}
