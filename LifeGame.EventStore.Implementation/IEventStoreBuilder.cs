// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEventStoreBuilder.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The EventStoreBuilder interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Implementation
{
    using NEventStore;

    /// <summary>
    /// The EventStoreBuilder interface.
    /// </summary>
    public interface IEventStoreBuilder
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get event store.
        /// </summary>
        /// <returns>
        /// The <see cref="IStoreEvents"/>.
        /// </returns>
        IStoreEvents GetEventStore();

        #endregion
    }
}