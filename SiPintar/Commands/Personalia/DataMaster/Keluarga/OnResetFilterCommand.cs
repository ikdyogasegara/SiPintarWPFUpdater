using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Keluarga
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly KeluargaViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(KeluargaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;
            _viewModel.KeluargaList = new ObservableCollection<MasterKeluargaWpf>();

            _viewModel.IsNoPegawaiChecked = false;
            _viewModel.IsNamaPegawaiChecked = false;
            _viewModel.IsStatusPegawaiChecked = false;
            _viewModel.IsNamaKeluargaChecked = false;

            _viewModel.FilterNoPegawai = null;
            _viewModel.FilterNamaPegawai = null;
            _viewModel.FilterStatusPegawai = null;
            _viewModel.FilterNamaKeluarga = null;

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

    }
}
