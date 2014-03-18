// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AllReadyInTransactionException.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The all ready in transaction exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Implementation
{
    using System;

    /// <summary>
    ///     The all ready in transaction exception.
    /// </summary>
    [Serializable]
    public class AllReadyInTransactionException : Exception
    {
    }
}