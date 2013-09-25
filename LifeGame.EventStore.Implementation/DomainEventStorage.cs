namespace LifeGame.EventStore.Implementation
{
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;
    using System.Transactions;

    using LifeGame.EventStore.Storage;

    using System;
    using System.Collections.Generic;

    using LifeGame.EventStore.Storage.Memento;

    using NEventStore;

    public class DomainEventStorage<TDomainEvent> : IDomainEventStorage<TDomainEvent>, IEnlistmentNotification
        where TDomainEvent : class, IDomainEvent
    {
        public ISnapShot GetSnapShot(Guid entityId)
        {
            var nSnapShot = this.EventStorage.Advanced.GetSnapshot(entityId, int.MaxValue);
            return new SnapShot(
                nSnapShot.StreamId,
                nSnapShot.StreamRevision,
                (IMemento)nSnapShot.Payload);
        }

        public void SaveShapShot(IEventProvider<TDomainEvent> entity)
        {
            var memento = ((IOrginator)entity).CreateMemento();
            var snapShot = new Snapshot(entity.Id, entity.Version, memento);
            this.EventStorage.Advanced.AddSnapshot(snapShot);
        }

        public void BeginTransaction()
        {
            if (this.TransactionScope != null) throw new AllReadyInTransactionException();
            this.TransactionScope = new TransactionScope();

        }

        internal TransactionScope TransactionScope { get; set; }

        public void Commit()
        {
            this.TransactionScope.Complete();
            this.TransactionScope.Dispose();
            this.TransactionScope = null;
        }

        public void Rollback()
        {
            this.TransactionScope.Dispose();
            this.TransactionScope = null;
        }

        public IEnumerable<TDomainEvent> GetAllEvents(Guid eventProviderId)
        {
            var stream = this.GetStream(eventProviderId);
            foreach (var committedEvent in stream.CommittedEvents)
            {
                yield return committedEvent.Body as TDomainEvent;
            }

            foreach (var committedEvent in stream.UncommittedEvents)
            {
                yield return committedEvent.Body as TDomainEvent;
            }
        }

        public IEnumerable<TDomainEvent> GetEventsSinceLastSnapShot(Guid eventProviderId)
        {
            var latestSnapshot = this.EventStorage.Advanced.GetSnapshot(eventProviderId, int.MaxValue);
            using (var stream = this.EventStorage.OpenStream(latestSnapshot, int.MaxValue))
            {
                foreach (var committedEvent in stream.CommittedEvents)
                {
                    yield return committedEvent.Body as TDomainEvent;
                }
            }
        }

        public int GetEventCountSinceLastSnapShot(Guid eventProviderId)
        {
            var latestSnapshot = this.EventStorage.Advanced.GetSnapshot(eventProviderId, int.MaxValue);
            using (var stream = this.EventStorage.OpenStream(latestSnapshot, int.MaxValue))
            {
                return stream.CommittedEvents.Count();
            }
        }

        public void Save(IEventProvider<TDomainEvent> eventProvider)
        {
            if (this.GetStream(eventProvider.Id).StreamRevision != eventProvider.Version)
            {
                throw new ConcurrencyViolationException();
            }
            using (var stream = this.GetStream(eventProvider.Id))
            {
                foreach (var domainEvent in eventProvider.GetChanges())
                {
                    stream.Add(new EventMessage { Body = domainEvent });
                }
                stream.CommitChanges(Guid.NewGuid());
            }
            eventProvider.UpdateVersion(eventProvider.GetChanges().Count());
        }

        public DomainEventStorage(IEventStoreBuilder builder)
        {
            this.EventStorage = builder.GetEventStore();
        }

        internal IStoreEvents EventStorage { get; set; }
        
        protected virtual IEventStream GetStream (Guid id)
        {
            if (this.EventStorage == null) throw new EventStorageNotInitializedException();
            return this.EventStorage.OpenStream(id, 0, int.MaxValue);
        }

        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            throw new NotImplementedException();
        }

        public void Commit(Enlistment enlistment)
        {
            throw new NotImplementedException();
        }

        public void Rollback(Enlistment enlistment)
        {
            throw new NotImplementedException();
        }

        public void InDoubt(Enlistment enlistment)
        {
            throw new NotImplementedException();
        }
    }
}
