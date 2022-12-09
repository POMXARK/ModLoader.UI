using Avalonia;
using Avalonia.Markup.Xaml;
using ModLoader.Views;
using Prism.DryIoc;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;

namespace ModLoader
{
    public class App : PrismApplication
    {
        // Note:
        //  Though, Prism.WPF v8.1 uses, `protected virtual void Initialize()`
        //  Avalonia's AppBuilderBase.cs calls, `.Setup() { ... Instance.Initialize(); ... }`
        //  Therefore, we need this as a `public override void` in PrismApplicationBase.cs
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            // DON'T FORGET TO CALL THIS
            base.Initialize();
        }


        /// <summary>Called after Initialize.</summary>
        protected override void OnInitialized()
        {
            // Register Views to the Region it will appear in. Don't register them in the ViewModel.
            var regionManager = Container.Resolve<IRegionManager>();
            //regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardView));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Views - Generic
            containerRegistry.Register<MainWindow>();
            containerRegistry.RegisterSingleton<Database>();

            //containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
            //containerRegistry.RegisterSingleton<ITwoWindowViewModel, TwoWindowViewModel>();

            // Services

            //containerRegistry.RegisterSingleton<INavigationViewModel, NavigationViewModel>();
        }

        /// <summary>User interface entry point, called after Register and ConfigureModules.</summary>
        /// <returns>Startup View.</returns>
        protected override IAvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}