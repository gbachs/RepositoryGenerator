using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators.Interfaces
{
    public interface ISqlCommandGenerator
    {
        string CreateForInsert(TableDefinition tableDefinition);
        string CreateForSelect(TableDefinition tableDefinition);
        string CreateForUpdate(TableDefinition tableDefinition);
        string CreateForDelete(TableDefinition tableDefinition);
        string CreateForExists(TableDefinition tableDefinition);
    }
}