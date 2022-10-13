using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.DiklatPelatihan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly DiklatPelatihanViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(DiklatPelatihanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;
            _viewModel.DiklatPelatihanList = new ObservableCollection<DiklatDto>();

            _viewModel.IsNoPegawaiChecked = false;
            _viewModel.IsNamaPegawaiChecked = false;
            _viewModel.IsNoSertifikatChecked = false;
            _viewModel.IsUraianChecked = false;
            _viewModel.IsTempatChecked = false;
            _viewModel.IsPenyelenggaraChecked = false;
            _viewModel.IsTglAwalChecked = false;
            _viewModel.IsTglAkhirChecked = false;

            _viewModel.FilterNoPegawai = null;
            _viewModel.FilterNamaPegawai = null;
            _viewModel.FilterNoSertifikat = null;
            _viewModel.FilterUraian = null;
            _viewModel.FilterTempat = null;
            _viewModel.FilterPenyelenggara = null;
            _viewModel.FilterTglAwal = null;
            _viewModel.FilterTglAkhir = null;

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

    }
}
