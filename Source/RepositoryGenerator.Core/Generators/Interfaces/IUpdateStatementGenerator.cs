using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators.Interfaces
{
    public interface IUpdateStatementGenerator
    {
        string Create(TableDefinition tableDefinition);
    }
}