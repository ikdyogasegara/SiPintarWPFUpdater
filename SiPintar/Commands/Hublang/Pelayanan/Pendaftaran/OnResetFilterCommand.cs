using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(PendaftaranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsCabangChecked = false;
            _viewModel.IsNomorRegisterChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsTanggalInputChecked = false;
            _viewModel.IsUserInputChecked = false;
            _viewModel.IsUserBeritaAcaraChecked = false;

            _viewModel.FilterCabang = null;
            _viewModel.FilterNomorRegister = null;
            _viewModel.FilterNama = null;
            _viewModel.FilterAlamat = null;
            _viewModel.FilterWilayah = null;
            _viewModel.FilterTanggalInputAwal = null;
            _viewModel.FilterTanggalInputAkhir = null;
            _viewModel.FilterUserInput = null;
            _viewModel.FilterUserBeritaAcara = null;

            _viewModel.FilterData = new ParamPermohonanNonPelangganFilterDto();

            await Task.FromResult(_isTest);
        }
    }
}
