// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UniverseMemento.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The universe memento.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain.Mementos
{
    using System;
    using System.Collections.Generic;

    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    ///     The universe memento.
    /// </summary>
    [Serializable]
    public class UniverseMemento : IMemento
    {
        #region Constructors and Destructors

        public UniverseMemento()
        {
            this.Cells = new List<CellMemento>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UniverseMemento" /> class.
        /// </summary>
        /// <param name="universe">
        ///     The universe.
        /// </param>
        public UniverseMemento(Universe universe)
            : this()
        {
            foreach (Cell cell in universe.Cells)
            {
                this.Cells.Add(cell.CreateMemento() as CellMemento);
            }
            this.NumberOfCellsLive = universe.NumberOfCellsLive;
            this.NumberOfLifeCell = universe.NumberOfLifeCell;
            this.NumberOfTicks = universe.NumberOfTicks;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the cells.
        /// </summary>
        public IList<CellMemento> Cells { get; set; }

        /// <summary>
        ///     Gets or sets the number of cells live.
        /// </summary>
        public int NumberOfCellsLive { get; set; }

        /// <summary>
        ///     Gets or sets the number of life cell.
        /// </summary>
        public int NumberOfLifeCell { get; set; }

        /// <summary>
        ///     Gets or sets the number of ticks.
        /// </summary>
        public long NumberOfTicks { get; set; }

        #endregion
    }
}