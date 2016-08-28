﻿using Microsoft.Practices.Unity;
using RepositoryGenerator.Core.Generators;
using RepositoryGenerator.Core.Generators.Interfaces;
using RepositoryGenerator.Core.Mappers;
using RepositoryGenerator.Core.Repositories;
using RepositoryGenerator.Core.Repositories.Interfaces;
using RepositoryGenerator.Core.Services;

namespace RepositoryGenerator.Core
{
    public static class CoreDependencyBuilder
    {
        public static IUnityContainer Create(IUnityContainer container = null)
        {
            if(container == null)
                container = new UnityContainer();

            container.RegisterType<ISelectStatementGenerator, SelectStatementGenerator>();
            container.RegisterType<IInsertStatementGenerator, InsertStatementGenerator>();
            container.RegisterType<ISqlCommandGenerator, SqlCommandGenerator>();
            container.RegisterType<ITableDefinitionRepository, TableDefinitionRepository>();
            container.RegisterType<ISqlDataTypeMapper, SqlDataTypeMapper>();
            container.RegisterType<IDataTypeToFunctionReaderMapper, DataTypeToFunctionReaderMapper>();
            container.RegisterType<IRepositoryClassGenerator, RepositoryClassGenerator>();
            container.RegisterType<IDatabaseRepository, DatabaseRepository>();
            container.RegisterType<IModelClassGenerator, ModelClassGenerator>();
            container.RegisterType<ICreateDatabaseClassesService, CreateDatabaseClassesService>();
            container.RegisterType<IUpdateStatementGenerator, UpdateStatementGenerator>();

            return container;
        }
    }
}
