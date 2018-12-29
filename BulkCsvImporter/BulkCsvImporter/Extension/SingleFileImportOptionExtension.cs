using BulkCsvImporter.Enum;
using BulkCsvImporter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Extension
{
    public static class SingleFileImportOptionExtension
    {
        public static SingleFileImportOption BuildLocalFileSource(this SingleFileImportOption option, string filePath)
        {
            option.FileSourceOption = new LocalFileSourceOption(filePath);
            return option;
        }

        public static SingleFileImportOption BuildDatabaseConnect(this SingleFileImportOption option, DatabaseType databaseType, string connectionString)
        {
            option.DatabaseConnectOption.DatabaseType = databaseType;
            option.DatabaseConnectOption.ConnectionString = connectionString;
            return option;
        }

        public static SingleFileImportOption BuildImportTaget(this SingleFileImportOption option, bool needUpdate, string targetTableName, List<string> columns, List<string> keyColumns)
        {
            option.ImportTargetOption.NeedUpdate = needUpdate;
            option.ImportTargetOption.KeyColumns = keyColumns;
            option.ImportTargetOption.TargetTableName = targetTableName;
            option.ImportTargetOption.Columns = columns;
            return option;
        }
    }
}
