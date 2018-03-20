﻿using fts_lib.Model;
using System.Text.RegularExpressions;

namespace fts_lib.Predicates
{
    public class RewriterContains : Rewriter
    {
        public static string PrefixContains => "-FTSCONTAINSPREFIX-";

        public RewriterContains() : base(PrefixContains)
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
