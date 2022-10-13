using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    public class CalculateBiayaCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly bool _isTest;

        public CalculateBiayaCommand(PendaftaranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var param = parameter as string;
            var ppn = (decimal)_viewModel.JenisNonAir.PersentasePpn;

            switch (param)
            {
                case "pendaftaran":
                    _viewModel.PpnBiayaPendaftaranForm = _viewModel.BiayaPendaftaranForm * (ppn / 100);
                    _viewModel.TotalBiayaPendaftaranForm = _viewModel.BiayaPendaftaranForm + _viewModel.PpnBiayaPendaftaranForm;
                    break;
                case "opname":
                    _viewModel.PpnBiayaOpnameForm = _viewModel.BiayaOpnameForm * (ppn / 100);
                    _viewModel.TotalBiayaOpnameForm = _viewModel.BiayaOpnameForm + _viewModel.PpnBiayaOpnameForm;
                    break;
                case "formulir":
                    _viewModel.PpnBiayaFormulirForm = _viewModel.BiayaFormulirForm * (ppn / 100);
                    _viewModel.TotalBiayaFormulirForm = _viewModel.BiayaFormulirForm + _viewModel.PpnBiayaFormulirForm;
                    break;
                case "jaminan":
                    _viewModel.PpnBiayaJaminanForm = _viewModel.BiayaJaminanForm * (ppn / 100);
                    _viewModel.TotalBiayaJaminanForm = _viewModel.BiayaJaminanForm + _viewModel.PpnBiayaJaminanForm;
                    break;
            }

            _viewModel.TotalBiayaSambunganBaruForm = _viewModel.TotalBiayaPendaftaranForm
                                                     + _viewModel.TotalBiayaOpnameForm
                                                     + _viewModel.TotalBiayaFormulirForm
                                                     + _viewModel.TotalBiayaJaminanForm;

            await Task.FromResult(_isTest);
        }
    }
}
