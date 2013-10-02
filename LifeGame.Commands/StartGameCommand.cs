// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StartGameCommand.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Commands
{
    using System;

    /// <summary>
    /// The start game command.
    /// </summary>
    [Serializable]
    public class StartGameCommand : Command
    {
        public StartGameCommand(Guid id, Guid idGame)
            : base(id)
        {
            this.IdGame = idGame;
        }

        public Guid IdGame { get; set; }

    }
}