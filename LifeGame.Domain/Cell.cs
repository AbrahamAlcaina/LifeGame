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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LifeGame.Domain.Mementos;
    using LifeGame.Events;
    using LifeGame.Events.Cell;
    using LifeGame.EventStore;
    using LifeGame.EventStore.Aggregate;
    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    ///     The cell.
    /// </summary>
    public class Cell : BaseEntity<IDomainEvent>, IOrginator
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Cell" /> class.
        /// </summary>
        public Cell()
        {
            this.Status = CellStatus.Dead;
            this.ChangedReason = CellChangeReason.Creation;
            this.Neighbors = new List<Cell>();
            this.InternalRegisterEvents();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Cell" /> class.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        public Cell(Guid id)
            : this()
        {
            this.Id = id;

            // this.Apply(new CellCreated(id));
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether is dead.
        /// </summary>
        public bool IsDead
        {
            get
            {
                return this.Status == CellStatus.Dead;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether is live.
        /// </summary>
        public bool IsLive
        {
            get
            {
                return !this.IsDead;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the future states cause.
        /// </summary>
        internal CellChangeReason ChangedReason { get; set; }

        /// <summary>
        ///     Gets or sets the future status.
        /// </summary>
        internal CellStatus? FutureStatus { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        internal CellStatus Status { get; set; }

        /// <summary>
        ///     Gets or sets the neighbors.
        /// </summary>
        internal IList<Cell> Neighbors { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add neighbors.
        /// </summary>
        /// <param name="neighbor">
        ///     The neighbor.
        /// </param>
        /// <returns>
        ///     The <see cref="Cell" />.
        /// </returns>
        public Cell AddNeighbor(Cell neighbor)
        {
            this.Neighbors.Add(neighbor);
            return this;
        }

        /// <summary>
        ///     The change next status.
        /// </summary>
        /// <param name="numberOfTicks">
        /// </param>
        /// <returns>
        ///     The <see cref="Cell" />.
        /// </returns>
        /// <exception cref="WithOutFutureStatusIsImposibleToEvolveException">
        /// </exception>
        public Cell ChangeNextStatus(long numberOfTicks)
        {
            if (this.FutureStatus == null)
            {
                throw new WithOutFutureStatusIsImposibleToEvolveException();
            }

            if (this.ChangedReason == CellChangeReason.StillEqual)
            {
                return this;
            }

            return this.FutureStatus == CellStatus.Dead
                ? this.ChangeToDead(numberOfTicks)
                : this.ChangeToLive(numberOfTicks);
        }

        /// <summary>
        ///     The create memento.
        /// </summary>
        /// <returns>
        ///     The <see cref="IMemento" />.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IMemento CreateMemento()
        {
            return new CellMemento(this);
        }

        /// <summary>
        ///     The prepair next status.
        /// </summary>
        /// <returns>
        ///     The <see cref="Cell" />.
        /// </returns>
        public Cell PrepairNextStatus()
        {
            if (this.IsLive && this.Neighbors.Count(n => n.IsLive) < 2)
            {
                // Any live cell with fewer than two live neighbours dies, as if caused by under-population.
                this.ChangedReason = CellChangeReason.UnderPopulation;
                this.FutureStatus = CellStatus.Dead;
            }

            if (this.IsLive && (this.Neighbors.Count(n => n.IsLive) == 2 || this.Neighbors.Count(n => n.IsLive) == 3))
            {
                // Any live cell with two or three live neighbours lives on to the next generation.
                this.ChangedReason = CellChangeReason.StillEqual;
                this.FutureStatus = CellStatus.Alive;
            }

            if (this.IsLive && this.Neighbors.Count(n => n.IsLive) > 3)
            {
                // Any live cell with more than three live neighbours dies, as if by overcrowding.
                this.ChangedReason = CellChangeReason.Overcrowding;
                this.FutureStatus = CellStatus.Dead;
            }

            if (this.IsDead && this.Neighbors.Count(n => n.IsLive) == 3)
            {
                // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                this.ChangedReason = CellChangeReason.Reprodution;
                this.FutureStatus = CellStatus.Alive;
            }

            if (this.IsDead && this.Neighbors.Count(n => n.IsLive) != 3)
            {
                // Any dead cell with non three live neighbours still dead on to the next generation.
                this.ChangedReason = CellChangeReason.StillEqual;
                this.FutureStatus = CellStatus.Dead;
            }

            return this;
        }

        /// <summary>
        ///     The set memento.
        /// </summary>
        /// <param name="memento">
        ///     The memento.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void SetMemento(IMemento memento)
        {
            var cellMemento = memento as CellMemento;
            this.Id = cellMemento.Id;
            this.Status = cellMemento.Status;
            this.ChangedReason = cellMemento.ChangeReason;
            this.FutureStatus = cellMemento.FutureStatus;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The change to live.
        /// </summary>
        /// <param name="numberOfTicks">
        /// </param>
        /// <returns>
        ///     The <see cref="Cell" />.
        /// </returns>
        internal Cell ChangeToLive(long numberOfTicks)
        {
            this.Apply(new CellLived(this.Id, this.ChangedReason, numberOfTicks));
            return this;
        }

        /// <summary>
        ///     The change to dead.
        /// </summary>
        /// <param name="numberOfTicks">
        /// </param>
        /// <returns>
        ///     The <see cref="Cell" />.
        /// </returns>
        private Cell ChangeToDead(long numberOfTicks)
        {
            this.Apply(new CellDead(this.Id, this.ChangedReason, numberOfTicks));
            return this;
        }

        /// <summary>
        ///     The internal register events.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        private void InternalRegisterEvents()
        {
            this.RegisterEvent<CellCreated>(this.OnCreated);
            this.RegisterEvent<CellLived>(this.OnCellLived);
            this.RegisterEvent<CellDead>(this.OnCellDead);
        }

        /// <summary>
        ///     The on cell dead.
        /// </summary>
        /// <param name="event">
        ///     The event.
        /// </param>
        /// <exception cref="CellAlreadyDeadException">
        /// </exception>
        private void OnCellDead(CellDead @event)
        {
            //if (this.IsDead)
            //{
            //    throw new CellAlreadyDeadException();
            //}

            this.Status = CellStatus.Dead;
        }

        /// <summary>
        ///     The on cell lived.
        /// </summary>
        /// <param name="event">
        ///     The event.
        /// </param>
        /// <exception cref="CellAlreadyLiveException">
        /// </exception>
        private void OnCellLived(CellLived @event)
        {
            //if (this.IsLive)
            //{
            //    throw new CellAlreadyLiveException();
            //}

            this.Status = CellStatus.Alive;
        }

        /// <summary>
        ///     The on created.
        /// </summary>
        /// <param name="event">
        ///     The event.
        /// </param>
        private void OnCreated(CellCreated @event)
        {
            this.Id = @event.Id;
        }

        #endregion
    }
}