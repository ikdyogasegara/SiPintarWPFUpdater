using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views.Hublang.Atribut.JenisNonAir;

namespace SiPintar.Commands.Hublang.Atribut.JenisNonAir
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly JenisNonAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(JenisNonAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsEdit = false;
            _viewModel.FormData = new MasterJenisNonAirDto();
            _viewModel.FormDataDetail = new ObservableCollection<MasterJenisNonAirDetailDto> { new MasterJenisNonAirDetailDto() };

            await ShowFormDialogAsync();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowFormDialogAsync()
        {
            if (!_isTest) await DialogHost.Show(new DialogFormView(_viewModel), "HublangRootDialog");
        }
    }
}
