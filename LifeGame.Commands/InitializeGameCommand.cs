// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitializeGameCommand.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The start game command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Commands
{
    using System;

    /// <summary>
    ///     The start game command.
    /// </summary>
    [Serializable]
    public class InitializeGameCommand : Command
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="InitializeGameCommand" /> class.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="idGame">
        ///     The id game.
        /// </param>
        /// <param name="numberOfCellsLive">
        ///     The number of live cells.
        /// </param>
        public InitializeGameCommand(Guid id, Guid idGame, int numberOfCellsLive)
            : base(id)
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