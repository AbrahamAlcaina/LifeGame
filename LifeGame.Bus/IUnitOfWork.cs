namespace LifeGame.Bus
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}