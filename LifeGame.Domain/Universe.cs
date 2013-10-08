// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Universe.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LifeGame.Domain
{
    using System.Collections.Generic;

    /// <summary>
    ///     The universe is an infinite two-dimensional orthogonal grid.
    /// </summary>
    public class Universe
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Universe"/> class.
        /// </summary>
        /// <param name="gameboardConstructor">
        /// The game board Constructor.
        /// </param>
        /// <param name="numberOfCells">
        /// The number Of Cells.
        /// </param>
        public Universe(IGameBoardStrategy gameboardConstructor, int numberOfCells)
        {
            this.NumberOfLifeCell = numberOfCells;
            this.Cells = gameboardConstructor.CreateGameboard(numberOfCells);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the number of life cell.
        /// </summary>
        public int NumberOfLifeCell { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the cells.
        /// </summary>
        internal IList<Cell> Cells { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="strategy">
        /// The strategy.
        /// </param>
        /// <param name="numberOfCells">
        /// The number of cells.
        /// </param>
        /// <returns>
        /// The <see cref="Universe"/>.
        /// </returns>
        public static Universe Create(IGameBoardStrategy strategy, int numberOfCells)
        {
            return new Universe(strategy, numberOfCells);
        }

        #endregion
    }
}