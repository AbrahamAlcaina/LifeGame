// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDomainEvent.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The DomainEvent interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore
{
    using System;

    /// <summary>
    ///     The DomainEvent interface.
    /// </summary>
    public interface IDomainEvent
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the aggregate id.
        /// </summary>
        Guid AggregateId { get; set; }

        /// <summary>
        ///     Gets the id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        int Version { get; set; }

        #endregion
    }
}