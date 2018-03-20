using System;
using System.Data;
using System.Data.Common;

namespace fts_lib.Model
{
    public abstract class Rewriter
    {
        public string Prefix { get; }
        public Type Type { get; }

        protected Rewriter(string prefix, Type type)
        {
            Prefix = prefix;
            Type = type;

            //if (!typeof(Rewriter).GetInterfaces().Contains(type))
              //  throw new ArgumentException();
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
            return ValueWrapper.Unwrap(Prefix, value);
        }

        protected abstract string RewriteCommandText(string commandText, string parameterName);
    }
}