// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainEvent.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The domain event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Events
{
    using System;

    using LifeGame.EventStore;

    /// <summary>
    /// The domain event.
    /// </summary>
    [Serializable]
    public class DomainEvent : IDomainEvent
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEvent"/> class.
        /// </summary>
        public DomainEvent()
        {
            this.Id = Guid.NewGuid();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the aggregate id.
        /// </summary>
        public Guid AggregateId { get; set; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; private set; }

        #endregion

        #region Explicit Interface Properties

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        int IDomainEvent.Version { get; set; }

        #endregion
    }
}