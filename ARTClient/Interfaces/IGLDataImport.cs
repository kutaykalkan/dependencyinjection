namespace SkyStem.ART.Client.Interfaces
{
    public interface IGLDataImport
    {
        bool IsProcessingRequiredForGLDataImport();
        void ProcessGLDataImport();
    }
}