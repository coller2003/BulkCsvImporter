using BulkCsvImporter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulkCsvImporter.Extension;
using BulkCsvImporter.Enum;

namespace BulkCsvImporter.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var singleFileImportOption = new SingleFileImportOption().BuildDatabaseConnect(DatabaseType.SQLServer, null).BuildImportTaget(true, string.Empty, null, null).BuildLocalFileSource(null);
            var importer = ImporterFactory.CreateInstance(singleFileImportOption);
            importer.Import();
        }
    }
}
