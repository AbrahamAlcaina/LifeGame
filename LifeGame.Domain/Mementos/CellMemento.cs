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
    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    /// The cell memento.
    /// </summary>
    internal class CellMemento : IMemento
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CellMemento"/> class.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        public CellMemento(CellStatus status)
        {
            this.Status = status;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        internal CellStatus Status { get; set; }

        #endregion
    }
}