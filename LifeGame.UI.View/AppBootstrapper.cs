// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppBootstrapper.cs" company="Abraham Alcaina">
//   AAA Code
// </copyright>
// <summary>
//   The app bootstrapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.UI.View
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Threading;

    using Caliburn.Micro;

    using LifeGame.Configuration;
    using LifeGame.EventHandlers;
    using LifeGame.Events;
    using LifeGame.EventStorage.Optimization;
    using LifeGame.EventStore;
    using LifeGame.UI.View.Handlers;
    using LifeGame.UI.View.ViewModels;

    using SimpleInjector;
    using SimpleInjector.Extensions;

    /// <summary>
    ///     The app bootstrapper.
    /// </summary>
    public class AppBootstrapper : Bootstrapper<MainViewModel>
    {
        #region Static Fields

        /// <summary>
        ///     The global container.
        /// </summary>
        public static Container ContainerInstance = new Container();

        #endregion

        #region Methods

        /// <summary>
        ///     The build up.
        /// </summary>
        /// <param name="instance">
        ///     The instance.
        /// </param>
        protected override void BuildUp(object instance)
        {
            ContainerInstance.InjectProperties(instance);
        }

        /// <summary>
        ///     The get all instances.
        /// </summary>
        /// <param name="service">
        ///     The service.
        /// </param>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return ContainerInstance.GetAllInstances(service);
        }

        /// <summary>
        ///     The get instance.
        /// </summary>
        /// <param name="service">
        ///     The service.
        /// </param>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        protected override object GetInstance(Type service, string key)
        {
            return ContainerInstance.GetInstance(service);
        }

        /// <summary>
        ///     The on startup.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            ContainerInstance = ApplicationBootStrapper.BootStrap();
            ContainerBoostrapForUI();
            base.OnStartup(sender, e);
        }

        /// <summary>
        ///     The on unhandled exception.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Write you custom code for handling Global unhandled excpetion of Dispatcher or UI thread.
            base.OnUnhandledException(sender, e);
        }

        /// <summary>
        ///     The container boostrap for ui.
        /// </summary>
        private static void ContainerBoostrapForUI()
        {
            ContainerInstance.Register<IWindowManager, WindowManager>();
            ContainerInstance.RegisterSingle<IEventAggregator, EventAggregator>();
            ContainerInstance.RegisterSingle<MainViewModel, MainViewModel>();

            ContainerInstance.Register<IHandler<GameCreated>, UIGameCreatedEventHandler>();

            ContainerInstance.RegisterManyForOpenGeneric(
                typeof(IHandler<>),
                AccessibilityOption.AllTypes,
                ContainerInstance.RegisterAll,
                typeof(IEventHandler<>).Assembly,
                typeof(CellDeadEventHandler).Assembly,
                typeof(GameEvolvedEventHandler).Assembly);
        }

        #endregion
    }
}