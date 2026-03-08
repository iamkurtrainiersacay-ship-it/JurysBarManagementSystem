    using JurysBarManagementSystem.Database;
    using System.Windows;

    namespace JurysBarManagementSystem
    {
        public partial class App : Application
        {
            protected override void OnStartup(StartupEventArgs e)
            {
                base.OnStartup(e);
                DatabaseInitializer.Initialize();
            }
        }
    }