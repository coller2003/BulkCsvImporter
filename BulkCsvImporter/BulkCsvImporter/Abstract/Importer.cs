using BulkCsvImporter.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Abstract
{
    public abstract class Importer
    {
        protected FileSourceOption _fileSourceOption = null;
        protected OptionValidator _optionValidator = null;

        public Importer(FileSourceOption fileSourceOption)
        {
            _fileSourceOption = fileSourceOption;
            _optionValidator = OptionValidatorFactory.Create(_fileSourceOption);
            _optionValidator.Validate();
        }
    }
}
