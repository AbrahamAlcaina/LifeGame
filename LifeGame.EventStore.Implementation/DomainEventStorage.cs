namespace LifeGame.EventStore.Implementation
{
    using System.ComponentModel;
    using System.Linq;

    using LifeGame.EventStore.Storage;

    using System;
    using System.Collections.Generic;

    using NEventStore;

    public class DomainEventStorage<TDomainEvent> : IDomainEventStorage<TDomainEvent> 
        where TDomainEvent : class, IDomainEvent
    {
        public ISnapShot GetSnapShot(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public void SaveShapShot(IEventProvider<TDomainEvent> entity)
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public int GetEventCountSinceLastSnapShot(Guid eventProviderId)
        {
            throw new NotImplementedException();
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
        
        private IEventStream GetStream (Guid id)
        {
            if (this.EventStorage == null) throw new EventStorageNotInitializedException();
            return this.EventStorage.OpenStream(id, 0, int.MaxValue);
        }

    }
}
