using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Commands.Personalia.GajiPokok.TenagaHarian
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly TenagaHarianViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(TenagaHarianViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;
            _viewModel.TenagaHarianList = new ObservableCollection<MasterPegawaiGajiHarianDto>();

            _viewModel.IsNoPegawaiChecked = false;
            _viewModel.IsNamaPegawaiChecked = false;
            _viewModel.IsAgamaChecked = false;
            _viewModel.IsAreaKerjaChecked = false;
            _viewModel.IsDepartemenChecked = false;
            _viewModel.IsDivisiChecked = false;
            _viewModel.IsJabatanChecked = false;
            _viewModel.IsJenisKelaminChecked = false;
            _viewModel.IsKawinChecked = false;
            _viewModel.IsPendidikanChecked = false;

            _viewModel.FilterNoPegawai = null;
            _viewModel.FilterNamaPegawai = null;
            _viewModel.FilterAgama = null;
            _viewModel.FilterAreaKerja = null;
            _viewModel.FilterDepartemen = null;
            _viewModel.FilterDivisi = null;
            _viewModel.FilterJabatan = null;
            _viewModel.FilterJenisKelamin = null;
            _viewModel.FilterKawin = null;
            _viewModel.FilterPendidikan = null;

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

    }
}
