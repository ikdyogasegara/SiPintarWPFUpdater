using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Distribusi.Atribut.KelainanGantiMeter;
using SiPintar.Views.Distribusi.Atribut.KelainanGantiMeter.GantiMeterNonRutin;

namespace SiPintar.Commands.Distribusi.Atribut.KelainanGantiMeter.GantiMeterNonRutin
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly GantiMeterNonRutinViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(GantiMeterNonRutinViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsAdd = true;

            _viewModel.IsCheckedDenganBiaya = false;
            _viewModel.FormGantiMeter = new ParamMasterJenisGantiMeterDto() { Kategori = "Non Rutin" };
            _viewModel.WarnaMeter = new MasterWarnaGantiMeterDto();
            _viewModel.JenisNonair = new MasterJenisNonAirDto();
            _viewModel.IsLoadingForm = false;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "DistribusiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
