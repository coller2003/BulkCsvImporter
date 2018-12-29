using BulkCsvImporter.Abstract;
using BulkCsvImporter.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Validator
{
    internal class LocalFileSourceOptionValidator : OptionValidator
    {
        public LocalFileSourceOptionValidator(FileSourceOption fileSourceOption) : base(fileSourceOption)
        { }

        public override void Validate()
        {
            var result = new ActionResult();

            if (!(_fileSourceOption is LocalFileSourceOption))
            {
                throw new ArgumentException("The fileSourceOption should be instance of LocalFileSourceOption");
            }

            var localFileSourceOption = _fileSourceOption as LocalFileSourceOption;
            if (string.IsNullOrWhiteSpace(localFileSourceOption.FilePath))
            {
                throw new ArgumentNullException("The FilePath is not provided");
            }

            if (!File.Exists(localFileSourceOption.FilePath))
            {
                throw new FileNotFoundException("Cannot find file:" + localFileSourceOption.FilePath);
            }
        }
    }
}
