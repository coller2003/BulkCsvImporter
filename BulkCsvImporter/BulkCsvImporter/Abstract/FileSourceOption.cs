using BulkCsvImporter.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Abstract
{
    public abstract class FileSourceOption : IOptionValidate
    {
        public FileSourceType FileSourceType { get; set; }

        public Dictionary<string, string> Options { get; set; }

        public FileSourceOption()
        {
            Options = new Dictionary<string, string>();

            if (FileSourceType == FileSourceType.Local)
            {
                Options.Add(FileSourceOptionKey.KEY_FILESOURCETYPE_LOCAL_FILEPATH, string.Empty);
            }
            else if (FileSourceType == FileSourceType.Ftp)
            {
                Options.Add(FileSourceOptionKey.KEY_FILESOURCETYPE_FTP_HOST, string.Empty);
                Options.Add(FileSourceOptionKey.KEY_FILESOURCETYPE_FTP_USERNAME, string.Empty);
                Options.Add(FileSourceOptionKey.KEY_FILESOURCETYPE_FTP_PASSWORD, string.Empty);
            }
        }

        public abstract void Validate();
    }
}
