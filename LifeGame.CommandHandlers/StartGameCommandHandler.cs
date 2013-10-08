// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StartGameCommandHandler.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The start game command handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.CommandHandlers
{
    using LifeGame.Commands;
    using LifeGame.Domain;
    using LifeGame.EventStore;

    /// <summary>
    /// The start game command handler.
    /// </summary>
    internal class StartGameCommandHandler : ICommandHandler<StartGameCommand>
    {
        #region Fields

        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IDomainRepository<IDomainEvent> repository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StartGameCommandHandler"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public StartGameCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        public void Execute(StartGameCommand command)
        {
            var game = this.repository.GetById<Game>(command.IdGame);
        }

        #endregion
    }
}