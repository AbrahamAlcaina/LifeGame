// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CellChangeReason.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The cell change reason.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.Domain
{
    /// <summary>
    ///     The cell change reason.
    /// </summary>
    public enum CellChangeReason
    {
        /// <summary>
        ///     The under population.
        /// </summary>
        UnderPopulation,

        /// <summary>
        ///     The overcrowding.
        /// </summary>
        Overcrowding,

        /// <summary>
        ///     The reprodution.
        /// </summary>
        Reprodution,

        /// <summary>
        ///     The still equal.
        /// </summary>
        StillEqual,

        /// <summary>
        ///     The creation.
        /// </summary>
        Creation
    }
}