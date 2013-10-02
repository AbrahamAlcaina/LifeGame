namespace LifeGame.CommandHandlers
{
    using LifeGame.Commands;
    using LifeGame.Domain;
    using LifeGame.EventStore;

    internal class StartGameCommandHandler : ICommandHandler<StartGameCommand>
    {
        #region Fields

        private readonly IDomainRepository<IDomainEvent> repository;

        #endregion

        #region Constructors and Destructors

        public StartGameCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Public Methods and Operators

        public void Execute(StartGameCommand command)
        {
            var game = this.repository.GetById<Game>(command.IdGame);
        }

        #endregion
    }
}