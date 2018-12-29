using BulkCsvImporter.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Model
{
    public class SingleFileImportOption : IOptionValidate
    {
        public ImportTagetOption ImportTargetOption { get; set; }

        public DatabaseConnectOption DatabaseConnectOption { get; set; }

        public FileSourceOption FileSourceOption { get; set; }

        public SingleFileImportOption()
        {
            ImportTargetOption = new ImportTagetOption();
            DatabaseConnectOption = new DatabaseConnectOption();
        }

        public void Validate()
        {
            if (FileSourceOption == null)
                throw new ArgumentNullException("FileSourceOption cannot be null");
            else
                FileSourceOption.Validate();

            if (ImportTargetOption == null)
                throw new ArgumentNullException("ImportTargetOption cannot be null");
            else
                ImportTargetOption.Validate();

            if (DatabaseConnectOption == null)
                throw new ArgumentNullException("DatabaseConnectOption cannot be null");
            else
                DatabaseConnectOption.Validate();
        }
    }
}
