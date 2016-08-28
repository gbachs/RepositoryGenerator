using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators.Interfaces
{
    public interface IRepositoryClassGenerator
    {
        string Create(TableDefinition tableDefinition);
    }
}