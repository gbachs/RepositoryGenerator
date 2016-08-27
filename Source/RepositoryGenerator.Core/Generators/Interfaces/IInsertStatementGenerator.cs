using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators.Interfaces
{
    public interface IInsertStatementGenerator
    {
        string Create(TableDefinition tableDefinition);
    }
}