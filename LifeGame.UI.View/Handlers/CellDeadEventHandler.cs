// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellDeadEventHandler.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The cell dead event handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.UI.View.Handlers
{
    using LifeGame.EventHandlers;
    using LifeGame.Events;
    using LifeGame.UI.View.ViewModels;

    /// <summary>
    ///     The cell dead event handler.
    /// </summary>
    internal class CellDeadEventHandler : IEventHandler<CellDead>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CellDeadEventHandler" /> class.
        /// </summary>
        /// <param name="viewModel">
        ///     The view model.
        /// </param>
        public CellDeadEventHandler(MainViewModel viewModel)
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
        public void Handle(CellDead @event)
        {
            this.ViewModel.OnCellDead(@event.IdCell, @event.NumberOfTicks);
        }

        #endregion
    }
}