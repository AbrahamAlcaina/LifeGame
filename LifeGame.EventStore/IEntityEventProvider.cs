// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntityEventProvider.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The EntityEventProvider interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The EntityEventProvider interface.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public interface IEntityEventProvider<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        #region Public Properties

        /// <summary>
        ///     Gets the id.
        /// </summary>
        Guid Id { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The clear.
        /// </summary>
        void Clear();

        /// <summary>
        ///     The get changes.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<TDomainEvent> GetChanges();

        /// <summary>
        ///     The hook up version provider.
        /// </summary>
        /// <param name="versionProvider">
        ///     The version provider.
        /// </param>
        void HookUpVersionProvider(Func<int> versionProvider);

        /// <summary>
        ///     The load from history.
        /// </summary>
        /// <param name="domainEvents">
        ///     The domain events.
        /// </param>
        void LoadFromHistory(IEnumerable<TDomainEvent> domainEvents);

        #endregion
    }
}