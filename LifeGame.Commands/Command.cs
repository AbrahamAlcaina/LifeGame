// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Command.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The base command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Commands
{
    using System;

    /// <summary>
    ///     The base command.
    /// </summary>
    [Serializable]
    public class Command : ICommand
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Command" /> class.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        public Command(Guid id)
        {
            this.Id = id;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the id.
        /// </summary>
        public Guid Id { get; private set; }

        #endregion
    }
}