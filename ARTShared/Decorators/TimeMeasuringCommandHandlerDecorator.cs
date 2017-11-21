using System.Diagnostics;
using SkyStem.ART.Shared.Interfaces;

namespace SkyStem.ART.Shared.Decorators
{
    public class TimeMeasuringCommandHandlerDecorator : BaseCommandHandler
    {
        private readonly BaseCommandHandler _decoratee;
        private readonly ILogger _logger;

        public TimeMeasuringCommandHandlerDecorator(BaseCommandHandler decoratee, ILogger logger) :
            base(decoratee.GetType())
        {
            _logger = logger;
            _decoratee = decoratee;
        }

        protected override void HandleInternal()
        {
            var watch = Stopwatch.StartNew();
            _decoratee.Handle();
            _logger.LogInfo($"{Decoratee.Name} executed in {watch.ElapsedMilliseconds} ms.");
            watch.Stop();
        }
    }
}