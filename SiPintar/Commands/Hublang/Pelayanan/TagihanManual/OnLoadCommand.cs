using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(TagihanManualViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.DataList = null;
            _viewModel.CurrentPage = 1;

            _viewModel.IsStatusChecked = true;
            _viewModel.FilterStatus = "Belum Lunas";

            ListJenisPelanggan();

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterGolongan",
                "MasterTarifLimbah",
                "MasterTarifLltt",
                "MasterRayon",
                "MasterKelurahan",
                "MasterJenisNonAir",
                "MasterWilayah",
            });

            _viewModel.GolonganList = MasterListGlobal.MasterGolongan;
            _viewModel.TarifLimbahList = MasterListGlobal.MasterTarifLimbah;
            _viewModel.TarifLlttList = MasterListGlobal.MasterTarifLltt;
            _viewModel.RayonList = MasterListGlobal.MasterRayon;
            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;
            _viewModel.KelurahanList = MasterListGlobal.MasterKelurahan;
            _viewModel.JenisNonAirList = MasterListGlobal.MasterJenisNonAir;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ListJenisPelanggan()
        {
            _viewModel.JenisPelangganKategori = new ObservableCollection<JenisPelangganDto>
            {
                new JenisPelangganDto() { NamaJenisPelanggan = "Pelanggan" },
                new JenisPelangganDto() { NamaJenisPelanggan = "Non Pelanggan" },
            };

            _viewModel.JenisPelangganList = new ObservableCollection<JenisPelangganDto>
            {
                new JenisPelangganDto() { NamaJenisPelanggan = "Pelanggan Air" },
                new JenisPelangganDto() { NamaJenisPelanggan = "Pelanggan Limbah" },
                new JenisPelangganDto() { NamaJenisPelanggan = "Pelanggan L2T2" },
                new JenisPelangganDto() { NamaJenisPelanggan = "Non Pelanggan" },
            };
        }
    }
}
