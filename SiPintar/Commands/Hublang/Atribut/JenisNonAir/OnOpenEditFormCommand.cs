using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Hublang.Atribut.JenisNonAir;

namespace SiPintar.Commands.Hublang.Atribut.JenisNonAir
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly JenisNonAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(JenisNonAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsEdit = true;
            _viewModel.FormData = _viewModel.SelectedData;
            _viewModel.FormDataDetail = new ObservableCollection<MasterJenisNonAirDetailDto>(_viewModel.SelectedData.DetailBiaya);
            if (!_viewModel.FormDataDetail.Any())
            {
                _viewModel.FormDataDetail = new ObservableCollection<MasterJenisNonAirDetailDto> { new MasterJenisNonAirDetailDto() };
            }
            await ShowFormDialogAsync();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowFormDialogAsync()
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedData != null) await DialogHost.Show(new DialogFormView(_viewModel), "HublangRootDialog");
                else await DialogHost.Show(new DialogGlobalView("Koreksi Jenis Non-Air", "Silahkan pilih data", "warning"), "HublangRootDialog");
            }
        }
    }
}
