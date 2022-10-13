using System;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.PeriodePosting;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PeriodePosting
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        public readonly PeriodePostingViewModel Vm;
        public readonly bool IsTest;
        public OnOpenAddFormCommand(PeriodePostingViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var LastPeriode = Vm.Data.OrderByDescending(x => x.IdPeriode).FirstOrDefault();
            if (LastPeriode != null)
            {
                int? BulanTerakhir = Convert.ToInt32(LastPeriode.KodePeriode.ToString().Substring(4));
                int? TahunTerakhir = Convert.ToInt32(LastPeriode.KodePeriode.ToString().Substring(0, 4));
                if (BulanTerakhir.HasValue && TahunTerakhir.HasValue)
                {
                    var NextPeriodeDate = new DateTime(year: TahunTerakhir.Value, month: BulanTerakhir.Value, 1).AddMonths(1);
                    Vm.BulanPeriode = Vm.DaftarBulan.FirstOrDefault(x => x.Key == NextPeriodeDate.Month);
                    Vm.TahunPeriode = Vm.DaftarTahun.FirstOrDefault(x => x.Key == NextPeriodeDate.Year);
                }
            }

            _ = await DialogHelper.ShowCustomDialogViewAsync(
                isTest: IsTest,
                dispatcher: true,
                identfier: "GudangRootDialog",
                getDialog: GetInstance
            );
            await Task.FromResult(IsTest);
        }

        private object GetInstance() => new DialogFormView(Vm);
    }
}
