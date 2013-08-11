// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseCommand.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Commands
{
    using System;

    /// <summary>
    /// The base command.
    /// </summary>
    [Serializable]
    public class Command : ICommand
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public BaseCommand(Guid id)
        {
            this.Id = id;
        }
    }
}