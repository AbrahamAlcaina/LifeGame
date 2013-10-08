// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoCBusAdapter.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The bus io c adapter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Bus.MemoryImplementation
{
    using System;
    using System.Collections.Generic;

    using MemBus;

    using SimpleInjector;

    /// <summary>
    /// The bus io c adapter.
    /// </summary>
    internal class BusIoCAdapter : IocAdapter
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BusIoCAdapter"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public BusIoCAdapter(Container container)
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
        /// The get all instances.
        /// </summary>
        /// <param name="desiredType">
        /// The desired type.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<object> GetAllInstances(Type desiredType)
        {
            return this.Container.GetAllInstances(desiredType);
        }

        #endregion
    }
}