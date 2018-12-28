using BulkCsvImporter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Abstract
{
    public abstract class FileSourceOptionValidator
    {
        private FtpFileSourceOption _ftpFileSourceOption = null;

        public FileSourceOptionValidator(FtpFileSourceOption ftpFileSourceOption)
        {
            _ftpFileSourceOption = ftpFileSourceOption;
        }

        public abstract ActionResult Validate();
    }
}
