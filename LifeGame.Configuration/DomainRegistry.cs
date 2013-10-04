using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Configuration
{
    using SimpleInjector;

    public class DomainRegistry
    {
        public DomainRegistry Registry(Container container)
        {
            return this;
        }

        public static DomainRegistry DomainBoostrap(Container container)
        {
            return new DomainRegistry().Registry(container);
        }
    }
}
