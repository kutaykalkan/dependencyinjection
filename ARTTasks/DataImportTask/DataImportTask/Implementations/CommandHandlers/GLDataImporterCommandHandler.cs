using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SkyStem.ART.Client.Interfaces;
using SkyStem.ART.Service.APP.BLL;
using SkyStem.ART.Shared.Interfaces;

namespace DataImportTask.Implementations.CommandHandlers
{
    /// <summary>
    /// Decoraters Wrapped: TimeMeasuringCommandHandlerDecorator.
    /// </summary>
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
            var oDictConnectionString = _cacheService.GetDistinctDatabaseList();
            Parallel.ForEach(oDictConnectionString.Values, oCompanyUserInfo =>
            {
                try
                {
                    var oGLDataImport = new GLDataImport(oCompanyUserInfo);
                    if (oGLDataImport.IsProcessingRequiredForGLDataImport())
                    {
                        _logger.LogInfo($"GL Data Import Started for Company: {oCompanyUserInfo.CompanyName} | CompanyId:{oCompanyUserInfo.CompanyID}");
                        oGLDataImport.ProcessGLDataImport();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"{oCompanyUserInfo.CompanyName}:{oCompanyUserInfo.CompanyID}");
                }
            });
        }
    }
}