using System.Linq;
using System.Text;
using RepositoryGenerator.Core.Generators.Interfaces;
using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators
{
    public class ExistsStatementGenerator : IExistsStatementGenerator
    {
        public string Create(TableDefinition tableDefinition)
        {
            var stringBuilder = new StringBuilder();
            //if exists (select 1 from BDCConfigurations where cfgid = 234234 ) select 1 else select 0

            stringBuilder.AppendLine($"if exists (select 1 from {tableDefinition.Name}");

            if (tableDefinition.PrimaryKeys.Any())
            {
                for (var i = 0; i < tableDefinition.PrimaryKeys.Count; i++)
                {
                    var columnName = tableDefinition.PrimaryKeys[i].Name;
                    stringBuilder.AppendLine(i == 0
                        ? $"where {columnName} = @{columnName}"
                        : $"and {columnName} = @{columnName}");
                }
            }

            stringBuilder.AppendLine(") select 1 else select 0");

            return stringBuilder.ToString();
        }
    }
}