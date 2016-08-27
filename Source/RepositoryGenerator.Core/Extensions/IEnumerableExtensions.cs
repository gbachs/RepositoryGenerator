using System;
using System.Collections.Generic;

namespace RepositoryGenerator.Core.Extensions
{
    static class EnumerableExtensions
    {
        public static HashSet<string> ToHashSet(this List<string> source)
        {
            return new HashSet<string>(source, StringComparer.CurrentCultureIgnoreCase);
        }
    }
}
