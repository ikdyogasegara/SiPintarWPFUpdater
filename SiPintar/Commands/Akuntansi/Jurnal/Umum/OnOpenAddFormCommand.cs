using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.Jurnal;
using SiPintar.Views.Akuntansi.Jurnal.Umum;

namespace SiPintar.Commands.Akuntansi.Jurnal.Umum
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly UmumViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(UmumViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsAdd = true;
            ExecuteCloseDialog();
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ExecuteCloseDialog()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate { CloseDialog(); });
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new DialogFormView(_viewModel);

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

    }
}
