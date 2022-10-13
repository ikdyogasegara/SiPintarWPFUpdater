using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.JenisNonAir
{
    public class OnAddDetailBiayaCommand : AsyncCommandBase
    {
        private readonly JenisNonAirViewModel _viewModel;

        public OnAddDetailBiayaCommand(JenisNonAirViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.FormDataDetail.Add(new MasterJenisNonAirDetailDto());

            await Task.FromResult(true);
        }

    }
}
