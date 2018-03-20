using System.Text.RegularExpressions;

namespace fts_lib.Rewriters
{
    public class Contains : Rewriter
    {
        public static string PrefixContains => "-FTSCONTAINSPREFIX-";

        public Contains() : base(PrefixContains)
        {
        }

        protected override string Unwrap(string value)
        {
            return $"FORMSOF (INFLECTIONAL, {base.Unwrap(value)})";
        }

        protected override string RewriteCommandText(string commandText, string parameterName)
        {
            return Regex.Replace(commandText,
                $@"\[(\w*)\].\[(\w*)\]\s*LIKE\s*@{parameterName}\s?(?:ESCAPE N?'~')",
                $@"CONTAINS([$1].[$2], @{parameterName}, LANGUAGE 'Russian')");
        }
    }
}
