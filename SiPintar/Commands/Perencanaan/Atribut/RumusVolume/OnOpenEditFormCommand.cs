using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut;
using SiPintar.Views.Perencanaan.Atribut.RumusVolume;

namespace SiPintar.Commands.Perencanaan.Atribut.RumusVolume
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly RumusVolumeViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(RumusVolumeViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsLoadingForm = false;
            _viewModel.IsEdit = true;

            _viewModel.IsBatas1Active = _viewModel.SelectedData.Bb1 > 0 || _viewModel.SelectedData.Ba1 > 0;
            _viewModel.IsBatas2Active = _viewModel.SelectedData.Bb2 > 0 || _viewModel.SelectedData.Ba2 > 0;
            _viewModel.IsBatas3Active = _viewModel.SelectedData.Bb3 > 0 || _viewModel.SelectedData.Ba3 > 0;
            _viewModel.IsBatas4Active = _viewModel.SelectedData.Bb4 > 0 || _viewModel.SelectedData.Ba4 > 0;
            _viewModel.IsBatas5Active = _viewModel.SelectedData.Bb5 > 0 || _viewModel.SelectedData.Ba5 > 0;

            _viewModel.SelectedOngkos = _viewModel.OngkosList.FirstOrDefault(i => i.IdOngkos == _viewModel.SelectedData.IdOngkos);
            _viewModel.FormData = new MasterRumusVolumeOngkosDto()
            {
                IdRumusVolumeOngkos = _viewModel.SelectedData.IdRumusVolumeOngkos,
                IdOngkos = _viewModel.SelectedData.IdOngkos,
                Bb1 = _viewModel.SelectedData.Bb1,
                Ba1 = _viewModel.SelectedData.Ba1,
                Volum1 = _viewModel.SelectedData.Volum1,
                Bb2 = _viewModel.SelectedData.Bb2,
                Ba2 = _viewModel.SelectedData.Ba2,
                Volum2 = _viewModel.SelectedData.Volum2,
                Bb3 = _viewModel.SelectedData.Bb3,
                Ba3 = _viewModel.SelectedData.Ba3,
                Volum3 = _viewModel.SelectedData.Volum3,
                Bb4 = _viewModel.SelectedData.Bb4,
                Ba4 = _viewModel.SelectedData.Ba4,
                Volum4 = _viewModel.SelectedData.Volum4,
                Bb5 = _viewModel.SelectedData.Bb5,
                Ba5 = _viewModel.SelectedData.Ba5,
                Volum5 = _viewModel.SelectedData.Volum5,
            };

            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogFormView(_viewModel), "PerencanaanRootDialog");
        }
    }
}
