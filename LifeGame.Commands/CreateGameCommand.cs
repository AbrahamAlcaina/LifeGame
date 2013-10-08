// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateGameCommand.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The create game command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Commands
{
    using System;

    /// <summary>
    /// The create game command.
    /// </summary>
    public class CreateGameCommand : Command
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGameCommand"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="numberOfCells">
        /// The number of cells.
        /// </param>
        public CreateGameCommand(Guid id, int numberOfCells)
            : base(id)
        {
            this.NumberOfCells = numberOfCells;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the number of cells.
        /// </summary>
        public int NumberOfCells { get; set; }

        #endregion
    }
}