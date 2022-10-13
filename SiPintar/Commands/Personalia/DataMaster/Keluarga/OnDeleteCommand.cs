using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Keluarga
{
    public class OnDeleteCommand : AsyncCommandBase
    {
        private readonly KeluargaViewModel _viewModel;
        private readonly bool _isTest;

        public OnDeleteCommand(KeluargaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedAnggotaKeluarga != null)
            {
                _viewModel.SelectedKeluargaListForm.Remove(_viewModel.SelectedAnggotaKeluarga);
            }

            await Task.FromResult(_isTest);
        }
    }
}
