using System.Threading.Tasks;
using SiPintar.ViewModels.Distribusi.Distribusi;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeter
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly GantiMeterViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(GantiMeterViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsTahunChecked = false;
            _viewModel.IsKategoriChecked = false;
            _viewModel.IsKelainanChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsNomorSpkChecked = false;
            _viewModel.IsNomorBaChecked = false;
            _viewModel.IsStatusDataChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsGolonganChecked = false;
            _viewModel.IsRayonChecked = false;
            _viewModel.IsAreaChecked = false;

            await Task.FromResult(_isTest);
        }
    }
}
