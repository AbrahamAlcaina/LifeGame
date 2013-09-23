namespace LifeGame.Domain
{
    using System;

    using LifeGame.Domain.Mementos;
    using LifeGame.EventStore;
    using LifeGame.EventStore.Aggregate;
    using LifeGame.EventStore.Storage.Memento;

    public class Game : BaseAggregateRoot<IDomainEvent>, IOrginator
    {
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
            this.NumberOfCells = gameMemento.NumberOfCells;
        }

        private int NumberOfCells { get; set; }
    }
}