using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Abstract
{
    public interface IStreamer
    {
        Stream GetFileStream(FileSourceOption fileSourceOption);
    }
}
