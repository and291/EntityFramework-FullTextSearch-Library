using System;
using System.Data;
using System.Data.Common;

namespace fts_lib.Model
{
    public abstract class Rewriter
    {
        public string Prefix { get; }

        protected Rewriter(string prefix)
        {
            Prefix = prefix;
        }

        public void Rewrite(DbCommand command, DbParameter parameter)
        {
            var value = Unwrap((string)parameter.Value);

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

        public string Wrap(string value)
        {
            return ValueWrapper.Wrap(Prefix, value);
        }

        protected virtual string Unwrap(string value)
        {
            return ValueWrapper.Unwrap(Prefix, value);
        }

        protected abstract string RewriteCommandText(string commandText, string parameterName);
    }
}