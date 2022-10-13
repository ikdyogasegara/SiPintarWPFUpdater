using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.MutasiJabatan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly MutasiJabatanViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(MutasiJabatanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;
            _viewModel.MutasiJabatanList = new ObservableCollection<MutasiJabatanDto>();

            _viewModel.IsNoPegawaiChecked = false;
            _viewModel.IsNamaPegawaiChecked = false;
            _viewModel.IsJabatanChecked = false;
            _viewModel.IsDepartemenChecked = false;
            _viewModel.IsDivisiChecked = false;
            _viewModel.IsAreaKerjaChecked = false;
            _viewModel.IsJabatanBaruChecked = false;
            _viewModel.IsDepartemenBaruChecked = false;
            _viewModel.IsDivisiBaruChecked = false;
            _viewModel.IsAreaKerjaBaruChecked = false;

            _viewModel.FilterNoPegawai = null;
            _viewModel.FilterNamaPegawai = null;
            _viewModel.FilterJabatan = null;
            _viewModel.FilterDepartemen = null;
            _viewModel.FilterDivisi = null;
            _viewModel.FilterAreaKerja = null;
            _viewModel.FilterJabatanBaru = null;
            _viewModel.FilterDepartemenBaru = null;
            _viewModel.FilterDivisiBaru = null;
            _viewModel.FilterAreaKerjaBaru = null;

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

    }
}
