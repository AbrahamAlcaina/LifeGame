// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Universe.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The universe is an infinite two-dimensional orthogonal grid.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LifeGame.Domain.Mementos;
    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    ///     The universe is an infinite two-dimensional orthogonal grid.
    /// </summary>
    public class Universe : IOrginator
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Universe" /> class.
        /// </summary>
        /// <param name="gameboardConstructor">
        ///     The game board Constructor.
        /// </param>
        /// <param name="numberOfCells">
        ///     The number Of Cells.
        /// </param>
        public Universe(IGameBoardStrategy gameboardConstructor, int numberOfCells)
        {
            this.NumberOfLifeCell = numberOfCells;

            // TODO REMOVE patch
            gameboardConstructor = gameboardConstructor ?? new SquareGameboard();

            this.Cells = gameboardConstructor.CreateGameboard(numberOfCells).ToList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the number of cells live.
        /// </summary>
        public int NumberOfCellsLive { get; private set; }

        /// <summary>
        ///     Gets the number of life cell.
        /// </summary>
        public int NumberOfLifeCell { get; private set; }

        /// <summary>
        ///     Gets the number of ticks.
        /// </summary>
        public long NumberOfTicks { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the cells.
        /// </summary>
        internal IList<Cell> Cells { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The create.
        /// </summary>
        /// <param name="strategy">
        ///     The strategy.
        /// </param>
        /// <param name="numberOfCells">
        ///     The number of cells.
        /// </param>
        /// <returns>
        ///     The <see cref="Universe" />.
        /// </returns>
        public static Universe Create(IGameBoardStrategy strategy, int numberOfCells)
        {
            return new Universe(strategy, numberOfCells);
        }

        /// <summary>
        ///     The create memento.
        /// </summary>
        /// <returns>
        ///     The <see cref="IMemento" />.
        /// </returns>
        public IMemento CreateMemento()
        {
            return new UniverseMemento(this);
        }

        /// <summary>
        ///     The evolve.
        /// </summary>
        public void Evolve()
        {
            foreach (Cell cell in this.Cells)
            {
                cell.PrepairNextStatus();
            }

            this.NumberOfCellsLive = 0;
            foreach (Cell cell in this.Cells)
            {
                cell.ChangeNextStatus(this.NumberOfTicks);
                if (cell.IsLive)
                {
                    this.NumberOfCellsLive++;
                }
            }

            this.NumberOfTicks++;
        }

        /// <summary>
        ///     The get cell.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        ///     The <see cref="Cell" />.
        /// </returns>
        public Cell GetCell(Guid id)
        {
            return this.Cells.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        ///     The initialize.
        /// </summary>
        /// <param name="numberOfCellsLive">
        ///     The number of cells live.
        /// </param>
        /// <returns>
        ///     The <see cref="Universe" />.
        /// </returns>
        public Universe Initialize(int numberOfCellsLive)
        {
            int i = 0;
            var rnd = new Random();

            while (i < numberOfCellsLive)
            {
                Cell cell = this.Cells[rnd.Next(0, this.Cells.Count())];
                if (cell.IsDead)
                {
                    cell.ChangeToLive(-1);
                    i++;
                }
            }

            return this;
        }

        /// <summary>
        ///     The set memento.
        /// </summary>
        /// <param name="memento">
        ///     The memento.
        /// </param>
        public void SetMemento(IMemento memento)
        {
            var universeMemento = memento as UniverseMemento;
            this.NumberOfCellsLive = universeMemento.NumberOfCellsLive;
            this.NumberOfLifeCell = universeMemento.NumberOfLifeCell;
            this.NumberOfTicks = universeMemento.NumberOfTicks;

            this.Cells.Clear();
            foreach (CellMemento cellMemento in universeMemento.Cells)
            {
                var cell = new Cell();
                cell.SetMemento(cellMemento);
                this.Cells.Add(cell);
            }

            foreach (CellMemento cellMemento in universeMemento.Cells)
            {
                var cell = this.Cells.First(c => c.Id == cellMemento.Id);
                foreach (var neighbor in cellMemento.Neighbors)
                {
                    cell.AddNeighbor(this.Cells.First(c => c.Id == neighbor));
                }
            }
        }

        #endregion
    }
}