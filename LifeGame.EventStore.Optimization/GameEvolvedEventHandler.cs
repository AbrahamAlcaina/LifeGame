using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.EventStorage.Optimization
{
    using LifeGame.Domain;
    using LifeGame.EventHandlers;
    using LifeGame.Events;
    using LifeGame.EventStore;
    using LifeGame.EventStore.Storage;

    public class GameEvolvedEventHandler : IEventHandler<GameEvolved>
    {
        public IDomainEventStorage<IDomainEvent> DomainEventStorage { get; set; }
        public IDomainRepository<IDomainEvent> DomainRepository { get; set; }

        public GameEvolvedEventHandler(IDomainEventStorage<IDomainEvent> domainEventStorage, IDomainRepository<IDomainEvent> domainRepository)
        {
            this.DomainEventStorage = domainEventStorage;
            this.DomainRepository = domainRepository;
        }

        public void Handle(GameEvolved @event)
        {
            var game = this.DomainRepository.GetById<Game>(@event.IdGame);
            this.DomainEventStorage.SaveShapShot(game);
        }
    }
}
