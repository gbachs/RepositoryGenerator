using RepositoryGenerator.Core.Models;

namespace RepositoryGenerator.Core.Mappers.Interfaces
{
    public interface ISqlDataTypeMapper
    {
        DataType Map(string sqlDataTypeName, bool isNullable);
    }
}