using System;
using System.Data;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Mappers
{
    public interface IDataTypeToFunctionReaderMapper
    {
        string Get(DataType dataType);
    }

    public class DataTypeToFunctionReaderMapper : IDataTypeToFunctionReaderMapper
    {
        public string Get(DataType dataType)
        {
            switch (dataType.DbType)
            {
                case DbType.Xml:
                    return "GetNullableString";
                case DbType.AnsiString:
                    return "GetNullableString";
                case DbType.Boolean:
                    return dataType.IsNullable ? "GetNullableBoolean" : "GetBoolean";
                case DbType.Date:
                    return dataType.IsNullable ? "GetNullableDateTime" : "GetDateTime";
                case DbType.DateTime:
                    return dataType.IsNullable ? "GetNullableDateTime" : "GetDateTime";
                case DbType.Decimal:
                    return dataType.IsNullable ? "GetNullableDecimal" : "GetDecimal";
                case DbType.Double:
                    return dataType.IsNullable ? "GetNullableDouble" : "GetDouble";
                case DbType.Guid:
                    return dataType.IsNullable ? "GetNullableGuid" : "GetGuid";
                case DbType.Int16:
                    return dataType.IsNullable ? "GetNullableInt16" : "GetInt16";
                case DbType.Int32:
                    return dataType.IsNullable ? "GetNullableInt32" : "GetInt32";
                case DbType.Int64:
                    return dataType.IsNullable ? "GetNullableInt64" : "GetInt64";
                case DbType.String:
                    return "GetNullableString";
                case DbType.UInt16:
                    return "GetInt16";
                case DbType.UInt32:
                    return "GetInt16";
                case DbType.UInt64:
                    return dataType.IsNullable ? "GetNullableInt64" : "GetInt64";
                case DbType.AnsiStringFixedLength:
                    return "GetNullableString";
                case DbType.StringFixedLength:
                    return "GetNullableString";
                case DbType.Binary:
                    return "GetBinary";
                default:
                    throw new NotSupportedException("Unsupported datatype for select: " + dataType.Name);
            }
        }
    }
}
