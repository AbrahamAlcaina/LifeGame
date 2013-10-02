using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Bus.MemoryImplementation
{
    using LifeGame.CommandHandlers;
    using SimpleInjector.Extensions;
    public class BusRegistry
    {
        public void Registry(Container container)
        {
            container.RegisterManyForOpenGeneric(typeof(ICommandHandler<>),
                AccessibilityOption.PublicTypesOnly,
                (serviceType, implTypes) => container.RegisterAll(serviceType, implTypes),
                typeof(ICommandHandler<>).Assembly
                );
        }
    }
}
