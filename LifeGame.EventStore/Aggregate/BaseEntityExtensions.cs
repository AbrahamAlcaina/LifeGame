// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEntityExtensions.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The try get by id extension.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Aggregate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The try get by id extension.
    /// </summary>
    public static class TryGetByIdExtension
    {
        #region Public Methods and Operators

        /// <summary>
        /// The try get value by id.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="Id">
        /// The id.
        /// </param>
        /// <param name="baseEntity">
        /// The base entity.
        /// </param>
        /// <typeparam name="TEventProvider">
        /// </typeparam>
        /// <typeparam name="TDomainEvent">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool TryGetValueById<TEventProvider, TDomainEvent>(
            this IEnumerable<TEventProvider> list, 
            Guid Id, 
            out IEntityEventProvider<TDomainEvent> baseEntity) where TEventProvider : IEntityEventProvider<TDomainEvent>
            where TDomainEvent : IDomainEvent
        {
            baseEntity = list.Where(x => x.Id == Id).FirstOrDefault();
            return baseEntity != null;
        }

        #endregion
    }
}