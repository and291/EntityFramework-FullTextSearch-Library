using fts_lib.Model;
using fts_lib.Predicates;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace fts_lib
{
    internal static class Extensions
    {
        public static bool In<T>(this T source, params T[] list)
        {
            return (list as IList<T>).Contains(source);
        }

        private static readonly List<Rewriter> ActiveRewriters = new List<Rewriter>
        {
            new RewriterContains(),
            new RewriterFreetext()
        };

        public static Rewriter FindRewriter(this DbParameter parameter)
        {
            return ActiveRewriters.FirstOrDefault(rewriter => rewriter.IsContains(parameter));
        }
    }
}
