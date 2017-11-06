namespace SkyStem.ART.Service.Interfaces
{
    public interface IGLDataImport
    {
        bool IsProcessingRequiredForGLDataImport();
        void ProcessGLDataImport();
    }
}