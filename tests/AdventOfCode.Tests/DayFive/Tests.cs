using AdventOfCode.Five;
using Xunit;

namespace AdventOfCode.Tests.DayFive
{
    public class Tests
    {
        [Fact]
        public void Test_Part1()
        {
            var input = new[]
            {
                "BFFFBBFRRR",
                "FFFBBBFRRR",
                "BBFFBBFRLL"
            };

            Assert.Equal(820, new Code().Part1(input));
        }
    }
}