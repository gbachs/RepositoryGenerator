using System;

namespace RepositoryGenerator.Core.Models
{
    public class Column
    {
        public string Name { get; set; }
        public DataType DataType { get; set; }
        public bool IsPrimaryKey { get; set; }
    }
}