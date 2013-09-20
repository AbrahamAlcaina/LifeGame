﻿
namespace LifeGame.EventStore
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}