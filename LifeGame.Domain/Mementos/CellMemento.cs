using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LifeGame.EventStore.Storage.Memento;

namespace LifeGame.Domain.Mementos
{
    class CellMemento : IMemento
    {
        internal CellStatus Status { get; set; }

        public CellMemento(CellStatus status)
        {
            this.Status = status;
        }
    }
}
