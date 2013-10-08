// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SquareGameboard.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The square gameboard.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The square gameboard.
    /// </summary>
    public class SquareGameboard : IGameBoardStrategy
    {
        #region Public Methods and Operators

        /// <summary>
        /// The create gameboard.
        /// </summary>
        /// <param name="numberOfCells">
        /// The number of cells.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public IList<Cell> CreateGameboard(int numberOfCells)
        {
            double sqr = Math.Sqrt(numberOfCells);
            if (sqr != Math.Truncate(sqr))
            {
                throw new Exception("not valid size");
            }

            var side = (int)sqr;
            var grid = new Cell[side, side];
            this.CreateCells(grid, side, side);
            this.AssociateCellsWithHisNeighbors(grid, side, side);
            return this.ToList(grid, side);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The associate neighbors.
        /// </summary>
        /// <param name="cells">
        /// The cells.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        private void AssociateCellsWithHisNeighbors(Cell[,] cells, int height, int width)
        {
            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = width - 1; j >= 0; j--)
                {
                    // coordinates relative to the cell we are working
                    int top = i == 0 ? height - 1 : i;
                    int left = j == 0 ? width - 1 : j;
                    int bottom = i == height - 1 ? 0 : i;
                    int right = j == width - 1 ? 0 : j;
                    int x = i;
                    int y = j;

                    // use compass rose to retrive neighbor
                    Cell cell = cells[x, y];
                    Cell nw = cells[top, left];
                    Cell n = cells[top, y];
                    Cell ne = cells[top, right];
                    Cell w = cells[x, left];
                    Cell e = cells[x, right];
                    Cell sw = cells[bottom, left];
                    Cell s = cells[bottom, y];
                    Cell se = cells[bottom, right];

                    // add neighbor
                    cell.AddNeighbor(nw);
                    cell.AddNeighbor(n);
                    cell.AddNeighbor(ne);
                    cell.AddNeighbor(w);
                    cell.AddNeighbor(e);
                    cell.AddNeighbor(sw);
                    cell.AddNeighbor(s);
                    cell.AddNeighbor(se);
                }
            }
        }

        /// <summary>
        /// The create cells.
        /// </summary>
        /// <param name="cells">
        /// The grid.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        private void CreateCells(Cell[,] cells, int height, int width)
        {
            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = width - 1; j >= 0; j--)
                {
                    var cell = new Cell();
                    cells[i, j] = cell;
                }
            }
        }

        /// <summary>
        /// The to list.
        /// </summary>
        /// <param name="grid">
        /// The grid.
        /// </param>
        /// <param name="side">
        /// The side.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        private IList<Cell> ToList(Cell[,] grid, int side)
        {
            var list = new List<Cell>();
            for (int i = side - 1; i >= 0; i--)
            {
                for (int j = side - 1; j >= 0; j--)
                {
                    list.Add(grid[i, j]);
                }
            }

            return list;
        }

        #endregion
    }
}