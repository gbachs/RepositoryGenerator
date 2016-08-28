using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators.Interfaces
{
    public interface IDeleteStatementGenerator
    {
        string Create(TableDefinition tableDefinition);
    }
}