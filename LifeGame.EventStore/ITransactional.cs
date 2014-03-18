// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITransactional.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The Transactional interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore
{
    /// <summary>
    ///     The Transactional interface.
    /// </summary>
    public interface ITransactional
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The begin transaction.
        /// </summary>
        void BeginTransaction();

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