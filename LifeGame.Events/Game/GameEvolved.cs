// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEvolved.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The game evolved.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Events
{
    using System;

    /// <summary>
    ///     The game evolved.
    /// </summary>
    public class GameEvolved : DomainEvent
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameEvolved" /> class.
        /// </summary>
        /// <param name="idGame">
        ///     The id game.
        /// </param>
        public GameEvolved(Guid idGame)
        {
            this.IdGame = idGame;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the id game.
        /// </summary>
        public Guid IdGame { get; set; }

        #endregion
    }
}