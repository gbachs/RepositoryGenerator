using System.Text;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators
{
    public interface ISqlCommandGenerator
    {
        string CreateForInsert(TableDefinition tableDefinition);
    }

    public class SqlCommandGenerator : ISqlCommandGenerator
    {
        private IInsertStatementGenerator _insertStatementGenerator = new InsertStatementGenerator();

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
    }
}