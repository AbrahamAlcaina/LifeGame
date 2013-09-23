namespace LifeGame.EventStore.Implementation
{
    using NEventStore;
    using NEventStore.Persistence.SqlPersistence.SqlDialects;

    public class EventStoreBuilder : IEventStoreBuilder
    {
        public IStoreEvents GetEventStore()
        {
            return Wireup.Init()
                .LogToOutputWindow()
                .UsingInMemoryPersistence()
                .UsingSqlPersistence("NEventStore") // Connection string is in app.config
                    .WithDialect(new SqliteDialect())
                    .InitializeStorageEngine()
                    .UsingJsonSerialization()
                        .Compress()
                        .EncryptWith(EncryptionKey)
                .UsingSynchronousDispatchScheduler()
                .Build(); 
        }

        private static readonly byte[] EncryptionKey = new byte[]
		{
			0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf
		};
    }
}
