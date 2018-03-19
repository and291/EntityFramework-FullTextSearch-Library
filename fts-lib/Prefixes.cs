using System;
using System.Collections.Generic;
using System.Linq;

namespace fts_lib
{
    public class Prefixes
    {
        public static Prefixes Instance { get; } = new Prefixes();

        private readonly Dictionary<Type, string> _prefixDictionary;

        private Prefixes()
        {
            _prefixDictionary = new Dictionary<Type, string>
            {
                {typeof(IContains), "-FTSCONTAINSPREFIX-"},
                {typeof(IFreetext), "-FTSFREETEXTPREFIX-"}
            };
        }

        public string GetPrefixByImplementedInterface(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return _prefixDictionary.First(x => type.GetInterfaces().Contains(x.Key)).Value;
        }

        public string GetPrefixForType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return _prefixDictionary.First(x => x.Key == type).Value;
        }
    }
}