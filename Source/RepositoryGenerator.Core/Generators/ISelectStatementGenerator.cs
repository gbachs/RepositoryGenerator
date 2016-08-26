using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Generators
{
    public interface ISelectStatementGenerator
    {
        string Create(TableDefinition tableDefinition);
    }

    public class SelectStatementGenerator : ISelectStatementGenerator
    {
        public string Create(TableDefinition tableDefinition)
        {
            throw new System.NotImplementedException();
        }
    }
}