using Shouldly;
using System;

namespace ResourceSharpTests
{
    public static class ShouldlyExtensions
    {
        public static void ShouldBe(this string text, string comparisonText, StringComparison comparator)
        {
            string.Equals(text, comparisonText, comparator).ShouldBe(true);
        }
    }
}
