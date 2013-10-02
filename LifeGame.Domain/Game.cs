namespace LifeGame.Domain
{
    using System;

    using LifeGame.Domain.Mementos;
    using LifeGame.EventStore;
    using LifeGame.EventStore.Aggregate;
    using LifeGame.EventStore.Storage.Memento;

    public class Game : BaseAggregateRoot<IDomainEvent>, IOrginator
    {
        public static Game CreateGame(int numberOfCells)
        {
            return new Game(numberOfCells);
        }

        public Game(int numberOfCells)
        {
            this.NumberOfCells = numberOfCells;
        }

        public Game()
        {
        }

        public IMemento CreateMemento()
        {
            return new GameMemento(this.Id, this.Version, this.NumberOfCells);
        }

        public void SetMemento(IMemento memento)
        {
            var gameMemento = memento as GameMemento;
            this.Id = gameMemento.Id;
            this.Version = gameMemento.Version;
            this.NumberOfCells = gameMemento.NumberOfCells;            
        }

        internal int NumberOfCells { get; set; }
    }
}