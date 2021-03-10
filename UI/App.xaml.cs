using System;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using UI.Startup;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var container = Initializer.Build();
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Unexpected error occured " +
                            $"{Environment.NewLine} " +
                            $"{e.Exception.Message}",
                "Unexpected error");
            e.Handled = true;
        }
    }
}
