// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateGameCommand.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The create game command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Commands
{
    using System;

    /// <summary>
    ///     The create game command.
    /// </summary>
    public class CreateGameCommand : Command
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateGameCommand" /> class.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="idGame">
        /// </param>
        /// <param name="numberOfCells">
        ///     The number of cells.
        /// </param>
        public CreateGameCommand(Guid id, Guid idGame, int numberOfCells)
            : base(id)
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