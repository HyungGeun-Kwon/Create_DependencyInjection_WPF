﻿using System.Windows;
using MVVMKit.App;
using MVVMKit.DI;
using MVVMKit.MVVM;
using MVVMKitSample.Application.ServiceA;
using MVVMKitSample.Infrastructure.ServiceA;
using MVVMKitSample.ViewModels;
using MVVMKitSample.Views;
using MVVMKit.Modules;
using MVVMKitSample.UI.ViewA.Views;
using MVVMKitSample.UI.Core.Names;
using MVVMKitSample.UI.ViewB.Views;
using MVVMKitSample.UI.DialogA.Views;
using MVVMKitSample.UI.DialogA.Modules;
using MVVMKitSample.UI.ViewA.Modules;
using MVVMKitSample.UI.ViewB.Modules;

namespace MVVMKitSample
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : MVVMKitApplication
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            WireDataContext();
            base.OnStartup(e);
        }
        private void WireDataContext()
        {
            ViewModelLocator.WireViewViewModel<MainWindow, MainViewModel>();
        }
        protected override Window CreateShell(IFrameworkContainerProvider frameworkContainerProvider)
        {
            // IContainerProvider의 Resolve는 ViewModel연결을 자동으로 해주지 않음.
            return frameworkContainerProvider.ResolveFrameworkElement<MainWindow>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IServiceA, ServiceA>();

            // 모듈을 사용하지 않고 Register 할 경우

            // containerRegistry.RegisterDialog<ADialog, ADialogViewModel>(ViewNames.ADialog);
            // containerRegistry.RegisterForNavigation<AView, AViewModel>(ViewNames.AView);
            // containerRegistry.RegisterForNavigation<BView, BViewModel>(ViewNames.BView);

            // 위 아래 동일

            // containerRegistry.RegisterDialog<ADialog>(ViewNames.ADialog);
            // containerRegistry.RegisterForNavigation<AView>(ViewNames.AView);
            // containerRegistry.RegisterForNavigation<BView>(ViewNames.BView);
            // ViewModelLocator.WireViewViewModel<ADialog, ADialogViewModel>();
            // ViewModelLocator.WireViewViewModel<AView, AViewModel>();
            // ViewModelLocator.WireViewViewModel<BView, BViewModel>();
        }
        protected override void AddModule(IModuleCatalog moduleCatalog)
        {
            // 모듈을 사용해 Register 할 경우
            moduleCatalog.AddModule<DialogAModule>();
            moduleCatalog.AddModule<ViewAModule>();
            moduleCatalog.AddModule<ViewBModule>();
        }
    }
}
