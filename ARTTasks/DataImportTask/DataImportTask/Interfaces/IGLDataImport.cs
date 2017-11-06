namespace DataImportTask.Interfaces
{
    public interface IGLDataImport
    {
        bool IsProcessingRequiredForGLDataImport();
        void ProcessGLDataImport();
    }
}
