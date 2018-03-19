using System.Text.RegularExpressions;

namespace fts_lib
{
    public interface IFreetext { }

    public class Freetext : FtsParameter, IFreetext
    {
        public Freetext(string search) : base(search)
        {
        }
    }

    public class RewriterFreetext : Rewriter, IFreetext
    {
        protected override string RewriteCommandText(string commandText, string parameterName)
        {
            return Regex.Replace(commandText,
                $@"\[(\w*)\].\[(\w*)\]\s*LIKE\s*@{parameterName}\s?(?:ESCAPE N?'~')",
                $@"FREETEXT([$1].[$2], @{parameterName}, LANGUAGE 'Russian')");
        }
    }
}
