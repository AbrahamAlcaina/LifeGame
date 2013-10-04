using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.UI.Console
{
    using LifeGame.Bus.MemoryImplementation;
    using LifeGame.Configuration;

    using SimpleInjector;

    class ApplicationBootStrapper
    {
        public Container BootStrapTheApplication()
        {
            var container = new Container();
            ApplicationRegistry.ApplicationBoostrap(container);
            DomainRegistry.DomainBoostrap(container);
            BusRegistry.BusBoostrap(container);
            EventStoreRegistry.EventStoreBoostrap(container);
            return container;
        }

        public static Container BootStrap()
        {
            return new ApplicationBootStrapper().BootStrapTheApplication();
        }

    }
}
