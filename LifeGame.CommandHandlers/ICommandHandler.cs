// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandHandler.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The CommandHandler interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.CommandHandlers
{
    using LifeGame.EventStore;

    /// <summary>
    ///     The CommandHandler interface.
    /// </summary>
    /// <typeparam name="TCommand">
    /// </typeparam>
    public interface ICommandHandler<in TCommand> : IHandler<TCommand>
        where TCommand : class
    {
    }
}