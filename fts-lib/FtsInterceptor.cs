using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace fts_lib
{
    public class FtsInterceptor : IDbCommandInterceptor
    {
        public static void RewriteFullTextQuery(DbCommand command)
        {
            foreach (DbParameter parameter in command.Parameters)
            {
                if (!parameter.DbType.In(DbType.String, DbType.AnsiString, DbType.StringFixedLength, DbType.AnsiStringFixedLength))
                    continue;

                if (parameter.Value == DBNull.Value)
                    continue;

                parameter.FindRewriter()
                    ?.Rewrite(command, parameter);
            }
        }

        #region IDbCommandInterceptor implements
        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            //
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            //
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            RewriteFullTextQuery(command);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            //
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            RewriteFullTextQuery(command);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            //
        } 
        #endregion
    }
}
