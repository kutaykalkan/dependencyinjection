using System;

namespace SkyStem.ART.Shared.Interfaces
{
    public abstract class BaseCommandHandler : ICommandHandler, IDecorator
    {
        protected readonly Type Decoratee;
        protected BaseCommandHandler(Type decoratee)
        {
            Decoratee = decoratee;
        }

        protected abstract void HandleInternal();


        public void Handle()
        {
            HandleInternal();
        }

        public Type DecoratedType => Decoratee;
    }
}
