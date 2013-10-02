using LifeGame.Commands;
using LifeGame.Domain;
using LifeGame.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.CommandHandlers
{
    class CreateGameCommandHandler : ICommandHandler<CreateGameCommand>
    {
        public CreateGameCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            this.Repository = repository;
        }

        public void Execute(CreateGameCommand command)
        {
            var game = Game.CreateGame(command.NumberOfCells);
            this.Repository.Add(game);
        }

        private IDomainRepository<IDomainEvent> Repository { get; set; }
    }
}
