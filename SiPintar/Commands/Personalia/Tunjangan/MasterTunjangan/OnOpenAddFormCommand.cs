using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Tunjangan;
using SiPintar.Views.Personalia.Tunjangan.MasterTunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.MasterTunjangan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly MasterTunjanganViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(MasterTunjanganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEdit = false;
            _viewModel.FormKode = null;
            _viewModel.FormNamaTunjangan = null;
            _viewModel.FormUrutan = 0;
            _viewModel.FormTipe = null;
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }
    }
}
