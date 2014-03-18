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
    using System;
    using System.Diagnostics;

    using LifeGame.Bus;
    using LifeGame.Commands;
    using LifeGame.Configuration;

    using SimpleInjector;

    /// <summary>
    ///     The program.
    /// </summary>
    internal class Program
    {
        #region Static Fields

        /// <summary>
        ///     The container.
        /// </summary>
        private static Container container;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get instance.
        /// </summary>
        /// <typeparam name="TService">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="TService" />.
        /// </returns>
        [DebuggerStepThrough]
        public static TService GetInstance<TService>() where TService : class
        {
            return container.GetInstance<TService>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The main.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void Main(string[] args)
        {
            Console.WriteLine("Start");
            container = ApplicationBootStrapper.BootStrap();
            var bus = GetInstance<IBus>();
            Guid gameId = Guid.NewGuid();

            bus.Publish(new CreateGameCommand(Guid.NewGuid(), gameId, 4));
            bus.Publish(new InitializeGameCommand(Guid.NewGuid(), gameId, 1));

            while (string.IsNullOrEmpty(Console.ReadLine()))
            {
                bus.Publish(new EvolveGameCommand(Guid.NewGuid(), gameId));
            }
        }

        #endregion
    }
}