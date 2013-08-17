namespace LifeGame.EventStore
{
    public interface ITransactional
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}