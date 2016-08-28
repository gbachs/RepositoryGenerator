using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using RepositoryGenerator.Core.Extensions.Database;

namespace RepositoryGenerator.Core.Repositories
{
    public interface IDatabaseRepository
    {
        IList<string> LoadTableNames();
    }

    public class DatabaseRepository: IDatabaseRepository
    {
        private readonly SqlDatabase _db = new SqlDatabase(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

        public IList<string> LoadTableNames()
        {
            return _db.Select("SELECT Name FROM sys.Tables", row => row.GetString(0)).ToList();
        }
    }
}
