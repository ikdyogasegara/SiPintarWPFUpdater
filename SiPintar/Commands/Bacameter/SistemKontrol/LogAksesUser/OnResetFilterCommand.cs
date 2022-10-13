using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.LogAksesUser
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly LogAksesUserViewModel _viewModel;

        public OnResetFilterCommand(LogAksesUserViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsStatusChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsRentangWaktuChecked = false;
            _viewModel.IsRoleChecked = false;
            _viewModel.IsTipeChecked = false;

            _viewModel.FilterData = new ParamMasterLoggerFilterDto();
            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }

    }
}
