// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusRegistry.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The bus registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Configuration
{
    using System;

    using LifeGame.Bus;
    using LifeGame.Bus.MemoryImplementation;
    using LifeGame.CommandHandlers;
    using LifeGame.EventStore;

    using SimpleInjector;
    using SimpleInjector.Extensions;

    /// <summary>
    ///     The bus registry.
    /// </summary>
    public class BusRegistry
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The bus boostrap.
        /// </summary>
        /// <param name="container">
        ///     The container.
        /// </param>
        /// <returns>
        ///     The <see cref="BusRegistry" />.
        /// </returns>
        public static BusRegistry BusBoostrap(Container container)
        {
            return new BusRegistry().Registry(container);
        }

        /// <summary>
        ///     The registry.
        /// </summary>
        /// <param name="container">
        ///     The container.
        /// </param>
        /// <returns>
        ///     The <see cref="BusRegistry" />.
        /// </returns>
        public BusRegistry Registry(Container container)
        {
            // Register generic handlers
            container.RegisterManyForOpenGeneric(
                typeof(IHandler<>),
                AccessibilityOption.AllTypes,
                container.RegisterAll,
                typeof(IHandler<>).Assembly);

            // Register command handlers
            container.RegisterManyForOpenGeneric(
                typeof(IHandler<>),
                AccessibilityOption.AllTypes,
                container.RegisterAll,
                typeof(ICommandHandler<>).Assembly);

            // Register event handlers
            //container.RegisterManyForOpenGeneric(
            //    typeof(IHandler<>),
            //    AccessibilityOption.AllTypes,
            //    container.RegisterAll,
            //    typeof(IEventHandler<>).Assembly);

            // Decorator registration with predicates.
            container.RegisterDecorator(
                typeof(IHandler<>),
                typeof(TransactionHandlerWithMutexDecorator<>),
                c => IsMutexCommand(GetCommandType(c.ServiceType)));

            // Decorator registration with predicates.
            container.RegisterDecorator(
                typeof(IHandler<>),
                typeof(TransactionHandlerDecorator<>),
                c => !IsMutexCommand(GetCommandType(c.ServiceType)));

            container.RegisterSingle<IBus, Bus>();
            container.RegisterSingle<IBusBuilder, MemBusBuilder>();

            return this;
        }

        #endregion

        // Some helper methods

        #region Methods

        /// <summary>
        ///     The get command type.
        /// </summary>
        /// <param name="handlerType">
        ///     The handler type.
        /// </param>
        /// <returns>
        ///     The <see cref="Type" />.
        /// </returns>
        private static Type GetCommandType(Type handlerType)
        {
            return handlerType.GetGenericArguments()[0];
        }

        /// <summary>
        ///     The is mutex command.
        /// </summary>
        /// <param name="commandType">
        ///     The command type.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        private static bool IsMutexCommand(Type commandType)
        {
            // Determine here is a class is a mutex command. 
            // Example:
            return typeof(IMutexCommandHandler<>).IsAssignableFrom(commandType);
        }

        #endregion
    }
}