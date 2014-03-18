// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellLivedEventHandler.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The cell lived event handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.UI.View.Handlers
{
    using LifeGame.EventHandlers;
    using LifeGame.Events;
    using LifeGame.UI.View.ViewModels;

    /// <summary>
    ///     The cell lived event handler.
    /// </summary>
    internal class CellLivedEventHandler : IEventHandler<CellLived>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CellLivedEventHandler" /> class.
        /// </summary>
        /// <param name="viewModel">
        ///     The view model.
        /// </param>
        public CellLivedEventHandler(MainViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the view model.
        /// </summary>
        public MainViewModel ViewModel { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The handle.
        /// </summary>
        /// <param name="event">
        ///     The event.
        /// </param>
        public void Handle(CellLived @event)
        {
            this.ViewModel.OnCellLived(@event.IdCell, @event.NumberOfTicks);
        }

        #endregion
    }
}