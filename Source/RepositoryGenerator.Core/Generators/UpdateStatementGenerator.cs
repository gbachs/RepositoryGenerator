using System.Linq;
using System.Text;
using RepositoryGenerator.Core.Generators.Interfaces;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators
{
    public class UpdateStatementGenerator : IUpdateStatementGenerator
    {
        public string Create(TableDefinition tableDefinition)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"update {tableDefinition.Name} set ");

            var updateColumns = tableDefinition.Columns.Where(x => !x.IsPrimaryKey).ToArray();

            for (var i = 0; i < updateColumns.Length; i++)
            {
                var column = updateColumns[i];

                stringBuilder.Append(i + 1 == updateColumns.Length
                    ? $"[{column.Name}] = @{column.Name} "
                    : $"[{column.Name}] = @{column.Name}, ");
            }

            var p = 0;
            foreach (var primaryKey in tableDefinition.PrimaryKeys)
            {
                stringBuilder.Append(p == 0
                    ? $"where {primaryKey.Name} = @{primaryKey.Name} "
                    : $"and {primaryKey.Name} = @{primaryKey.Name} ");
                p++;
            }

            return stringBuilder.ToString();
        }
    }
}