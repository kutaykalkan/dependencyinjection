using System.Diagnostics;
using SkyStem.ART.Shared.Interfaces;

namespace SkyStem.ART.Shared.Decorators
{
    public class TimeMeasuringCommandHandlerDecorator<T> : ICommandHandler<T>
    {
        private readonly ICommandHandler<T> _decoratee;
        private readonly ILogger _logger;

        public TimeMeasuringCommandHandlerDecorator(ICommandHandler<T> decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
        }

        public void Handle(T command)
        {
            var watch = Stopwatch.StartNew();
            _decoratee.Handle(command);
            _logger.LogInfo($"{command.GetType().Name} executed in {watch.ElapsedMilliseconds} ms.");
        }
    }
}
