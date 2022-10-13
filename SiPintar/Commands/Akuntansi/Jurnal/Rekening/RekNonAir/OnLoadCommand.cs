using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.Jurnal.Rekening;

namespace SiPintar.Commands.Akuntansi.Jurnal.Rekening.RekNonAir
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly RekNonAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(RekNonAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterPeriodeAkuntansi",
                "MasterWilayah"
            });

            _viewModel.PeriodeList = new(MasterListGlobal.MasterPeriodeAkuntansi.DistinctBy(x => x.KodePeriode).OrderByDescending(x => x.Tahun));

            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;
            if (_viewModel.WilayahList[0].IdWilayah != null)
                _viewModel.WilayahList.Insert(0, new MasterWilayahDto { KodeWilayah = "--", NamaWilayah = "Pilih Semua" });
            _viewModel.SelectedWilayah = _viewModel.WilayahList[0];

            await Task.FromResult(_isTest);
        }
    }
}
