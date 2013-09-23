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
            const int Version = 0;
            const int NumberOfCellsMememto = 100000;

            var game = new Game(NumberOfCells);
            var memento = new GameMemento(Guid.NewGuid(), Version, NumberOfCellsMememto);

            game.SetMemento(memento);

            Assert.Equal(NumberOfCells, memento.NumberOfCells);
        }

        #endregion

    }
}