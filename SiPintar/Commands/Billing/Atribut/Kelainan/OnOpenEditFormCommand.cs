using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views.Billing.Atribut.Kelainan;

namespace SiPintar.Commands.Billing.Atribut.Kelainan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly KelainanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(KelainanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.FormData = new AppBusiness.Data.DTOs.MasterKelainanDto()
            {
                IdKelainan = _viewModel.SelectedData.IdKelainan,
                KodeKelainan = _viewModel.SelectedData.KodeKelainan,
                Kelainan = _viewModel.SelectedData.Kelainan,
                JenisKelainan = _viewModel.SelectedData.JenisKelainan,
                Deskripsi = _viewModel.SelectedData.Deskripsi,
                Posisi = _viewModel.SelectedData.Posisi,
                Status = _viewModel.SelectedData.Status,
            };

            var Status = _viewModel.FormData.Status == true ? 1 : 0;

            _viewModel.JenisKelainanForm = _viewModel.JenisKelainanList.FirstOrDefault(i => i.Value == _viewModel.FormData.JenisKelainan);
            _viewModel.StatusForm = _viewModel.StatusList.FirstOrDefault(i => i.Key == Status);

            ShowDialog();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogFormView(_viewModel), "BillingRootDialog");
        }
    }
}
