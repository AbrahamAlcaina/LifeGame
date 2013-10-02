using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Configuration
{
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    using LifeGame.Bus;    
    using LifeGame.EventStore;
    using LifeGame.EventStore.Implementation;
    using LifeGame.EventStore.Storage;

    using SimpleInjector;

    using IUnitOfWork = LifeGame.Bus.IUnitOfWork;

    class ApplicationRegistry
    {
        public void Registry(Container container)
        {   
            container.Register<IDomainEventStorage<IDomainEvent>, DomainEventStorage<IDomainEvent>>();
            container.Register<IIdentityMap<IDomainEvent>, EventStoreIdentityMap<IDomainEvent>>();
            container.Register<IEventStoreUnitOfWork<IDomainEvent>, EventStoreUnitOfWork<IDomainEvent>>();
            container.Register<IDomainRepository<IDomainEvent>, DomainRepository<IDomainEvent>>();
        }
    }
}
