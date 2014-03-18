// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RowViewModel.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The row.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.UI.View.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    ///     The row.
    /// </summary>
    public class RowViewModel
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RowViewModel" /> class.
        /// </summary>
        public RowViewModel()
        {
            this.Cells = new List<CellViewModel>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the cells.
        /// </summary>
        public IList<CellViewModel> Cells { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add cell.
        /// </summary>
        /// <param name="cell">
        ///     The cell.
        /// </param>
        /// <returns>
        ///     The <see cref="RowViewModel" />.
        /// </returns>
        public RowViewModel AddCell(CellViewModel cell)
        {
            this.Cells.Add(cell);
            return this;
        }

        #endregion
    }
}