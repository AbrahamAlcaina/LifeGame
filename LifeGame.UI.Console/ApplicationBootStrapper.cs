using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.UI.Console
{
    using SimpleInjector;

    class ApplicationBootStrapper
    {
        public Container BootStrapTheApplication()
        {
            var container = new Container();
            return container;
        }

        public static Container BootStrap()
        {
            return new ApplicationBootStrapper().BootStrapTheApplication();
        }

    }
}
