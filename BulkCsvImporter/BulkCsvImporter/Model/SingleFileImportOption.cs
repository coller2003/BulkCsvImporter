using BulkCsvImporter.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Model
{
    public class SingleFileImportOption
    {
        public Dictionary<int, string> ColumnMatchDictionary { get; set; }

        public string TableName { get; set; }

        public FileSourceOption FileSourceOption { get; set; }
    }
}
