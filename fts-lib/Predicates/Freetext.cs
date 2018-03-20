using System;
using System.Text.RegularExpressions;
using fts_lib.Model;

namespace fts_lib.Predicates
{
    public class Freetext : FtsParameter
    {
        public Freetext(string search) : base(search)
        {
        }
    }

    public class RewriterFreetext : Rewriter
    {
        public RewriterFreetext(string prefix, Type type) : base(prefix, type)
        {
        }

        protected override string RewriteCommandText(string commandText, string parameterName)
        {
            return Regex.Replace(commandText,
                $@"\[(\w*)\].\[(\w*)\]\s*LIKE\s*@{parameterName}\s?(?:ESCAPE N?'~')",
                $@"FREETEXT([$1].[$2], @{parameterName}, LANGUAGE 'Russian')");
        }
    }
}
