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

        public SqlCommandGenerator(IDataTypeToFunctionReaderMapper dataTypeToFunctionReaderMapper, ISelectStatementGenerator selectStatementGenerator, IInsertStatementGenerator insertStatementGenerator)
        {
            _dataTypeToFunctionReaderMapper = dataTypeToFunctionReaderMapper;
            _selectStatementGenerator = selectStatementGenerator;
            _insertStatementGenerator = insertStatementGenerator;
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
                stringBuilder.AppendLine($"cmd.AddInParam(\"{column.Name}\", DbType.{column.DataType.DbType}, dto.{column.Name});");
            }
            stringBuilder.AppendLine("});");

            return stringBuilder.ToString();
        }

        public string CreateForSelect(TableDefinition tableDefinition)
        {
            var selectStatement = _selectStatementGenerator.Create(tableDefinition);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"const string sql = @\"{selectStatement}\";");
            stringBuilder.AppendLine("_db.ExecuteNonQuery(sql, cmd=>");
            stringBuilder.AppendLine("{");

            stringBuilder.AppendLine($"}}, row=>{{ return new {tableDefinition.Name} {{");

            foreach (var column in tableDefinition.Columns)
            {
                stringBuilder.AppendLine($"{column.Name} = row.{_dataTypeToFunctionReaderMapper.Get(column.DataType)}");
            }

            stringBuilder.AppendLine("}}).SingleOrDefault();");

            return stringBuilder.ToString();
        }
    }
}