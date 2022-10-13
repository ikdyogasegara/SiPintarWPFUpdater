using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.RotasiMeter
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly RotasiMeterViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(RotasiMeterViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.DataList = null;

            var dg = await DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "DistribusiRootDialog", "Mohon tunggu", "sedang memuat data attribute...", "");

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterGolongan",
                "MasterDiameter",
                "MasterRayon",
                "MasterArea",
                "MasterWilayah",
                "MasterKelurahan",
                "MasterKecamatan",
                "MasterCabang",
                "MasterUser",
                "MasterBlok",
                "MasterSumberAir",
                "MasterJenisBangunan",
                "MasterKepemilikan",
                "MasterPeruntukan",
                "MasterDma",
                "MasterDmz",
                "MasterJenisGantiMeter",
            });

            _viewModel.GolonganList = MasterListGlobal.MasterGolongan;
            _viewModel.DiameterList = MasterListGlobal.MasterDiameter;
            _viewModel.RayonList = MasterListGlobal.MasterRayon;
            _viewModel.AreaList = MasterListGlobal.MasterArea;
            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;
            _viewModel.KelurahanList = MasterListGlobal.MasterKelurahan;
            _viewModel.KecamatanList = MasterListGlobal.MasterKecamatan;
            _viewModel.CabangList = MasterListGlobal.MasterCabang;
            _viewModel.UserList = MasterListGlobal.MasterUser;
            _viewModel.PekerjaanList = MasterListGlobal.MasterPekerjaan;
            _viewModel.BlokList = MasterListGlobal.MasterBlok;
            _viewModel.SumberAirList = MasterListGlobal.MasterSumberAir;
            _viewModel.JenisBangunanList = MasterListGlobal.MasterJenisBangunan;
            _viewModel.KepemilikanBangunanList = MasterListGlobal.MasterKepemilikan;
            _viewModel.PeruntukanList = MasterListGlobal.MasterPeruntukan;
            _viewModel.DmaList = MasterListGlobal.MasterDma;
            _viewModel.DmzList = MasterListGlobal.MasterDmz;
            _viewModel.JenisGantiMeterList = MasterListGlobal.MasterJenisGantiMeter;

            DialogHelper.CloseDialog(_isTest, true, "DistribusiRootDialog", dg);

            await Task.FromResult(_isTest);
        }

    }
}
