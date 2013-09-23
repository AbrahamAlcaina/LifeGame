namespace LifeGame.EventStore.Implementation
{
    using NEventStore;

    public interface IEventStoreBuilder
    {
        IStoreEvents GetEventStore();
    }
}