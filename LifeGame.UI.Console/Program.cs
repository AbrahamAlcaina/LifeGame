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
    using LifeGame.Bus;
    using LifeGame.Commands;

    using SimpleInjector;
    using System;

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
            Console.WriteLine("Start");
            container = ApplicationBootStrapper.BootStrap();
            var bus = GetInstance<IBus>();
            var startCommand = new CreateGameCommand(Guid.NewGuid(), 1000);
            bus.Publish(startCommand);

            Console.ReadLine();
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