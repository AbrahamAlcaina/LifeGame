// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The main view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.UI.View.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Caliburn.Micro;

    using LifeGame.Bus;
    using LifeGame.Commands;

    /// <summary>
    ///     The main view model.
    /// </summary>
    public class MainViewModel : PropertyChangedBase
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="bus">
        ///     The bus.
        /// </param>
        public MainViewModel(IBus bus)
        {
            this.Bus = bus;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the bus.
        /// </summary>
        public IBus Bus { get; set; }

        /// <summary>
        ///     Gets a value indicating whether can create.
        /// </summary>
        public bool CanCreate
        {
            get
            {
                return default(Guid) == this.CurrentGame;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether can evolve.
        /// </summary>
        public bool CanEvolve
        {
            get
            {
                return !this.CanCreate && this.CanInitialize;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether can initialize.
        /// </summary>
        public bool CanInitialize
        {
            get
            {
                return default(Guid) != this.CurrentGame;
            }
        }

        /// <summary>
        ///     Gets or sets the current game.
        /// </summary>
        public Guid CurrentGame { get; set; }

        /// <summary>
        ///     Gets or sets the universe grid.
        /// </summary>
        public UniverseViewModel UniverseGrid { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The evolve game.
        /// </summary>
        public void EvolveGame()
        {
            this.Bus.Publish(new EvolveGameCommand(Guid.NewGuid(), this.CurrentGame));
        }

        /// <summary>
        ///     The inicializate game.
        /// </summary>
        public void InicializateGame()
        {
            this.Bus.Publish(new InitializeGameCommand(Guid.NewGuid(), this.CurrentGame, 1500));
            //this.Bus.Publish(new InitializeGameCommand(Guid.NewGuid(), this.CurrentGame, 4));
            this.NotifyOfPropertyChange(() => this.CanEvolve);
        }

        /// <summary>
        ///     The new game.
        /// </summary>
        public void NewGame()
        {
            this.CurrentGame = Guid.NewGuid();
            this.Bus.Publish(new CreateGameCommand(Guid.NewGuid(), this.CurrentGame, 2500));
            //this.Bus.Publish(new CreateGameCommand(Guid.NewGuid(), this.CurrentGame, 16));
            this.NotifyOfPropertyChange(() => this.CanInitialize);
            this.NotifyOfPropertyChange(() => this.CurrentGame);
        }

        /// <summary>
        ///     The on cell dead.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="numberOfTicks">
        ///     The number of ticks.
        /// </param>
        public void OnCellDead(Guid id, long numberOfTicks)
        {
            this.UniverseGrid.AssignCell(id, false);
        }

        /// <summary>
        ///     The on cell lived.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="numberOfTicks">
        ///     The number of ticks.
        /// </param>
        public void OnCellLived(Guid id, long numberOfTicks)
        {
            this.UniverseGrid.AssignCell(id, true);
        }

        /// <summary>
        ///     The on created game.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="numberOfCells">
        ///     The number of cells.
        /// </param>
        public void OnCreatedGame(Guid id, int numberOfCells)
        {
            this.UniverseGrid = new UniverseViewModel();
            double side = Math.Sqrt(numberOfCells);
            var rows = new List<RowViewModel>();
            int ids = 0;
            for (int i = 0; i < side; i++)
            {
                var row = new RowViewModel();
                for (int j = 0; j < side; j++)
                {
                    var cell = new CellViewModel
                               {
                                   Id =
                                       new Guid(
                                       string.Format("{0:00000000000000000000000000000000}", ids))
                               };
                    ids++;
                    row.AddCell(cell);
                }

                rows.Add(row);
            }

            this.UniverseGrid.Rows = rows;
            this.NotifyOfPropertyChange(() => this.UniverseGrid);
        }

        #endregion
    }
}