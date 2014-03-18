namespace LifeGame.Domain.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Xunit;
    using Xunit.Extensions;

    public class SquareGameboardTests
    {
        #region Public Methods and Operators

        /* Cells ids
         * 03 07 11 15
         * 02 06 10 14
         * 01 05 09 13
         * 00 04 08 12
         */

        [Theory]
        [InlineData(0, 0, 1, 5, 4, 7, 3, 15, 12, 13)]
        [InlineData(3, 0, 13, 01, 00, 03, 15, 11, 08, 09)]
        [InlineData(0, 3, 00, 04, 07, 06, 02, 14, 15, 12)]
        [InlineData(3, 3, 12, 00, 03, 02, 14, 10, 11, 08)]
        [InlineData(1, 1, 06, 10, 09, 08, 04, 00, 01, 02)]
        public void AssociateCells(int x, int y, int n, int ne, int e, int se, int s, int sw, int w, int nw)
        {
            // arrange
            const int Width = 4;
            const int Height = 4;
            var cells = new Cell[Width, Height];
            int ids = 0;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    cells[i, j] = new Cell(new Guid(string.Format("{0:00000000000000000000000000000000}", ids)));
                    ids++;
                }
            }
            var sut = new SquareGameboard();

            // act
            sut.AssociateCellsWithHisNeighbors(cells, Width, Height);


            // assert
            var cell = cells[x, y];
            Assert.True(
                cell.Neighbors.Any(
                    neighbor => neighbor.Id == new Guid(string.Format("{0:00000000000000000000000000000000}", n))));
            Assert.True(
                cell.Neighbors.Any(
                    neighbor => neighbor.Id == new Guid(string.Format("{0:00000000000000000000000000000000}", ne))));
            Assert.True(
                cell.Neighbors.Any(
                    neighbor => neighbor.Id == new Guid(string.Format("{0:00000000000000000000000000000000}", e))));
            Assert.True(
                cell.Neighbors.Any(
                    neighbor => neighbor.Id == new Guid(string.Format("{0:00000000000000000000000000000000}", se))));
            Assert.True(
                cell.Neighbors.Any(
                    neighbor => neighbor.Id == new Guid(string.Format("{0:00000000000000000000000000000000}", s))));
            Assert.True(
                cell.Neighbors.Any(
                    neighbor => neighbor.Id == new Guid(string.Format("{0:00000000000000000000000000000000}", sw))));
            Assert.True(
                cell.Neighbors.Any(
                    neighbor => neighbor.Id == new Guid(string.Format("{0:00000000000000000000000000000000}", w))));
            Assert.True(
                cell.Neighbors.Any(
                    neighbor => neighbor.Id == new Guid(string.Format("{0:00000000000000000000000000000000}", nw))));
        }

        [Fact]
        public void CreateGameboardTest()
        {
            // arrange 
            const int NumberOfCells = 9;
            var sut = new SquareGameboard();

            // act
            IEnumerable<Cell> cells = sut.CreateGameboard(NumberOfCells);

            // assert
            Assert.Equal(cells.Count(), NumberOfCells);
        }

        #endregion
    }
}