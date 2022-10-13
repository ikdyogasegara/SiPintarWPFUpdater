using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.JenisNonAir
{
    public class OnRemoveDetailBiayaCommand : AsyncCommandBase
    {
        private readonly JenisNonAirViewModel _viewModel;

        public OnRemoveDetailBiayaCommand(JenisNonAirViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var data = parameter as MasterJenisNonAirDetailDto;
            if (_viewModel.FormDataDetail.Count > 1)
                _viewModel.FormDataDetail.Remove(data);

            await Task.FromResult(true);
        }
    }
}
