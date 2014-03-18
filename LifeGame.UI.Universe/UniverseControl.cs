// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UniverseControl.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The universe control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.UI.Universe
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    ///     The universe control.
    /// </summary>
    public class UniverseControl : Control
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="UniverseControl" /> class.
        /// </summary>
        static UniverseControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(UniverseControl),
                new FrameworkPropertyMetadata(typeof(UniverseControl)));
        }

        #endregion
    }
}