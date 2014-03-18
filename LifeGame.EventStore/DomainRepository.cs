// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainRepository.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The domain repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore
{
    using System;
    using System.Transactions;

    using LifeGame.EventStore.Storage;
    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    ///     The domain repository.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// </typeparam>
    public class DomainRepository<TDomainEvent> : IDomainRepository<TDomainEvent>, IEnlistmentNotification
        where TDomainEvent : IDomainEvent
    {
        #region Fields

        /// <summary>
        ///     The _event store unit of work.
        /// </summary>
        private readonly IEventStoreUnitOfWork<TDomainEvent> _eventStoreUnitOfWork;

        /// <summary>
        ///     The _identity map.
        /// </summary>
        private readonly IIdentityMap<TDomainEvent> _identityMap;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainRepository{TDomainEvent}" /> class.
        /// </summary>
        /// <param name="eventStoreUnitOfWork">
        ///     The event store unit of work.
        /// </param>
        /// <param name="identityMap">
        ///     The identity map.
        /// </param>
        public DomainRepository(
            IEventStoreUnitOfWork<TDomainEvent> eventStoreUnitOfWork,
            IIdentityMap<TDomainEvent> identityMap)
        {
            this._eventStoreUnitOfWork = eventStoreUnitOfWork;
            this._identityMap = identityMap;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add.
        /// </summary>
        /// <param name="aggregateRoot">
        ///     The aggregate root.
        /// </param>
        /// <typeparam name="TAggregate">
        /// </typeparam>
        public void Add<TAggregate>(TAggregate aggregateRoot)
            where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new()
        {
            this.EnlistTransacionIfItsNedeed();
            this._eventStoreUnitOfWork.Add(aggregateRoot);
        }

        /// <summary>
        ///     The commit.
        /// </summary>
        /// <param name="enlistment">
        ///     The enlistment.
        /// </param>
        public virtual void Commit(Enlistment enlistment)
        {
            this._eventStoreUnitOfWork.Commit();
            enlistment.Done();
        }

        /// <summary>
        ///     The get by id.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <typeparam name="TAggregate">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="TAggregate" />.
        /// </returns>
        public TAggregate GetById<TAggregate>(Guid id)
            where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new()
        {
            this.EnlistTransacionIfItsNedeed();
            return this.RegisterForTracking(this._identityMap.GetById<TAggregate>(id))
                   ?? this._eventStoreUnitOfWork.GetById<TAggregate>(id);
        }

        /// <summary>
        ///     The in doubt.
        /// </summary>
        /// <param name="enlistment">
        ///     The enlistment.
        /// </param>
        public virtual void InDoubt(Enlistment enlistment)
        {
            enlistment.Done();
        }

        /// <summary>
        ///     The prepare.
        /// </summary>
        /// <param name="preparingEnlistment">
        ///     The preparing enlistment.
        /// </param>
        public virtual void Prepare(PreparingEnlistment preparingEnlistment)
        {
            preparingEnlistment.Prepared();
        }

        /// <summary>
        ///     The rollback.
        /// </summary>
        /// <param name="enlistment">
        ///     The enlistment.
        /// </param>
        public virtual void Rollback(Enlistment enlistment)
        {
            this._eventStoreUnitOfWork.Rollback();
            enlistment.Done();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The enlist transacion if its nedeed.
        /// </summary>
        private void EnlistTransacionIfItsNedeed()
        {
            if (Transaction.Current != null)
            {
                Transaction.Current.EnlistVolatile(this, EnlistmentOptions.None);
            }
        }

        /// <summary>
        ///     The register for tracking.
        /// </summary>
        /// <param name="aggregateRoot">
        ///     The aggregate root.
        /// </param>
        /// <typeparam name="TAggregate">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="TAggregate" />.
        /// </returns>
        private TAggregate RegisterForTracking<TAggregate>(TAggregate aggregateRoot)
            where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new()
        {
            if (aggregateRoot == null)
            {
                return aggregateRoot;
            }

            this._eventStoreUnitOfWork.RegisterForTracking(aggregateRoot);
            return aggregateRoot;
        }

        #endregion
    }
}