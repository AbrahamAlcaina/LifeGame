using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Configuration
{
    using LifeGame.EventStore;
    using LifeGame.EventStore.Implementation;
    using LifeGame.EventStore.Storage;

    using SimpleInjector;
    using SimpleInjector.Extensions;

    public class EventStoreRegistry
    {

        public static EventStoreRegistry EventStoreBoostrap(Container container)
        {
            return new EventStoreRegistry().Registry(container);
        }

        private EventStoreRegistry Registry(Container container)
        {
            container.Register<IEventStoreBuilder, EventStoreBuilder>();
            container.RegisterManyForOpenGeneric(
                typeof(IDomainEventStorage<>),
                AccessibilityOption.AllTypes,
                (serviceType, implTypes) => container.RegisterAll(serviceType, implTypes),
                typeof(IDomainEventStorage<>).Assembly);
            container.RegisterManyForOpenGeneric(
                typeof(IEventStoreUnitOfWork<>),
                AccessibilityOption.AllTypes,
                (serviceType, implTypes) => container.RegisterAll(serviceType, implTypes),
                typeof(IEventStoreUnitOfWork<>).Assembly);
            return this;

        }
    }
}
