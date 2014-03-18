// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EvolveCommandHandler.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The start game command handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.CommandHandlers
{
    using LifeGame.Commands;
    using LifeGame.Domain;
    using LifeGame.EventStore;

    /// <summary>
    ///     The start game command handler.
    /// </summary>
    internal class EvolveCommandHandler : ICommandHandler<EvolveGameCommand>
    {
        #region Fields

        /// <summary>
        ///     The repository.
        /// </summary>
        private readonly IDomainRepository<IDomainEvent> repository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EvolveCommandHandler" /> class.
        /// </summary>
        /// <param name="repository">
        ///     The repository.
        /// </param>
        public EvolveCommandHandler(IDomainRepository<IDomainEvent> repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The execute.
        /// </summary>
        /// <param name="command">
        ///     The command.
        /// </param>
        public void Handle(EvolveGameCommand command)
        {
            var game = this.repository.GetById<Game>(command.IdGame);
            game.Evolve();
        }

        #endregion
    }
}