using System;
using System.Collections.Generic;

namespace LifeGame.Bus.Direct
{
    public interface IQueue
    {
        void Put(object item);
        void Pop(Action<object> popAction);
    }

    public class InMemoryQueue : IQueue
    {
        private readonly Queue<object> itemQueue;
        private readonly Queue<Action<object>> listenerQueue;

        public InMemoryQueue()
        {
            this.itemQueue = new Queue<object>(32);
            this.listenerQueue = new Queue<Action<object>>(32);
        }

        public void Put(object item)
        {
            if (this.listenerQueue.Count == 0)
            {
                this.itemQueue.Enqueue(item);
                return;
            }

            var listener = this.listenerQueue.Dequeue();
            listener(item);
        }

        public void Pop(Action<object> popAction)
        {
            if (this.itemQueue.Count == 0)
            {
                this.listenerQueue.Enqueue(popAction);
                return;
            }

            var item = this.itemQueue.Dequeue();
            popAction(item);
        }
    }
}