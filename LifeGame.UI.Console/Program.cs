// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.UI.Console
{
    using SimpleInjector;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        #region Methods

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            container = ApplicationBootStrapper.BootStrap();
        }

        #endregion

        private static Container container;

        [System.Diagnostics.DebuggerStepThrough]
        public static TService GetInstance<TService>() where TService : class
        {
            return container.GetInstance<TService>();
        }

    }
}