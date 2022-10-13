using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.GajiPokok;
using SiPintar.Views.Personalia.GajiPokok.TenagaKontrak;

namespace SiPintar.Commands.Personalia.GajiPokok.TenagaKontrak
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly TenagaKontrakViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(TenagaKontrakViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEdit = false;
            _viewModel.FormGaji = 0;
            _viewModel.SelectedDataPegawaiForm = new MasterPegawaiDto();
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }
    }
}
