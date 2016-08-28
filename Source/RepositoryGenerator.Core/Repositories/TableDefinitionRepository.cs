using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using RepositoryGenerator.Core.Extensions;
using RepositoryGenerator.Core.Extensions.Database;
using RepositoryGenerator.Core.Mappers.Interfaces;
using RepositoryGenerator.Core.Models;
using RepositoryGenerator.Core.Repositories.Interfaces;

namespace RepositoryGenerator.Core.Repositories
{
    public class TableDefinitionRepository : ITableDefinitionRepository
    {
        private readonly SqlDatabase _db = new SqlDatabase(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

        private readonly ISqlDataTypeMapper _sqlDataTypeMapper;

        public TableDefinitionRepository(ISqlDataTypeMapper sqlDataTypeMapper)
        {
            _sqlDataTypeMapper = sqlDataTypeMapper;
        }

        public TableDefinition Load(string tableName)
        {
            var tableDefinition = new TableDefinition {Name = tableName};
            tableDefinition.Columns = LoadColumns(tableDefinition);
            return tableDefinition;
        }

        private IList<Column> LoadColumns(TableDefinition tableDefinition)
        {
            var primaryKeys = LoadPrimaryKeyColumns(tableDefinition);

            const string sql = @"SELECT COLUMN_NAME, IS_NULLABLE, DATA_Type
                                FROM INFORMATION_SCHEMA.COLUMNS
                                WHERE table_name = @TableName ORDER BY ORDINAL_POSITION";

            return _db.Select(sql, cmd => cmd.AddInParam("TableName", DbType.String, tableDefinition.Name), row =>
            {
                var columnName = row.GetString(0);
                var isNullable = row.GetString(1) == "YES";
                return new Column
                {
                    Name = columnName,
                    DataType = _sqlDataTypeMapper.Map(row.GetString(2), isNullable),
                    IsPrimaryKey = primaryKeys.Contains(columnName)
                };
            }).ToList();
        }

        private HashSet<string> LoadPrimaryKeyColumns(TableDefinition tableDefinition)
        {
            const string sql = @"SELECT COLUMN_NAME
                                FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                                WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + CONSTRAINT_NAME), 'IsPrimaryKey') = 1
                                AND TABLE_NAME = @TableName";

            return _db.Select(sql,
                cmd => cmd.AddInParam("TableName", DbType.String, tableDefinition.Name),
                row => row.GetString(0)).ToList().ToHashSet();
        }
    }
}