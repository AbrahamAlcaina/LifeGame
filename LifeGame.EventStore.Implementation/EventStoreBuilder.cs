// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventStoreBuilder.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The event store builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Implementation
{
    using NEventStore;
    using NEventStore.Persistence.SqlPersistence.SqlDialects;

    /// <summary>
    /// The event store builder.
    /// </summary>
    public class EventStoreBuilder : IEventStoreBuilder
    {
        #region Static Fields

        /// <summary>
        /// The encryption key.
        /// </summary>
        private static readonly byte[] EncryptionKey =
        {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xb, 0xc, 
            0xd, 0xe, 0xf
        };

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get event store.
        /// </summary>
        /// <returns>
        /// The <see cref="IStoreEvents"/>.
        /// </returns>
        public IStoreEvents GetEventStore()
        {
            return
                Wireup.Init()
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

        #endregion
    }
}