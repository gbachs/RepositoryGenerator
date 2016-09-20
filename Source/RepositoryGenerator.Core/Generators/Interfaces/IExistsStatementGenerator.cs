using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators.Interfaces
{
    public interface IExistsStatementGenerator
    {
        string Create(TableDefinition tableDefinition);
    }
}