using ResourceSharp;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace ResourceSharpTests
{
    public class NameFinderTests
    {

        [Fact]
        public void Exact_match_with_only_one_item() =>
            new NameFinder().FindMatchingName("a", new List<string> { "a" })
                .ShouldBe("a", StringComparison.InvariantCultureIgnoreCase);

        [Fact]
        public void Prefer_exact_case_over_different_case() =>
            new NameFinder().FindMatchingName("a", new List<string> { "A", "a" })
                .ShouldBe("a", StringComparison.InvariantCultureIgnoreCase);

        [Fact]
        public void Prefer_spaces_over_different_case() =>
            new NameFinder().FindMatchingName("a", new List<string> { "A", " a  " })
                .ShouldBe(" a  ", StringComparison.InvariantCultureIgnoreCase);

        [Fact]
        public void Different_case_will_do_if_theres_no_exact_match() =>
            new NameFinder().FindMatchingName("a", new List<string> { "b", "A" })
                .ShouldBe("A", StringComparison.InvariantCultureIgnoreCase);

        [Fact]
        public void When_there_are_no_matches_Then_it_should_return_null() =>
            new NameFinder().FindMatchingName("a", new List<string> { "b", "c" })
                .ShouldBe(null);
    }
}
