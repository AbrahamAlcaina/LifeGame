// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusRegistry.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The bus registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LifeGame.Bus.MemoryImplementation
{
    using LifeGame.CommandHandlers;

    using SimpleInjector;
    using SimpleInjector.Extensions;

    /// <summary>
    ///     The bus registry.
    /// </summary>
    public class BusRegistry
    {
        #region Public Methods and Operators

        /// <summary>
        /// The bus boostrap.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <returns>
        /// The <see cref="BusRegistry"/>.
        /// </returns>
        public static BusRegistry BusBoostrap(Container container)
        {
            return new BusRegistry().Registry(container);
        }

        /// <summary>
        /// The registry.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <returns>
        /// The <see cref="BusRegistry"/>.
        /// </returns>
        public BusRegistry Registry(Container container)
        {
            container.RegisterManyForOpenGeneric(
                typeof(ICommandHandler<>), 
                AccessibilityOption.AllTypes, 
                (serviceType, implTypes) => container.RegisterAll(serviceType, implTypes), 
                typeof(ICommandHandler<>).Assembly);

            container.Register<IBus, Bus>();
            container.Register<IBusBuilder, MemBusBuilder>();
            return this;
        }

        #endregion
    }
}