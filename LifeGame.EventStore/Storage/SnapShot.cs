// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SnapShot.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The snap shot.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Storage
{
    using System;

    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    ///     The snap shot.
    /// </summary>
    [Serializable]
    public class SnapShot : ISnapShot
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SnapShot" /> class.
        /// </summary>
        /// <param name="eventProviderId">
        ///     The event provider id.
        /// </param>
        /// <param name="version">
        ///     The version.
        /// </param>
        /// <param name="memento">
        ///     The memento.
        /// </param>
        public SnapShot(Guid eventProviderId, int version, IMemento memento)
        {
            this.EventProviderId = eventProviderId;
            this.Version = version;
            this.Memento = memento;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the event provider id.
        /// </summary>
        public Guid EventProviderId { get; private set; }

        /// <summary>
        ///     Gets the memento.
        /// </summary>
        public IMemento Memento { get; private set; }

        /// <summary>
        ///     Gets the version.
        /// </summary>
        public int Version { get; private set; }

        #endregion
    }
}