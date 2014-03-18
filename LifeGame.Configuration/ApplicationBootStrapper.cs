// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationBootStrapper.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The application boot strapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Configuration
{
    using SimpleInjector;

    /// <summary>
    ///     The application boot strapper.
    /// </summary>
    public class ApplicationBootStrapper
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The boot strap.
        /// </summary>
        /// <returns>
        ///     The <see cref="Container" />.
        /// </returns>
        public static Container BootStrap()
        {
            return new ApplicationBootStrapper().BootStrapTheApplication();
        }

        /// <summary>
        ///     The boot strap the application.
        /// </summary>
        /// <returns>
        ///     The <see cref="Container" />.
        /// </returns>
        public Container BootStrapTheApplication()
        {
            var container = new Container();
            ApplicationRegistry.ApplicationBoostrap(container);
            DomainRegistry.DomainBoostrap(container);
            BusRegistry.BusBoostrap(container);
            EventStoreRegistry.EventStoreBoostrap(container);
            return container;
        }

        #endregion
    }
}