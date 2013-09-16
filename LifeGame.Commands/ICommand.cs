// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommand.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The Command interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Commands
{
    using System;

    /// <summary>
    /// The Command interface.
    /// </summary>
    internal interface ICommand
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        Guid Id { get; }

        
    }
}