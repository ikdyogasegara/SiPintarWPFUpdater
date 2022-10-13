using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.Jurnal.Rekening;

namespace SiPintar.Commands.Akuntansi.Jurnal.Rekening.RekAir
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly RekAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(RekAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterPeriodeAkuntansi"
            });

            _viewModel.PeriodeList = new ObservableCollection<MasterPeriodeAkuntansiDto>(MasterListGlobal.MasterPeriodeAkuntansi.OrderByDescending(x => x.KodePeriode));

            await Task.FromResult(_isTest);
        }
    }
}
