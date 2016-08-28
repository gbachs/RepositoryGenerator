using System.IO;
using RepositoryGenerator.Core.Generators.Interfaces;
using RepositoryGenerator.Core.Repositories;
using RepositoryGenerator.Core.Repositories.Interfaces;
using RepositoryGenerator.Core.Services.Interfaces;

namespace RepositoryGenerator.Core.Services
{
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

        public void Create(string outputPath)
        {
            var tables = _databaseRepository.LoadTableNames();

            foreach (var table in tables)
            {
                var tableDefinition = _tableDefinitionRepository.Load(table);

                var repositoryClass = _repositoryClassGenerator.Create(tableDefinition);
                var modelClass = _modelClassGenerator.Create(tableDefinition);

                File.WriteAllText(Path.Combine(outputPath, $"{table}Repository.cs"), repositoryClass);
                File.WriteAllText(Path.Combine(outputPath, $"{table}.cs"), modelClass);
            }
        }
    }
}