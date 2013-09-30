using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Bus.MemoryImplementation
{
    public class Bus : IBus
    {
        public void Publish(object message)
        {
            this.MemoryBus.Publish(message);
        }

        public void Publish(IEnumerable<object> messages)
        {
            foreach (var message in messages)
            {
                Publish(message);
            }
        }

        public void Commit()
        {
            throw new NotSupportedException();
        }
        
        public void Rollback()
        {
            throw new NotSupportedException();
        }

        public Bus(IBusBuilder busBuilder)
        {
            this.MemoryBus = busBuilder.GetBus();
        }

        internal MemBus.IBus MemoryBus { get; set; }
    }
}
