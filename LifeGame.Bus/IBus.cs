using System.Collections.Generic;

namespace LifeGame.Bus
{
    public interface IBus : IUnitOfWork
    {
        void Publish(object message);
        void Publish(IEnumerable<object> messages);
    }
}