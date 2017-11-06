using System.Threading.Tasks;
using SkyStem.ART.Service.APP.BLL;
using SkyStem.ART.Service.Interfaces;
using SkyStem.ART.Shared.Interfaces;

namespace DataImportTask.Implementations.CommandHandlers
{
    internal class GLDataImporterCommandHandler : ICommandHandler
    {
        private readonly ICacheService _cacheService;
        private readonly IGLDataImport _glDataImport;
        private readonly ILogger _logger;

        public GLDataImporterCommandHandler(ILogger logger, ICacheService cacheService, IGLDataImport glDataImport)
        {
            _logger = logger;
            _cacheService = cacheService;
            _glDataImport = glDataImport;
        }

        public void Handle()
        {
            _logger.LogInfo("GL Data Import Started.");
            var oDictConnectionString = _cacheService.GetDistinctDatabaseList();
            var options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 1;
            Parallel.ForEach(oDictConnectionString.Values, options, oCompanyUserInfo =>
            {
                var oGLDataImport = new GLDataImport(oCompanyUserInfo);
                if (oGLDataImport.IsProcessingRequiredForGLDataImport())
                    oGLDataImport.ProcessGLDataImport();
            });
            _logger.LogInfo("GL Data Import Ended.");
        }
    }
}