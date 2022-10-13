using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.ViewModels;
using SiPintar.Views;

namespace SiPintar.Commands.Authentication
{
    [ExcludeFromCodeCoverage]
    public class OnOpenMainMenuCommand : AsyncCommandBase
    {
        private readonly MainViewModel _parent;
        private readonly bool _isTest;

        public OnOpenMainMenuCommand(MainViewModel parent, bool isTest = false)
        {
            _parent = parent;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var currentModule = parameter as string;

            _parent.IsMinimizeToTray = false;

            if (!_isTest && !App.OpenedWindow.ContainsKey("main"))
            {
                var Window = new MainView(_parent);
                Window.Show();
                Window.Activate();

                App.OpenedWindow.Add("main", Window);
            }
            else
            {
                App.OpenedWindow["main"].Show();
                _parent.WindowState = WindowState.Normal;
                App.OpenedWindow["main"].Activate();
            }

            App.OpenedWindow[currentModule].Close();
            App.OpenedWindow.Remove(currentModule);
            //((MainViewModel)App.OpenedWindow["main"].DataContext).OnLoadCommand.Execute(null) COMMENT OUT FOR SPEEDUP APP
            AppDispatcherHelper.Run(_isTest, () =>
            {
                Application.Current.Resources["CURRENT_MODULE"] = "DEFAULT";
            });
            await Task.FromResult(true);
        }
    }
}
