// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBusBuilder.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The BusBuilder interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Bus.MemoryImplementation
{
    using MemBus;

    /// <summary>
    ///     The BusBuilder interface.
    /// </summary>
    public interface IBusBuilder
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get bus.
        /// </summary>
        /// <returns>
        ///     The <see cref="IBus" />.
        /// </returns>
        IBus GetBus();

        #endregion
    }
}