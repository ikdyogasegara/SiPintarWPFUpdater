using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnSavePesertaBiayaFormCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly bool _isTest;

        public OnSavePesertaBiayaFormCommand(PerjalananDinasViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!_viewModel.IsEditPesertaBiaya)
            {
                _viewModel.FormDeskripsi = _viewModel.SppdBiayaFormList.FirstOrDefault(k => k.IdBiayaSppd == _viewModel.FormBiayaSppd)?.Deskripsi;
                _viewModel.FormPesertaBiayaList.Add(new SppdPesertaBiayaDto
                {
                    IdBiayaSppd = _viewModel.FormBiayaSppd,
                    Deskripsi = _viewModel.FormDeskripsi,
                    Biaya = _viewModel.FormBiaya,
                    Qty = _viewModel.FormQty,
                    Jumlah = _viewModel.FormJumlah,
                    Keterangan = _viewModel.FormKeteranganBiaya,
                });
            }
            else
            {
                var index = _viewModel.FormPesertaBiayaList.IndexOf(_viewModel.SelectedDataPesertaBiaya);

                var formPesertaBiaya = (SppdPesertaBiayaDto)_viewModel.FormPesertaBiayaList[index].Clone();

                formPesertaBiaya.Deskripsi = _viewModel.SppdBiayaFormList.FirstOrDefault(k => k.IdBiayaSppd == _viewModel.FormBiayaSppd)?.Deskripsi;
                formPesertaBiaya.IdBiayaSppd = _viewModel.FormBiayaSppd;
                formPesertaBiaya.Biaya = _viewModel.FormBiaya;
                formPesertaBiaya.Qty = _viewModel.FormQty;
                formPesertaBiaya.Jumlah = _viewModel.FormJumlah;
                formPesertaBiaya.Keterangan = _viewModel.FormKeteranganBiaya;

                _viewModel.FormPesertaBiayaList[index] = (SppdPesertaBiayaDto)formPesertaBiaya.Clone();
            }

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerInnerDialog");

            await Task.FromResult(_isTest);
        }
    }
}
