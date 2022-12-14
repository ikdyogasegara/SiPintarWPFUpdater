using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.Direksi
{
    public class OnSelectPegawaiCommand : AsyncCommandBase
    {
        private readonly DireksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnSelectPegawaiCommand(DireksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsBerdasarkanGajiPegawai)
                _viewModel.SelectedDataPegawaiRefForm = (MasterPegawaiDto)_viewModel.SelectedDataPegawai.Clone();
            else
                _viewModel.SelectedDataPegawaiForm = (MasterPegawaiDto)_viewModel.SelectedDataPegawai.Clone();

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
