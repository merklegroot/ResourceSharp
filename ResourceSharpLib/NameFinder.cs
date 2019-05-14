using System;
using System.Collections.Generic;
using System.Linq;

namespace ResourceSharpLib
{
    internal class NameFinder
    {
        /// <summary>
        /// Uses a hierarchy to find the best matching string.
        /// </summary>
        /// <param name="desired">The text that we're looking for</param>
        /// <param name="candidates">The candidate items to check</param>
        /// <returns>The best match</returns>
        public string FindMatchingName(string desired, IEnumerable<string> candidates)
        {
            if (string.IsNullOrWhiteSpace(desired)) { throw new ArgumentNullException(nameof(desired)); }
            if (candidates == null) { return null; }

            var trimmedDesired = desired.Trim();
            var trimmedUpperDesired = trimmedDesired.ToUpper();

            string trimmedMatch = null;
            string caseInsensitiveMatch = null;
            string caseInsensitiveTrimmedMatch = null;
            string containsMatch = null;

            foreach (var candidate in candidates)
            {
                if (candidate == null) { continue; }

                // An exact match always wins.
                if (string.Equals(desired, candidate, StringComparison.Ordinal)) { return candidate; }
                var trimmedCandidate = candidate.Trim();

                if (trimmedMatch == null && string.Equals(trimmedDesired, trimmedCandidate, StringComparison.Ordinal))
                { trimmedMatch = candidate; }

                if (caseInsensitiveMatch == null && string.Equals(desired, candidate, StringComparison.InvariantCultureIgnoreCase))
                { caseInsensitiveMatch = candidate; }

                if (caseInsensitiveTrimmedMatch == null && string.Equals(trimmedDesired, trimmedCandidate, StringComparison.InvariantCultureIgnoreCase))
                { caseInsensitiveMatch = candidate; }

                var trimmedUpperCandidate = trimmedCandidate.ToUpper();
                if (containsMatch == null && trimmedUpperCandidate.Contains(trimmedUpperDesired))
                { containsMatch = candidate; }
            }

            // Return the best of the matches based on the hierarchy.
            return new List<string> { trimmedMatch, caseInsensitiveMatch, caseInsensitiveTrimmedMatch, containsMatch }
                .FirstOrDefault(queryItem => queryItem != null);
        }
    }
}
