// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The game.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain
{
    using System;

    using LifeGame.Domain.Mementos;
    using LifeGame.Events;
    using LifeGame.Events.Cell;
    using LifeGame.EventStore;
    using LifeGame.EventStore.Aggregate;
    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    ///     The game.
    /// </summary>
    public class Game : BaseAggregateRoot<IDomainEvent>, IOrginator
    {
        #region Fields

        /// <summary>
        ///     The cells.
        /// </summary>
        private readonly EntityList<Cell, IDomainEvent> cells;

        /// <summary>
        ///     The game board constructor.
        /// </summary>
        private readonly IGameBoardStrategy gameBoardConstructor;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Game" /> class.
        /// </summary>
        /// <param name="idGame">
        ///     The game id
        /// </param>
        /// <param name="gameBoardConstructor">
        ///     Strategy to create universes
        /// </param>
        /// <param name="numberOfCells">
        ///     The number of cells.
        /// </param>
        public Game(Guid idGame, IGameBoardStrategy gameBoardConstructor, int numberOfCells)
            : this()
        {
            this.gameBoardConstructor = gameBoardConstructor;
            this.Apply(new GameCreated(idGame, numberOfCells));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Game" /> class.
        ///     This constructor is used by event source to create a new instance, after that ES loads all events.
        /// </summary>
        public Game()
        {
            this.cells = new EntityList<Cell, IDomainEvent>(this);
            this.Universe = new Universe(null, 0);
            this.InternalRegisterEvents();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the number of cells live.
        /// </summary>
        public int NumberOfCellsLive { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the number of cells.
        /// </summary>
        internal int NumberOfCells { get; set; }

        /// <summary>
        ///     Gets or sets the universe.
        /// </summary>
        internal Universe Universe { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The create game.
        /// </summary>
        /// <param name="idGame">
        ///     The id game.
        /// </param>
        /// <param name="gameBoardConstructor">
        ///     Strategy to create a universe.
        /// </param>
        /// <param name="numberOfCells">
        ///     The number of cells.
        /// </param>
        /// The number of cells live in the initialization.
        /// <returns>
        ///     The <see cref="Game" />.
        /// </returns>
        public static Game CreateGame(Guid idGame, IGameBoardStrategy gameBoardConstructor, int numberOfCells)
        {
            return new Game(idGame, gameBoardConstructor, numberOfCells);
        }

        /// <summary>
        ///     The create memento.
        /// </summary>
        /// <returns>
        ///     The <see cref="IMemento" />.
        /// </returns>
        public IMemento CreateMemento()
        {
            return new GameMemento(this);
        }

        /// <summary>
        ///     The evolve.
        /// </summary>
        public void Evolve()
        {
            this.Apply(new GameEvolved(this.Id));
        }

        /// <summary>
        ///     The initialize.
        /// </summary>
        /// <param name="numberOfCellsLive">
        ///     The number of cells live.
        /// </param>
        public void Initialize(int numberOfCellsLive)
        {
            this.Universe.Initialize(numberOfCellsLive);
            this.Apply(new GameInitiliazed(this.Id, numberOfCellsLive));
        }

        /// <summary>
        ///     The set memento.
        /// </summary>
        /// <param name="memento">
        ///     The memento.
        /// </param>
        public void SetMemento(IMemento memento)
        {
            var gameMemento = memento as GameMemento;
            this.Id = gameMemento.Id;
            this.Version = gameMemento.Version;
            this.NumberOfCells = gameMemento.NumberOfCells;
            this.Universe.SetMemento(gameMemento.Universe);
            this.RegisterCellEvents();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The internal register events.
        /// </summary>
        private void InternalRegisterEvents()
        {
            this.RegisterEvent<GameCreated>(this.OnCreated);
            this.RegisterEvent<GameInitiliazed>(this.OnInitialize);
            this.RegisterEvent<GameEvolved>(this.OnEvolved);
            this.RegisterEventsToChildEntities();
        }

        /// <summary>
        ///     The on any event to cells.
        /// </summary>
        /// <param name="domainEvent">
        ///     The domain event.
        /// </param>
        /// <exception cref="NonExitCellToSendTheEvent">
        ///     The cell to pass the event is not created.
        /// </exception>
        private void OnAnyEventToCells(IDomainEvent domainEvent)
        {
            IEntityEventProvider<IDomainEvent> cell;
            if (!this.cells.TryGetValueById(domainEvent.AggregateId, out cell))
            {
                throw new NonExitCellToSendTheEvent();
            }

            cell.LoadFromHistory(new[] { domainEvent });
        }

        /// <summary>
        ///     The on created.
        /// </summary>
        /// <param name="event">
        ///     The event.
        /// </param>
        private void OnCreated(GameCreated @event)
        {
            this.Id = @event.IdGame;
            this.NumberOfCells = @event.NumberOfCells;
            this.Universe = new Universe(this.gameBoardConstructor, this.NumberOfCells);
            this.RegisterCellEvents();
        }

        /// <summary>
        ///     The on evolved.
        /// </summary>
        /// <param name="event">
        ///     The event.
        /// </param>
        private void OnEvolved(GameEvolved @event)
        {
            this.Universe.Evolve();
        }

        /// <summary>
        ///     The on initialize.
        /// </summary>
        /// <param name="event">
        ///     The event.
        /// </param>
        private void OnInitialize(GameInitiliazed @event)
        {
            this.NumberOfCellsLive = @event.NumberOfCellsLive;
        }

        /// <summary>
        ///     The register cell events.
        /// </summary>
        private void RegisterCellEvents()
        {
            foreach (Cell cell in this.Universe.Cells)
            {
                this.cells.Add(cell);
            }
        }

        /// <summary>
        ///     The register events to child entities.
        /// </summary>
        private void RegisterEventsToChildEntities()
        {
            this.RegisterEvent<CellCreated>(this.OnAnyEventToCells);
            this.RegisterEvent<CellLived>(this.OnAnyEventToCells);
            this.RegisterEvent<CellDead>(this.OnAnyEventToCells);
        }

        #endregion
    }
}