using System.Collections.Generic;
using AdventOfCode.Day2;
using Xunit;

namespace AdventOfCode.Tests.DayTwo
{
    public class Tests
    {
        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", false)]
        public void Test_Part2(string input, bool expected)
        {
            var output = new Code(new List<string> {input}).Part2();
            Assert.Equal(expected,output == 1);
        }
    }
}