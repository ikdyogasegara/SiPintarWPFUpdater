using System.Collections.Generic;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Commands.Perencanaan.Atribut.Ongkos
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly OngkosViewModel _viewModel;

        public OnResetFilterCommand(OngkosViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsKodeOngkosChecked = false;
            _viewModel.IsNamaOngkosChecked = false;

            _viewModel.SelectedDataKategori = new KeyValuePair<string, bool>("Standard", false);

            _viewModel.OngkosFilter = new MasterOngkosDto();
            _viewModel.OnFilterCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
