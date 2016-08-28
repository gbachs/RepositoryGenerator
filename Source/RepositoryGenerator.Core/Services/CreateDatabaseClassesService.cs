using System.IO;
using System.Security.AccessControl;
using RepositoryGenerator.Core.Generators;
using RepositoryGenerator.Core.Repositories;
using RepositoryGenerator.Core.Repositories.Interfaces;

namespace RepositoryGenerator.Core.Services
{
    public interface ICreateDatabaseClassesService
    {
        void Create();
    }

    public class CreateDatabaseClassesService: ICreateDatabaseClassesService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly ITableDefinitionRepository _tableDefinitionRepository;
        private readonly IRepositoryClassGenerator _repositoryClassGenerator;
        private readonly IModelClassGenerator _modelClassGenerator;

        public CreateDatabaseClassesService(IDatabaseRepository databaseRepository, ITableDefinitionRepository tableDefinitionRepository, IRepositoryClassGenerator repositoryClassGenerator, IModelClassGenerator modelClassGenerator)
        {
            _databaseRepository = databaseRepository;
            _tableDefinitionRepository = tableDefinitionRepository;
            _repositoryClassGenerator = repositoryClassGenerator;
            _modelClassGenerator = modelClassGenerator;
        }

        public void Create()
        {
            var tables = _databaseRepository.LoadTableNames();

            foreach (var table in tables)
            {
                var tableDefinition = _tableDefinitionRepository.Load(table);

                var repositoryClass = _repositoryClassGenerator.Generate(tableDefinition);
                var modelClass = _modelClassGenerator.Generate(tableDefinition);

                File.WriteAllText($"c:\\temp\\{table}Repository.cs", repositoryClass);
                File.WriteAllText($"c:\\temp\\{table}.cs", modelClass);
            }
        }
    }
}
