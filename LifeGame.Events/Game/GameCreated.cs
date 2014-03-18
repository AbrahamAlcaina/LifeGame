// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameCreated.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The game created.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Events
{
    using System;

    /// <summary>
    ///     The game created.
    /// </summary>
    public class GameCreated : DomainEvent
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameCreated" /> class.
        /// </summary>
        /// <param name="idGame">
        ///     The id game.
        /// </param>
        /// <param name="numberOfCells">
        ///     The number of cells.
        /// </param>
        public GameCreated(Guid idGame, int numberOfCells)
        {
            this.IdGame = idGame;
            this.NumberOfCells = numberOfCells;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the id game.
        /// </summary>
        public Guid IdGame { get; set; }

        /// <summary>
        ///     Gets or sets the number of cells.
        /// </summary>
        public int NumberOfCells { get; set; }

        #endregion
    }
}