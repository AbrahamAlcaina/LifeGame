// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionHandlerDecorator.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The transaction handler decorator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.CommandHandlers
{
    using System.Transactions;

    using LifeGame.EventStore;

    /// <summary>
    ///     The transaction handler decorator.
    /// </summary>
    /// <typeparam name="TCommand">
    /// </typeparam>
    public class TransactionHandlerDecorator<TCommand> : IHandler<TCommand>
        where TCommand : class
    {
        #region Fields

        /// <summary>
        ///     The handler to call.
        /// </summary>
        private readonly IHandler<TCommand> handlerToCall;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransactionHandlerDecorator{TCommand}" /> class.
        /// </summary>
        /// <param name="decorated">
        ///     The decorated.
        /// </param>
        public TransactionHandlerDecorator(IHandler<TCommand> decorated)
        {
            this.handlerToCall = decorated;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The handle.
        /// </summary>
        /// <param name="command">
        ///     The command.
        /// </param>
        public void Handle(TCommand command)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                this.handlerToCall.Handle(command);
                scope.Complete();
            }
        }

        #endregion
    }
}