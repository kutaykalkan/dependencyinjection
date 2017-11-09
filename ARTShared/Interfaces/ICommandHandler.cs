using System;

namespace SkyStem.ART.Shared.Interfaces
{
    /// <summary>
    /// Data portion of the cqrs pattern.
    /// </summary>
    public interface ICommandHandler<in T>
    {
        void Handle(T command);
    }

    public interface ICommandHandler
    {
        void Handle();
    }
}
