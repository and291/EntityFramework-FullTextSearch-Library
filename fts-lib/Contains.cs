using System.Text.RegularExpressions;

namespace fts_lib
{
    public interface IContains { }

    public class Contains : FtsParameter, IContains
    {
        public Contains(string search) : base(search)
        {
        }
    }

    public class RewriterContains : Rewriter, IContains
    {
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
