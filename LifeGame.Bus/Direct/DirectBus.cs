namespace LifeGame.Bus.Direct
{
    using System.Collections.Generic;

    public class DirectBus : IBus
    {
        #region Fields

        private readonly object lockObject = new object();

        private readonly InMemoryQueue postCommitQueue;

        private readonly Queue<object> preCommitQueue;

        private readonly IRouteMessages routeMessages;

        #endregion

        #region Constructors and Destructors

        public DirectBus(IRouteMessages routeMessages)
        {
            this.routeMessages = routeMessages;
            this.preCommitQueue = new Queue<object>(32);
            this.postCommitQueue = new InMemoryQueue();
            this.postCommitQueue.Pop(this.DoPublish);
        }

        #endregion

        #region Public Methods and Operators

        public void Commit()
        {
            lock (this.lockObject)
            {
                while (this.preCommitQueue.Count > 0)
                {
                    this.postCommitQueue.Put(this.preCommitQueue.Dequeue());
                }
            }
        }

        public void Publish(object message)
        {
            lock (this.lockObject)
            {
                this.preCommitQueue.Enqueue(message);
            }
        }

        public void Publish(IEnumerable<object> messages)
        {
            lock (this.lockObject)
            {
                foreach (object message in messages)
                {
                    this.preCommitQueue.Enqueue(message);
                }
            }
        }

        public void Rollback()
        {
            lock (this.lockObject)
            {
                this.preCommitQueue.Clear();
            }
        }

        #endregion

        #region Methods

        private void DoPublish(object message)
        {
            try
            {
                this.routeMessages.Route(message);
            }
            finally
            {
                this.postCommitQueue.Pop(this.DoPublish);
            }
        }

        #endregion
    }
}