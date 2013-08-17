using System;
using LifeGame.EventStore.Storage.Memento;

namespace LifeGame.EventStore.Storage
{
    public interface ISnapShot 
    {
        IMemento Memento { get; }
        Guid EventProviderId { get; }
        int Version { get; }
    }
}