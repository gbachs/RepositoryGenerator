using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using RepositoryGenerator.Core.Mappers;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Repositories
{
    public interface ITableDefinitionRepository
    {
        TableDefinition Load(string tableName);
    }

    public class TableDefinitionRepository : ITableDefinitionRepository
    {
        private readonly ISqlDataTypeMapper _sqlDataTypeMapper;

        public TableDefinitionRepository(ISqlDataTypeMapper sqlDataTypeMapper)
        {
            _sqlDataTypeMapper = sqlDataTypeMapper;
        }

        public TableDefinition Load(string tableName)
        {
            const string sql = @"SELECT COLUMN_NAME, ORDINAL_POSITION, IS_NULLABLE, DATA_Type
                                FROM INFORMATION_SCHEMA.COLUMNS
                                WHERE table_name = @TableName ORDER BY ORDINAL_POSITION";

            var tableDefinition = new TableDefinition { Name = tableName };

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("TableName", tableName);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var isNullable = reader.GetString(2) == "YES";

                            var column = new Column
                            {
                                Name = reader.GetString(0),
                                DataType = _sqlDataTypeMapper.Map(reader.GetString(3), isNullable)
                            };

                            tableDefinition.Columns.Add(column);
                        }
                    }
                }
            }

            return tableDefinition;
        }
    }
}
