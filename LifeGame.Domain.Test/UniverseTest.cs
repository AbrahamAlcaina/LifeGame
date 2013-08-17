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
        public void StartGrid()
        {
            // arrange
            var sut = new Universe();
            const int NumberOfLifeCells = 20;

            // act
            sut.SetUp(NumberOfLifeCells);

            // assert
            Assert.Equal(NumberOfLifeCells, sut.NumberOfLifeCell);
        }
    }
}