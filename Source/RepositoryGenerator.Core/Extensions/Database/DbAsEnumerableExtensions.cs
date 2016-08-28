using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RepositoryGenerator.Core.Extensions.Database
{
    public static class DbAsEnumerableExtensions
    {
        public static IEnumerable<IDataRecord> Select(this SqlDatabase db, string sql, Action<IDbCommand> setupCommand)
        {
            using(var connection = db.CreateConnection())
            using (var cmd = connection.CreateTextCommand(sql))
            {
                setupCommand(cmd);
                connection.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        yield return rdr;
                }
            }
        }

        public static IEnumerable<IDataRecord> Select(this SqlDatabase db, string sql)
        {
            using (var connection = db.CreateConnection())
            using (var cmd = connection.CreateTextCommand(sql))
            {
                connection.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        yield return rdr;
                }
            }
        }

        public static IEnumerable<T> Select<T>(this SqlDatabase db, string sql, Func<IDataRecord, T> selector)
        {
            return db.Select(sql).Select(selector);
        }

        public static IEnumerable<T> Select<T>(this SqlDatabase db, string sql, Action<IDbCommand> setupCommand, Func<IDataRecord, T> selector)
        {
            return db.Select(sql, setupCommand).Select(selector);
        }
    }
}