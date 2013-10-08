// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommand.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The Command interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LifeGame.Commands
{
    using System;

    /// <summary>
    ///     The Command interface.
    /// </summary>
    public interface ICommand
    {
        #region Public Properties

        /// <summary>
        ///     Gets the id.
        /// </summary>
        Guid Id { get; }

        #endregion
    }
}