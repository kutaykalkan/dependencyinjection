using SkyStem.ART.Service.Interfaces;

namespace DataImportTask.Implementations.Proxies
{
    public class GLDataImportProxy : IGLDataImport
    {
        private readonly IGLDataImport _glDataImport;

        public GLDataImportProxy(IGLDataImport glDataImport)
        {
            _glDataImport = glDataImport;
        }
        public bool IsProcessingRequiredForGLDataImport()
        {
            return _glDataImport.IsProcessingRequiredForGLDataImport();
        }

        public void ProcessGLDataImport()
        {
            _glDataImport.ProcessGLDataImport();
        }
    }
}