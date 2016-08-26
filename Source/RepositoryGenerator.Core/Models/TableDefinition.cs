using System.Collections.Generic;

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
    }
}
