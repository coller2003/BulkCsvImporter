using BulkCsvImporter.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Model
{
    public class ImportTagetOption : IOptionValidate
    {
        public ImportTagetOption()
        {
            KeyColumns = new List<string>();
            Columns = new List<string>();
        }

        public string TargetTableName { get; set; }
        public List<string> KeyColumns { get; set; }
        public List<string> Columns { get; set; }
        public bool NeedUpdate { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(TargetTableName))
            {
                throw new ArgumentNullException("ImportTargetOption.TargetTableName should not be null or empty");
            }

            if (Columns == null || Columns.Count <= 0)
            {
                throw new ArgumentNullException("ImportTargetOption.Columns should not be null or contains nothing");
            }

            if (NeedUpdate && (KeyColumns == null || KeyColumns.Count <= 0))
            {
                throw new ArgumentNullException("When NeedUpdate is True, ImportTargetOption.KeyColumns should not be null or contains nothing");
            }
        }
    }
}
