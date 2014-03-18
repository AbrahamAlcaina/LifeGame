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
    ///     The game memento.
    /// </summary>
    [Serializable]
    public class GameMemento : IMemento
    {
        #region Constructors and Destructors

        public GameMemento()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameMemento" /> class.
        /// </summary>
        /// <param name="game">
        ///     The game.
        /// </param>
        public GameMemento(Game game)
        {
            this.Id = game.Id;
            this.Version = game.Version;
            this.NumberOfCells = game.NumberOfCells;
            this.Universe = game.Universe.CreateMemento() as UniverseMemento;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the universe.
        /// </summary>
        public UniverseMemento Universe { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets the number of cells.
        /// </summary>
        public int NumberOfCells { get; set; }

        /// <summary>
        ///     Gets the version.
        /// </summary>
        public int Version { get; set; }

        #endregion
    }
}