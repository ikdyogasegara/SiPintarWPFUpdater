using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using IniFileParser.Model;
using Newtonsoft.Json;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;

namespace SiPintar.Commands.Billing.Supervisi.PenghapusanRekening
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(PenghapusanRekeningViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.RekeningAirHapusSecaraAkuntansiList = new ObservableCollection<RekeningAirHapusSecaraAkuntansiWpf> { };
            _viewModel.Filter.IdFlag = 0;
            _viewModel.IsUsulan = true;
            _viewModel.SelectedData = null;

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterPeriode",
                "MasterGolongan",
                "MasterKelurahan",
                "MasterCabang",
                "MasterRayon",
                "MasterCabang",
                "MasterWilayah",
            });

            var tahun = MasterListGlobal.MasterPeriode.Select(p => p.Tahun).Distinct();
            _viewModel.TahunPeriodeList = JsonConvert.DeserializeObject<ObservableCollection<int?>>(JsonConvert.SerializeObject(tahun));
            _viewModel.GolonganList = MasterListGlobal.MasterGolongan;
            _viewModel.KelurahanList = MasterListGlobal.MasterKelurahan;
            _viewModel.CabangList = MasterListGlobal.MasterCabang;
            _viewModel.RayonList = MasterListGlobal.MasterRayon;
            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;

            await Task.FromResult(_isTest);
        }
    }
}
