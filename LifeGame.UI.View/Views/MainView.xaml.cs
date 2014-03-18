// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainView.xaml.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.UI.View.Views
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Threading;

    using MahApps.Metro.Controls;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : MetroWindow
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainView" /> class.
        /// </summary>
        public MainView()
        {
            this.InitializeComponent();
            var t = new DispatcherTimer(TimeSpan.FromSeconds(5), DispatcherPriority.Normal, this.Tick, this.Dispatcher);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The tick.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.transitioning.Content = new TextBlock { Text = "Last News " + dateTime, SnapsToDevicePixels = true };
            this.customTransitioning.Content = new TextBlock
                                               {
                                                   Text = "Last game " + dateTime,
                                                   SnapsToDevicePixels = true
                                               };
        }

        #endregion
    }
}