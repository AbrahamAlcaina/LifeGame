// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridTest.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The grid test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain.Test
{
    using Xunit;

    /// <summary>
    /// The grid test.
    /// </summary>
    public class UniverseTest
    {
        [Fact]
        public void DefaultConstructorCreatesOneMillionOfCells()
        {
            // arrange 
            const int NumberOfCells = 1000 * 1000;
            var strategy = new SquareGameboard();
            // act
            var sut = new Universe(strategy, NumberOfCells);

            // assert
            Assert.Equal(NumberOfCells, sut.NumberOfLifeCell);
        }
    }
}