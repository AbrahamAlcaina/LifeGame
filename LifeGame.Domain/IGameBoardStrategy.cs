﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameBoardStrategy.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The game board strategy interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain
{
    using System.Collections.Generic;

    /// <summary>
    ///     The game board strategy interface.
    /// </summary>
    public interface IGameBoardStrategy
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The create gameboard.
        /// </summary>
        /// <param name="numberOfCells">
        ///     The number of cells.
        /// </param>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<Cell> CreateGameboard(int numberOfCells);

        #endregion
    }
}