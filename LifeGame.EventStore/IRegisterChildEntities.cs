// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegisterChildEntities.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The RegisterChildEntities interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore
{
    /// <summary>
    /// The RegisterChildEntities interface.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public interface IRegisterChildEntities<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register child event provider.
        /// </summary>
        /// <param name="entityEventProvider">
        /// The entity event provider.
        /// </param>
        void RegisterChildEventProvider(IEntityEventProvider<TDomainEvent> entityEventProvider);

        #endregion
    }
}