namespace LifeGame.CommandHandlers
{
    using LifeGame.EventStore;
    using System;

    public class TransactionHandler<TCommand, TCommandHandler>
        where TCommandHandler : ICommandHandler<TCommand>
        where TCommand : class
    {
        private readonly IUnitOfWork unitOfWork;

        public TransactionHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Execute(TCommand command, TCommandHandler commandHandler)
        {
            try
            {
                commandHandler.Execute(command);
                this.unitOfWork.Commit();
            }
            catch (Exception)
            {
                this.unitOfWork.Rollback();
                throw;
            }
        }
    }
}