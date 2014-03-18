// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnregisteredDomainEventException.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The unregistered domain event exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Aggregate
{
    using System;

    /// <summary>
    ///     The unregistered domain event exception.
    /// </summary>
    [Serializable]
    public class UnregisteredDomainEventException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnregisteredDomainEventException" /> class.
        /// </summary>
        /// <param name="message">
        ///     The message.
        /// </param>
        public UnregisteredDomainEventException(string message)
            : base(message)
        {
        }

        #endregion
    }
}