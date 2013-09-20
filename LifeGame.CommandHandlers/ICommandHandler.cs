using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.CommandHandlers
{
    interface ICommandHandler<TCommand> where TCommand: class
    {
        bool Execute(TCommand command);
    }
}
