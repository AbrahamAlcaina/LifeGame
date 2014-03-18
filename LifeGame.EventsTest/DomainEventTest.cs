// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainEventTest.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The domain event test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Events.Test
{
    using System;

    using Xunit;

    /// <summary>
    ///     The domain event test.
    /// </summary>
    public class DomainEventTest
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The domain event constructor test.
        /// </summary>
        [Fact]
        public void DomainEventConstructorTest()
        {
            // arrange & act
            var sut = new DomainEvent();

            // assert
            Assert.NotNull(sut);
            Assert.NotEqual(default(Guid), sut.Id);
        }

        #endregion
    }
}