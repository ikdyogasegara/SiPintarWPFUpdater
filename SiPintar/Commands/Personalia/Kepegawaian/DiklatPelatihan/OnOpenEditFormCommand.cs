using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.DiklatPelatihan;

namespace SiPintar.Commands.Personalia.Kepegawaian.DiklatPelatihan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly DiklatPelatihanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(DiklatPelatihanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsEdit = true;

            if (_viewModel.SelectedDataTable != null)
            {
                _viewModel.SelectedData = _viewModel.DiklatPelatihanList.FirstOrDefault(d => d.IdDiklat == _viewModel.SelectedDataTable.IdDiklat);
                _viewModel.FormNoSertifikat = _viewModel.SelectedData.NoSertifikat;
                _viewModel.FormTglAwal = _viewModel.SelectedData.TglAwal;
                _viewModel.FormTglAkhir = _viewModel.SelectedData.TglAkhir;
                _viewModel.FormUraian = _viewModel.SelectedData.Uraian;
                _viewModel.FormTempat = _viewModel.SelectedData.Tempat;
                _viewModel.FormPenyelenggara = _viewModel.SelectedData.Penyelenggara;
                _viewModel.FormDetailList = new ObservableCollection<DiklatDetailDto>(_viewModel.SelectedData.Detail);
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi Diklat & Pelatihan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }
    }
}
