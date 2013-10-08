// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LifeGame.Domain
{
    using LifeGame.Domain.Mementos;
    using LifeGame.EventStore;
    using LifeGame.EventStore.Aggregate;
    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    ///     The game.
    /// </summary>
    public class Game : BaseAggregateRoot<IDomainEvent>, IOrginator
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="numberOfCells">
        /// The number of cells.
        /// </param>
        /// <param name="gameBoardConstructor">
        /// Strategy to create universe
        /// </param>
        public Game(int numberOfCells, IGameBoardStrategy gameBoardConstructor)
        {
            this.NumberOfCells = numberOfCells;
            this.Universe = Universe.Create(gameBoardConstructor, numberOfCells);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Game" /> class.
        /// </summary>
        public Game()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the number of cells.
        /// </summary>
        internal int NumberOfCells { get; set; }

        /// <summary>
        ///     Gets or sets the universe.
        /// </summary>
        private Universe Universe { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create game.
        /// </summary>
        /// <param name="numberOfCells">
        /// The number of cells.
        /// </param>
        /// <param name="gameBoardConstructor">
        /// Strategy to create a universe.
        /// </param>
        /// <returns>
        /// The <see cref="Game"/>.
        /// </returns>
        public static Game CreateGame(int numberOfCells, IGameBoardStrategy gameBoardConstructor)
        {
            return new Game(numberOfCells, gameBoardConstructor);
        }

        /// <summary>
        ///     The create memento.
        /// </summary>
        /// <returns>
        ///     The <see cref="IMemento" />.
        /// </returns>
        public IMemento CreateMemento()
        {
            return new GameMemento(this.Id, this.Version, this.NumberOfCells);
        }

        /// <summary>
        /// The set memento.
        /// </summary>
        /// <param name="memento">
        /// The memento.
        /// </param>
        public void SetMemento(IMemento memento)
        {
            var gameMemento = memento as GameMemento;
            this.Id = gameMemento.Id;
            this.Version = gameMemento.Version;
            this.NumberOfCells = gameMemento.NumberOfCells;
        }

        #endregion
    }
}