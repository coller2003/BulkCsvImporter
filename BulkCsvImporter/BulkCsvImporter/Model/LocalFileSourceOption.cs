using BulkCsvImporter.Abstract;
using BulkCsvImporter.Constant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Model
{
    public class LocalFileSourceOption : FileSourceOption
    {
        public LocalFileSourceOption(string filePath) : base()
        {
            FileSourceType = Constant.FileSourceType.Local;
            FilePath = filePath;
        }

        public string FilePath
        {
            get
            {
                return Options[FileSourceOptionKey.KEY_FILESOURCETYPE_LOCAL_FILEPATH];
            }
            set
            {
                Options[FileSourceOptionKey.KEY_FILESOURCETYPE_LOCAL_FILEPATH] = value;
            }
        }

        public override void Validate()
        {
            var result = new ActionResult();

            if (string.IsNullOrWhiteSpace(this.FilePath))
            {
                throw new ArgumentNullException("The FilePath is not provided");
            }

            if (!this.FilePath.EndsWith(".csv"))
            {
                throw new ArgumentException("The specified file is not in CSV format");
            }

            if (!File.Exists(this.FilePath))
            {
                throw new FileNotFoundException("Cannot find file:" + this.FilePath);
            }
        }
    }
}
