using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeGame.Bus.MemoryImplementation;
using Xunit;
using Moq;
using LifeGame.Commands;
namespace LifeGame.Bus.MemoryImplementation.Test
{
    public class BusTest
    {
        [Fact]
        public void BusConstructor()
        {
            // arrange
            var builder = new MemBusBuilder();

            // act
            var sut = new Bus(builder);

            // assert
            Assert.NotNull(sut);
        }

        [Fact]
        public void PublishTest()
        {
            // arrange
            var builder = new Mock<IBusBuilder>();
            var memBus = new Mock<MemBus.IBus>();
            var sut = GetBus(builder, memBus);
            var command = new Command(Guid.NewGuid());

            // act
            sut.Publish(command);

            // assert
            memBus.Verify(bus => bus.Publish(command));
        }

        [Fact]
        public void MultiPublishTest()
        {
            // arrange
            var builder = new Mock<IBusBuilder>();
            var memBus = new Mock<MemBus.IBus>();
            var sut = GetBus(builder, memBus);
            var messages = new List<Command> { new Command(Guid.NewGuid()), new Command(Guid.NewGuid()) };

            // act
            sut.Publish(messages);

            // assert
            memBus.Verify( bus => bus.Publish(It.IsAny<object>()), Times.Exactly(2));
        }


        private static Bus GetBus(Mock<IBusBuilder> builder, Mock<MemBus.IBus> memBus)
        {
            builder.Setup(b => b.GetBus()).Returns(memBus.Object);

            var sut = new Bus(builder.Object);
            return sut;
        }

        [Fact()]
        public void CommitTest()
        {
            // arrange 
            var builder = new Mock<IBusBuilder>();
            var sut = new Bus(builder.Object);

            // act & assert
            Assert.Throws<NotSupportedException>(() => sut.Commit());
        }


        [Fact()]
        public void RollbackTest()
        {
            // arrange 
            var builder = new Mock<IBusBuilder>();
            var sut = new Bus(builder.Object);

            // act & assert
            Assert.Throws<NotSupportedException>(() => sut.Rollback());
        }
    }
}
