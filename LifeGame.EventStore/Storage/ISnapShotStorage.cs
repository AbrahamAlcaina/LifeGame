// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISnapShotStorage.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The SnapShotStorage interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Storage
{
    using System;

    /// <summary>
    ///     The SnapShotStorage interface.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public interface ISnapShotStorage<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get snap shot.
        /// </summary>
        /// <param name="entityId">
        ///     The entity id.
        /// </param>
        /// <returns>
        ///     The <see cref="ISnapShot" />.
        /// </returns>
        ISnapShot GetSnapShot(Guid entityId);

        /// <summary>
        ///     The save shap shot.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        void SaveShapShot(IEventProvider<TDomainEvent> entity);

        #endregion
    }
}