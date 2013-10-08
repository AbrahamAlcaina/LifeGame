// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bus.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The bus.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Bus.MemoryImplementation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The bus.
    /// </summary>
    public class Bus : IBus
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Bus"/> class.
        /// </summary>
        /// <param name="busBuilder">
        /// The bus builder.
        /// </param>
        public Bus(IBusBuilder busBuilder)
        {
            this.MemoryBus = busBuilder.GetBus();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the memory bus.
        /// </summary>
        internal MemBus.IBus MemoryBus { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The commit.
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// </exception>
        public void Commit()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The publish.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Publish(object message)
        {
            this.MemoryBus.Publish(message);
        }

        /// <summary>
        /// The publish.
        /// </summary>
        /// <param name="messages">
        /// The messages.
        /// </param>
        public void Publish(IEnumerable<object> messages)
        {
            foreach (object message in messages)
            {
                Publish(message);
            }
        }

        /// <summary>
        /// The rollback.
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// </exception>
        public void Rollback()
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}