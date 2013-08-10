// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Grid.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain
{
    using System.Collections.Generic;

    /// <summary>
    /// The grid.
    /// </summary>
    public class Grid
    {
        #region Properties

        /// <summary>
        /// Gets or sets the cells.
        /// </summary>
        internal IEnumerable<Cell> Cells { get; set; }

        #endregion
    }
}