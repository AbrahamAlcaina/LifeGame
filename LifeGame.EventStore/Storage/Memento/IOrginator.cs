namespace LifeGame.EventStore.Storage.Memento
{
    public interface IOrginator
    {
        IMemento CreateMemento();
        void SetMemento(IMemento memento);
    }
}