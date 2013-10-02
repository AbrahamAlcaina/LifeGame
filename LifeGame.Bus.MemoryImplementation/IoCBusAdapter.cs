using LifeGame.CommandHandlers;
using MemBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator.SimpleInjectorAdapter;
using SimpleInjector;

namespace LifeGame.Bus.MemoryImplementation
{
    class BusIoCAdapter : IocAdapter
    {
        public BusIoCAdapter(Container container)
        {
            this.Container = container;
        }
        public IEnumerable<object> GetAllInstances(Type desiredType)
        {
            return this.Container.GetAllInstances(desiredType);
        }

        internal Container Container { get; set; }
    }
}
