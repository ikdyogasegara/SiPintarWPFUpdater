using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut;
using SiPintar.Views.Perencanaan.Atribut.Material;

namespace SiPintar.Commands.Perencanaan.Atribut.Material
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly MaterialViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenEditFormCommand(MaterialViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.MaterialForm = (MasterMaterialDto)_viewModel.SelectedData.Clone();

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            _viewModel.IsLoadingForm = false;
            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest)
            {
                _ = DialogHost.Show(new DialogFormView(_viewModel), "PerencanaanRootDialog");
            }
        }
    }
}
