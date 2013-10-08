// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameCreated.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The game created.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Events
{
    using System;

    /// <summary>
    /// The game created.
    /// </summary>
    internal class GameCreated : DomainEvent
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the id game.
        /// </summary>
        public Guid IdGame { get; set; }

        #endregion
    }
}