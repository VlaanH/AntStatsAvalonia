using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using OxyPlot.Avalonia;

namespace AntStats.Avalonia
{
    public class App : Application
    {
        public App()
        {
            RegisterServices();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (!(ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
            {
                throw new PlatformNotSupportedException();
            }

            desktop.MainWindow = new Stats();

            base.OnFrameworkInitializationCompleted();
        }

    }
}