// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemBusBuilder.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The mem bus builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Bus.MemoryImplementation
{
    using LifeGame.CommandHandlers;

    using MemBus;
    using MemBus.Configurators;

    using SimpleInjector;

    /// <summary>
    /// The mem bus builder.
    /// </summary>
    public class MemBusBuilder : IBusBuilder
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MemBusBuilder"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public MemBusBuilder(Container container)
        {
            this.Container = container;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        internal Container Container { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get bus.
        /// </summary>
        /// <returns>
        /// The <see cref="IBus"/>.
        /// </returns>
        public IBus GetBus()
        {
            return
                BusSetup.StartWith<AsyncConfiguration>()
                    .Apply<IoCSupport>(
                        s =>
                            s.SetAdapter(new BusIoCAdapter(this.Container))
                                .SetHandlerInterface(typeof(ICommandHandler<>)))
                    .Construct();
        }

        #endregion
    }
}