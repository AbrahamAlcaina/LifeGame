using System;

namespace LifeGame.EventStore.Storage
{
    public interface ISnapShotStorage<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        ISnapShot GetSnapShot(Guid entityId);
        void SaveShapShot(IEventProvider<TDomainEvent> entity);
    }
}