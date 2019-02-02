using BulkCsvImporter.Model;
using BulkCsvImporter.Streamer;

namespace BulkCsvImporter.Abstract
{
    public abstract class Importer
    {
        protected IStreamer _streamer = null;
        protected SingleFileImportOption _singleFileImportOption = null;

        public Importer(SingleFileImportOption singleFileImportOption)
        {
            _singleFileImportOption = singleFileImportOption;
            _singleFileImportOption.Validate();
            _streamer = StreamerFactory.CreateInstance(singleFileImportOption.FileSourceOption.FileSourceType);
        }

        public abstract void Import();
    }
}
