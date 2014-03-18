// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellDead.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The cell dead.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Events
{
    using System;

    using LifeGame.Domain;

    /// <summary>
    ///     The cell dead.
    /// </summary>
    [Serializable]
    public class CellDead : DomainEvent
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CellDead" /> class.
        /// </summary>
        /// <param name="idCell"></param>
        /// <param name="reason">
        /// </param>
        /// <param name="numberOfTicks">
        /// </param>
        public CellDead(Guid idCell, CellChangeReason reason, long numberOfTicks)
        {
            this.IdCell = idCell;
            this.Reason = reason;
            this.NumberOfTicks = numberOfTicks;
        }

        #endregion

        #region Public Properties

        public Guid IdCell { get; set; }

        /// <summary>
        ///     Gets or sets the number of ticks.
        /// </summary>
        public long NumberOfTicks { get; set; }

        /// <summary>
        ///     Gets or sets the reason.
        /// </summary>
        public CellChangeReason Reason { get; set; }

        #endregion
    }
}