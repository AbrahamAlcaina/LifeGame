namespace LifeGame.Domain.Mementos
{
    using System;

    using LifeGame.EventStore.Storage.Memento;

    [Serializable]
    public class GameMemento : IMemento
    {
        #region Constructors and Destructors

        public GameMemento(Guid id, int version, int numberOfCells)
        {
            this.Id = id;
            this.Version = version;
            this.NumberOfCells = numberOfCells;
        }

        #endregion

        #region Properties

        internal Guid Id { get; private set; }
        internal int NumberOfCells { get; private set; }
        internal int Version { get; private set; }

        #endregion
    }
}