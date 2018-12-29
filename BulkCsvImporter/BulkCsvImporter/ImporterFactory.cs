using BulkCsvImporter.Abstract;
using BulkCsvImporter.DatabaseImporter;
using BulkCsvImporter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter
{
    public static class ImporterFactory
    {
        public static Importer CreateInstance(SingleFileImportOption singleFileImportOption)
        {
            Importer importer = null;

            switch (singleFileImportOption.DatabaseConnectOption.DatabaseType)
            {
                case Enum.DatabaseType.SQLServer:
                    importer = new SqlServerImporter(singleFileImportOption);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return importer;
        }
    }
}
