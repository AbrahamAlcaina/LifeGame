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

    public class DomainEventStorageTest
    {
        #region Public Methods and Operators

        [Fact]
        public void BeginTransactionTest()
        {
            // arrange 
            var sut = this.GetFakeEventStore();

            // act 
            sut.BeginTransaction();

            // assert
            Assert.NotNull(sut.TransactionScope);
        }

        [Fact]
        public void BeginTransactionTwoAtTheSameTimeTest()
        {
            // arrange 
            var sut = this.GetFakeEventStore();
            sut.BeginTransaction();

            // assert
            Assert.Throws<AllReadyInTransactionException>(() => sut.BeginTransaction());
        }

        [Fact]
        public void CommitTest()
        {
            // arrange 
            var sut = this.GetFakeEventStore();
            sut.BeginTransaction();

            // act 
            sut.Commit();

            // assert
            Assert.Null(sut.TransactionScope);
        }

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

        [Fact]
        public void GetAllEventsTest()
        {
            // arrange 
            var id = Guid.NewGuid();
            var stream = new Mock<IEventStream>();
            stream.Setup(s => s.CommittedEvents)
                .Returns(new List<EventMessage> { new EventMessage { Body = new EntityCreated(id, 0) } });
            stream.Setup(s => s.UncommittedEvents)
                .Returns(new List<EventMessage> { new EventMessage { Body = new EntityChangedNameEvent("NewName") } });
            var sut = this.GetFakeEventStore(stream);

            // act
            var events = sut.GetAllEvents(id);

            // assert
            Assert.Equal(2, events.Count());
        }

        [Fact]
        public void GetEventCountSinceLastSnapShotTest()
        {
            // arrange
            var id = Guid.NewGuid();
            var stream = new Mock<IEventStream>();
            var snapshot = new Snapshot(id, 0, new Entity(Guid.NewGuid(), 0));
            var store = new Mock<IStoreEvents>();
            var advanced = new Mock<IPersistStreams>();
            var sut = this.FakeEventStoreForSnapshots(id, stream, snapshot, store, advanced);

            // act 
            var countEvents = sut.GetEventCountSinceLastSnapShot(id);

            // assert
            Assert.Equal(1, countEvents);
            advanced.Verify(a => a.GetSnapshot(id, int.MaxValue));
            store.Verify(s => s.OpenStream(snapshot, int.MaxValue));
        }

        [Fact]
        public void GetEventsSinceLastSnapShotTest()
        {
            // arrange
            var id = Guid.NewGuid();
            var stream = new Mock<IEventStream>();
            var snapshot = new Snapshot(id, 0, new Entity(Guid.NewGuid(), 0));
            var store = new Mock<IStoreEvents>();
            var advanced = new Mock<IPersistStreams>();
            var sut = this.FakeEventStoreForSnapshots(id, stream, snapshot, store, advanced);

            // act 
            var events = sut.GetEventsSinceLastSnapShot(id);

            // assert
            Assert.Equal(1, events.Count());
            advanced.Verify(a => a.GetSnapshot(id, int.MaxValue));
            store.Verify(s => s.OpenStream(snapshot, int.MaxValue));
        }

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

            var sut = this.GetFakeEventStore(store.Object);

            // act 
            var snapShot = sut.GetSnapShot(id);

            // assert
            store.Verify(s => s.Advanced);
            advanced.Verify(a => a.GetSnapshot(id, int.MaxValue));
        }

        [Fact]
        public void RollbackTest()
        {
            // arrange 
            var sut = this.GetFakeEventStore();
            sut.BeginTransaction();

            // act 
            sut.Rollback();

            // assert
            Assert.Null(sut.TransactionScope);
        }

        [Fact(Skip = "Integration Test")]
        public void SaveIntegrationDBTest()
        {
            // arrange
            var sut = this.GetEventStore();
            const string Newname = "NewName";
            var domainEntity = new Entity();
            domainEntity.ChangeName(Newname);
            // act
            sut.Save(domainEntity);

            // assert
            Assert.Equal(2, sut.EventStorage.OpenStream(domainEntity.Id, 0, int.MaxValue).CommittedEvents.Count);
        }

        [Fact]
        public void SaveShapShotTest()
        {
            // arrange 
            var entity = new Entity();
            var store = new Mock<IStoreEvents>();
            var advanced = new Mock<IPersistStreams>();

            store.Setup(s => s.Advanced).Returns(advanced.Object);
            var sut = this.GetFakeEventStore(store.Object);

            // act
            sut.SaveShapShot(entity);

            // assert
            store.Verify(s => s.Advanced);
            advanced.Verify(a => a.AddSnapshot(It.IsAny<Snapshot>()));
        }

        [Fact]
        public void SaveTest()
        {
            // arrange
            var stream = new Mock<IEventStream>();
            var store = new Mock<IStoreEvents>();
            store.Setup(s => s.OpenStream(It.IsAny<Guid>(), 0, int.MaxValue)).Returns(stream.Object);
            var sut = this.GetFakeEventStore(store.Object);

            var domainEntity = new Entity();

            // act
            sut.Save(domainEntity);

            // assert
            store.Verify(s => s.OpenStream(domainEntity.Id, 0, int.MaxValue));
            stream.Verify(s => s.Add(It.IsAny<EventMessage>()), Times.Exactly(1));
            stream.Verify(s => s.CommitChanges(It.IsAny<Guid>()));
        }

        [Fact]
        public void SaveWithIncorrectVersionTest()
        {
            // arrange
            var sut = this.GetEventStore();
            var domainEntity = new Entity(2); //Version 2

            // act & assert
            Assert.Throws<ConcurrencyViolationException>(() => sut.Save(domainEntity));
        }

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

        private DomainEventStorage<IDomainEvent> GetEventStore()
        {
            var eventStoreBuilder = new EventStoreBuilder();

            return new DomainEventStorage<IDomainEvent>(eventStoreBuilder);
        }

        private DomainEventStorage<IDomainEvent> GetFakeEventStore()
        {
            var store = new Mock<IStoreEvents>();
            return GetFakeEventStore(store.Object);
        }

        private DomainEventStorage<IDomainEvent> GetFakeEventStore(Mock<IEventStream> streamMock)
        {
            var store = new Mock<IStoreEvents>();
            return this.GetFakeEventStore(store, streamMock);
        }

        private DomainEventStorage<IDomainEvent> GetFakeEventStore(IStoreEvents store)
        {
            var storeBuilder = new Mock<IEventStoreBuilder>();
            storeBuilder.Setup(s => s.GetEventStore()).Returns(store);
            return new DomainEventStorage<IDomainEvent>(storeBuilder.Object);
        }

        private DomainEventStorage<IDomainEvent> GetFakeEventStore(
            Mock<IStoreEvents> store,
            Mock<IEventStream> streamMock)
        {
            var storeBuilder = new Mock<IEventStoreBuilder>();
            storeBuilder.Setup(s => s.GetEventStore()).Returns(store.Object);
            return new DomainEventStorageFake(storeBuilder.Object, streamMock);
        }

        #endregion

        internal class DomainEventStorageFake : DomainEventStorage<IDomainEvent>
        {
            #region Constructors and Destructors

            public DomainEventStorageFake(IEventStoreBuilder builder, Mock<IEventStream> mock)
                : base(builder)
            {
                this.StreamMock = mock;
            }

            #endregion

            #region Public Properties

            public Mock<IEventStream> StreamMock { get; set; }

            #endregion

            #region Methods

            protected override IEventStream GetStream(Guid id)
            {
                return this.StreamMock.Object;
            }

            #endregion
        }
    }
}