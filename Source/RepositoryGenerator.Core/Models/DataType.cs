using System;
using System.Data;

namespace RepositoryGenerator.Core.Models
{
    public class DataType
    {
        public DataType(string name, Type dotNetType, SqlDbType sqlDbType, DbType dbType, bool isNullable)
        {
            Name = name;
            DotNetType = dotNetType;
            SqlDbType = sqlDbType;
            DbType = dbType;
            IsNullable = isNullable;
        }

        public string Name { get; set; }
        public Type DotNetType { get; set; }
        public Type DotNetTypeNotNullable { get; set; }
        public SqlDbType SqlDbType { get; set; }
        public DbType DbType { get; set; }
        public bool IsNullable { get; set; }
    }
}