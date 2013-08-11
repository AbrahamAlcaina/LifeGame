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
        /// <summary>
        /// Initializes a new instance of the <see cref="StartGameCommand"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public StartGameCommand(Guid id)
            : base(id)
        {
            
        }
    }
}