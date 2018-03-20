using fts_lib.Model;
using System.Text.RegularExpressions;

namespace fts_lib.Predicates
{
    public class Freetext : Rewriter
    {
        public static string PrefixFreetext => "-FTSFREETEXTPREFIX-";

        public Freetext() : base(PrefixFreetext)
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
