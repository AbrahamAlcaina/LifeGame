using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeGame.Bus.Implementation;
using Xunit;
namespace LifeGame.Bus.Implementation.Test
{
    public class BusTest
    {
        [Fact]
        public void IntegratedTest()
        {
            // arrange 
            var integratedBuilder = new BusBuilder();
            
            // act 
            var sut = new Bus(integratedBuilder);

            // assert
            Assert.NotNull(sut);
        }
    }
}
