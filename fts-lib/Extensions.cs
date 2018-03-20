using System.Collections.Generic;
using System.Data.Common;
using fts_lib.Rewriters;

namespace fts_lib
{
    internal static class Extensions
    {
        public static bool In<T>(this T source, params T[] list)
        {
            return (list as IList<T>).Contains(source);
        }

        public static Rewriter FindRewriter(this DbParameter parameter)
        {
            return Storage.Instance.FindRewriter(parameter);
        }
    }
}
