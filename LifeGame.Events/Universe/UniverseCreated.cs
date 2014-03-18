// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UniverseCreated.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The universe created.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Events
{
    /// <summary>
    ///     The universe created.
    /// </summary>
    public class UniverseCreated : DomainEvent
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UniverseCreated" /> class.
        /// </summary>
        /// <param name="numberOfCells">
        ///     The number of cells.
        /// </param>
        public UniverseCreated(int numberOfCells)
        {
            this.NumberOfCells = numberOfCells;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the number of cells.
        /// </summary>
        public int NumberOfCells { get; set; }

        #endregion
    }
}