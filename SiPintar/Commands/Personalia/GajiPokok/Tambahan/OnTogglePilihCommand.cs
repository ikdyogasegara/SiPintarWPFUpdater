using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.Tambahan
{
    public class OnTogglePilihCommand : AsyncCommandBase
    {
        private readonly TambahanViewModel _viewModel;
        private readonly bool _isTest;

        public OnTogglePilihCommand(TambahanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!_viewModel.IsPilih)
            {
                if (_viewModel.FilterPeriode == null || _viewModel.FilterKodeGaji == null)
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(
                        _isTest,
                        true,
                        "PersonaliaRootDialog",
                        "Warning",
                        "Periode dan Kode Gaji harus dipilih",
                        "warning",
                        "OK",
                        false,
                        "personalia");
                }
                else
                {
                    _viewModel.IsPilih = true;
                    await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));
                }
            }
            else
            {
                _viewModel.IsPilih = false;
                await Task.Run(() => ((AsyncCommandBase)_viewModel.OnResetFilterCommand).ExecuteAsync("PeriodeAndKodeGaji"));
                _viewModel.TambahanList = new ObservableCollection<MasterPegawaiGajiTambahanDto>();
                _viewModel.IsEmpty = !_viewModel.TambahanList.Any();
            }

            await Task.FromResult(true);
        }
    }
}
