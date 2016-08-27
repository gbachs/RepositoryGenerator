using System.Linq;
using System.Text;
using RepositoryGenerator.Core.Generators.Interfaces;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators
{
    public class InsertStatementGenerator : IInsertStatementGenerator
    {
        public string Create(TableDefinition tableDefinition)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"insert into {tableDefinition.Name} ({string.Join(",", tableDefinition.Columns.Select(x => $"[{x.Name}]"))})");
            stringBuilder.AppendLine(" values(");

            for (var i = 0; i < tableDefinition.Columns.Count; i++)
            {
                stringBuilder.Append(i == 0 ? $" @{tableDefinition.Columns[i].Name}" : $" ,@{tableDefinition.Columns[i].Name}");
            }

            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }
}
