// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityList.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The entity list.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Aggregate
{
    using System.Collections.Generic;

    /// <summary>
    ///     The entity list.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public class EntityList<TEntity, TDomainEvent> : List<TEntity>
        where TEntity : IEntityEventProvider<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        #region Fields

        /// <summary>
        ///     The _aggregate root.
        /// </summary>
        private readonly IRegisterChildEntities<TDomainEvent> _aggregateRoot;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityList{TEntity,TDomainEvent}" /> class.
        /// </summary>
        /// <param name="aggregateRoot">
        ///     The aggregate root.
        /// </param>
        public EntityList(IRegisterChildEntities<TDomainEvent> aggregateRoot)
        {
            this._aggregateRoot = aggregateRoot;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityList{TEntity,TDomainEvent}" /> class.
        /// </summary>
        /// <param name="aggregateRoot">
        ///     The aggregate root.
        /// </param>
        /// <param name="capacity">
        ///     The capacity.
        /// </param>
        public EntityList(IRegisterChildEntities<TDomainEvent> aggregateRoot, int capacity)
            : base(capacity)
        {
            this._aggregateRoot = aggregateRoot;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityList{TEntity,TDomainEvent}" /> class.
        /// </summary>
        /// <param name="aggregateRoot">
        ///     The aggregate root.
        /// </param>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        public EntityList(IRegisterChildEntities<TDomainEvent> aggregateRoot, IEnumerable<TEntity> collection)
            : base(collection)
        {
            this._aggregateRoot = aggregateRoot;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        public new void Add(TEntity entity)
        {
            this._aggregateRoot.RegisterChildEventProvider(entity);
            base.Add(entity);
        }

        #endregion
    }
}