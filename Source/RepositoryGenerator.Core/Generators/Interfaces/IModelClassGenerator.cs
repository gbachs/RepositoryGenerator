using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators.Interfaces
{
    public interface IModelClassGenerator
    {
        string Create(TableDefinition tableDefinition);
    }
}