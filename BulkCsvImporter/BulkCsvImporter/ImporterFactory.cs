using BulkCsvImporter.Abstract;
using BulkCsvImporter.DatabaseImporter;
using BulkCsvImporter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter
{
    /// <summary>
    /// Simple factory for init Importer
    /// </summary>
    public static class ImporterFactory
    {
        /// <summary>
        /// Create an Importer instance
        /// </summary>
        /// <param name="singleFileImportOption">A single file import option</param>
        /// <returns>Importer instance</returns>
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
