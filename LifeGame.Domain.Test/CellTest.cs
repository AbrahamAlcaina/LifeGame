// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellTest.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The cell test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain.Test
{
    using Xunit;
    using Xunit.Extensions;

    /// <summary>
    ///     The cell test.
    /// </summary>
    public class CellTest
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The deade cell next step reprodution.
        /// </summary>
        [Theory]
        [InlineData(3, 5)]
        public void DeadeCellNextStepReprodution(int liveCells, int deadCells)
        {
            // arrange
            var sut = new Cell { Status = CellStatus.Dead };
            ArrangeDeadNeighbours(deadCells, sut);
            ArrangeLiveNeighbours(liveCells, sut);

            // act
            sut.PrepairNextStatus();

            // assert
            Assert.NotNull(sut.FutureStatus);
            Assert.True(sut.FutureStatus.Value == CellStatus.Alive);
            Assert.NotNull(sut.ChangedReason);
            Assert.True(sut.ChangedReason == CellChangeReason.Reprodution);
        }

        /// <summary>
        ///     The deade cell next step still dead.
        /// </summary>
        [Theory]
        [InlineData(0, 8)]
        [InlineData(1, 7)]
        [InlineData(4, 4)]
        [InlineData(7, 1)]
        public void DeadeCellNextStepStillDead(int liveCells, int deadCells)
        {
            // arrange
            var sut = new Cell { Status = CellStatus.Dead };
            ArrangeDeadNeighbours(deadCells, sut);
            ArrangeLiveNeighbours(liveCells, sut);

            // act
            sut.PrepairNextStatus();

            // assert
            Assert.NotNull(sut.FutureStatus);
            Assert.True(sut.FutureStatus.Value == CellStatus.Dead);
            Assert.NotNull(sut.ChangedReason);
            Assert.True(sut.ChangedReason == CellChangeReason.StillEqual);
        }

        /// <summary>
        ///     The live cell next step overcrowding.
        /// </summary>
        [Theory]
        [InlineData(4, 4)]
        [InlineData(8, 0)]
        public void LiveCellNextStepOvercrowding(int liveCells, int deadCells)
        {
            // arrange
            var sut = new Cell { Status = CellStatus.Alive };
            ArrangeDeadNeighbours(deadCells, sut);
            ArrangeLiveNeighbours(liveCells, sut);

            // act
            sut.PrepairNextStatus();

            // assert
            Assert.NotNull(sut.FutureStatus);
            Assert.True(sut.FutureStatus.Value == CellStatus.Dead);
            Assert.NotNull(sut.ChangedReason);
            Assert.True(sut.ChangedReason == CellChangeReason.Overcrowding);
        }

        /// <summary>
        ///     The live cell next step still live.
        /// </summary>
        [Theory]
        [InlineData(2, 6)]
        [InlineData(3, 5)]
        public void LiveCellNextStepStillLive(int liveCells, int deadCells)
        {
            // arrange
            var sut = new Cell { Status = CellStatus.Alive };
            ArrangeDeadNeighbours(deadCells, sut);
            ArrangeLiveNeighbours(liveCells, sut);

            // act
            sut.PrepairNextStatus();

            // assert
            Assert.NotNull(sut.FutureStatus);
            Assert.True(sut.FutureStatus.Value == CellStatus.Alive);
            Assert.NotNull(sut.ChangedReason);
            Assert.True(sut.ChangedReason == CellChangeReason.StillEqual);
        }

        /// <summary>
        ///     The live cell next step unde population.
        /// </summary>
        [Theory]
        [InlineData(1, 7)]
        [InlineData(0, 8)]
        public void LiveCellNextStepUndePopulation(int liveCells, int deadCells)
        {
            // arrange
            var sut = new Cell { Status = CellStatus.Alive };
            ArrangeDeadNeighbours(deadCells, sut);
            ArrangeLiveNeighbours(liveCells, sut);

            // act
            sut.PrepairNextStatus();

            // assert
            Assert.NotNull(sut.FutureStatus);
            Assert.True(sut.FutureStatus.Value == CellStatus.Dead);
            Assert.NotNull(sut.ChangedReason);
            Assert.True(sut.ChangedReason == CellChangeReason.UnderPopulation);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The arrange dead neighbours.
        /// </summary>
        /// <param name="deadNeighbours">
        ///     The dead neighbours.
        /// </param>
        /// <param name="sut">
        ///     The sut.
        /// </param>
        private static void ArrangeDeadNeighbours(int deadNeighbours, Cell sut)
        {
            for (int i = 0; i < deadNeighbours; i++)
            {
                sut.AddNeighbor(new Cell { Status = CellStatus.Dead });
            }
        }

        /// <summary>
        ///     The arrange live neighbours.
        /// </summary>
        /// <param name="aliveNeighbours">
        ///     The alive neighbours.
        /// </param>
        /// <param name="sut">
        ///     The sut.
        /// </param>
        private static void ArrangeLiveNeighbours(int aliveNeighbours, Cell sut)
        {
            for (int i = 0; i < aliveNeighbours; i++)
            {
                sut.AddNeighbor(new Cell { Status = CellStatus.Alive });
            }
        }

        #endregion
    }
}