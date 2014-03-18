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
    ///     The square gameboard.
    /// </summary>
    public class SquareGameboard : IGameBoardStrategy
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The create gameboard.
        /// </summary>
        /// <param name="numberOfCells">
        ///     The number of cells.
        /// </param>
        /// <returns>
        ///     The <see cref="IList" />.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public IEnumerable<Cell> CreateGameboard(int numberOfCells)
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
        ///     The associate neighbors.
        /// </summary>
        /// <param name="cells">
        ///     The cells.
        /// </param>
        /// <param name="height">
        ///     The height.
        /// </param>
        /// <param name="width">
        ///     The width.
        /// </param>
        internal void AssociateCellsWithHisNeighbors(Cell[,] cells, int height, int width)
        {
            for (int i = width - 1; i >= 0; i--)
            {
                for (int j = height - 1; j >= 0; j--)
                {
                    var x = i;
                    var y = j;
                    
                    // coordinates relative to the cell we are working
                    var top = y + 1 == height ? 0 : y + 1;
                    var bottom = y - 1 < 0 ? height - 1 : y - 1;
                    var left = x - 1 < 0 ? width - 1 : x - 1;
                    var right = x + 1 == width ? 0 : x + 1;
                    
                    // use compass rose to retrive neighbor
                    Cell cell = cells[x, y];
                    Cell nw = cells[left,top];
                    Cell n = cells[x, top];
                    Cell ne = cells[right,top];
                    Cell w = cells[left, y];
                    Cell e = cells[right, y];
                    Cell se = cells[right,bottom];
                    Cell s = cells[x, bottom];
                    Cell sw = cells[left, bottom];
                    

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
        ///     The create cells.
        /// </summary>
        /// <param name="cells">
        ///     The grid.
        /// </param>
        /// <param name="height">
        ///     The height.
        /// </param>
        /// <param name="width">
        ///     The width.
        /// </param>
        private void CreateCells(Cell[,] cells, int height, int width)
        {
            int ids = 0;
            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = width - 1; j >= 0; j--)
                {
                    var cell = new Cell(new Guid(string.Format("{0:00000000000000000000000000000000}", ids)));
                    ids++;
                    cells[i, j] = cell;
                }
            }
        }

        /// <summary>
        ///     The to list.
        /// </summary>
        /// <param name="grid">
        ///     The grid.
        /// </param>
        /// <param name="side">
        ///     The side.
        /// </param>
        private IEnumerable<Cell> ToList(Cell[,] grid, int side)
        {
            for (int i = side - 1; i >= 0; i--)
            {
                for (int j = side - 1; j >= 0; j--)
                {
                    yield return grid[i, j];
                }
            }
        }

        #endregion
    }
}