namespace LifeGame.Domain
{
    using System;

    using AutoMapper;

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

        public IMemento CreateMemento()
        {
            return new GameMemento(this.Id, this.Version, this.NumberOfCells);
        }

        public void SetMemento(IMemento memento)
        {
            var gameMemento = memento as GameMemento;
            gameMemento = Mapper.Map<GameMemento>(this);
        }

        private int NumberOfCells { get; set; }
    }
}