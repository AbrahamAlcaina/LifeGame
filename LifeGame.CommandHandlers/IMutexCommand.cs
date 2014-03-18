// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMutexCommand.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The MutexCommandHandler interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.CommandHandlers
{
    /// <summary>
    ///     The MutexCommandHandler interface.
    /// </summary>
    /// <typeparam name="TCommand">
    /// </typeparam>
    public interface IMutexCommandHandler<in TCommand> : ICommandHandler<TCommand>
        where TCommand : class
    {
    }
}