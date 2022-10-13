using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.GajiPokok;
using SiPintar.Views.Personalia.GajiPokok.TenagaHarian;

namespace SiPintar.Commands.Personalia.GajiPokok.TenagaHarian
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly TenagaHarianViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(TenagaHarianViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                _viewModel.FormGaji = _viewModel.SelectedData.Jumlah;
                _viewModel.FormKeteranganGaji = _viewModel.SelectedData.Tipe;
                _viewModel.SelectedDataPegawaiForm = new MasterPegawaiDto
                {
                    IdPegawai = _viewModel.SelectedData.IdPegawai,
                    NoPegawai = _viewModel.SelectedData.NoPegawai,
                    NamaPegawai = _viewModel.SelectedData.NamaPegawai,
                };
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi Gaji Tenaga Harian",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }
    }
}
