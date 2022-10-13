using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.TunjanganBulanan
{
    public class OnNextFormPageCommand : AsyncCommandBase
    {
        private readonly TunjanganBulananViewModel _viewModel;
        private readonly bool _isTest;

        public OnNextFormPageCommand(TunjanganBulananViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.FormCurrentPage == 1)
                _viewModel.FormCurrentPage = 2;
            else
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, false, "PersonaliaInnerDialog",
                    _viewModel.IsEdit ? "Konfirmasi Koreksi Tunjangan Bulanan" : "Konfirmasi Tambah Tunjangan Bulanan",
                    _viewModel.IsEdit ? $"Anda akan mengoreksi pengaturan tunjangan untuk {_viewModel.JenisTunjanganListForm.FirstOrDefault(j => j.IdJenisTunjangan == _viewModel.FormJenisTunjangan).NamaJenisTunjangan} ?" : $"Anda akan menambahkan pengaturan tunjangan untuk {_viewModel.JenisTunjanganListForm.FirstOrDefault(j => j.IdJenisTunjangan == _viewModel.FormJenisTunjangan).NamaJenisTunjangan} ?",
                    "confirmation", _viewModel.OnSubmitFormCommand, "Simpan", "Batal",
                    false, false, "Personalia");

            await Task.FromResult(true);
        }


    }
}
