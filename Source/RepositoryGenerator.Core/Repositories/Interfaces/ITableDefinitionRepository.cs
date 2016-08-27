using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Repositories.Interfaces
{
    public interface ITableDefinitionRepository
    {
        TableDefinition Load(string tableName);
    }
}
