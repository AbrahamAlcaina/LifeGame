// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UniverseViewModel.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The universe view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.UI.View.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Caliburn.Micro;

    /// <summary>
    ///     The universe view model.
    /// </summary>
    public class UniverseViewModel : PropertyChangedBase
    {
        #region Fields

        /// <summary>
        ///     The height.
        /// </summary>
        private long height;

        /// <summary>
        ///     The id.
        /// </summary>
        private Guid id;

        private IEnumerable<RowViewModel> rows;

        /// <summary>
        ///     The width.
        /// </summary>
        private long width;

        #endregion

        #region Constructors and Destructors

        public UniverseViewModel()
        {
            this.Cells = new List<CellViewModel>();
        }

        #endregion

        #region Public Properties

        public IList<CellViewModel> Cells { get; private set; }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        public long Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = value;
                this.NotifyOfPropertyChange(() => this.Height);
            }
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                this.NotifyOfPropertyChange(() => this.Id);
            }
        }

        /// <summary>
        ///     Gets or sets the rows.
        /// </summary>
        public IEnumerable<RowViewModel> Rows
        {
            get
            {
                return this.rows;
            }
            set
            {
                this.rows = value;
                foreach (RowViewModel rowViewModel in value)
                {
                    foreach (CellViewModel cell in rowViewModel.Cells)
                    {
                        this.Cells.Add(cell);
                    }
                }
                this.NotifyOfPropertyChange(() => this.Rows);
            }
        }

        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        public long Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = value;
                this.NotifyOfPropertyChange(() => this.Width);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The assign cell.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="isLive">
        ///     The is live.
        /// </param>
        /// <returns>
        ///     The <see cref="UniverseViewModel" />.
        /// </returns>
        public UniverseViewModel AssignCell(Guid id, bool isLive)
        {
            this.Cells.First(r => r.Id == id).Alive = isLive;
            return this;
        }

        #endregion
    }
}