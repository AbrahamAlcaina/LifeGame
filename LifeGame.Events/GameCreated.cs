using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Events
{
    class GameCreated : DomainEvent
    {
        public Guid IdGame { get; set; }
    }
}
