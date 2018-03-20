using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using fts_lib.Model;
using fts_lib.Predicates;

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
                new RewriterContains(prefix:"-FTSCONTAINSPREFIX-", type:typeof(Contains)),
                new RewriterFreetext(prefix:"-FTSFREETEXTPREFIX-", type:typeof(Freetext))
            };
        }

        public Rewriter FindRewriter(DbParameter parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            return ActiveRewriters
                .FirstOrDefault(item => ((string)parameter.Value).StartsWith($"\"{item.Prefix}"));
        }

        //public string GetPrefixByImplementedInterface(Type type)
        //{
        //    if (type == null) throw new ArgumentNullException(nameof(type));
        //    return ActiveRewriters.First(x => type.GetInterfaces().Contains(x.Type)).Prefix;
        //}

        public string GetPrefixForType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return ActiveRewriters.First(x => x.Type == type).Prefix;
        }
    }
}