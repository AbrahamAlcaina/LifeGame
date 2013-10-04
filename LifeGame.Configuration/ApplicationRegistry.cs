// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationRegistry.cs" company="">
//   
// </copyright>
// <summary>
//   The application registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



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

    /// <summary>
    /// The application registry.
    /// </summary>
    public class ApplicationRegistry
    {
        /// <summary>
        /// The registry.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public ApplicationRegistry Registry(Container container)
        {
            container.Register<IDomainEventStorage<IDomainEvent>, DomainEventStorage<IDomainEvent>>();
            container.Register<IIdentityMap<IDomainEvent>, EventStoreIdentityMap<IDomainEvent>>();
            container.Register<IEventStoreUnitOfWork<IDomainEvent>, EventStoreUnitOfWork<IDomainEvent>>();
            container.Register<IDomainRepository<IDomainEvent>, DomainRepository<IDomainEvent>>();
            return this;
        }

        /// <summary>
        /// The application boostrap.
        /// </summary>
        /// <param name="container"></param>
        /// <returns>
        /// The <see cref="ApplicationRegistry"/>.
        /// </returns>
        public static ApplicationRegistry ApplicationBoostrap(Container container)
        {
            return new ApplicationRegistry().Registry(container);
        }
    }
}