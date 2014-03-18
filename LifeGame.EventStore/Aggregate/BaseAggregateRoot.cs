// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseAggregateRoot.cs" company="Abraham Alcaina">
//   AAA Code
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
    ///     The base aggregate root.
    /// </summary>
    /// <typeparam name="TDomainEntity">
    ///     Kind of entity
    /// </typeparam>
    public class BaseAggregateRoot<TDomainEntity> : IEventProvider<TDomainEntity>, IRegisterChildEntities<TDomainEntity>
        where TDomainEntity : IDomainEvent
    {
        #region Fields

        /// <summary>
        ///     The applied events.
        /// </summary>
        private readonly List<TDomainEntity> appliedEvents;

        /// <summary>
        ///     The child event providers.
        /// </summary>
        private readonly List<IEntityEventProvider<TDomainEntity>> childEventProviders;

        /// <summary>
        ///     The registered events.
        /// </summary>
        private readonly Dictionary<Type, Action<TDomainEntity>> registeredEvents;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAggregateRoot{TDomainEntity}" /> class.
        /// </summary>
        public BaseAggregateRoot()
        {
            this.registeredEvents = new Dictionary<Type, Action<TDomainEntity>>();
            this.appliedEvents = new List<TDomainEntity>();
            this.childEventProviders = new List<IEntityEventProvider<TDomainEntity>>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the event version.
        /// </summary>
        public int EventVersion { get; protected set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        public int Version { get; protected set; }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        ///     The clear.
        /// </summary>
        void IEventProvider<TDomainEntity>.Clear()
        {
            this.childEventProviders.ForEach(x => x.Clear());
            this.appliedEvents.Clear();
        }

        /// <summary>
        ///     The get changes.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<TDomainEntity> IEventProvider<TDomainEntity>.GetChanges()
        {
            return
                this.appliedEvents.Concat(this.GetChildEventsAndUpdateEventVersion()).OrderBy(x => x.Version).ToList();
        }

        /// <summary>
        ///     The load from history.
        /// </summary>
        /// <param name="domainEvents">
        ///     The domain events.
        /// </param>
        void IEventProvider<TDomainEntity>.LoadFromHistory(IEnumerable<TDomainEntity> domainEvents)
        {
            if (!domainEvents.Any())
            {
                return;
            }

            foreach (TDomainEntity domainEvent in domainEvents)
            {
                this.apply(domainEvent.GetType(), domainEvent);
            }

            this.Version += domainEvents.Count();
            this.EventVersion = this.Version;
        }

        /// <summary>
        ///     The update version.
        /// </summary>
        /// <param name="version">
        ///     The version.
        /// </param>
        void IEventProvider<TDomainEntity>.UpdateVersion(int version)
        {
            this.Version = version;
        }

        /// <summary>
        ///     The register child event provider.
        /// </summary>
        /// <param name="entityEventProvider">
        ///     The entity event provider.
        /// </param>
        void IRegisterChildEntities<TDomainEntity>.RegisterChildEventProvider(
            IEntityEventProvider<TDomainEntity> entityEventProvider)
        {
            entityEventProvider.HookUpVersionProvider(this.GetNewEventVersion);
            this.childEventProviders.Add(entityEventProvider);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The apply.
        /// </summary>
        /// <param name="domainEvent">
        ///     The domain event.
        /// </param>
        /// <typeparam name="TEvent">
        /// </typeparam>
        protected void Apply<TEvent>(TEvent domainEvent) where TEvent : class, TDomainEntity
        {
            domainEvent.AggregateId = this.Id;
            domainEvent.Version = this.GetNewEventVersion();
            this.apply(domainEvent.GetType(), domainEvent);
            this.appliedEvents.Add(domainEvent);
        }

        /// <summary>
        ///     The register event.
        /// </summary>
        /// <param name="eventHandler">
        ///     The event handler.
        /// </param>
        /// <typeparam name="TEvent">
        /// </typeparam>
        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, TDomainEntity
        {
            this.registeredEvents.Add(typeof(TEvent), theEvent => eventHandler(theEvent as TEvent));
        }

        /// <summary>
        ///     The get child events and update event version.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        private IEnumerable<TDomainEntity> GetChildEventsAndUpdateEventVersion()
        {
            return this.childEventProviders.SelectMany(entity => entity.GetChanges());
        }

        /// <summary>
        ///     The get new event version.
        /// </summary>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        private int GetNewEventVersion()
        {
            return ++this.EventVersion;
        }

        /// <summary>
        ///     The apply.
        /// </summary>
        /// <param name="eventType">
        ///     The event type.
        /// </param>
        /// <param name="domainEvent">
        ///     The domain event.
        /// </param>
        /// <exception cref="UnregisteredDomainEventException">
        /// </exception>
        private void apply(Type eventType, TDomainEntity domainEvent)
        {
            Action<TDomainEntity> handler;

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