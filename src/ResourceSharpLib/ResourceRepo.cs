using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ResourceSharpLib
{
    public class ResourceRepo : IResourceRepo
    {
        private readonly NameFinder _nameFinder;

        public ResourceRepo()
        {
            _nameFinder = new NameFinder();
        }

        public string Get(string name)
        {
            return Get(name, Assembly.GetCallingAssembly());
        }

        public string Get(string name, Assembly assembly)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentNullException(nameof(name)); }
            if (assembly == null) { throw new ArgumentNullException(nameof(assembly)); }

            var manifestResourceNames = assembly.GetManifestResourceNames().ToList();
            var matchingName = _nameFinder.FindMatchingName(name, manifestResourceNames);
            if (matchingName == null) { throw new ApplicationException($"Failed to find an embedded resource named \"{name}\" in assembly \"{assembly.FullName}\"."); }

            using (var stream = assembly.GetManifestResourceStream(matchingName))
            {
                if (stream == null) { throw new ApplicationException($"Failed to get resource stream for {matchingName}."); }
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public T Get<T>(string name)
        {
            return Get<T>(name, Assembly.GetCallingAssembly());
        }

        public T Get<T>(string name, Assembly assembly)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentNullException(nameof(name)); }
            if (assembly == null) { throw new ArgumentNullException(nameof(assembly)); }

            var contents = Get(name, assembly);
            return !string.IsNullOrWhiteSpace(contents)
                ? JsonConvert.DeserializeObject<T>(contents)
                : default(T);
        }

        private string FindMatchingResourceName(string name, Assembly assembly)
        {
            //var manifestResourceNames = assembly.GetManifestResourceNames().ToList();
            //foreach (var queryName in manifestResourceNames)
            //{
            //    if (queryName == null) { continue; }
            //}

            //var exactMatch = manifestResourceNames.SingleOrDefault(queryName => string.Equals(queryName, trimmedName));

            //var effectiveName = name.Trim().ToUpperInvariant();
            //var matchingName = manifestResourceNames.Single(queryName => queryName.ToUpperInvariant().EndsWith(effectiveName));

            //return matchingName;

            throw new NotImplementedException();
        }
    }
}
