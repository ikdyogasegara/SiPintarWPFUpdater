using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.Jurnal.Instalasi;

namespace SiPintar.Commands.Akuntansi.Jurnal.Instalasi.DaftarHutang
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DaftarHutangViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(DaftarHutangViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                "MasterWilayah",
            });

            _viewModel.PeriodeAwal = DateTime.Now;
            _viewModel.PeriodeAkhir = DateTime.Now;
            _viewModel.SelectedProses = null;
            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;
            _viewModel.SelectedWilayah = _viewModel.WilayahList[0];
            _viewModel.IsKonsolidasi = true;

            await Task.FromResult(_isTest);
        }
    }
}
