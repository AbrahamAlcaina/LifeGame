namespace LifeGame.Domain
{
    using System.Collections.Generic;

    public interface IGameboardConstructorStrategy
    {
        IList<Cell> CreateGameboard(int numberOfCells);
    }
}