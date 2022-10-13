using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.PelangganAir
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PelangganAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(PelangganAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            SetComboBox();
            await Task.FromResult(_isTest);
        }

        private void SetComboBox()
        {
            if (!_isTest)
            {
                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterStatus",
                }).ContinueWith(result =>
                {
                    _viewModel.StatusList = JArray.FromObject(MasterListGlobal.MasterStatus).ToObject<ObservableCollection<MasterStatusDto>>();
                    _viewModel.StatusList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                     "MasterGolongan",
                }).ContinueWith(result =>
                {
                    _viewModel.GolonganList = JArray.FromObject(MasterListGlobal.MasterGolongan).ToObject<ObservableCollection<MasterGolonganDto>>();
                    _viewModel.GolonganList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterDiameter",
                }).ContinueWith(result =>
                {
                    _viewModel.DiameterList = JArray.FromObject(MasterListGlobal.MasterDiameter).ToObject<ObservableCollection<MasterDiameterDto>>();
                    _viewModel.DiameterList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterKelurahan",
                }).ContinueWith(result =>
                {
                    _viewModel.KelurahanList = JArray.FromObject(MasterListGlobal.MasterKelurahan).ToObject<ObservableCollection<MasterKelurahanDto>>();
                    _viewModel.KelurahanList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterCabang",
                }).ContinueWith(result =>
                {
                    _viewModel.CabangList = JArray.FromObject(MasterListGlobal.MasterCabang).ToObject<ObservableCollection<MasterCabangDto>>();
                    _viewModel.CabangList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterKecamatan",
                }).ContinueWith(result =>
                {
                    _viewModel.KecamatanList = JArray.FromObject(MasterListGlobal.MasterKecamatan).ToObject<ObservableCollection<MasterKecamatanDto>>();
                    _viewModel.KecamatanList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterRayon",
                }).ContinueWith(result =>
                {
                    _viewModel.RayonList = JArray.FromObject(MasterListGlobal.MasterRayon).ToObject<ObservableCollection<MasterRayonDto>>();
                    _viewModel.RayonList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterWilayah",
                }).ContinueWith(result =>
                {
                    _viewModel.WilayahList = JArray.FromObject(MasterListGlobal.MasterWilayah).ToObject<ObservableCollection<MasterWilayahDto>>();
                    _viewModel.WilayahList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterKolektif",
                }).ContinueWith(result =>
                {
                    _viewModel.KolektifList = JArray.FromObject(MasterListGlobal.MasterKolektif).ToObject<ObservableCollection<MasterKolektifDto>>();
                    _viewModel.KolektifList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterFlag",
                }).ContinueWith(result =>
                {
                    _viewModel.FlagList = JArray.FromObject(MasterListGlobal.MasterFlag).ToObject<ObservableCollection<MasterFlagDto>>();
                    _viewModel.FlagList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterKondisiMeter",
                }).ContinueWith(result =>
                {
                    _viewModel.KondisiMeterList = JArray.FromObject(MasterListGlobal.MasterKondisiMeter).ToObject<ObservableCollection<MasterKondisiMeterDto>>();
                    _viewModel.KondisiMeterList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterAdministrasiLain",
                }).ContinueWith(result =>
                {
                    _viewModel.AdministrasiLainList = JArray.FromObject(MasterListGlobal.MasterAdministrasiLain).ToObject<ObservableCollection<MasterAdministrasiLainDto>>();
                    _viewModel.AdministrasiLainList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterPemeliharaanLain",
                }).ContinueWith(result =>
                {
                    _viewModel.PemeliharaanLainList = JArray.FromObject(MasterListGlobal.MasterPemeliharaanLain).ToObject<ObservableCollection<MasterPemeliharaanLainDto>>();
                    _viewModel.PemeliharaanLainList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterRetribusiLain",
                }).ContinueWith(result =>
                {
                    _viewModel.RetribusiLainList = JArray.FromObject(MasterListGlobal.MasterRetribusiLain).ToObject<ObservableCollection<MasterRetribusiLainDto>>();
                    _viewModel.RetribusiLainList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterBlok",
                }).ContinueWith(result =>
                {
                    _viewModel.BlokList = JArray.FromObject(MasterListGlobal.MasterBlok).ToObject<ObservableCollection<MasterBlokDto>>();
                    _viewModel.BlokList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterMerekMeter",
                }).ContinueWith(result =>
                {
                    _viewModel.MerekMeterList = JArray.FromObject(MasterListGlobal.MasterMerekMeter).ToObject<ObservableCollection<MasterMerekMeterDto>>();
                    _viewModel.MerekMeterList ??= new();
                });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterSumberAir",
                }).ContinueWith(result =>
                {
                    _viewModel.SumberAirList = JArray.FromObject(MasterListGlobal.MasterSumberAir).ToObject<ObservableCollection<MasterSumberAirDto>>();
                    _viewModel.SumberAirList ??= new();
                });
            }
        }
    }
}
