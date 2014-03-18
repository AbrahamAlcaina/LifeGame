// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellViewModel.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The cell.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.UI.View.ViewModels
{
    using System;
    using System.Windows.Media;

    using Caliburn.Micro;

    /// <summary>
    ///     The cell.
    /// </summary>
    public class CellViewModel : PropertyChangedBase
    {

        public CellViewModel ()
        {
            this.Alive = false;
        }
        

        #region Fields

        /// <summary>
        ///     The alive.
        /// </summary>
        private bool alive;

        /// <summary>
        ///     The color brush.
        /// </summary>
        private Brush colorBrush;

        /// <summary>
        ///     The id.
        /// </summary>
        private Guid id;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether alive.
        /// </summary>
        public bool Alive
        {
            get
            {
                return this.alive;
            }

            set
            {
                this.alive = value;
                this.ColorBrush = this.alive ? Brushes.White : Brushes.Black;
            }
        }

        /// <summary>
        ///     Gets or sets the color brush.
        /// </summary>
        public Brush ColorBrush
        {
            get
            {
                return this.colorBrush;
            }

            set
            {
                this.colorBrush = value;
                this.NotifyOfPropertyChange(() => this.ColorBrush);
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

        #endregion
    }
}