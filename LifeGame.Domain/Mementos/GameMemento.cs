// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameMemento.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The game memento.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain.Mementos
{
    using System;

    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    /// The game memento.
    /// </summary>
    [Serializable]
    public class GameMemento : IMemento
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameMemento"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <param name="numberOfCells">
        /// The number of cells.
        /// </param>
        public GameMemento(Guid id, int version, int numberOfCells)
        {
            this.Id = id;
            this.Version = version;
            this.NumberOfCells = numberOfCells;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the id.
        /// </summary>
        internal Guid Id { get; private set; }

        /// <summary>
        /// Gets the number of cells.
        /// </summary>
        internal int NumberOfCells { get; private set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        internal int Version { get; private set; }

        #endregion
    }
}