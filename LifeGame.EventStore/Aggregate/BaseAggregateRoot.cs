namespace LifeGame.EventStore.Aggregate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BaseAggregateRoot<TDomainEvent> : IEventProvider<TDomainEvent>, IRegisterChildEntities<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        #region Fields

        private readonly List<TDomainEvent> appliedEvents;

        private readonly List<IEntityEventProvider<TDomainEvent>> childEventProviders;

        private readonly Dictionary<Type, Action<TDomainEvent>> registeredEvents;

        #endregion

        #region Constructors and Destructors

        public BaseAggregateRoot()
        {
            this.registeredEvents = new Dictionary<Type, Action<TDomainEvent>>();
            this.appliedEvents = new List<TDomainEvent>();
            this.childEventProviders = new List<IEntityEventProvider<TDomainEvent>>();
        }

        #endregion

        #region Public Properties

        public int EventVersion { get; protected set; }

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        #endregion

        #region Explicit Interface Methods

        void IEventProvider<TDomainEvent>.Clear()
        {
            this.childEventProviders.ForEach(x => x.Clear());
            this.appliedEvents.Clear();
        }

        IEnumerable<TDomainEvent> IEventProvider<TDomainEvent>.GetChanges()
        {
            return
                this.appliedEvents.Concat(this.GetChildEventsAndUpdateEventVersion()).OrderBy(x => x.Version).ToList();
        }

        void IEventProvider<TDomainEvent>.LoadFromHistory(IEnumerable<TDomainEvent> domainEvents)
        {
            if (!domainEvents.Any())
            {
                return;
            }

            foreach (TDomainEvent domainEvent in domainEvents)
            {
                this.apply(domainEvent.GetType(), domainEvent);
            }

            this.Version = domainEvents.Last().Version;
            this.EventVersion = this.Version;
        }

        void IEventProvider<TDomainEvent>.UpdateVersion(int version)
        {
            this.Version = version;
        }

        void IRegisterChildEntities<TDomainEvent>.RegisterChildEventProvider(
            IEntityEventProvider<TDomainEvent> entityEventProvider)
        {
            entityEventProvider.HookUpVersionProvider(this.GetNewEventVersion);
            this.childEventProviders.Add(entityEventProvider);
        }

        #endregion

        #region Methods

        protected void Apply<TEvent>(TEvent domainEvent) where TEvent : class, TDomainEvent
        {
            domainEvent.AggregateId = this.Id;
            domainEvent.Version = this.GetNewEventVersion();
            this.apply(domainEvent.GetType(), domainEvent);
            this.appliedEvents.Add(domainEvent);
        }

        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, TDomainEvent
        {
            this.registeredEvents.Add(typeof(TEvent), theEvent => eventHandler(theEvent as TEvent));
        }

        private IEnumerable<TDomainEvent> GetChildEventsAndUpdateEventVersion()
        {
            return this.childEventProviders.SelectMany(entity => entity.GetChanges());
        }

        private int GetNewEventVersion()
        {
            return ++this.EventVersion;
        }

        private void apply(Type eventType, TDomainEvent domainEvent)
        {
            Action<TDomainEvent> handler;

            if (!this.registeredEvents.TryGetValue(eventType, out handler))
            {
                throw new UnregisteredDomainEventException(
                    string.Format(
                        "The requested domain event '{0}' is not registered in '{1}'",
                        eventType.FullName,
                        this.GetType().FullName));
            }

            handler(domainEvent);
        }

        #endregion
    }
}