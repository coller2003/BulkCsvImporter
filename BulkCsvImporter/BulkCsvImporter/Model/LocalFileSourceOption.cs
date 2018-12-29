using BulkCsvImporter.Abstract;
using BulkCsvImporter.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Model
{
    public class LocalFileSourceOption : FileSourceOption
    {
        public LocalFileSourceOption(string filePath) : base()
        {
            FileSourceType = Constant.FileSourceTypeEnum.Local;
            FilePath = filePath;
        }

        public string FilePath
        {
            get
            {
                return Options[FileSourceOptionKeyConst.KEY_FILESOURCETYPE_LOCAL_FILEPATH];
            }
            set
            {
                Options[FileSourceOptionKeyConst.KEY_FILESOURCETYPE_LOCAL_FILEPATH] = value;
            }
        }
    }
}
