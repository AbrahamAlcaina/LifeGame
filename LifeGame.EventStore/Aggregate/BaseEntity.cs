// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEntity.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The base entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Aggregate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The base entity.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public class BaseEntity<TDomainEvent> : IEntityEventProvider<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        #region Fields

        /// <summary>
        /// The _applied events.
        /// </summary>
        private readonly List<TDomainEvent> _appliedEvents;

        /// <summary>
        /// The _events.
        /// </summary>
        private readonly Dictionary<Type, Action<TDomainEvent>> _events;

        /// <summary>
        /// The _version provider.
        /// </summary>
        private Func<int> _versionProvider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity{TDomainEvent}"/> class.
        /// </summary>
        public BaseEntity()
        {
            this._events = new Dictionary<Type, Action<TDomainEvent>>();
            this._appliedEvents = new List<TDomainEvent>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; protected set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The hook up version provider.
        /// </summary>
        /// <param name="versionProvider">
        /// The version provider.
        /// </param>
        public void HookUpVersionProvider(Func<int> versionProvider)
        {
            this._versionProvider = versionProvider;
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// The clear.
        /// </summary>
        void IEntityEventProvider<TDomainEvent>.Clear()
        {
            this._appliedEvents.Clear();
        }

        /// <summary>
        /// The get changes.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<TDomainEvent> IEntityEventProvider<TDomainEvent>.GetChanges()
        {
            return this._appliedEvents;
        }

        /// <summary>
        /// The load from history.
        /// </summary>
        /// <param name="domainEvents">
        /// The domain events.
        /// </param>
        void IEntityEventProvider<TDomainEvent>.LoadFromHistory(IEnumerable<TDomainEvent> domainEvents)
        {
            if (domainEvents.Count() == 0)
            {
                return;
            }

            foreach (TDomainEvent domainEvent in domainEvents)
            {
                this.apply(domainEvent.GetType(), domainEvent);
            }
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
            domainEvent.Version = this._versionProvider();
            this.apply(domainEvent.GetType(), domainEvent);
            this._appliedEvents.Add(domainEvent);
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
            this._events.Add(typeof(TEvent), theEvent => eventHandler(theEvent as TEvent));
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

            if (!this._events.TryGetValue(eventType, out handler))
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