// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainRegistry.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The domain registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Configuration
{
    using SimpleInjector;

    /// <summary>
    /// The domain registry.
    /// </summary>
    public class DomainRegistry
    {
        #region Public Methods and Operators

        /// <summary>
        /// The domain boostrap.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <returns>
        /// The <see cref="DomainRegistry"/>.
        /// </returns>
        public static DomainRegistry DomainBoostrap(Container container)
        {
            return new DomainRegistry().Registry(container);
        }

        /// <summary>
        /// The registry.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <returns>
        /// The <see cref="DomainRegistry"/>.
        /// </returns>
        public DomainRegistry Registry(Container container)
        {
            return this;
        }

        #endregion
    }
}