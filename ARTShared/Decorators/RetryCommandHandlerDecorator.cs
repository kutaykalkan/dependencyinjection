using System;
using System.Threading;
using SkyStem.ART.Shared.Interfaces;

namespace SkyStem.ART.Shared.Decorators
{
    public class RetryCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _decorated;

        public RetryCommandHandlerDecorator(ICommandHandler<TCommand> decorated)
        {
            _decorated = decorated;
        }

        public void Handle(TCommand command)
        {
            HandleWithCountDown(command, 5);
        }

        private void HandleWithCountDown(TCommand command, int count)
        {
            try
            {
                _decorated.Handle(command);
            }
            catch (Exception)
            {
                if (count <= 0)
                    throw;

                Thread.Sleep(3000);

                HandleWithCountDown(command, count - 1);
            }
        }
    }
}