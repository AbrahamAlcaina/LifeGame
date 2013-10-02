using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeGame.Events;
using Xunit;
namespace LifeGame.Events.Test
{
    public class DomainEventTest
    {
        [Fact]
        public void DomainEventConstructorTest()
        {
            // arrange & act
            var sut = new DomainEvent();

            // assert
            Assert.NotNull(sut);
            Assert.NotEqual(default(Guid), sut.Id);
        }
    }
}
