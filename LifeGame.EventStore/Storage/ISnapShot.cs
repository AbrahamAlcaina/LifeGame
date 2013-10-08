// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISnapShot.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The SnapShot interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Storage
{
    using System;

    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    /// The SnapShot interface.
    /// </summary>
    public interface ISnapShot
    {
        #region Public Properties

        /// <summary>
        /// Gets the event provider id.
        /// </summary>
        Guid EventProviderId { get; }

        /// <summary>
        /// Gets the memento.
        /// </summary>
        IMemento Memento { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        int Version { get; }

        #endregion
    }
}