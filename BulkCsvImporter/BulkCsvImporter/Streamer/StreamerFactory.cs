using BulkCsvImporter.Abstract;
using BulkCsvImporter.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Streamer
{
    /// <summary>
    /// Simple factory for Streamer
    /// </summary>
    static class StreamerFactory
    {
        /// <summary>
        /// Create an IStreamer instance
        /// </summary>
        /// <param name="fileSourceType">The file source type of streamer</param>
        /// <returns>IStreamer instance</returns>
        public static IStreamer CreateInstance(FileSourceType fileSourceType)
        {
            IStreamer result = null;
            switch (fileSourceType)
            {
                case FileSourceType.Local:
                    result = new LocalFileStreamer();
                    break;
                //case FileSourceType.Ftp:
                //    break;
                default:
                    throw new NotImplementedException();
            }

            return result;
        }
    }
}
