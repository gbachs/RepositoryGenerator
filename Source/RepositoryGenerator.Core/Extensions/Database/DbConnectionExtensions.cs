using System;
using System.Data;

namespace RepositoryGenerator.Core.Extensions.Database
{
    public static class IDbConnectionExtensions
    {
        public static IDbCommand CreateTextCommand(this IDbConnection thisObj, string sql)
        {
            return CreateCommand(thisObj, sql, CommandType.Text);
        }

        internal static IDbCommand CreateCommand(this IDbConnection thisObj, string text, CommandType cmdType)
        {
            IDbCommand cmd = null;
            try
            {
                cmd = thisObj.CreateCommand();
                cmd.Connection = thisObj;
                cmd.CommandText = text;
                cmd.CommandType = cmdType;
                return cmd;
            }
            catch (Exception)
            {
                cmd?.Dispose();
                throw;
            }
        }
    }
}
