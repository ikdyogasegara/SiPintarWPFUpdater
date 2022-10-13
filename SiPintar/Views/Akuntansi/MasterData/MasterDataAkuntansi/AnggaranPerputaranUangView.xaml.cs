using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi
{
    /// <summary>
    /// Interaction logic for AnggaranPerputaranUangView.xaml
    /// </summary>
    public partial class AnggaranPerputaranUangView : UserControl
    {
        public AnggaranPerputaranUangView()
        {
            InitializeComponent();
            IsLoadingUraian.Visibility = Visibility.Collapsed;
            IsEmptyUraian.Visibility = Visibility.Visible;
            CheckButton();
        }

        private void HideGrid_Click(object sender, RoutedEventArgs e)
        {
            DataGridJenisTotal.Visibility = Visibility.Collapsed;
            DataGridJenisBorder.Width = new GridLength(0, GridUnitType.Pixel);
        }

        private void ShowGrid_Click(object sender = null, RoutedEventArgs e = null)
        {
            DataGridJenisTotal.Visibility = Visibility.Visible;
            DataGridJenisBorder.Width = new GridLength(350, GridUnitType.Pixel);
        }

        private void DataJenis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsLoadingUraian.Visibility = Visibility.Visible;
            IsEmptyUraian.Visibility = Visibility.Visible;
            var vm = DataContext as AnggaranPerputaranUangViewModel;
            if (vm != null && vm.SelectedDataJenis.Value != null)
            {
                TextBlockJenis.Text = $"Jenis : {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(vm.SelectedDataJenis.Value.ToLower())}";

                if (vm.IsRekap)
                {
                    vm.DataUraianList = new ObservableCollection<LaporanPerputaranUangDto>(
                        vm.LaporanPerputaranUang.Where(x => x.Header == vm.SelectedDataJenis.Value)
                                                .GroupBy(x => x.Uraian)
                                                .Select(y => new LaporanPerputaranUangDto
                                                {
                                                    IdPdam = y.First().IdPdam,
                                                    Header = y.First().Header,
                                                    IdKelompok = y.First().IdKelompok,
                                                    Uraian = y.First().Uraian,
                                                    Tahun = y.First().Tahun,
                                                    Anggaran1 = y.Sum(z => z.Anggaran1),
                                                    Anggaran2 = y.Sum(z => z.Anggaran2),
                                                    Anggaran3 = y.Sum(z => z.Anggaran3),
                                                    Anggaran4 = y.Sum(z => z.Anggaran4),
                                                    Anggaran5 = y.Sum(z => z.Anggaran5),
                                                    Anggaran6 = y.Sum(z => z.Anggaran6),
                                                    Anggaran7 = y.Sum(z => z.Anggaran7),
                                                    Anggaran8 = y.Sum(z => z.Anggaran8),
                                                    Anggaran9 = y.Sum(z => z.Anggaran9),
                                                    Anggaran10 = y.Sum(z => z.Anggaran10),
                                                    Anggaran11 = y.Sum(z => z.Anggaran11),
                                                    Anggaran12 = y.Sum(z => z.Anggaran12)
                                                }));
                }
                else
                {
                    vm.DataUraianList = new ObservableCollection<LaporanPerputaranUangDto>(vm.LaporanPerputaranUang.Where(x => x.Uraian == vm.SelectedDataJenis.Value));
                }

                if (vm.DataUraianList.Count > 0)
                    IsEmptyUraian.Visibility = Visibility.Collapsed;
            }
            IsLoadingUraian.Visibility = Visibility.Collapsed;
        }

        private void ButtonPilih_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as AnggaranPerputaranUangViewModel;
            if (vm != null)
            {
                vm.DataUraianList = new ObservableCollection<LaporanPerputaranUangDto>();
                vm.SelectedDataJenis = new KeyValuePair<string, string>();
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        #region Unchecked Filter
        private void CheckKodePerkiraan_Unchecked(object sender, RoutedEventArgs e)
        {
            KodePerkiraan.Text = string.Empty;
            var vm = DataContext as AnggaranPerputaranUangViewModel;
            if (vm != null)
            {
                vm.FilterKodePerkiraan = string.Empty;
            }
        }

        private void CheckNamaPerkiraan_Unchecked(object sender, RoutedEventArgs e)
        {
            NamaPerkiraan.Text = string.Empty;
            var vm = DataContext as AnggaranPerputaranUangViewModel;
            if (vm != null)
            {
                vm.FilterNamaPerkiraan = string.Empty;
            }
        }
        #endregion

        #region Validation Field
        private void CheckButton()
        {
            if (ComboTahun.SelectedIndex == -1 || ComboKodeWilayah.SelectedIndex == -1)
                BtnPilih.IsEnabled = false;
            else
                BtnPilih.IsEnabled = true;


            if (string.IsNullOrEmpty(NamaPerkiraan.Text) && string.IsNullOrEmpty(KodePerkiraan.Text))
                BtnTampilkan.IsEnabled = false;
            else
                BtnTampilkan.IsEnabled = true;

        }

        private void ComboTahun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void KodePerkiraan_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckButton();
        }

        private void NamaPerkiraan_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckButton();
        }

        private void ComboKodeWilayah_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
            var vm = DataContext as AnggaranPerputaranUangViewModel;
            if (vm != null)
            {
                if (!vm.IsKonsolidasi && ComboKodeWilayah.SelectedIndex == 0)
                {
                    BtnSaldoAnggaran.Visibility = Visibility.Collapsed;
                    CtxMenuSaldoAnggaran.Visibility = Visibility.Collapsed;
                }
                else
                {
                    BtnSaldoAnggaran.Visibility = Visibility.Visible;
                    CtxMenuSaldoAnggaran.Visibility = Visibility.Visible;
                }
            }
        }
        #endregion

        private void DataGridJenisRekap_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ComboTahun.SelectedIndex = -1;
            ComboKodeWilayah.SelectedIndex = -1;
            ComboNamaWilayah.SelectedIndex = -1;
        }

    }
}
