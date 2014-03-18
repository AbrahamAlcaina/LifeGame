// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameCreatedEventHandler.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The game created event handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LifeGame.EventHandlers
{
    using LifeGame.Events;

    /// <summary>
    ///     The game created event handler.
    /// </summary>
    public class GameCreatedEventHandler : IEventHandler<GameCreated>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="event">
        /// The the event.
        /// </param>
        public void Handle(GameCreated @event)
        {
            // ADD new game to Reporting
            var s = 3;
        }

        #endregion
    }
}