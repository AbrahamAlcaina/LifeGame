// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EvolveGameCommand.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The evolve game command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Commands
{
    using System;

    /// <summary>
    ///     The evolve game command.
    /// </summary>
    public class EvolveGameCommand : Command
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EvolveGameCommand" /> class.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="idGame">
        ///     The id game.
        /// </param>
        public EvolveGameCommand(Guid id, Guid idGame)
            : base(id)
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