using System.Collections.Generic;
using System.Linq;
using RepositoryGenerator.Core.Extensions;

namespace RepositoryGenerator.Core.Models
{
    public class TableDefinition
    {
        private string _name;

        public TableDefinition()
        {
            Columns = new List<Column>();
        }

        public IList<Column> Columns { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value.FirstLetterToUpper(); }
        }

        public List<Column> PrimaryKeys => Columns.Where(x => x.IsPrimaryKey).ToList();
    }
}
