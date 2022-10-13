using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly InteraksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(InteraksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "AkuntansiRootDialog", "Mohon tunggu", "sedang memuat data attribute...");

            try
            {
                _viewModel.SelectedPage = (parameter as string)!;
                _viewModel.UpdatePage(_viewModel.SelectedPage!);


                await UpdateListData.ProcessAsync(_isTest, new List<string>()
                    {
                        "MasterGolongan",
                        "MasterWilayah",
                        "MasterPerkiraan1",
                        "MasterPerkiraan2",
                        "MasterJenisBarang",
                        "MasterKeperluan",
                        "MasterPeriodeAkuntansi",
                        "MasterParameterAkuntansi",
                        "MasterPerkiraan3",
                        "MasterJenisNonAir"
                    });


                #region InPelayanan
                _viewModel.WilayahList = MasterListGlobal.MasterWilayah;
                _viewModel.GolonganList = MasterListGlobal.MasterGolongan;
                _viewModel.JenisNonAirList = MasterListGlobal.MasterJenisNonAir;

                _viewModel.Perkiraan3DebetList = new ObservableCollection<MasterPerkiraan3Dto>(MasterListGlobal.MasterPerkiraan3.OrderBy(x => x.KodePerkiraan3));
                _viewModel.Perkiraan3KreditList = new ObservableCollection<MasterPerkiraan3Dto>(MasterListGlobal.MasterPerkiraan3.OrderBy(x => x.KodePerkiraan3));
                _viewModel.Perkiraan3NonAirList = new ObservableCollection<MasterPerkiraan3Dto>(MasterListGlobal.MasterPerkiraan3.OrderBy(x => x.KodePerkiraan3));
                #endregion

                #region InJenisPersediaan
                _viewModel.JenisBarangList = MasterListGlobal.MasterJenisBarang;
                _viewModel.KeperluanList = MasterListGlobal.MasterKeperluan;
                var debetList = new List<int> { 3, 5, 7 };
                _viewModel.PerkiraanDebet = new(MasterListGlobal.MasterPerkiraan3.Where(x => x.IdJenisVoucher != null && debetList.Contains((int)x.IdJenisVoucher)));
                _viewModel.PerkiraanKredit = new(MasterListGlobal.MasterPerkiraan3.Where(x => x.IdJenisVoucher != null && (int)x.IdJenisVoucher == 1));
                #endregion

                #region InPenyusutan
                _viewModel.AkunAktivaList = new(MasterListGlobal.MasterPerkiraan3.Where(x => x.IdJenisVoucher != null && (int)x.IdJenisVoucher == 5));

                var akmPenyIds = MasterListGlobal.MasterParameterAkuntansi.Where(x => x.IdJenisParameter == 6).Select(x => x.IdPerkiraan);
                if (akmPenyIds != null)
                    _viewModel.AkunAkumPenyusutanList = new(MasterListGlobal.MasterPerkiraan3.Where(x => akmPenyIds.Contains(x.IdPerkiraan3)));

                var biayaList = new List<int> { 3, 7 };
                _viewModel.AkunBiayaList = new(MasterListGlobal.MasterPerkiraan3.Where(x => x.IdJenisVoucher != null && biayaList.Contains((int)x.IdJenisVoucher)));

                var aktivaIds = _viewModel.AkunAktivaList.DistinctBy(x => x.IdPerkiraan2).Select(x => x.IdPerkiraan2);
                if (aktivaIds != null)
                    _viewModel.AkunAktivaListSecond = new(MasterListGlobal.MasterPerkiraan2.Where(x => aktivaIds.Contains(x.IdPerkiraan2)));
                #endregion


            }
            catch (Exception e)
            {
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            finally
            {
                DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);
            }
        }
    }
}
