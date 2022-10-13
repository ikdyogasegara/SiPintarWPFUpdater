using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Main;
using SiPintar.Views.Main.ManajementParaf;

namespace SiPintar.Commands.Main.ManajementParaf
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly ManajementParafViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(ManajementParafViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.FormData = new ParamMasterParafDto()
            {
                IdParaf = _viewModel.SelectedData.IdParaf,
                NamaPdam = _viewModel.SelectedData.NamaPdam,
                Key = _viewModel.SelectedData.Key,

                Keterangan1 = _viewModel.SelectedData.Keterangan1,
                Jabatan1 = _viewModel.SelectedData.Jabatan1,
                NoId1 = _viewModel.SelectedData.NoId1,
                Nama1 = _viewModel.SelectedData.Nama1,

                Keterangan2 = _viewModel.SelectedData.Keterangan2,
                Jabatan2 = _viewModel.SelectedData.Jabatan2,
                NoId2 = _viewModel.SelectedData.NoId2,
                Nama2 = _viewModel.SelectedData.Nama2,

                Keterangan3 = _viewModel.SelectedData.Keterangan3,
                Jabatan3 = _viewModel.SelectedData.Jabatan3,
                NoId3 = _viewModel.SelectedData.NoId3,
                Nama3 = _viewModel.SelectedData.Nama3,

                Keterangan4 = _viewModel.SelectedData.Keterangan4,
                Jabatan4 = _viewModel.SelectedData.Jabatan4,
                NoId4 = _viewModel.SelectedData.NoId4,
                Nama4 = _viewModel.SelectedData.Nama4,

                Keterangan5 = _viewModel.SelectedData.Keterangan5,
                Jabatan5 = _viewModel.SelectedData.Jabatan5,
                NoId5 = _viewModel.SelectedData.NoId5,
                Nama5 = _viewModel.SelectedData.Nama5,
            };

            OpenDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new DialogFormView(_viewModel), "MainRootDialog");
        }
    }
}
