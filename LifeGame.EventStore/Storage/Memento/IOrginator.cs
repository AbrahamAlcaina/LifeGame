// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrginator.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The Orginator interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Storage.Memento
{
    /// <summary>
    ///     The Orginator interface.
    /// </summary>
    public interface IOrginator
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The create memento.
        /// </summary>
        /// <returns>
        ///     The <see cref="IMemento" />.
        /// </returns>
        IMemento CreateMemento();

        /// <summary>
        ///     The set memento.
        /// </summary>
        /// <param name="memento">
        ///     The memento.
        /// </param>
        void SetMemento(IMemento memento);

        #endregion
    }
}