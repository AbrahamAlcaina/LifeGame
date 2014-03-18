// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDomainRepository.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The DomainRepository interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore
{
    using System;

    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    ///     The DomainRepository interface.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public interface IDomainRepository<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The add.
        /// </summary>
        /// <param name="aggregateRoot">
        ///     The aggregate root.
        /// </param>
        /// <typeparam name="TAggregate">
        /// </typeparam>
        void Add<TAggregate>(TAggregate aggregateRoot)
            where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new();

        /// <summary>
        ///     The get by id.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <typeparam name="TAggregate">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="TAggregate" />.
        /// </returns>
        TAggregate GetById<TAggregate>(Guid id)
            where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new();

        #endregion
    }
}