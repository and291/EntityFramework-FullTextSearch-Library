using System;
using System.Text.RegularExpressions;
using fts_lib.Model;

namespace fts_lib.Predicates
{
    public class Contains : FtsParameter
    {
        public Contains(string search) : base(search)
        {
        }
    }

    public class RewriterContains : Rewriter
    {
        public RewriterContains(string prefix, Type type) : base(prefix, type)
        {
        }

        protected override string RewriteParameterValue(string value)
        {
            return $"FORMSOF (INFLECTIONAL, {base.RewriteParameterValue(value)})";
        }

        protected override string RewriteCommandText(string commandText, string parameterName)
        {
            return Regex.Replace(commandText,
                $@"\[(\w*)\].\[(\w*)\]\s*LIKE\s*@{parameterName}\s?(?:ESCAPE N?'~')",
                $@"CONTAINS([$1].[$2], @{parameterName}, LANGUAGE 'Russian')");
        }
    }
}
