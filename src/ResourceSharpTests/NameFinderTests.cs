using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResourceSharp;
using Shouldly;
using System;
using System.Collections.Generic;

namespace ResourceSharpTests
{
    [TestClass]
    public class NameFinderTests
    {
        private NameFinder _nameFinder;

        [TestInitialize]
        public void Initialize()
        {
            _nameFinder = new NameFinder();
        }

        [TestMethod]
        public void Exact_match_with_only_one_item()
        {
            _nameFinder.FindMatchingName("a", new List<string> { "a" })
                .ShouldBe("a", StringComparison.InvariantCultureIgnoreCase);
        }

        [TestMethod]
        public void Prefer_exact_case_over_different_case()
        {
            _nameFinder.FindMatchingName("a", new List<string> { "A", "a" })
                .ShouldBe("a", StringComparison.InvariantCultureIgnoreCase);
        }

        [TestMethod]
        public void Prefer_spaces_over_different_case()
        {
            _nameFinder.FindMatchingName("a", new List<string> { "A", " a  " })
                .ShouldBe(" a  ", StringComparison.InvariantCultureIgnoreCase);
        }

        [TestMethod]
        public void Different_case_will_do_if_theres_no_exact_match()
        {
            _nameFinder.FindMatchingName("a", new List<string> { "b", "A" })
                .ShouldBe("A", StringComparison.InvariantCultureIgnoreCase);
        }

        [TestMethod]
        public void When_there_are_no_matches_Then_it_should_return_null()
        {
            _nameFinder.FindMatchingName("a", new List<string> { "b", "c" }).ShouldBe(null);
        }
    }
}
