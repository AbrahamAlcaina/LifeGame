// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEventProvider.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The EventProvider interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The EventProvider interface.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public interface IEventProvider<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        #region Public Properties

        /// <summary>
        /// Gets the id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        int Version { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The clear.
        /// </summary>
        void Clear();

        /// <summary>
        /// The get changes.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<TDomainEvent> GetChanges();

        /// <summary>
        /// The load from history.
        /// </summary>
        /// <param name="domainEvents">
        /// The domain events.
        /// </param>
        void LoadFromHistory(IEnumerable<TDomainEvent> domainEvents);

        /// <summary>
        /// The update version.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        void UpdateVersion(int version);

        #endregion
    }
}