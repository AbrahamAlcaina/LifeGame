// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainEventStorage.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The domain event storage.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Transactions;

    using LifeGame.EventStore.Storage;
    using LifeGame.EventStore.Storage.Memento;

    using NEventStore;

    /// <summary>
    /// The domain event storage.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public class DomainEventStorage<TDomainEvent> : IDomainEventStorage<TDomainEvent>
        where TDomainEvent : class, IDomainEvent
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventStorage{TDomainEvent}"/> class.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        public DomainEventStorage(IEventStoreBuilder builder)
        {
            this.EventStorage = builder.GetEventStore();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the event storage.
        /// </summary>
        internal IStoreEvents EventStorage { get; set; }

        /// <summary>
        /// Gets or sets the transaction scope.
        /// </summary>
        internal TransactionScope TransactionScope { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <exception cref="AllReadyInTransactionException">
        /// </exception>
        public void BeginTransaction()
        {
            if (this.TransactionScope != null)
            {
                throw new AllReadyInTransactionException();
            }

            this.TransactionScope = new TransactionScope();
        }

        /// <summary>
        /// The commit.
        /// </summary>
        public void Commit()
        {
            this.TransactionScope.Complete();
            this.TransactionScope.Dispose();
            this.TransactionScope = null;
        }

        /// <summary>
        /// The get all events.
        /// </summary>
        /// <param name="eventProviderId">
        /// The event provider id.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<TDomainEvent> GetAllEvents(Guid eventProviderId)
        {
            IEventStream stream = this.GetStream(eventProviderId);
            foreach (EventMessage committedEvent in stream.CommittedEvents)
            {
                yield return committedEvent.Body as TDomainEvent;
            }

            foreach (EventMessage committedEvent in stream.UncommittedEvents)
            {
                yield return committedEvent.Body as TDomainEvent;
            }
        }

        /// <summary>
        /// The get event count since last snap shot.
        /// </summary>
        /// <param name="eventProviderId">
        /// The event provider id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetEventCountSinceLastSnapShot(Guid eventProviderId)
        {
            Snapshot latestSnapshot = this.EventStorage.Advanced.GetSnapshot(eventProviderId, int.MaxValue);
            using (IEventStream stream = this.EventStorage.OpenStream(latestSnapshot, int.MaxValue))
            {
                return stream.CommittedEvents.Count();
            }
        }

        /// <summary>
        /// The get events since last snap shot.
        /// </summary>
        /// <param name="eventProviderId">
        /// The event provider id.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<TDomainEvent> GetEventsSinceLastSnapShot(Guid eventProviderId)
        {
            Snapshot latestSnapshot = this.EventStorage.Advanced.GetSnapshot(eventProviderId, int.MaxValue);
            using (IEventStream stream = this.EventStorage.OpenStream(latestSnapshot, int.MaxValue))
            {
                foreach (EventMessage committedEvent in stream.CommittedEvents)
                {
                    yield return committedEvent.Body as TDomainEvent;
                }
            }
        }

        /// <summary>
        /// The get snap shot.
        /// </summary>
        /// <param name="entityId">
        /// The entity id.
        /// </param>
        /// <returns>
        /// The <see cref="ISnapShot"/>.
        /// </returns>
        public ISnapShot GetSnapShot(Guid entityId)
        {
            Snapshot nSnapShot = this.EventStorage.Advanced.GetSnapshot(entityId, int.MaxValue);
            return new SnapShot(nSnapShot.StreamId, nSnapShot.StreamRevision, (IMemento)nSnapShot.Payload);
        }

        /// <summary>
        /// The rollback.
        /// </summary>
        public void Rollback()
        {
            this.TransactionScope.Dispose();
            this.TransactionScope = null;
        }

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="eventProvider">
        /// The event provider.
        /// </param>
        /// <exception cref="ConcurrencyViolationException">
        /// </exception>
        public void Save(IEventProvider<TDomainEvent> eventProvider)
        {
            if (this.GetStream(eventProvider.Id).StreamRevision != eventProvider.Version)
            {
                throw new ConcurrencyViolationException();
            }

            using (IEventStream stream = this.GetStream(eventProvider.Id))
            {
                foreach (TDomainEvent domainEvent in eventProvider.GetChanges())
                {
                    stream.Add(new EventMessage { Body = domainEvent });
                }

                stream.CommitChanges(Guid.NewGuid());
            }

            eventProvider.UpdateVersion(eventProvider.GetChanges().Count());
        }

        /// <summary>
        /// The save shap shot.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public void SaveShapShot(IEventProvider<TDomainEvent> entity)
        {
            IMemento memento = ((IOrginator)entity).CreateMemento();
            var snapShot = new Snapshot(entity.Id, entity.Version, memento);
            this.EventStorage.Advanced.AddSnapshot(snapShot);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get stream.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="IEventStream"/>.
        /// </returns>
        /// <exception cref="EventStorageNotInitializedException">
        /// </exception>
        protected virtual IEventStream GetStream(Guid id)
        {
            if (this.EventStorage == null)
            {
                throw new EventStorageNotInitializedException();
            }

            return this.EventStorage.OpenStream(id, 0, int.MaxValue);
        }

        #endregion
    }
}