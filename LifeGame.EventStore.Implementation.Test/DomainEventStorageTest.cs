// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainEventStorageTest.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The domain event storage test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Implementation.Test
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SQLite;
    using System.Linq;

    using LifeGame.EventStore.Storage;

    using Moq;

    using NEventStore;
    using NEventStore.Persistence;

    using Xunit;

    /// <summary>
    ///     The domain event storage test.
    /// </summary>
    public class DomainEventStorageTest
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The begin transaction test.
        /// </summary>
        [Fact]
        public void BeginTransactionTest()
        {
            // arrange 
            DomainEventStorage<IDomainEvent> sut = this.GetFakeEventStore();

            // act 
            sut.BeginTransaction();

            // assert
            Assert.NotNull(sut.TransactionScope);
        }

        /// <summary>
        ///     The begin transaction two at the same time test.
        /// </summary>
        [Fact]
        public void BeginTransactionTwoAtTheSameTimeTest()
        {
            // arrange 
            DomainEventStorage<IDomainEvent> sut = this.GetFakeEventStore();
            sut.BeginTransaction();

            // assert
            Assert.Throws<AllReadyInTransactionException>(() => sut.BeginTransaction());
        }

        /// <summary>
        ///     The commit test.
        /// </summary>
        [Fact]
        public void CommitTest()
        {
            // arrange 
            DomainEventStorage<IDomainEvent> sut = this.GetFakeEventStore();
            sut.BeginTransaction();

            // act 
            sut.Commit();

            // assert
            Assert.Null(sut.TransactionScope);
        }

        /// <summary>
        ///     The constructor test.
        /// </summary>
        [Fact]
        public void ConstructorTest()
        {
            // arrange
            var eventStoreBuilder = new EventStoreBuilder();

            // act
            var sut = new DomainEventStorage<IDomainEvent>(eventStoreBuilder);

            // assert
            Assert.NotNull(sut);
        }

        /// <summary>
        ///     The get all events test.
        /// </summary>
        [Fact]
        public void GetAllEventsTest()
        {
            // arrange 
            Guid id = Guid.NewGuid();
            var stream = new Mock<IEventStream>();
            stream.Setup(s => s.CommittedEvents)
                .Returns(new List<EventMessage> { new EventMessage { Body = new EntityCreated(id, 0) } });
            stream.Setup(s => s.UncommittedEvents)
                .Returns(new List<EventMessage> { new EventMessage { Body = new EntityChangedNameEvent("NewName") } });
            DomainEventStorage<IDomainEvent> sut = this.GetFakeEventStore(stream);

            // act
            IEnumerable<IDomainEvent> events = sut.GetAllEvents(id);

            // assert
            Assert.Equal(2, events.Count());
        }

        /// <summary>
        ///     The get event count since last snap shot test.
        /// </summary>
        [Fact]
        public void GetEventCountSinceLastSnapShotTest()
        {
            // arrange
            Guid id = Guid.NewGuid();
            var stream = new Mock<IEventStream>();
            var snapshot = new Snapshot(id, 0, new Entity(Guid.NewGuid(), 0));
            var store = new Mock<IStoreEvents>();
            var advanced = new Mock<IPersistStreams>();
            DomainEventStorage<IDomainEvent> sut = this.FakeEventStoreForSnapshots(
                id,
                stream,
                snapshot,
                store,
                advanced);

            // act 
            int countEvents = sut.GetEventCountSinceLastSnapShot(id);

            // assert
            Assert.Equal(1, countEvents);
            advanced.Verify(a => a.GetSnapshot(id, int.MaxValue));
            store.Verify(s => s.OpenStream(snapshot, int.MaxValue));
        }

        /// <summary>
        ///     The get events since last snap shot test.
        /// </summary>
        [Fact]
        public void GetEventsSinceLastSnapShotTest()
        {
            // arrange
            Guid id = Guid.NewGuid();
            var stream = new Mock<IEventStream>();
            var snapshot = new Snapshot(id, 0, new Entity(Guid.NewGuid(), 0));
            var store = new Mock<IStoreEvents>();
            var advanced = new Mock<IPersistStreams>();
            DomainEventStorage<IDomainEvent> sut = this.FakeEventStoreForSnapshots(
                id,
                stream,
                snapshot,
                store,
                advanced);

            // act 
            IEnumerable<IDomainEvent> events = sut.GetEventsSinceLastSnapShot(id);

            // assert
            Assert.Equal(1, events.Count());
            advanced.Verify(a => a.GetSnapshot(id, int.MaxValue));
            store.Verify(s => s.OpenStream(snapshot, int.MaxValue));
        }

        /// <summary>
        ///     The get snap shot test.
        /// </summary>
        [Fact]
        public void GetSnapShotTest()
        {
            // arrange
            var id = new Guid("a12c64c5-cd10-439d-bce1-0ab50e7cb26d");
            var store = new Mock<IStoreEvents>();
            var advanced = new Mock<IPersistStreams>();
            var memento = new EntityMemento(id, 0, "Name");

            advanced.Setup(a => a.GetSnapshot(id, int.MaxValue))
                .Returns(new Snapshot(Guid.NewGuid(), int.MaxValue, memento));
            store.Setup(s => s.Advanced).Returns(advanced.Object);

            DomainEventStorage<IDomainEvent> sut = this.GetFakeEventStore(store.Object);

            // act 
            ISnapShot snapShot = sut.GetSnapShot(id);

            // assert
            store.Verify(s => s.Advanced);
            advanced.Verify(a => a.GetSnapshot(id, int.MaxValue));
        }

        /// <summary>
        ///     The rollback test.
        /// </summary>
        [Fact]
        public void RollbackTest()
        {
            // arrange 
            DomainEventStorage<IDomainEvent> sut = this.GetFakeEventStore();
            sut.BeginTransaction();

            // act 
            sut.Rollback();

            // assert
            Assert.Null(sut.TransactionScope);
        }

        /// <summary>
        ///     The save integration db test.
        /// </summary>
        [Fact(Skip = "Integration Test")]
        public void SaveIntegrationDBTest()
        {
            // arrange
            DomainEventStorage<IDomainEvent> sut = this.GetEventStore();
            const string Newname = "NewName";
            var domainEntity = new Entity();
            domainEntity.ChangeName(Newname);

            // act
            sut.Save(domainEntity);

            // assert
            Assert.Equal(2, sut.EventStorage.OpenStream(domainEntity.Id, 0, int.MaxValue).CommittedEvents.Count);
        }

        /// <summary>
        ///     The save shap shot test.
        /// </summary>
        [Fact]
        public void SaveShapShotTest()
        {
            // arrange 
            var entity = new Entity();
            var store = new Mock<IStoreEvents>();
            var advanced = new Mock<IPersistStreams>();

            store.Setup(s => s.Advanced).Returns(advanced.Object);
            DomainEventStorage<IDomainEvent> sut = this.GetFakeEventStore(store.Object);

            // act
            sut.SaveShapShot(entity);

            // assert
            store.Verify(s => s.Advanced);
            advanced.Verify(a => a.AddSnapshot(It.IsAny<Snapshot>()));
        }

        /// <summary>
        ///     The save test.
        /// </summary>
        [Fact]
        public void SaveTest()
        {
            // arrange
            var stream = new Mock<IEventStream>();
            var store = new Mock<IStoreEvents>();
            store.Setup(s => s.OpenStream(It.IsAny<Guid>(), 0, int.MaxValue)).Returns(stream.Object);
            DomainEventStorage<IDomainEvent> sut = this.GetFakeEventStore(store.Object);

            var domainEntity = new Entity();

            // act
            sut.Save(domainEntity);

            // assert
            store.Verify(s => s.OpenStream(domainEntity.Id, 0, int.MaxValue));
            stream.Verify(s => s.Add(It.IsAny<EventMessage>()), Times.Exactly(1));
            stream.Verify(s => s.CommitChanges(It.IsAny<Guid>()));
        }

        /// <summary>
        ///     The save with incorrect version test.
        /// </summary>
        [Fact]
        public void SaveWithIncorrectVersionTest()
        {
            // arrange
            DomainEventStorage<IDomainEvent> sut = this.GetEventStore();
            var domainEntity = new Entity(2); // Version 2

            // act & assert
            Assert.Throws<ConcurrencyViolationException>(() => sut.Save(domainEntity));
        }

        /// <summary>
        ///     The sql is installed.
        /// </summary>
        [Fact]
        public void SqlIsInstalled()
        {
            // arrange
            string connectionString = ConfigurationManager.ConnectionStrings["NEventStore"].ConnectionString;
            var sut = new SQLiteConnection(connectionString);

            // act
            sut.Open();
            sut.Close();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The fake event store for snapshots.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="stream">
        ///     The stream.
        /// </param>
        /// <param name="snapshot">
        ///     The snapshot.
        /// </param>
        /// <param name="store">
        ///     The store.
        /// </param>
        /// <param name="advanced">
        ///     The advanced.
        /// </param>
        /// <returns>
        ///     The <see cref="DomainEventStorage" />.
        /// </returns>
        private DomainEventStorage<IDomainEvent> FakeEventStoreForSnapshots(
            Guid id,
            Mock<IEventStream> stream,
            Snapshot snapshot,
            Mock<IStoreEvents> store,
            Mock<IPersistStreams> advanced)
        {
            advanced.Setup(a => a.GetSnapshot(id, int.MaxValue)).Returns(snapshot);
            store.Setup(s => s.Advanced).Returns(advanced.Object);
            store.Setup(s => s.OpenStream(snapshot, int.MaxValue)).Returns(stream.Object);
            stream.Setup(s => s.CommittedEvents)
                .Returns(new List<EventMessage> { new EventMessage { Body = new EntityCreated(id, 0) } });

            return this.GetFakeEventStore(store, stream);
        }

        /// <summary>
        ///     The get event store.
        /// </summary>
        /// <returns>
        ///     The <see cref="DomainEventStorage" />.
        /// </returns>
        private DomainEventStorage<IDomainEvent> GetEventStore()
        {
            var eventStoreBuilder = new EventStoreBuilder();

            return new DomainEventStorage<IDomainEvent>(eventStoreBuilder);
        }

        /// <summary>
        ///     The get fake event store.
        /// </summary>
        /// <returns>
        ///     The <see cref="DomainEventStorage" />.
        /// </returns>
        private DomainEventStorage<IDomainEvent> GetFakeEventStore()
        {
            var store = new Mock<IStoreEvents>();
            return GetFakeEventStore(store.Object);
        }

        /// <summary>
        ///     The get fake event store.
        /// </summary>
        /// <param name="streamMock">
        ///     The stream mock.
        /// </param>
        /// <returns>
        ///     The <see cref="DomainEventStorage" />.
        /// </returns>
        private DomainEventStorage<IDomainEvent> GetFakeEventStore(Mock<IEventStream> streamMock)
        {
            var store = new Mock<IStoreEvents>();
            return this.GetFakeEventStore(store, streamMock);
        }

        /// <summary>
        ///     The get fake event store.
        /// </summary>
        /// <param name="store">
        ///     The store.
        /// </param>
        /// <returns>
        ///     The <see cref="DomainEventStorage" />.
        /// </returns>
        private DomainEventStorage<IDomainEvent> GetFakeEventStore(IStoreEvents store)
        {
            var storeBuilder = new Mock<IEventStoreBuilder>();
            storeBuilder.Setup(s => s.GetEventStore()).Returns(store);
            return new DomainEventStorage<IDomainEvent>(storeBuilder.Object);
        }

        /// <summary>
        ///     The get fake event store.
        /// </summary>
        /// <param name="store">
        ///     The store.
        /// </param>
        /// <param name="streamMock">
        ///     The stream mock.
        /// </param>
        /// <returns>
        ///     The <see cref="DomainEventStorage" />.
        /// </returns>
        private DomainEventStorage<IDomainEvent> GetFakeEventStore(
            Mock<IStoreEvents> store,
            Mock<IEventStream> streamMock)
        {
            var storeBuilder = new Mock<IEventStoreBuilder>();
            storeBuilder.Setup(s => s.GetEventStore()).Returns(store.Object);
            return new DomainEventStorageFake(storeBuilder.Object, streamMock);
        }

        #endregion

        /// <summary>
        ///     The domain event storage fake.
        /// </summary>
        internal class DomainEventStorageFake : DomainEventStorage<IDomainEvent>
        {
            #region Constructors and Destructors

            /// <summary>
            ///     Initializes a new instance of the <see cref="DomainEventStorageFake" /> class.
            /// </summary>
            /// <param name="builder">
            ///     The builder.
            /// </param>
            /// <param name="mock">
            ///     The mock.
            /// </param>
            public DomainEventStorageFake(IEventStoreBuilder builder, Mock<IEventStream> mock)
                : base(builder)
            {
                this.StreamMock = mock;
            }

            #endregion

            #region Public Properties

            /// <summary>
            ///     Gets or sets the stream mock.
            /// </summary>
            public Mock<IEventStream> StreamMock { get; set; }

            #endregion

            #region Methods

            /// <summary>
            ///     The get stream.
            /// </summary>
            /// <param name="id">
            ///     The id.
            /// </param>
            /// <returns>
            ///     The <see cref="IEventStream" />.
            /// </returns>
            protected override IEventStream GetStream(Guid id)
            {
                return this.StreamMock.Object;
            }

            #endregion
        }
    }
}