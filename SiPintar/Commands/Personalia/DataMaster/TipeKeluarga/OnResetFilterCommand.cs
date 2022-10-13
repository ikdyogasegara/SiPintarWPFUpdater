using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.TipeKeluarga
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly TipeKeluargaViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(TipeKeluargaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;
            _viewModel.TipeKeluargaList = new ObservableCollection<MasterTipeKeluargaDto>();

            _viewModel.IsKodeTipeKeluargaChecked = false;
            _viewModel.IsUraianChecked = false;

            _viewModel.FilterKodeTipeKeluarga = null;
            _viewModel.FilterUraian = null;

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

    }
}
