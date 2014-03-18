// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellAlreadyLiveException.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The cell already live exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain
{
    using System;

    /// <summary>
    ///     The cell already live exception.
    /// </summary>
    [Serializable]
    public class CellAlreadyLiveException : Exception
    {
    }
}