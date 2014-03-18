// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellAlreadyDeadException.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The cell already dead exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain
{
    using System;

    /// <summary>
    ///     The cell already dead exception.
    /// </summary>
    [Serializable]
    internal class CellAlreadyDeadException : Exception
    {
    }
}