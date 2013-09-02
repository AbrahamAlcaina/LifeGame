using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.CommandHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand : class
    {
        void Execute(TCommand command);
    }
}
