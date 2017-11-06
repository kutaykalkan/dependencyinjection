namespace SkyStem.ART.Shared.Interfaces
{
    public interface IGLDataImport
    {
        bool IsProcessingRequiredForGLDataImport();
        void ProcessGLDataImport();
    }
}
