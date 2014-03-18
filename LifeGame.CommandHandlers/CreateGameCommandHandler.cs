// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateGameCommandHandler.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The create game command handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.CommandHandlers
{
    using LifeGame.Commands;
    using LifeGame.Domain;
    using LifeGame.EventStore;

    /// <summary>
    ///     The create game command handler.
    /// </summary>
    internal class CreateGameCommandHandler : ICommandHandler<CreateGameCommand>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateGameCommandHandler" /> class.
        /// </summary>
        /// <param name="repository">
        ///     The repository.
        /// </param>
        /// <param name="strategy">
        ///     The strategy to construct the universe.
        ///     The strategy.
        /// </param>
        public CreateGameCommandHandler(IDomainRepository<IDomainEvent> repository, IGameBoardStrategy strategy)
        {
            this.Repository = repository;
            this.GameBoardConstructor = strategy;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the game board constructor.
        /// </summary>
        private IGameBoardStrategy GameBoardConstructor { get; set; }

        /// <summary>
        ///     Gets or sets the repository.
        /// </summary>
        private IDomainRepository<IDomainEvent> Repository { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The execute.
        /// </summary>
        /// <param name="command">
        ///     The command.
        /// </param>
        public void Handle(CreateGameCommand command)
        {
            Game game = Game.CreateGame(command.IdGame, this.GameBoardConstructor, command.NumberOfCells);
            this.Repository.Add(game);
        }

        #endregion
    }
}