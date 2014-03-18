// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventStorageNotInitializedException.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The event storage not initialized exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Implementation
{
    using System;

    /// <summary>
    ///     The event storage not initialized exception.
    /// </summary>
    [Serializable]
    public class EventStorageNotInitializedException : Exception
    {
    }
}