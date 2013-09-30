using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Bus.MemoryImplementation
{
    public IEnumerable<CommandHandler<T>> GetHandlers<T>()
        where T : class
    {
        return SimpleInjectorContainer.GetAllInstances<CommandHandler<T>>();
    }
}
