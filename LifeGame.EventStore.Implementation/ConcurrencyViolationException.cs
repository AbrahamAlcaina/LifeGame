// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConcurrencyViolationException.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The concurrency violation exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Implementation
{
    using System;

    /// <summary>
    ///     The concurrency violation exception.
    /// </summary>
    [Serializable]
    public class ConcurrencyViolationException : Exception
    {
    }
}