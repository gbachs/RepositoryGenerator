using System.Collections.Generic;
using System.Linq;

namespace RepositoryGenerator.Core.Models
{
    public class TableDefinition
    {
        public TableDefinition()
        {
            Columns = new List<Column>();
        }

        public IList<Column> Columns { get; set; }
        public string Name { get; set; }

        public List<Column> PrimaryKeys => Columns.Where(x => x.IsPrimaryKey).ToList();
    }
}
