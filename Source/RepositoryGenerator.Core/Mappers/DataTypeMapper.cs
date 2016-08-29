using System;
using System.Data;
using RepositoryGenerator.Core.Mappers.Interfaces;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Mappers
{
    public class SqlDataTypeMapper : ISqlDataTypeMapper
    {
        public DataType Map(string sqlDataTypeName, bool isNullable)
        {
            switch (sqlDataTypeName.ToLower())
            {
                case "bigint":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(long?), SqlDbType.BigInt, DbType.Int64, true)
                        : new DataType(sqlDataTypeName, typeof(long), SqlDbType.BigInt, DbType.Int64, false);
                case "decimal":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(decimal?), SqlDbType.Decimal, DbType.Decimal, true)
                        : new DataType(sqlDataTypeName, typeof(decimal), SqlDbType.Decimal, DbType.Decimal, false);
                case "bit":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(bool?), SqlDbType.Bit, DbType.Boolean, true)
                        : new DataType(sqlDataTypeName, typeof(bool), SqlDbType.Bit, DbType.Boolean, false);
                case "char":
                    return new DataType(sqlDataTypeName, typeof(string), SqlDbType.Char, DbType.String, true);
                case "nchar":
                    return new DataType(sqlDataTypeName, typeof(string), SqlDbType.NChar, DbType.String, true);
                case "ntext":
                    return new DataType(sqlDataTypeName, typeof(string), SqlDbType.NText, DbType.String, true);
                case "nvarchar":
                    return new DataType(sqlDataTypeName, typeof(string), SqlDbType.NVarChar, DbType.String, true);
                case "text":
                    return new DataType(sqlDataTypeName, typeof(string), SqlDbType.Text, DbType.String, true);
                case "varchar":
                    return new DataType(sqlDataTypeName, typeof(string), SqlDbType.VarChar, DbType.String, true);
                case "xml":
                    return new DataType(sqlDataTypeName, typeof(string), SqlDbType.Xml, DbType.Xml, true);
                case "int":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(int?), SqlDbType.Int, DbType.Int32, true)
                        : new DataType(sqlDataTypeName, typeof(int), SqlDbType.Int, DbType.Int32, false);
                case "float":
                    if (isNullable)
                        return new DataType(sqlDataTypeName, typeof(long?), SqlDbType.Float, DbType.Int64, true);
                    return new DataType(sqlDataTypeName, typeof(long), SqlDbType.Float, DbType.Int64, false);
                case "date":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(DateTime?), SqlDbType.Date, DbType.Date, true)
                        : new DataType(sqlDataTypeName, typeof(DateTime), SqlDbType.Date, DbType.Date, false);
                case "datetime":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(DateTime?), SqlDbType.DateTime, DbType.DateTime, true)
                        : new DataType(sqlDataTypeName, typeof(DateTime), SqlDbType.DateTime, DbType.DateTime, false);
                case "time":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(DateTime?), SqlDbType.Time, DbType.Time, true)
                        : new DataType(sqlDataTypeName, typeof(DateTime), SqlDbType.Time, DbType.Time, false);
                case "smallint":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(short?), SqlDbType.SmallInt, DbType.Int16, true)
                        : new DataType(sqlDataTypeName, typeof(short), SqlDbType.SmallInt, DbType.Int16, false);
                case "tinyint":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(short?), SqlDbType.TinyInt, DbType.Int16, true)
                        : new DataType(sqlDataTypeName, typeof(short), SqlDbType.TinyInt, DbType.Int16, false);
                case "uniqueidentifier":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(Guid?), SqlDbType.UniqueIdentifier, DbType.Guid, true)
                        : new DataType(sqlDataTypeName, typeof(Guid), SqlDbType.UniqueIdentifier, DbType.Guid, false);
                case "varbinary":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(byte?[]), SqlDbType.VarBinary, DbType.Binary, true)
                        : new DataType(sqlDataTypeName, typeof(byte[]), SqlDbType.VarBinary, DbType.Binary, false);
                case "numeric":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(decimal?), SqlDbType.Decimal, DbType.Decimal, true)
                        : new DataType(sqlDataTypeName, typeof(decimal), SqlDbType.Decimal, DbType.Decimal, false);
                case "datetime2":
                    return isNullable
                        ? new DataType(sqlDataTypeName, typeof(DateTime?), SqlDbType.DateTime2, DbType.DateTime2, true)
                        : new DataType(sqlDataTypeName, typeof(DateTime), SqlDbType.DateTime2, DbType.DateTime2, false);
                default:
                    return new DataType(sqlDataTypeName, typeof(string), SqlDbType.NVarChar, DbType.String, false);
            }
        }
    }
}