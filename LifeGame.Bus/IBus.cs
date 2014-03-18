// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBus.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The Bus interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Bus
{
    using System.Collections.Generic;

    /// <summary>
    ///     The Bus interface.
    /// </summary>
    public interface IBus : IUnitOfWork
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The publish.
        /// </summary>
        /// <param name="message">
        ///     The message.
        /// </param>
        void Publish(object message);

        /// <summary>
        ///     The publish.
        /// </summary>
        /// <param name="messages">
        ///     The messages.
        /// </param>
        void Publish(IEnumerable<object> messages);

        #endregion
    }
}