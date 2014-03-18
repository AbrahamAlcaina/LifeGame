// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameInitiliazed.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The game initiliazed.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Events
{
    using System;

    /// <summary>
    ///     The game initiliazed.
    /// </summary>
    public class GameInitiliazed : DomainEvent
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameInitiliazed" /> class.
        /// </summary>
        /// <param name="idGame">
        ///     The id game.
        /// </param>
        /// <param name="numberOfCellsLive">
        ///     The number of cells live.
        /// </param>
        public GameInitiliazed(Guid idGame, int numberOfCellsLive)
        {
            this.IdGame = idGame;
            this.NumberOfCellsLive = numberOfCellsLive;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the id game.
        /// </summary>
        public Guid IdGame { get; set; }

        /// <summary>
        ///     Gets or sets the number of cells live.
        /// </summary>
        public int NumberOfCellsLive { get; set; }

        #endregion
    }
}