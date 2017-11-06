using System.Threading.Tasks;
using DataImportTask.Interfaces;
using SkyStem.ART.Service.APP.BLL;
using SkyStem.ART.Shared.Interfaces;

namespace DataImportTask.Implementations.CommandHandlers
{
    internal class GLDataImporterCommandHandler : ICommandHandler
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger _logger;

        public GLDataImporterCommandHandler(ILogger logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        public void Handle()
        {
            _logger.LogInfo("GL Data Import Started.");
            var oDictConnectionString = _cacheService.GetDistinctDatabaseList();
            ParallelOptions options = new ParallelOptions();
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