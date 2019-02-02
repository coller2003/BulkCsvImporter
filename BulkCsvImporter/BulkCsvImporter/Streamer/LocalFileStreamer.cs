using BulkCsvImporter.Abstract;
using BulkCsvImporter.Constant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Streamer
{
    /// <summary>
    /// The local file streamer
    /// </summary>
    class LocalFileStreamer : IStreamer
    {
        /// <summary>
        /// Get stream of specified local file.
        /// </summary>
        /// <param name="fileSourceOption">A local file source option</param>
        /// <returns>Stream</returns>
        public Stream GetFileStream(FileSourceOption fileSourceOption)
        {
            var filePath = fileSourceOption.Options[FileSourceOptionKey.KEY_FILESOURCETYPE_LOCAL_FILEPATH];
            return File.Open(filePath, FileMode.Open);
        }
    }
}
