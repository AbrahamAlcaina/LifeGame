// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseAggregateRoot.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The base aggregate root.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Aggregate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The base aggregate root.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public class BaseAggregateRoot<TDomainEvent> : IEventProvider<TDomainEvent>, IRegisterChildEntities<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        #region Fields

        /// <summary>
        /// The applied events.
        /// </summary>
        private readonly List<TDomainEvent> appliedEvents;

        /// <summary>
        /// The child event providers.
        /// </summary>
        private readonly List<IEntityEventProvider<TDomainEvent>> childEventProviders;

        /// <summary>
        /// The registered events.
        /// </summary>
        private readonly Dictionary<Type, Action<TDomainEvent>> registeredEvents;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAggregateRoot{TDomainEvent}"/> class.
        /// </summary>
        public BaseAggregateRoot()
        {
            this.registeredEvents = new Dictionary<Type, Action<TDomainEvent>>();
            this.appliedEvents = new List<TDomainEvent>();
            this.childEventProviders = new List<IEntityEventProvider<TDomainEvent>>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the event version.
        /// </summary>
        public int EventVersion { get; protected set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public int Version { get; protected set; }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// The clear.
        /// </summary>
        void IEventProvider<TDomainEvent>.Clear()
        {
            this.childEventProviders.ForEach(x => x.Clear());
            this.appliedEvents.Clear();
        }

        /// <summary>
        /// The get changes.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<TDomainEvent> IEventProvider<TDomainEvent>.GetChanges()
        {
            return
                this.appliedEvents.Concat(this.GetChildEventsAndUpdateEventVersion()).OrderBy(x => x.Version).ToList();
        }

        /// <summary>
        /// The load from history.
        /// </summary>
        /// <param name="domainEvents">
        /// The domain events.
        /// </param>
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

        /// <summary>
        /// The update version.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        void IEventProvider<TDomainEvent>.UpdateVersion(int version)
        {
            this.Version = version;
        }

        /// <summary>
        /// The register child event provider.
        /// </summary>
        /// <param name="entityEventProvider">
        /// The entity event provider.
        /// </param>
        void IRegisterChildEntities<TDomainEvent>.RegisterChildEventProvider(
            IEntityEventProvider<TDomainEvent> entityEventProvider)
        {
            entityEventProvider.HookUpVersionProvider(this.GetNewEventVersion);
            this.childEventProviders.Add(entityEventProvider);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The apply.
        /// </summary>
        /// <param name="domainEvent">
        /// The domain event.
        /// </param>
        /// <typeparam name="TEvent">
        /// </typeparam>
        protected void Apply<TEvent>(TEvent domainEvent) where TEvent : class, TDomainEvent
        {
            domainEvent.AggregateId = this.Id;
            domainEvent.Version = this.GetNewEventVersion();
            this.apply(domainEvent.GetType(), domainEvent);
            this.appliedEvents.Add(domainEvent);
        }

        /// <summary>
        /// The register event.
        /// </summary>
        /// <param name="eventHandler">
        /// The event handler.
        /// </param>
        /// <typeparam name="TEvent">
        /// </typeparam>
        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, TDomainEvent
        {
            this.registeredEvents.Add(typeof(TEvent), theEvent => eventHandler(theEvent as TEvent));
        }

        /// <summary>
        /// The get child events and update event version.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<TDomainEvent> GetChildEventsAndUpdateEventVersion()
        {
            return this.childEventProviders.SelectMany(entity => entity.GetChanges());
        }

        /// <summary>
        /// The get new event version.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int GetNewEventVersion()
        {
            return ++this.EventVersion;
        }

        /// <summary>
        /// The apply.
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <param name="domainEvent">
        /// The domain event.
        /// </param>
        /// <exception cref="UnregisteredDomainEventException">
        /// </exception>
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