using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.MasterTunjangan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly MasterTunjanganViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(MasterTunjanganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;
            _viewModel.MasterTunjanganList = new ObservableCollection<MasterJenisTunjanganDto>();

            _viewModel.IsKodeJenisTunjanganChecked = false;
            _viewModel.IsNamaJenisTunjanganChecked = false;
            _viewModel.IsTipeChecked = false;

            _viewModel.FilterKodeJenisTunjangan = null;
            _viewModel.FilterNamaJenisTunjangan = null;
            _viewModel.FilterTipe = null;

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }

    }
}
