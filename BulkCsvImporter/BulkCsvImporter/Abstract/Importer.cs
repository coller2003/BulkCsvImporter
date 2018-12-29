using BulkCsvImporter.Model;

namespace BulkCsvImporter.Abstract
{
    public abstract class Importer
    {
        protected SingleFileImportOption _singleFileImportOption = null;

        public Importer(SingleFileImportOption singleFileImportOption)
        {
            _singleFileImportOption = singleFileImportOption;
            _singleFileImportOption.Validate();
        }

        public abstract void Import();
    }
}
