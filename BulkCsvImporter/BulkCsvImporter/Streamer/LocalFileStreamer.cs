using BulkCsvImporter.Abstract;
using BulkCsvImporter.Constant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Streamer
{
    class LocalFileStreamer : IStreamer
    {
        public Stream GetFileStream(FileSourceOption fileSourceOption)
        {
            var filePath = fileSourceOption.Options[FileSourceOptionKey.KEY_FILESOURCETYPE_LOCAL_FILEPATH];
            return File.Open(filePath, FileMode.Open);
        }
    }
}
