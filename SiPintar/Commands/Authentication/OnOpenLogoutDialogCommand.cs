using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Authentication
{
    [ExcludeFromCodeCoverage]
    public class OnOpenLogoutDialogCommand : AsyncCommandBase
    {
        public OnOpenLogoutDialogCommand() { }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = DialogHost.Show(new DialogLogout(), "MainRootDialog");

            await Task.FromResult(true);
        }
    }
}
