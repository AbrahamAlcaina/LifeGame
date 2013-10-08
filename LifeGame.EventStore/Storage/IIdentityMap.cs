// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIdentityMap.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The IdentityMap interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Storage
{
    using System;

    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    /// The IdentityMap interface.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public interface IIdentityMap<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="aggregateRoot">
        /// The aggregate root.
        /// </param>
        /// <typeparam name="TAggregate">
        /// </typeparam>
        void Add<TAggregate>(TAggregate aggregateRoot)
            where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new();

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <typeparam name="TAggregate">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TAggregate"/>.
        /// </returns>
        TAggregate GetById<TAggregate>(Guid id)
            where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new();

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="aggregateRootType">
        /// The aggregate root type.
        /// </param>
        /// <param name="aggregateRootId">
        /// The aggregate root id.
        /// </param>
        void Remove(Type aggregateRootType, Guid aggregateRootId);

        #endregion
    }
}