using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Permohonan
{
    public partial class DaftarOngkosView : UserControl
    {
        private readonly PermohonanViewModel Vm;
        private readonly bool? _flagBiayaDibebankanKePdam;
        private readonly bool? _flagDialihkanKeVendor;

        public DaftarOngkosView(object dataContext, bool flagBiayaDibebankanKePdam = false, bool flagDialihkanKeVendor = false)
        {
            InitializeComponent();
            Vm = dataContext as PermohonanViewModel;
            DataContext = Vm;
            _flagBiayaDibebankanKePdam = flagBiayaDibebankanKePdam;
            _flagDialihkanKeVendor = flagDialihkanKeVendor;

            NamaOngkos.Text = "";
            KodeOngkos.Text = "";
            PreviewKeyUp += DaftarOngkosView_PreviewKeyUp;

            Vm.CurrentPageMaterialOngkos = 1;
            Vm.LimitDataMaterialOngkos = new KeyValuePair<int, string>(10, "10");
        }

        private void DaftarOngkosView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(null, null);
            }
        }

        private void PilihOngkos_Click(object sender, RoutedEventArgs e)
        {
            if ((Vm.SelectedPaketRabPipaPersil != null || Vm.SelectedPaketRabPipaDistribusi != null) && Vm.SelectedDaftarOngkos != null)
            {
                _ = DialogHost.Show(new TambahOngkosView(Vm, _flagBiayaDibebankanKePdam, _flagDialihkanKeVendor), "InnerSecondGlobalRootDialog");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "currentPage", Vm.CurrentPageMaterialOngkos },
                { "pageSize", Vm.LimitDataMaterialOngkos.Key },
                { "NamaOngkos", NamaOngkos.Text },
                { "KodeOngkos", KodeOngkos.Text },
            };
            _ = ((AsyncCommandBase)Vm.OnCariOngkosCommand).ExecuteAsync(param);
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            Vm.CurrentPageMaterialOngkos--;
            Button_Click(null, null);
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            Vm.CurrentPageMaterialOngkos++;
            Button_Click(null, null);
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Button_Click(null, null);
        }
    }
}
