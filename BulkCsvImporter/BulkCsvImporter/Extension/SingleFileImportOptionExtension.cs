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
        /// <summary>
        /// Build a local single file source option for import.
        /// </summary>
        /// <param name="option">A single file option</param>
        /// <param name="filePath">The file path of local file</param>
        /// <returns>SingleFileImportOption</returns>
        public static SingleFileImportOption BuildLocalFileSource(this SingleFileImportOption option, string filePath)
        {
            option.FileSourceOption = new LocalFileSourceOption(filePath);
            return option;
        }

        /// <summary>
        /// Build data base connect for a local single file import.
        /// </summary>
        /// <param name="option">A single file option</param>
        /// <param name="databaseType">The database type</param>
        /// <param name="connectionString">The connetion string</param>
        /// <returns>SingleFileImportOption</returns>
        public static SingleFileImportOption BuildDatabaseConnect(this SingleFileImportOption option, DatabaseType databaseType, string connectionString)
        {
            option.DatabaseConnectOption.DatabaseType = databaseType;
            option.DatabaseConnectOption.ConnectionString = connectionString;
            return option;
        }

        /// <summary>
        /// Build import target for a local single file import.
        /// </summary>
        /// <param name="option">A single file option</param>
        /// <param name="needUpdate">If records with same business PK, set to true; if only need insert, set to false.</param>
        /// <param name="targetTableName">The table name the csv should import to</param>
        /// <param name="columns">The table columns</param>
        /// <param name="keyColumns">The business PK columns</param>
        /// <returns></returns>
        public static SingleFileImportOption BuildImportTarget(this SingleFileImportOption option, bool needUpdate, string targetTableName, List<string> columns, List<string> keyColumns)
        {
            option.ImportTargetOption.NeedUpdate = needUpdate;
            option.ImportTargetOption.KeyColumns = keyColumns;
            option.ImportTargetOption.TargetTableName = targetTableName;
            option.ImportTargetOption.Columns = columns;
            return option;
        }
    }
}
