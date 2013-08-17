using System;

namespace LifeGame.EventStore.Aggregate
{
    public class UnregisteredDomainEventException : Exception
    {
        public UnregisteredDomainEventException(string message) : base(message) {}
    }
}