using System.Text;
using RepositoryGenerator.Core.Generators.Interfaces;
using RepositoryGenerator.Core.Mappers;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators
{
    public class SqlCommandGenerator : ISqlCommandGenerator
    {
        private readonly IInsertStatementGenerator _insertStatementGenerator;
        private readonly ISelectStatementGenerator _selectStatementGenerator;
        private readonly IDataTypeToFunctionReaderMapper _dataTypeToFunctionReaderMapper;
        private readonly IUpdateStatementGenerator _updateStatementGenerator;

        public SqlCommandGenerator(IDataTypeToFunctionReaderMapper dataTypeToFunctionReaderMapper, ISelectStatementGenerator selectStatementGenerator, IInsertStatementGenerator insertStatementGenerator, IUpdateStatementGenerator updateStatementGenerator)
        {
            _dataTypeToFunctionReaderMapper = dataTypeToFunctionReaderMapper;
            _selectStatementGenerator = selectStatementGenerator;
            _insertStatementGenerator = insertStatementGenerator;
            _updateStatementGenerator = updateStatementGenerator;
        }

        public string CreateForInsert(TableDefinition tableDefinition)
        {
            var insertStatement = _insertStatementGenerator.Create(tableDefinition);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"const string sql = @\"{insertStatement}\";");
            stringBuilder.AppendLine("_db.ExecuteNonQuery(sql, cmd=>");
            stringBuilder.AppendLine("{");

            foreach (var column in tableDefinition.Columns)
            {
                stringBuilder.AppendLine($"cmd.AddInParam(\"{column.Name}\", DbType.{column.DataType.DbType}, {tableDefinition.Name.ToLower()}.{column.Name});");
            }

            stringBuilder.AppendLine("})");

            return stringBuilder.ToString();
        }

        public string CreateForSelect(TableDefinition tableDefinition)
        {
            var selectStatement = _selectStatementGenerator.Create(tableDefinition);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"const string sql = @\"{selectStatement}\";");
            stringBuilder.AppendLine("return _db.ExecuteNonQuery(sql, cmd=>");
            stringBuilder.AppendLine("{");

            foreach (var primaryKey in tableDefinition.PrimaryKeys)
            {
                stringBuilder.AppendLine($"cmd.AddInParam(\"{primaryKey.Name}\", DbType.{primaryKey.DataType.DbType}, {primaryKey.Name.ToLower()});");
            }

            stringBuilder.AppendLine($"}}, row=>{{ return new {tableDefinition.Name} {{");

            var i = 0;

            foreach (var column in tableDefinition.Columns)
            {
                stringBuilder.AppendLine($"{column.Name} = row.{_dataTypeToFunctionReaderMapper.Get(column.DataType)}({i}),");
                i++;
            }

            stringBuilder.AppendLine("};}).SingleOrDefault();");

            return stringBuilder.ToString();
        }

        public string CreateForUpdate(TableDefinition tableDefinition)
        {
            var updateStatement = _updateStatementGenerator.Create(tableDefinition);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"const string sql = @\"{updateStatement}\";");
            stringBuilder.AppendLine("_db.ExecuteNonQuery(sql, cmd=>");
            stringBuilder.AppendLine("{");

            foreach (var column in tableDefinition.Columns)
            {
                stringBuilder.AppendLine($"cmd.AddInParam(\"{column.Name}\", DbType.{column.DataType.DbType}, {tableDefinition.Name.ToLower()}.{column.Name});");
            }

            stringBuilder.AppendLine("})");

            return stringBuilder.ToString();
        }
    }
}