// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The UnitOfWork interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Bus
{
    /// <summary>
    ///     The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The commit.
        /// </summary>
        void Commit();

        /// <summary>
        ///     The rollback.
        /// </summary>
        void Rollback();

        #endregion
    }
}