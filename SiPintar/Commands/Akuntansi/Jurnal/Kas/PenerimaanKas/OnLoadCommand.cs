using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.Jurnal.Kas;

namespace SiPintar.Commands.Akuntansi.Jurnal.Kas.PenerimaanKas
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PenerimaanKasViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(PenerimaanKasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterPeriodeAkuntansi",
                "MasterWilayah",
            });

            _viewModel.PeriodeList = new(MasterListGlobal.MasterPeriodeAkuntansi.OrderByDescending(x => x.KodePeriode));
            _viewModel.SelectedUraian = null;
            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;
            if (_viewModel.WilayahList[0].IdWilayah != null)
                _viewModel.WilayahList.Insert(0, new MasterWilayahDto { KodeWilayah = "--", NamaWilayah = "Pilih Semua" });
            _viewModel.SelectedWilayah = _viewModel.WilayahList[0];
            _viewModel.IsKonsolidasi = true;

            await Task.FromResult(_isTest);
        }
    }
}
