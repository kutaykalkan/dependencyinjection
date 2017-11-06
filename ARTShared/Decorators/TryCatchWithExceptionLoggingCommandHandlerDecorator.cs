using System;
using SkyStem.ART.Shared.Interfaces;

namespace SkyStem.ART.Shared.Decorators
{
    public class TryCatchWithExceptionLoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _decorated;
        private readonly ILogger _logger;

        public TryCatchWithExceptionLoggingCommandHandlerDecorator(ICommandHandler<TCommand> decorated, ILogger logger)
        {
            _decorated = decorated;
            _logger = logger;
        }

        public void Handle(TCommand command)
        {
            try
            {
                _decorated.Handle(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw;
            }
        }
    }
}