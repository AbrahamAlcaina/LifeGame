// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDomainEventStorage.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The DomainEventStorage interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Storage
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The DomainEventStorage interface.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public interface IDomainEventStorage<TDomainEvent> : ISnapShotStorage<TDomainEvent>, ITransactional
        where TDomainEvent : IDomainEvent
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get all events.
        /// </summary>
        /// <param name="eventProviderId">
        ///     The event provider id.
        /// </param>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<TDomainEvent> GetAllEvents(Guid eventProviderId);

        /// <summary>
        ///     The get event count since last snap shot.
        /// </summary>
        /// <param name="eventProviderId">
        ///     The event provider id.
        /// </param>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        int GetEventCountSinceLastSnapShot(Guid eventProviderId);

        /// <summary>
        ///     The get events since last snap shot.
        /// </summary>
        /// <param name="eventProviderId">
        ///     The event provider id.
        /// </param>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<TDomainEvent> GetEventsSinceLastSnapShot(Guid eventProviderId);

        /// <summary>
        ///     The save.
        /// </summary>
        /// <param name="eventProvider">
        ///     The event provider.
        /// </param>
        void Save(IEventProvider<TDomainEvent> eventProvider);

        #endregion
    }
}