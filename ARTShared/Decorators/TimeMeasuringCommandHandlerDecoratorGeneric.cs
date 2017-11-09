using System.Diagnostics;
using SkyStem.ART.Shared.Interfaces;

namespace SkyStem.ART.Shared.Decorators
{
    public class TimeMeasuringCommandHandlerDecoratorGeneric<T> : ICommandHandler<T>
    {
        private readonly ICommandHandler<T> _decoratee;
        private readonly ILogger _logger;

        public TimeMeasuringCommandHandlerDecoratorGeneric(ICommandHandler<T> decoratee, ILogger logger)
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
