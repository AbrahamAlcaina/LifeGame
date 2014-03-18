// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameTest.cs" company="Abraham Alcaina">
//   AAA Code
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
    ///     The game test.
    /// </summary>
    public class GameTest
    {
        #region Fields

        /// <summary>
        ///     The board constructor.
        /// </summary>
        private readonly Guid boardConstructor;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameTest" /> class.
        /// </summary>
        /// <param name="boardConstructor1">
        ///     The board constructor 1.
        /// </param>
        public GameTest(Guid boardConstructor1)
        {
            this.boardConstructor = this.boardConstructor;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The create memento test.
        /// </summary>
        [Fact]
        public void CreateMementoTest()
        {
            // arrange
            const int NumberOfCells = 100;
            var sut = new Game(this.boardConstructor, new SquareGameboard(), NumberOfCells);

            // act
            IMemento memento = sut.CreateMemento();
            var gameMemento = memento as GameMemento;

            // assert
            Assert.NotNull(gameMemento);
        }

        /// <summary>
        ///     The set memento test.
        /// </summary>
        [Fact]
        public void SetMementoTest()
        {
            // arrange
            const int NumberOfCells = 100;
            const int Version = 10;
            const int NumberOfCellsMememto = 100000;
            Guid id = Guid.NewGuid();

            var sut = new Game(id, new SquareGameboard(), NumberOfCells);
            var memento = new GameMemento(sut);

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