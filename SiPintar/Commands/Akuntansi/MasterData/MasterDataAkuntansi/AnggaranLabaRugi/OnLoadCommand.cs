using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranLabaRugi
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly AnggaranLabaRugiViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(AnggaranLabaRugiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "AkuntansiRootDialog", "Mohon tunggu", "sedang memproses tindakan...");

            await UpdateListData.ProcessAsync(_isTest, new List<string>() { "MasterPeriodeAkuntansi", "MasterWilayah" });

            _viewModel.DataList = new();
            _viewModel.DataJenisList = new();
            _viewModel.DataUraianList = new();
            _viewModel.SelectedTahun = new();
            _viewModel.SelectedWilayah = new();
            _viewModel.SelectedDataJenis = new();
            _viewModel.SelectedDataUraian = new();
            _viewModel.IsEmptyJenis = true;

            _viewModel.PeriodeTutupBukuList = new(MasterListGlobal.MasterPeriodeAkuntansi.Where(x => x.FlagTutupBuku == true));

            var tahunList = MasterListGlobal.MasterPeriodeAkuntansi.OrderBy(x => x.Tahun).Select(x => x.Tahun).Distinct();
            if (tahunList != null)
                _viewModel.TahunList = new(tahunList.Select((val, i) => new KeyValuePair<int, string>(i, val.ToString()!)));

            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;
            if (_viewModel.WilayahList.Count > 0 && _viewModel.WilayahList[0].IdWilayah != null)
                _viewModel.WilayahList.Insert(0, new MasterWilayahDto { KodeWilayah = "--", NamaWilayah = "KANTOR PUSAT" });

            SetTipeAkuntansiKonsolidasi();

            DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void SetTipeAkuntansiKonsolidasi()
        {
            if (!_isTest && Application.Current.Resources["TipeAkuntansi"].ToString() == "Konsolidasi")
            {
                _viewModel.IsKonsolidasi = true;
                _viewModel.SelectedWilayah = _viewModel.WilayahList[0];
            }
        }
    }
}
