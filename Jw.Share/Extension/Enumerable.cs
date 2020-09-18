using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jw.Share
{
    public static class Enumerable
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }
        public static bool IsNotEmpty<T>(this IEnumerable<T> source)
        {
            return source.Any();
        }
    }
}
