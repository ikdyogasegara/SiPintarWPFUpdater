using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Distribusi.Atribut.KelainanGantiMeter;
using SiPintar.Views.Distribusi.Atribut.KelainanGantiMeter.GantiMeterNonRutin;

namespace SiPintar.Commands.Distribusi.Atribut.KelainanGantiMeter.GantiMeterNonRutin
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly GantiMeterNonRutinViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(GantiMeterNonRutinViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsAdd = false;
            if (_viewModel.SelectedData?.JenisGantiMeter == null)
                return;

            _viewModel.FormGantiMeter = new ParamMasterJenisGantiMeterDto()
            {
                Kategori = _viewModel.SelectedData.Kategori,
                IdWarnaGantiMeter = _viewModel.SelectedData.IdWarnaGantiMeter,
                IdJenisGantiMeter = _viewModel.SelectedData.IdJenisGantiMeter,
                JenisGantiMeter = _viewModel.SelectedData.JenisGantiMeter,
                IdJenisNonAir = _viewModel.SelectedData.IdJenisNonAir,
                IdPdam = _viewModel.SelectedData.IdPdam,
            };

            _viewModel.WarnaMeter = _viewModel.WarnaMeterList.FirstOrDefault(x => x.IdWarnaGantiMeter == _viewModel.FormGantiMeter.IdWarnaGantiMeter);
            if (_viewModel.FormGantiMeter.IdJenisNonAir != null)
            {
                _viewModel.JenisNonair = _viewModel.JenisNonairList.FirstOrDefault(x => x.IdJenisNonAir == _viewModel.FormGantiMeter.IdJenisNonAir);
                _viewModel.IsCheckedDenganBiaya = true;
            }
            else
                _viewModel.IsCheckedDenganBiaya = false;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "DistribusiRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }
    }
}
