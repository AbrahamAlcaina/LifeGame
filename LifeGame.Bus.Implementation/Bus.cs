using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Bus.Implementation
{
    public class Bus: IBus
    {
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Publish(object message)
        {
            throw new NotImplementedException();
        }

        public void Publish(IEnumerable<object> messages)
        {
            throw new NotImplementedException();
        }

        public Bus(IBusBuilder builder)
        {
            NBus = builder.GetBus();
        }

        internal NServiceBus.IBus NBus { get; set; }
    }
}
