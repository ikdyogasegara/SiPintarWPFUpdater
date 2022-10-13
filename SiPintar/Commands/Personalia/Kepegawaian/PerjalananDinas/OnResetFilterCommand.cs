using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(PerjalananDinasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;
            _viewModel.PerjalananDinasList = new ObservableCollection<SppdDto>();

            _viewModel.IsNoPegawaiChecked = false;
            _viewModel.IsNamaPegawaiChecked = false;
            _viewModel.IsNoSptChecked = false;
            _viewModel.IsNoSppdChecked = false;
            _viewModel.IsKeperluanChecked = false;
            _viewModel.IsTempatTujuanChecked = false;
            _viewModel.IsBebanAnggaranChecked = false;
            _viewModel.IsTglBerangkatChecked = false;
            _viewModel.IsTglKembaliChecked = false;

            _viewModel.FilterNoPegawai = null;
            _viewModel.FilterNamaPegawai = null;
            _viewModel.FilterNoSpt = null;
            _viewModel.FilterNoSppd = null;
            _viewModel.FilterKeperluan = null;
            _viewModel.FilterTempatTujuan = null;
            _viewModel.FilterBebanAnggaran = null;
            _viewModel.FilterTglBerangkat = null;
            _viewModel.FilterTglKembali = null;

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

    }
}
