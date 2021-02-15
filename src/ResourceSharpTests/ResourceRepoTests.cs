using ResourceSharp;
using Shouldly;
using System;
using Xunit;

namespace ResourceSharpTests
{
    public class ResourceRepoTests
    {

        [Fact]
        public void Get_text_resource() =>
            new ResourceRepo().Get("lorem-ipsum.txt", GetType().Assembly)
                .ShouldBe("Lorem ipsum", StringComparison.Ordinal);

        [Fact]
        public void Get_text_resource_without_specifying_assembly() =>
            new ResourceRepo().Get("lorem-ipsum.txt")
                .ShouldBe("Lorem ipsum", StringComparison.Ordinal);

        [Fact]
        public void Get_non_existent_resource() =>
            Should.Throw<ApplicationException>(() => 
                new ResourceRepo().Get("argh", GetType().Assembly));

        private class Person
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
        }

        [Fact]
        public void Get_json_resource()
        {
            var person = new ResourceRepo().Get<Person>("person", GetType().Assembly);

            person.LastName.ShouldBe("Jenkins", StringComparison.Ordinal);
            person.FirstName.ShouldBe("Leeroy", StringComparison.Ordinal);
        }
    }
}
