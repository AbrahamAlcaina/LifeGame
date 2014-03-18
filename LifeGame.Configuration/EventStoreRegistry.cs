// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventStoreRegistry.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The event store registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Configuration
{
    using LifeGame.EventStore;
    using LifeGame.EventStore.Implementation;
    using LifeGame.EventStore.Storage;

    using SimpleInjector;
    using SimpleInjector.Extensions;

    /// <summary>
    ///     The event store registry.
    /// </summary>
    public class EventStoreRegistry
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The event store boostrap.
        /// </summary>
        /// <param name="container">
        ///     The container.
        /// </param>
        /// <returns>
        ///     The <see cref="EventStoreRegistry" />.
        /// </returns>
        public static EventStoreRegistry EventStoreBoostrap(Container container)
        {
            return new EventStoreRegistry().Registry(container);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The registry.
        /// </summary>
        /// <param name="container">
        ///     The container.
        /// </param>
        /// <returns>
        ///     The <see cref="EventStoreRegistry" />.
        /// </returns>
        private EventStoreRegistry Registry(Container container)
        {
            container.Register<IEventStoreBuilder, EventStoreBuilder>();
            container.Register<IDomainRepository<IDomainEvent>, DomainRepository<IDomainEvent>>();

            container.RegisterManyForOpenGeneric(
                typeof(IDomainEventStorage<>),
                AccessibilityOption.AllTypes,
                container.RegisterAll,
                typeof(DomainEventStorage<>).Assembly);

            container.RegisterManyForOpenGeneric(
                typeof(IEventStoreUnitOfWork<>),
                AccessibilityOption.AllTypes,
                container.RegisterAll,
                typeof(IEventStoreUnitOfWork<>).Assembly);

            return this;
        }

        #endregion
    }
}