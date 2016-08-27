using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators.Interfaces
{
    public interface ISelectStatementGenerator
    {
        string Create(TableDefinition tableDefinition);
    }
}