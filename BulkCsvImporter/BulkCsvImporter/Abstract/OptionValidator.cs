using BulkCsvImporter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Abstract
{
    public abstract class OptionValidator
    {
        protected FileSourceOption _fileSourceOption = null;

        public OptionValidator(FileSourceOption fileSourceOption)
        {
            _fileSourceOption = fileSourceOption;
        }

        public abstract void Validate();
    }
}
