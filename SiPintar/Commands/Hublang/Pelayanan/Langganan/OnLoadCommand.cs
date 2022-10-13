using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Langganan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly LanggananViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(LanggananViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.SelectedJenisPelanggan = parameter as string;
            _viewModel.DataList = null;
            _viewModel.IsTabKoreksi = false;

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterRayon",
                "MasterArea",
                "MasterWilayah",
                "MasterKelurahan",
                "MasterKecamatan",
                "MasterGolongan",
                "MasterTarifLimbah",
                "MasterTarifLltt",
                "MasterDiameter",
                "MasterCabang",
                "MasterPekerjaan",
                "MasterBlok",
                "MasterSumberAir",
                "MasterJenisBangunan",
                "MasterKepemilikan",
                "MasterPeruntukan",
                "MasterStatus",
                "MasterFlag",
                "MasterAdministrasiLain",
                "MasterPemeliharaanLain",
                "MasterRetribusiLain",
                "MasterMerekMeter",
                "MasterKondisiMeter",
                "MasterDma",
                "MasterDmz",
                "MasterKolektif"
            });

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
            _viewModel.StatusList = MasterListGlobal.MasterStatus;
            _viewModel.FlagList = MasterListGlobal.MasterFlag;
            _viewModel.GolonganList = MasterListGlobal.MasterGolongan;
            _viewModel.TarifLimbahList = MasterListGlobal.MasterTarifLimbah;
            _viewModel.TarifLlttList = MasterListGlobal.MasterTarifLltt;
            _viewModel.DiameterList = MasterListGlobal.MasterDiameter;
            _viewModel.DmaList = MasterListGlobal.MasterDma;
            _viewModel.DmzList = MasterListGlobal.MasterDmz;
            _viewModel.AdministrasiLainList = MasterListGlobal.MasterAdministrasiLain;
            _viewModel.PemeliharaanLainList = MasterListGlobal.MasterPemeliharaanLain;
            _viewModel.RetribusiLainList = MasterListGlobal.MasterRetribusiLain;
            _viewModel.MerekMeterList = MasterListGlobal.MasterMerekMeter;
            _viewModel.KondisiMeterList = MasterListGlobal.MasterKondisiMeter;
            _viewModel.KolektifList = MasterListGlobal.MasterKolektif;

            _viewModel.OnResetFilterCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
