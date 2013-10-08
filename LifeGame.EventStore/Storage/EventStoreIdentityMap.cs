// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventStoreIdentityMap.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The event store identity map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Storage
{
    using System;
    using System.Collections.Generic;

    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    /// The event store identity map.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public class EventStoreIdentityMap<TDomainEvent> : IIdentityMap<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        #region Fields

        /// <summary>
        /// The _identity map.
        /// </summary>
        private readonly Dictionary<Type, Dictionary<Guid, object>> _identityMap;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStoreIdentityMap{TDomainEvent}"/> class.
        /// </summary>
        public EventStoreIdentityMap()
        {
            this._identityMap = new Dictionary<Type, Dictionary<Guid, object>>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="aggregateRoot">
        /// The aggregate root.
        /// </param>
        /// <typeparam name="TAggregate">
        /// </typeparam>
        public void Add<TAggregate>(TAggregate aggregateRoot)
            where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new()
        {
            Dictionary<Guid, object> aggregates;
            if (!this._identityMap.TryGetValue(typeof(TAggregate), out aggregates))
            {
                aggregates = new Dictionary<Guid, object>();
                this._identityMap.Add(typeof(TAggregate), aggregates);
            }

            if (aggregates.ContainsKey(aggregateRoot.Id))
            {
                return;
            }

            aggregates.Add(aggregateRoot.Id, aggregateRoot);
        }

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
        public TAggregate GetById<TAggregate>(Guid id)
            where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new()
        {
            Dictionary<Guid, object> aggregates;
            if (!this._identityMap.TryGetValue(typeof(TAggregate), out aggregates))
            {
                return null;
            }

            object aggregate;
            if (!aggregates.TryGetValue(id, out aggregate))
            {
                return null;
            }

            return (TAggregate)aggregate;
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="aggregateRootType">
        /// The aggregate root type.
        /// </param>
        /// <param name="aggregateRootId">
        /// The aggregate root id.
        /// </param>
        public void Remove(Type aggregateRootType, Guid aggregateRootId)
        {
            Dictionary<Guid, object> aggregates;
            if (!this._identityMap.TryGetValue(aggregateRootType, out aggregates))
            {
                return;
            }

            aggregates.Remove(aggregateRootId);
        }

        #endregion
    }
}