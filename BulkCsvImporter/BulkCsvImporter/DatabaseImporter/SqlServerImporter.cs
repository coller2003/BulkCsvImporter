using BulkCsvImporter.Abstract;
using BulkCsvImporter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.DatabaseImporter
{
    internal class SqlServerImporter : Importer
    {
        public SqlServerImporter(SingleFileImportOption singleFileImportOption) : base(singleFileImportOption)
        {

        }

        public override void Import()
        {
            throw new NotImplementedException();
        }
    }
}
