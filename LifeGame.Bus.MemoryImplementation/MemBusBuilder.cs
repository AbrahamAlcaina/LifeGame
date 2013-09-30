using LifeGame.CommandHandlers;
using MemBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Bus.MemoryImplementation
{
    public class MemBusBuilder: IBusBuilder
    {

        public MemBus.IBus GetBus()
        {
            return BusSetup.StartWith<MemBus.Configurators.AsyncConfiguration>()
                 .Apply<FlexibleSubscribeAdapter>(a =>
                 {
                     a.ByInterface(typeof(ICommandHandler<>));                     
                 })
                 .Apply<
                 .Construct();
        }

        public MemBusBuilder() { }
    }
}
