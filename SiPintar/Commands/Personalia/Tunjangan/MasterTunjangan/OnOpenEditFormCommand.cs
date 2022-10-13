using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Tunjangan;
using SiPintar.Views.Personalia.Tunjangan.MasterTunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.MasterTunjangan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly MasterTunjanganViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(MasterTunjanganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEdit = true;

            if (_viewModel.SelectedData != null)
            {
                _viewModel.FormKode = _viewModel.SelectedData.KodeJenisTunjangan;
                _viewModel.FormNamaTunjangan = _viewModel.SelectedData.NamaJenisTunjangan;
                _viewModel.FormUrutan = _viewModel.SelectedData.Urutan;
                _viewModel.FormTipe = _viewModel.SelectedData.Tipe;
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi Master Tunjangan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }
    }
}
