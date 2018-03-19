using System;
using System.Collections.Generic;
using System.Linq;

namespace fts_lib
{
    public abstract class Parent
    {
        internal static readonly Dictionary<Type, string> Prefixes = new Dictionary<Type, string>
        {
            {typeof(IContains), "-FTSCONTAINSPREFIX-"},
            {typeof(IFreetext), "-FTSFREETEXTPREFIX-"}
        };

        internal string Prefix => 
            Prefixes.First(x => GetType().GetInterfaces().Contains(x.Key)).Value;
    }
}