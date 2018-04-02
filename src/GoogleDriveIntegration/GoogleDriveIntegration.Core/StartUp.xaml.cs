using System;
using System.Collections.Generic;
using GoogleDriveIntegration.Core.Views;
using Prism;
using Prism.Autofac;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoogleDriveIntegration.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartUp : PrismApplication
    {
        public StartUp(IPlatformInitializer platformInitializer = null) : base(platformInitializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync(nameof(NavigationPage) + "/" + nameof(HomeView));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomeView>();
        }
    }
}
