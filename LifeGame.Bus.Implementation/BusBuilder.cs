using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Bus.Implementation
{
    using NServiceBus;

    public class BusBuilder: IBusBuilder
    {
        public IBus GetBus()
        {
            return
                Configure.With()
                .DefaultBuilder()
                .DoNotCreateQueues()
                .UseNHibernateSubscriptionPersister()
                .UseNHibernateSagaPersister()
                .UseNHibernateTimeoutPersister()
                .InMemoryFaultManagement()
                .CreateBus()
                .Start();
        }
    }
}
