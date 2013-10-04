using LifeGame.CommandHandlers;
using MemBus;
using MemBus.Configurators;
using MemBus.Subscribing;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Bus.MemoryImplementation
{
    public class MemBusBuilder: IBusBuilder
    {
        public MemBusBuilder(Container container)
        {
            this.Container = container;
        }
        public MemBus.IBus GetBus()
        {
            return BusSetup.StartWith<AsyncConfiguration>()
                 .Apply<IoCSupport>(s => s.SetAdapter(new BusIoCAdapter(this.Container)).SetHandlerInterface(typeof(ICommandHandler<>)))
                 .Construct();
        }

        internal Container Container { get; set; }
    }
}
