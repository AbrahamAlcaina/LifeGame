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
        ///     Initializes a new instance of the <see cref="Universe" /> class.
        /// </summary>
        public Universe(IGameboardConstructorStrategy gameboardConstructor, int numberOfCells)
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
    }
}