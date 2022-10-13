using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Personalia.DataMaster.Golongan;

namespace SiPintar.Commands.Personalia.DataMaster.Golongan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly GolonganViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(GolonganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsEdit = false;
            _viewModel.FormKodeGolongan = null;
            _viewModel.FormGolongan = null;
            _viewModel.FormUrutan = null;
            _viewModel.FormTingkat = null;
            _viewModel.FormTunjangan = null;
            _viewModel.UrutanList = new ObservableCollection<int>(Enumerable.Range(1, 100).ToList());
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));

            await Task.FromResult(_isTest);
        }
    }
}
