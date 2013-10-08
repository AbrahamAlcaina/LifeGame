// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cell.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The cell.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LifeGame.Domain
{
    using System.Collections.Generic;

    /// <summary>
    ///     The cell.
    /// </summary>
    public class Cell
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Cell" /> class.
        /// </summary>
        public Cell()
        {
            this.Neighbors = new List<Cell>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        public CellStatus Status { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the neighbors.
        /// </summary>
        private IList<Cell> Neighbors { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add neighbors.
        /// </summary>
        /// <param name="neighbor">
        /// The neighbor.
        /// </param>
        /// <returns>
        /// The <see cref="Cell"/>.
        /// </returns>
        public Cell AddNeighbor(Cell neighbor)
        {
            this.Neighbors.Add(neighbor);
            return this;
        }

        #endregion
    }
}