using System.Linq;
using System.Text;
using RepositoryGenerator.Core.Generators.Interfaces;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators
{
    public class DeleteStatementGenerator : IDeleteStatementGenerator
    {
        public string Create(TableDefinition tableDefinition)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"delete from {tableDefinition.Name}");

            if (tableDefinition.PrimaryKeys.Any())
            {
                for (var i = 0; i < tableDefinition.PrimaryKeys.Count; i++)
                {
                    var columnName = tableDefinition.PrimaryKeys[i].Name;
                    stringBuilder.AppendLine(i == 0
                        ? $" where {columnName} = @{columnName}"
                        : $" and {columnName} = @{columnName}");
                }
            }

            return stringBuilder.ToString();
        }
    }
}