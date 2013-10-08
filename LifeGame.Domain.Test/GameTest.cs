// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameTest.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The game test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain.Test
{
    using System;

    using LifeGame.Domain.Mementos;
    using LifeGame.EventStore.Storage.Memento;

    using Xunit;

    /// <summary>
    /// The game test.
    /// </summary>
    public class GameTest
    {
        #region Public Methods and Operators

        /// <summary>
        /// The create memento test.
        /// </summary>
        [Fact]
        public void CreateMementoTest()
        {
            // arrange
            const int NumberOfCells = 100;
            var sut = new Game(NumberOfCells, new SquareGameboard());

            // act
            IMemento memento = sut.CreateMemento();
            var gameMemento = memento as GameMemento;

            // assert
            Assert.NotNull(gameMemento);
        }

        /// <summary>
        /// The set memento test.
        /// </summary>
        [Fact]
        public void SetMementoTest()
        {
            // arrange
            const int NumberOfCells = 100;
            const int Version = 10;
            const int NumberOfCellsMememto = 100000;
            Guid id = Guid.NewGuid();

            var sut = new Game(NumberOfCells, new SquareGameboard());
            var memento = new GameMemento(id, Version, NumberOfCellsMememto);

            // act
            sut.SetMemento(memento);

            // assert
            Assert.Equal(id, sut.Id);
            Assert.Equal(Version, sut.Version);
            Assert.Equal(NumberOfCellsMememto, sut.NumberOfCells);
        }

        #endregion
    }
}