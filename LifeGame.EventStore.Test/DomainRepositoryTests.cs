// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainRepositoryTests.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The domain repository tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Tests
{
    using System.Transactions;

    using LifeGame.EventStore.Aggregate;
    using LifeGame.EventStore.Storage;
    using LifeGame.EventStore.Storage.Memento;

    using Moq;

    using Xunit;

    /// <summary>
    ///     The domain repository tests.
    /// </summary>
    public class DomainRepositoryTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The add aggregate with out transaction.
        /// </summary>
        [Fact]
        public void AddAggregateWithOutTransaction()
        {
            // arrange
            var eventStoreUnit = new Mock<IEventStoreUnitOfWork<IDomainEvent>>();
            var identityMap = new Mock<IIdentityMap<IDomainEvent>>();
            var aggregateRoot = new AggregateTest();
            var sut = new DomainRepositoryForTest(eventStoreUnit.Object, identityMap.Object);

            // act 
            sut.Add(aggregateRoot);

            // assert
            Assert.False(sut.Enlisted);
            Assert.False(sut.Committed);
            Assert.False(sut.Rollbacked);
        }

        /// <summary>
        ///     The automatic transation enlist and rollback.
        /// </summary>
        /// <exception cref="TransactionException">
        /// </exception>
        [Fact]
        public void AutomaticTransationEnlistAndRollback()
        {
            // arrange
            var eventStoreUnit = new Mock<IEventStoreUnitOfWork<IDomainEvent>>();
            var identityMap = new Mock<IIdentityMap<IDomainEvent>>();
            var aggregateRoot = new AggregateTest();
            var sut = new DomainRepositoryForTest(eventStoreUnit.Object, identityMap.Object);

            // act & assert
            Assert.Throws<TransactionException>(
                () =>
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        sut.Add(aggregateRoot);
                        throw new TransactionException();
                    }
                });
            Assert.False(sut.Enlisted);
            Assert.False(sut.Committed);
            Assert.True(sut.Rollbacked);
        }

        /// <summary>
        ///     The automatic transation enlist in add aggregate.
        /// </summary>
        [Fact]
        public void AutomaticTransationEnlistInAddAggregate()
        {
            // arrange
            var eventStoreUnit = new Mock<IEventStoreUnitOfWork<IDomainEvent>>();
            var identityMap = new Mock<IIdentityMap<IDomainEvent>>();
            var aggregateRoot = new AggregateTest();
            var sut = new DomainRepositoryForTest(eventStoreUnit.Object, identityMap.Object);

            // act
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                sut.Add(aggregateRoot);
                scope.Complete();
            }

            // assert
            Assert.True(sut.Enlisted);
            Assert.True(sut.Committed);
        }

        #endregion

        /// <summary>
        ///     The aggregate test.
        /// </summary>
        internal class AggregateTest : BaseAggregateRoot<IDomainEvent>, IOrginator
        {
            #region Public Methods and Operators

            /// <summary>
            ///     The create memento.
            /// </summary>
            /// <returns>
            ///     The <see cref="IMemento" />.
            /// </returns>
            public IMemento CreateMemento()
            {
                return null;
            }

            /// <summary>
            ///     The set memento.
            /// </summary>
            /// <param name="memento">
            ///     The memento.
            /// </param>
            public void SetMemento(IMemento memento)
            {
                // do nothing
            }

            #endregion
        }

        /// <summary>
        ///     The domain repository for test.
        /// </summary>
        internal class DomainRepositoryForTest : DomainRepository<IDomainEvent>
        {
            #region Constructors and Destructors

            /// <summary>
            ///     Initializes a new instance of the <see cref="DomainRepositoryForTest" /> class.
            /// </summary>
            /// <param name="eventStoreUnitOfWork">
            ///     The event store unit of work.
            /// </param>
            /// <param name="identityMap">
            ///     The identity map.
            /// </param>
            public DomainRepositoryForTest(
                IEventStoreUnitOfWork<IDomainEvent> eventStoreUnitOfWork,
                IIdentityMap<IDomainEvent> identityMap)
                : base(eventStoreUnitOfWork, identityMap)
            {
                this.Enlisted = false;
                this.Committed = false;
                this.Rollbacked = false;
            }

            #endregion

            #region Public Properties

            /// <summary>
            ///     Gets or sets a value indicating whether committed.
            /// </summary>
            public bool Committed { get; set; }

            /// <summary>
            ///     Gets or sets a value indicating whether enlisted.
            /// </summary>
            public bool Enlisted { get; set; }

            /// <summary>
            ///     Gets or sets a value indicating whether rollbacked.
            /// </summary>
            public bool Rollbacked { get; set; }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            ///     The commit.
            /// </summary>
            /// <param name="enlistment">
            ///     The enlistment.
            /// </param>
            public override void Commit(Enlistment enlistment)
            {
                this.Committed = true;
                base.Commit(enlistment);
            }

            /// <summary>
            ///     The prepare.
            /// </summary>
            /// <param name="preparingEnlistment">
            ///     The preparing enlistment.
            /// </param>
            public override void Prepare(PreparingEnlistment preparingEnlistment)
            {
                this.Enlisted = true;
                base.Prepare(preparingEnlistment);
            }

            /// <summary>
            ///     The rollback.
            /// </summary>
            /// <param name="enlistment">
            ///     The enlistment.
            /// </param>
            public override void Rollback(Enlistment enlistment)
            {
                this.Rollbacked = true;
                base.Rollback(enlistment);
            }

            #endregion
        }
    }
}