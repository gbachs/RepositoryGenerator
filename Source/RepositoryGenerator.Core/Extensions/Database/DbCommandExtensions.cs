using System;
using System.Data;

namespace RepositoryGenerator.Core.Extensions.Database
{
    public static class DbCommandExtensions
    {
        public static void AddInParam(this IDbCommand thisObj, string name, DbType type, object value)
        {
            IDataParameter param = thisObj.CreateDbParam(name, type, value);
            thisObj.Parameters.Add(param); //Do not add until after all values have been set, in case of exception
        }


        public static IDbDataParameter CreateDbParam(this IDbCommand thisObj, string name, DbType type, Object value)
        {
            return CreateDbParam(thisObj, name, type, ParameterDirection.Input, value);
        }

        public static IDbDataParameter CreateDbParam(this IDbCommand thisObj, string name, DbType type, ParameterDirection direction, Object value)
        {
            var param = thisObj.CreateParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Value = value ?? DBNull.Value;
            param.Direction = direction;
            return param;
        }
    }
}