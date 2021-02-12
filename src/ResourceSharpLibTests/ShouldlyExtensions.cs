using Shouldly;
using System;

namespace ResourceSharpLibTests
{
    public static class ShouldlyExtensions
    {
        public static void ShouldBe(this string text, string comparisonText, StringComparison comparator)
        {
            string.Equals(text, comparisonText, comparator).ShouldBe(true);
        }
    }
}
