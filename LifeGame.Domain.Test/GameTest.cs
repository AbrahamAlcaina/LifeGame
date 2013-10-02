namespace LifeGame.Domain.Test
{
    using System;

    using LifeGame.Domain.Mementos;

    using Xunit;

    public class GameTest
    {
        #region Public Methods and Operators

        [Fact]
        public void SetMementoTest()
        {
            // arrange
            const int NumberOfCells = 100;
            const int Version = 10;
            const int NumberOfCellsMememto = 100000;
            var id = Guid.NewGuid();

            var sut = new Game(NumberOfCells);
            var memento = new GameMemento(id, Version, NumberOfCellsMememto);

            // act
            sut.SetMemento(memento);

            // assert
            Assert.Equal(id, sut.Id);
            Assert.Equal(Version, sut.Version);
            Assert.Equal(NumberOfCellsMememto, sut.NumberOfCells);
        }

        [Fact]
        public void CreateMementoTest()
        {
            // arrange
            const int NumberOfCells = 100;
            var sut = new Game(NumberOfCells);

            // act
            var memento = sut.CreateMemento();
            var gameMemento = memento as GameMemento;

            // assert
            Assert.NotNull(gameMemento);
        }

        #endregion

    }
}