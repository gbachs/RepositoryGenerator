using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Mappers.Interfaces
{
    public interface IDataTypeToFunctionReaderMapper
    {
        string Get(DataType dataType);
    }
}