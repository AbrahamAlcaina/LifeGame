// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventStoreUnitOfWork.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The event store unit of work.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LifeGame.Bus;
    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    /// The event store unit of work.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public class EventStoreUnitOfWork<TDomainEvent> : IEventStoreUnitOfWork<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        #region Fields

        /// <summary>
        /// The _bus.
        /// </summary>
        private readonly IBus _bus;

        /// <summary>
        /// The _domain event storage.
        /// </summary>
        private readonly IDomainEventStorage<TDomainEvent> _domainEventStorage;

        /// <summary>
        /// The _event providers.
        /// </summary>
        private readonly List<IEventProvider<TDomainEvent>> _eventProviders;

        /// <summary>
        /// The _identity map.
        /// </summary>
        private readonly IIdentityMap<TDomainEvent> _identityMap;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStoreUnitOfWork{TDomainEvent}"/> class.
        /// </summary>
        /// <param name="domainEventStorage">
        /// The domain event storage.
        /// </param>
        /// <param name="identityMap">
        /// The identity map.
        /// </param>
        /// <param name="bus">
        /// The bus.
        /// </param>
        public EventStoreUnitOfWork(
            IDomainEventStorage<TDomainEvent> domainEventStorage, 
            IIdentityMap<TDomainEvent> identityMap, 
            IBus bus)
        {
            this._domainEventStorage = domainEventStorage;
            this._identityMap = identityMap;
            this._bus = bus;
            this._eventProviders = new List<IEventProvider<TDomainEvent>>();
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
            this.RegisterForTracking(aggregateRoot);
        }

        /// <summary>
        /// The commit.
        /// </summary>
        public void Commit()
        {
            this._domainEventStorage.BeginTransaction();

            foreach (var eventProvider in this._eventProviders)
            {
                this._domainEventStorage.Save(eventProvider);
                this._bus.Publish(eventProvider.GetChanges().Select(x => (object)x));
                eventProvider.Clear();
            }

            this._eventProviders.Clear();

            this._bus.Commit();
            this._domainEventStorage.Commit();
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
            var aggregateRoot = new TAggregate();

            this.LoadSnapShotIfExists(id, aggregateRoot);

            this.loadRemainingHistoryEvents(id, aggregateRoot);

            this.RegisterForTracking(aggregateRoot);

            return aggregateRoot;
        }

        /// <summary>
        /// The register for tracking.
        /// </summary>
        /// <param name="aggregateRoot">
        /// The aggregate root.
        /// </param>
        /// <typeparam name="TAggregate">
        /// </typeparam>
        public void RegisterForTracking<TAggregate>(TAggregate aggregateRoot)
            where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new()
        {
            this._eventProviders.Add(aggregateRoot);
            this._identityMap.Add(aggregateRoot);
        }

        /// <summary>
        /// The rollback.
        /// </summary>
        public void Rollback()
        {
            this._bus.Rollback();
            this._domainEventStorage.Rollback();
            foreach (var eventProvider in this._eventProviders)
            {
                this._identityMap.Remove(eventProvider.GetType(), eventProvider.Id);
            }

            this._eventProviders.Clear();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The load snap shot if exists.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="aggregateRoot">
        /// The aggregate root.
        /// </param>
        private void LoadSnapShotIfExists(Guid id, IOrginator aggregateRoot)
        {
            ISnapShot snapShot = this._domainEventStorage.GetSnapShot(id);
            if (snapShot == null)
            {
                return;
            }

            aggregateRoot.SetMemento(snapShot.Memento);
        }

        /// <summary>
        /// The load remaining history events.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="aggregateRoot">
        /// The aggregate root.
        /// </param>
        private void loadRemainingHistoryEvents(Guid id, IEventProvider<TDomainEvent> aggregateRoot)
        {
            IEnumerable<TDomainEvent> events = this._domainEventStorage.GetEventsSinceLastSnapShot(id);
            if (events.Count() > 0)
            {
                aggregateRoot.LoadFromHistory(events);
                return;
            }

            aggregateRoot.LoadFromHistory(this._domainEventStorage.GetAllEvents(id));
        }

        #endregion
    }
}