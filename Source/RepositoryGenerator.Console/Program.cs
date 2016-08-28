using Microsoft.Practices.Unity;
using RepositoryGenerator.Core;
using RepositoryGenerator.Core.Services.Interfaces;

namespace RepositoryGenerator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = CoreDependencyBuilder.Create();
            container.Resolve<ICreateDatabaseClassesService>().Create("C:\\temp\\");
        }
    }
}
