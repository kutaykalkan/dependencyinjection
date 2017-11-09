using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SkyStem.ART.Client.Interfaces;
using SkyStem.ART.Service.APP.BLL;
using SkyStem.ART.Shared.Interfaces;

namespace DataImportTask.Implementations.CommandHandlers
{
    internal class GLDataImporterCommandHandler : BaseCommandHandler
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger _logger;

        public GLDataImporterCommandHandler(ILogger logger, ICacheService cacheService) : base(
            typeof(GLDataImporterCommandHandler))
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        protected override void HandleInternal()
        {
            _logger.LogInfo("GL Data Import Started.");
            var oDictConnectionString = _cacheService.GetDistinctDatabaseList();

            var watch = Stopwatch.StartNew();
            Parallel.ForEach(oDictConnectionString.Values, oCompanyUserInfo =>
            {
                try
                {
                    var oGLDataImport = new GLDataImport(oCompanyUserInfo);
                    if (oGLDataImport.IsProcessingRequiredForGLDataImport())
                        oGLDataImport.ProcessGLDataImport();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"{oCompanyUserInfo.CompanyName}:{oCompanyUserInfo.CompanyID}");
                }
            });
            watch.Stop();
            _logger.LogInfo($"GL Data Import Ended in {watch.ElapsedMilliseconds}ms");
        }
    }
}