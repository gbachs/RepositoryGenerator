using System;
using System.Data;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Mappers
{
    public interface ISqlDataTypeMapper
    {
        DataType Map(string sqlDataTypeName, bool isNullable);
    }

    public class SqlDataTypeMapper : ISqlDataTypeMapper
    {
        public DataType Map(string sqlDataTypeName, bool isNullable)
        {
            switch (sqlDataTypeName.ToLower())
            {
                case "bigint":
                    if (isNullable)
                        return new DataType(sqlDataTypeName, typeof(long?), SqlDbType.BigInt, DbType.Int64, true);
                    return new DataType(sqlDataTypeName, typeof(long), SqlDbType.BigInt, DbType.Int64, false);
                case "decimal":
                    if (isNullable)
                        return new DataType(sqlDataTypeName, typeof(decimal?), SqlDbType.Decimal, DbType.Decimal, true);
                    return new DataType(sqlDataTypeName, typeof(decimal), SqlDbType.Decimal, DbType.Decimal, false);
                case "bit":
                    if (isNullable)
                        return new DataType(sqlDataTypeName, typeof(bool?), SqlDbType.Bit, DbType.Boolean, true);
                    return new DataType(sqlDataTypeName, typeof(bool), SqlDbType.Bit, DbType.Boolean, false);
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
                    if (isNullable)
                        return new DataType(sqlDataTypeName, typeof(int?), SqlDbType.Int, DbType.Int32, true);
                    return new DataType(sqlDataTypeName, typeof(int), SqlDbType.Int, DbType.Int32, false);
                case "float":
                    if (isNullable)
                        return new DataType(sqlDataTypeName, typeof(long?), SqlDbType.Float, DbType.Int64, true);
                    return new DataType(sqlDataTypeName, typeof(long), SqlDbType.Float, DbType.Int64, false);
                case "date":
                    if (isNullable)
                        return new DataType(sqlDataTypeName, typeof(DateTime?), SqlDbType.Date, DbType.Date, true);
                    return new DataType(sqlDataTypeName, typeof(DateTime), SqlDbType.Date, DbType.Date, false);
                case "datetime":
                    if (isNullable)
                        return new DataType(sqlDataTypeName, typeof(DateTime?), SqlDbType.DateTime, DbType.DateTime, true);
                    return new DataType(sqlDataTypeName, typeof(DateTime), SqlDbType.DateTime, DbType.DateTime, false);
                case "time":
                    if (isNullable)
                        return new DataType(sqlDataTypeName, typeof(DateTime?), SqlDbType.Time, DbType.Time, true);
                    return new DataType(sqlDataTypeName, typeof(DateTime), SqlDbType.Time, DbType.Time, false);
                case "smallint":
                    if (isNullable)
                        return new DataType(sqlDataTypeName, typeof(Int16?), SqlDbType.SmallInt, DbType.Int16, true);
                    return new DataType(sqlDataTypeName, typeof(Int16), SqlDbType.SmallInt, DbType.Int16, false);
                case "tinyint":
                    if (isNullable)
                        return new DataType(sqlDataTypeName, typeof(Int16?), SqlDbType.TinyInt, DbType.Int16, true);
                    return new DataType(sqlDataTypeName, typeof(Int16), SqlDbType.TinyInt, DbType.Int16, false);
                case "uniqueIdentifier":
                    if (isNullable)
                        return new DataType(sqlDataTypeName, typeof(Guid?), SqlDbType.UniqueIdentifier, DbType.Guid, true);
                    return new DataType(sqlDataTypeName, typeof(Guid), SqlDbType.UniqueIdentifier, DbType.Guid, false);
                default:
                    throw new ApplicationException("Unsupported datatype: " + sqlDataTypeName);
            }
        }
    }
}
