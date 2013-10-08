// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StartGameCommand.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The start game command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LifeGame.Commands
{
    using System;

    /// <summary>
    ///     The start game command.
    /// </summary>
    [Serializable]
    public class StartGameCommand : Command
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StartGameCommand"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="idGame">
        /// The id game.
        /// </param>
        public StartGameCommand(Guid id, Guid idGame)
            : base(id)
        {
            this.IdGame = idGame;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the id game.
        /// </summary>
        public Guid IdGame { get; set; }

        #endregion
    }
}