// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandHandler.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The CommandHandler interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.CommandHandlers
{
    /// <summary>
    /// The CommandHandler interface.
    /// </summary>
    /// <typeparam name="TCommand">
    /// </typeparam>
    public interface ICommandHandler<in TCommand>
        where TCommand : class
    {
        #region Public Methods and Operators

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        void Execute(TCommand command);

        #endregion
    }
}