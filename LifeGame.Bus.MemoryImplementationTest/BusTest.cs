// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusTest.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The bus test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Bus.MemoryImplementation.Test
{
    using System;
    using System.Collections.Generic;

    using LifeGame.Commands;

    using MemBus;

    using Moq;

    using SimpleInjector;

    using Xunit;

    /// <summary>
    /// The bus test.
    /// </summary>
    public class BusTest
    {
        #region Public Methods and Operators

        /// <summary>
        /// The bus constructor.
        /// </summary>
        [Fact]
        public void BusConstructor()
        {
            // arrange
            var container = new Container();
            var builder = new MemBusBuilder(container);

            // act
            var sut = new Bus(builder);

            // assert
            Assert.NotNull(sut);
        }

        /// <summary>
        /// The commit test.
        /// </summary>
        [Fact]
        public void CommitTest()
        {
            // arrange 
            var builder = new Mock<IBusBuilder>();
            var sut = new Bus(builder.Object);

            // act & assert
            Assert.Throws<NotSupportedException>(() => sut.Commit());
        }

        /// <summary>
        /// The multi publish test.
        /// </summary>
        [Fact]
        public void MultiPublishTest()
        {
            // arrange
            var builder = new Mock<IBusBuilder>();
            var memBus = new Mock<IBus>();
            Bus sut = GetBus(builder, memBus);
            var messages = new List<Command> { new Command(Guid.NewGuid()), new Command(Guid.NewGuid()) };

            // act
            sut.Publish(messages);

            // assert
            memBus.Verify(bus => bus.Publish(It.IsAny<object>()), Times.Exactly(2));
        }

        /// <summary>
        /// The publish test.
        /// </summary>
        [Fact]
        public void PublishTest()
        {
            // arrange
            var builder = new Mock<IBusBuilder>();
            var memBus = new Mock<IBus>();
            Bus sut = GetBus(builder, memBus);
            var command = new Command(Guid.NewGuid());

            // act
            sut.Publish(command);

            // assert
            memBus.Verify(bus => bus.Publish(command));
        }

        /// <summary>
        /// The rollback test.
        /// </summary>
        [Fact]
        public void RollbackTest()
        {
            // arrange 
            var builder = new Mock<IBusBuilder>();
            var sut = new Bus(builder.Object);

            // act & assert
            Assert.Throws<NotSupportedException>(() => sut.Rollback());
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get bus.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <param name="memBus">
        /// The mem bus.
        /// </param>
        /// <returns>
        /// The <see cref="Bus"/>.
        /// </returns>
        private static Bus GetBus(Mock<IBusBuilder> builder, Mock<IBus> memBus)
        {
            builder.Setup(b => b.GetBus()).Returns(memBus.Object);

            var sut = new Bus(builder.Object);
            return sut;
        }

        #endregion
    }
}