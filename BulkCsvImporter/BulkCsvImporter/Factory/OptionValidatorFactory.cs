using BulkCsvImporter.Abstract;
using BulkCsvImporter.Constant;
using BulkCsvImporter.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Factory
{
    static class OptionValidatorFactory
    {
        public static IValidator Create(FileSourceOption fileSourceOption)
        {
            IValidator result = null;
            switch (fileSourceOption.FileSourceType)
            {
                case FileSourceType.Local:
                    result = new LocalFileSourceOptionValidator(fileSourceOption);
                    break;
                //case FileSourceType.Ftp:
                //    break;
                default:
                    throw new ArgumentOutOfRangeException("Not supported FileSourceType");
            }
            return result;
        }
    }
}
