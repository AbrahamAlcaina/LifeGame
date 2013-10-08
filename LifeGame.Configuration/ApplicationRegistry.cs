// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationRegistry.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The application registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LifeGame.Configuration
{
    using LifeGame.EventStore;
    using LifeGame.EventStore.Implementation;
    using LifeGame.EventStore.Storage;

    using SimpleInjector;

    /// <summary>
    ///     The application registry.
    /// </summary>
    public class ApplicationRegistry
    {
        #region Public Methods and Operators

        /// <summary>
        /// The application boostrap.
        /// </summary>
        /// <param name="container">
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationRegistry"/>.
        /// </returns>
        public static ApplicationRegistry ApplicationBoostrap(Container container)
        {
            return new ApplicationRegistry().Registry(container);
        }

        /// <summary>
        /// The registry.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationRegistry"/>.
        /// </returns>
        public ApplicationRegistry Registry(Container container)
        {
            container.Register<IDomainEventStorage<IDomainEvent>, DomainEventStorage<IDomainEvent>>();
            container.Register<IIdentityMap<IDomainEvent>, EventStoreIdentityMap<IDomainEvent>>();
            container.Register<IEventStoreUnitOfWork<IDomainEvent>, EventStoreUnitOfWork<IDomainEvent>>();
            container.Register<IDomainRepository<IDomainEvent>, DomainRepository<IDomainEvent>>();
            return this;
        }

        #endregion
    }
}