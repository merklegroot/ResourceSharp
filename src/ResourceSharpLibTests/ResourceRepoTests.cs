using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResourceSharpLib;
using System;

namespace ResourceSharpLibTests
{
    [TestClass]
    public class ResourceRepoTests
    {
        private ResourceRepo _resourceRepo;

        [TestInitialize]
        public void Initialize()
        {
            _resourceRepo = new ResourceRepo();
        }

        [TestMethod]
        public void Get_text_resource()
        {
            var text = _resourceRepo.Get("lorem-ipsum.txt", GetType().Assembly);
            Console.WriteLine(text);

            text.ShouldBe("Lorem ipsum", StringComparison.Ordinal);
        }

        [TestMethod]
        public void Get_text_resource_without_specifying_assembly()
        {
            var text = _resourceRepo.Get("lorem-ipsum.txt");
            Console.WriteLine(text);

            text.ShouldBe("Lorem ipsum", StringComparison.Ordinal);
        }

        [TestMethod]
        public void Get_non_existent_resource()
        {
            Assert.ThrowsException<ApplicationException>(() =>
            { 
                _resourceRepo.Get("argh", GetType().Assembly);
            });
        }

        private class Person
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
        }

        [TestMethod]
        public void Get_json_resource()
        {
            var person = _resourceRepo.Get<Person>("person", GetType().Assembly);

            person.LastName.ShouldBe("Jenkins", StringComparison.Ordinal);
            person.FirstName.ShouldBe("Leeroy", StringComparison.Ordinal);
        }
    }
}
