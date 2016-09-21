using System.Text;
using RepositoryGenerator.Core.Generators.Interfaces;
using RepositoryGenerator.Core.Mappers.Interfaces;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators
{
    public class SqlCommandGenerator : ISqlCommandGenerator
    {
        private readonly IInsertStatementGenerator _insertStatementGenerator;
        private readonly ISelectStatementGenerator _selectStatementGenerator;
        private readonly IDataTypeToFunctionReaderMapper _dataTypeToFunctionReaderMapper;
        private readonly IUpdateStatementGenerator _updateStatementGenerator;
        private readonly IDeleteStatementGenerator _deleteStatementGenerator;
        private readonly IExistsStatementGenerator _existsStatementGenerator;

        public SqlCommandGenerator(IDataTypeToFunctionReaderMapper dataTypeToFunctionReaderMapper, ISelectStatementGenerator selectStatementGenerator, IInsertStatementGenerator insertStatementGenerator, IUpdateStatementGenerator updateStatementGenerator, IDeleteStatementGenerator deleteStatementGenerator, IExistsStatementGenerator existsStatementGenerator)
        {
            _dataTypeToFunctionReaderMapper = dataTypeToFunctionReaderMapper;
            _selectStatementGenerator = selectStatementGenerator;
            _insertStatementGenerator = insertStatementGenerator;
            _updateStatementGenerator = updateStatementGenerator;
            _deleteStatementGenerator = deleteStatementGenerator;
            _existsStatementGenerator = existsStatementGenerator;
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
            stringBuilder.AppendLine("return _db.Select(sql, cmd=>");
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

        public string CreateForDelete(TableDefinition tableDefinition)
        {
            var deleteStatement = _deleteStatementGenerator.Create(tableDefinition);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"const string sql = @\"{deleteStatement}\";");
            stringBuilder.AppendLine("_db.ExecuteNonQuery(sql, cmd=>");
            stringBuilder.AppendLine("{");

            foreach (var column in tableDefinition.PrimaryKeys)
            {
                stringBuilder.AppendLine($"cmd.AddInParam(\"{column.Name}\", DbType.{column.DataType.DbType}, {tableDefinition.Name.ToLower()}.{column.Name});");
            }

            stringBuilder.AppendLine("})");

            return stringBuilder.ToString();
        }

        public string CreateForExists(TableDefinition tableDefinition)
        {
            var existsStatement = _existsStatementGenerator.Create(tableDefinition);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"const string sql = @\"{existsStatement}\";");
            stringBuilder.AppendLine("return _db.ExecuteScalar<int>(sql, cmd=>");
            stringBuilder.AppendLine("{");

            foreach (var column in tableDefinition.PrimaryKeys)
            {
                stringBuilder.AppendLine($"cmd.AddInParam(\"{column.Name}\", DbType.{column.DataType.DbType}, {tableDefinition.Name.ToLower()}.{column.Name});");
            }

            stringBuilder.AppendLine("}) == 1");

            return stringBuilder.ToString();
        }
    }
}