using System.Collections.Generic;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Commands.Perencanaan.Atribut.Material
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly MaterialViewModel _viewModel;

        public OnResetFilterCommand(MaterialViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsKodeMaterialChecked = false;
            _viewModel.IsNamaMaterialChecked = false;

            _viewModel.SelectedDataKategori = new KeyValuePair<string, bool>("Standard", false);

            _viewModel.MaterialFilter = new MasterMaterialDto();
            _viewModel.OnFilterCommand.Execute(null);

            await Task.FromResult(true);
        }

    }
}
