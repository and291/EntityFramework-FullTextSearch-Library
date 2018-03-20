using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using fts_lib.Rewriters;

namespace fts_lib
{
    public class Storage
    {
        public static Storage Instance { get; } = new Storage();

        public readonly List<Rewriter> ActiveRewriters;

        private Storage()
        {
            ActiveRewriters = new List<Rewriter>
            {
                new Contains(),
                new Freetext()
            };
        }

        public Rewriter FindRewriter(DbParameter parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            return ActiveRewriters
                .FirstOrDefault(item => ((string)parameter.Value).StartsWith($"\"{item.Prefix}"));
        }
    }
}