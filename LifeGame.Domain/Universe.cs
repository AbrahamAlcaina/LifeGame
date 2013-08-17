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
        #region Constants

        /// <summary>
        /// The default height.
        /// </summary>
        private const int DefaultHeight = 1000;

        /// <summary>
        /// The default width.
        /// </summary>
        private const int DefaultWidth = 1000;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Universe" /> class.
        /// </summary>
        public Universe()
            : this(DefaultHeight, DefaultWidth)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Universe"/> class.
        /// </summary>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        internal Universe(int height, int width)
        {
            var grid = new Cell[height, width];
            this.Cells = new List<Cell>();

            this.CreateCells(grid, height, width);
            this.AssociateCellsWithHisNeighbors(grid, height, width);
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
        /// The initialize.
        /// </summary>
        /// <param name="numberOfLifeCells">
        /// The number of life cells.
        /// </param>
        /// <returns>
        /// The <see cref="Universe"/>.
        /// </returns>
        public Universe SetUp(int numberOfLifeCells)
        {
            this.NumberOfLifeCell = numberOfLifeCells;
            return this;
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
                    int top = i == 0 ? height : i;
                    int left = j == 0 ? width : j;
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
                    this.Cells.Add(cell);
                }
            }
        }

        #endregion
    }
}