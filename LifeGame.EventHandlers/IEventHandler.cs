// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEventHandler.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The EventHandler interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventHandlers
{
    using LifeGame.EventStore;

    /// <summary>
    ///     The EventHandler interface.
    /// </summary>
    /// <typeparam name="TEvent">
    /// </typeparam>
    public interface IEventHandler<in TEvent> : IHandler<TEvent>
        where TEvent : class
    {
    }
}