using BulkCsvImporter.Abstract;
using BulkCsvImporter.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Model
{
    public class DatabaseConnectOption : IOptionValidate
    {
        public DatabaseType DatabaseType { get; set; }
        public string ConnectionString { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                throw new ArgumentNullException("DatabaseConnectOption.ConnectionString cannot be null or empty");
            }
        }
    }
}
