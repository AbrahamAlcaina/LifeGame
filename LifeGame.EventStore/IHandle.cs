// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHandle.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The Handler interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore
{
    /// <summary>
    ///     The Handler interface.
    /// </summary>
    /// <typeparam name="TEvent">
    /// </typeparam>
    public interface IHandler<in TEvent>
        where TEvent : class
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The handle.
        /// </summary>
        /// <param name="theEvent">
        ///     The the event.
        /// </param>
        void Handle(TEvent theEvent);

        #endregion
    }
}