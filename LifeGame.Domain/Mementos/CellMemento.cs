// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellMemento.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The cell memento.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain.Mementos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    ///     The cell memento.
    /// </summary>
    [Serializable]
    public class CellMemento : IMemento
    {
        #region Constructors and Destructors

        public CellMemento()
        {
            this.Neighbors = new List<Guid>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CellMemento" /> class.
        /// </summary>
        /// <param name="cell">
        ///     The cell.
        /// </param>
        public CellMemento(Cell cell): this()
        {
            this.Id = cell.Id;
            this.Status = cell.Status;
            this.ChangeReason = cell.ChangedReason;
            this.FutureStatus = cell.FutureStatus;
            this.Neighbors = cell.Neighbors.Select(c=> c.Id).ToList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the future states cause.
        /// </summary>
        public CellChangeReason ChangeReason { get; set; }

        /// <summary>
        ///     Gets or sets the future status.
        /// </summary>
        public CellStatus? FutureStatus { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        public CellStatus Status { get; set; }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        public int Version { get; set; }

        public IList<Guid> Neighbors { get; set; }

        #endregion
    }
}