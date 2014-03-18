// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionHandlerWithMutexDecorator.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The transaction handler with mutex decorator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.CommandHandlers
{
    using LifeGame.EventStore;

    /// <summary>
    ///     The transaction handler with mutex decorator.
    /// </summary>
    /// <typeparam name="TCommand">
    /// </typeparam>
    public class TransactionHandlerWithMutexDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : class
    {
        #region Static Fields

        /// <summary>
        ///     The lock.
        /// </summary>
        private static readonly object Lock = new object();

        #endregion

        #region Fields

        /// <summary>
        ///     The handler to call.
        /// </summary>
        private readonly IHandler<TCommand> handlerToCall;

        /// <summary>
        ///     The unit of work.
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransactionHandlerWithMutexDecorator{TCommand}" /> class.
        /// </summary>
        /// <param name="unitOfWork">
        ///     The unit of work.
        /// </param>
        /// <param name="decorated">
        ///     The decorated.
        /// </param>
        public TransactionHandlerWithMutexDecorator(IUnitOfWork unitOfWork, IHandler<TCommand> decorated)
        {
            this.handlerToCall = decorated;
            this.unitOfWork = unitOfWork;
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
            lock (Lock)
            {
                this.handlerToCall.Handle(command);
                this.unitOfWork.Commit();
            }
        }

        #endregion
    }
}