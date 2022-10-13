using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;
using SiPintar.Views.Akuntansi.MasterData.MasterDataKeuangan.SaldoAwal;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.SaldoAwal
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly SaldoAwalViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(SaldoAwalViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.SaldoAwalForm = new()
            {
                TotalSaldoKasBank = _viewModel.Data!.TotalSaldoKasBank,
                SaldoAwalRekon = new List<SaldoAwalRekonDto>(_viewModel.Data.SaldoAwalRekon.Select(x => new SaldoAwalRekonDto
                {
                    IdBank = x.IdBank,
                    IdSaldoKasBank = x.IdSaldoKasBank,
                    NamaBank = x.NamaBank,
                    TanggalSaldo = x.TanggalSaldo,
                    JumlahSaldo = x.JumlahSaldo,
                    JumlahSaldoOri = x.JumlahSaldoOri
                }))
            };

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
