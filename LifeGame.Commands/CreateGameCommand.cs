using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Commands
{
    public class CreateGameCommand : Command
    {
        public CreateGameCommand(Guid id, int numberOfCells)
            : base(id)
        {
            this.NumberOfCells = numberOfCells;
        }

        public int NumberOfCells { get; set; }

    }
}
