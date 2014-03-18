// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameCreatedHandler.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The game created handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.UI.View.Handlers
{
    using LifeGame.EventHandlers;
    using LifeGame.Events;
    using LifeGame.UI.View.ViewModels;

    /// <summary>
    ///     The game created handler.
    /// </summary>
    public class UIGameCreatedEventHandler : IEventHandler<GameCreated>
    {
        #region Fields

        /// <summary>
        ///     The view model.
        /// </summary>
        private readonly MainViewModel viewModel;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UIGameCreatedEventHandler" /> class.
        /// </summary>
        /// <param name="viewModel">
        ///     The view model.
        /// </param>
        public UIGameCreatedEventHandler(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The handle.
        /// </summary>
        /// <param name="event">
        ///     The event.
        /// </param>
        public void Handle(GameCreated @event)
        {
            this.viewModel.OnCreatedGame(@event.Id, @event.NumberOfCells);
        }

        #endregion
    }
}