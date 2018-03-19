using System;
using System.Data;
using System.Data.Common;

namespace fts_lib
{
    public abstract class Rewriter : Parent
    {
        public bool IsContains(DbParameter parameter)
        {
            return parameter.Value.ToString().StartsWith($"\"{Prefix}");
        }

        public void Rewrite(DbCommand command, DbParameter parameter)
        {
            var value = RewriteParameterValue((string)parameter.Value);

            parameter.Size = value.Length;
            parameter.DbType = DbType.AnsiStringFixedLength;
            parameter.Value = value;

            var commandText = command.CommandText;
            command.CommandText = RewriteCommandText(commandText, parameter.ParameterName);
            if (commandText == command.CommandText)
            {
                throw new Exception("FTS was not replaced on: " + commandText);
            }
        }

        protected virtual string RewriteParameterValue(string value)
        {
            value = value.Replace(Prefix, ""); // remove prefix we added n linq query
            return value.Substring(1, value.Length - 2); // remove %% escaping by linq translator from string.Contains to sql LIKE
        }

        protected abstract string RewriteCommandText(string commandText, string parameterName);
    }
}